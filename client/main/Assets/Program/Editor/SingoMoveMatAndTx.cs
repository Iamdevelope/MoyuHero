using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public class SingoMoveMatAndTx : EditorWindow
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
    private GameObject targetPrefab;
    // 选中对象的路径
    private string targetPrefabPath;
    // 依赖项数组
    private string[] dependObjs;
    // 依赖材质数组
    private List<string> dependMaterialPath = new List<string>();
    // 依赖的纹理数组
    private List<string> dependTexture2DPath = new List<string>();
    // 脚本数组
    private List<string> dependScriptPath = new List<string>();
    // 其他类型引用数组
    private List<string> dependOthersPath = new List<string>();
    // 已存在文件列表
    Dictionary<string, string> existList = new Dictionary<string, string>(); // 已经存在于目标位置的文件列表！
    // 滚动参数
    private Vector2 scrollPos = Vector2.zero;
    // 路径选择窗口，默认的路径位置
    private string openPath = string.Empty;

    [MenuItem("Assets/保存单个特效Prefab的材质和纹理到指定目录")]
    private static void SingoMove()
    {
        // 打开Win窗口
        SingoMoveMatAndTx win = (SingoMoveMatAndTx)EditorWindow.GetWindow(typeof(SingoMoveMatAndTx), true, "移动特效Prefab材质球和纹理的位置");
    }
    // ====================== Win 状态 ================================
    private void OnGUI()
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
        // 1:  获取当前选中的物体
        UnityEngine.Object targetObj = null ;
        if (targetPrefab == null)
            targetObj = Selection.activeObject;
        else
            targetObj = targetPrefab;
        // 2：判断当前物体是否是有效的Prefab
        if (targetObj == null || targetObj.GetType() != typeof(GameObject))
        {
            OpenErrorAndCloseWin(1);
            return;
        }
        targetPrefab = targetObj as GameObject;
        targetPrefabPath = AssetDatabase.GetAssetPath(targetPrefab);
        // 进行有效性判断，目录要有Characters，并且要有至少一个Particle特效
        if (targetPrefabPath.IndexOf("Characters") == -1 || !FindParticle(targetPrefab.transform))
        {
            OpenErrorAndCloseWin(2);
        }
        // 计算 开启窗口默认路径位置
        string[] splitPath = targetPrefabPath.Split('/');  // 拆分路径
        for (int i = 1; i < splitPath.Length - 2; ++i)
        {
            openPath += splitPath[i];
            if (i != splitPath.Length - 3)
            {
                openPath += "/";
            }
        }
        openPath = Application.dataPath + "/" + openPath;

        // 3: 获取所有依赖项
        dependObjs = AssetDatabase.GetDependencies(new string[] { targetPrefabPath });
        // 4: 将依赖项按类型进行区分，材质，纹理，脚本，等等！
        for (int i = 0; i < dependObjs.Length; ++i)
        {
            splitPath = dependObjs[i].Split('/');  // 拆分路径
            string exName = splitPath[splitPath.Length - 1].Split('.')[1];  // 获得文件扩展名
            exName = exName.ToLower(); // 扩展名强制转换成小写

            if (exName == "tga" || exName == "png" ||
                exName == "dds" || exName == "jpg" ||
                exName == "bmp" || exName == "gif" ||
                exName == "iff" || exName == "tiff" || exName == "pict")
            {
                // 添加图片资源
                dependTexture2DPath.Add(dependObjs[i]);
            }
            else if (exName == "mat")
            {
                // 添加材质资源
                dependMaterialPath.Add(dependObjs[i]);
            }
            else if (exName == "cs")
            {
                // 添加脚本资源
                dependScriptPath.Add(dependObjs[i]);
            }
            else
            {
                // 添加其他资源
                dependOthersPath.Add(dependObjs[i]);
            }
        }
        // 4: 切换显示状态
        curWinState = WinState.StartState;
    }

    // 窗口开始界面
    private void OnStartState()
    {
        EditorGUILayout.HelpBox(targetPrefabPath + " 所使用的内容如下：", MessageType.Info, true);
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        // 显示 Material 列表
        EditorGUILayout.SelectableLabel("使用的 Material ：");
        for (int i = 0; i < dependMaterialPath.Count; ++i)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField("", dependMaterialPath[i]);
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150)))
            {
                Selection.activeObject = AssetDatabase.LoadAssetAtPath(dependMaterialPath[i], typeof(Material)) as Material;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();

        // 显示 Texture 列表
        EditorGUILayout.SelectableLabel("使用的 Texture2D ：");
        for (int i = 0; i < dependTexture2DPath.Count; ++i)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField("", dependTexture2DPath[i]);
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150)))
            {
                Selection.activeObject = AssetDatabase.LoadAssetAtPath(dependTexture2DPath[i], typeof(Texture2D)) as Texture2D;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();

        // 显示 其他引用资源 列表
        EditorGUILayout.SelectableLabel("使用的其他资源  ：");
        for (int i = 0; i < dependOthersPath.Count; ++i)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField("", dependOthersPath[i]);
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150)))
            {
                Selection.activeObject = AssetDatabase.LoadAssetAtPath(dependOthersPath[i], typeof(GameObject)) as GameObject;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();

        // 显示 脚本资源引用 列表
        EditorGUILayout.SelectableLabel("使用的脚本文件  ：");
        for (int i = 0; i < dependScriptPath.Count; ++i)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField("", dependScriptPath[i]);
            if (GUILayout.Button("点击查看资源", GUILayout.Width(150)))
            {
                Selection.activeObject = AssetDatabase.LoadAssetAtPath(dependOthersPath[i], typeof(MonoScript)) as MonoScript;
            }
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("将所有Texture2D和Material移动到指定位置！", GUILayout.Height(30)))
        {
            string path = EditorUtility.SaveFolderPanel("将 " + targetPrefab.name + " 使用到的纹理和材质球保存到指定位置", openPath, "Materials");
            path = ResetPathToUnity(path);
            //Debug.Log("path = " + path);
            // 移动所有Texture2D到目标目录下  例如：From = "Assets/Art/Cube.prefab";  To = "Assets/Art/Cube.prefab";
            MoveAllResToPath(path, dependMaterialPath, typeof(Material));
            MoveAllResToPath(path, dependTexture2DPath, typeof(Texture2D));    
            DisPlayExistList(); // 显示冲突文件是否需要覆盖的提示！
            AssetDatabase.Refresh();  // 刷新资源库
            if(EditorUtility.DisplayDialog("OK", "文件移动完毕！", "完成"))
            {
                ResetUIState();
            }
        }
    }

    // 错误提示，关闭窗口
    private void OpenErrorAndCloseWin(int index)
    {
        Debug.Log(index);
        if (EditorUtility.DisplayDialog("打开的姿势不对哦！！！！" + index, " 本功能只能针对 Assets/Art/Characters 目录下的特效Prefab对象操作！", "关闭"))
        {
            this.Close();
        }
    }
    // 遍历查找Particle
    private bool FindParticle(Transform trans)
    {
        Debug.Log("FindParticle : " + trans.name);
        if (trans.particleSystem != null || trans.particleEmitter != null)
        {
            return true;
        }
        else
        {
            for (int i = 0; i < trans.childCount; ++i)
            {
                if (FindParticle(trans.GetChild(i)))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // 将系统路径转换到Unity路径
    private string ResetPathToUnity(string path)
    {
        string[] splitPath = path.Split('/');
        int index = path.IndexOf("Assets");
        path = path.Substring(index);
        return path;
    }

    // 移动资源到目标目录下
    private void MoveAllResToPath(string targetPath,List<string> list, System.Type tt)
    {
        int curIndex = 0;
        int maxIndex = list.Count;
        // 1:检查文件是否已经存在，或者目标位置跟当前位置是否相等！
        for (int i = 0; i < list.Count; ++i)
        {
            curIndex++;
            string fromPath = list[i]; //起始位置
            string[] splitPath = fromPath.Split('/');  // 拆分路径
            string fileName = splitPath[splitPath.Length - 1]; // 文件名称
            string toPath = targetPath + "/" + fileName; // 目标位置完整路径
            // 是否相等，或者已经存在
            // 起始位置和结束位置相同，跳过，并继续
            if (toPath == list[i])
            {
                Debug.Log("文件目录相同，跳过移动文件的操作: " + toPath);
                continue;
            }
            // 判断文件是否已经存在！
            if (AssetDatabase.LoadAssetAtPath(toPath, tt) != null)
            {
                Debug.Log("文件已经存在，添加到待处理列表中：" + toPath);
                // 添加到 待覆盖列表
                existList.Add(fromPath, toPath);
            }
            else
            {
                // 移动文件到目标位置
                //Debug.Log("移动文件到目标位置 : " + fromPath + ">>>>" + toPath);
                //FileUtil.MoveFileOrDirectory(fromPath, toPath); // 用FileUtil会丢失文件之间在Unity中的索引关系！
                EditorUtility.DisplayProgressBar("移动文件", fromPath, (float)curIndex / (float)maxIndex);
                ReplaceAndDelete(fromPath, toPath,false);
            }
            if (curIndex >= maxIndex)
            {
                EditorUtility.ClearProgressBar();
            }
        } 
    }



    // 移动所有Material到目标目录下
    private void MoveAllMaterial(string targetPath)
    {
        // 1:检查文件是否已经存在，或者目标位置跟当前位置是否相等！
        for (int i = 0; i < dependMaterialPath.Count; ++i)
        {
            string fromPath = dependMaterialPath[i]; //起始位置
            string[] splitPath = fromPath.Split('/');  // 拆分路径
            string fileName = splitPath[splitPath.Length - 1]; // 文件名称
            string toPath = targetPath + "/" + fileName; // 目标位置完整路径
            // 是否相等，或者已经存在
            // 起始位置和结束位置相同，跳过，并继续
            if (toPath == dependMaterialPath[i])
            {
                Debug.Log("文件目录相同，跳过移动文件的操作: " + toPath);
                continue;
            }
            // 判断文件是否已经存在！
            if (AssetDatabase.LoadAssetAtPath(toPath, typeof(Material)) != null)
            {
                Debug.Log("文件已经存在，添加到待处理列表中：" + toPath);
                // 添加到 待覆盖列表
                existList.Add(fromPath, toPath);
            }
            else
            {
                // 移动文件到目标位置
                AssetDatabase.MoveAsset(fromPath, toPath);// 用FileUtil会丢失文件之间在Unity中的索引关系！
            }
        } 
    }

    // 移动所有Texture2D到目标目录下
    private void MoveAllTexture2D(string targetPath)
    {
        // 1:检查文件是否已经存在，或者目标位置跟当前位置是否相等！

    }

    //显示已经存在的文件列表覆盖提示
    private void DisPlayExistList()
    {
        bool isReplace = false;
        int curindex = 0; 
        int maxIndex = existList.Count;
        // 遍历待覆盖列表，获取用户操作，是否覆盖！
        foreach (var item in existList)
        {
            curindex++;
            if(isReplace)
            {
                EditorUtility.DisplayProgressBar("移动文件",item.Key,(float)curindex / (float)maxIndex);
                if (curindex >= maxIndex)
                {
                    EditorUtility.ClearProgressBar();
                }
                ReplaceAndDelete(item.Key,item.Value,true);
                continue;
            }
            int index = EditorUtility.DisplayDialogComplex("文件已经存在", item.Value, "覆盖", "跳过", "全部覆盖");
            if (index == 0)
            {
                // 覆盖
                Debug.Log("覆盖");
                ReplaceAndDelete(item.Key, item.Value,true);
            }
            else if (index == 1)
            {
                // 跳过
                Debug.Log("跳过");
                continue;
            }
            else if (index == 2)
            {
                // 之后文件全部覆盖
                Debug.Log("全部覆盖");
                ReplaceAndDelete(item.Key, item.Value,true);
                isReplace = true;
            }
        }
    } // End of :  private void DisPlayExistList()

    // 覆盖并删除原始图！
    private void ReplaceAndDelete(string from, string to,bool isDelete)
    {
        if (isDelete)
        {
            AssetDatabase.DeleteAsset(to); // 用FileUtil.DeleteFileOrDirectory(from); 会丢失文件之间在Unity中的索引关系！
        }
        Debug.Log("移动文件: " + from + ">>>>" + to);
        AssetDatabase.MoveAsset(from, to);// 用FileUtil.ReplaceFile(from, to);会丢失文件之间在Unity中的索引关系！ 
    }

    // 刷新界面，
    private void ResetUIState()
    {
        dependMaterialPath.Clear();
        dependTexture2DPath.Clear();
        dependScriptPath.Clear();
        dependOthersPath.Clear();
        existList.Clear();
        curWinState = WinState.InitState;
    }
}
