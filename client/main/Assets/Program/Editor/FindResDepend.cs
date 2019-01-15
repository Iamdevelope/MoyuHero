using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

// 查找所有依赖此资源的地方
public class FindResDepend : EditorWindow
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
    // 当前选中的对象
    private Object m_selectedObj; 
    // 关闭界面标记
    private bool isClose = false;
    // 用来保存找到的引用Material
    private List<string> materialPathList = new List<string>();
    // 用来保存找到的引用Prefab
    private List<string> prefabPathList = new List<string>();
    //// 用来保存找到的所有引用资源包括脚本等等！
    private List<string> allDependPathList = new List<string>(); 
    // 用来保存所有找到的资源路径的数据
    private string[] allAssetList;
    // 滚动参数
    private Vector2 scrollPos = Vector2.zero;
    // 选中物体的路径
    private string selectedStrPath = string.Empty;
    // 搜索路径
    private string searchPath = "Assets"; //"Assets/Art/Characters";
    // 单例
    public static FindResDepend win;

    [MenuItem("Assets/查看所有关联资源")]
    public static void RenameAndReplace()
    {
        if(win == null)
        {
            win = (FindResDepend)EditorWindow.GetWindow(typeof(FindResDepend), true, "查看所有关联资源");
        }
    }

    void OnGUI()
    {
        EditorGUILayout.HelpBox("请确保当前场景已经保存,否则没保存的资源将无法被搜索到！", MessageType.Warning);
        switch (curWinState)
        {
            case WinState.InitState:
                OnInitState();
                break;
            case WinState.StartState:
                OnStartState();
                break;
        }
        if(GUILayout.Button("刷新", GUILayout.Height(40)))
        {
            curWinState = WinState.InitState;
        }
    }

    private void OnInitState()
    {
        
         // 1: 获取选中的对象
        if (m_selectedObj == null)
        {
            m_selectedObj = Selection.activeObject;
            selectedStrPath = AssetDatabase.GetAssetPath(m_selectedObj);
        }
        if (m_selectedObj == null)
            this.Close();
        // 2: 显示选中的对象的所有依赖项
        // 2.1 显示纹理依赖
        if (m_selectedObj.GetType() == typeof(Texture2D))
        {
            FindTextureDepend();
        }
        else if (m_selectedObj.GetType() == typeof(Material))
        {
            FindMaterialDepend();
        }
        else if(m_selectedObj.GetType() == typeof(GameObject))
        {
            FindGameObjectDepend();
        }
        else
        {
            if (EditorUtility.DisplayDialog("错误", "查看不了（" + m_selectedObj.GetType() + "）这种资源的依赖,如果需要请联系程序部！", "关闭"))
            {
                this.Close();
            }
            else
            {
                this.Close();
            }
        }
        // 更新win的Title标题
        win.title = "使用了 " + m_selectedObj.name + " 的所有资源";
        curWinState = WinState.StartState;

#region 确定是否保存了场景
        // 确定是否保存了场景
        if (!EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            ShowNotification(new GUIContent("请确保当前场景已经保存,否则没保存的资源将无法被搜索到！"));
        }
 #endregion
    }

    private void OnStartState()
    {
        // 1：先 显示搜索的资源
        EditorGUILayout.SelectableLabel("源文件：");
        GUILayout.BeginHorizontal();
        Object obj = AssetDatabase.LoadAssetAtPath(selectedStrPath, typeof(Object)) as Object;
        Texture2D texture = AssetPreview.GetAssetPreview(obj);
        Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
        if (texture != null)
            EditorGUI.DrawPreviewTexture(ret, texture);
        EditorGUILayout.TextField("", selectedStrPath, GUILayout.Height(30));
        if (GUILayout.Button("点击查看资源", GUILayout.Width(150), GUILayout.Height(30)))
        {
            Selection.activeObject = obj;
        }
        GUILayout.EndHorizontal();

        // 显示依赖的纹理Material
        if (materialPathList.Count > 0)
        {
            ShowTextureDepend();
        }
        else if (prefabPathList.Count > 0)
        {
            ShowMaterialDepend();
        }
        else if(allDependPathList.Count > 0)
        {
            ShowPrefabDepend();
        }
    }


    // 1.1 显示纹理依赖
    private void FindTextureDepend()
    {
        materialPathList.Clear();
        // 1：查找所有 Material 找到纹理被引用的Material
        allAssetList = AssetDatabase.FindAssets("t:Material", new string[] { searchPath });
        for (int i = 0; i < allAssetList.Length; ++i)
        {
            string materialPath = AssetDatabase.GUIDToAssetPath(allAssetList[i]);        // 遍历 Material 纹理
			// 显示进度条
            EditorUtility.DisplayProgressBar("查找所有用到\" " + m_selectedObj.name + " \" 的Material", materialPath, (float)i / (float)allAssetList.Length);
            string[] materialDependList = AssetDatabase.GetDependencies(new string[] { materialPath }); // 纹理依赖项 
            for (int k = 0; k < materialDependList.Length; ++k)
            {
               if (selectedStrPath.Equals(materialDependList[k]))
               {
                    materialPathList.Add(materialPath);
                }
            }
            if (i >= allAssetList.Length - 1)
            {
                EditorUtility.ClearProgressBar();
            }
        }
    }

    // 1.2 显示Material依赖的Prefab
    private void FindMaterialDepend()
    {
        prefabPathList.Clear();
        // 1：查找所有 Material 找到纹理被引用的Material
        allAssetList = AssetDatabase.FindAssets("t:Prefab", new string[] { searchPath });
        for (int i = 0; i < allAssetList.Length; ++i)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(allAssetList[i]);        // 遍历 Prefab 纹理
            EditorUtility.DisplayProgressBar("查找所有用到\" " + m_selectedObj.name + ".mat\" 的Prefab", prefabPath, (float)i / (float)allAssetList.Length);
            string[] prefabDependList = AssetDatabase.GetDependencies(new string[] { prefabPath }); // Prefab依赖项 
            for (int k = 0; k < prefabDependList.Length; ++k)
            {
                if (selectedStrPath.Equals(prefabDependList[k]))
                {
                    prefabPathList.Add(prefabPath);
                }
            }
            if (i >= allAssetList.Length - 1)
            {
                EditorUtility.ClearProgressBar();
            }
        }
    }

    // 1.3 显示FindGameObject依赖的所有资源
    private void FindGameObjectDepend()
    {
        allDependPathList.Clear();
        // 1: 获取所有依赖项
        allAssetList = AssetDatabase.GetDependencies(new string[] { selectedStrPath });
        for (int i = 0; i < allAssetList.Length; ++i)
        {
            allDependPathList.Add(allAssetList[i]);
        }
    }

    // 显示依赖的Material界面布局
    private void ShowTextureDepend()
    {
        // 1：显示 Material 列表
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        EditorGUILayout.SelectableLabel("使用的 Material ：");
        for (int i = 0; i < materialPathList.Count; ++i)
        {
            Material mt = AssetDatabase.LoadAssetAtPath(materialPathList[i], typeof(Material)) as Material;
            Texture2D texture = AssetPreview.GetAssetPreview(mt);
            GUILayout.BeginHorizontal();
            Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
            EditorGUI.DrawPreviewTexture(ret, texture);
            EditorGUILayout.TextField("", materialPathList[i], GUILayout.Height(30));
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150), GUILayout.Height(30)))
            {
                Selection.activeObject = mt;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    // 显示Material依赖的Prefab
    private void ShowMaterialDepend()
    {
        // 1: 显示搜索得到的资源
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        // 显示 Prefab 列表
        EditorGUILayout.SelectableLabel("使用的 Prefab ：");
        for (int i = 0; i < prefabPathList.Count; ++i)
        {
            GUILayout.BeginHorizontal();
            GameObject obj = AssetDatabase.LoadAssetAtPath(prefabPathList[i], typeof(GameObject)) as GameObject;
            Texture2D texture = AssetPreview.GetAssetPreview(obj);
            Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
            if (texture != null)
                EditorGUI.DrawPreviewTexture(ret, texture);
            EditorGUILayout.TextField("", prefabPathList[i], GUILayout.Height(30));
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150), GUILayout.Height(30)))
            {
                Selection.activeObject = obj;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    // 显示所有GameObject对象的引用资源
    private void ShowPrefabDepend()
    {
        // 1: 先将资源进行分类
        List<string> matList = new List<string>();
        List<string> textureList = new List<string>();
        List<string> others = new List<string>();
        for(int i = 0; i < allDependPathList.Count; ++i)
        {
           Object asset = AssetDatabase.LoadAssetAtPath(allDependPathList[i], typeof(Object)) as Object;
            if(asset.GetType() == typeof(Material))
            {
                matList.Add(allDependPathList[i]);
            }
            else if(asset.GetType() == typeof(Texture2D))
            {
                textureList.Add(allDependPathList[i]);
            }
            else
            {
                others.Add(allDependPathList[i]);
            }
        }

        // 2: 显示分类后的资源
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        // 显示 Material 列表
        EditorGUILayout.SelectableLabel("使用的 Material ：");
        for (int i = 0; i < matList.Count; ++i)
        {
            Material mt = AssetDatabase.LoadAssetAtPath(matList[i], typeof(Material)) as Material;
            Texture2D texture = AssetPreview.GetAssetPreview(mt);
            GUILayout.BeginHorizontal();
            Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
            EditorGUI.DrawPreviewTexture(ret, texture);
            EditorGUILayout.TextField("", matList[i], GUILayout.Height(30));
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150), GUILayout.Height(30)))
            {
                Selection.activeObject = mt;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
        // 显示 Texture
        EditorGUILayout.SelectableLabel("使用的 Texture ：");
        for (int i = 0; i < textureList.Count; ++i)
        {
            Texture2D texture = AssetDatabase.LoadAssetAtPath(textureList[i], typeof(Texture2D)) as Texture2D;
            GUILayout.BeginHorizontal();
            Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30),GUILayout.Width(30));
            EditorGUI.DrawPreviewTexture(ret,texture);
            EditorGUILayout.TextField("", textureList[i], GUILayout.Height(30));
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150), GUILayout.Height(30)))
            {
                Selection.activeObject = texture;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
        // 显示 Other
        EditorGUILayout.SelectableLabel("使用的 Other ：");
        for (int i = 0; i < others.Count; ++i)
        {
            Object obj = AssetDatabase.LoadAssetAtPath(others[i], typeof(Object)) as Object;
            Texture2D texture = AssetPreview.GetAssetPreview(obj);
            GUILayout.BeginHorizontal();
            Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
            if (texture != null)
            {
                EditorGUI.DrawPreviewTexture(ret, texture);
            }
            EditorGUILayout.TextField("", others[i], GUILayout.Height(30));
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150), GUILayout.Height(30)))
            {
                Selection.activeObject = obj;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }



    void OnProjectChange()
    {
        curWinState = WinState.InitState;
    }
}
