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
    {
        Debug.Log("Hi");
    }
}
#endif