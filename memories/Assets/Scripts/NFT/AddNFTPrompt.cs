using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Numerics;
using UnityEngine.Networking;
using TMPro;

public class AddNFTPrompt : MonoBehaviour
{
    string chain = "ethereum";
    string network = "goerli";
    string contract;
    string account;
    string tokenId;
    bool alreadyHasThis;

    [SerializeField] TMP_InputField contractInput;
    [SerializeField] TMP_InputField tokenIdInput;
    [SerializeField] GameObject errorField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Dropdown standard;

    public class Response {
        public string image;
    }

    async public void StartValidation()
    {
        contract = contractInput.text;
        account = PlayerPrefs.GetString("Account");
        tokenId = tokenIdInput.text;

        BigInteger balanceOf1155 = 0;
        int balanceOf721 = 0;

        if(standard.value == 1)
            balanceOf1155 = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        else if(standard.value == 0)
            balanceOf721 = await ERC721.BalanceOf(chain, network, contract, account, tokenId);
        

        if (balanceOf1155 > 0 || balanceOf721 > 0)
        {
            for (int i = 1; i < PlayerPrefs.GetInt("SkinsQuantity") + 1; i++)
            {
                if (PlayerPrefs.GetString("SkinContract" + i.ToString()) == contract)
                {
                    alreadyHasThis = true;
                }
            }

            if (!alreadyHasThis)
            {
                PlayerPrefs.SetInt("SkinsQuantity", PlayerPrefs.GetInt("SkinsQuantity") + 1);
                PlayerPrefs.SetString("SkinContract" + PlayerPrefs.GetInt("SkinsQuantity").ToString(), contract);

                LoadAndSaveNFTTexture();
            }
            else
            {
                ThrowAnError("У вас уже есть NFT с таким контрактом");
            }
        }
    }

    async private void LoadAndSaveNFTTexture()
    {
        // fetch uri
        string uri = "";
        if(standard.value == 1)
            uri = await ERC1155.URI(chain, network, contract, tokenId);
        else if(standard.value == 0)
            uri = await ERC1155.URI(chain, network, contract, tokenId);


        print("uri: " + uri);
        

        // fetch json from uri
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        await webRequest.SendWebRequest();
        Response data = JsonUtility.FromJson<Response>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));

        // parse json to get image uri
        string imageUri = data.image;
        print("imageUri: " + imageUri);
        if (imageUri.StartsWith("ipfs://"))
        {
            imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
        }
        Debug.Log("Revised URI: " + imageUri);

        // fetch image and display in game
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri);
        try 
        {
            await textureRequest.SendWebRequest();
            byte[] textureBytes = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + contract, textureBytes);
            Debug.Log("File written to disk!");
        }
        catch (Exception ex)
        {
            ThrowAnError("Произошла ошибка (Код ошибки: " + ex.ToString() + ")");
        }
    }

    private void ThrowAnError(string error)
    {
        errorField.SetActive(true);
        errorText.text = error;
    }
}
