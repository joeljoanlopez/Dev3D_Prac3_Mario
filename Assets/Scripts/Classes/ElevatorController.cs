using UnityEngine;
public class ElevatorController : MonoBehaviour
{
    public float maxAngleToAttach = 8.0f;
    Collider currentElevator;

    private void Start()
    {
        currentElevator = null;
    }

    private void Update()
    {
        if (currentElevator == null)
        {
            return;
        }
        if (!IsAttachableElevator(currentElevator))
        {
            DetachElevator();
        }
    }

    private void LateUpdate()
    {
        Vector3 angles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0.0f, angles.y, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator") && CanAttachElevator(other))
        {
            AttachElevator(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Elevator") && other == currentElevator)
        {
            DetachElevator();
        }
    }

    bool CanAttachElevator(Collider elevator)
    {
        if (currentElevator != null)
        {
            return false;
        }
        return IsAttachableElevator(elevator);
    }

    bool IsAttachableElevator(Collider elevator)
    {
        float dotAngle = Vector3.Dot(elevator.transform.forward, Vector3.up);
        if (dotAngle >= Mathf.Cos(maxAngleToAttach * Mathf.Deg2Rad))
        {
            return true;
        }
        return false;
    }

    void AttachElevator(Collider elevator)
    {
        transform.SetParent(elevator.transform.parent);
        currentElevator = elevator;
    }

    void DetachElevator()
    {
        currentElevator = null;
        transform.SetParent(null);
    }
}