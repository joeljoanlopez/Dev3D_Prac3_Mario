using System.Collections;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    [Header("Jump")]
    public KeyCode jumpKeyCode = KeyCode.Space;
    public float jumpVerticalSpeed = 5.0f;
    public float waitStartJumpTime = 0.12f;
    public float fallingVerticalSpeedMultiplier = 0.1f;

    private MarioController marioController;
    private Animator animator;

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
        marioController.UpdateVertSpeed(jumpVerticalSpeed);
        animator.SetBool("Falling", false);
    }

}