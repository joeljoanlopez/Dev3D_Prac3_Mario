using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public float bridgeForce = 10.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Bridge"))
        {
            hit.rigidbody.AddForceAtPosition(-hit.normal * bridgeForce, hit.point);
        }
    }
}