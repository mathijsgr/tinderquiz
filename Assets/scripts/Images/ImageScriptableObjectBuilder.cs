using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ImageScriptableObjectBuilder : MonoBehaviour
{
    public TextAsset SettingsTextAsset;
    public String FolderPath;
    public List<String> categories;
    public List<String> terms;
    public List<String> imageInfos;

    public List<Sprite> images;


    public void BuildImageScriptableObjects()
    {
        GetSettings();
    }

    private void CreateMyAsset(string name,List<string> terms, List<string> ignoreTerms, Sprite image, string helpText)
    {
        ImageScriptableObject asset = ScriptableObject.CreateInstance<ImageScriptableObject>();

        AssetDatabase.CreateAsset(asset, "Assets/ImageScriptableObjects/"+name+".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
        asset.Setup(name,terms,ignoreTerms,image,helpText);
    }

    private void GetSettings()
    {
        var sr = new StreamReader(Application.dataPath + "/" + SettingsTextAsset.name + ".txt");
        var fileContents = sr.ReadToEnd();
        sr.Close();

        string[] lines = fileContents.Split("\n"[0]);
        for (int i = 1; i < lines.Length;i++)
        {
            string[] words = lines[i].Split("	"[0]);
            if (i == 1)
            {
                CreateList(categories, words);
            }
            else if (i == 2)
            {
                CreateList(terms, words);
                terms.RemoveAt(0);
            }
            else
            {
                if (words[0] == "") break;
                Sprite image = Resources.Load<Sprite>("Images/"+words[0]);
                string imagename = words[0].Substring(4);
                List<string> localTerms = new List<string>();
                List<string> localIgnoreTerms = new List<string>();
                for (int j = 2; j < words.Length; j++)
                {
                    if (words[j] == "1") localTerms.Add(terms[j]);
                    if (words[j] == "x") localIgnoreTerms.Add(terms[j]);
                }
                CreateMyAsset(imagename, localTerms, localIgnoreTerms, image, "");
            }
        }
    }
    private void CreateList(List<string> list, string[] words)
    {
        list.Clear();
        foreach (string word in words)
        {
            if (word != "")
            {
                list.Add(word);
            }
        }
    }
}
