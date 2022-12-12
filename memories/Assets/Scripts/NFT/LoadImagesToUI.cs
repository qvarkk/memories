using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;


public class LoadImagesToUI : MonoBehaviour
{
    Image imageRenderer;

    [SerializeField] GameObject[] coordinatesSemple;
    [SerializeField] TMP_Text contractList;
    [SerializeField] TMP_Text imageList;
    [SerializeField] GameObject image;
    GameObject[] imagesArray;
    GameObject imageToChange;
    [SerializeField] AddNFTPrompt script;

    private void Start() {
        RefreshPictures();
    }

    public void RefreshPictures()
    {
        for (int i = 1; i < PlayerPrefs.GetInt("TexturesQuantity") + 1; i++)
        {
            if (!File.Exists(Application.persistentDataPath + "/playerTexture" + i.ToString() + ".png"))
            {
                script.ThrowAnError("Произошла системная ошибка. Файл с NFT не был найден.");
                continue;
            }

            Vector3 coord = coordinatesSemple[i-1].transform.position;
            contractList.text += "\n\n" + PlayerPrefs.GetString("TextureName" + i.ToString());
            Instantiate(image, coord, Quaternion.identity, imageList.transform);

            imagesArray = GameObject.FindGameObjectsWithTag("nftImage");
            imageToChange = imagesArray[i - 1];

            imageRenderer = imageToChange.GetComponent<Image>();

            byte[] byteTexture = File.ReadAllBytes(Application.persistentDataPath + "/" + "playerTexture" + i.ToString() + ".png");
            Texture2D loadedTexture = new Texture2D(0, 0);
            loadedTexture.LoadImage(byteTexture);
            imageRenderer.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
        }
    }

    public void DeletePicture()
    {
        for (int i = 1; i < PlayerPrefs.GetInt("TexturesQuantity") + 1; i++)
        {
            if (!File.Exists(Application.persistentDataPath + "/playerTexture" + i.ToString() + ".png"))
            {
                script.ThrowAnError("Произошла системная ошибка. Нахуя ты файлы дергал.");
                continue;
            }
            File.Delete(Application.persistentDataPath + "/" + "playerTexture" + i.ToString() + ".png");
            PlayerPrefs.SetString("TextureName" + i.ToString(), "");
        }
        PlayerPrefs.SetInt("TexturesQuantity", 0);
        RefreshPictures();
    }
}
