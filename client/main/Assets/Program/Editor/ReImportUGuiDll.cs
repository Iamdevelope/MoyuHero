using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text.RegularExpressions;
using System.IO;

public class ReImportUGuiDll : MonoBehaviour {

	// Use this for initialization
   [MenuItem("Tools/ReImportUGUI",false,100)]
    static void ReImPortUIDll()
    {
#if UNITY_4_6
        var path = EditorApplication.applicationContentsPath + "/UnityExtensions/Unity/GUISystem/{0}/{1}";
        var version = Regex.Match(Application.unityVersion,@"^[0-9]+\.[0-9]+\.[0-9]+").Value;
#else
        var path = EditorApplication.applicationContentsPath + "/UnityExtensions/Unity/GUISystem/{1}";
        var version =string.Empty
#endif
        string engineDll = string.Format(path,version,"UnityEngine.UI.dll");
        string editorDll = string.Format(path,version,"Editor/UnityEditor.UI.dll");
        ReImport(engineDll);
        ReImport(editorDll);
    }
   static void ReImport(string path)
   {
       if (File.Exists(path))
           AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.DontDownloadFromCacheServer);
       else
           Debug.LogError("Not Found Dll "+path);
   }
}
