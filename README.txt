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

_____________________

- Finished (10.1.2023; 23:50)
 + Set up Pinata.cloud IPFS
 + Uploaded temporary AssetBundles to Pinata
 + Edited AssetBundleWebLoader scripts to load files from IPFS (works)
 + Started editing ERC1155 smart contract to enable minting AssetBundles to the contract address (ownerOnly), added Ownable

_____________________

- Finished (12.01.2023; 03:00)
 + Finished watching - https://www.youtube.com/watch?v=wYOPh8TX_Tw
 + Found useful website which lists Public IPFS Gateways: https://ipfs.github.io/public-gateway-checker/
 + Created multiple versions of the smart contract
 + Couldn't verify imports on Etherscan due to OpenZeppelin imports, so I decided to use the "flatten" feature in Remix 
 to merge all the imports into 1 file directly before deploying the contract.
 + Called functions from 1 contract to give access to ERC1155 trading for the other one
 + Enabled minting of AssetBundles to the contract's address
 + Bound the URI for the bundles with the IPFS node where they're uploaded (Pinata)
 + Enabled users to purchase the bundles for 0.01 ETH 
 + The ETH is sent to the contract address now. It used to be sent to the contract owner, but the owner can't send 
 money to himself and it just ends up disappearing :)
 + Added a withdraw function, which transfers the ETH from the contract to the owner

// 1st contract -> https://goerli.etherscan.io/address/0xb5f0d69fc89c197b3a8ae1d3b69de09aece1f6fe#writeContract
// 2nd contract (improved version of 1st, gives token transfer permissions to 3rd) -> https://goerli.etherscan.io/address/0x479b32454685a1c2fe5b25541ac0dda1d5383e97
// 3rd contract (sells tokens from 2nd contract): https://goerli.etherscan.io/address/0x479b32454685a1c2fe5b25541ac0dda1d5383e97

// 4th contract (Everything works!!!) [2 AM... I hate my life] -> https://goerli.etherscan.io/address/0xc2fd08660427e903df9122b0f118d763f2efe0c8
// 5th contract (Improved version of the 4th) [2:25 AM] -> https://goerli.etherscan.io/address/0x20ba8a2112eb5fb8c03a8febb810242d46a6bac4#writeContract

// 6th contract (Improved purchase functionality. Eth is sent to the contract, and can be withdrawn to the owner address) -> https://goerli.etherscan.io/address/0xe51c2807efa9f7513dc6b2a2859eefeac15a5d75
[2:50 AM]

- Immediate plans:
  !!! MainMenu Scene !!!
  - Prilikom logina, pozvati funkciju iz contracta koja vrati sve indekse tokena koje user ima (1, 2, 3) -> MainMenu Continue() funkcije, ScriptableObjects ili PlayerPrefs

  !!! SkinSelect Scene !!!
  - Sama funkcionalnost biranja skinova
  - Bazirano na redoslijedu tokena u contractu, ubaciti lokalne presete u listu (red = 1, blue = ?, pink = ?)
  - Napraviti mapping za index -> lokalni idleAssetName

  !!! ili Flappy Scene ili SkinSelect scene pa transferati na Flappy; nisam siguran sta je najbolja opcija trenutno !!!
  - Napraviti drugi mapping za index -> ERC1155 URL
  - Napraviti treci (mozda postoji bolji nacin) mapping za index -> ERC1155 playableAssetName
  - Prilikom povratka na MainMenu, pored brisanja priv / pub keys, pobrisati i listu tokena + selected skin SO (mozda)

- Finished (14.1.2023; 23:00)
 + C# Contract Definition created
 + PlayerSO changed to hold chainID, url and other info
 + Created script: FetchBoughtSkins, which queries the blockchain for the number of skins a user has bought
 + Adjusted Register script to attempt fetching that info upon logging in 
 (it won't work without a login screen, since swapping scenes cancels coroutines)

- Finished (15.1.2023; 23:00)
 + Edited FetchBoughtSkins so that it transfers over to the skin select screen with the info
 + Added loading screen before skin select
 + Started working on the locked / unlocked skins functionality
 + Fixed all login / register scripts

ONLY MINT RED (1 -> QmS5iuPUuAYGiQxc59hQzsN4Xe33KfoQ8ijBp2VDudRRzz) AND 
PINK (2 -> QmawWKn6aGjHM5TVeY7Tim3CZ98WsyPYohtA3tFsEnD7pU)
BLUE (0) IS FREE.


- Finished (17.1.2023; 21:30)
 + Improved assetBundle prefabs: added PlayerStatusScript, fixed sortingLayer issues, etc.
 + New contract address due to assetBundles needed to be changed: https://goerli.etherscan.io/address/0x65724360670a18bc2a2fa3041bd78f483e34a3d4
 + Could not generate AssetBundles due to the Editor Build mechanism throwing errors for using UnityEditor.SceneManagement
 instead of UnityEngine.SceneManagement, so I had to go through every script that used that import and add preprocessor rules
 + Restructured project for PlasticSCM
 + Generated new AssetBundles
 + Minted b1_red AssetBundle
 + Fixed Skin Select scene to react to the information that the user has or does not have a bundle bought
 (the user can continue if bought, or TO BE IMPLEMENTED: buy the bundle if not)
 + Added fetching urls for assetBundles
 + Loading bought skins from assetBundles (blockchain) works
 + Avoided making a new loading screen for this (camera is moved to the player once the bundle is fully loaded instead,
  which makes it a lot easier and faster develop)
 + Added a backup for AssetBundles on this repo in case something breaks again...

- Finished (18.1.2023; 17:14)
 + Somehow managed to implement purchasing of skins (the documentation for Nethereum is horrible... There are a million
 different ways to call a writeable function from the contract, but are either too bloated to implement or are missing
 the required documentation (somehow managed to find the answer to my issue -> sending ethereum to the contract in the
 official Discord -> The person who intially asked that question ended up fighting with the mods for 2 hours 
 before getting a proper answer...)
 + Once the user starts purchasing a skin, the other buttons are disabled to not break something
 + Once the purchase is finished the buttons are enabled, and if it was successful, the skin is added 
 to the bought skins list

- Finished (18.1.2023; 22:37)
 + Improved clearing of player info upon returning to main menu
 + Added public key textbox on the skin select screen
 + Added account balance & skin price textboxes on the skin select screen
 + Added ability to fetch the account's ETH balance 
 + Added automatic refresh of ETH balance upon successful skin purchase

- Finished(19.1.2023; 15:10)
 + Added a "Main Menu" button in the skin selection scene
 + Implemented the score counting system
 + Added the leaderboard to the left side (currently hardcoded)
  * Tried building it with the OnGUI functionality, and it looked nice, but when I accidentally scaled the 
  game screen, I noticed that the GUI doesn't scale along with it
  * Ended up scrapping that whole thing, and using the Canvas UI

- Next I need to:
 + Create a new Solidity contract to enable the players to push their own score and pull the leaderboard
 + Limit the score upload to only 1 account whose private key will be exposed in the repository
  * Prevents players from finding the contract manually and uploading fake scores

- Plans which probably won't be fulfilled:
 + Trading of skins between players 
  * Reason: Not enough time to complete
 + Adding usernames for each player (pubkey -> username and username -> pubkey mapping)
  * Reason: How to make sure that both the AssetBundleVendor and the Leaderboard contract have the same user mappings?
    It's probably doable, but I don't have enough time currently. I wouldn't want to mess something up for tomorrow

