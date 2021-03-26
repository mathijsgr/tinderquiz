using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Images;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private static GameUI instance = new GameUI();

    private GameLogic gameLogic = GameLogic.GetInstance();
    private Score score = Score.GetInstance();

    private ImageCard currentImage;
    public GameObject GameUICanvas;
    public GameObject border;
    public Text scoreText;

    public GameUI GetInstance()
    {
        return instance;
    }

    public void SetCurrentImage(ImageCard newImage)
    {
        currentImage = newImage;
    }

    //swiped left or right
    public void SwipeActions(int direction)
    {
        switch (direction)
        {
            case -1: //down
                break;
            case 0: // left - wrong
                gameLogic.CheckAnswer(currentImage,false);
                break;
            case 1: // up - help
                if(score.CanBuyHint())
                    ShowHelpUI();
                break;
            case 2: // right - correct
                gameLogic.CheckAnswer(currentImage,true);
                break;
            default:
                break;
        }
    }

    public void SetScoreText()
    {
        scoreText.text = "score: " + gameLogic.GetScore();
    }

    public void ShowHelpUI()
    {

    }

    public void ShowGameCompletedUI()
    {

    }

    public void ShowCorrectUI()
    {

    }

    public void ShowWrongUI()
    {

    }

}
