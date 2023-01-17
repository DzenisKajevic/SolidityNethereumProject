using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nethereum.Unity.Rpc;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using System;
using System.Data.SqlClient;
using Nethereum.HdWallet;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#else
using UnityEngine.SceneManagement;
#endif

public class RegisterOrLoginWithHDWallet : MonoBehaviour
{
    public const string Words =
       "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
    [SerializeField]
    private PlayerSO loggedInPlayerSO;

    public InputField ResultAccountAddress;
    public InputField InputWords;
    public InputField ResultPrivateKey;

    // Use this for initialization
    void Start()
    {
        InputWords.text = Words;
    }

    public void GetHdWalletAccounts()
    {
        var wallet = new Wallet(InputWords.text, null);
        var account = wallet.GetAccount(0);
        ResultAccountAddress.text = account.Address;
        ResultPrivateKey.text = account.PrivateKey;
        Debug.Log(account.Address);
    }
    public void ContinueMnemonic()
    {
        // no need to check the public key, since both of them are filled in at the same time
        if (ResultPrivateKey.text.Length != 0)
        {
            loggedInPlayerSO.PrivateKey = ResultPrivateKey.text;
            loggedInPlayerSO.PublicKey = ResultAccountAddress.text;
#if UNITY_EDITOR
            EditorSceneManager.LoadScene("LoadBoughtSkins");
#else
            SceneManager.LoadScene("LoadBoughtSkins");
#endif
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
