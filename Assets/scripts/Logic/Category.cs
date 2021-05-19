using System;
using System.Collections.Generic;

[Serializable]
public class Category
{
    public string CategoryName = "";
    public int Score = 0;
    public List<Term> Terms = new List<Term>();
}
