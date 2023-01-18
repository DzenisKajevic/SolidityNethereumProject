using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSO : ScriptableObject
{
    [SerializeField]
    private string _privateKey;
    [SerializeField]
    private string _publicKey;
    public List<System.Numerics.BigInteger> _skinList;
    public int skinIndex;
    public string skinURL;
    public decimal balance;

    // even though these values are initialised here, they will be null. They need to be manually added either through functions or the Editor
    public string _url = "https://goerli.infura.io/v3/890a7f3aaf0d4b8eae37992a8e12a982";
    public int chainID = 5;
    public string contractAddress = "0x65724360670a18bC2A2fa3041BD78F483e34a3D4";
    public string contractABI = "[{'inputs':[],'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'account','type':'address'},{'indexed':true,'internalType':'address','name':'operator','type':'address'},{'indexed':false,'internalType':'bool','name':'approved','type':'bool'}],'name':'ApprovalForAll','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'previousOwner','type':'address'},{'indexed':true,'internalType':'address','name':'newOwner','type':'address'}],'name':'OwnershipTransferred','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'account','type':'address'}],'name':'Paused','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'role','type':'bytes32'},{'indexed':true,'internalType':'bytes32','name':'previousAdminRole','type':'bytes32'},{'indexed':true,'internalType':'bytes32','name':'newAdminRole','type':'bytes32'}],'name':'RoleAdminChanged','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'role','type':'bytes32'},{'indexed':true,'internalType':'address','name':'account','type':'address'},{'indexed':true,'internalType':'address','name':'sender','type':'address'}],'name':'RoleGranted','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'role','type':'bytes32'},{'indexed':true,'internalType':'address','name':'account','type':'address'},{'indexed':true,'internalType':'address','name':'sender','type':'address'}],'name':'RoleRevoked','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'operator','type':'address'},{'indexed':true,'internalType':'address','name':'from','type':'address'},{'indexed':true,'internalType':'address','name':'to','type':'address'},{'indexed':false,'internalType':'uint256[]','name':'ids','type':'uint256[]'},{'indexed':false,'internalType':'uint256[]','name':'values','type':'uint256[]'}],'name':'TransferBatch','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'operator','type':'address'},{'indexed':true,'internalType':'address','name':'from','type':'address'},{'indexed':true,'internalType':'address','name':'to','type':'address'},{'indexed':false,'internalType':'uint256','name':'id','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'value','type':'uint256'}],'name':'TransferSingle','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'string','name':'value','type':'string'},{'indexed':true,'internalType':'uint256','name':'id','type':'uint256'}],'name':'URI','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'internalType':'address','name':'account','type':'address'}],'name':'Unpaused','type':'event'},{'inputs':[],'name':'DEFAULT_ADMIN_ROLE','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'MINTER_ROLE','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'PAUSER_ROLE','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'string','name':'assetBundleID','type':'string'},{'internalType':'uint256','name':'initialSupply','type':'uint256'}],'name':'addAssetBundleID','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'assetBundleIDCounter','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'assetBundlePrice','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'uint256','name':'id','type':'uint256'}],'name':'balanceOf','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address[]','name':'accounts','type':'address[]'},{'internalType':'uint256[]','name':'ids','type':'uint256[]'}],'name':'balanceOfBatch','outputs':[{'internalType':'uint256[]','name':'','type':'uint256[]'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'uint256','name':'id','type':'uint256'},{'internalType':'uint256','name':'value','type':'uint256'}],'name':'burn','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'uint256[]','name':'ids','type':'uint256[]'},{'internalType':'uint256[]','name':'values','type':'uint256[]'}],'name':'burnBatch','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'getAllTokens','outputs':[{'internalType':'uint256[]','name':'','type':'uint256[]'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'string','name':'assetBundleID','type':'string'}],'name':'getContractTokenBalanceByAssetBundleId','outputs':[{'internalType':'uint256','name':'balance','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'uint256','name':'index','type':'uint256'}],'name':'getContractTokenBalanceByIndex','outputs':[{'internalType':'uint256','name':'balance','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'}],'name':'getRoleAdmin','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'uint256','name':'index','type':'uint256'}],'name':'getRoleMember','outputs':[{'internalType':'address','name':'','type':'address'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'}],'name':'getRoleMemberCount','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'string','name':'assetBundleID','type':'string'}],'name':'getUserTokenBalanceByAssetBundleId','outputs':[{'internalType':'uint256','name':'balance','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'uint256','name':'index','type':'uint256'}],'name':'getUserTokenBalanceByIndex','outputs':[{'internalType':'uint256','name':'balance','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'grantRole','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'hasRole','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'string','name':'','type':'string'}],'name':'idmap','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'address','name':'operator','type':'address'}],'name':'isApprovedForAll','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'uint256','name':'','type':'uint256'}],'name':'lookupmap','outputs':[{'internalType':'string','name':'','type':'string'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'to','type':'address'},{'internalType':'uint256','name':'id','type':'uint256'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'bytes','name':'data','type':'bytes'}],'name':'mint','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'to','type':'address'},{'internalType':'uint256[]','name':'ids','type':'uint256[]'},{'internalType':'uint256[]','name':'amounts','type':'uint256[]'},{'internalType':'bytes','name':'data','type':'bytes'}],'name':'mintBatch','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'','type':'address'},{'internalType':'address','name':'','type':'address'},{'internalType':'uint256[]','name':'','type':'uint256[]'},{'internalType':'uint256[]','name':'','type':'uint256[]'},{'internalType':'bytes','name':'','type':'bytes'}],'name':'onERC1155BatchReceived','outputs':[{'internalType':'bytes4','name':'','type':'bytes4'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'','type':'address'},{'internalType':'address','name':'','type':'address'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'bytes','name':'','type':'bytes'}],'name':'onERC1155Received','outputs':[{'internalType':'bytes4','name':'','type':'bytes4'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'owner','outputs':[{'internalType':'address','name':'','type':'address'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'pause','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'paused','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'uint256','name':'index','type':'uint256'}],'name':'purchaseAssetBundleID','outputs':[],'stateMutability':'payable','type':'function'},{'inputs':[],'name':'renounceOwnership','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'renounceRole','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'revokeRole','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'from','type':'address'},{'internalType':'address','name':'to','type':'address'},{'internalType':'uint256[]','name':'ids','type':'uint256[]'},{'internalType':'uint256[]','name':'amounts','type':'uint256[]'},{'internalType':'bytes','name':'data','type':'bytes'}],'name':'safeBatchTransferFrom','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'from','type':'address'},{'internalType':'address','name':'to','type':'address'},{'internalType':'uint256','name':'id','type':'uint256'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'bytes','name':'data','type':'bytes'}],'name':'safeTransferFrom','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'operator','type':'address'},{'internalType':'bool','name':'approved','type':'bool'}],'name':'setApprovalForAll','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes4','name':'interfaceId','type':'bytes4'}],'name':'supportsInterface','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'newOwner','type':'address'}],'name':'transferOwnership','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'unpause','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'uint256','name':'index','type':'uint256'}],'name':'uri','outputs':[{'internalType':'string','name':'','type':'string'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'withdraw','outputs':[],'stateMutability':'nonpayable','type':'function'}]";

    public void resetValues()
    {
        PrivateKey = null;
        PublicKey = null;
        SkinList = null;
        skinIndex = 0;
        skinURL = null;
        balance = 0;
    }
    public string PrivateKey
    {
        get { return _privateKey; }
        set { _privateKey = value; }
    }
    public string PublicKey
    {
        get { return _publicKey; }
        set { _publicKey = value; }
    }
    public List<System.Numerics.BigInteger> SkinList
    {
        get { return _skinList; }
        set { _skinList = value; }
    }

}
