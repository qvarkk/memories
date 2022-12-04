using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using UnityEngine.Networking;

public class CheckNFTOnAccount : MonoBehaviour
{
    [SerializeField] GameObject cubeSprite;

    public class Response {
        public string image;
    }

    async void Start()
    {
        string chain = "ethereum";
        string network = "goerli";
        string contract = "0x2c1867bc3026178a47a677513746dcc6822a137a";
        string account = PlayerPrefs.GetString("Account");
        string tokenId = "0x01559ae4021a5e67af58c6b612a6842b598c09dcbe0342deb7a49ceaf2709902";

        BigInteger quantityOfNFT = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        
        if (quantityOfNFT == 0)
        {
            // fetch uri
            string uri = await ERC1155.URI(chain, network, contract, tokenId);
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
            await textureRequest.SendWebRequest();
            cubeSprite.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
        }
    }
}
