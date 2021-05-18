using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.scripts.Images;

public class ImageCardBuilder : MonoBehaviour
{
    private static ImageCardBuilder _instance;

    public TextAsset SettingsTextAsset;
    public TextAsset HelpTextsAsset;
    private string[] helpTexts;
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

        TextAsset fileContents = Resources.Load<TextAsset>(HelpTextsAsset.name);
        string contents = Encoding.UTF8.GetString(fileContents.bytes);
        helpTexts = contents.Split("\n"[0]);
        rawCategories.RemoveAt(0);
        rawCategories.RemoveAt(0);
        rawCategories.RemoveAt(0);
        terms = new List<string>(lines[termsIndex].Split("\t"[0]));
        terms.RemoveAt(0);
        terms.RemoveAt(0);
        terms.RemoveAt(0);
    }

    public static ImageCardBuilder GetInstance()
    {
        return _instance;
    }

    private string GetTextAssetContent()
    {
        TextAsset fileContents = Resources.Load<TextAsset>(SettingsTextAsset.name);
        string contents = Encoding.UTF8.GetString(fileContents.bytes);
        return contents;
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
        Texture2D image2D = Resources.Load("Images/" + words[0]) as Texture2D;
        Sprite image = Sprite.Create(image2D, new Rect(0.0f, 0.0f, image2D.width, image2D.height),
            new Vector2(0.5f, 0.5f), 100.0f);
        Tuple<List<Category>, List<Category>> termsLists = CreateTermsListsForImageCard(words);

        ImageCard imageCard = new ImageCard
        {
            Title = words[2],
            SubTitle = words[1],
            Categories = termsLists.Item1,
            IgnoreCategories = termsLists.Item2,
            HelpText = helpText,
            Image = image
        };
        return imageCard;
    }

    public Tuple<List<Category>, List<Category>> CreateTermsListsForImageCard(string[] words)
    {
        List<Category> localCategories = new List<Category>();
        List<Category> localIgnoreCategories = new List<Category>();
        words = words.Skip(3).ToArray();

        foreach (Category category in Categories.GetInstance().CategoriesList)
        {
            List<Term> lTerms = new List<Term>();
            List<Term> ignoreTerms = new List<Term>();
            for (int i = 0; i < words.Length; i++)
            {
                if (rawCategories[i] == category.CategoryName)
                {
                    if (words[i] == "1") lTerms.Add(allTerms[i]);
                    if (words[i] == "x") ignoreTerms.Add(allTerms[i]);
                }
            }

            localCategories.Add(new Category
            {
                CategoryName = category.CategoryName,
                Terms = lTerms
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
                    string helpText = GetHelpText(terms[i]);
                    Term term = new Term
                    {
                        TermName = terms[i],
                        HelpText = helpText
                    };
                    category.Terms.Add(term);
                    allTerms.Add(term);
                }
            }
        }

        return categories;
    }

    public string GetHelpText(string termName)
    {
        foreach (var helpText in helpTexts)
        {
            string[] words = helpText.Split("\t"[0]);
            if (words[0] == termName)
            {
                return words[1];
            }
        }
        return "";
    }
}