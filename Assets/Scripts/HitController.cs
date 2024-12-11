using UnityEngine;

public class HitController : MonoBehaviour
{
    private MarioController marioController;
    public float killJumpVerticalSpeed = 0.5f;
    public float maxAngleNeededToKill = 15.0f;

    public float minVerticalSpeedNeededToKill = 1.0f;

    private void Awake()
    {
        marioController = GetComponent<MarioController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Goomba"))
        {
            if (IsUpperHit(hit.transform))
            {
                hit.gameObject.GetComponent<GoombaController>().Kill();
                marioController.UpdateVertSpeed(killJumpVerticalSpeed);
            }
            else
            {
                //TODO test take damage
                //TODO Separar player i goomba
                Debug.LogWarning("Mario Was Hit!! Current health ");
                GetComponent<HealthController>().RemoveHealth(1);
            }
        }
    }

    private bool IsUpperHit(Transform enemy)
    {
        Vector3 enemyDirection = transform.position - enemy.position;
        enemyDirection.Normalize();

        float dotAngle = Vector3.Dot(enemyDirection, Vector3.up);
        if (dotAngle >= Mathf.Cos(maxAngleNeededToKill * Mathf.Deg2Rad) && marioController.VerticalSpeed <= minVerticalSpeedNeededToKill)
        {
            Debug.Log("Upper Hit done!");
            return true;
        }
        Debug.Log("Hit was not from the upper side");
        return false;
    }
}