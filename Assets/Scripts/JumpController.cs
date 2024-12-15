using System.Collections;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [Header("Jump")]
    public KeyCode jumpKeyCode = KeyCode.Space;
    public float jumpVerticalSpeed = 5.0f;
    public float waitStartJumpTime = 0.12f;
    public float fallingVerticalSpeedMultiplier = 0.1f;
    public float nextJumpAvailable = 0.7f;

    private MarioController marioController;
    private Animator animator;
    private int currentJumpId;
    private int maxJumps = 3;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        marioController = GetComponent<MarioController>();
    }

    private void Update()
    {
        if (CanJump() && Input.GetKeyDown(jumpKeyCode))
        {
            Jump();
        }
    }
    private bool CanJump()
    {
        //TODO when can mario jump?
        return marioController.GetGrounded();
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        float diffTime = Time.time - marioController.GetLastGroundTime();
        if (diffTime <= nextJumpAvailable)
        {
            currentJumpId = (currentJumpId + 1) % maxJumps;
        }
        else
        {
            currentJumpId = 0;
        }
        animator.SetInteger("JumpN", currentJumpId);
        StartCoroutine(ExecuteJump());
    }

    IEnumerator ExecuteJump()
    {
        yield return new WaitForSeconds(waitStartJumpTime);
        float vertSpeedModifier = 1 + currentJumpId / 10;

        marioController.UpdateVertSpeed(jumpVerticalSpeed * vertSpeedModifier);
        animator.SetBool("Falling", false);
    }

}