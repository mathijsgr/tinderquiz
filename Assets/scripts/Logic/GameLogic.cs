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
    private string category;
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
        MenuUI.GetInstance().ShowMenuUICanvas();
        imageCardBuilder = ImageCardBuilder.GetInstance();
    }

    public static GameLogic GetInstance()
    {
        return instance;
    }

    public void NewGame (string category)
    {
        gameUI.ShowGameUICanvas();
        score.ClearScore();
        images = imageCardBuilder.CreateImageCards(category);
        terms = imageCardBuilder.GetTermsListForCategory(category);
        this.category = category;
        gameUI.SetCategoryText(category);
        NextImage();
    }

    public void CrashHandler ()
    {
        gameUI.HideGameUICanvas();
        MenuUI.GetInstance().ShowMenuUICanvas();
    }

    public void Replay()
    {
        ClearImages();
        gameUI.ShowGameUICanvas();
        score.ClearScore();
        images = imageCardBuilder.CreateImageCards(category);
        terms = imageCardBuilder.GetTermsListForCategory(category);
        gameUI.SetCategoryText(category);
        NextImage();
    }

    public void ClearImages()
    {
        if (images.Count == 0) return;
        foreach(ImageCard imageCard in images)
        {
            Destroy(imageCard.gameObject);
        }
        images = new List<ImageCard>();
    }

    public string GetCategory()
    {
        return category;
    }

    private void NextImage()
    {
        int randomNumber = PickRandomCardNumber();
        currentImage = images[randomNumber];
        currentTerm = pickRandomTerm();
        int count = 0;
        while (CheckIfIgnoreListMatch(currentTerm, currentImage))
        {
            if (count > 30) 
            {
                NextImage();
                break; 
            }
            currentTerm = pickRandomTerm(); // check if this problem
            count++;
        }
        gameUI.SetNewInfo(currentImage, currentTerm);
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
    private int PickRandomCardNumber()
    {
        int randomnumber = Random.Range(0, images.Count);
        return randomnumber;
    }

    private string pickRandomTerm()
    {
        int randomnumber = Random.Range(0, terms.Count);
        return terms[randomnumber];
    }

    public void CheckAnswer(bool isCorrect)
    {
        gameUI.SetIsButtonsLocked(true);
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
