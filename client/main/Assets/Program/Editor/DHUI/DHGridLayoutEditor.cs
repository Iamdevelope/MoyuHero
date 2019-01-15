using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.UI;
using UnityEditor;

[CustomEditor(typeof(DHGridLayout), true)]
[CanEditMultipleObjects]
public class DHGridLayoutEditor : GridLayoutGroupEditor
{
	SerializedProperty _loadLength;
	SerializedProperty _offsetLength;
	SerializedProperty _index;
	SerializedProperty _cellCount;
	SerializedProperty m_ChildAlignment;
	SerializedProperty m_Constraint;
	SerializedProperty m_ConstraintCount;

	protected virtual void OnEnable()
	{
		base.OnEnable();
		_loadLength = serializedObject.FindProperty("_loadLength");
		_offsetLength = serializedObject.FindProperty("_offsetLength");
		_index = serializedObject.FindProperty("_index");
		_cellCount = serializedObject.FindProperty("cellCount");
		//_isAutoLoad = serializedObject.FindProperty("_isAutoLoad");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.Update();
		EditorGUILayout.PropertyField(_loadLength);
		EditorGUILayout.PropertyField(_offsetLength);
		EditorGUILayout.PropertyField(_index);
		EditorGUILayout.PropertyField(_cellCount);
		//EditorGUILayout.PropertyField(_isAutoLoad);
		serializedObject.ApplyModifiedProperties();
	}
}
