using UnityEngine;

public class RenderTextureScaler : MonoBehaviour
{
    [Header("Target")]
    public RenderTexture tex;

    [Header("Scale"), Range(0.1f, 1f)]
    public float scale = 1.0f;  // (10% - 100%)

    [Header("Camera")]
    public bool refreshCamera;
    public Camera rtCam;

    [Header("Debug?")]
    public bool debug;

    private void Start()
    {
        if (refreshCamera) UpdateRenderTexture();
    }

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
        if (debug)
        {
            scale = GUI.HorizontalSlider(new Rect(250, 20, 300, 80), scale, 0.1f, 1.0f);

            GUI.Label(new Rect(250, 20, 300, 80), $"Scale: {Mathf.RoundToInt(scale * 100)}%");

            if (GUI.changed)
            {
                UpdateRenderTexture();
            }
        }
    }
}
