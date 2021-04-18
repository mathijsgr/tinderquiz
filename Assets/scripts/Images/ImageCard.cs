using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.Images
{
    public class ImageCard
    {
        public string ImageName;
        public List<Category> Categories;
        public List<Category> IgnoreCategories;
        public Sprite Image;
        public string HelpText;
    }
}