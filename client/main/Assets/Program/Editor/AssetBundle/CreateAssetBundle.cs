using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class VisualAutuPack : EditorWindow
{
    public static string[] folders = new string[] { "Assets/Art/Characters" };
    public static int curNum = 0;
    public static int maxNum = 100;
    public static string[] allmat;
    public static string path = string.Empty;
    public static string SavePath;
    public static UnityEditor.BuildTarget _target;
    public static void DelAnimationComponent()
    {
        allmat = AssetDatabase.FindAssets("t:Prefab", folders);
        for (int i = 0; i < allmat.Length; i++)
        {
            path = AssetDatabase.GUIDToAssetPath(allmat[i]);
            if (path.Contains("Effects/Resources"))
            {
                continue;
            }
            GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object)) as GameObject;

            Animation _anim = obj.GetComponent<Animation>();
            DestroyImmediate(_anim,true);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    public static void ExecuteAnim(UnityEditor.BuildTarget target)
    {
        allmat = AssetDatabase.FindAssets("t:AnimationClip", folders);
        Dictionary<string,List<Object>> animMap = new Dictionary<string,List<Object>>();

        string strKey = string.Empty;
        SavePath = AssetBundleController.GetPlatformPath(target);

        for (int i = 0; i < allmat.Length; i++ )
        {
            path = AssetDatabase.GUIDToAssetPath(allmat[i]);
            if (path.Contains("Models/Animations") == false)
            {
                continue;
            }
            Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));

            strKey = path.Split('/')[3];

            if (animMap.ContainsKey(strKey) == false)
            {
                List<Object> animObj = new List<Object>();
                animObj.Clear();
                animMap.Add(strKey, animObj);
                animMap[strKey].Add(obj);
            }
            else
            {
                animMap[strKey].Add(obj);
            }
        }
        foreach (KeyValuePair<string,List<Object>> item in animMap)
        {
            StringBuilder animName = new StringBuilder();
            animName.Append(item.Key);
            animName.Append("_Anim");
            string exportPate = SavePath + animName.ToString() + ".assetbundle";
            Object[] list = item.Value.ToArray();
            BuildPipeline.BuildAssetBundle(null, list, exportPate,
                                           BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle,
                                           target);
        }

        EditorUtility.DisplayDialog("", "一键所有动作组件bundle", "OK");
        AssetDatabase.Refresh();
    }

    public static void Execute(UnityEditor.BuildTarget target)
    {
        allmat = AssetDatabase.FindAssets("t:Prefab", folders);
        maxNum = allmat.Length;
        curNum = 0;
        _target = target;
        SavePath = AssetBundleController.GetPlatformPath(_target);

        for (int i = 0; i < maxNum; i++)
        {
            curNum = i;
            path = AssetDatabase.GUIDToAssetPath(allmat[curNum]);
            if (path.Contains("3D"))
            {
                continue;
            }
            Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));

            path = SavePath + obj.name;
            path += ".assetbundle";
            Debug.Log(path);

            BuildPipeline.BuildAssetBundle(obj, null, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, _target);
        }

        EditorUtility.DisplayDialog("", "一键打包", "OK");
        //VisualAutuPack win = (VisualAutuPack)EditorWindow.GetWindowWithRect(typeof(VisualAutuPack), new Rect(Screen.width / 2 - 300, Screen.height / 2, 600, 400), true, "打包中");
    }

    void OnGUI()
    {
//         EditorGUILayout.LabelField(path);
//         Rect rec = GUILayoutUtility.GetRect(50, 50, "TextField");//new Rect(0,0,600,50)
//         EditorGUI.ProgressBar(rec, (float)curNum / (float)maxNum, "   当前进度: " + (int)((float)curNum / (float)maxNum * 100) + "%");
//         if (GUI.Button(new Rect(50f, 100f, 200f, 50f), "开始打包"))
//         {
//             for (int i = 0; i < maxNum; i++ )
//             {
//                 curNum = i;
//                 path = AssetDatabase.GUIDToAssetPath(allmat[curNum]);
//                 if (path.Contains("3D"))
//                 {
//                     continue;
//                 }
//                 Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
// 
//                 path = SavePath + obj.name;
//                 path += ".assetbundle";
//                 Debug.Log(path);
// 
//                 BuildPipeline.BuildAssetBundle(obj, null, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, _target);
//             }
// 
//             EditorUtility.DisplayDialog("", "一键打包", "OK");
//         }
    }
}
public class CreateAssetBundle
{
    public static void Execute(UnityEditor.BuildTarget target)
    {
//         if (AssetBundleController.CheckPlatform(target) == false)
//             return;

        string SavePath = AssetBundleController.GetPlatformPath(target);

        // 当前选中的资源列表
        foreach (Object o in Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets))
        {
            string path = AssetDatabase.GetAssetPath(o);

            // 过滤掉meta文件和文件夹
            if (path.Contains(".meta") || path.Contains(".") == false)
                continue;

            // 过滤掉UIAtlas目录下的贴图和材质(UI/Common目录下的所有资源都是UIAtlas)
            if (path.Contains("UI/Common"))
            {
                if ((o is Texture) || (o is Material))
                    continue;
            }

            path = SavePath + o.name;
            //path = path.Substring(SavePath.Length, path.Length - path.LastIndexOf('.') - 1);
            path += ".assetbundle";

            BuildPipeline.BuildAssetBundle(o, null, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, target);
        }

