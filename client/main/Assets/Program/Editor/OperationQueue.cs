using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class OperationQueue : EditorWindow
{
    bool isFallback = false;
    int index = 0;
    List<Object> m_SelectQueue = new List<Object>();

    [MenuItem ( "GameTools/OperationQueue" )]
    static public void Init ()
    {
        EditorWindow.GetWindow ( typeof ( OperationQueue ) );
    }

    void OnEnable ()
    {
        autoRepaintOnSceneChange = true;
    }

    void OnSelectionChange ()
    {
        if (isFallback)
        {
            isFallback = false;
            return;
        }

        if (Selection.activeObject == null)
            return;

        Debug.Log(Selection.activeObject.name);        

        if ( m_SelectQueue.Count < 1 )
        {
            m_SelectQueue.Add ( Selection.activeObject );

            index = m_SelectQueue.Count - 1;
        }

        // 添加
        if ( Selection.activeObject != m_SelectQueue [ m_SelectQueue.Count - 1 ] )
        {
            m_SelectQueue.Add ( Selection.activeObject );
            index = m_SelectQueue.Count - 1;
        }

        // 移除
        if ( m_SelectQueue.Count > 10 )
        {
            m_SelectQueue.RemoveAt ( 0 );
        }
    }


    void OnGUI ()
    {
        EditorGUILayout.BeginHorizontal ();

        if ( GUILayout.Button ( "<--", GUILayout.Height ( 45 ) ) )
        {
            if ( index > 0 )
            {
                isFallback = true;
                index--;
                Selection.activeObject = m_SelectQueue[index];                
            }            
        }

        if ( GUILayout.Button ( "-->", GUILayout.Height ( 45 ) ) )
        {
            if ( index < m_SelectQueue.Count - 1 )
            {
                isFallback = true;
                index++;
                Selection.activeObject = m_SelectQueue [ index ];
            }
        }

        if (GUILayout.Button("Clear", GUILayout.Height(45)))
        {
            index = 0;
            m_SelectQueue.Clear();
        }

        EditorGUILayout.EndHorizontal ();

        //EditorGUILayout.BeginVertical ();
        //EditorGUILayout.IntField ( "Index", index );

        for ( int i = m_SelectQueue.Count - 1; i >= 0; --i )
        {
            //EditorGUILayout.ObjectField ( "index " + i.ToString (), m_SelectQueue [ i ], typeof ( GameObject ), GUILayout.Height ( 35 ) );
        }

        //EditorGUILayout.EndVertical ();
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}
