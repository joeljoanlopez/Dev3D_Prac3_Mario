using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text score;
    public Text lives;
    public Animation anim;
    public AnimationClip idleAnimationClip;
    public AnimationClip showAnimationClip;

    private void Start()
    {
        DependencyInjector.GetDependency<IScoreManager>().scoreChangedDelegate += UpdateScore;
        DependencyInjector.GetDependency<ILivesManager>().livesChangedDelegate += UpdateLives;
    }
    private void OnDestroy()
    {
        DependencyInjector.GetDependency<IScoreManager>().scoreChangedDelegate -= UpdateScore;
        DependencyInjector.GetDependency<ILivesManager>().livesChangedDelegate += UpdateLives;
    }
    public void UpdateScore(IScoreManager scoreManager)
    {
        score.text = "" + scoreManager.GetScore();
        ShowAnimation();
    }

    public void UpdateLives(ILivesManager livesManager)
    {
        lives.text = "" + livesManager.GetLives();
        ShowAnimation();
    }

    void ShowAnimation()
    {
        anim.Play(showAnimationClip.name);
        anim.PlayQueued(idleAnimationClip.name);
    }
}
