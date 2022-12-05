using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;


public class LoadImagesToUI : MonoBehaviour
{
    Image imageRenderer;

    [SerializeField] TMP_Text contractList;
    [SerializeField] TMP_Text imageList;
    [SerializeField] GameObject image;
    GameObject[] imagesArray;
    GameObject imageToChange;

    public void Start()
    {
        int k = 0;
        for (int i = 1; i < PlayerPrefs.GetInt("TexturesQuantity") + 1; i++)
        {
            contractList.text += "\n\n" + PlayerPrefs.GetString("TextureName" + i.ToString());
            Instantiate(image, new Vector3(485f, 400f - k, 0f), Quaternion.identity, imageList.transform);

            imagesArray = GameObject.FindGameObjectsWithTag("nftImage");
            imageToChange = imagesArray[i - 1];

            imageRenderer = imageToChange.GetComponent<Image>();

            if (!File.Exists(Application.persistentDataPath + "playerTexture" + i.ToString()))
            {
                Debug.Log("Error");
                return;
            }

            byte[] byteTexture = File.ReadAllBytes(Application.persistentDataPath + "playerTexture" + i.ToString());
            Texture2D loadedTexture = new Texture2D(0, 0);
            loadedTexture.LoadImage(byteTexture);
            imageRenderer.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
            
            k += 55;
        }
    }
}
