using System.Net.NetworkInformation;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureEditor : MonoBehaviour
{
    [SerializeField]private Texture2D texture;
    [SerializeField]private FilterMode filterMode;
    [SerializeField]private TextureWrapMode textureWrapMode;
    [SerializeField][Range(2,512)]private int textureSize = 64;
    [SerializeField]private Material material;
    [SerializeField]private Camera camera;
    [SerializeField]private Collider collider;


    void OnValidate()
    {
        if(texture == null)
        {
            texture = new Texture2D(textureSize, textureSize);
        }
        
        if(texture.width != textureSize)
        {
            texture.Reinitialize(textureSize, textureSize);
        }
        texture.filterMode = filterMode;
        texture.wrapMode = textureWrapMode;
        material.mainTexture = texture;
        texture.Apply();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(collider.Raycast(ray, out hit, 100f))
            {
                int rayX = (int)(hit.textureCoord.x * textureSize);
                int rayY = (int)(hit.textureCoord.y * textureSize);
                texture.SetPixel(rayX, rayY, Color.black);
                texture.Apply();
            }
        }
    }
}
