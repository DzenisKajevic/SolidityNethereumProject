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

_______________

Added Asset Bundle Browser Unity extension.

Managed to somehow enable loading of characters with their animations from a server (localhost) using UnityWebRequest.

- Finished (26.12; 18:46)
 + Added a new gameplay scene, added obstacles, backgrounds, custom 2D colliders
 + Added another prefab set for the GameObjects that will be used in the skin selection scene (no movement, different idle animation)

- Finished (26.12; 22:26)
 + Added a spawner Object + Script for spawning obstacles
 + Added horizontal (obstacle) and vertical (player) movement
 + (Accidentally :)) created a local-coop game, where each player controls a different bird
// local coop - player collision disabled in Edit -> Project Settings -> Physics 2D
  * I COOOOOULD *MAYBE* ??? Create a coop NFT game where each of the 3 players would have to sign in, and choose a skin from their
  own wallet. MAYBE. Probably not.
_____________________

- Finished (28.12; 00:20)
 + Added a GameController script & object + PlayerStatusScript (for keeping track of player details like: player.isDead)
 + Added a UI for the game over scene
 + Added a ScriptableObject for transferring data between scenes
 (currently used for restarting scenes, but can be used later for transferring NFT bundle location)
_____________________

- Finished (3.1.2023; 15:15)
 + Added a UI Scene for logging in with a wallet
 + Added scripts for logging in with an existing private key, for generating a new private + public key and for generating a new 
 private + public key (or logging in, in case such an account already exists) with an HD Wallet
_____________________

- Finished (8.1.2023; 21:23)
 + Added Player ScriptableObject
 + Edited various scripts
 + Added feature to save the private / public keys across the game scenes + log user out upon returning to the Main Menu
 + Added functions for the "Continue" buttons on the login / register screen to continue to the next scenes with the previous data saved
 + Adjusted UI functionalities accordingly

Possible Issue with private keys might come up later (PKs that are generated through my scripts start with 0x, while the metamask PKs
don't have 0x in front. Might need to remove 0x from custom PKs later if issues arise): 
https://github.com/sammchardy/python-idex/issues/7

- Next I need to:
 + Mint AssetBundles to the main account
 + Allow users to purchase said bundles (skins)
 + Load which bundles a user has access to upon scene entry
 + Add "Select Skin" button in the "Flappy" scene that will transfer over to the "Skin select" scene
 + Create skin selection and purchase features
 + Somehow load the list of URLs for the assets saved on IPFS (Pinata.cloud)

- Plans from earlier:
 + Then I need to enable a next / previous asset bundle script that would load the rest from the server upon clicking
 (or maybe I could download all the asset bundles upon initial load, and just swap between them for a more responsive experience)
 + Finally, edit the already existing ERC-1155 contract to:
  * mint the asset bundles to the blockchain using the main contractAddress
  * set the price for purchasing asset bundles and implement functions for how that would work
  * fetch the asset bundles that an address holds
  * optionally implement trading of said asset bundles between addresses

- Optionally:
 + Add a counter for collisions, AKA. score counter (started working on it, but it might be a waste of time atm)
 + Add a UI for generating a new wallet / account
 + Add a "skip login" option


