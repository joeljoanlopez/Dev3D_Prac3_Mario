using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class AttackBehavior : StateMachineBehaviour
{
	public float speed = 10f;

	private AIData data;
	private NavMeshAgent agent;
	private CharacterController characterController;
	private Vector3 direction;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
		AnimatorControllerPlayable controller)
	{
		characterController = animator.GetComponent<CharacterController>();
		data = animator.GetComponent<AIData>();
		agent = animator.GetComponent<NavMeshAgent>();
		
		direction = (data.playerTransform.position - animator.transform.position).normalized;
		agent.speed = speed;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		agent.destination = animator.transform.position + direction;
		
		Vector3 movement = agent.velocity * Time.deltaTime;
		characterController.Move(movement);
		
		if ((characterController.collisionFlags & CollisionFlags.Sides) != 0)
			animator.SetTrigger("Hit");
	}
}