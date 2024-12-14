#if UNITY_EDITOR
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InitWolfFolder : MonoBehaviour
{

    [MenuItem("Wolf/Init Wolf Workspace")]
    static void Test()
    { // フォルダ構成のルート
        string root = "Assets";

        // 作成するフォルダ一覧
        string[] folders = new string[]
        {
            "Art",
            "Art/Materials",
            "Art/VFX",
            "Art/Models",
            "Art/Characters",
            "Art/Characters/Sample",
            "Art/Characters/Sample/FBX",
            "Scripts",
            "Scripts/Gameplay",
            "Scripts/Editor",
            "Resources",
            "Scenes",
            "Settings",
            "Audio",
            "Audio/Music",
            "Audio/SFX"
        };


        foreach (string folder in folders)
        {
            string folderPath = Path.Combine(root, folder);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            else
            {
                Debug.Log($"Folder already exists: {folderPath}");
            } 
        }

        AssetDatabase.Refresh();
    }
}
#endif