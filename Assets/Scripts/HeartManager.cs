using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public int health;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthController>().AddHealth(health);
            Destroy(this.gameObject);
        }
    }
}