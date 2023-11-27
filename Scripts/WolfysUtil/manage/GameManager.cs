using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] bool useSingleton;
    public static GameManager singleton;
    
    private void Awake()
    {
        if(useSingleton){
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
