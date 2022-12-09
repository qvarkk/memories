using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnEnable : MonoBehaviour
{
    [SerializeField] AddNFTPrompt script;

    private void OnEnable() {
        script.ReloadNFTs();
    }

    private void OnDisable() {
        foreach (Transform child in script.imageList.transform)
        {
             Destroy(child.gameObject);
        }
        script.contractList.text = "Контракты";
    }
}
