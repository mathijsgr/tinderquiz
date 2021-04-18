using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class Categories : MonoBehaviour
{
    [NonSerialized] private static Categories _instance;
    public List<Category> CategoriesList = new List<Category>();

    void Awake()
    {
        _instance = this;
    }

    public static Categories GetInstance()
    {
        return _instance;
    }
}
