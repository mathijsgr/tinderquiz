using Assets.scripts.Images;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private static GameUI _instance;

    //instances
    private GameLogic gameLogic;
    private Score score;

    //image stuff
    public Image ImageHolder;

    //canvas
    public GameObject GameUiCanvas;
    public GameObject Border;

    //texts
    public Text ScoreText;
    public Text CategoryText;
    public Text TermText;
    public Text TitleText;

    //buttons
    public Button YesButton;
    public Button NoButton;
    public Button HomeButton;

    //prevent too fast clicking
    public bool IsButtonsLocked;

    private void Awake()
    {
        _instance = this;
        NoButton.onClick.AddListener(NoButtonClick);
        YesButton.onClick.AddListener(YesButtonClick);
        HomeButton.onClick.AddListener(HomeButtonClick);
    }

    private void Start()
    {
        gameLogic = GameLogic.GetInstance();
        score = Score.GetInstance();
    }
 
    public static GameUI GetInstance()
    {
        return _instance;
    }

    public void SetNewInfo(ImageCard currentImage, string term)
    {
        ImageHolder.sprite = currentImage.Image;
        SetTermText(term);
        SetTitleText(currentImage.ImageName);
        SetScoreText();
        SetIsButtonsLocked(false);
    }

    public void SetIsButtonsLocked(bool setLock)
    {
        IsButtonsLocked = setLock;
    }

    private void SetScoreText()
    {
        ScoreText.text = "Score: " + Score.GetInstance().GetScore();
    }

    private void SetTermText(string term)
    {
        TermText.text = term;
    }
    public void SetCategoryText(string category)
    {
        CategoryText.text = category;
    }

    private void SetTitleText(string title)
    {
        TitleText.text = title;
    }


    public void ShowHelpUi()
    {

    }

    public void ShowCorrectUi()
    {

    }

    public void ShowWrongUi()
    {

    }

    private void NoButtonClick()
    {
        if (!IsButtonsLocked) gameLogic.CheckAnswer(false);
    }

    private void YesButtonClick()
    {
        if(!IsButtonsLocked) gameLogic.CheckAnswer(true);
    }
    public void ShowGameUiCanvas()
    {
        GameUiCanvas.SetActive(true);
    }
    public void HideGameUiCanvas()
    {
        GameUiCanvas.SetActive(false);
    }

    private void HomeButtonClick()
    {
        HideGameUiCanvas();
        MenuUI.GetInstance().ShowMenuUiCanvas();
    }


}
