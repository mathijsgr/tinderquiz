using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Categories : MonoBehaviour
{
    [NonSerialized] private static Categories _instance;
    public List<Category> CategoriesList = new List<Category>();
    public int TotalScore;

    void Awake()
    {
        _instance = this;
    }

    public static Categories GetInstance()
    {
        return _instance;
    }
}
