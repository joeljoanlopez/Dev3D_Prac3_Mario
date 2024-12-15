using UnityEngine;

public interface IScoreManager
{
    void AddScore(int Points);
    int GetScore();
    event ScoreChanged scoreChangedDelegate;
}
public delegate void ScoreChanged(IScoreManager scoreManager);

public class ScoreManager : MonoBehaviour, IScoreManager
{
    [SerializeField] int score;
    public event ScoreChanged scoreChangedDelegate;

    void Awake()
    {
        DependencyInjector.AddDependency<IScoreManager>(this);
        score = 0;
    }

    private void Start()
    {
        scoreChangedDelegate?.Invoke(this);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreChangedDelegate?.Invoke(this);
    }
    public int GetScore() { return score; }
}