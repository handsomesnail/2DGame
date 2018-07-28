using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct AssetData {
    
    public string tag;
    public string name;
    public Object assetReference;
    public AssetData(string tag, string name, Object assetReference) {
        this.tag = tag;
        this.name = name;
        this.assetReference = assetReference;
    }
}

public static class ResourceManager {

    public const string globalResourceTag = "Global";

    //name-data
    private static Dictionary<string, AssetData> resourceCache = new Dictionary<string, AssetData>();

    public static IEnumerator LoadAsync(string name) {
        yield return LoadAsync(name, globalResourceTag);
    }

    public static IEnumerator LoadAsync(string name, string tag) {
        if (resourceCache.ContainsKey(name))
            Debug.LogWarning("重复加载" + name);

        ResourceRequest resourceRequest = Resources.LoadAsync(name);
        while (!resourceRequest.isDone) {
            yield return resourceRequest;
        }
        Object asset = resourceRequest.asset;
        resourceCache.Add(name, new AssetData(tag, name, asset));
    }

    public static void UnLoadAsset(string name) {
        if (!resourceCache.ContainsKey(name))
            Debug.LogWarning("未加载该资源" + name);
        Resources.UnloadAsset(resourceCache[name].assetReference);
        resourceCache.Remove(name);
    }

    public static void UnLoadTagedAsset(string tag) {
       foreach (var assetData in resourceCache.ToList().Where((kvp)=>kvp.Key == tag)) {
            UnLoadAsset(assetData.Key);
        }
    }

}
