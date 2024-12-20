using UnityEngine;

public class GoombaController : MonoBehaviour, IRestartGameElement
{
    CharacterController characterController;
    Vector3 startingPosition;
    Quaternion startingRotation;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        GameManager.GetGameManager().AddRestartGameElement(this);
    }

    public void RestartGame()
    {
        gameObject.SetActive(true);
        characterController.enabled = false;
        transform.position = startingPosition;
        transform.rotation = startingRotation;
        characterController.enabled = true;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}