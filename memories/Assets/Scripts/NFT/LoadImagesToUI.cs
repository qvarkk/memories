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

    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] ImageForList dropdownScript;

    private void Start() {
        RefreshPictures();
    }

    public void RemovePictureFromUI()
    {
        contractList.text = "Название";
        foreach (Transform child in imageList.transform)
        {
             Destroy(child.gameObject);
        }
        dropdownScript.StateChange();
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
        script.ThrowAConfirmScreen("Вы уверены? Вы не сможете их вернуть.", () => {
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
            RemovePictureFromUI();
            RefreshPictures();
        });
    }

    public void ChooseSkin()
    {
        if (File.Exists(Application.persistentDataPath + "/playerTexture" + (dropdown.value + 1).ToString() + ".png")){
            PlayerPrefs.SetString("SelectedSkin", "playerTexture" + (dropdown.value + 1).ToString());
            script.ThrowAnSuccessMessage("Скин успешно выбран!");
        }
        else{
            script.ThrowAnError("Такого файла не существует.");
        }
    }
}
