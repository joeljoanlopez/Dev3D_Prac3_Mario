using UnityEngine;
using UnityEngine.SceneManagement;

public interface ILivesManager
{
    void AddLife(int value);
    void RemoveLife(int value);
    int GetScore();
    event LivesChanged livesChangedDelegate;
}

public delegate void LivesChanged(ILivesManager livesManager);

public class LifesController : MonoBehaviour
{
    public GameManager gameManager;
    public int maxLives = 10;
    public int startLives = 1;
    int lives;

    private void Start()
    {
        lives = startLives;
    }

    private void Update()
    {
        lives = Mathf.Clamp(lives, 0, maxLives);
        if (lives == 0)
        {
            Die();
        }
    }

    public void AddLife(int value)
    {
        lives += value;
    }

    public void RemoveLife(int value)
    {
        lives -= value;
    }

    private void Die()
    {
        gameManager.RestartGame();
    }

    public int GetLives() { return lives; }
}