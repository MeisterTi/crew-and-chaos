using UnityEngine;

public class SimpleBoatPhysics : MonoBehaviour
{
    [SerializeField] private Transform[] floaters;
    [SerializeField] private float buoyancyForce = 15f;
    [SerializeField] private float waterLevel = 0f;
    [SerializeField] private float dragInWater = 1f;
    [SerializeField] private float angularDragInWater = 0.5f;
    private Rigidbody rb;
    private int floatersUnderwater;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
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
}
