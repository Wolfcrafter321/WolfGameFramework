/*==============================
 *
 * Author       : Wolfcrafter321
 * Usage        : To Edit Mouse Lock State Easily in Unity.
 * How To Use   : Attach this component to some gameObject
 *                1) Edit Some in Inspector.
 *                2) You can update state from inspector components humburger button. (Context name = Update)
 *                3) Also you can call SetNewState from your script.
 * Note         : maybe, singleton doesnt worked pefrectly...?
 * Licence      : Almost Free! you can use this in any project(commericals too :9 ).
 *              : But don't "ReUpload", "say [I Coded This]".
 *
 *==============================*/
using UnityEngine.Networking;
using UnityEngine;

[AddComponentMenu("Wolf/Manager/CursorManager")]
public class CursorManager : MonoBehaviour
{

    #region singleton

    public static CursorManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("MAN : There is 2 more CursorManager.");
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public bool EscapeKeyToFree = true;
    public CursorLockMode lockmode;
    public bool visible;

    void Start()
    {
        UpdateState();
    }

    /*
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && EscapeKeyToFree)
        {
            SetNewState(CursorLockMode.None, true);
        }
    }
    */

    [ContextMenu("Update")]
    public void UpdateState()
    {
        Cursor.lockState = lockmode;
        Cursor.visible = visible;
    }

    public void SetNewState(CursorLockMode nlockmode, bool nvisible)
    {
        Cursor.lockState = nlockmode;
        Cursor.visible = nvisible;
    }

}
