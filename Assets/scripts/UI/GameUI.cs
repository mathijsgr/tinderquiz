using Assets.scripts.Images;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private static GameUI instance;

    //instances
    private GameLogic gameLogic;
    private Score score;

    //image stuff
    public Image ImageHolder;

    //canvas
    public GameObject GameUICanvas;
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

    public GameObject GameCompletedPanel;
    public Button PlayAgainYesButton;
    public Button PlayAgainNoButton;
    public Text FinalScoreText;

    //prevent too fast clicking
    public bool IsButtonsLocked;

    private void Awake()
    {
        instance = this;
        NoButton.onClick.AddListener(NoButtonClick);
        YesButton.onClick.AddListener(YesButtonClick);
        HomeButton.onClick.AddListener(HomeButtonClick);
        PlayAgainYesButton.onClick.AddListener(PlayAgainYesButtonClick);
        PlayAgainNoButton.onClick.AddListener(PlayAgainNoButtonClick);
    }

    private void Start()
    {
        gameLogic = GameLogic.GetInstance();
        score = Score.GetInstance();
    }
 
    public static GameUI GetInstance()
    {
        return instance;
    }

    public void SetNewInfo(ImageCard currentImage, string term)
    {
        ImageHolder.sprite = currentImage.GetImage();
        SetTermText(term);
        SetTitleText(currentImage.GetImageName());
        SetScoreText();
        SetIsButtonsLocked(false);
    }

    //swiped left or right
    public void SwipeActions(int direction)
    {
        switch (direction)
        {
            case -1: //down
                break;
            case 0: // left - wrong
                gameLogic.CheckAnswer(false);
                break;
            case 1: // up - help
                if(score.CanBuyHint())
                    ShowHelpUI();
                break;
            case 2: // right - correct
                gameLogic.CheckAnswer(true);
                break;
            default:
                break;
        }
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


    public void ShowHelpUI()
    {

    }

    public void ShowGameCompletedUI()
    {
        GameCompletedPanel.SetActive(true);
        FinalScoreText.text = "Eind score: " + score.GetScore();
        //PlayAgainYesButton.onClick.AddListener(PlayAgainYesButtonClick);
        //PlayAgainNoButton.onClick.AddListener(PlayAgainNoButtonClick);
    }

    private void PlayAgainYesButtonClick()
    {
        gameLogic.Replay();
        //PlayAgainYesButton.onClick.RemoveListener(PlayAgainYesButtonClick);
        GameCompletedPanel.SetActive(false);
    }

    private void PlayAgainNoButtonClick ()
    {
        GameUICanvas.SetActive(false);
        //PlayAgainNoButton.onClick.RemoveListener(PlayAgainNoButtonClick);
        GameCompletedPanel.SetActive(false);
        MenuUI.GetInstance().ShowMenuUICanvas();
    }

    public void ShowCorrectUI()
    {

    }

    public void ShowWrongUI()
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
    public void ShowGameUICanvas()
    {
        GameUICanvas.SetActive(true);
    }
    public void HideGameUICanvas()
    {
        GameUICanvas.SetActive(false);
    }

    private void HomeButtonClick()
    {
        gameLogic.ClearImages();
        HideGameUICanvas();
        MenuUI.GetInstance().ShowMenuUICanvas();
    }


}
