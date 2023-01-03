using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nethereum.Unity.Rpc;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using System;
using System.Data.SqlClient;
using Nethereum.HdWallet;

public class LoginWithPrivateKey : MonoBehaviour
{
    public const string PrivateKey =
       "d56fafc4fec3addbcf487a807d76be569942d16ef7e5bd3dc05196e8844e0626";

    public InputField InputPrivateKey;
    public InputField ResultAccountAddress;
    //public InputField ResultPrivateKey;
    //public InputField InputWords;

    // Use this for initialization
    void Start()
    {
        InputPrivateKey.text = PrivateKey;
    }

    public void LoginWithPrivKey()
    {
        var account = new Nethereum.Web3.Accounts.Account(InputPrivateKey.text, 5);
        ResultAccountAddress.text = account.Address;
        //ResultPrivateKey.text = account.PrivateKey;
        Debug.Log(account.Address);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
