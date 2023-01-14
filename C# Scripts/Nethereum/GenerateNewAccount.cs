using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nethereum.Unity.Rpc;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using System;
using System.Data.SqlClient;
using Nethereum.HdWallet;
using UnityEditor.SceneManagement;

public class GenerateNewAccount : MonoBehaviour
{
    [SerializeField]
    private PlayerSO loggedInPlayerSO;
    [SerializeField]
    private FetchBoughtSkins fetchBoughtSkinsScript;
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
    public void ContinueRegister()
    {
        // no need to check the public key, since both of them are filled in at the same time
        if (ResultAccountAddress.text.Length != 0)
        {
            loggedInPlayerSO.PrivateKey = ResultPrivateKey.text;
            loggedInPlayerSO.PublicKey = ResultAccountAddress.text;

            // won't be able to fetch skins in time. The scene will instantly change since coroutines aren't blocking
            //fetchBoughtSkinsScript.coroutinefetchBoughtSkins();

            EditorSceneManager.LoadScene("Flappy");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
