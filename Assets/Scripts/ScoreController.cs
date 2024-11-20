using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    public int score;

    private void Start()
    {
        score = 0;
    }

    public void AddScore(int value)
    {
        score += value;
    }

}