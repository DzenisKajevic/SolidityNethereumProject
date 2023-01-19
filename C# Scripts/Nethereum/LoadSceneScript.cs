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

public class LoadSceneScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text leaderboardText;
    [SerializeField]
    private LeaderboardScript leaderboardScript;
    [SerializeField]
    private LoadSkinScript loadSkinScript;
    [SerializeField]
    private PlayerSO loggedInPlayerSO;

    // Start is called before the first frame update
    void Start()
    {
        //161.8 48.5 -228.3
        // 0, 0, -25
    }

    public IEnumerator loadScene()
    {
        yield return StartCoroutine(leaderboardScript.loadLeaderboard());

        // fill up the actual text box for the leaderboard
        leaderboardText.text = "";
        for (int i = 0; i < loggedInPlayerSO._leaderboardAddressList.Count; i++)
        {
            leaderboardText.text += loggedInPlayerSO._leaderboardScoreList[i] + ": " + loggedInPlayerSO._leaderboardAddressList[i] + "\n";
            Debug.Log("Address " + i + ": " + loggedInPlayerSO._leaderboardAddressList[i]);
            Debug.Log("Address " + i + ": " + loggedInPlayerSO._leaderboardScoreList[i]);
        }
        yield return StartCoroutine(loadSkinScript.loadSkin());
    }


}
