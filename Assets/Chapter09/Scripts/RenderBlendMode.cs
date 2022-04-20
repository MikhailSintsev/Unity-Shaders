using UnityEngine;

[ExecuteInEditMode]

public class RenderBlendMode : MonoBehaviour
{
    #region Variables

    public Shader curShader;
    public Texture2D blendTexture;
    public float blendOpacity = 1.0f;

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
            ScreenMat.SetTexture("_BlendTex", blendTexture);
            ScreenMat.SetFloat("_Opacity", blendOpacity);

            Graphics.Blit(sourceTexture, destTexture, ScreenMat);
        }
        else
            Graphics.Blit(sourceTexture, destTexture);
    }

    private void Update()
    {
        blendOpacity = Mathf.Clamp(blendOpacity, 0.0f, 1.0f);
    }

    private void OnDisable()
    {
        if (screenMat)
            DestroyImmediate(screenMat);
    }
}
