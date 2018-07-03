using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture2DArrayBehaviour : MonoBehaviour {
    public Texture2D[] _textures;

    void Start() {
        Texture2DArray texture2DArray = new Texture2DArray(_textures[0].width, _textures[0].height, _textures.Length, TextureFormat.RGBA32, false, false);
        
        texture2DArray.filterMode = FilterMode.Bilinear;
        texture2DArray.wrapMode = TextureWrapMode.Repeat;
        
        for (int i = 0; i < _textures.Length; i++) {
            texture2DArray.SetPixels(_textures[i].GetPixels(0), i, 0);
        }

        texture2DArray.Apply();
        gameObject.GetComponent<Renderer>().sharedMaterial.SetTexture("_Textures", texture2DArray);
    }
}