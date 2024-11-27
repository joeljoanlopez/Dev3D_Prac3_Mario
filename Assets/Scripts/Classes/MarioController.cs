using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class MarioController : MonoBehaviour, RestartGameElement
{
    private CharacterController characterController;
    public Camera camera;
    private Animator animator;

    [Header("Speeds")]
    public float walkSpeed = 2.0f;
    public float runSpeed = 8.0f;
    public float rotationSpeed = 1.0f;
    private float verticalSpeed = 0.0f;
    public float VerticalSpeed { get { return verticalSpeed; } }

    [Header("Jump")]
    public float jumpVerticalSpeed = 5.0f;
    public float waitStartJumpTime = 0.12f;
    public float fallingVerticalSpeedMultiplier = 0.1f;

    [Header("Input")]
    public KeyCode leftKeyCode = KeyCode.A;
    public KeyCode rightKeyCode = KeyCode.D;
    public KeyCode upKeyCode = KeyCode.W;
    public KeyCode downKeyCode = KeyCode.D;
    public KeyCode runKeyCode = KeyCode.LeftShift;
    public KeyCode jumpKeyCode = KeyCode.Space;

    // Game manager vars
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private CheckpointController currentCheckpoint;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.fireEvents = false;
        GameManager.GetGameManager().AddRestartGameElement(this);
        startingPosition = transform.position;
        startingRotation = transform.rotation;
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

        if (CanJump() && Input.GetKeyDown(jumpKeyCode))
        {
            Jump();
        }

        movement = movement * speed * Time.deltaTime;
        verticalSpeed += Physics.gravity.y * Time.deltaTime;
        movement.y = verticalSpeed * Time.deltaTime;

        CollisionFlags collisionFlags = characterController.Move(movement);
        if ((collisionFlags & CollisionFlags.Below) != 0 && verticalSpeed < 0.0f)
        {
            animator.SetBool("Falling", false);
        }
        else
        {
            animator.SetBool("Falling", true);
        }
        if ((collisionFlags & CollisionFlags.Below) != 0 && (verticalSpeed < 0.0f) || (collisionFlags & CollisionFlags.Above) != 0 && (verticalSpeed > 0.0f))
        {
            verticalSpeed = 0;
        }

    }

    private bool CanJump()
    {
        //TODO fix this
        return true;
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        StartCoroutine(ExecuteJump());
    }

    IEnumerator ExecuteJump()
    {
        yield return new WaitForSeconds(waitStartJumpTime);
        verticalSpeed = jumpVerticalSpeed;
        animator.SetBool("Falling", false);
    }

    public void SetCheckpoint(CheckpointController newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    public void RestartGame()
    {
        characterController.enabled = false;
        if (!currentCheckpoint)
        {
            transform.position = startingPosition;
            transform.rotation = startingRotation;
        }
        else
        {
            transform.position = currentCheckpoint.respawnPoint.position;
            transform.rotation = currentCheckpoint.respawnPoint.rotation;
        }
        characterController.enabled = true;
    }
}