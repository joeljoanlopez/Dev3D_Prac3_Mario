using UnityEngine;
using UnityEngine.SceneManagement;

public interface ILivesManager
{
    void AddLife(int value);
    void RemoveLife(int value);
    int GetLives();
    event LivesChanged livesChangedDelegate;
}

public delegate void LivesChanged(ILivesManager livesManager);

public class LifesController : MonoBehaviour, ILivesManager
{
    public GameManager gameManager;
    public int maxLives = 10;
    public int startLives = 1;
    int lives;

    public event LivesChanged livesChangedDelegate;

    private void Awake()
    {
        DependencyInjector.AddDependency<IScoreManager>(this);
    }

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
        livesChangedDelegate?.Invoke(this);
    }

    public void RemoveLife(int value)
    {
        lives -= value;
        livesChangedDelegate?.Invoke(this);
    }

    private void Die()
    {
        gameManager.RestartGame();
    }

    public int GetLives() { return lives; }
}