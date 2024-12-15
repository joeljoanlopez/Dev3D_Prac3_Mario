using UnityEngine;

public class CrouchController : MonoBehaviour
{
    [Header("Input")]
    public KeyCode crouchKey = KeyCode.LeftControl;

    MarioController marioController;
    Animator animator;

    private void Awake()
    {
        marioController = GetComponent<MarioController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool crouching = Input.GetKey(crouchKey);
        animator.SetBool("Crouching", crouching);
        marioController.SetCrouch(crouching);
    }
}