using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool GetGroundedState()
    {
        float sphereRadius = 0.2f;
        float sphereCastDistance = 0.5f;
        Vector3 sphereCastOrigin = transform.position + Vector3.up * sphereRadius;

        // Perform the sphere cast
        return Physics.SphereCast(sphereCastOrigin, sphereRadius, Vector3.down, out RaycastHit hit, sphereCastDistance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.2f, 0.5f);
    }
}