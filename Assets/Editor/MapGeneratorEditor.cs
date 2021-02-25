using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Extend from Editor instead of MonoBehaviour
// This is a custom Editor of the MapGenerator class
[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // get reference to MapGenerator
        MapGenerator mapGen = (MapGenerator)target;

        // if any value is changed, then we can also generate the map
        if (DrawDefaultInspector ())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }

        // add a button - if the button is pressed, generate map
        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}
