using UnityEngine;
using System.Collections;
using UnityEditor.UI;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(RichText), true)]
[CanEditMultipleObjects]
public class RichTextEditor : UnityEditor.UI.TextEditor
{
    RichText richText;

	SerializedProperty m_TextCache;
	SerializedProperty m_Width;
	SerializedProperty m_Height;
	SerializedProperty m_Space;
	SerializedProperty m_ImageAlignment;
    SerializedProperty m_ChildAlignment;

	protected override void OnEnable()
	{
		m_TextCache = serializedObject.FindProperty("_textCache");
		m_Width = serializedObject.FindProperty("_width");
		m_Height = serializedObject.FindProperty("_height");
		m_Space = serializedObject.FindProperty("_space");
		m_ImageAlignment = serializedObject.FindProperty("_imageAlignment");
        m_ChildAlignment = serializedObject.FindProperty("_childAlignment");
        richText = (RichText)target;
		base.OnEnable();
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.Update();
		EditorGUILayout.PropertyField(m_TextCache);
		EditorGUILayout.PropertyField(m_Width);
		EditorGUILayout.PropertyField(m_Height);
		EditorGUILayout.PropertyField(m_Space);
		EditorGUILayout.PropertyField(m_ImageAlignment);
        EditorGUILayout.PropertyField(m_ChildAlignment);

        if(GUILayout.Button("预览"))
        {
            richText.ShowRichText(richText._textCache);
        }

        if(GUILayout.Button("清理"))
        {
            for (int i = richText.transform.childCount - 1; i >= 0; --i)
            {
                richText.transform.GetChild(i).gameObject.SetActive(false);
                DestroyImmediate(richText.transform.GetChild(i).gameObject);
            }
        }

		serializedObject.ApplyModifiedProperties();
	}
}
