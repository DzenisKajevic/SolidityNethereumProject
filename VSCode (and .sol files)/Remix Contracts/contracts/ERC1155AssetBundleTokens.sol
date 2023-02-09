// SPDX-License-Identifier: MIT
pragma solidity ^0.8.1;

import "@openzeppelin/contracts/token/ERC1155/presets/ERC1155PresetMinterPauser.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/token/ERC1155/extensions/ERC1155Supply.sol";
import "@openzeppelin/contracts/token/ERC1155/IERC1155Receiver.sol";

contract AssetBundleTokens is
    ERC1155PresetMinterPauser,
    Ownable,
    IERC1155Receiver
{
    uint256 public assetBundlePrice = 0.01 ether;
    uint256 public assetBundleIDCounter = 0;

    address payable contractOwner;

    mapping(string => uint256) public idmap;
    mapping(uint256 => string) public lookupmap;

    constructor()
        ERC1155PresetMinterPauser("https://gateway.pinata.cloud/ipfs/")
    {
        contractOwner = payable(msg.sender);
        //base URI
    }

    function addAssetBundleID(
        string memory assetBundleID,
        uint256 initialSupply
    ) public onlyOwner {
        require(
            idmap[assetBundleID] == 0,
            "AssetBundleTokens: This AssetBundleID already exists"
        );

        assetBundleIDCounter = assetBundleIDCounter + 1;
        idmap[assetBundleID] = assetBundleIDCounter;
        lookupmap[assetBundleIDCounter] = assetBundleID;

        _mint(address(this), assetBundleIDCounter, initialSupply, "");
    }

    function purchaseAssetBundleID(uint256 index) public payable {
        require(
            msg.value == assetBundlePrice,
            "Insufficient funds for skin purchase"
        );
        safeTransferFrom(address(this), msg.sender, index, 1, "");
    }

    function withdraw() external onlyOwner {
        uint256 balance = address(this).balance;
        contractOwner.transfer(balance);
    }

    // fetches the URI for the IPFS nft
    function uri(uint256 index)
        public
        view
        virtual
        override
        returns (string memory)
    {
        return string(abi.encodePacked(super.uri(index), lookupmap[index]));
    }

    function getUserTokenBalanceByAssetBundleId(
        address account,
        string calldata assetBundleID
    ) public view returns (uint256 balance) {
        return balanceOf(account, idmap[assetBundleID]);
    }

    function getUserTokenBalanceByIndex(address account, uint256 index)
        public
        view
        returns (uint256 balance)
    {
        return balanceOf(account, index);
    }

    function getContractTokenBalanceByAssetBundleId(
        string calldata assetBundleID
    ) public view returns (uint256 balance) {
        return balanceOf(address(this), idmap[assetBundleID]);
    }

    function getContractTokenBalanceByIndex(uint256 index)
        public
        view
        returns (uint256 balance)
    {
        return balanceOf(address(this), index);
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

    // this function is currently useless, since the other contract is no longer needed (commented out),

    //function approvalForAnotherContractToSellAssetBundles(address operatorContractAddress, bool approved) public onlyOwner {
    //    setApprovalForAll(operatorContractAddress, approved);
    //}

    function onERC1155Received(
        address,
        address,
        uint256,
        uint256,
        bytes memory
    ) public virtual override returns (bytes4) {
        return this.onERC1155Received.selector;
    }

    function onERC1155BatchReceived(
        address,
        address,
        uint256[] memory,
        uint256[] memory,
        bytes memory
    ) public virtual override returns (bytes4) {
        return this.onERC1155BatchReceived.selector;
    }

    // overridden so that everyone can purchase bundles from the contract
    // previously, there was a permission error
    // it took me 3 hours to solve... Not sure how I didn't think of overriding the base function sooner
    // although, the contract doesn't follow ERC1155PresetMinterPauser's interface fully anymore because of this...
    function safeTransferFrom(
        address from,
        address to,
        uint256 id,
        uint256 amount,
        bytes memory data
    ) public virtual override {
        //require(
        //    from == _msgSender() || isApprovedForAll(from, _msgSender()),
        //    "ERC1155: caller is not owner nor approved"
        //);
        _safeTransferFrom(from, to, id, amount, data);
    }
}
/*
contract AssetBundleSeller {
    uint public assetBundlePrice = 0.01 ether;
    address payable contractOwner;
    
    AssetBundleTokens private mainAssetBundleContract;
    address private mainAssetBundleContractAddress;

    constructor(address mainContractAddress)  {
        mainAssetBundleContract = AssetBundleTokens(mainContractAddress);
        mainAssetBundleContractAddress = mainContractAddress;
        contractOwner = payable(msg.sender);
    }

    function purchaseAssetBundleID(uint256 index) public payable {
        require(msg.value == assetBundlePrice, "Insufficient funds for skin purchase");
        //mainAssetBundleContract.safeTransferFrom(mainAssetBundleContractAddress, msg.sender, index, 1, "");
        mainAssetBundleContract.purchaseAssetBundleID(msg.sender, index);
        contractOwner.transfer(msg.value);
    }

}*/
