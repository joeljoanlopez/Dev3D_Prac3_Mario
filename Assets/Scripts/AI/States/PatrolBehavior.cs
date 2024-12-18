using UnityEngine;
using UnityEngine.AI;

public class PatrolBehavior : StateMachineBehaviour
{
	public float speed = 5f;
	public float changeOffset = 0.01f;
	public float viewDistance = 10f;
	public float viewAngle = 90f;
	public float hearingDistance = 0.5f;

	private Transform path;
	private Transform[] nodes;
	private int currentNodeIndex;
	private NavMeshAgent agent;
	private AIData data;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		data = animator.GetComponent<AIData>();
		currentNodeIndex = data.currentNodeIndex;
		path = data.path;
		nodes = path.GetComponentsInChildren<Transform>();

		agent = animator.GetComponent<NavMeshAgent>();
		agent.autoBraking = false;
		agent.speed = speed;
		agent.destination = nodes[currentNodeIndex].position;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (agent.remainingDistance <= changeOffset)
		{
			TargetNextNode();
		}

		if (IsCharacterSeen(data.playerTransform))
		{
			animator.SetTrigger("Alert");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		data.currentNodeIndex = currentNodeIndex;
	}

	private void TargetNextNode()
	{
		if (nodes.Length == 0)
			return;
		agent.destination = nodes[currentNodeIndex].position;
		currentNodeIndex = (currentNodeIndex + 1) % nodes.Length;
	}

	private bool IsCharacterSeen(Transform target)
	{
		
		Vector3 agentPosition = agent.transform.position;
		Vector3 targetPosition = target.position;

		Vector3 directionToTarget = targetPosition - agentPosition;
		float distanceToTarget = directionToTarget.magnitude;
		directionToTarget.Normalize();
		
		if (distanceToTarget > viewDistance)
			return false;
		if (distanceToTarget < hearingDistance)
			return true;
		
		Vector3 agentForward = agent.transform.forward;
		float angleToTargetCos = Vector3.Dot(agentForward, directionToTarget);

		if (angleToTargetCos <= Mathf.Cos(viewAngle * Mathf.Deg2Rad))
			return false;

		if (Physics.Raycast(agentPosition, directionToTarget, out RaycastHit hit, distanceToTarget))
			if (!hit.collider.transform.IsChildOf(target))
				return false;

		return true;
	}
}