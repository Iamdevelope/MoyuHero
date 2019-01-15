using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;

// 查找所有依赖此资源的地方
public class FindSameShader : EditorWindow
{
	// 界面状态
	private enum WinState
	{
		InputState,
		InitState,
		StartState,
		GoState,
		EndState,
	}
	// 当前界面状态
	private WinState curWinState = WinState.InputState;
	// 用来保存所有找到的资源路径的数据
	private string[] allMatrerialList;
	private string[] allPrefabList;

	// 用来保存找到的引用Prefab
	//private List<string> m_PrefabPathList = new List<string>();

	//List<string> m_MaterialPathList = new List<string>();
	// 滚动参数
	private Vector2 scrollPos = Vector2.zero;
	// 滚动参数
	private Vector2 scrollPos2 = Vector2.zero;

	// 搜索路径
	private string searchPath = "Assets/Art/Characters/test";
	// 单例
	public static FindSameShader win;

	// 目标 Material 
	Material m_TargetMt = null;

	Material m_FindMt = null;

	Transform m_FindObj = null;

	// 当前选中的对象
	private Object m_SelectedObjs;
	string m_SelectStrPath;

	Dictionary<string, List<string>> m_MaterialPath = new Dictionary<string, List<string>>();
	Dictionary<string, List<string>> m_PrefabPath = new Dictionary<string, List<string>>();
	Dictionary<string, List<string>> m_ObjPath = new Dictionary<string, List<string>>();
	
	[MenuItem("Assets/查找所有效果相同，命名不同的材质球")]
	public static void RenameAndReplace()
	{
		if (win == null)
		{
			win = (FindSameShader)EditorWindow.GetWindow(typeof(FindSameShader), true, "查找所有效果相同，命名不同的材质球");
			win.minSize = new Vector2(800, 480);
		}
	}

	void OnGUI()
	{
		switch (curWinState)
		{
			case WinState.InputState:
				OnInputState();
				break;
			case WinState.InitState:
				OnInitState();
				break;
			case WinState.StartState:
				OnStartState();
				break;
		}
	}

