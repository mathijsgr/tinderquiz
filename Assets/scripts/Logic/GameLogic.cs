using System.Collections.Generic;
using Assets.scripts.Images;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private static GameLogic instance;
    private Score score;
    private List<ImageCard> images;
    private ImageCard currentImage;
    private List<string> terms;
    private string currentTerm;
    private string category = "Ruimte";
    private GameUI gameUI;
    private ImageCardBuilder imageCardBuilder;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        score = Score.GetInstance();
        gameUI = GameUI.GetInstance();
        imageCardBuilder = ImageCardBuilder.GetInstance();
        images = imageCardBuilder.CreateImageCards(category);
        terms = imageCardBuilder.GetTermsListForCategory(category);
        NextImage();
    }

    public static GameLogic GetInstance()
    {
        return instance;
    }

    public void NewGame (List<ImageCard> images)
    {
        this.images = images;
    }

    private void NextImage()
    {
        if (images.Count == 0)
        {
            gameUI.ShowGameCompletedUI();
            return;
        }
        currentImage = images[0];
        currentTerm = pickRandomTerm();
        while (CheckIfIgnoreListMatch(currentTerm, currentImage))
        {
            currentTerm = pickRandomTerm();
        }
        gameUI.SetNewInfo(currentImage, currentTerm);
        Destroy(currentImage.gameObject);
        images.RemoveAt(0);
    }

    private bool CheckIfIgnoreListMatch(string lCurrentTerm, ImageCard imageCard)
    {
        foreach(string term in imageCard.GetIgnoreTerms())
        {
            if (term == lCurrentTerm)
            {
                return true;
            }
        }
        return false;
    }

    private string pickRandomTerm()
    {
        int randomnumber = Random.Range(0, terms.Count);
        return terms[randomnumber];
    }

    public void CheckAnswer(bool isCorrect)
    {
        if (CheckIfTermMatch(currentTerm,currentImage) == isCorrect)
        {
            score.AddPoints();
        }
        else
        {
            score.SubtractPoints();
        }
        NextImage();
    }
    public bool CheckIfTermMatch(string lCurrentTerm, ImageCard imageCard)
    {
        foreach (string term in imageCard.GetTerms())
        {
            if (term == lCurrentTerm)
            {
                return true;
            }
        }
        return false;
    }
}