        AssetDatabase.Refresh();
    }


    public static void ExecuteScene(UnityEditor.BuildTarget target)
    {
        //清空一下缓存
        Caching.CleanCache();

        string SavePath = AssetBundleController.GetPlatformPath(target);
        string exportPath = SavePath + "Scene/";
        if (Directory.Exists(exportPath) == false)
            Directory.CreateDirectory(exportPath);

        string currentScene = EditorApplication.currentScene;
        string currentSceneName = currentScene.Substring(currentScene.LastIndexOf('/') + 1, currentScene.LastIndexOf('.') - currentScene.LastIndexOf('/') - 1);
        string fileName = exportPath + currentSceneName + ".unity3d";
        BuildPipeline.BuildStreamedSceneAssetBundle(new string[1] { EditorApplication.currentScene }, fileName, target);

        AssetDatabase.Refresh();

    }

    public static void ExecuteAll(UnityEditor.BuildTarget target,string name)
    {
        //清空一下缓存
        Caching.CleanCache();

        string SavePath = AssetBundleController.GetPlatformPath(target);
        if (SavePath.Length != 0)
        {
            Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            string exportPate = SavePath + name + ".assetbundle";

            BuildPipeline.BuildAssetBundle(null, SelectedAsset, exportPate,
                                           BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle,
                                           target);
        }

        AssetDatabase.Refresh();
    }

    public static void ExecuteAll(string filePath, UnityEditor.BuildTarget target)
    {
        //清空一下缓存
        Caching.CleanCache();

        if (filePath.Length != 0)
        {
            Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            BuildPipeline.BuildAssetBundle(null, SelectedAsset, filePath,
                                          BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle,
                                          target);
        }

        AssetDatabase.Refresh();
    }

    public static void ExecuteEncryption(UnityEditor.BuildTarget target)
    {
        //清空一下缓存
        Caching.CleanCache();

        string platfrom = AssetBundleController.GetPlatformName(target);
        string dir = System.IO.Path.Combine(Application.dataPath, "StreamingAssets/AssetBundle/" + platfrom);

        foreach (string filePath in Directory.GetFiles(dir))
        {
            if (filePath.Contains(".meta") || filePath.Contains("VersionMD5") || filePath.Contains(".xml") || filePath.Contains(".enc") || filePath.Contains(".lua"))
                continue;
            string key = filePath.Substring(dir.Length + 1, filePath.Length - dir.Length - 1);

            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            byte[] fileData = new byte[file.Length];
            byte[] NewHeader = System.Text.Encoding.UTF8.GetBytes("DreamfactionGame");
            byte[] NewEnd = System.Text.Encoding.UTF8.GetBytes("zmy");
            byte[] buff = new byte[(int)file.Length + NewHeader.Length + NewEnd.Length];
            file.Read(fileData, 0, (int)file.Length);

            //对二进制文件添加固定头尾，中端修改内容
            for (int i = 0; i < buff.Length; i++)
            {
                if (i >= 0 && i < NewHeader.Length)
                {
                    buff[i] = NewHeader[i];
                    //buff[i] += (byte)i;
                }
                else if (i >= NewHeader.Length && i < fileData.Length + NewHeader.Length)
                {
                    buff[i] = fileData[i - NewHeader.Length];
                    buff[i] += 1;
                    
                }
                else if (i >= fileData.Length + NewHeader.Length && i < buff.Length)
                {
                    int randomNum = Random.Range(97, 123);
                    buff[i] = (byte)randomNum;
                }
            }
            file.Flush();
            file.Close();
            fileData = null;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            key = key.Substring(0, key.LastIndexOf('.'));
            key += ".enc";

            string newFilePath = filePath.Substring(0, filePath.LastIndexOf('.'));
            newFilePath += ".enc";
            if (File.Exists(newFilePath))
            {
//                 string nMessage = key + "资源有重名，是否覆盖";
//                 if (EditorUtility.DisplayDialog("", nMessage, "是", "否") == false)
//                 {
//                     EditorUtility.DisplayDialog("", "资源加密已取消，请修改资源名称重新打包", "OK");
//                     return;
//                 }
//                 else
//                 {
//                     File.Delete(key);
//                 }
                File.Delete(key);
            }

            FileStream stream = new FileStream(dir + "/" + key, FileMode.Create);
            stream.Write(buff, 0, buff.Length);
            stream.Flush();
            stream.Close();
            buff = null;

        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("", "资源加密完成", "OK");
    }

    public static void ExecuteDecrypt(UnityEditor.BuildTarget target)
    {
        //清空一下缓存
        Caching.CleanCache();

        string platfrom = AssetBundleController.GetPlatformName(target);
        string dir = System.IO.Path.Combine(Application.dataPath, "AssetBundle/" + platfrom);

        foreach (string filePath in Directory.GetFiles(dir))
        {
            if (filePath.Contains(".meta") || filePath.Contains("VersionMD5") || filePath.Contains(".xml") || filePath.Contains(".assetbundle"))
                continue;
            string key = filePath.Substring(dir.Length + 1, filePath.Length - dir.Length - 1);

            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            byte[] fileData = new byte[file.Length];
            byte[] NewHeader = System.Text.Encoding.UTF8.GetBytes("DreamfactionGame");
            byte[] NewEnd = System.Text.Encoding.UTF8.GetBytes("zmy");
            byte[] buff = new byte[(int)file.Length - NewHeader.Length - NewEnd.Length];
            file.Read(fileData, 0, (int)file.Length);

            for (int i = 0; i < fileData.Length; i++)
            {
                if (i >= 0 && i < NewHeader.Length)
                {
                    //buff[i] = NewHeader[i];
                    //buff[i] += (byte)i;
                }
                else if (i >= NewHeader.Length && i < fileData.Length - NewEnd.Length)
                {
                    buff[i - NewHeader.Length] = fileData[i];
                    buff[i - NewHeader.Length] -= 1;
                               
                }
                else if (i >= fileData.Length - NewEnd.Length && i < fileData.Length)
                {
                    //buff[i] = NewEnd[i - fileData.Length];
                }
            }

            file.Flush();
            file.Close();
            fileData = null;

            key = key.Substring(0, key.LastIndexOf('.'));
            key += ".dec";

            FileStream stream = new FileStream(dir + "/" + key, FileMode.Create);
            stream.Write(buff, 0, buff.Length);
            stream.Flush();
            stream.Close();
            buff = null;
        }

        AssetDatabase.Refresh();
    }
    static string ConvertToAssetBundleName(string ResName)
    {
        return ResName.Replace('/', '.');
    }

}