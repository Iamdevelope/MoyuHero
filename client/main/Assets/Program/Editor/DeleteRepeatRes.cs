using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 删除所有的重复资源
/// 现在还有一个技术问题没有解决
/// 当一个 Matrial 中含有多个 Texture 的时候，只能够替换第一个 Texture，即 Matrial 的 mainTexture
/// </summary>
public class DeleteRepeatRes : EditorWindow
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
	// 当前选中的对象
	private Object[] m_SelectedObjs;
	// 用来保存找到的引用Material
	private List<string> m_MaterialPathList = new List<string>();
	// 具有多个 Texture 的列表
	private List<string> m_MultipleTextureMaterial = new List<string>();

	private List<string> m_NameID = new List<string>();   // 

	// 用来保存找到的引用Prefab
	private List<string> m_PrefabPathList = new List<string>();
	//// 用来保存找到的所有引用资源包括脚本等等！
	private List<string> m_AllDependPathList = new List<string>();
	// 用来保存所有找到的资源路径的数据
	private string[] m_AllAssetList;
	// 滚动参数
	private Vector2 scrollPos = Vector2.zero;
	// 选中物体的路径
	List<string> m_SelectedStrPath = new List<string>();

	// 搜索路径
	private string m_SearchPath = "Assets/Art/Characters";
	// 单例
	public static DeleteRepeatRes m_Win;

	[MenuItem("Assets/合并重复贴图")]
	public static void RenameAndReplace()
	{
		if (m_Win == null)
		{
			m_Win = (DeleteRepeatRes)EditorWindow.GetWindow(typeof(DeleteRepeatRes), true, "合并重复贴图");
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

				if (GUILayout.Button("合并重复贴图", GUILayout.Height(40)))
				{
					if (m_MaterialPathList.Count > 0)
					{
						for (int i = 0; i < m_SelectedStrPath.Count; ++i)
						{
							if (m_SelectedStrPath[i].Contains(".png"))
							{
								Object obj = AssetDatabase.LoadAssetAtPath(m_SelectedStrPath[i], typeof(Object)) as Object;
								Selection.activeObject = obj;

								string message = "所有的材质都将引用这个纹理：" + m_SelectedStrPath[i] + " ，将删除其他纹理";
								if (EditorUtility.DisplayDialog("删除重复资源", message, "确定", "取消"))
								{
									DeleteRes(m_SelectedStrPath[i]);
									Debug.Log("EditorUtility.DisplayDialog");
									break;
								}
								else
								{
									break;
								}
							}
						}
					}
				}
				m_CurWinState = WinState.StartState;
				break;
			case WinState.FinishState:
				{

				}
				break;
		}
		if (GUILayout.Button("刷新", GUILayout.Height(40)))
		{
			m_CurWinState = WinState.InitState;
		}
	}

	private void OnInitState()
	{
		// 1: 获取选中的对象
		if (m_SelectedObjs == null)
		{
			m_SelectedObjs = Selection.objects;
			//selectedStrPath = AssetDatabase.GetAssetPath(m_SelectedObjs);

			for (int i = 0; i < m_SelectedObjs.Length; ++i)
			{
				m_SelectedStrPath.Add(AssetDatabase.GetAssetPath(m_SelectedObjs[i]));
			}
		}
		if (m_SelectedObjs == null)
			this.Close();

		// 2: 显示选中的对象的所有依赖项
		// 2.1 显示纹理依赖

		m_MaterialPathList.Clear();
		m_MultipleTextureMaterial.Clear();
		m_PrefabPathList.Clear();
		m_AllDependPathList.Clear();
		m_NameID.Clear();

		for (int i = 0; i < m_SelectedObjs.Length; ++i)
		{
			if (m_SelectedObjs[i].GetType() == typeof(Texture2D))
			{
				FindTextureDepend(m_SelectedStrPath[i]);
			}
			else if (m_SelectedObjs[i].GetType() == typeof(Material))
			{
				//FindMaterialDepend(m_SelectedStrPath[i]);
			}
			else if (m_SelectedObjs[i].GetType() == typeof(GameObject))
			{
				//FindGameObjectDepend(m_SelectedStrPath[i]);
			}
			else
			{
				if (EditorUtility.DisplayDialog("错误", "查看不了（" + m_SelectedObjs[i].GetType() + "）这种资源的依赖,如果需要请联系程序部！", "关闭"))
				{
					this.Close();
				}
				else
				{
					this.Close();
				}
			}
		}

		// 更新m_Win的Title标题
		string _title = "使用了";
		for (int i = 0; i < m_SelectedObjs.Length; ++i)
		{
			title += m_SelectedObjs[i].name;
		}
		_title += " 的所有资源";
		m_Win.title = _title;
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
		// 1：先 显示搜索的资源
		GUILayout.BeginVertical();
		EditorGUILayout.SelectableLabel("源文件：");
		for (int i = 0; i < m_SelectedStrPath.Count; ++i)
		{
			GUILayout.BeginHorizontal();
			Object obj = AssetDatabase.LoadAssetAtPath(m_SelectedStrPath[i], typeof(Object)) as Object;
			Texture2D texture = AssetPreview.GetAssetPreview(obj);
			Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
			if (texture != null)
				EditorGUI.DrawPreviewTexture(ret, texture);
			EditorGUILayout.TextField("", m_SelectedStrPath[i], GUILayout.Height(30));
			if (GUILayout.Button("点击查看资源", GUILayout.Width(150), GUILayout.Height(30)))
			{
				Selection.activeObject = obj;
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndVertical();

		// 显示依赖的纹理Material
		if (m_MaterialPathList.Count > 0)
		{
			ShowTextureDepend();
		}
		else if (m_PrefabPathList.Count > 0)
		{
			ShowMaterialDepend();
		}
		else if (m_AllDependPathList.Count > 0)
		{
			ShowPrefabDepend();
		}
	}

	// 1.1 显示纹理依赖
	private void FindTextureDepend(string selectedStrPath)
	{
		// 1：查找所有 Material 找到纹理被引用的Material
		m_AllAssetList = AssetDatabase.FindAssets("t:Material", new string[] { m_SearchPath });
		Texture2D texture = AssetDatabase.LoadAssetAtPath(selectedStrPath, typeof(Texture2D)) as Texture2D;

		for (int i = 0; i < m_AllAssetList.Length; ++i)
		{
			string materialPath = AssetDatabase.GUIDToAssetPath(m_AllAssetList[i]);        // 遍历 Material 纹理
			// 显示进度条
			EditorUtility.DisplayProgressBar("查找所有用到\" " + selectedStrPath + " \" 的Material", materialPath, (float)i / (float)m_AllAssetList.Length);

			Material material = AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material)) as Material;
			if (material.mainTexture == texture)
			{
				m_MaterialPathList.Add(materialPath);
			}

			//string[] materialDependList = AssetDatabase.GetDependencies(new string[] { materialPath }); // 纹理依赖项 
			//Debug.Log(materialPath + "  " + materialDependList.Length.ToString());
			//for (int k = 0; k < materialDependList.Length; ++k)
			//{
			//	if (selectedStrPath.Equals(materialDependList[k]))
			//	{
			//		if (materialDependList.Length > 2)
			//		{
			//			m_MultipleTextureMaterial.Add(materialPath);
			//		}
			//		else
			//		{
			//			m_MaterialPathList.Add(materialPath);
			//		}

			//		Debug.Log(materialPath);
			//	}
			//}
			if (i >= m_AllAssetList.Length - 1)
			{
				EditorUtility.ClearProgressBar();
			}
		}
	}

	// 1.2 显示Material依赖的Prefab
	private void FindMaterialDepend(string selectedStrPath)
	{
		// 1：查找所有 Material 找到纹理被引用的Material
		m_AllAssetList = AssetDatabase.FindAssets("t:Prefab", new string[] { m_SearchPath });
		for (int i = 0; i < m_AllAssetList.Length; ++i)
		{
			string prefabPath = AssetDatabase.GUIDToAssetPath(m_AllAssetList[i]);        // 遍历 Prefab 纹理
			EditorUtility.DisplayProgressBar("查找所有用到\" " + selectedStrPath + ".mat\" 的Prefab", prefabPath, (float)i / (float)m_AllAssetList.Length);
			string[] prefabDependList = AssetDatabase.GetDependencies(new string[] { prefabPath }); // Prefab依赖项 
			for (int k = 0; k < prefabDependList.Length; ++k)
			{
				if (selectedStrPath.Equals(prefabDependList[k]))
				{
					m_PrefabPathList.Add(prefabPath);
					Debug.Log("m_PrefabPathList :" + prefabPath);
				}
			}
			if (i >= m_AllAssetList.Length - 1)
			{
				EditorUtility.ClearProgressBar();
			}
		}
	}

	// 1.3 显示FindGameObject依赖的所有资源
	private void FindGameObjectDepend(string selectedStrPath)
	{
		// 1: 获取所有依赖项
		m_AllAssetList = AssetDatabase.GetDependencies(new string[] { selectedStrPath });
		for (int i = 0; i < m_AllAssetList.Length; ++i)
		{
			m_AllDependPathList.Add(m_AllAssetList[i]);
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

		EditorGUILayout.SelectableLabel("具有多个 Texture 的 Material ，请手动处理：");
		foreach (var item in m_MultipleTextureMaterial)
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

	// 显示Material依赖的Prefab
	private void ShowMaterialDepend()
	{
		// 1: 显示搜索得到的资源
		EditorGUILayout.BeginVertical();
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		// 显示 Prefab 列表
		EditorGUILayout.SelectableLabel("使用的 Prefab ：");
		for (int i = 0; i < m_PrefabPathList.Count; ++i)
		{
			GUILayout.BeginHorizontal();
			GameObject obj = AssetDatabase.LoadAssetAtPath(m_PrefabPathList[i], typeof(GameObject)) as GameObject;
			Texture2D texture = AssetPreview.GetAssetPreview(obj);
			Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
			if (texture != null)
				EditorGUI.DrawPreviewTexture(ret, texture);
			EditorGUILayout.TextField("", m_PrefabPathList[i], GUILayout.Height(30));
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
		for (int i = 0; i < m_AllDependPathList.Count; ++i)
		{
			Object asset = AssetDatabase.LoadAssetAtPath(m_AllDependPathList[i], typeof(Object)) as Object;
			if (asset.GetType() == typeof(Material))
			{
				matList.Add(m_AllDependPathList[i]);
			}
			else if (asset.GetType() == typeof(Texture2D))
			{
				textureList.Add(m_AllDependPathList[i]);
			}
			else
			{
				others.Add(m_AllDependPathList[i]);
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
			Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
			EditorGUI.DrawPreviewTexture(ret, texture);
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
		m_CurWinState = WinState.InitState;
	}

	void DeleteRes(string specifiedRes)
	{
		Texture2D texture = AssetDatabase.LoadAssetAtPath(specifiedRes, typeof(Texture2D)) as Texture2D;

		// AssetPreview 只能用来预览，不是真正的 Texture 
		//Texture2D texture = AssetPreview.GetAssetPreview(obj);

		for (int i = 0; i < m_MaterialPathList.Count; ++i)
		{
			//Renderer
			Material mt = AssetDatabase.LoadAssetAtPath(m_MaterialPathList[i], typeof(Material)) as Material;
			mt.mainTexture = texture;
			//Shader.SetGlobalTexture(m_NameID[i], texture);
		}

		// 删除重复的资源
		for (int i = 0; i < m_SelectedStrPath.Count; ++i)
		{
			if (m_SelectedStrPath[i] != specifiedRes)
			{
				AssetDatabase.DeleteAsset(m_SelectedStrPath[i]);
			}
		}

		m_SelectedStrPath.Clear();
		m_SelectedStrPath.Add(specifiedRes);

		AssetDatabase.Refresh();
	}


}
