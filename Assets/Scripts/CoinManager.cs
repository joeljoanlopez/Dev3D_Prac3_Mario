using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int score = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ScoreController>().AddScore(score);
            Destroy(this.gameObject);
        }
    }
}