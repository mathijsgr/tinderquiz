using System.Collections.Generic;
using System.Linq;
using Assets.scripts.Images;
using UnityEngine;

public class GameLogicAllCategories : GameLogic
{
    private static GameLogicAllCategories _instance;
    private int score;
    

    private void Awake()
    {
        _instance = this;
    }

    public void Setup()
    {
        gameUi = GameUI.GetInstance();
        gameUi.Setup(1);
        MenuUI.GetInstance().ShowMenuUiCanvas();
        imageCardBuilder = ImageCardBuilder.GetInstance();
        categories = Categories.GetInstance();
        score = SaveLoad.GetInstance().LoadTotalScore();
        images = imageCardBuilder.CreateImageCards();
    }

    public static GameLogicAllCategories GetInstance()
    {
        return _instance;
    }

    public void NewGame()
    {
        Setup();
        gameUi.ShowGameUiCanvas();
        NextImage();
    }

    private int PickRandomCategory()
    {
        return Random.Range(0, categories.CategoriesList.Count);
    }

    public override void NextImage()
    {
        currentCategory = categories.CategoriesList[PickRandomCategory()];
        base.NextImage();
        gameUi.SetCategoryText(currentCategory.CategoryName);
        SaveLoad.GetInstance().SaveTotalScore(score);
    }

    protected override void SetNewInfo()
    {
        gameUi.SetNewInfo(currentImage, currentTerm, score, currentCategory);
    }

    public override void CheckAnswer(bool isCorrect)
    {
        gameUi.SetIsButtonsLocked(true);
        if (CheckIfTermMatch(currentTerm.TermName, currentImage) == isCorrect)
        {
            foreach (var category in categories.CategoriesList.Where(category =>
                category.CategoryName == currentCategory.CategoryName))
            {
                score++;
                gameUi.ShowAnswerImage(true);
                if (currentTerm.Score < 9)
                {
                    currentTerm.Score++;
                }
            }
        }
        else
        {
            foreach (var category in categories.CategoriesList.Where(category =>
                category.CategoryName == currentCategory.CategoryName))
            {
                gameUi.ShowAnswerImage(false);
                if (score > 0)
                { 
                    score--;
                }
                if (currentTerm.Score > 0) currentTerm.Score--;
            }
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }
}