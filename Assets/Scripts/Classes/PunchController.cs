
using JetBrains.Annotations;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    public enum PunchType
    {
        RIGHT_HAND = 0,
        LEFT_HAND = 1,
        RIGHT_LEG = 2,
    }

    public int punchButton = 0;
    public float punchComboAvailable = 1.3f;
    public GameObject leftHandHitCollider;
    public GameObject rightHandHitCollider;
    public GameObject rightLegHitCollider;

    private Animator animator;
    private int currentPunchId;
    private int maxPunches = 2;
    private float lastPunchTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        leftHandHitCollider.SetActive(false);
        rightHandHitCollider.SetActive(false);
        rightLegHitCollider.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(punchButton) && canPunch())
        {
            PunchCombo();
        }
    }

    public bool canPunch()
    {
        return true;
    }

    private void PunchCombo()
    {
        animator.SetTrigger("Punch");
        float diffTime = Time.time - lastPunchTime;
        if (diffTime <= punchComboAvailable)
        {
            currentPunchId = (currentPunchId + 1) % 3;
        }
        else
        {
            currentPunchId = 0;
        }
        lastPunchTime = Time.time;
        animator.SetInteger("PunchCombo", currentPunchId);
    }

    public void EnableHitCollider(PunchType punchType, bool active)
    {
        switch (punchType)
        {
            case PunchType.LEFT_HAND:
                leftHandHitCollider.SetActive(active);
                break;
            case PunchType.RIGHT_HAND:
                rightHandHitCollider.SetActive(active);
                break;
            case PunchType.RIGHT_LEG:
                rightLegHitCollider.SetActive(active);
                break;

        }
    }
}