using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTPort;
using TMPro;

public class MintButton : MonoBehaviour
{
    [SerializeField] public Minted_model minter = new Minted_model();
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_InputField descField;

    public void MintPicture()
    {
        string fileName = "playerTexture" + (dropdown.value + 1).ToString() + ".png";
        
        Mint_File
            .Initialize()
            .OnComplete(Minted => minter = Minted)
            .OnStarted(started => Debug.Log(started))
            .OnProgress(percent => Debug.Log(percent.ToString()))
            .OnError(error => Debug.Log(error))
            .SetChain(Mint_File.Chains.goerli)
            .SetParameters(
                FilePath: Application.persistentDataPath + fileName,
                Name: nameField.text != "" ? nameField.text : fileName,
                Description: descField.text,
                MintToAddress: PlayerPrefs.GetString("Account")
            )
            .Run();
    }
}
