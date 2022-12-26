using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class AssetBundleWebLoader : MonoBehaviour
{
    public string bundleUrl = "http://localhost/AssetBundles/b1_pink";
    // Skin select version
    public string assetName = "Bird1 Pink Idle Variant";
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