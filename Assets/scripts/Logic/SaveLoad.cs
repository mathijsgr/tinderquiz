using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Assets.scripts.Images;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private static SaveLoad _instance;

    void Awake()
    {
        _instance = this;
    }

    public static SaveLoad GetInstance()
    {
        return _instance;
    }

    public void SaveCategories(List<Category> categories)
    {
        string destination = Application.persistentDataPath + "/save.dat";

        if (File.Exists(destination))
        {
            File.Delete(destination);
        }
        using (Stream stream = File.Open(destination, FileMode.Create))
        {
            BinaryFormatter bin = new BinaryFormatter();
            bin.Serialize(stream, categories);
        }
    }

    public List<Category> LoadCategories()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (!File.Exists(destination))
        {
            return new List<Category>();
        }

        List<Category> categories = new List<Category>();
        using (Stream stream = File.Open(destination, FileMode.Open))
        {
            BinaryFormatter bin = new BinaryFormatter();

            try
            {
                categories = (List<Category>)bin.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }

        return categories;
    }
}