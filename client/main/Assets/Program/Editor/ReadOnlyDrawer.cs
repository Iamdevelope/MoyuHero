using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        switch(prop.propertyType)
        {
            case SerializedPropertyType.Integer:
                EditorGUI.LabelField(position, label.text, prop.intValue.ToString());
                break;
            case SerializedPropertyType.Boolean:
                EditorGUI.LabelField(position, label.text, prop.boolValue.ToString());
                break;
            case SerializedPropertyType.Float:
                EditorGUI.LabelField(position, label.text, prop.floatValue.ToString("0.00000"));
                break;
            case SerializedPropertyType.String:
                EditorGUI.LabelField(position, label.text, prop.stringValue);
                break;
            case SerializedPropertyType.Vector2:
                EditorGUI.LabelField(position, label.text, prop.vector2Value.ToString());
                //EditorGUI.Vector2Field(position, label.text, prop.vector2Value);
                break;
            case SerializedPropertyType.Vector3:
                EditorGUI.LabelField(position, label.text, prop.vector3Value.ToString());    
                //EditorGUI.Vector3Field(position, label.text, prop.vector3Value);
                break;
            case SerializedPropertyType.ObjectReference:
                EditorGUI.ObjectField(position, label.text, prop.objectReferenceValue, typeof(System.Object), true);
                break;
            case SerializedPropertyType.ArraySize:
                EditorGUI.LabelField(position, label.text, prop.arraySize.ToString());
                break;
            default:
                break;
        }
    }
}