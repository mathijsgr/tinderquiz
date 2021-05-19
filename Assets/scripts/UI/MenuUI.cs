using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private static MenuUI _instance;
    public List<Button> CategoryButtons;
    public Button StartButtonNormal;
    public Button StartButtonAllCategories;
    public Button ExitButton;

    public GameObject MenuOptions;
    public GameObject CategoryOptions;
    public GameObject MenuUiCanvas;

    public Text TotalScoreText;
    public Text TotalScoreCategoriesText;

    private List<Category> categoriesList;
    private Categories categories;

    public Button HomeButton;


    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        List<Category> lCategories = SaveLoad.GetInstance().LoadCategories();
        categories = Categories.GetInstance();
        categories.CategoriesList = lCategories.Count > 0 ? lCategories : ImageCardBuilder.GetInstance().BuildListOfAllCategoriesWithTerms();
        categoriesList = categories.CategoriesList;
        for (int i = 0; i < categoriesList.Count; i++)
        {
            Text text = CategoryButtons[i].transform.GetChild(0).GetChild(0).GetComponent<Text>();
            CategoryButtons[i].onClick.AddListener(delegate { ButtonClick(text.text); });
            CategoryButtons[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Score: " + categoriesList[i].Score;
        }
        StartButtonNormal.onClick.AddListener(StartButtonClick);
        StartButtonAllCategories.onClick.AddListener(AllCategoriesButtonClick);
        ExitButton.onClick.AddListener(ExitButtonClick);
        HomeButton.onClick.AddListener(HomeButtonClick);
        SetTotalScore();
    }

    public static MenuUI GetInstance()
    {
        return _instance;
    }

    private void SetTotalScore()
    {
        int totalScore = 0;
        foreach (Category category in categoriesList)
        {
            totalScore += category.Score;
        }
        TotalScoreText.text = "Totale score: " + (totalScore + SaveLoad.GetInstance().LoadTotalScore());
        TotalScoreCategoriesText.text = "Totale score: " + totalScore;
    }

    private void ButtonClick (string category)
    {
        GameLogicNormal.GetInstance().NewGame(category);
        CategoryOptions.SetActive(false);
        MenuOptions.SetActive(true);
        HideMenuUiCanvas();
    }

    private void StartButtonClick()
    {
        CategoryOptions.SetActive(true);
        MenuOptions.SetActive(false);
    }

    private void AllCategoriesButtonClick()
    {
        GameLogicAllCategories.GetInstance().NewGame();
        MenuOptions.SetActive(true);
        HideMenuUiCanvas();
    }

    private void HomeButtonClick()
    {
        CategoryOptions.SetActive(false);
        MenuOptions.SetActive(true);
    }

    private void ExitButtonClick()
    {
        SaveLoad.GetInstance().SaveCategories(Categories.GetInstance().CategoriesList);
        Application.Quit();
    }

    public void ShowMenuUiCanvas()
    {
        MenuUiCanvas.SetActive(true);
        categories = Categories.GetInstance();
        categoriesList = categories.CategoriesList;
        for (int i = 0; i < categoriesList.Count; i++)
        {
            CategoryButtons[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Score: " + categoriesList[i].Score;
        }

        SetTotalScore();
    }
    public void HideMenuUiCanvas()
    {
        MenuUiCanvas.SetActive(false);
    }
}
