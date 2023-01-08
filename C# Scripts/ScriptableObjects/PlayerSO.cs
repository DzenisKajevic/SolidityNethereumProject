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
    [SerializeField]
    private string[] _assetBundleLinkArray;

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
