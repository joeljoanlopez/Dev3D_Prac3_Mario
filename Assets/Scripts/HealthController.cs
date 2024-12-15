using UnityEngine;
using UnityEngine.SceneManagement;

public interface IHealthController
{
    bool AddHealth(int value);
    void RemoveHealth(int value);
    int GetHealth();
    event HealthChanged healthChangedDelegate;
}

public delegate void HealthChanged(IHealthController healthController);

public class HealthController : MonoBehaviour, IHealthController, RestartGameElement
{
    public GameManager gameManager;
    public int maxHealth = 8;
    public int health;
    public float invincibilityTime = 0.5f;
    private float damageTimer;

    public event HealthChanged healthChangedDelegate;

    private void Awake()
    {
        DependencyInjector.AddDependency<IHealthController>(this);
    }

    private void Start()
    {
        health = maxHealth;
        damageTimer = invincibilityTime;
        healthChangedDelegate?.Invoke(this);
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

    public bool AddHealth(int value)
    {
        if (health >= maxHealth)
        {
            return false;
        }
        health += value;
        healthChangedDelegate?.Invoke(this);
        return true;
    }

    public void RemoveHealth(int value)
    {
        if (damageTimer <= 0.0f)
        {
            health -= value;
            damageTimer = invincibilityTime;
            Debug.Log("Health Remaining: " + health);
        }
        healthChangedDelegate?.Invoke(this);
    }

    public void Die()
    {
        // TODO implement game over screen
        LifesController lifesController = GetComponent<LifesController>();
        if (lifesController)
        {
            lifesController.RemoveLife(1);
            gameManager.RestartGame();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void RestartGame()
    {
        health = maxHealth;
    }
}