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

    // even though these values are initialised here, they will be null. They need to be manually added either through functions or the Editor
    public string _url = "https://goerli.infura.io/v3/890a7f3aaf0d4b8eae37992a8e12a982";
    public int chainID = 5;
    public string contractAddress = "0x65724360670a18bC2A2fa3041BD78F483e34a3D4";
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
