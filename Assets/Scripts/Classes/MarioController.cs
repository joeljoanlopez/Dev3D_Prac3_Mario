using System.Collections;
using System.Net.WebSockets;
using JetBrains.Annotations;
using UnityEngine;

public class MarioController : MonoBehaviour, RestartGameElement
{
    public Camera cam;
    private CharacterController characterController;
    private Animator animator;

    [Header("Speeds")]
    public float walkSpeed = 2.0f;
    public float runSpeed = 8.0f;
    public float crouchSpeed = 1.0f;
    public float rotationSpeed = 1.0f;
    public float maxVerticalSpeed = 10.0f;
    public float minVerticalSpeed = -10.0f;
    public float verticalSpeed = 0.0f;

    public float VerticalSpeed { get { return verticalSpeed; } }

    [Header("Input")]
    public KeyCode leftKeyCode = KeyCode.A;
    public KeyCode rightKeyCode = KeyCode.D;
    public KeyCode upKeyCode = KeyCode.W;
    public KeyCode downKeyCode = KeyCode.D;
    public KeyCode runKeyCode = KeyCode.LeftShift;

    [Header("Ground Check")]
    public GroundChecker groundChecker;
    private bool wasGroundedPrevFrame;
    private bool isGrounded;
    private float groundTime;

    private bool isCrouching;
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
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        forward.y = 0.0f;
        right.y = 0.0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = HandleInputs(forward, right);

        bool hasMovement = movement != Vector3.zero; // I May need to set it in each input
        float speed = HandleSpeed(hasMovement, movement);

        movement = movement * speed * Time.deltaTime;
        verticalSpeed += Physics.gravity.y * Time.deltaTime;
        movement.y += verticalSpeed * Time.deltaTime;
        CollisionFlags collisionFlags = characterController.Move(movement);

        wasGroundedPrevFrame = isGrounded;
        isGrounded = groundChecker.GetGroundedState() && verticalSpeed <= 0;
        bool hitSomethingAbove = (characterController.collisionFlags & CollisionFlags.Above) != 0 && (verticalSpeed > 0.0f);
        if (isGrounded)
        {
            animator.SetBool("Falling", false);
        }
        else
        {
            animator.SetBool("Falling", true);
        }
        if (isGrounded || hitSomethingAbove)
        {
            verticalSpeed = -2.0f;
        }
        if (!wasGroundedPrevFrame && isGrounded)
        {
            groundTime = Time.time;
        }
        verticalSpeed = Mathf.Clamp(verticalSpeed, minVerticalSpeed, maxVerticalSpeed);
    }

    Vector3 HandleInputs(Vector3 forward, Vector3 right)
    {
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

        return movement;
    }

    float HandleSpeed(bool isMoving, Vector3 movement)
    {
        float speed = 0.0f;
        if (isMoving)
        {
            if (Input.GetKey(runKeyCode))
            {
                speed = runSpeed;
                animator.SetFloat("Speed", 1.0f);
            }
            else if (isCrouching)
            {
                speed = crouchSpeed;
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

        return speed;
    }

    public void SetCheckpoint(CheckpointController newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    public void UpdateVertSpeed(float value)
    {
        verticalSpeed = value;
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

    public bool GetGrounded()
    {
        return isGrounded;
    }

    public float GetLastGroundTime()
    {
        return groundTime;
    }

    public void SetCrouch(bool value)
    {
        isCrouching = value;
    }
}