using UnityEngine;

public class Score : MonoBehaviour
{
    private static Score instance;

    private int score = 0;
    private int hintCost = 0;

    private void Awake()
    {
        instance = this;
    }

    public static Score GetInstance()
    {
        return instance;
    }

    public void AddPoints()
    {
        score += 1;
    }

    public void SubtractPoints()
    {
        score -= 1;
    }

    public int GetScore()
    {
        return score;
    }

    public void ClearScore()
    {
        score = 0;
    }

    public int GetHintCost()
    {
        return hintCost;
    }

    public bool CanBuyHint()
    {
        return score > hintCost;
    }

    public void Save()
    {
        //TODO: Save game
    }

    public void Load()
    {
        //TODO: Load game
    }
}
