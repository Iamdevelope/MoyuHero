using UnityEngine;
using UnityEditor;
using System.Collections;

public class CheckCharacterShader : EditorWindow
{
    /// <summary>
    /// 检测Character材质是否丢失shader "DreamFaction/Characters/Characters" 
    /// 丢失后重新指认
    /// 请把脚本拷贝到Editor文件件下右键执行
    /// </summary>
    static CheckCharacterShader listWindow;
    private string checkPath = "Assets/Art/Characters";

    [MenuItem("Assets/CheckCharacterShader")]

	// Use this for initialization
	static void Start () {
        listWindow = (CheckCharacterShader)EditorWindow.GetWindow(typeof(CheckCharacterShader), true, "角色空shader指定");
        listWindow.minSize = new Vector2(600,200);
        listWindow.Show();	
	}

    void OnGUI()
    {
        if (GUILayout.Button("角色空shader指定", GUILayout.Height(40)))
        {
            AssignNullShader();
        }
    }

    void AssignNullShader()
    {
        string[] PathList = AssetDatabase.FindAssets("t:Material", new string[] { checkPath });

        for (int i = 0; i < PathList.Length; ++i)
        {            
            string matPath = AssetDatabase.GUIDToAssetPath(PathList[i]);

            if (matPath.Contains("Models") && !matPath.Contains("Effects"))
            {
                Material material = AssetDatabase.LoadAssetAtPath(matPath, typeof(Material)) as Material;

                //if (material.shader == Shader.Find("Hidden/InternalErrorShader"))
                {
                    material.shader = Shader.Find("DreamFaction/Characters/CharactersV3");
                    int id = Shader.PropertyToID("_Value");
                    material.SetFloat(id, 0.2f);
                }
             
            }
        }

        AssetDatabase.Refresh();
        this.ShowNotification(new GUIContent("Project已更新!"));
    }
}
