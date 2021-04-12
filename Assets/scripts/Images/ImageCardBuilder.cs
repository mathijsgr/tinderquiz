using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.Images;

public class ImageCardBuilder : MonoBehaviour
{
    private static ImageCardBuilder instance;

    public TextAsset SettingsTextAsset;
    private List<Tuple<string, List<string>>> categories = new List<Tuple<string, List<string>>>();
    public List<string> rawCategories = new List<string>();
    public List<string> terms = new List<string>();
    public List<Texture2D> texture2Ds;
    private int maxRetries = 100;
    private int currentTries;
    //private List<string> imageInfos = new List<string>();

    private int categoriesIndex = 0;
    private int termsIndex = 1;

    private void Awake()
    {
        instance = this;
    }

    public static ImageCardBuilder GetInstance()
    {
        return instance;
    }

    private ImageCard CreateMyAsset(string helpText, string[] words, string pickedCategory)
    {
        Texture2D image2D = Resources.Load<Texture2D>("Images/" + words[0]);
        if (image2D == null)
        {
            Debug.Log("not found");
            if (currentTries < maxRetries)
            {
                currentTries++;
                return CreateMyAsset(helpText, words, pickedCategory);
            }
            else
            {
                currentTries = 0;
                GameLogic.GetInstance().CrashHandler();
                return null;
            }
        }
        Sprite image = Sprite.Create(image2D, new Rect(0.0f, 0.0f, image2D.width, image2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        string imagename = words[0];
        Tuple<List<string>, List<string>> TermsLists = CreateTermsListsForImageCard(words, pickedCategory);
        GameObject imageCardGameOject = new GameObject
        {
            name = imagename
        };
        ImageCard imageCard = imageCardGameOject.AddComponent<ImageCard>();
        imageCard.setup(imagename, TermsLists.Item1, TermsLists.Item2, image, helpText);

        Image rawImage = imageCardGameOject.AddComponent<Image>();
        rawImage.sprite = image;

        imageCardGameOject.SetActive(false);

        return imageCardGameOject.GetComponent<ImageCard>();
    }

    private string GetTextAssetContent()
    {
        string fileContents = Resources.Load<TextAsset>(SettingsTextAsset.name).ToString();
        return fileContents;
    }

    private void ClearAll()
    {
        categories = new List<Tuple<string, List<string>>>();
        rawCategories = new List<string>();
        terms = new List<string>();
    }

    public List<ImageCard> CreateImageCards(string pickedCategory)
    {
        ClearAll();
        List<ImageCard> imageCards = new List<ImageCard>();
        string[] lines = GetTextAssetContent().Split("\n"[0]);
        for (int i = categoriesIndex; i < lines.Length;i++)
        {
            string[] words = lines[i].Split("\t"[0]);
            if (i == categoriesIndex)
            {
                string lastword = "";
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j] != "")
                    {
                        rawCategories.Add(words[j]);
                    }
                    if (words[j] != lastword)
                    {
                        categories.Add(new Tuple<string, List<string>>(words[j], null));
                    }
                    lastword = words[j];
                }
                categories.RemoveAt(categories.Count -1);
            }
            else if (i == termsIndex)
            {
                for (int j = 1; j < words.Length; j++)
                {
                    if (words[j] != "")
                    {
                        terms.Add(words[j]);
                    }
                }
            }
            else
            {
                if (words[0] == "") break; //skip if empty
                imageCards.Add(CreateMyAsset("",words,pickedCategory));
            }
        }
        BuildListOfAllCategoriesWithTerms();
        return imageCards;
    }

    public Tuple<List<string>,List<string>> CreateTermsListsForImageCard(string[] words, string pickedCategory)
    {
        List<string> localTerms = new List<string>();
        List<string> localIgnoreTerms = new List<string>();

        for (int i = 0; i < rawCategories.Count; i++)
        {
            if (pickedCategory == rawCategories[i])
            {
                if (words[i +1] == "1") localTerms.Add(terms[i]);
                if (words[i +1] == "x") localIgnoreTerms.Add(terms[i]);
            }
        }

        return new Tuple<List<string>, List<string>>(localTerms, localIgnoreTerms);
    }

    public void BuildListOfAllCategoriesWithTerms()
    {
        for (int i = 0; i < categories.Count; i++)
        {
            List<string> localTerms = new List<string>();
            for (int j = 0; j < terms.Count; j++)
            {
                if (categories[i].Item1 == rawCategories[j])
                {
                    localTerms.Add(terms[j]);
                }
            }
            categories[i] = new Tuple<string, List<string>>(categories[i].Item1, localTerms);
        }
    }
    public List<Tuple<string, List<string>>> GetListOfAllCategoriesWithTerms()
    {
        return categories;
    }

    public List<string> GetTermsListForCategory(string pickedCategory)
    {
        foreach (Tuple<string, List<string>> category in categories)
        {
            if (category.Item1 == pickedCategory)
            {
                return category.Item2;
            }
        }
        return null;
    }
}
