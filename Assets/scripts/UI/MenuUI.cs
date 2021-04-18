using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private static MenuUI _instance;
    public List<Button> CategoryButtons;
    public Button StartButton;
    public Button ExitButton;

    public GameObject MenuOptions;
    public GameObject CategoryOptions;
    public GameObject MenuUiCanvas;


    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameLogic.GetInstance().Setup();
        Categories categories = Categories.GetInstance();
        List<Category> categoriesList = categories.CategoriesList;
        for (int i = 0; i < categoriesList.Count; i++)
        {
            Text text = CategoryButtons[i].transform.GetChild(0).GetChild(0).GetComponent<Text>();
            CategoryButtons[i].onClick.AddListener(delegate { ButtonClick(text.text); });
            CategoryButtons[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Score: " + categoriesList[i].Score;
        }
        StartButton.onClick.AddListener(StartButtonClick);
        ExitButton.onClick.AddListener(ExitButtonClick);
    }


    public static MenuUI GetInstance()
    {
        return _instance;
    }

    private void ButtonClick (string category)
    {
        GameLogic.GetInstance().NewGame(category);
        CategoryOptions.SetActive(false);
        MenuOptions.SetActive(true);
        HideMenuUiCanvas();
    }

    private void StartButtonClick()
    {
        CategoryOptions.SetActive(true);
        MenuOptions.SetActive(false);
    }
    private void ExitButtonClick()
    {
        SaveLoad.GetInstance().SaveCategories(Categories.GetInstance().CategoriesList);
        Application.Quit();
    }


    public void ShowMenuUiCanvas()
    {
        MenuUiCanvas.SetActive(true);
        Categories categories = Categories.GetInstance();
        List<Category> categoriesList = categories.CategoriesList;
        for (int i = 0; i < categoriesList.Count; i++)
        {
            CategoryButtons[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Score: " + categoriesList[i].Score;
        }
    }
    public void HideMenuUiCanvas()
    {
        MenuUiCanvas.SetActive(false);
    }
}
