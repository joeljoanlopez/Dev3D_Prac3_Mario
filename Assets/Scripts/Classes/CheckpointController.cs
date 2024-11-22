using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MarioController>()?.SetCheckpoint(this);
        }
    }
}