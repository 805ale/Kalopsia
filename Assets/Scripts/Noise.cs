using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Noise class
public static class Noise 
{
    // Generate a noise map that returns a grid of values between 0 and 1
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if(scale <=0)
        {
            scale = 0.0001f;
        }

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                // sample coordinates
                float sampleX = x / scale;
                float sampleY = y / scale;

                // apply sample coordinates to noise map
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }
}
