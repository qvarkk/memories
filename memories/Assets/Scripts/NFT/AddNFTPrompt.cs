using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;
using TMPro;
using NFTPort;
using UnityEngine.UI;
using System.Net;



public class AddNFTPrompt : MonoBehaviour
{
    Image imageRenderer;

    [SerializeField] public TMP_Text contractList;
    [SerializeField] public TMP_Text imageList;
    [SerializeField] GameObject image;
    [SerializeField] GameObject[] coordinatesSemple;
    [SerializeField] Button reloadButton;
    [SerializeField] NFTForList dropdownScript;

    GameObject[] imagesArray;
    GameObject imageToChange;
    
    [SerializeField] GameObject errorField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] GameObject successField;
    [SerializeField] TMP_Text successText;
    [SerializeField] GameObject loadingField;
    [SerializeField] TMP_Text loadingText;
    [SerializeField] GameObject confirmField;
    [SerializeField] TMP_Text confirmText;
    [SerializeField] TMP_Dropdown dropdown;

    [SerializeField] Button confirmButton;
    [SerializeField] Button cancelButton;


    NFTs_model NFTsOfUser;

    private Action callback;

    public class Response {
        public string image;
    }

    private void Start() {
        SetupCallback();
    }

    public void StartAccountCheck() {
        NFTs_OwnedByAnAccount
            .Initialize()
            .SetChain(NFTs_OwnedByAnAccount.Chains.goerli)
            .SetAddress(PlayerPrefs.GetString("Account"))
            .SetInclude(NFTs_OwnedByAnAccount.Includes.metadata)
            .OnError(error=> {
                ThrowAnError("Произошла ошибка на стороне сервера. " + error);
            })
            .OnComplete(NFTs=> {
                NFTsOfUser = NFTs;
                SaveAvailableNFTs(NFTsOfUser);
            })
            .Run();
    }

    async private void SaveAvailableNFTs(NFTs_model NFTsOfUser)
    {
        int amount = NFTsOfUser.nfts.Count;
        int savedImages = 0;
        bool wereAnyErrors = false;

        ClearNFTs();
        ThrowALoadingScreen(true, "Ищем ваши NFT...");

        if (amount > 0)
        {
            reloadButton.interactable = false;
            for (int i = 0; i < amount && savedImages < 5; i++)
            {
                string contract = NFTsOfUser.nfts[i].contract_address;
                string tokenId = NFTsOfUser.nfts[i].token_id;
                string uri = NFTsOfUser.nfts[i].metadata_url;

                if (uri != null && uri != "ipfs://" && uri != "")
                {
                    if (uri.StartsWith("ipfs://"))
                    {
                        uri = uri.Replace("ipfs://", "https://ipfs.io/ipfs/");
                    }
                
                    UnityWebRequest webRequest = UnityWebRequest.Get(uri);
                    await webRequest.SendWebRequest();
                    Response data = JsonUtility.FromJson<Response>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));

                    string imageUri = data.image;
                    if (imageUri.StartsWith("ipfs://"))
                    {
                        imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
                    }
                    UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri);
                    await textureRequest.SendWebRequest();

                    byte[] textureBytes = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture.EncodeToPNG();
                    File.WriteAllBytes(Application.persistentDataPath +"/" + contract + tokenId + ".png", textureBytes);

                    PlayerPrefs.SetInt("SkinsQuantity", PlayerPrefs.GetInt("SkinsQuantity") + 1);
                    PlayerPrefs.SetString("SkinContract" + PlayerPrefs.GetInt("SkinsQuantity").ToString(), contract + tokenId);

                    savedImages++;
                }
                else
                {
                    wereAnyErrors = true;
                }
            }
            if (wereAnyErrors && savedImages > 1)
            {
                ThrowAnError("Все NFT были загружены, кроме тех, у которых отсутсвовала метадата :(");
            }
            else if (savedImages == 0)
            {
                ThrowAnError("У вас не было подходящих NFT на аккаунте :(");
            }
            else
            {
                ThrowAnSuccessMessage("Все NFT были успешно загружены!");
            }
            ReloadNFTs();
            reloadButton.interactable = true;
            ThrowALoadingScreen();
        }
    }
    public void ReloadNFTs()
    {
        ThrowALoadingScreen(true, "Обновляем информацию...");
        for (int i = 1; i < PlayerPrefs.GetInt("SkinsQuantity") + 1; i++)
        {
            if (!File.Exists(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SkinContract" + i.ToString()) + ".png"))
            {
                Debug.Log("Error");
                continue;
            }

            Vector3 coord = coordinatesSemple[i-1].transform.position;
            contractList.text += "\n\n" + PlayerPrefs.GetString("SkinContract" + i.ToString());
            Instantiate(image, coord, Quaternion.identity, imageList.transform);

            imagesArray = GameObject.FindGameObjectsWithTag("nftImage");
            imageToChange = imagesArray[i - 1];

            imageRenderer = imageToChange.GetComponent<Image>();

            byte[] byteTexture = File.ReadAllBytes(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SkinContract" + i.ToString()) + ".png");
            Texture2D loadedTexture = new Texture2D(0, 0);
            loadedTexture.LoadImage(byteTexture);
            imageRenderer.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
        }
        ThrowALoadingScreen();
        dropdownScript.StateChange();
    }

    public void ClearNFTs(){
        foreach (Transform child in imageList.transform)
        {
             Destroy(child.gameObject);
        }
        contractList.text = "Контракты";
        for (int i = 1; i < PlayerPrefs.GetInt("SkinsQuantity") + 1; i++)
        {
            PlayerPrefs.SetString("SkinContract" + i.ToString(), "");
            if(File.Exists(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SkinContract" + i.ToString()) + ".png"))
                File.Delete(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SkinContract" + i.ToString()) + ".png");
        }
        PlayerPrefs.SetInt("SkinsQuantity", 0);
        dropdownScript.StateChange();
    }

    public void ThrowAnError(string error)
    {
        errorField.SetActive(true);
        errorText.text = error;
    }

    public void ThrowAnSuccessMessage(string msg)
    {
        successField.SetActive(true);
        successText.text = msg;
    }

    public void ThrowALoadingScreen(bool state = false, string msg = "")
    {
        loadingField.SetActive(state);
        loadingText.text = msg;
    }

    public void SetupCallback()
    {
        confirmButton.onClick.AddListener(() => {
            callback?.Invoke();
            confirmField.SetActive(false);
        });
    }

    public void ThrowAConfirmScreen(string msg, Action func)
    {
        callback = func;
        confirmField.SetActive(true);
        confirmText.text = msg;

        cancelButton.onClick.AddListener(() => {
            confirmField.SetActive(false);
        });
    }

    public void ChooseNFT(){
        if (File.Exists(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SkinContract" + (dropdown.value + 1).ToString()) + ".png")){
            PlayerPrefs.SetString("SelectedSkin", PlayerPrefs.GetString("SkinContract" + (dropdown.value + 1).ToString()));
            ThrowAnSuccessMessage("Скин успешно выбран!");
        }
        else{
            ThrowAnError("Такого файла не существует.");
        }
    }
}
