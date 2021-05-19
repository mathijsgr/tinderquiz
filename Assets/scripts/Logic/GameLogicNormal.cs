using System.Collections.Generic;
using System.Linq;
using Assets.scripts.Images;
using UnityEngine;

public class GameLogicNormal : GameLogic
{
    private static GameLogicNormal _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void Setup()
    {
        gameUi = GameUI.GetInstance();
        gameUi.Setup(0);
        MenuUI.GetInstance().ShowMenuUiCanvas();
        imageCardBuilder = ImageCardBuilder.GetInstance();
        categories = Categories.GetInstance();
        images = imageCardBuilder.CreateImageCards();
    }

    public static GameLogicNormal GetInstance()
    {
        return _instance;
    }

    public void NewGame(string category)
    {
        Setup();
        foreach (var cat in categories.CategoriesList)
        {
            if (cat.CategoryName == category)
            {
                currentCategory = cat;
                break;
            }
        }

        gameUi.ShowGameUiCanvas();
        gameUi.SetCategoryText(category);
        NextImage();
    }
}