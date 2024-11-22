using UnityEngine;

public class PunchBehavior : StateMachineBehaviour
{
    PunchController punchController;
    public PunchController.PunchType punchType;
    public float enablePunchStartAnimationPct;
    public float enablePunchEndAnimationPct;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        punchController = animator.GetComponent<PunchController>();
        punchController.EnableHitCollider(punchType, false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        bool enabled = animatorStateInfo.normalizedTime >= enablePunchStartAnimationPct && animatorStateInfo.normalizedTime <= enablePunchEndAnimationPct;
        punchController.EnableHitCollider(punchType, enabled);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        punchController.EnableHitCollider(punchType, false);
    }


}