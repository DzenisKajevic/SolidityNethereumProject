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
    public string[] _assetBundleLinkArray;
    // even though these values are initialised here, they will be null. They need to be manually added either through functions or the Editor
    public string _url = "https://goerli.infura.io/v3/890a7f3aaf0d4b8eae37992a8e12a982";
    public int chainID = 5;
    public string contractAddress = "0xe51C2807EfA9F7513Dc6b2a2859eEFEac15A5D75";
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
    public string[] AssetBundleLinkArray
    {
        get { return _assetBundleLinkArray; }
        set { _assetBundleLinkArray = value; }
    }

}
