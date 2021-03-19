using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScriptableObject : ScriptableObject
{
    public String name;
    public String Category;
    public bool IsCorrect;


    // Start is called before the first frame update
    public ImageScriptableObject(String name, String category, bool isCorrect)
    {
        this.name = name;
        this.Category = category;
        this.IsCorrect = isCorrect;
    }

}
