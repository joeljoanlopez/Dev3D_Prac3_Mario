using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class AttackBehavior : StateMachineBehaviour
{
	public float speed = 10f;
	public float attackTime = 2f;

	private AIData data;
	private NavMeshAgent agent;
	private CharacterController characterController;
	private Vector3 direction;
	private float timer;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
		AnimatorControllerPlayable controller)
	{
		characterController = animator.GetComponent<CharacterController>();
		data = animator.GetComponent<AIData>();
		agent = animator.GetComponent<NavMeshAgent>();
		
		direction = (data.playerTransform.position - animator.transform.position).normalized;
		agent.speed = speed;

		timer = attackTime;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (timer <= 0)
			animator.SetTrigger("Hit");

		timer -= Time.deltaTime;
		agent.destination = animator.transform.position + direction;
		
		Vector3 movement = agent.velocity * Time.deltaTime;
		characterController.Move(movement);
		
		// GETTING RANDOM SIDE HITS ON SLOPES SO SETTING UP A TIMER
		// if ((characterController.collisionFlags & CollisionFlags.Sides) != 0)
		// 	animator.SetTrigger("Hit");
	}
}