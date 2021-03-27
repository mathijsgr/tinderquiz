using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category : MonoBehaviour
{
    private string categoryName;
    private List<string> terms;
    
    public Category(string categoryName, List<string> terms)
    {
        this.categoryName = categoryName;
        this.terms = terms;
    }
    public string GetCategoryName()
    {
        return categoryName;
    }

    public List<string> GetTerms()
    {
        return terms;
    }

}
