using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Turn the noisemap into a texture
public class MapDisplay : MonoBehaviour
{
    // Reference to the renderer
    public Renderer textureRender;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        // get the width and height of the noisemap
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        // create a 2D texture
        Texture2D texture = new Texture2D(width, height);

        // create a colour array
        Color[] colourMap = new Color[width * height];
        // loop through all of the values in the noisemap
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // get the index of the colour map and set it to a colour between black and white
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        // set the colour of each of the pixels in the texture and apply to texture
        texture.SetPixels(colourMap);
        texture.Apply();

        // apply the texture to the texture renderer
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(width, 1, height);
    }
}