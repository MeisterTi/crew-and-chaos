using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [SerializeField] private float depthBeforeSubmerged = 1;
    [SerializeField] private float displacementAmount = 3;
    [SerializeField] private Rigidbody rb;

    private void Update()
    {
        if (transform.position.y < 0f)
        {
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
        }
    }
}
