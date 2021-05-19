using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Assets.scripts.Images;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private static GameUI _instance;

    //instances
    private GameLogic gameLogic;

    //image stuff
    public Image ImageHolder;
    public Button ImageButton;

    public GameObject LargeImage;
    public Button CloseButton;
    public Image LargeImageImage;

    //canvas
    public GameObject GameUiCanvas;
    public GameObject Border;

    //texts
    public Text ScoreText;
    public Text CategoryText;
    public Text TermText;
    public Text TitleText;
    public Text SubTitle;

    //help
    public GameObject HelpPanel;
    public Button HelpClose;
    public Text HelpTitle;
    public Text HelpBody;

    //buttons
    public Button YesButton;
    public Button NoButton;
    public Button HomeButton;
    public Button HelpButton;

    //prevent too fast clicking
    public bool IsButtonsLocked;

    //answer images
    public List<Texture2D> CorrectImages;
    public List<Texture2D> WrongImages;

    public GameObject AnswerPanel;
    public Image AnswerPanelImage;
    public Button CloseAnswerPanelButton;
    private Category currentCategory;

    private void Awake()
    {
        _instance = this;
        NoButton.onClick.AddListener(NoButtonClick);
        YesButton.onClick.AddListener(YesButtonClick);
        HomeButton.onClick.AddListener(HomeButtonClick);
        ImageButton.onClick.AddListener(ShowBigImageButtonClick);
        CloseButton.onClick.AddListener(CloseBigImageButtonClick);
        HelpButton.onClick.AddListener(HelpButtonClick);
        HelpClose.onClick.AddListener(HelpCloseButtonClick);
        CloseAnswerPanelButton.onClick.AddListener(CloseAnswerPanelButtonClick);
    }

    private void HelpButtonClick()
    {
        if (currentCategory.Score >= 5)
        {
            HelpPanel.SetActive(true);
            currentCategory.Score -= 5;
            SetScoreText(currentCategory.Score);
        }

        GameLogicAllCategories gameLogicAllCategories = GameLogicAllCategories.GetInstance();
        if (gameLogicAllCategories != null)
        {
            if (gameLogicAllCategories.GetScore() >= 5)
            {
                HelpPanel.SetActive(true);
                gameLogicAllCategories.SetScore(gameLogicAllCategories.GetScore() - 5);
                SetScoreText(gameLogicAllCategories.GetScore());
            }
        }
    }

    private void HelpCloseButtonClick()
    {
        HelpPanel.SetActive(false);
    }

    public void Setup(int gameMode) // 0 = normal 1 = all categories
    {
        if (gameMode == 0) gameLogic = GameLogicNormal.GetInstance();
        else gameLogic = GameLogicAllCategories.GetInstance();
    }
 
    public static GameUI GetInstance()
    {
        return _instance;
    }

    public void SetNewInfo(ImageCard currentImage, Term term, int score, Category currentCategory)
    {
        ImageHolder.sprite = currentImage.Image;
        LargeImageImage.sprite = currentImage.Image;
        this.currentCategory = currentCategory;
        FixBorderSize();

        SetTermText(term);
        SetTitleText(currentImage.Title); 
        SetSubTitleText(currentImage.SubTitle);
        SetScoreText(score);
        SetIsButtonsLocked(false);
    }

    private void FixBorderSize()
    {
        Rect parentRect = ImageHolder.GetComponentInParent<RectTransform>().rect;
        Rect imageRect = ImageHolder.sprite.rect;
        float scaleX = parentRect.height / imageRect.height;
        float scaleY = parentRect.width / imageRect.width;
        int offsetX = 50;
        int offsetY = 50;

        float width = offsetX + imageRect.width * scaleX;
        float height = offsetY + parentRect.height;

        if (width > parentRect.width) width = parentRect.width + 20;
        if (height > parentRect.height) height = parentRect.height + offsetY;

        Border.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public void SetIsButtonsLocked(bool setLock)
    {
        IsButtonsLocked = setLock;
    }

    private void SetScoreText(int score)
    {
        ScoreText.text = "Score: " + score;
    }

    private void SetTermText(Term term)
    {
        TermText.text = term.TermName;
        HelpTitle.text = term.TermName;
        HelpBody.text = term.HelpText;
    }
    public void SetCategoryText(string category)
    {
        CategoryText.text = category;
    }

    private void SetTitleText(string title)
    {
        TitleText.text = title;
    }

    private void SetSubTitleText(string subTitle)
    {
        SubTitle.text = subTitle;
    }

    public void ShowAnswerImage(bool isCorrect)
    {
        AnswerPanelImage.sprite = PickRandomImage(isCorrect ? CorrectImages : WrongImages);
        AnswerPanel.SetActive(true);
    }

    private Sprite PickRandomImage(List<Texture2D> options)
    {
        int random = Random.Range(0, options.Count);
        Sprite image = Sprite.Create(options[random], new Rect(0.0f, 0.0f, options[random].width, options[random].height),
            new Vector2(0.5f, 0.5f), 100.0f);
        return image;
    }

    private void CloseAnswerPanelButtonClick ()
    {
        AnswerPanel.SetActive(false);
        gameLogic.NextImage();
    }

    private void CloseBigImageButtonClick()
    {
        LargeImage.SetActive(false);
    }

    private void ShowBigImageButtonClick()
    {
        LargeImage.SetActive(true);
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
        SaveLoad saveLoad = SaveLoad.GetInstance();
        Categories categories = Categories.GetInstance();
        saveLoad.SaveCategories(categories.CategoriesList);
        HideGameUiCanvas();
        MenuUI.GetInstance().ShowMenuUiCanvas();
    }


}
