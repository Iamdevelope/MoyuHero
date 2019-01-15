/********************************************************************
	created:	2014/10/13
	created:	13:10:2014   15:31
	filename: 	D:\GameCode\GameClient\trunk\Assets\Editor\AssetBundle\AssetBundleController.cs
	file path:	D:\GameCode\GameClient\trunk\Assets\Editor\AssetBundle
	file base:	AssetBundleController
	file ext:	cs
	author:		Zmy
	
	purpose:	bundle打包控制器。根据不同平台，主要分为打散包和公共包；
 *              散包与公共包统一分为两个步骤：
 *              1,根据需求。选择打包方式，散包根据所选资源所在目标命名
 *                 公共包暂时分三大类，固定命名
 *              2，生成所有包后，选择 生成MD5 ；自动对比文件，生成MD5
 *                 信息，此过程自动完成，会对比上次的文件变化生成新旧
 *                 两份MD5对比文件，方便查看是否文件有更改
 *              
 *              ps:公共包的选项，后续可以根据需求再进行相应扩展
*********************************************************************/
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleController : EditorWindow
{
    public static AssetBundleController window;
    public static UnityEditor.BuildTarget buildTarget = BuildTarget.StandaloneWindows;

    [MenuItem("Create Bundle/AssetBundle/AssetBundle For Windows32", false, 1)]
    public static void ExecuteWindows32()
    {
        if (window == null)
        {
            window = (AssetBundleController)GetWindow(typeof(AssetBundleController));
        }
        buildTarget = UnityEditor.BuildTarget.StandaloneWindows;
        window.Show();
    }

    [MenuItem("Create Bundle/AssetBundle/AssetBundle For IPhone", false, 2)]
    public static void ExecuteIPhone()
    {
        if (window == null)
        {
            window = (AssetBundleController)GetWindow(typeof(AssetBundleController));
        }
        buildTarget = UnityEditor.BuildTarget.iPhone;
        window.Show();
    }

    [MenuItem("Create Bundle/AssetBundle/AssetBundle For Mac", false, 3)]
    public static void ExecuteMac()
    {
        if (window == null)
        {
            window = (AssetBundleController)GetWindow(typeof(AssetBundleController));
        }
        buildTarget = UnityEditor.BuildTarget.StandaloneOSXUniversal;
        window.Show();
    }

    [MenuItem("Create Bundle/AssetBundle/AssetBundle For Android", false, 4)]
    public static void ExecuteAndroid()
    {
        if (window == null)
        {
            window = (AssetBundleController)GetWindow(typeof(AssetBundleController));
        }
        buildTarget = UnityEditor.BuildTarget.Android;
        window.Show();
    }

    [MenuItem("Create Bundle/AssetBundle/AssetBundle For WebPlayer", false, 5)]
    public static void ExecuteWebPlayer()
    {
        if (window == null)
        {
            window = (AssetBundleController)GetWindow(typeof(AssetBundleController));
        }
        buildTarget = UnityEditor.BuildTarget.WebPlayer;
        window.Show();
    }
    [MenuItem("Create Bundle/SceneBundle For Windows")]
    public static void CreateSceneWin()
    {
        buildTarget = UnityEditor.BuildTarget.StandaloneWindows;
        CreateAssetBundle.ExecuteScene(buildTarget);
        EditorUtility.DisplayDialog("", "CreateSceneWin (1) Completed", "OK");
    }
    [MenuItem("Create Bundle/SceneBundle For Android")]
    public static void CreateSceneAndroid()
    {
        buildTarget = UnityEditor.BuildTarget.Android;
        CreateAssetBundle.ExecuteScene(buildTarget);
        EditorUtility.DisplayDialog("", "CreateSceneAndroid (1) Completed", "OK");
    }
    [MenuItem("Create Bundle/SceneBundle For IOS")]
    public static void CreateSceneIOS()
    {
        buildTarget = UnityEditor.BuildTarget.iPhone;
        CreateAssetBundle.ExecuteScene(buildTarget);
        EditorUtility.DisplayDialog("", "CreateSceneIOS (1) Completed", "OK");
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(40f, 20f, 200f, 50f), "创建assetbundle散包"))
        {
            CreateAssetBundle.Execute(buildTarget);
            EditorUtility.DisplayDialog("", "创建assetbundle散包", "OK");
        }
        if (GUI.Button(new Rect(40f, 90f, 200f, 50f), "创建assetbundle整包"))
        {
            string filename = EditorUtility.SaveFilePanel("", AssetBundleController.GetPlatformPath(buildTarget), "输入bundle名称,点击保存", "assetbundle");
            CreateAssetBundle.ExecuteAll(filename,buildTarget);
            EditorUtility.DisplayDialog("", filename, "OK");
        }
        if (GUI.Button(new Rect(300f, 20f, 200f, 50f), "一键所有prefab散包"))
        {
            VisualAutuPack.Execute(buildTarget);
            //CreateAssetBundle.Execute(buildTarget);
            //EditorUtility.DisplayDialog("", "创建assetbundle散包", "OK");
        }
        if (GUI.Button(new Rect(300f, 90f, 200f, 50f), "删除动作组件"))
        {
            bool isOk = EditorUtility.DisplayDialog("删除动作组件", "警告：不可逆过程，需要通过svn才能还原！确定要删除所有动作组件吗", "确定", "取消");
            if (isOk)
            {
                VisualAutuPack.DelAnimationComponent();
            }
        }
        if (GUI.Button(new Rect(300f, 160f, 200f, 50f), "一键所有动作组件bundle"))
        {
            VisualAutuPack.ExecuteAnim(buildTarget);

        }
        if (GUI.Button(new Rect(40f, 160f, 200f,50f), "资源加密"))
        {
            CreateAssetBundle.ExecuteEncryption(buildTarget);
            
        }

        if (GUI.Button(new Rect(40f, 230f, 200f, 50f), "生成 MD5"))
        {
            CreateMD5List.Execute(buildTarget);
            EditorUtility.DisplayDialog("", "MD5 生成OK", "OK");
        }

    }
    public static string GetPlatformPath(UnityEditor.BuildTarget target)
    {
        string SavePath = "";
        switch (target)
        {
            case BuildTarget.StandaloneWindows:
                SavePath = "Assets/StreamingAssets/AssetBundle/PC/";
                break;
            case BuildTarget.StandaloneWindows64:
                SavePath = "Assets/StreamingAssets/AssetBundle/Windows64/";
                break;
            case BuildTarget.iPhone:
                SavePath = "Assets/StreamingAssets/AssetBundle/IOS/";
                break;
            case BuildTarget.StandaloneOSXUniversal:
                SavePath = "Assets/StreamingAssets/AssetBundle/Mac/";
                break;
            case BuildTarget.Android:
                SavePath = "Assets/StreamingAssets/AssetBundle/Android/";
                break;
            case BuildTarget.WebPlayer:
                SavePath = "Assets/StreamingAssets/AssetBundle/WebPlayer/";
                break;
            default:
                SavePath = "Assets/StreamingAssets/AssetBundle/";
                break;
        }

        if (Directory.Exists(SavePath) == false)
            Directory.CreateDirectory(SavePath);

        return SavePath;
    }

    public static string GetPlatformName(UnityEditor.BuildTarget target)
    {
        string platform = "PC";
        switch (target)
        {
            case BuildTarget.StandaloneWindows:
                platform = "PC";
                break;
            case BuildTarget.StandaloneWindows64:
                platform = "PC";
                break;
            case BuildTarget.iPhone:
                platform = "IOS";
                break;
            case BuildTarget.StandaloneOSXUniversal:
                platform = "Mac";
                break;
            case BuildTarget.Android:
                platform = "Android";
                break;
            case BuildTarget.WebPlayer:
                platform = "WebPlayer";
                break;
            default:
                break;
        }
        return platform;
    }
    
    public static bool CheckPlatform(UnityEditor.BuildTarget target)
    {
        if (EditorUserBuildSettings.activeBuildTarget != target)
        {
            EditorUtility.DisplayDialog("目标平台与当前平台不一致，请先进行平台转换", "当前平台：" + EditorUserBuildSettings.activeBuildTarget + "\n目标平台：" + target, "OK");
            return false;
        }
        return true;
    }

}