using UnityEngine;
using UnityEngine.AI;

public class PatrolController : MonoBehaviour
{
    public Transform path;
    public float speed = 5f;
    public float changeOffset = 0.01f;

    private Transform[] nodes;
    private int currentNodeIndex;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        nodes = path.GetComponentsInChildren<Transform>();
        currentNodeIndex = 0;
        agent.autoBraking = false;
        TargetNextNode();
    }

    private void Update()
    {
        if (agent.remainingDistance < changeOffset)
            TargetNextNode();
    }

    void TargetNextNode()
    {
        if (nodes.Length == 0)
            return;
        agent.destination = nodes[currentNodeIndex].position;
        currentNodeIndex = (currentNodeIndex + 1) % nodes.Length;
    }
}