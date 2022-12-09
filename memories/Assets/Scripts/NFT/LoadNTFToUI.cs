using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class LoadNTFToUI : MonoBehaviour
{
    Image imageRenderer;

    [SerializeField] TMP_Text contractList;
    [SerializeField] TMP_Text imageList;
    [SerializeField] GameObject image;
    [SerializeField] GameObject coordinatesSemple;
    GameObject[] imagesArray;
    GameObject imageToChange;

    public void ReloadNFTs()
    {
        int k = 0;
        for (int i = 1; i < PlayerPrefs.GetInt("SkinsQuantity") + 1; i++)
        {
            if (!File.Exists(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SkinContract" + i.ToString()) + ".png"))
            {
                Debug.Log("Error");
                continue;
            }

            contractList.text += "\n\n" + PlayerPrefs.GetString("SkinContract" + i.ToString());
            Instantiate(image, coordinatesSemple.transform.position += Vector3.up * -k, Quaternion.identity, imageList.transform);

            imagesArray = GameObject.FindGameObjectsWithTag("nftImage");
            imageToChange = imagesArray[i - 1];

            imageRenderer = imageToChange.GetComponent<Image>();

            byte[] byteTexture = File.ReadAllBytes(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SkinContract" + i.ToString()) + ".png");
            Texture2D loadedTexture = new Texture2D(0, 0);
            loadedTexture.LoadImage(byteTexture);
            imageRenderer.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
            
            k += 55;
        }
    }
}
