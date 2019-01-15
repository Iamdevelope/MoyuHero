using UnityEngine;
using UnityEditor;
using System.Collections;
using DreamFaction.Utils;

public class PrograsBarWin : EditorWindow
{
    // 需要遍历的路径, 注意：战斗时创建的动态特效需要动态单独绑定脚本，不在这里处理！因为特效资源打包的时候不能挂脚本！
    public static string[] folders = new string[] { "Assets/Art" };
    public  static int curNum = 0;
    public static int maxNum = 100;
    public  static string[] allmat;
    public static string path = string.Empty;

    [MenuItem("GameTool/Particle 添加画质高低优化用的控制脚本！（再次点击删除脚本）")]
    public static void Init()
    {
        // 1:查找资源类型和资源目录！
        allmat = AssetDatabase.FindAssets("t:Prefab", folders);
        int len = allmat.Length;
        maxNum = allmat.Length;
        curNum = 0;
        path = string.Empty;
        // 1.1 显示进度条界面：
        //PrograsBarWin win = (PrograsBarWin)EditorWindow.GetWindow(typeof(PrograsBarWin));
        PrograsBarWin win = (PrograsBarWin)EditorWindow.GetWindowWithRect(typeof(PrograsBarWin), new Rect(Screen.width / 2 - 300, Screen.height / 2, 600, 100), true, "脚本添加中");
    }


    void Update()
    {
        if (curNum < maxNum)
        {
            // 3：获取资源路径path
            path = AssetDatabase.GUIDToAssetPath(allmat[curNum]);
            //Debug.Log(" ====== " + curNum + " : " + path + " ===== ");
            // 4：加载Prefab资源
            GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
            // 5：实例化Prefab
            GameObject instObj = PrefabUtility.InstantiatePrefab(obj) as GameObject;
            // 6：重命名Prefab，去掉 (clone)后缀
            instObj.name = instObj.name.Split('(')[0];
            // 7：获取实例化的prefab的Transform
            Transform trans = instObj.GetComponent<Transform>();
            // 8：遍历Transform查找所有子节点，并找到ParticleSystem然后添加脚本
            GameUtils.AttachParticleCS(trans);
            // 9：重新覆盖Prefab，应用修改！
            PrefabUtility.ReplacePrefab(instObj, obj);
            // 10：删除实例化出来的Prefab对象
            Object.DestroyImmediate(instObj);
            curNum++;
            this.Repaint();
        }
        if (curNum == maxNum)
        {
            this.Close();
        }
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField(path);
        Rect rec = GUILayoutUtility.GetRect(50, 50, "TextField");//new Rect(0,0,600,50)
        EditorGUI.ProgressBar(rec, (float)curNum / (float)maxNum , "   当前进度: " + (int)((float)curNum / (float)maxNum * 100) + "%");
        //GUILayout.Button("取消");
    }
}

