using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;

// 角色特效资源导出文本：
public class CharacterEffectRes
{
    public  static string[] folders = new string[] { "Assets/Art/Characters" };
    // 用来保存，角色名对应的动作数组
    public static Dictionary<string, List<string>> characterList = new Dictionary<string, List<string>>();

    [MenuItem("GameTool/Character Effect 统计角色特效Prefab名称！")]
    public static void characterEffectRes()
    {
        // 清空角色列表
        characterList.Clear();

        // 1:查找资源类型和资源目录！
        string[] allmat = AssetDatabase.FindAssets("t:Prefab", folders);
        // 2:遍历所有prefab
        for (int i = 0; i < allmat.Length; ++i)
        {
            // 3：获取资源路径path
            string path = AssetDatabase.GUIDToAssetPath(allmat[i]);
            //Debug.Log(" ====== " + i + " : " + path + " ===== ");
            string[] pathList = path.Split('/');
            // 4：只处理在Resources目录下的资源
            if (pathList[pathList.Length - 2] == "Resources")
            {
                // 5：取出 文件名，和角色名
                string fileName = pathList[pathList.Length - 1].Split('.')[0];
                string characterName = fileName.Split('_')[0];
                if(characterList.ContainsKey(characterName))
                {
                    characterList[characterName].Add(fileName);
                }
                else
                {
                    List<string> fileNameList = new List<string>();
                    fileNameList.Add(fileName);
                    characterList.Add(characterName, fileNameList);
                }
            }
        }
    
        //输出到窗口
        ResoultWin window = (ResoultWin)EditorWindow.GetWindowWithRect(typeof(ResoultWin)
                            ,new Rect(Screen.width / 2 - 400, Screen.height / 2, 1280, 960), 
                            true, "统计角色特效"); 
    }
}

public class ResoultWin : EditorWindow 
{
    private Vector2 scrollPos = Vector2.zero;

    void OnGUI()
    {
        EditorGUILayout.HelpBox("角色名对应的特效名称，使用“#”连接！搜索根目录：" + CharacterEffectRes.folders[0] + "。注意：只导出Resource目录下的Prefab！",
                                                     MessageType.Info);

        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(1280), GUILayout.Height(800));
        // 6: 统计输出出所有角色动作名称
        foreach (var res in CharacterEffectRes.characterList)
        {
            //Debug.Log(res.Key + " :  " + res.Value.Count);
            string endStr = string.Empty;
            int len = res.Value.Count;
            for (int i = 0; i < len; ++i)
            {
                endStr += res.Value[i];
                if (i != len - 1)
                {
                    endStr += "#";
                }
            }
            EditorGUILayout.TextField(res.Key, endStr);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

}
