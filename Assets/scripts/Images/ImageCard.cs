using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.Images
{
    public class ImageCard
    {
        public string Title;
        public List<Category> Categories;
        public List<Category> IgnoreCategories;
        public Sprite Image;
        public string HelpText;
        public string SubTitle;
    }
}