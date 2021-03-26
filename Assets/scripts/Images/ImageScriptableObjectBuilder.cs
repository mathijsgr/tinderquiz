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
    public List<String> settingsText;


    public void BuildImageScriptableObjects()
    {
        GetSettings();
    }

    private void CreateMyAsset(String name, string category, bool isCorrect, RawImage image, String helpText)
    {
        ImageScriptableObject asset = ScriptableObject.CreateInstance<ImageScriptableObject>();

        AssetDatabase.CreateAsset(asset, "Assets/ImageScriptableObjects/"+category+"/"+name+".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
        asset.Setup(name,category,isCorrect,image,helpText);
    }

    private void GetSettings()
    {
        var sr = new StreamReader(Application.dataPath + "/" + SettingsTextAsset.name + ".txt");
        var fileContents = sr.ReadToEnd();
        sr.Close();

        string[] lines = fileContents.Split("\n"[0]);
        foreach (string line in lines)
        {
            settingsText.Add(line);
        }
    }
}
