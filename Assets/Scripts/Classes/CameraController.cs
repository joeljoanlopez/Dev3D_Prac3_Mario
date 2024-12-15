using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Accessibility;

public class CameraController : MonoBehaviour
{
    public Transform followObject;
    public LayerMask layerMask;

    [Header("Limits")]
    public float minCameraDistance = 5.0f;
    public float maxCameraDistance = 10.0f;
    public float maxPitch = 60;
    public float minPitch = -60;
    public float offsetHit = 0.1f;

    [Header("Sensitivity")]
    public float pitchSpeed = 1.0f;
    public float yawSpeed = 1.0f;
    private float pitch;
    private float yaw;

    private void LateUpdate()
    {
        float horizontalAxis = Input.GetAxis("Mouse X");
        float verticalAxis = Input.GetAxis("Mouse Y");

        Vector3 lookDirection = followObject.position - transform.position;
        lookDirection.y = 0.0f;
        lookDirection.Normalize();
        yaw = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;

        float distanceToPlayer = lookDirection.magnitude;
        distanceToPlayer = Mathf.Clamp(distanceToPlayer, minCameraDistance, maxCameraDistance);


        yaw += horizontalAxis * yawSpeed * Time.deltaTime * Mathf.Deg2Rad;
        pitch += verticalAxis * pitchSpeed * Time.deltaTime * Mathf.Deg2Rad;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        float yawRads = yaw * Mathf.Deg2Rad;
        float pitchRads = pitch * Mathf.Deg2Rad;


        Vector3 cameraForward = new Vector3(Mathf.Sin(yawRads) * Mathf.Cos(pitchRads), Mathf.Sin(pitchRads), Mathf.Cos(yawRads) * Mathf.Cos(pitchRads));
        Vector3 desiredPosition = followObject.position - cameraForward * distanceToPlayer;

        Ray ray = new Ray(followObject.position, -cameraForward);
        if (Physics.Raycast(ray, out RaycastHit hit, distanceToPlayer, layerMask.value))
        {
            desiredPosition = hit.point + cameraForward * offsetHit;
        }

        transform.position = desiredPosition;
        transform.LookAt(followObject.position);
    }

    public void PlaceBehind()
    {
        Vector3 behindDirection = -followObject.forward;
        Vector3 desiredPosition = followObject.position + behindDirection * minCameraDistance;
        transform.position = desiredPosition;
        transform.LookAt(followObject.position);
    }
}