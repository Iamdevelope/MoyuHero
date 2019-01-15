using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(DHText), true)]
[CanEditMultipleObjects]
public class DHTextEditor : UnityEditor.UI.TextEditor
{
	SerializedProperty m_TextIndex;
	protected override void OnEnable()
	{
		m_TextIndex = serializedObject.FindProperty("m_TextIndex");
		base.OnEnable();		
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.Update();
		EditorGUILayout.PropertyField(m_TextIndex);
		serializedObject.ApplyModifiedProperties();
	}
}

