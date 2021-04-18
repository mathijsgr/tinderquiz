using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.Images;

public class ImageCardBuilder : MonoBehaviour
{
    private static ImageCardBuilder _instance;

    public TextAsset SettingsTextAsset;
    public List<Term> allTerms = new List<Term>();
    private List<string> rawCategories = new List<string>();
    private List<string> terms = new List<string>();
    private string[] lines;
    private int categoriesIndex = 0;
    private int termsIndex = 1;


    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        lines = GetTextAssetContent().Split("\n"[0]);
        rawCategories = new List<string>(lines[categoriesIndex].Split("\t"[0]));

        rawCategories.RemoveAt(0);
        terms = new List<string>(lines[termsIndex].Split("\t"[0]));
        terms.RemoveAt(0);
    }

    public static ImageCardBuilder GetInstance()
    {
        return _instance;
    }

    private string GetTextAssetContent()
    {
        string fileContents = Resources.Load<TextAsset>(SettingsTextAsset.name).ToString();
        return fileContents;
    }

    public List<ImageCard> CreateImageCards()
    {
        List<ImageCard> imageCards = new List<ImageCard>();

        BuildListOfAllCategoriesWithTerms();
        for (int i = 2; i < lines.Length; i++)
        {
            string[] words = lines[i].Split("\t"[0]);

            if (words[0] == "") break; //skip if empty
            imageCards.Add(CreateMyAsset("", words));
        }
        return imageCards;
    }

    private ImageCard CreateMyAsset(string helpText, string[] words)
    {
        Texture2D image2D = Resources.Load<Texture2D>("Images/" + words[0]);
        Sprite image = Sprite.Create(image2D, new Rect(0.0f, 0.0f, image2D.width, image2D.height),
            new Vector2(0.5f, 0.5f), 100.0f);
        string imagename = words[0];
        Tuple<List<Category>, List<Category>> termsLists = CreateTermsListsForImageCard(words);

        ImageCard imageCard = new ImageCard();
        imageCard.ImageName = imagename;
        imageCard.Categories = termsLists.Item1;
        imageCard.IgnoreCategories = termsLists.Item2;
        imageCard.HelpText = helpText;
        imageCard.Image = image;

        return imageCard;
    }

    public Tuple<List<Category>, List<Category>> CreateTermsListsForImageCard(string[] words)
    {
        List<Category> localCategories = new List<Category>();
        List<Category> localIgnoreCategories = new List<Category>();
        words = words.Skip(1).ToArray();

        foreach (Category category in Categories.GetInstance().CategoriesList)
        {
            List<Term> terms = new List<Term>();
            List<Term> ignoreTerms = new List<Term>();
            for (int i = 0; i < words.Length; i++)
            {
                if (rawCategories[i] == category.CategoryName)
                {
                    if (words[i] == "1") terms.Add(allTerms[i]);
                    if (words[i] == "x") ignoreTerms.Add(allTerms[i]);
                }
            }

            localCategories.Add(new Category
            {
                CategoryName = category.CategoryName,
                Terms = terms
            });
            localIgnoreCategories.Add(new Category
            {
                CategoryName = category.CategoryName,
                Terms = ignoreTerms
            });
        }

        return new Tuple<List<Category>, List<Category>>(localCategories, localIgnoreCategories);
    }

    public List<Category> BuildListOfAllCategoriesWithTerms()
    {
        string lastWord = "";
        List<Category> categories = new List<Category>();
        foreach (string category in rawCategories)
        {
            if (lastWord != category)
            {
                categories.Add(new Category
                {
                    CategoryName = category
                });
                lastWord = category;
            }
        }

        categories.RemoveAt(categories.Count - 1);
        foreach (Category category in categories)
        {
            for (int i = 0; i < rawCategories.Count; i++)
            {
                if (rawCategories[i] == category.CategoryName)
                {
                    Term term = new Term {TermName = terms[i]};
                    category.Terms.Add(term);
                    allTerms.Add(term);
                }
            }
        }

        return categories;
    }
}