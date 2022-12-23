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

(23.12.) 
- It might actually still be possible to do what I initially wanted.
- If I can figure out how to save whole Player presets with animations as an AssetBundle and then mint that AssetBundle 
as an ERC-1155 token, I could be able to completely swap out a playable character upon selection.

- How to go about doing it?
- How to create an AssetBundle?
- How to mint one?
- Do I need to use the Animator programmatically to change the animations which are bound to each state?


Example of an ERC-1155 smart contract for minting Bundles (similar to the previous one; 
I'll probably continue using the first one):
pragma solidity ^0.5.0;

import "https://github.com/OpenZeppelin/openzeppelin-contracts/blob/master/contracts/token/ERC1155/ERC1155.sol";

contract AssetBundleToken is ERC1155 {

  constructor(string memory _name, string memory _symbol, uint256 _totalSupply) public {
    _name = "Asset Bundle Token";
    _symbol = "ABT";
    _totalSupply = 1;
  }

  function mintAssetBundle(string memory _assetBundleName, uint256 _amount) public returns (uint256) {
    return _mint(_assetBundleName, _amount);
  }
}

Theoretical steps on how to import an AssetBundle with a playable character inside of it, then replace the already-active
playable character with the new one:

1. Load the AssetBundle using the AssetBundle API.
2. Instantiate the new playable character prefab from the AssetBundle.
3. Use the Animation component to load the new character's animation clips.
4. Use the Animator component to define transitions between the new character's animation states.
5. Set the new character as the active character in the game.
6. Use the Play() and CrossFade() methods of the Animation component to start playing the new character's animations.

