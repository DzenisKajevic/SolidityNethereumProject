using System.Collections;
using System.Runtime.InteropServices;
using Nethereum.Web3;
using Nethereum.JsonRpc.UnityClient;
using UnityEngine;
using Nethereum.RPC.Eth.DTOs;
using TMPro;

public class EthereumManager : MonoBehaviour
{

    public string Url = "https://goerli.infura.io/v3/890a7f3aaf0d4b8eae37992a8e12a982";
    // privkey only needed if using without Metamask
    //public string PrivateKey = "d56fafc4fec3addbcf487a807d76be569942d16ef7e5bd3dc05196e8844e0626";
    public string AddressTo = "0xFD50635f9Ed75685EA6f2fE19E86fE74D6ce4E7f";

    public TextMeshPro mText;

    public delegate void OnTransactionProcessed(string txHash);
    public OnTransactionProcessed onTransactionDelegate;

    [DllImport("__Internal")]
    private static extern string GetAccount();

    [DllImport("__Internal")]
    private static extern void SendTransaction(string to, string data, string returnObj, string returnFunc);

    public void TransferRequest(float amount)
    {
        Logger("Sending " + amount);
        SendTransaction(AddressTo, amount.ToString(), gameObject.name, "TransactionCallback");
    }

    public string GetAddress()
    {
        return GetAccount();
    }

    // System.Action == callback in Unity
    public IEnumerator GetAccountBalance(string account, System.Action<float> callback)
    {
        var balanceRequest = new EthGetBalanceUnityRequest(Url);
        yield return balanceRequest.SendRequest(account, BlockParameter.CreateLatest());
        var ether = Web3.Convert.FromWei(balanceRequest.Result.Value);
        if (callback != null) callback(decimal.ToSingle(ether));

        Logger("Balance is " + ether);
    }

    public void TransactionCallback(string txHash)
    {
        Logger("Got Tx Hash from Unity: " + txHash);
        StartCoroutine(TransactionPolling(txHash));
    }

    private IEnumerator TransactionPolling(string txHash)
    {
        //create a poll to get the receipt when mined
        var transactionReceiptPolling = new TransactionReceiptPollingRequest(Url);
        //checking every 2 seconds for the receipt
        yield return transactionReceiptPolling.PollForReceipt(txHash, 2);

        Logger("Transaction mined");

        onTransactionDelegate?.Invoke(txHash);
    }

    private void Logger(string log)
    {
        mText.text = log;
        Debug.Log(log);
    }
}
