using UnityEngine;

public class MarioController : MonoBehaviour
{
    private CharacterController characterController;
    public Camera camera;
    private Animator animator;

    [Header("Speeds")]
    public float walkSpeed = 2.0f;
    public float runSpeed = 8.0f;
    public float rotationSpeed = 1.0f;
    float verticalSpeed = 0.0f;

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
        bool hasMovement = movement != Vector3.zero; // I May need to set it in each input
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
        verticalSpeed += Physics.gravity.y * Time.deltaTime;
        movement.y = verticalSpeed;

        CollisionFlags collisionFlags = characterController.Move(movement);
        if ((collisionFlags & CollisionFlags.Below) != 0 || (collisionFlags & CollisionFlags.Above) != 0 && verticalSpeed > 0.0f)
        {
            verticalSpeed = 0;
        }
    }
}