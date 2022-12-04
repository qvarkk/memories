using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

public class ERC1155BalanceOfExample : MonoBehaviour
{
    async void Start()
    {
        string chain = "ethereum";
        string network = "goerli";
        string contract = "0xf4910C763eD4e47A585E2D34baA9A4b611aE448C";
        string account = PlayerPrefs.GetString("Account");
        string tokenId = "1884019054070662296616151707165051851692388519576287873722218841625287720961";

        BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        
    }
}
