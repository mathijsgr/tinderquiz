using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.Images
{
    public class ImageCard : MonoBehaviour
    {
        private String name;
        private String category;
        private List<String> terms;
        private bool isCorrect;
        private RawImage image;
        private String helpText;

        public ImageCard(String name, string category, List<String> terms, bool isCorrect, RawImage image, String helpText)
        {
            this.name = name;
            this.category = category;
            this.terms = terms;
            this.isCorrect = isCorrect;
            this.image = image;
            this.helpText = helpText;
        }

        public String GetName()
        {
            return name;
        }

        public String GetCategory()
        {
            return category;
        }

        public List<String> GetTerms()
        {
            return terms;
        }

        public bool GetIsCorrect()
        {
            return isCorrect;
        }

        public RawImage GetImage()
        {
            return image;
        }

        public String GetHelpText()
        {
            return helpText;
        }
    }
}