using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class GlitchControl : MonoBehaviour
{
    public Material shadowMaterial;
    private Camera cam;


    [Range(0, 10)]
    public float Redch;
    [Range(0, 10)]
    public float Greench;
    [Range(0, 10)]
    public float Bluech;
    [Range(0.1f, 10)]
    public float glipower;//1~10
    [Range(1, 100)]
    public float GlitchflowSpeed;
    [Range(0.1f, 5)]
    public float glitchuv;//0.1~5
    public bool zero;


    public void GlitchTextureChange(Texture glitchTexture)
    {
        shadowMaterial.SetTexture("_MainTex6", glitchTexture);
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.DepthNormals;
    }


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        shadowMaterial.SetFloat("_Redch", Redch);
        shadowMaterial.SetFloat("_Greench", Greench);
        shadowMaterial.SetFloat("_Bluech", Bluech);
        shadowMaterial.SetFloat("_glipower", glipower);
        shadowMaterial.SetFloat("_GlitchflowSpeed", GlitchflowSpeed);
        shadowMaterial.SetFloat("_glitchuv", glitchuv);
        if (zero == true)
        {
            shadowMaterial.SetFloat("_zero", 1);
        }
        else
        {
            shadowMaterial.SetFloat("_zero", 0);
        }

        Graphics.Blit(source, destination, shadowMaterial);
    }
}