	void OnInputState()
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("请输入搜索路径", GUILayout.Width(120));
		searchPath = EditorGUILayout.TextArea(searchPath);
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("删除上次生成的文件", GUILayout.Height(30)))
		{
			curWinState = WinState.InitState;
		}

		EditorGUILayout.EndVertical();
	}

	private void OnInitState()
	{
		FindTextureDepend();
		FindMaterialDepend();
		
		// 更新win的Title标题
		win.title = "查找所有效果相同，命名不同的材质球";
		curWinState = WinState.StartState;
	}

	private void OnStartState()
	{
		try
		{
			ShowTextureDepend();
		}
		catch (System.Exception ex)
		{

		}
	}

	// 1.1 显示纹理依赖
	private void FindTextureDepend()
	{
		// 1：查找所有 Material 找到纹理被引用的Material
		allMatrerialList = AssetDatabase.FindAssets("t:Material", new string[] { searchPath });
		for (int i = 0; i < allMatrerialList.Length; ++i)
		{
			string materialPath = AssetDatabase.GUIDToAssetPath(allMatrerialList[i]);        // 遍历 Material 纹理
			// 显示进度条
			EditorUtility.DisplayProgressBar("查找所有用到的Material", materialPath, (float)i / (float)allMatrerialList.Length);
			try
			{
				Material material = AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material)) as Material;

				bool ret = false;
				foreach (var item in m_MaterialPath)
				{
					Material mt = AssetDatabase.LoadAssetAtPath(item.Key, typeof(Material)) as Material;
					if (mt.shader.name == material.shader.name && mt.mainTexture.name == material.mainTexture.name)
					{
						ret = true;
						item.Value.Add(materialPath);
						break;
					}

					//List<string> mtPath = mtItem.Value;
					//foreach (var item in mtPath)
					//{
					//	Material mt = AssetDatabase.LoadAssetAtPath(item, typeof(Material)) as Material;
					//	if (mt.shader.name == material.shader.name && mt.mainTexture.name == material.mainTexture.name)
					//	{
					//		ret = true;
					//		mtPath.Add(materialPath);
					//		break;
					//	}
					//}		
				}

				if (!ret)
				{
					List<string> materialPathList = new List<string>();
					materialPathList.Add(materialPath);
					m_MaterialPath.Add(materialPath, materialPathList);
				}
			}
			catch (System.Exception ex)
			{

			}

			if (i >= allMatrerialList.Length - 1)
			{
				EditorUtility.ClearProgressBar();
			}
		}
	}

	// 1.2 显示Material依赖的Prefab
	private void FindMaterialDepend()
	{
		// 1：查找所有 Material 找到纹理被引用的Material
		allPrefabList = AssetDatabase.FindAssets("t:Prefab", new string[] { searchPath });
		for (int i = 0; i < allPrefabList.Length; ++i)
		{
			string prefabPath = AssetDatabase.GUIDToAssetPath(allPrefabList[i]);        // 遍历 Prefab 纹理
			EditorUtility.DisplayProgressBar("查找所有用到的Prefab", prefabPath, (float)i / (float)allPrefabList.Length);
			string[] prefabDependList = AssetDatabase.GetDependencies(new string[] { prefabPath }); // Prefab依赖项 
			for (int j = 0; j < prefabDependList.Length; ++j)
			{
				foreach (var mtItem in m_MaterialPath)
				{
					string key = mtItem.Key;
					List<string> materials = mtItem.Value;
					for (int k = 0; k < materials.Count; ++k)
					{
						if (prefabDependList[j] == materials[k])
						{
							if (m_PrefabPath.ContainsKey(key))
							{
								m_PrefabPath[key].Add(prefabPath);
							}
							else
							{
								List<string> prefabPathList = new List<string>();
								prefabPathList.Add(prefabPath);
								m_PrefabPath.Add(key, prefabPathList);
							}

							if (m_ObjPath.ContainsKey(key))
							{
								string path = GetObjNameWithMaterial(key, prefabPath);
								m_ObjPath[key].Add(path); 
							}
							else
							{
								List<string> objPathList = new List<string>();
								string path = GetObjNameWithMaterial(key, prefabPath);
								objPathList.Add(path);
								m_ObjPath.Add(key, objPathList);
							}
						}
					}
				}
			}
			if (i >= allPrefabList.Length - 1)
			{
				EditorUtility.ClearProgressBar();
			}
		}
	}

	string GetObjNameWithMaterial(string materialPath, string prefabPath)
	{
		m_FindMt = AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material)) as Material;

		// 4：加载Prefab资源
		GameObject obj = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)) as GameObject;
		// 5：实例化Prefab
		GameObject instObj = PrefabUtility.InstantiatePrefab(obj) as GameObject;

		m_FindObj = null;
		GetObj(instObj.transform);

		string path = "";

		if (m_FindObj != null)
		{
			path = m_FindObj.name;
			Transform t = m_FindObj.parent;
			
			while (t != null)
			{
				path = string.Format("{0}/{1}", t.name, path);
				t = t.parent;
			}
		}

		Object.DestroyImmediate(instObj);
		return path;
	}

	void GetObj(Transform transform)
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			Transform obj = transform.GetChild(i);
			// Paritical System
			try
			{
				ParticleSystem particalSys = obj.GetComponent<ParticleSystem>();
				if (particalSys)
				{
					Material[] mts = particalSys.renderer.sharedMaterials;
					for (int j = 0; j < mts.Length; ++j)
					{
						if (particalSys.renderer.sharedMaterial.mainTexture.name == m_FindMt.mainTexture.name && particalSys.renderer.sharedMaterial.shader.name == m_FindMt.shader.name)
						{
							m_FindObj = obj;
							return;
						}
					}
					
				}
			}
			catch (System.Exception ex)
			{

			}
			
			// Particle Renderer
			try
			{
				ParticleRenderer particalRend = obj.GetComponent<ParticleRenderer>();
				if (particalRend)
				{
					if (particalRend.sharedMaterial.mainTexture.name == m_FindMt.mainTexture.name && particalRend.sharedMaterial.shader.name == m_FindMt.shader.name)
					{
						m_FindObj = obj;
						return;
					}

				}
			}
			catch (System.Exception ex)
			{

			}

			// Trail Renderer
			try
			{
				TrailRenderer trailRenderer = obj.GetComponent<TrailRenderer>();
				if (trailRenderer)
				{
					if (trailRenderer.sharedMaterial.mainTexture.name == m_FindMt.mainTexture.name && trailRenderer.sharedMaterial.shader.name == m_FindMt.shader.name)
					{
						m_FindObj = obj;
						return;
					}
				}
			}
			catch (System.Exception ex)
			{

			}

			// Line Renderer
			try
			{
				LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
				if (lineRenderer)
				{
					//Material[] mts = lineRenderer.sharedMaterials;
					//for (int j = 0; j < mts.Length; ++j)
					//{
					//	Material mt = mts[j];
					//	if (mt.mainTexture.name == m_TargetMt.mainTexture.name && mt.shader.name == m_TargetMt.shader.name)
					//	{
					//		lineRenderer.sharedMaterials[j] = m_TargetMt;
					//	}
					//}

					if (lineRenderer.sharedMaterial.mainTexture.name == m_FindMt.mainTexture.name && lineRenderer.sharedMaterial.shader.name == m_FindMt.shader.name)
					{
						m_FindObj = obj;
						return;
					}
				}
			}
			catch (System.Exception ex)
			{

			}

			FindMaterial(obj);
		}
	}

	// 显示依赖的Material界面布局
	private void ShowTextureDepend()
	{
		// 1：显示 Material 列表
		EditorGUILayout.BeginVertical();
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		foreach(var mtItem in m_MaterialPath)
		{
			if (mtItem.Value.Count <= 1)
				continue;

			EditorGUILayout.SelectableLabel("所有功能的材质球");

			foreach (var item in mtItem.Value)
			{
				Material mt = AssetDatabase.LoadAssetAtPath(item, typeof(Material)) as Material;
				Texture2D texture = AssetPreview.GetAssetPreview(mt);
				EditorGUILayout.BeginHorizontal();
				Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
				EditorGUI.DrawPreviewTexture(ret, texture);
				EditorGUILayout.TextField("", item, GUILayout.Height(30));
				if (GUILayout.Button("点击查看资源", GUILayout.Width(150), GUILayout.Height(30)))
				{
					Selection.activeObject = mt;
				}
				if (GUILayout.Button("从列表中移除", GUILayout.Width(150), GUILayout.Height(30)))
				{
					mtItem.Value.Remove(item);
				}
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.Space();

			if (m_PrefabPath.ContainsKey(mtItem.Key))
			{
				//
				EditorGUILayout.SelectableLabel("使用的 Prefab ：");
				int i = 0;
				foreach (var item in m_PrefabPath[mtItem.Key])
				{
					EditorGUILayout.BeginHorizontal();
					GameObject obj = AssetDatabase.LoadAssetAtPath(item, typeof(GameObject)) as GameObject;
					Texture2D texture = AssetPreview.GetAssetPreview(obj);
					Rect ret = EditorGUILayout.GetControlRect(GUILayout.Height(30), GUILayout.Width(30));
					if (texture != null)
						EditorGUI.DrawPreviewTexture(ret, texture);
					EditorGUILayout.TextField("", item, GUILayout.Height(30));
					EditorGUILayout.TextField("", m_ObjPath[mtItem.Key][i], GUILayout.Height(30));
					if (GUILayout.Button("点击查看资源", GUILayout.Width(150), GUILayout.Height(30)))
					{
						Selection.activeObject = obj;
					}
					if (GUILayout.Button("从列表中移除", GUILayout.Width(150), GUILayout.Height(30)))
					{
						m_PrefabPath[mtItem.Key].Remove(item);
					}
					EditorGUILayout.EndHorizontal();

					++i;
				}
			}

			if (GUILayout.Button("合并", GUILayout.Height(40)))
			{
				DeleceSameRes(mtItem.Key);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}
		

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}

	void DeleceSameRes(string key)
	{
		
		List<string> materialPathList = m_MaterialPath[key];
		List<string> prefabPathList = m_PrefabPath[key];
		m_TargetMt = AssetDatabase.LoadAssetAtPath(key, typeof(Material)) as Material;
		//Object mt = AssetDatabase.LoadAssetAtPath(m_MaterialPathList[0], typeof(Object)) as Object;
		//m_TargetMt = PrefabUtility.InstantiatePrefab(mt) as Material;
		Debug.Log(key);
		for (int i = 0; i < prefabPathList.Count; ++i)
		{
			// 4：加载Prefab资源
			GameObject obj = AssetDatabase.LoadAssetAtPath(prefabPathList[i], typeof(GameObject)) as GameObject;
			// 5：实例化Prefab
			GameObject instObj = PrefabUtility.InstantiatePrefab(obj) as GameObject;

			FindMaterial(instObj.transform);

			// 9：重新覆盖Prefab，应用修改！
			PrefabUtility.ReplacePrefab(instObj, obj);
			// 10：删除实例化出来的Prefab对象
			Object.DestroyImmediate(instObj);

			// 删除重复的资源
			for (int j = 1; i < m_MaterialPath[key].Count; ++j)
			{
				AssetDatabase.DeleteAsset(m_MaterialPath[key][j]);
			}
		}
	}

	void FindMaterial(Transform transform)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform obj = transform.GetChild(i);
			// Paritical System
			try
			{
				ParticleSystem particalSys = obj.GetComponent<ParticleSystem>();
				if (particalSys)
				{
					Material[] mts = particalSys.renderer.sharedMaterials;
					for (int j = 0; j < mts.Length; ++j)
					{
						if (particalSys.renderer.sharedMaterial.mainTexture.name == m_TargetMt.mainTexture.name && particalSys.renderer.sharedMaterial.shader.name == m_TargetMt.shader.name)
						{
							particalSys.renderer.sharedMaterial = m_TargetMt;
						}
					}					
				}
			}
			catch (System.Exception ex)
			{

			}
			
			// Particle Renderer
			try
			{
				ParticleRenderer particalRend = obj.GetComponent<ParticleRenderer>();
				if (particalRend)
				{
					if (particalRend.sharedMaterial.mainTexture.name == m_TargetMt.mainTexture.name && particalRend.sharedMaterial.shader.name == m_TargetMt.shader.name)
					{
						particalRend.sharedMaterial = m_TargetMt;
					}

				}
			}
			catch (System.Exception ex)
			{

			}

			// Trail Renderer
			try
			{
				TrailRenderer trailRenderer = obj.GetComponent<TrailRenderer>();
				if (trailRenderer)
				{
					if (trailRenderer.sharedMaterial.mainTexture.name == m_TargetMt.mainTexture.name && trailRenderer.sharedMaterial.shader.name == m_TargetMt.shader.name)
					{
						trailRenderer.sharedMaterial = m_TargetMt;
					}
				}
			}
			catch (System.Exception ex)
			{

			}

			// Line Renderer
			try
			{
				LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
				if (lineRenderer)
				{
					//Material[] mts = lineRenderer.sharedMaterials;
					//for (int j = 0; j < mts.Length; ++j)
					//{
					//	Material mt = mts[j];
					//	if (mt.mainTexture.name == m_TargetMt.mainTexture.name && mt.shader.name == m_TargetMt.shader.name)
					//	{
					//		lineRenderer.sharedMaterials[j] = m_TargetMt;
					//	}
					//}

					if (lineRenderer.sharedMaterial.mainTexture.name == m_TargetMt.mainTexture.name && lineRenderer.sharedMaterial.shader.name == m_TargetMt.shader.name)
					{
						lineRenderer.sharedMaterial = m_TargetMt;
					}
				}
			}
			catch (System.Exception ex)
			{

			}

			FindMaterial(obj);
		}
	}
}
