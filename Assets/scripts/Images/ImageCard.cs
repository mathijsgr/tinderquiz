﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.Images
{
    public class ImageCard : MonoBehaviour
    {
        private string imageName;
        private List<string> terms;
        private List<string> ignoreTerms;
        private Texture2D image;
        private string helpText;

        public void setup(string imageName, List<string> terms, List<string> ignoreTerms, Texture2D image, string helpText)
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

        public Texture2D GetImage()
        {
            return image;
        }

        public string GetHelpText()
        {
            return helpText;
        }
    }
}