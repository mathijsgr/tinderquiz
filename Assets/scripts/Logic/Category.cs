using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class Category
{
    public string CategoryName = "";
    public int Score = 0;
    public List<Term> Terms = new List<Term>();
}
