using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float invincibilityTime = 0.5f;
    private float damageTimer;

    private void Start()
    {
        health = maxHealth;
        damageTimer = invincibilityTime;
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health == 0)
        {
            Die();
        }
        damageTimer -= Time.deltaTime;
    }

    public void AddHealth(int value)
    {
        health += value;
    }

    public void RemoveHealth(int value)
    {
        if (damageTimer <= 0.0f)
        {
            health -= value;
            damageTimer = invincibilityTime;
            Debug.Log("Health Remaining: " + health);
        }
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