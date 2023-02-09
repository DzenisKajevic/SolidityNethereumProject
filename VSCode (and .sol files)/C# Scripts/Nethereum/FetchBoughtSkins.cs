using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.Extensions;
using Nethereum.HdWallet;
using Nethereum.Unity.Rpc;
using UnityEngine;
using UnityEngine.Assertions;
// using contract definition
using NethereumProject.Contracts.AssetBundleTokens.ContractDefinition;
using Nethereum.Util;
using Nethereum.RPC.Eth.DTOs;
using System;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#else
using UnityEngine.SceneManagement;
#endif

public class FetchBoughtSkins : MonoBehaviour
{
    [SerializeField]
    private PlayerSO loggedInPlayerSO;
    public int[] boughtSkins;

    public void Start()
    {
        coroutinefetchBoughtSkins();
    }
    public void coroutinefetchBoughtSkins()
    {
        StartCoroutine(fetchBoughtSkins());
    }
    public IEnumerator fetchBoughtSkins()
    {
        Debug.Log("chainURL: " + loggedInPlayerSO._url);
        Debug.Log("chainID: " + loggedInPlayerSO.chainID);
        Debug.Log("priv: " + loggedInPlayerSO.PrivateKey);
        Debug.Log("pub: " + loggedInPlayerSO.PublicKey);
        Debug.Log("contract: " + loggedInPlayerSO.contractAddress);

        var queryRequest = new QueryUnityRequest<GetAllTokensFunction, GetAllTokensOutputDTO>(loggedInPlayerSO._url, loggedInPlayerSO.PublicKey);
        yield return queryRequest.Query(new GetAllTokensFunction() { FromAddress = loggedInPlayerSO.PublicKey, Account = loggedInPlayerSO.PublicKey }, loggedInPlayerSO.contractAddress);

        // Getting the dto response already decoded
        var skinList = queryRequest.Result;
        // returns number of skins boughts
        loggedInPlayerSO.SkinList = skinList.ReturnValue1;
        Debug.Log(skinList.ReturnValue1.Count);
        for (int i = 0; i < skinList.ReturnValue1.Count; i++)
        {
            Debug.Log("Skin " + i + ": " + skinList.ReturnValue1[i]);
        }

        var balanceRequest = new EthGetBalanceUnityRequest(loggedInPlayerSO._url);
        yield return balanceRequest.SendRequest(loggedInPlayerSO.PublicKey, BlockParameter.CreateLatest());

        loggedInPlayerSO.balance = Math.Round(UnitConversion.Convert.FromWei(balanceRequest.Result.Value), 7);

#if UNITY_EDITOR
        EditorSceneManager.LoadScene("Skin select");
#else
        SceneManager.LoadScene("Skin select");
#endif
    }
}