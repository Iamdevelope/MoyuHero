using UnityEngine;
using UnityEditor;
using System.Collections;

public class ClearParticleShadow 
{

    [MenuItem("GameTool/关闭Particle的castShadows和receiveShadows属性")]
    public static void ClearShadow()
    {
        // 1:查找资源类型和资源目录！
        string[] allmat = AssetDatabase.FindAssets("t:Prefab");
        GameObject obj = null;
        if(EditorUtility.DisplayDialog("提示：","你确定要将当前项目中所有Particle的castShadows和receiveShadows属性关掉？","开始","取消"))
        {
            // 2:遍历所有prefab
            for (int i = 0; i < allmat.Length; ++i)
            {
                // 3：获取资源路径path
                string path = AssetDatabase.GUIDToAssetPath(allmat[i]);
                EditorUtility.DisplayProgressBar("关闭阴影属性中！", path, (float)i / (float)allmat.Length);
                if (i >= allmat.Length-1)
                {
                    EditorUtility.ClearProgressBar();
                }
                obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                ResetParticleShadow(obj.transform);
            }
        }
        EditorUtility.DisplayDialog("特效阴影关闭操作", "完成！", "完成");
    }


    // 遍历查找Particle
    private static void ResetParticleShadow(Transform trans)
    {
        ParticleRenderer prender = trans.GetComponent<ParticleRenderer>();
        if (trans.particleSystem != null)
        {
            trans.particleSystem.renderer.castShadows = false;
            trans.particleSystem.renderer.receiveShadows = false;
        }
        else if (prender != null)
        {
            prender.receiveShadows = false;
            prender.castShadows = false;
        }
        else
        {
            for (int i = 0; i < trans.childCount; ++i)
            {
                ResetParticleShadow(trans.GetChild(i));
            }
        }
    }
}