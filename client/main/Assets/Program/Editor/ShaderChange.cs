using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;

public class ShaderChange 
{
 
    [MenuItem("GameTool/Shader替换功能")]
    public static void changeShader()
    {
        //shaderChangeWin win = (shaderChangeWin)EditorWindow.GetWindow(typeof(shaderChangeWin),true,"Shader脚本转换");
        shaderChangeWin win = (shaderChangeWin)EditorWindow.GetWindowWithRect(typeof(shaderChangeWin),
                                                                                                                                            new Rect(Screen.width / 2 - 400,Screen.height / 2, 800, 600),
                                                                                                                                            true,
                                                                                                                                            "Shader脚本转换");
    }
}

// ====================================================================
// shaderChangeWin 
// ====================================================================
public class shaderChangeWin : EditorWindow
{
    private enum WinState
    {
        InitState,
        StartState,
        GoState,
        EndState, 
    }
    private WinState state = WinState.InitState;
    // 未处理列表
    private Dictionary<string, List<string>> EnchangedList = new Dictionary<string, List<string>>();
    // 已处理列表
    private Dictionary<string, List<string>> changedList = new Dictionary<string, List<string>>();
    // 所有shader列表
    private Dictionary<string, List<string>> allList = new Dictionary<string, List<string>>(); 
    // Shader转换对应关系表
    private Dictionary<string, string> shaderMap = new Dictionary<string, string>();
    private string[] folders = new string[] { "Assets/Art/Characters" };
    private string[] allmat;

    private Vector2 scrollPos = Vector2.zero;
    private bool isShowEnchangedList;
    private int selGridInt = 0;
    private string [] selStrings =  new string[] {"未处理的Shader","已经处理的Shader" };
    void OnGUI()
    {
        switch (state)
        {
            case WinState.InitState:
                Init();
                break;
            case WinState.StartState:
                Start();
                break;
            case WinState.GoState:
                Go();
                break;
            case WinState.EndState:
                End();
                break;
        }
    }



    // ================== 私有接口 =========================
    //初始化
    private void Init()
    {
        allmat = AssetDatabase.FindAssets("t:Material", folders);//查找资源类型和资源目录！
        shaderMap.Add("Particles/Additive", "Mobile/Particles/Additive");
        shaderMap.Add("Particles/Additive (Soft)", "Mobile/Particles/Additive");
        shaderMap.Add("Particles/Additive_Layer", "Mobile/Particles/Additive");
        shaderMap.Add("Particles/Alpha Blended", "Mobile/Particles/Alpha Blended");
        shaderMap.Add("Particles/Alpha Blended Premultiply", "Mobile/Particles/Alpha Blended");
        shaderMap.Add("Particles/Multiply", "Mobile/Particles/Alpha Blended");
        shaderMap.Add("Particles/Multiply (Double)", "Mobile/Particles/Alpha Blended");
        shaderMap.Add("Particles/VertexLit Blended", "Mobile/Particles/VertexLit Blended");
        string matFilePath = string.Empty;
        for (int i = 0; i < allmat.Length; ++i)
        {
            matFilePath = AssetDatabase.GUIDToAssetPath(allmat[i]); // 将GUID转换成路径
            Material mat = AssetDatabase.LoadAssetAtPath(matFilePath, typeof(Material)) as Material;//加载资源
            if (allList.ContainsKey(mat.shader.name))
            {
                allList[mat.shader.name].Add(matFilePath);
            }
            else
            {
                List<string> pathList = new List<string>();
                pathList.Add(matFilePath);
                allList.Add(mat.shader.name, pathList);
            }
        }
        state = WinState.StartState;
    }

