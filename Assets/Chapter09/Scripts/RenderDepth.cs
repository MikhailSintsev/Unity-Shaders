using UnityEngine;

[ExecuteInEditMode]

public class RenderDepth : MonoBehaviour
{
    #region Variables

    public Shader curShader;
    public float depthPower = 0.2f;

    public Material screenMat;

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
            ScreenMat.SetFloat("_DepthPower", depthPower);
            Graphics.Blit(sourceTexture, destTexture, ScreenMat);
        }
        else
            Graphics.Blit(sourceTexture, destTexture);
    }

    private void Update()
    {
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
        depthPower = Mathf.Clamp(depthPower, 0, 1);
    }

    private void OnDisable()
    {
        if (screenMat)
            DestroyImmediate(screenMat);
    }
}
