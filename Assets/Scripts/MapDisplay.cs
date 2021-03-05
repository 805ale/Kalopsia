using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Turn the noisemap into a texture
public class MapDisplay : MonoBehaviour
{
    // Reference to the renderer
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    //Draw a texture to the screen
    public void DrawTexture(Texture2D texture)
    { 
        // apply the texture to the texture renderer
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    // Draw a mesh to the screen
    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}