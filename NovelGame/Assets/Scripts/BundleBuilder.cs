using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BundleBuilder : Editor
{
    [MenuItem("Assets/ Build AssetBundles")]
    static void BuildAssetBundle ()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Users\Dimas\Desktop", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
    }
}
