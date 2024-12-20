using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IRestartGameElement
{
    void RestartGame();
}

public interface IDieElement
{
    void Die();
}

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    static GameManager gameManager;
    private bool isGameOver;
    private List<IRestartGameElement> restartGameElements = new List<IRestartGameElement>();
    private List<IDieElement> dieElements = new List<IDieElement>();

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

    private void Start()
    {
        isGameOver = false;
    }

    static public GameManager GetGameManager()
    {
        return gameManager;
    }

    public void AddRestartGameElement(IRestartGameElement restartGameElement)
    {
        restartGameElements.Add(restartGameElement);
    }

    public void RestartGame()
    {
        foreach (IRestartGameElement restartGameElement in restartGameElements)
        {
            restartGameElement.RestartGame();
        }
        isGameOver = false;
    }

    public void AddDieElement(IDieElement dieElement)
    {
        dieElements.Add(dieElement);
    }

    public void Die()
    {
        foreach (IDieElement dieElement in dieElements)
        {
            dieElement.Die();
        }
        isGameOver = true;
    }

    public void ReloadGame()
    {
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
        gameOverScreen.SetActive(isGameOver);
    }
}