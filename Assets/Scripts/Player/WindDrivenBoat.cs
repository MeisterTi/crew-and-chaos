using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WindDrivenBoat : NetworkSingleton<WindDrivenBoat>
{
    [Header("Floating Settings")]
    [SerializeField] private Transform[] floaters;
    [SerializeField] private float buoyancyForce = 15f;
    [SerializeField] private float waterLevel = 0f;
    [SerializeField] private float dragInWater = 1f;
    [SerializeField] private float angularDragInWater = 0.5f;

    [Header("Wind Settings")]
    public Transform sailTransform;
    public float sailEfficiency = 1.0f;
    public float maxWindForce = 50f;
    [SerializeField] private Rigidbody sailRigidbody;

    private Rigidbody rb;
    private int floatersUnderwater;
    private bool isSailTied = false;
    private float sailTension = 1f; // 0 = loose, 1 = fully tight

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!IsServer) return;
        ApplyBuoyancy();
        ApplyWindTorque();
        ApplyWindForce();
    }

    private void OnDrawGizmos()
    {
        if (sailTransform == null) return;
        Vector3 pos = sailTransform.position;
        Gizmos.color = Color.red;
        Vector3 windDir = WindMap.Instance.GetWindAt(pos).normalized;
        Gizmos.DrawLine(pos, pos + windDir * 5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + sailTransform.forward * 5f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos, pos + sailTransform.right * 5f);
    }

    public void SetSailTension(float tension)
    {
        sailTension = Mathf.Clamp01(tension);
    }


    private void ApplyBuoyancy()
    {
        floatersUnderwater = 0;

        foreach (Transform floater in floaters)
        {
            float difference = floater.position.y - waterLevel;

            if (difference < 0f)
            {
                rb.AddForceAtPosition(Vector3.up * buoyancyForce * Mathf.Abs(difference), floater.position, ForceMode.Force);
                floatersUnderwater++;
            }
        }

        if (floatersUnderwater > 0)
        {
            rb.linearDamping = dragInWater;
            rb.angularDamping = angularDragInWater;
        }
        else
        {
            rb.linearDamping = 0f;
            rb.angularDamping = 0.05f;
        }
    }

    private void ApplyWindForce()
    {
        if (sailTransform == null) return;

        Vector3 wind = WindMap.Instance.GetWindAt(transform.position);
        float windSpeed = wind.magnitude;
        Vector3 windDir = wind.normalized;

        // Use sail surface normal (likely right or up, depending on your model)
        Vector3 sailNormal = sailTransform.right;

        // Calculate how perpendicular the sail is to wind
        float alignment = Vector3.Dot(windDir, sailNormal);
        float effectiveness = Mathf.Abs(alignment); // max at 90°, low at 0° or 180°

        effectiveness = Mathf.Clamp01(effectiveness);

        Vector3 forwardForce = transform.forward * windSpeed * effectiveness * sailEfficiency;
        forwardForce = Vector3.ClampMagnitude(forwardForce, maxWindForce);

        rb.AddForce(forwardForce);

        // Optional: reduce sideways drift
        Vector3 localVel = transform.InverseTransformDirection(rb.linearVelocity);
        localVel.x *= 0.1f;
        rb.linearVelocity = transform.TransformDirection(localVel);

        Debug.Log($"[EFFICIENCY] Dot (Abs): {alignment} → {effectiveness}, WindForce: {forwardForce}");
    }



    void ApplyWindTorque()
    {
        if (sailTransform == null || sailRigidbody == null || isSailTied) return;

        Vector3 wind = WindMap.Instance.GetWindAt(sailTransform.position);
        wind.y = 0f;
        if (wind.sqrMagnitude < 0.001f) return;

        Vector3 sailForward = sailTransform.forward;
        float tensionFactor = Mathf.Lerp(0f, 90f, sailTension);
        float angleDiff = Vector3.SignedAngle(sailForward, wind.normalized, Vector3.up);
        angleDiff = Mathf.Clamp(angleDiff, -tensionFactor, tensionFactor);

        // Adjust torque based on remaining allowed swing
        float torqueStrength = 5f;
        Vector3 torque = Vector3.up * angleDiff * torqueStrength;

        sailRigidbody.AddTorque(torque);
    }

    public void TieSail()
    {
        isSailTied = true;

        // Freeze rotation completely (or partially)
        sailRigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        sailRigidbody.angularVelocity = Vector3.zero;
    }

    public void UntieSail()
    {
        isSailTied = false;

        // Remove rotation freeze constraints
        sailRigidbody.constraints = RigidbodyConstraints.None;
    }

}