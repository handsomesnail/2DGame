using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTexture : MonoBehaviour
{

	public void generateTexture(int width, int height)
    {
        Texture2D proceduralTexture = new Texture2D(width, height,TextureFormat.Alpha8,false);

        Color[] colors = new Color[width * height];

        for(int i = 0;i<colors.Length;i++)
        {
            colors[i] = Color.clear;
        }

        proceduralTexture.SetPixels(colors);
        proceduralTexture.Apply();

    }
}
