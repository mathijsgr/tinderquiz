using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Score : MonoBehaviour
{
    private static Score instance = new Score();

    private int score;
    private int hintCost;

    public static Score GetInstance()
    {
        return instance;
    }

    public void AddPoints()
    {
        score += 10;
    }

    public void SubtractPoints()
    {
        score -= 1;
    }

    public int GetScore()
    {
        return score;
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
