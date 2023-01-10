// SPDX-License-Identifier: MIT
pragma solidity ^0.8.1;

import "@openzeppelin/contracts/token/ERC1155/presets/ERC1155PresetMinterPauser.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/token/ERC1155/extensions/ERC1155Supply.sol";

contract AssetBundleTokens is ERC1155PresetMinterPauser, Ownable {
    uint public assetBundlePrice = 0.01 ether;
    uint256 public assetBundleIDCounter;

    mapping(string => uint256) public idmap;
    mapping(uint256 => string) public lookupmap;

    constructor() ERC1155PresetMinterPauser("https://gateway.pinata.cloud/ipfs/") {
        //base URI
    }

    function addAssetBundleID(string memory assetBundleID, uint256 initialSupply) public onlyOwner {
        require(idmap[assetBundleID] == 0, "AssetBundleTokens: This AssetBundleID already exists");

        assetBundleIDCounter = assetBundleIDCounter + 1;
        idmap[assetBundleID] = assetBundleIDCounter;
        lookupmap[assetBundleIDCounter] = assetBundleID;

        _mint(msg.sender, assetBundleIDCounter, initialSupply, "");
    }

    function purchaseAssetBundleID() public payable {
        require(msg.value == assetBundlePrice, "Insufficient funds for skin purchase");
        //safeTransferFrom
    }

    function uri(uint256 id)
        public
        view
        virtual
        override
        returns (string memory)
    {
        return
            string(
                abi.encodePacked(super.uri(id), lookupmap[id])
            );
    }

    function getAllTokens(address account)
        public
        view
        returns (uint256[] memory)
    {
        uint256 numTokens = 0;
        for (uint256 i = 0; i <= assetBundleIDCounter; i++) {
            if (balanceOf(account, i) > 0) {
                numTokens++;
            }
        }

        uint256[] memory ret = new uint256[](numTokens);
        uint256 counter = 0;
        for (uint256 i = 0; i <= assetBundleIDCounter; i++) {
            if (balanceOf(account, i) > 0) {
                ret[counter] = i;
                counter++;
            }
        }

        return ret;
    }
}
