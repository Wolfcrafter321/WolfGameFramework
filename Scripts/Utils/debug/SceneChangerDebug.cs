using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wolf
{
    [AddComponentMenu("Wolf/Debug/SceneChangerDebug")]
    public class SceneChangerDebug : MonoBehaviour
    {

        //public GUIStyle style;

        private void OnGUI()
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                //if (GUI.Button(new Rect(20, 20 + i * 50, 150, 35), i.ToString(), style))
                if (GUI.Button(new Rect(20, 20 + i * 50, 150, 35), i.ToString()))
                {
                    BasicGameManager.singleton.LoadScene(i);
                }
            }

        }

    }
}
