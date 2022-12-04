using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountNumberInMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI accountText;

    void Start()
    {
        if (PlayerPrefs.GetInt("NFTMode") == 1)
            accountText.text = PlayerPrefs.GetString("Account");
        else
            accountText.text = "No NFT mode";
    }
}
