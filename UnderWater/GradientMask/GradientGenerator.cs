using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientGenerator : MonoBehaviour {

    public int gradientTextureWidth = 512;
    public int gradientTextureHeight = 512;

    public float strengthThreshold;
    public Texture2D texture2;
    public Material gradientMat;

    private void Start()
    {
        texture2 = GenerateGradientTexture(texture2);
        //GetComponent<RawImage>().texture = texture2;
        GetComponent<Image>().material.mainTexture = texture2;
    }
    public Texture2D GenerateGradientTexture(Texture2D tex)
    {
        tex = new Texture2D(gradientTextureWidth, gradientTextureHeight, TextureFormat.RGBA32, true);
        Vector2 texCenter = new Vector2(gradientTextureWidth * 0.5f, gradientTextureHeight * 0.5f);

        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                float dstFromCenter = Vector2.Distance(texCenter, new Vector2(x, y));
                float maskPix = (float)(0.5 - (dstFromCenter / gradientTextureWidth)) * strengthThreshold;
                tex.SetPixel(x, y, new Color(maskPix, maskPix, maskPix, -maskPix));
            }
        }
        tex.name = "GradientTexture";
        tex.Apply();
        return tex;
    }
}
