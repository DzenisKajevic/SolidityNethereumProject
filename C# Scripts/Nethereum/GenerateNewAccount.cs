using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nethereum.Unity.Rpc;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using System;
using System.Data.SqlClient;
using Nethereum.HdWallet;

public class GenerateNewAccount : MonoBehaviour
{
    public const string Words =
       "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";


    public InputField ResultAccountAddress;
    public InputField ResultPrivateKey;
    //public InputField InputWords;

    // Use this for initialization
    void Start()
    {
        //InputWords.text = Words;
    }

    public void GetNewAccount()
    {
        var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
        var privateKeyBytes = ecKey.GetPrivateKeyAsBytes();
        var privateKey = BitConverter.ToString(privateKeyBytes).Replace("-", string.Empty);
        var account = new Nethereum.Web3.Accounts.Account(privateKey, 5);


        //var wallet = new Wallet(InputWords.text, null);
        //var account = wallet.GetAccount(0);
        ResultAccountAddress.text = account.Address;
        ResultPrivateKey.text = account.PrivateKey;
        Debug.Log(account.Address);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
