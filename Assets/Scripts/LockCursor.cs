using UnityEngine;

public class LockCursor : MonoBehaviour, IDieElement, IRestartGameElement
{
	private bool isCursorLocked;

	void Start()
	{
		GameManager.GetGameManager().AddRestartGameElement(this);
		GameManager.GetGameManager().AddDieElement(this);
		
		isCursorLocked = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = !isCursorLocked;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || !isCursorLocked)
		{
			Cursor.lockState = CursorLockMode.None;
		}

		if (Input.GetMouseButtonDown(0) && isCursorLocked)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		Cursor.visible = isCursorLocked;
	}

	public void Die()
	{
		isCursorLocked = false;
	}

	public void RestartGame()
	{
		isCursorLocked = true;
	}
}