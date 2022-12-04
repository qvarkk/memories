using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

public class ERC1155BalanceOfBatchExample : MonoBehaviour
{
    async void Start()
    {
        string chain = "ethereum";
        string network = "goerli";
        string contract = "0xf4910C763eD4e47A585E2D34baA9A4b611aE448C";
        string[] accounts = { "0x042a5126A23Af2cA153b19B88d54c59263953778", "0xaCA9B6D9B1636D99156bB12825c75De1E5a58870" };
        string[] tokenIds = { "17", "22" };

        List<BigInteger> batchBalances = await ERC1155.BalanceOfBatch(chain, network, contract, accounts, tokenIds);
        foreach (var balance in batchBalances)
        {
            print ("BalanceOfBatch: " + balance);
        } 
    }
}
