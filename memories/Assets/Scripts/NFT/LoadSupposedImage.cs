using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadSupposedImage : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void LoadAnImage(string fileName)
    {
        if (!File.Exists(Application.persistentDataPath + fileName))
        {
            Debug.Log("Error");
            return;
        }

        byte[] byteTexture = File.ReadAllBytes(Application.persistentDataPath + fileName);
        Texture2D loadedTexture = new Texture2D(0, 0);
        loadedTexture.LoadImage(byteTexture);
        spriteRenderer.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
    }
}
