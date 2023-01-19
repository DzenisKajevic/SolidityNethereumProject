using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#else
using UnityEngine.SceneManagement;
#endif
using Nethereum.Unity.Rpc;
using NethereumProject.Contracts.Leaderboard.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using System;
using Nethereum.Util;

public class LeaderboardScript : MonoBehaviour
{
    public PlayerSO loggedInPlayerSO;
    [SerializeField]
    private GameController gameControllerScript;

    public void submitScoreButtonOnClick()
    {
        // call contract and buy the skin
        StartCoroutine(submitScore());
        Debug.Log("Submitting score");

    }

    public IEnumerator loadLeaderboard()
    {
        var queryRequest = new QueryUnityRequest<GetLeaderboardFunction, GetLeaderboardOutputDTO>(loggedInPlayerSO._url, loggedInPlayerSO.PublicKey);
        yield return queryRequest.Query(new GetLeaderboardFunction() { FromAddress = loggedInPlayerSO.PublicKey }, loggedInPlayerSO.leaderboardContractAddress);

        // Getting the dto response already decoded
        var leaderboardFunctionResult = queryRequest.Result;
        // returns number of skins boughts
        loggedInPlayerSO._leaderboardAddressList = leaderboardFunctionResult.ReturnValue1;
        loggedInPlayerSO._leaderboardScoreList = leaderboardFunctionResult.ReturnValue2;

        Debug.Log(loggedInPlayerSO._leaderboardAddressList.Count);
        Debug.Log(loggedInPlayerSO._leaderboardScoreList.Count);

        for (int i = 0; i < loggedInPlayerSO._leaderboardAddressList.Count; i++)
        {
            Debug.Log("Address " + i + ": " + loggedInPlayerSO._leaderboardAddressList[i]);
            Debug.Log("Address " + i + ": " + loggedInPlayerSO._leaderboardScoreList[i]);
        }
    }

    public IEnumerator submitScore()
    {
        gameControllerScript.enableInterface(false);
        var transactionTransferRequest = new TransactionSignedUnityRequest(loggedInPlayerSO._url, loggedInPlayerSO.leaderboardAdminPrivateKey, loggedInPlayerSO.chainID);

        transactionTransferRequest.UseLegacyAsDefault = true;

        var transactionMessage = new AddScoreFunction
        {
            FromAddress = loggedInPlayerSO.leaderboardAdminPublicKey,
            ScoreHolderAddress = loggedInPlayerSO.PublicKey,
            Score = TrackScoreGUISO.score
        };
        //transactionMessage.AmountToSend = 10000000000000000;
        Debug.Log(transactionMessage);
        yield return transactionTransferRequest.SignAndSendTransaction<AddScoreFunction>(transactionMessage, loggedInPlayerSO.leaderboardContractAddress);

        var transactionTransferHash = transactionTransferRequest.Result;

        if (transactionTransferRequest.Exception == null)
        {
            Debug.Log("Transfer txn hash:" + transactionTransferHash);

            var transactionReceiptPolling = new TransactionReceiptPollingRequest(loggedInPlayerSO._url);
            yield return transactionReceiptPolling.PollForReceipt(transactionTransferHash, 2);
            var transferReceipt = transactionReceiptPolling.Result;

            Debug.Log(transferReceipt.Logs);

            // if the score upload was successful, query the contract for updated information
            StartCoroutine(loadLeaderboard());

            gameControllerScript.enableInterface(true);
        }
        else
        {
            Debug.Log("RW: Error submitted tx: " + transactionTransferRequest.Exception.Message);
            gameControllerScript.enableInterface(true);
        }

    }


}
