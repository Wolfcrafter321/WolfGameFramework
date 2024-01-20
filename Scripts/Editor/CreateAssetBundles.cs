using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
        BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }
}

/*
 * 
 * 
 * アセットバンドルとアセットをロードする
ローカル ストレージからロードしたい場合は、以下のように AssetBundles.LoadFromFile APIを使用します。

public class LoadFromFileExample : MonoBehaviour {
    void Start() {
        var myLoadedAssetBundle 
            = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "myassetBundle"));
        if (myLoadedAssetBundle == null) {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("MyObject");
        Instantiate(prefab);
    }
}
LoadFromFile はバンドルファイルのパスを必要とします。

アセットバンドルを独自にホスティングしていて、それをアプリケーションにダウンロードする必要がある場合は、UnityWebRequestAssetBundle API を使用できます。以下はその使用例です。

IEnumerator InstantiateObject()
{
    string url = "file:///" + Application.dataPath + "/AssetBundles/" + assetBundleName;        
    var request 
        = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
    yield return request.Send();
    AssetBundle bundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);
    GameObject cube = bundle.LoadAsset<GameObject>("Cube");
    GameObject sprite = bundle.LoadAsset<GameObject>("Sprite");
    Instantiate(cube);
    Instantiate(sprite);
}
 * 
 * 
 */