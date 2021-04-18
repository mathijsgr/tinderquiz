using System.Collections.Generic;
using System.Linq;
using Assets.scripts.Images;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private static GameLogic _instance;
    private Score score;
    private List<ImageCard> images;
    private ImageCard currentImage;
    private Category currentCategory;
    private string currentTerm;
    private GameUI gameUi;
    private ImageCardBuilder imageCardBuilder;
    private SaveLoad saveLoad;
    private Categories categories;

    private void Awake()
    {
        _instance = this;
    }

    public void Setup()
    {
        score = Score.GetInstance();
        gameUi = GameUI.GetInstance();
        saveLoad = SaveLoad.GetInstance();
        MenuUI.GetInstance().ShowMenuUiCanvas();
        imageCardBuilder = ImageCardBuilder.GetInstance();
        categories = Categories.GetInstance();

        List<Category> lCategories = saveLoad.LoadCategories();
        if (lCategories.Count > 0)
        {
            categories.CategoriesList = lCategories;
        }
        else categories.CategoriesList = imageCardBuilder.BuildListOfAllCategoriesWithTerms();

        images = imageCardBuilder.CreateImageCards();
    }

    public static GameLogic GetInstance()
    {
        return _instance;
    }

    public void NewGame(string category)
    {
        foreach (var cat in categories.CategoriesList)
        {
            if (cat.CategoryName == category)
            {
                currentCategory = cat;
                break;
            }
        }

        gameUi.ShowGameUiCanvas();
        score.ClearScore();
        gameUi.SetCategoryText(category);
        NextImage();
    }

    public void CrashHandler()
    {
        gameUi.HideGameUiCanvas();
        MenuUI.GetInstance().ShowMenuUiCanvas();
    }

    private void NextImage()
    {
        int randomNumber = PickRandomCardNumber();
        currentImage = images[randomNumber];
        currentTerm = PickRandomTerm();
        int count = 0;
        while (CheckIfIgnoreListMatch(currentTerm, currentImage))
        {
            if (count > 30)
            {
                NextImage();
                break;
            }

            currentTerm = PickRandomTerm();
            count++;
        }

        gameUi.SetNewInfo(currentImage, currentTerm);
    }

    private bool CheckIfIgnoreListMatch(string lCurrentTerm, ImageCard imageCard)
    {
        List<Category> ignoreCategories = imageCard.IgnoreCategories;
        foreach (var category in ignoreCategories)
        {
            if (category.CategoryName == currentCategory.CategoryName)
            {
                List<Term> terms = category.Terms;
                foreach (var term in terms)
                {
                    if (term.TermName == lCurrentTerm)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private int PickRandomCardNumber()
    {
        int randomNumber = Random.Range(0, images.Count);
        return randomNumber;
    }

    private string PickRandomTerm()
    {
        int randomNumber = Random.Range(0, currentCategory.Terms.Count);
        return currentCategory.Terms[randomNumber].TermName;
    }

    public void CheckAnswer(bool isCorrect)
    {
        gameUi.SetIsButtonsLocked(true);
        if (CheckIfTermMatch(currentTerm, currentImage) == isCorrect)
        {
            score.AddPoints();
            foreach (var category in categories.CategoriesList.Where(category =>
                category.CategoryName == currentCategory.CategoryName))
            {
                category.Score++;
            }
        }
        else
        {
            foreach (var category in categories.CategoriesList.Where(category =>
                category.CategoryName == currentCategory.CategoryName))
            {
                category.Score--;
            }
            score.SubtractPoints();
        }

        NextImage();
    }

    public bool CheckIfTermMatch(string lCurrentTerm, ImageCard imageCard)
    {
        List<Category> lCategories = imageCard.Categories;
        foreach (var category in lCategories)
        {
            if (category.CategoryName == currentCategory.CategoryName)
            {
                var terms = category.Terms;
                foreach (var term in terms)
                {
                    if (term.TermName == lCurrentTerm) return true;
                }
            }
        }

        return false;
    }
}