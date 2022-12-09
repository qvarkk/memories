using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Numerics;
using UnityEngine.Networking;
using TMPro;
using NFTPort;

public class AddNFTPrompt : MonoBehaviour
{
    bool isImageSaved;
    
    [SerializeField] GameObject errorField;
    [SerializeField] TMP_Text errorText;

    NFTs_model NFTsOfUser;

    public class Response {
        public string image;
    }

    private void StartAccountCheck() {
        

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
        }
    }

    // async public void StartValidation()
    // {
    //     contract = contractInput.text;
    //     account = PlayerPrefs.GetString("Account");
    //     tokenId = tokenIdInput.text;

    //     BigInteger balanceOf1155 = 0;
    //     int balanceOf721 = 0;

    //     if(standard.value == 1)
    //         balanceOf1155 = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
    //     else if(standard.value == 0)
    //         balanceOf721 = await ERC721.BalanceOf(chain, network, contract, account, tokenId);
        

    //     if (balanceOf1155 > 0 || balanceOf721 > 0)
    //     {
    //         for (int i = 1; i < PlayerPrefs.GetInt("SkinsQuantity") + 1; i++)
    //         {
    //             if (PlayerPrefs.GetString("SkinContract" + i.ToString()) == contract + tokenId)
    //             {
    //                 alreadyHasThis = true;
    //             }
    //         }

    //         if (!alreadyHasThis)
    //         {
    //             LoadAndSaveNFTTexture();

    //             int i = 0;
    //             while (!isImageSaved && i < 500)
    //             {
    //                 i++;
    //             }

    //             if (isImageSaved)
    //             {
    //                 PlayerPrefs.SetInt("SkinsQuantity", PlayerPrefs.GetInt("SkinsQuantity") + 1);
    //                 PlayerPrefs.SetString("SkinContract" + PlayerPrefs.GetInt("SkinsQuantity").ToString(), contract + tokenId);
    //                 ThrowAnError("Сохранено!");
    //             }
    //         }
    //         else
    //         {
    //             ThrowAnError("У вас уже есть этот NFT");
    //             return;
    //         }             
    //     }
    // }

    // async private void LoadAndSaveNFTTexture()
    // {
    //     // fetch uri
    //     isImageSaved = false;
    //     string uri = "";
    //     if(standard.value == 1)
            // uri = await ERC1155.URI(chain, network, contract, tokenId);
    //     else if(standard.value == 0)
    //         uri = await ERC1155.URI(chain, network, contract, tokenId);

    //     print("uri: " + uri);

    //     // fetch json from uri
    //     UnityWebRequest webRequest = UnityWebRequest.Get(uri);
    //     await webRequest.SendWebRequest();
    //     Response data = JsonUtility.FromJson<Response>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));

    //     // parse json to get image uri
    //     string imageUri = data.image;
    //     print("imageUri: " + imageUri);
    //     if (imageUri.StartsWith("ipfs://"))
    //     {
    //         imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
    //     }
    //     Debug.Log("Revised URI: " + imageUri);
    //     UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri);
    //     await textureRequest.SendWebRequest();

    //     byte[] textureBytes = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture.EncodeToPNG();
    //     File.WriteAllBytes(Application.persistentDataPath +"/" + contract + tokenId + ".png", textureBytes);
    //     isImageSaved = true;
    //     Debug.Log("File written to disk!");
    // }

    private void ThrowAnError(string error)
    {
        errorField.SetActive(true);
        errorText.text = error;
    }
}
