using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Determine which map is going to be generated
    public enum DrawMode {NoiseMap, ColourMap, Mesh};
    public DrawMode drawMode;

    // values defining the noise map
    public const int mapChunkSize = 241;      // this will replace mapWidth and mapHeight
    [Range(0,6)]                        // multiply the LOD by 2
    public int levelOfDetail;           // LOD
    public float noiseScale;

    public int octaves;
    [Range(0,1)]                // slider
    public float persistance;   // the persistance should always be in a range of 0 to 1
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    // Fetch the 2D noise map from the Noise class
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, 
                                                   octaves, persistance, lacunarity, offset);

        // Create a Colour Map
        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        // loop through the noise map produced to figure out the height and width values
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                // loop through all of the regions 
                for (int i = 0; i < regions.Length; i++)
                {
                    // if region found, break
                    if (currentHeight <= regions[i].height) {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;    // save colour
                        break;
                    }
                }
            }

        }

        // pass along to the MapDisplay class - draw noisemap to the screen
        MapDisplay display = FindObjectOfType<MapDisplay>();
        // draw noise map
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        // draw colour map
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
        }
        // draw mesh
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
        }
    }

    // Called automatically whenever one of the scripts variables is changed in the inspector
    private void OnValidate()
    {
        if (lacunarity < 1)     // lacunarity should always be greater than 0
        {
            lacunarity = 1;
        }
        if (octaves < 0)        // octaves should never be negative
        {
            octaves = 0;
        }
    }
}


[System.Serializable]
public struct TerrainType
{
    public string name;     // name of the terrain type
    public float height;    // height of the terrain
    public Color colour;    // colour of the terrain
}