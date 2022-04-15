using UnityEngine;

[ExecuteInEditMode]

public class RenderBSC : MonoBehaviour
{
    #region Variables

    public Shader curShader;
    public float brightness = 1.0f;
    public float saturation = 1.0f;
    public float contrast = 1.0f;

    private Material screenMat;

    #endregion

    #region Properties

    Material ScreenMat
    {
        get
        {
            if (screenMat == null)
            {
                screenMat = new Material(curShader);
                screenMat.hideFlags = HideFlags.HideAndDontSave;
            }
            return screenMat;
        }
    }
    #endregion

    private void Start()
    {
        if (!curShader && !curShader.isSupported)
            enabled = false;
    }

    private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (curShader != null)
        {
            ScreenMat.SetFloat("_Brightness", brightness);
            ScreenMat.SetFloat("_Saturation", saturation);
            ScreenMat.SetFloat("_Contrast", contrast);

            Graphics.Blit(sourceTexture, destTexture, ScreenMat);
        }
        else
            Graphics.Blit(sourceTexture, destTexture);
    }

    private void Update()
    {
        brightness = Mathf.Clamp(brightness, 0.0f, 2.0f);
        saturation = Mathf.Clamp(saturation, 0.0f, 2.0f);
        contrast = Mathf.Clamp(contrast, 0.0f, 3.0f);
    }

    private void OnDisable()
    {
        if (screenMat)
            DestroyImmediate(screenMat);
    }
}
