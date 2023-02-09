using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class AssetBundleWebLoader: MonoBehaviour
{
    //public string bundleUrl = "http://localhost/AssetBundles/b1_pink";
    public string bundleUrl = "https://gateway.pinata.cloud/ipfs/Qma2ukdaZmcdfYF9m5pBop29jT1iBozBEdssmKrCLBGaeP"; //b1_blue
    // CID pink: QmawWKn6aGjHM5TVeY7Tim3CZ98WsyPYohtA3tFsEnD7pU
    // CID blue: Qma2ukdaZmcdfYF9m5pBop29jT1iBozBEdssmKrCLBGaeP
    // CID red: Qma2ukdaZmcdfYF9m5pBop29jT1iBozBEdssmKrCLBGaeP

    // Skin select version
    public string assetName = "Bird1 Blue Idle Variant";
    // Playable version
    // public string assetName = "Bird1 Pink"; 

    void Start()
    {
        StartCoroutine(GetAssetBundle());
    }

    IEnumerator GetAssetBundle()
    {
        Debug.Log(bundleUrl + assetName);
        //UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("http://localhost/AssetBundles/b1_red");
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        // no need to send the JWT for Pinata Cloud if accessing from their public gateway
        //www.SetRequestHeader("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySW5mb3JtYXRpb24iOnsiaWQiOiIyZDAxM2IxZi1iNjViLTQ2YWUtOGE4My1mYWFjODZjMDdlODciLCJlbWFpbCI6ImRlbmlzZ2VuaXNAeWFob28uY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsInBpbl9wb2xpY3kiOnsicmVnaW9ucyI6W3siaWQiOiJGUkExIiwiZGVzaXJlZFJlcGxpY2F0aW9uQ291bnQiOjF9LHsiaWQiOiJOWUMxIiwiZGVzaXJlZFJlcGxpY2F0aW9uQ291bnQiOjF9XSwidmVyc2lvbiI6MX0sIm1mYV9lbmFibGVkIjpmYWxzZSwic3RhdHVzIjoiQUNUSVZFIn0sImF1dGhlbnRpY2F0aW9uVHlwZSI6InNjb3BlZEtleSIsInNjb3BlZEtleUtleSI6ImVjMGU1ZGIxYzBiZDU2Mjg4N2I4Iiwic2NvcGVkS2V5U2VjcmV0IjoiNGUyN2M2Y2M5NzljZTE4OTYxOGYwMzA2ZmZiYWJmNzAxYmE4NDE4MmM2M2E1NjJlNDRhNzNlNTRmYjk4YTAwOSIsImlhdCI6MTY3MzM4NDAzMn0.7mUIpaOPzzqBXmxcP4LcNzogap416r1z4j_tjn0uO1g");
        yield return www.SendWebRequest();
        
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            Debug.Log(bundle.GetAllAssetNames());
            Instantiate(bundle.LoadAsset(assetName));
            bundle.Unload(false);
        }
    }
}