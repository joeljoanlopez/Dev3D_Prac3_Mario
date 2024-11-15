using UnityEngine;

public class MarioController : MonoBehaviour
{
    public Camera camera;
    private Animator animator;
    private CharacterController characterController;

    [Header("Speeds")]
    public float walkSpeed = 2.0f;
    public float runSpeed = 8.0f;
    public float rotationSpeed = 1.0f;

    [Header("Input")]
    public KeyCode leftKeyCode = KeyCode.A;
    public KeyCode rightKeyCode = KeyCode.D;
    public KeyCode upKeyCode = KeyCode.W;
    public KeyCode downKeyCode = KeyCode.D;
    public KeyCode runKeyCode = KeyCode.LeftShift;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
        forward.y = 0.0f;
        right.y = 0.0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(leftKeyCode))
        {
            movement -= right;
        }
        if (Input.GetKey(rightKeyCode))
        {
            movement += right;
        }
        if (Input.GetKey(upKeyCode))
        {
            movement += forward;
        }
        if (Input.GetKey(downKeyCode))
        {
            movement -= forward;
        }
        movement.Normalize();
        bool hasMovement = movement != Vector3.zero;
        float speed = 0.0f;

        if (hasMovement)
        {
            if (Input.GetKey(runKeyCode))
            {
                speed = runSpeed;
                animator.SetFloat("Speed", 1.0f);
            }
            else
            {
                speed = walkSpeed;
                animator.SetFloat("Speed", 0.2f);
            }
            Quaternion desiredRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }


        movement = movement * speed * Time.deltaTime;
        characterController.Move(movement);
    }
}