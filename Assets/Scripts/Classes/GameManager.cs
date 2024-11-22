using System.Collections.Generic;
using UnityEngine;

public interface RestartGameElement
{
    void RestartGame();
}

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    List<RestartGameElement> restartGameElements = new List<RestartGameElement>();

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    static public GameManager GetGameManager()
    {
        return gameManager;
    }

    public void AddRestartGameElement(RestartGameElement restartGameElement)
    {
        restartGameElements.Add(restartGameElement);
    }

    public void RestartGame()
    {
        foreach (RestartGameElement restartGameElement in restartGameElements)
        {
            restartGameElement.RestartGame();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}