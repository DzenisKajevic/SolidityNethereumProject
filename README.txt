Working with multiple external repositories (inefficient to keep all progress on github), so I will use the notes below to track progress:

- Researching the process of calling JS functions from Unity with C# (.dll, .jslib) for use with WebGL.
- Using and editing the Unity, Solidity & Nethereum logic from 
 * FusedVR (Vasanth Mohan): https://github.com/FusedVR
 * Juan Blanco: https://github.com/juanfranblanco
 * Bethany Ouseke: https://github.com/bethanyuo


 Notes:
 - (might be useful) Unity Streaming (testing) with Blockchain samples: https://youtu.be/yRuqdHdDqFg
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


(18.12.) CHANGE OF PROJECT PLANS ...
- WebGL project version is broken [external .dll files (like Newtonsoft.Json.dll) 
which were imported throwing errors after swapping to newer Unity version)].

- Starting from scratch
- Using the private key (no metamask) version

- Initial plan was to allow the player to buy skins for the main character. 
- After trying for a while to implement that, the conclusion is that it does not look good with only skins (no animations)
- Saving animations externally is possible, but loading the sprite + animation from an external storage is either outside of my
knowledge or impossible.
- I'll probably save backgrounds and obstacle textures externally and keep track of them on the blockchain.
- Also, retrieving these items from IPFS is inefficient with non-premium features, so I will store them elsewhere.

- The only viable option for buying and importing textures using the blockchain is for static objects / backgrounds...
- Could be a good option for card games or something similar, where the characters aren't intended to move at all,
but definitely not for the game I'm going for.