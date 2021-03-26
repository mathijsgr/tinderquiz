using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Assets.scripts.Images;
using UnityEditorInternal;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private static GameLogic instance = new GameLogic();
    private Score score = Score.GetInstance();
    private List<ImageCard> images;
    public GameUI GameUI;

    public static GameLogic GetInstance()
    {
        return instance;
    }

    public int GetScore ()
    {
        return score.GetScore();
    }

    public void NewGame (List<ImageCard> images)
    {
        this.images = images;
    }

    public void NextImage()
    {
        if (images.Count == 0)
        {
            GameUI.ShowGameCompletedUI();
            return;
        }
        GameUI.SetCurrentImage(images[images.Count -1]);
        images.RemoveAt(images.Count -1);
    }

    public void CheckAnswer(ImageCard currentImage, bool isCorrect)
    {
        if (currentImage.GetIsCorrect() == isCorrect)
        {
            score.AddPoints();
        }
        else
        {
            score.SubtractPoints();
        }
        NextImage();
    }
}
