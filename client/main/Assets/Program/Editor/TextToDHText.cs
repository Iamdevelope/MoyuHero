using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextToDHText : EditorWindow
{
	private struct TextSet
	{
		public string text;
		public Font font;
		public FontStyle fontStyle;
		public int fontSize;
		public float lineSpacing;
		public bool richText;
		public TextAnchor alignment;
		public HorizontalWrapMode horizontalOverflow;
		public VerticalWrapMode verticalOverflow;
		public bool bestFit;
		public Color color;
		public Material material;
	}

	private enum WinState
	{
		InitState,
		FindResState,
	}

	private WinState m_State = WinState.InitState;

	private bool m_IsSelect = false; // 是否选择
	private List<GameObject> m_SelectTargets = new List<GameObject>();    // 选中的对象列表
	private List<string> m_SelectedStrPath = new List<string>();  // 选中的对象的路径列表

	private List<GameObject> m_ReplaceTargets = new List<GameObject>();
	private List<string> m_ReplaceStrPath = new List<string>();

	private List<TextSet> textSet = new List<TextSet>();
	private List<Transform> textList = new List<Transform>();


	private static TextToDHText m_Win;

	[MenuItem("GameTool/TextToDHText")]
	public static void Init()
	{
		m_Win = (TextToDHText)EditorWindow.GetWindow(typeof(TextToDHText), true, "TextToDHText");
		m_Win.ShowNotification(new GUIContent("替换操作无法撤销，请谨慎操作"));
	}

	void OnGUI()
	{
		switch (m_State)
		{
			case WinState.InitState:
				{
					OnInitState();
				}
				break;
			case WinState.FindResState:
				{
					OnInitState();
					ShowFindRes();
				}
				break;
			default:
				break;
		}
	}

	void OnInitState()
	{
		// 1: 获取选中的对象
		if (m_IsSelect == false)
		{
			GameObject[] gameObjects = Selection.gameObjects;
			m_SelectTargets.Clear();
			for (int i = 0; i < gameObjects.Length; i++)
			{
				m_SelectTargets.Add(gameObjects[i]);
				m_SelectedStrPath.Add(AssetDatabase.GetAssetPath(gameObjects[i]));
			}

			if (gameObjects.Length > 0)
			{
				m_IsSelect = true;
			}

		}

		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("选择目标", GUILayout.Width(100)))
		{
			m_IsSelect = false;
			m_SelectTargets.Clear();
			m_SelectedStrPath.Clear();
			m_ReplaceTargets.Clear();
			m_ReplaceStrPath.Clear();
		}

		if (GUILayout.Button("开始查找", GUILayout.Width(100)))
		{
			for (int i = 0; i < m_SelectTargets.Count; ++i)
			{
				GameObject obj = m_SelectTargets[i];
				if (obj != null)
				{
					GetAllText(obj.transform);
					m_ReplaceTargets.Add(obj);
					m_ReplaceStrPath.Add(m_SelectedStrPath[i]);
				}
			}

			if (m_SelectedStrPath.Count > 0)
			{
				m_Win.ShowNotification(new GUIContent("查找成功"));
				m_State = WinState.FindResState;
			}
		}

		if (GUILayout.Button("删除文本 Text", GUILayout.Width(100)))
		{
			for (int i = 0; i < textList.Count; i++)
			{
				DestroyImmediate(textList[i].gameObject.GetComponent<Text>(), true);
			}
			m_Win.ShowNotification(new GUIContent("删除成功"));
		}

		if (GUILayout.Button("添加文本 DHText", GUILayout.Width(100)))
		{
			for (int i = 0; i < textList.Count; i++)
			{
				DHText dhtext = textList[i].gameObject.AddComponent<DHText>();
				TextSet setting = textSet[i];
				dhtext.text = setting.text;
				dhtext.font = setting.font;
				dhtext.fontStyle = setting.fontStyle;
				dhtext.fontSize = setting.fontSize;
				dhtext.lineSpacing = setting.lineSpacing;
				dhtext.supportRichText = setting.richText;
				dhtext.alignment = setting.alignment;
				dhtext.horizontalOverflow = setting.horizontalOverflow;
				dhtext.verticalOverflow = setting.verticalOverflow;
				dhtext.resizeTextForBestFit = setting.bestFit;
				dhtext.color = setting.color;
				dhtext.material = setting.material;
			}

			m_Win.ShowNotification(new GUIContent(" 替换成功"));
			textSet.Clear();
			textList.Clear();
			m_SelectTargets.Clear();
		}

		if(GUILayout.Button("退出操作", GUILayout.Width(100)))
		{
			m_Win.Close();
		}

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginVertical();
		for (int i = 0; i < m_SelectedStrPath.Count; ++i)
		{
			GUILayout.Label(m_SelectedStrPath[i]);
		}
		EditorGUILayout.EndVertical();
	}

	void ShowFindRes()
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginScrollView(new Vector2(400, 100));

		for (int i = 0; i < m_ReplaceStrPath.Count; ++i)
		{
			EditorGUILayout.TextField("", m_ReplaceStrPath[i], GUILayout.Height(30));
		}

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}

	public void GetAllText(Transform transform)
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			Transform obj = transform.GetChild(i);
			Text text = obj.GetComponent<Text>();
			if (text != null)
			{
				TextSet setting = new TextSet();
				setting.text = text.text;
				setting.font = text.font;
				setting.fontStyle = text.fontStyle;
				setting.fontSize = text.fontSize;
				setting.lineSpacing = text.lineSpacing;
				setting.richText = text.supportRichText;
				setting.alignment = text.alignment;
				setting.horizontalOverflow = text.horizontalOverflow;
				setting.verticalOverflow = text.verticalOverflow;
				setting.bestFit = text.resizeTextForBestFit;
				setting.color = text.color;
				setting.material = text.material;

				textSet.Add(setting);

				textList.Add(obj);
			}

			GetAllText(obj);
		}
	}
}
