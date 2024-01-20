using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

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
#endif

/*
 * 
 * 
 * �A�Z�b�g�o���h���ƃA�Z�b�g�����[�h����
���[�J�� �X�g���[�W���烍�[�h�������ꍇ�́A�ȉ��̂悤�� AssetBundles.LoadFromFile API���g�p���܂��B

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
LoadFromFile �̓o���h���t�@�C���̃p�X��K�v�Ƃ��܂��B

�A�Z�b�g�o���h����Ǝ��Ƀz�X�e�B���O���Ă��āA������A�v���P�[�V�����Ƀ_�E�����[�h����K�v������ꍇ�́AUnityWebRequestAssetBundle API ���g�p�ł��܂��B�ȉ��͂��̎g�p��ł��B

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