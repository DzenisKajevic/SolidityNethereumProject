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

public class LoginWithPrivateKey : MonoBehaviour
{
    public const string PrivateKey =
       "d56fafc4fec3addbcf487a807d76be569942d16ef7e5bd3dc05196e8844e0626";
    [SerializeField]
    private PlayerSO loggedInPlayerSO;
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
    public void ContinueLoginPrivateKey()
    {
        // no need to check the public key, since both of them are filled in at the same time
        if (ResultAccountAddress.text.Length != 0)
        {
            loggedInPlayerSO.PrivateKey = InputPrivateKey.text;
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
