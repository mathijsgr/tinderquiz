using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ImageScriptableObjectBuilder))]
public class ImageScriptableObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ImageScriptableObjectBuilder myScript = (ImageScriptableObjectBuilder) target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.BuildImageScriptableObjects();
        }
    }

}
