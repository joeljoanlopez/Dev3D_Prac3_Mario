using UnityEngine;
using UnityEngine.PlayerLoop;

public class CrouchController : MonoBehaviour
{
    [Header("Input")]
    public KeyCode crouchKey = KeyCode.LeftControl;
    bool crouching;

    MarioController marioController;
    Animator animator;

    private void Awake()
    {
        marioController = GetComponent<MarioController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        crouching = Input.GetKey(crouchKey);
        animator.SetBool("Crouching", crouching);
        marioController.IsCrouching = crouching;
    }
}