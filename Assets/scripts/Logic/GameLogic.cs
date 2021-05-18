using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.scripts.Images;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    protected List<ImageCard> images;
    protected ImageCard currentImage;
    protected Category currentCategory;
    protected Term currentTerm;
    protected GameUI gameUi;
    protected ImageCardBuilder imageCardBuilder;
    protected Categories categories;

    public virtual void NextImage()
    {
        int randomNumber = PickRandomCardNumber();
        currentImage = images[randomNumber];
        currentTerm = PickRandomTerm();
        int count = 0;
        while (CheckIfIgnoreListMatch(currentTerm.TermName, currentImage))
        {
            if (count > 30)
            {
                NextImage();
                break;
            }
            currentTerm = PickRandomTerm();
            count++;
        }

        SetNewInfo();
    }

    protected virtual void SetNewInfo()
    {
        gameUi.SetNewInfo(currentImage, currentTerm, currentCategory.Score, currentCategory);
    }

    private bool CheckIfIgnoreListMatch(string lCurrentTerm, ImageCard imageCard)
    {
        List<Category> ignoreCategories = imageCard.IgnoreCategories;
        return ignoreCategories.Where(category => category.CategoryName == currentCategory.CategoryName).SelectMany(category => category.Terms).Any(term => term.TermName == lCurrentTerm);
    }

    private int PickRandomCardNumber()
    {
        int randomNumber = Random.Range(0, images.Count);
        return randomNumber;
    }

    private Term PickRandomTerm()
    {
        int randomNumber = Random.Range(0, currentCategory.Terms.Count);
        Term term = currentCategory.Terms[randomNumber];
        int randomNumber2 = Random.Range(0, 100);
        int count = 0;
        while (term.Score < randomNumber2)
        {
            if (count == 30) return term;
            randomNumber = Random.Range(0, currentCategory.Terms.Count);
            term = currentCategory.Terms[randomNumber];
            randomNumber2 = Random.Range(0, 10);
            count++;
        }

        return currentCategory.Terms[randomNumber];
    }

    public virtual void CheckAnswer(bool isCorrect)
    {
        gameUi.SetIsButtonsLocked(true);
        if (CheckIfTermMatch(currentTerm.TermName, currentImage) == isCorrect)
        {
            foreach (var category in categories.CategoriesList.Where(category =>
                category.CategoryName == currentCategory.CategoryName))
            {
                category.Score++;
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
                if (category.Score > 0)
                {
                    category.Score--;
                }
                if (currentTerm.Score > 0) currentTerm.Score--;
            }
        }
    }

    public bool CheckIfTermMatch(string lCurrentTerm, ImageCard imageCard)
    {
        List<Category> lCategories = imageCard.Categories;
        return lCategories.Where(category => category.CategoryName == currentCategory.CategoryName).SelectMany(category => category.Terms).Any(term => term.TermName == lCurrentTerm);
    }
}
