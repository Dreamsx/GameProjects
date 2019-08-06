using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Ef_WaterWave : MonoBehaviour 
{
    public Texture[] textures;
    private Material material;
    private int Index;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        InvokeRepeating("changeTexture", 0, 0.1f); 
    }

    void changeTexture()
    {
        material.mainTexture = textures[Index];
        Index = (Index + 1) % textures.Length;
    }

}