    // 开始状态
    private void Start()
    {
        EditorGUILayout.HelpBox("所有的Shader如下，共计" + allmat.Length+ "个，根目录：" + folders[0], MessageType.Info);
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(800), GUILayout.Height(500));
        foreach (var item in allList)
        {
            EditorGUILayout.SelectableLabel("Shader名称：" + item.Key);
            for (int i = 0; i < item.Value.Count; ++i)
            {
                EditorGUILayout.TextField("", item.Key +"  ==>> "+item.Value[i]);
            }
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        if(GUILayout.Button("开始转换"))
        {
            state = WinState.GoState;
        }
    }

    private void Go()
    {
        string matFilePath = string.Empty;
        int num = 0;
        Debug.Log("开始转换：累计搜索可用 Material " + allmat.Length + "个！");
        for (int i = 0; i < allmat.Length; ++i)
        {
            matFilePath = AssetDatabase.GUIDToAssetPath(allmat[i]); // 将GUID转换成路径
            Material mat = AssetDatabase.LoadAssetAtPath(matFilePath, typeof(Material)) as Material;//加载资源
            bool ischanged = false;
            if (shaderMap.ContainsKey(mat.shader.name))
            {
                string newShader = shaderMap[mat.shader.name];
                Debug.Log("开始替换：" + mat.name + " ( " + mat.shader.name + " -->> " + newShader + " )");
                // 转换前保存
                if (changedList.ContainsKey(mat.shader.name))
                {
                    changedList[mat.shader.name].Add(matFilePath);
                }
                else
                {
                    List<string> pathList = new List<string>();
                    pathList.Add(matFilePath);
                    changedList.Add(mat.shader.name, pathList);
                }
                // 开始转换
                mat.shader = Shader.Find(newShader);
                num++;
                ischanged = true;
                float progress = (float)i / (float)allmat.Length;
                EditorUtility.DisplayProgressBar("Shader替换进度", matFilePath, progress); // 显示进度
            }

            bool isChanged = false;
            if (!ischanged)
            {
                foreach (var shaderMapInfo in shaderMap)
                {
                    if (shaderMapInfo.Value.Equals(mat.shader.name))
                    {
                        isChanged = true;
                        break; // 一定要Break
                    }
                    else
                    {
                        isChanged = false;
                    }
                }
                if (!isChanged)
                {
                    Debug.Log("未进行转换的 Shader ：<<" + mat.shader.name + ">>  :  " + matFilePath);
                    if (EnchangedList.ContainsKey(mat.shader.name))
                    {
                        EnchangedList[mat.shader.name].Add(matFilePath);
                    }
                    else
                    {
                        List<string> pathList = new List<string>();
                        pathList.Add(matFilePath);
                        EnchangedList.Add(mat.shader.name, pathList);
                    }
                }
            }
        }
        Debug.Log("所有Shader转换结束，共计转换" + num + "个");
        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
        state = WinState.EndState;
    }

    // 结束状态
    private void End()
    {
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1);
        if (selGridInt == 0)
        {
            ShowUnChangedShader();
        }
        else
        {
            ShowChangedShader();
        }
    }

    // 显示未处理的shader
    private void ShowUnChangedShader()
    {
        EditorGUILayout.HelpBox("未处理的Shader如下，共计" + EnchangedList .Count+ "个，根目录：" + folders[0], MessageType.Info);
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(800), GUILayout.Height(500));
        foreach (var item in EnchangedList)
        {
            EditorGUILayout.SelectableLabel("Shader名称：" + item.Key);
            for (int i = 0; i < item.Value.Count; ++i)
            {
                EditorGUILayout.TextField("", item.Value[i]);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
    // 显示已处理的shader
    private void ShowChangedShader()
    {
        EditorGUILayout.HelpBox("已经处理的Shader如下，共计" + changedList.Count + "个，根目录：" + folders[0], MessageType.Info);
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(800), GUILayout.Height(500));
        foreach (var item in changedList)
        {
            EditorGUILayout.SelectableLabel("Shader 替换：" + item.Key + " >>> " + shaderMap[item.Key]);
            for (int i = 0; i < item.Value.Count; ++i)
            {
                EditorGUILayout.TextField("", item.Value[i]);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

}