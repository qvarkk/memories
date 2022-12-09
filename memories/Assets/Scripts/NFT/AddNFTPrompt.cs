using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;
using TMPro;
using NFTPort;
using UnityEngine.UI;



public class AddNFTPrompt : MonoBehaviour
{
    Image imageRenderer;

    [SerializeField] TMP_Text contractList;
    [SerializeField] TMP_Text imageList;
    [SerializeField] GameObject image;
    [SerializeField] GameObject[] coordinatesSemple;

    GameObject[] imagesArray;
    GameObject imageToChange;
    
    [SerializeField] GameObject errorField;
    [SerializeField] TMP_Text errorText;

    NFTs_model NFTsOfUser;

    public class Response {
        public string image;
    }

    public void StartAccountCheck() {
        

        NFTs_OwnedByAnAccount
            .Initialize()
            .SetChain(NFTs_OwnedByAnAccount.Chains.goerli)
            .SetAddress(PlayerPrefs.GetString("Account"))
            .SetInclude(NFTs_OwnedByAnAccount.Includes.metadata)
            .OnError(error=>Debug.Log(error))
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

        if (amount > 0)
        {
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
                    Debug.Log("File written to disk!");
                }
                else
                {
                    wereAnyErrors = true;
                }

                if (wereAnyErrors)
                {
                    ThrowAnError("У некоторых/всех NFT на вашем аккаунте не оказалось метадаты");
                }
                else
                {
                    ThrowAnError("Все NFT были успешно загружены");
                }
            }
            ReloadNFTs();
        }
    }
    public void ReloadNFTs()
    {
        
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
        
    }

    private void ThrowAnError(string error)
    {
        errorField.SetActive(true);
        errorText.text = error;
    }

}