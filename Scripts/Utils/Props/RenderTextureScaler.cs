using UnityEngine;

public class RenderTextureScaler : MonoBehaviour
{
    [Header("Target")]
    public RenderTexture tex;

    [Header("Scale"), Range(0.1f, 1f)]
    public float scale = 1.0f;  // ?????_?[?e?N?X?`????X?P?[?? (10% - 100%)

    [Header("Camera")]
    public bool refreshCamera;
    public Camera rtCam;


    private void UpdateRenderTexture()
    {
        if(refreshCamera) rtCam.ResetAspect();
        int width = Mathf.Clamp(Mathf.RoundToInt(Screen.width * scale), 1, Screen.width);
        int height = Mathf.Clamp(Mathf.RoundToInt(Screen.height * scale), 1, Screen.height);
        tex.Release();
        tex.width = width;
        tex.height = height;
        tex.Create();

    }

    private void OnGUI()
    {
        // ?X???C?_?[??????? (10% - 100%)
        scale = GUI.HorizontalSlider(new Rect(20, 20, 300, 40), scale, 0.1f, 1.0f);

        // ?X???C?_?[??l??\??
        GUI.Label(new Rect(400, 400, 300, 60), $"Scale: {Mathf.RoundToInt(scale * 100)}%");

        // ?????_?[?e?N?X?`????X?V
        if (GUI.changed)
        {
            UpdateRenderTexture();
        }
    }
}
