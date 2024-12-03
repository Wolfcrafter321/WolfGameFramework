using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Wolf
{
    [AddComponentMenu("Wolf/Manager/BasicGameManager")]
    public class BasicGameManager : MonoBehaviour
    {

        [SerializeField] bool useSingleton;
        public static BasicGameManager singleton;

        private void Awake()
        {
            if (useSingleton)
            {
                if (singleton == null)
                    singleton = this;
                else
                    Destroy(this.gameObject);
            }

        }

        public void LoadScene(int id)
        {
            SceneManager.LoadScene(id);
        }
        public void LoadScene(string nm)
        {
            SceneManager.LoadScene(nm);
        }

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

    }
}