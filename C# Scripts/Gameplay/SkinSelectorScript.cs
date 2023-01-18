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
using NethereumProject.Contracts.AssetBundleTokens.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using System;
using Nethereum.Util;

public class SkinSelectorScript : MonoBehaviour
{

    public PlayerSO loggedInPlayerSO;
    public SkinDatabaseSO skinDatabase;
    public int selectedOption = 0;
    public GameObject currentSkin;
    [SerializeField]
    private Button mainButton;
    [SerializeField]
    private Button previousButton;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private TMP_Text mainButtonText;
    private string buyOrContinue = "Continue";
    [SerializeField]
    private TMP_Text publicKeyText;
    [SerializeField]
    private TMP_Text balanceText;

    void Start()
    {
        publicKeyText.text = loggedInPlayerSO.PublicKey;
        balanceText.text = "Balance: " + loggedInPlayerSO.balance + " ETH";
    }

    public void returnToMainMenu()
    {
        loggedInPlayerSO.resetValues();
#if UNITY_EDITOR
        EditorSceneManager.LoadScene("Main Menu");
#else
        SceneManager.LoadScene("Main Menu");
#endif
    }


    public void mainButtonOnClick()
    {
        if (buyOrContinue == "Continue")
        {
            loggedInPlayerSO.skinIndex = selectedOption;
#if UNITY_EDITOR
            EditorSceneManager.LoadScene("Flappy");
#else
            SceneManager.LoadScene("Flappy");
#endif
        }
        else
        {
            // call contract and buy the skin
            StartCoroutine(purchaseSkin());
            Debug.Log("Purchasing skin");
        }
    }

    public IEnumerator purchaseSkin()
    {
        enableInterface(false);
        var transactionTransferRequest = new TransactionSignedUnityRequest(loggedInPlayerSO._url, loggedInPlayerSO.PrivateKey, loggedInPlayerSO.chainID);

        transactionTransferRequest.UseLegacyAsDefault = true;

        var transactionMessage = new PurchaseAssetBundleIDFunction
        {
            FromAddress = loggedInPlayerSO.PublicKey,
            Index = selectedOption
        };
        transactionMessage.AmountToSend = 10000000000000000;
        Debug.Log(transactionMessage);
        yield return transactionTransferRequest.SignAndSendTransaction<PurchaseAssetBundleIDFunction>(transactionMessage, loggedInPlayerSO.contractAddress);

        var transactionTransferHash = transactionTransferRequest.Result;

        if (transactionTransferRequest.Exception == null)
        {
            Debug.Log("Transfer txn hash:" + transactionTransferHash);

            var transactionReceiptPolling = new TransactionReceiptPollingRequest(loggedInPlayerSO._url);
            yield return transactionReceiptPolling.PollForReceipt(transactionTransferHash, 2);
            var transferReceipt = transactionReceiptPolling.Result;

            Debug.Log(transferReceipt.Logs);

            // if the purchase was successful, add the skin to the bought skin list and change the button to enable to player to continue
            loggedInPlayerSO.SkinList.Add(selectedOption);
            loggedInPlayerSO.SkinList.Sort();
            var balanceRequest = new EthGetBalanceUnityRequest(loggedInPlayerSO._url);
            yield return balanceRequest.SendRequest(loggedInPlayerSO.PublicKey, BlockParameter.CreateLatest());

            loggedInPlayerSO.balance = Math.Round(UnitConversion.Convert.FromWei(balanceRequest.Result.Value), 7);
            balanceText.text = "Balance: " + loggedInPlayerSO.balance + " ETH";
            changeButtonColour();
            enableInterface(true);
        }
        else
        {
            Debug.Log("RW: Error submitted tx: " + transactionTransferRequest.Exception.Message);
            enableInterface(true);
        }

    }

    public void enableInterface(bool state)
    {
        mainButton.interactable = state;
        previousButton.interactable = state;
        nextButton.interactable = state;
    }

    public void changeButtonColour()
    {
        // if the skin is the free one (blue) or the user has bought the selected skin, allow them to continue
        if (selectedOption == 0 || loggedInPlayerSO.SkinList.Contains(selectedOption))
        {
            mainButton.GetComponent<Image>().color = new Color(152, 255, 0);
            mainButtonText.text = "Continue";
            buyOrContinue = "Continue";
        }
        else
        {
            mainButton.GetComponent<Image>().color = Color.white;
            mainButtonText.text = "Purchase (0.01 ETH)";
            buyOrContinue = "Buy Skin";
        }
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= skinDatabase.SkinCount)
        {
            selectedOption = 0;
        }

        Debug.Log(selectedOption);
        changeButtonColour();
        SwapCharacter(skinDatabase.skinList[selectedOption]);
    }

    public void PreviousOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = skinDatabase.SkinCount - 1;
        }

        Debug.Log(selectedOption);
        changeButtonColour();
        SwapCharacter(skinDatabase.skinList[selectedOption]);
    }

    public void SwapCharacter(string selectedOption)
    {
        Debug.Log(selectedOption + " Idle Variant");
        // save the location of the old skin
        Transform position = currentSkin.transform;

        // remove the current skin
        Destroy(currentSkin, 0);
        // instantiate the new skin
        currentSkin = Instantiate(Resources.Load("Birds/" + selectedOption + " Idle Variant", typeof(GameObject))) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
