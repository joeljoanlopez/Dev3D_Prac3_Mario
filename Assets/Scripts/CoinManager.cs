using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int score = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        other.GetComponent<ScoreManager>().AddScore(score);
        Destroy(this.gameObject);
    }
}