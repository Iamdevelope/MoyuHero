using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(MonsterGroup))]
    public class MonsterGroupEditor : Editor
    {
        MonsterGroup Monstergroup;
        private void OnEnable()
        {
            Monstergroup = (MonsterGroup)target;
            if (Monstergroup.MonsterGroupdata == null)
                Monstergroup.MonsterGroupdata = new MonstersGroupData();
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("怪物组波数");
            Monstergroup.MonsterGroupdata.MonstersGroupID = EditorGUILayout.IntSlider(Monstergroup.MonsterGroupdata.MonstersGroupID, 1, 50);
            Monstergroup.MonsterGroupdata.MonsterGroupAngle = Monstergroup.transform.eulerAngles;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("当前怪物组是否有支援怪物");
            Monstergroup.MonsterGroupdata.isSupport = EditorGUILayout.Toggle(Monstergroup.MonsterGroupdata.isSupport);
            Monstergroup.MonsterGroupdata.MonsterGroupAngle = Monstergroup.transform.eulerAngles;
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("创建怪物出生点"))
            {
                GameObject point = GameObject.CreatePrimitive(PrimitiveType.Cube);
                point.transform.parent = Monstergroup.transform;
                point.name = "Point";
                point.transform.localPosition = Vector3.zero;
                point.transform.localEulerAngles = Vector3.zero;
                point.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                point.AddComponent<MonsterPoint>();
            }
        }
        void OnSceneGUI()
        {
            Handles.color = Color.blue;
        }
    }
}

