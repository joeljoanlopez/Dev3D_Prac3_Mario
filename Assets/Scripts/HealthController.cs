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

public class HealthController : MonoBehaviour, IHealthController, IRestartGameElement
{
	public int maxHealth = 8;
	public int health;
	public float invincibilityTime = 0.5f;
	private GameManager gameManager;
	private float damageTimer;
	private LifesController lifesController;

	public event HealthChanged healthChangedDelegate;

	private void Awake()
	{
		DependencyInjector.AddDependency<IHealthController>(this);
		gameManager = GameManager.GetGameManager();
	}

	private void Start()
	{
		lifesController = GetComponent<LifesController>();
		gameManager.AddRestartGameElement(this);
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
			return false;
		
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
		if (!lifesController)
		{
			gameManager.Die();
			return;
		}
		lifesController.RemoveLife(1);
		gameManager.RestartGame();
	}

	public int GetHealth()
	{
		return health;
	}

	public void RestartGame()
	{
		health = maxHealth;
		healthChangedDelegate?.Invoke(this);
	}
}