using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public int maxHealth;
    public int health;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health == 0)
        {
            Die();
        }
    }

    public void AddHealth(int value)
    {
        health += value;
    }

    public void RemoveHealth(int value)
    {
        health -= value;
    }

    public void Die()
    {
        LifesController lifesController = GetComponent<LifesController>();
        if (lifesController)
        {
            lifesController.RemoveLife(1);
        }
        else
        {
            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    }
}