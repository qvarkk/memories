using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class skpirtgovna : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        byte[] byteTexture = File.ReadAllBytes(Application.persistentDataPath + PlayerPrefs.GetString("SkinContract1"));
        Texture2D loadedTexture = new Texture2D(0, 0);
        loadedTexture.LoadImage(byteTexture);
        spriteRenderer.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
    }

}
