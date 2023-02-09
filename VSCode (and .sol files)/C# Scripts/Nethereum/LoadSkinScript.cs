using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nethereum.Unity.Rpc;
using NethereumProject.Contracts.AssetBundleTokens.ContractDefinition;
using UnityEngine.Networking;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#else
using UnityEngine.SceneManagement;
#endif

public class LoadSkinScript : MonoBehaviour
{
    [SerializeField]
    private PlayerSO loggedInPlayerSO;
    [SerializeField]
    private SkinDatabaseSO skinDatabaseSO;
    [SerializeField]
    private GameController gameControllerScript;
    [SerializeField]
    private BoolSO restarted;
    public IEnumerator loadSkin()
    {
        Debug.Log("chainURL: " + loggedInPlayerSO._url);
        Debug.Log("chainID: " + loggedInPlayerSO.chainID);
        Debug.Log("priv: " + loggedInPlayerSO.PrivateKey);
        Debug.Log("pub: " + loggedInPlayerSO.PublicKey);
        Debug.Log("contract: " + loggedInPlayerSO.contractAddress);

        // If using free skin, load directly from local files
        if (loggedInPlayerSO.skinIndex == 0)
        {
            Debug.Log("SkinIndex == 0");
            Instantiate(Resources.Load("Birds/" + skinDatabaseSO.skinList[loggedInPlayerSO.skinIndex], typeof(GameObject)));
            gameControllerScript.camera.transform.position = new Vector3(0, 0, -25);
            if (restarted.Value)
            {
                gameControllerScript.StartGame();
                restarted.Value = false;
            }
        }
        // Else fetch skin url...
        else
        {
            Debug.Log("SkinIndex != 0");
            //Query request using our acccount and the contracts address (no parameters needed and default values)
            var queryRequest = new QueryUnityRequest<UriFunction, UriOutputDTO>(loggedInPlayerSO._url, loggedInPlayerSO.PublicKey);
            yield return queryRequest.Query(new UriFunction() { FromAddress = loggedInPlayerSO.PublicKey, Index = loggedInPlayerSO.skinIndex }, loggedInPlayerSO.contractAddress);

            //Getting the dto response already decoded
            var result = queryRequest.Result;
            // returns number of skins boughts
            loggedInPlayerSO.skinURL = result.ReturnValue1;
            Debug.Log(loggedInPlayerSO.skinURL);

            // ... Finally, load the skin from the url
            yield return StartCoroutine(InstantiateSkin(loggedInPlayerSO.skinURL, skinDatabaseSO.skinList[loggedInPlayerSO.skinIndex]));
        }
    }

    IEnumerator InstantiateSkin(string bundleUrl, string assetName)
    {
        Debug.Log(bundleUrl + assetName);
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
#if UNITY_EDITOR
            EditorSceneManager.LoadScene("Main Menu");
#else
            SceneManager.LoadScene("Main Menu");
#endif
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            /*string[] assetNames = bundle.GetAllAssetNames();
            foreach (string assetNameTest in assetNames)
            {
                Debug.Log(assetNameTest);
            }
            Debug.Log(bundle.GetAllAssetNames());*/
            Instantiate(bundle.LoadAsset(assetName));
            bundle.Unload(false);
            // "Loading" camera location: 161.8, 48.5, -47
            gameControllerScript.camera.transform.position = new Vector3(0, 0, -25);
            if (restarted.Value)
            {
                gameControllerScript.StartGame();
                restarted.Value = false;
            }
        }
    }
}
