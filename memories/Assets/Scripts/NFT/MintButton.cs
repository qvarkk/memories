using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTPort;
using TMPro;
using System.IO;

public class MintButton : MonoBehaviour
{
    [SerializeField] public Minted_model minter = new Minted_model();
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_InputField descField;
    [SerializeField] AddNFTPrompt script;

    public void MintPicture()
    {
        string fileName = "/playerTexture" + (dropdown.value + 1).ToString() + ".png";
        
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            Mint_File
                .Initialize()
                .OnComplete(Minted => {
                    minter = Minted;
                    Debug.Log(minter);
                    script.ThrowALoadingScreen();
                    script.ThrowAnSuccessMessage("NFT было успешно создано. Оно появится в вашем профиле в течение 2 минут.");
                })
                .OnStarted(started => script.ThrowALoadingScreen(true, "Загружаем NFT на маркетплейс..."))
                .OnError(error => script.ThrowAnError("Произошла ошибка на стороне сервера. " + error))
                .SetChain(Mint_File.Chains.goerli)
                .SetParameters(
                    FilePath: Application.persistentDataPath + fileName,
                    Name: nameField.text != "" ? nameField.text : fileName,
                    Description: descField.text,
                    MintToAddress: PlayerPrefs.GetString("Account")
                )
                .Run();
        }
        else
        {
            script.ThrowAnError("Файл с рисунком не был найден. Возможно вы выбрали слишком большой номер рисунка.");
        }
    }
}
