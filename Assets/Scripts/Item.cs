using UnityEngine;
public class Item : MonoBehaviour, RestartGameElement
{
    private void Start()
    {
        GameManager.GetGameManager().AddRestartGameElement(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DependencyInjector.GetDependency<IScoreManager>().AddScore(1);
            gameObject.SetActive(false);
        }
    }

    public void RestartGame()
    {
        gameObject.SetActive(true);
    }
}