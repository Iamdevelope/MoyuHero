using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CheckPos : EditorWindow
{
    GameObject _target;  // 目标对象
    Canvas canvas = null;

    [MenuItem("GameTool/查看点击的按钮的屏幕位置")]
    public static void Init()
    {
        CheckPos win = (CheckPos)EditorWindow.GetWindow(typeof(CheckPos), true, "查看点击的按钮的屏幕位置");
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        // 选择的目标
        _target = Selection.activeGameObject;
        EditorGUILayout.ObjectField(_target, typeof(GameObject), true, GUILayout.Width(180), GUILayout.Height(20));


        if (_target != null)
        {
            Transform t = _target.transform;
            while (true)
            {
                if (t.parent.parent != null)
                {
                    t = t.parent;
                }
                else
                {
                    break;
                }
            }

            try
            {
                canvas = t.GetComponent<Canvas>();
            }
            catch (System.Exception ex)
            {
                Debug.Log("获取 Canvas 失败");
            }

            if (canvas != null)
            {
                EditorGUILayout.ObjectField(canvas, typeof(GameObject), true, GUILayout.Width(180), GUILayout.Height(20));

                Vector3 screenPos = canvas.worldCamera.WorldToScreenPoint(_target.transform.position);
                //Vector3 screenPos = canvas.worldCamera.WorldToViewportPoint(_target.transform.position);

                //EditorGUILayout.BeginHorizontal();

                //EditorGUILayout.LabelField("X 坐标", GUILayout.Width(50));
                //EditorGUILayout.TextField(screenPos.x.ToString(), GUILayout.Height(40));

                //EditorGUILayout.LabelField("y 坐标", GUILayout.Width(50));
                //EditorGUILayout.TextField(screenPos.y.ToString(), GUILayout.Height(40));
                //EditorGUILayout.EndHorizontal();
                //Vector2 pos;
                //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, _target.transform.position, canvas.worldCamera, out pos))
                //{
                //    EditorGUILayout.BeginHorizontal();
                //    EditorGUILayout.LabelField("X 坐标", GUILayout.Width(50));
                //    EditorGUILayout.TextField(pos.x.ToString(), GUILayout.Height(40));

                //    EditorGUILayout.LabelField("y 坐标", GUILayout.Width(50));
                //    EditorGUILayout.TextField(pos.y.ToString(), GUILayout.Height(40));
                //    EditorGUILayout.EndHorizontal();
                //}


                //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, _target.GetComponent<RectTransform>().anchoredPosition, canvas.worldCamera, out pos))
                //{
                //    EditorGUILayout.BeginHorizontal();
                //    EditorGUILayout.LabelField("X 坐标", GUILayout.Width(50));
                //    EditorGUILayout.TextField(pos.x.ToString(), GUILayout.Height(40));

                //    EditorGUILayout.LabelField("y 坐标", GUILayout.Width(50));
                //    EditorGUILayout.TextField(pos.y.ToString(), GUILayout.Height(40));
                //    EditorGUILayout.EndHorizontal();
                //}

                Vector2 pos;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out pos))
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("X 坐标", GUILayout.Width(50));
                    EditorGUILayout.TextField(pos.x.ToString(), GUILayout.Height(40));

                    EditorGUILayout.LabelField("y 坐标", GUILayout.Width(50));
                    EditorGUILayout.TextField(pos.y.ToString(), GUILayout.Height(40));
                    EditorGUILayout.EndHorizontal();
                }


                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
