using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private static MenuUI instance;

    public List<Button> CategoryButtons;
    public Button StartButton;
    public Button HighScoreButton;
    public Button ExitButton;

    public GameObject MenuOptions;
    public GameObject CategoryOptions;
    public GameObject MenuUICanvas;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Button button in CategoryButtons)
        {
            string text = button.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
            if (text == null || text == "") return;
            button.onClick.AddListener(delegate { ButtonClick(text); });
        }
        StartButton.onClick.AddListener(StartButtonClick);
        ExitButton.onClick.AddListener(ExitButtonClick);
    }


    public static MenuUI GetInstance()
    {
        return instance;
    }

    private void ButtonClick (string category)
    {
        GameLogic.GetInstance().NewGame(category);
        CategoryOptions.SetActive(false);
        MenuOptions.SetActive(true);
        HideMenuUICanvas();
    }

    private void StartButtonClick()
    {
        CategoryOptions.SetActive(true);
        MenuOptions.SetActive(false);
    }
    private void ExitButtonClick()
    {
        Application.Quit();
    }


    public void ShowMenuUICanvas()
    {
        MenuUICanvas.SetActive(true);
    }
    public void HideMenuUICanvas()
    {
        MenuUICanvas.SetActive(false);
    }
}
