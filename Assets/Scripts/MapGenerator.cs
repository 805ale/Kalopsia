using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // values defining the map
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public bool autoUpdate;

    // Fetch the 2D noise map from the Noise class
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        // pass along to the MapDisplay class - draw noisemap to the screen
        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}
