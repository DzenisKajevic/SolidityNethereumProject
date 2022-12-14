Working with multiple external repositories (inefficient to keep all progress on github), so I will use the notes below to track progress:

- Researching the process of calling JS functions from Unity with C# (.dll, .jslib) for use with WebGL.
- Using and editing the Unity, Solidity & Nethereum logic from 
 * FusedVR: https://github.com/FusedVR
 * Juan Blanco: https://github.com/juanfranblanco
 * Bethany Ouseke: https://github.com/bethanyuo


 Notes:
 - To make read-only requests, use QueryUnityRequest:
  var queryRequest = new QueryUnityRequest<FunctionName, FunctionOutputDTO>(parameter1, parameter2, ...);
  yield return queryRequest.Query(new FunctionName(){ Parameter1 = parameter1 }, contractAddress);
  var dtoResult = queryRequet.Result;

- To make write requests, use TransactionSignedUnityRequest:
  var transactionTransferRequest = new TransactionSignedUnityRequest(url, privateKey);
  // signed requests won't work without UseLegacyAsDefault
  transactionTransferRequest.UseLegacyAsDefault = true;
  var transactionMessage = new FunctionName { };

  yield return transactionTransferRequest.SignAndSendTransaction(transactionMessage, contractAddress);
  string transactionTransferHash = transactionTransferRequest.Result;

  TransactionReceiptPollingRequest transactionReceiptPolling = new TransactionReceiptPollingRequest(url);
  yield return transactionReceiptPolling.PollForReceipt(transactionTransferHash, 2);
  var transactionReceipt = transactionReceiptPolling.Result;

  Debug.log("Finished writing");

- To reload scenes:
 SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
 // similar to change scenes