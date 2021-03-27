using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ImageScriptableObject", order = 1)]

public class ImageScriptableObject : ScriptableObject
{
    public string imageName;
    public List<string> terms;
    public List<string> ignoreTerms;
    public Sprite image;
    public string helpText;

    public void Setup(string imageName, List<string> terms, List<string> ignoreTerms, Sprite image, string helpText)
    {
        this.imageName = imageName;
        this.terms = terms;
        this.ignoreTerms = ignoreTerms;
        this.image = image;
        this.helpText = helpText;
    }

    public string GetImageName()
    {
        return imageName;
    }

    public List<string> GetTerms()
    {
        return terms;
    }

    public List<string> GetIgnoreTerms()
    {
        return ignoreTerms;
    }

    public Sprite GetImage()
    {
        return image;
    }

    public string GetHelpText()
    {
        return helpText;
    }



}
