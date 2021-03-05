using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Noise class
public static class Noise 
{
    // Generate a noise map that returns a grid of values between 0 and 1
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, 
                                            int octaves, float persistance, float lacunarity,
                                            Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // the seed is useful for when we want to get the same map again
        System.Random prng = new System.Random(seed);       // pseudo-random number generator
        // Each octave will be sampled from a different location, therefore we create an array
        Vector2[] octaveOffsets = new Vector2[octaves];

        //Loop through all octaves
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;  // scroll through the noise on the x axis
            float offsetY = prng.Next(-100000, 100000) + offset.y;  // scroll through the noise on the y axis
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if(scale <=0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;      //max noise height
        float minNoiseHeight = float.MaxValue;      //min noise height

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;        //amplitude
                float frequency = 1;        //frequency
                float noiseHeight = 0;      //height value
                
                for(int i = 0; i < octaves; i++)
                {
                    // this is useful for when we change the noise scale - zoom in to the center of the noise map
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    // the perlin value could sometimes be negative so that the noise height would decrease
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    // increase noise height by the Perlin value of each octave
                    noiseHeight += perlinValue * amplitude;

                    // at the end of each octave, the amplitude gets multipliet by the persistance value
                    amplitude *= persistance;
                    // the frequency increases each octave
                    frequency *= lacunarity;
                }

                // keep track of the lowest and highest values in the noise map
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                } 
                else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }
        
        for(int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}
