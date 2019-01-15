using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class RenameFolderAndFiles : EditorWindow
{
    // 界面状态
    private enum WinState
    {
        InitState,
        StartState,
        GoState,
        EndState,
    }
    // 当前界面状态
    private WinState curWinState = WinState.InitState;
    // 替换前的文本
    private string Replace1 = string.Empty;
    // 替换后的文本
    private string Replace2 = string.Empty;
    // 滚动参数
    private Vector2 scrollPos = Vector2.zero;
    // 所有目标路径
    private List<Object> allAssetList = new List<Object>();


    [MenuItem("GameTool/查找替换选中的目录或者文件！")]
    public static void RenameAndReplace()
    {
        RenameFolderAndFiles win = (RenameFolderAndFiles)EditorWindow.GetWindow(typeof(RenameFolderAndFiles), true, "查找/替换 目录名，文件名，只搜索以下目录：Assets/Art/Characters");
        win.ShowNotification(new GUIContent("注意：区分大小写，否则无法完成替换操作！"));
    }


    void OnGUI()
    {
        switch (curWinState)
        {
            case WinState.InitState:
                OnInitState();
                break;
            case WinState.StartState:
                OnStartState();
                break;
        }
    }

       // 窗口初始化操作
    private void OnInitState()
    {
        EditorGUILayout.HelpBox("查找/替换 目录名，文件名，只搜索以下目录：Assets/Art/Characters", MessageType.Info);
        // 绘制替换文本输入框
        GUILayout.BeginHorizontal();
        Replace1 = EditorGUILayout.TextField("要替换的文本：", Replace1);
        if (GUILayout.Button("开始搜索",GUILayout.Width(100)))
        {
            curWinState = WinState.StartState;
            ShowNotification(new GUIContent("注意：区分大小写，否则无法完成替换操作！"));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        
    }


       // 窗口开始界面
    private void OnStartState()
    {
        EditorGUILayout.HelpBox("查找/替换 目录名，文件名，只搜索了Assets/Art/Characters ", MessageType.Info);
        // 绘制替换文本输入框
        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("要替换的文本：", Replace1);
        Replace2 = EditorGUILayout.TextField("替换后的文本：", Replace2);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        // 3: 搜索目录下所有同名资源
        string[] assetList = AssetDatabase.FindAssets(Replace1, new string[] { "Assets/Art/Characters" });
        if (assetList.Length <= 0)
        {
            if (EditorUtility.DisplayDialog("提示", "没有找到搜索的内容：" + Replace1, "关闭"))
            {
                this.Close();
            }
        }
        // 4:初始化路径
        allAssetList.Clear();
        for (int i = 0; i < assetList.Length; ++i)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetList[i]);
            allAssetList.Add(AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object)) as Object);
        }
        
        // 遍历显示所有资源
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for (int i = 0; i < allAssetList.Count; ++i)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField("", AssetDatabase.GetAssetPath(allAssetList[i]));
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150)))
            {
                Selection.activeObject = allAssetList[i];
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("开始重命名", GUILayout.Height(30)))
        {
            if (Replace1 == string.Empty || Replace2 == string.Empty)
            {
                ShowNotification(new GUIContent("替换文本不能为空，请输入一个有效的名称！"));
            }
            else
            {
                StartReplace();
            }
        }
    }

    private void StartReplace()
    {
          // 1:  更改文件名
        for (int i = 0; i < allAssetList.Count; ++i)
        {
            string oldPath = AssetDatabase.GetAssetPath(allAssetList[i]); // 原始文件位置  ： Assets/Art/Characters/Ailuen/Effects/Resources/Ailuen01_Attack01_Fly01.prefab
            //string namex = allAssetList[i].name;
            string newFileName = allAssetList[i].name.Replace(Replace1, Replace2); // 新的文件名称 
            Debug.Log(allAssetList[i].name + " : " + Replace1 + " : " + Replace2);
            Debug.Log(i + "文件位置：" + oldPath + " , 新名称：" + newFileName);
            AssetDatabase.RenameAsset(oldPath, newFileName);
        }
        AssetDatabase.Refresh();

        // 3:  提示是否继续
        if (EditorUtility.DisplayDialog("名称替换", "完成！", "关闭"))
        {
            this.Close();
        }
        else
        {
            this.Close();
        }
    }
}
