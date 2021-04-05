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
    public RawImage ImageHolder;

    //canvas
    public GameObject GameUICanvas;
    public GameObject Border;

    //texts
    public Text ScoreText;
    public Text ThemeText;
    public Text TermText;

    //buttons
    public Button YesButton;
    public Button NoButton;

    private void Awake()
    {
        instance = this;
        NoButton.onClick.AddListener(NoButtonClick);
        YesButton.onClick.AddListener(YesButtonClick);
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
        ImageHolder.texture = currentImage.GetImage();
        TermText.text = term;
        SetScoreText();
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

    public void SetScoreText()
    {
        ScoreText.text = "Score: " + Score.GetInstance().GetScore();
    }

    public void ShowHelpUI()
    {

    }

    public void ShowGameCompletedUI()
    {

    }

    public void ShowCorrectUI()
    {

    }

    public void ShowWrongUI()
    {

    }

    private void NoButtonClick()
    {
        gameLogic.CheckAnswer(false);
    }

    private void YesButtonClick()
    {
        gameLogic.CheckAnswer(true);
    }


}
