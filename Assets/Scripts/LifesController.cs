using UnityEngine;
using UnityEngine.SceneManagement;

public class LifesController : MonoBehaviour
{
    public int maxLives = 10;
    public int startLives = 1;
    public int lives;

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
        var scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
}