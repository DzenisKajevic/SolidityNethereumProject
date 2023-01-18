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
    // Start is called before the first frame update

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
        //string url = "https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c";
        //string privateKey = "PRIVATE_KEY_HERE";
        //string fromAddress = "0x06C403f435d63835D027F517C2a231a663a1cF5E";
        //string contractAddress = "0x20ba8a2112eb5fb8c03a8febb810242d46a6bac4";
        Debug.Log("chainURL: " + loggedInPlayerSO._url);
        Debug.Log("chainID: " + loggedInPlayerSO.chainID);
        Debug.Log("priv: " + loggedInPlayerSO.PrivateKey);
        Debug.Log("pub: " + loggedInPlayerSO.PublicKey);
        Debug.Log("contract: " + loggedInPlayerSO.contractAddress);

        //Query request using our acccount and the contracts address (no parameters needed and default values)
        var queryRequest = new QueryUnityRequest<GetAllTokensFunction, GetAllTokensOutputDTO>(loggedInPlayerSO._url, loggedInPlayerSO.PublicKey);
        yield return queryRequest.Query(new GetAllTokensFunction() { FromAddress = loggedInPlayerSO.PublicKey, Account = loggedInPlayerSO.PublicKey }, loggedInPlayerSO.contractAddress);

        //Getting the dto response already decoded
        var skinList = queryRequest.Result;
        // returns number of skins boughts
        loggedInPlayerSO.SkinList = skinList.ReturnValue1;
        Debug.Log(skinList.ReturnValue1.Count);
        for (int i = 0; i < skinList.ReturnValue1.Count; i++)
        {
            Debug.Log("Skin " + i + ": " + skinList.ReturnValue1[i]);
        }
        /*
        foreach (var skinFTIndex in skinList.ReturnValue1)
        {
            Debug.Log(skinFTIndex);
            // code block to be executed
        }
        */

#if UNITY_EDITOR
        EditorSceneManager.LoadScene("Skin select");
#else
        SceneManager.LoadScene("Skin select");
#endif
    }
}