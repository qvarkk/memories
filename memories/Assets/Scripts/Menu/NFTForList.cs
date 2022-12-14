using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class NFTForList : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite noImageSprite;

    TMP_Dropdown dropdown;

    private void Start() {
        dropdown = gameObject.GetComponent<TMP_Dropdown>();
        StateChange();
    }

    public void StateChange()
    {
        if (File.Exists(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SkinContract" + (dropdown.value + 1).ToString()) + ".png"))
        {
            byte[] byteTexture = File.ReadAllBytes(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SkinContract" + (dropdown.value + 1).ToString()) + ".png");
            Texture2D loadedTexture = new Texture2D(0, 0);
            loadedTexture.LoadImage(byteTexture);
            image.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero); 
        }
        else
        {
            image.sprite = noImageSprite;
        }
    }
}
