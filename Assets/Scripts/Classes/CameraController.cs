using System;
using UnityEngine;
using UnityEngine.Accessibility;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraDistance;
    public float pitchSpeed = 1.0f;
    public float yawSpeed = 1.0f;
    public float minPitch = -60;
    public float maxPitch = 60;
    private float pitch;
    private float yaw;
    private void LateUpdate()
    {
        float horizontalAxis = Input.GetAxis("Mouse X");
        float verticalAxis = Input.GetAxis("Mouse Y");

        yaw += horizontalAxis * yawSpeed * Time.deltaTime * Mathf.Deg2Rad;
        pitch += verticalAxis * pitchSpeed * Time.deltaTime * Mathf.Deg2Rad;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        float yawRads = yaw * Mathf.Deg2Rad;
        float pitchRads = pitch * Mathf.Deg2Rad;

        Vector3 cameraForward = new Vector3(Mathf.Sin(yawRads) * Mathf.Cos(pitchRads), Mathf.Sin(pitchRads), Mathf.Cos(yawRads) * Mathf.Cos(pitchRads));
        Vector3 desiredPosition = target.position - cameraForward * cameraDistance;
        transform.position = desiredPosition;
        transform.LookAt(target.position);
    }
}