using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SetPlayerSkin : MonoBehaviour
{
   private SpriteRenderer sprite;

   private void Start() {
    sprite = gameObject.GetComponent<SpriteRenderer>();
    byte[] byteTexture = File.ReadAllBytes(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SelectedSkin") + ".png");
    Texture2D loadedTexture = new Texture2D(2, 2);
    loadedTexture.LoadImage(byteTexture);
    //sprite.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f));
    GetComponent<Renderer>().material.mainTexture = loadedTexture;
   }
}
