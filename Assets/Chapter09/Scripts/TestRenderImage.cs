using UnityEngine;

[ExecuteInEditMode]

public class TestRenderImage : MonoBehaviour
{
    #region Variables

    public Shader curShader;
    public float greyscaleAmount = 1.0f;

    private Material screenMat;

    #endregion

    #region Properties

    Material ScreenMat
    {
        get
        {
            if ( screenMat == null )
            {
                screenMat = new Material( curShader );
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
            ScreenMat.SetFloat("_Luminosity", greyscaleAmount);
            Graphics.Blit(sourceTexture, destTexture, ScreenMat);
        }
        else
            Graphics.Blit(sourceTexture, destTexture);
    }

    private void Update()
    {
        greyscaleAmount = Mathf.Clamp(greyscaleAmount, 0.0f, 1.0f);
    }

    private void OnDisable()
    {
        if (screenMat)
            DestroyImmediate(screenMat);
    }
}
