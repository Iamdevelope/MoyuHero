using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class FindMulTextureMaterial : EditorWindow
{
	// 界面状态
	private enum WinState
	{
		InitState,
		StartState,
		FinishState,
	}

	// 当前界面状态
	private WinState m_CurWinState = WinState.InitState;
	// 用来保存找到的引用Material
	private List<string> m_MaterialPathList = new List<string>();
	// 用来保存所有找到的资源路径的数据
	private string[] m_AllAssetList;
	// 滚动参数
	private Vector2 scrollPos = Vector2.zero;

	// 搜索路径
	private string m_SearchPath = "Assets/Art/Characters";
	// 单例
	public static FindMulTextureMaterial m_Win;

	[MenuItem("Assets/查找所有有多个 Texture 的 Matrial ")]
	public static void RenameAndReplace()
	{
		if (m_Win == null)
		{
			m_Win = (FindMulTextureMaterial)EditorWindow.GetWindow(typeof(FindMulTextureMaterial), true, "查找所有有多个 Texture 的 Matrial ");
			m_Win.minSize = new Vector2(800, 480);
		}
	}

	void OnGUI()
	{
		EditorGUILayout.HelpBox("请确保当前场景已经保存,否则没保存的资源将无法被搜索到！", MessageType.Warning);
		switch (m_CurWinState)
		{
			case WinState.InitState:
				OnInitState();
				break;
			case WinState.StartState:
				OnStartState();
				break;
			default:
				break;
		}

		if (GUILayout.Button("刷新", GUILayout.Height(40)))
		{
			m_CurWinState = WinState.InitState;
		}
	}

	private void OnInitState()
	{
		m_MaterialPathList.Clear();
		FindTextureDepend();

		m_Win.title = "查找所有有多个 Texture 的 Matrial";
		m_CurWinState = WinState.StartState;

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
		// 显示依赖的纹理Material
		if (m_MaterialPathList.Count > 0)
		{
			ShowTextureDepend();
		}
	}

	// 1.1 显示纹理依赖
	private void FindTextureDepend()
	{
		// 1：查找所有 Material 找到纹理被引用的Material
		m_AllAssetList = AssetDatabase.FindAssets("t:Material", new string[] { m_SearchPath });

		for (int i = 0; i < m_AllAssetList.Length; ++i)
		{
			string materialPath = AssetDatabase.GUIDToAssetPath(m_AllAssetList[i]);        // 遍历 Material 纹理
			// 显示进度条
			EditorUtility.DisplayProgressBar("查找所有用到\" " + " \" 的Material", materialPath, (float)i / (float)m_AllAssetList.Length);

			Material material = AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material)) as Material;

			string[] materialDependList = AssetDatabase.GetDependencies(new string[] { materialPath }); // 纹理依赖项 
			Debug.Log(materialPath + "  " + materialDependList.Length.ToString());
			for (int k = 0; k < materialDependList.Length; ++k)
			{
				if (materialDependList.Length > 2)
				{
					m_MaterialPathList.Add(materialPath);
				}
			}
			if (i >= m_AllAssetList.Length - 1)
			{
				EditorUtility.ClearProgressBar();
			}
		}
	}
	// 显示依赖的Material界面布局
	private void ShowTextureDepend()
	{
		// 1：显示 Material 列表
		EditorGUILayout.BeginVertical();
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		EditorGUILayout.SelectableLabel("使用的 Material ：");

		foreach (var item in m_MaterialPathList)
		{
			Material mt = AssetDatabase.LoadAssetAtPath(item, typeof(Material)) as Material;
			Texture2D texture = AssetPreview.GetAssetPreview(mt);
			GUILayout.BeginHorizontal();
			Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
			if (texture != null)
				EditorGUI.DrawPreviewTexture(ret, texture);
			EditorGUILayout.TextField("", item, GUILayout.Height(30));
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
}
