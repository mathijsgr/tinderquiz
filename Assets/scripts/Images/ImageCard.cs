using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.Images
{
    public class ImageCard : MonoBehaviour
    {
        private string imageName;
        public List<string> terms;
        public List<string> ignoreTerms;
        private Sprite image;
        private string helpText;

        public void setup(string imageName, List<string> terms, List<string> ignoreTerms, Sprite image, string helpText)
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
}