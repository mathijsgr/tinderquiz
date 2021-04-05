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
    private List<string> rawCategories = new List<string>();
    private List<string> terms = new List<string>();
    //private List<string> imageInfos = new List<string>();

    private int categoriesIndex = 1;
    private int termsIndex = 2;

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
        Texture2D image = Resources.Load<Texture2D>("Images/" + words[0]);
        string imagename = words[0].Substring(4);
        Tuple<List<string>, List<string>> TermsLists = CreateTermsListsForImageCard(words, pickedCategory);
        GameObject imageCardGameOject = new GameObject
        {
            name = imagename
        };
        ImageCard imageCard = imageCardGameOject.AddComponent<ImageCard>();
        imageCard.setup(imagename, TermsLists.Item1, TermsLists.Item2, image, helpText);

        RawImage rawImage = imageCardGameOject.AddComponent<RawImage>();
        rawImage.texture = image;

        imageCardGameOject.SetActive(false);

        return imageCardGameOject.GetComponent<ImageCard>();
    }

    private string GetTextAssetContent()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/" + SettingsTextAsset.name + ".txt");
        string fileContents = sr.ReadToEnd();
        sr.Close();
        return fileContents;
    }

    public List<ImageCard> CreateImageCards(string pickedCategory)
    {
        List<ImageCard> imageCards = new List<ImageCard>();
        string[] lines = GetTextAssetContent().Split("\n"[0]);
        for (int i = categoriesIndex; i < lines.Length;i++)
        {
            string[] words = lines[i].Split("\t"[0]);
            if (i == categoriesIndex)
            {
                string lastword = "";
                foreach (string word in words)
                {
                    if (word != "")
                    {
                        rawCategories.Add(word);
                    }
                    if (word != lastword)
                    {
                        categories.Add(new Tuple<string, List<string>>(word,null));
                    }
                    lastword = word;
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

        for (int k = 0; k < rawCategories.Count; k++)
        {
            if (pickedCategory == rawCategories[k])
            {
                if (words[k] == "1") localTerms.Add(terms[k]);
                if (words[k] == "x") localIgnoreTerms.Add(terms[k]);
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
