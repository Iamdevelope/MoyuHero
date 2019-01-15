using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(MonsterPoint))]
    //[ExecuteInEditMode()]
    public class MonsterPointEditor : Editor
    {
        private MonsterPoint Monsterpoint;
        //private float RunAroundTime;//编辑参考用位移时间
        private void OnEnable()
        {
            Monsterpoint = (MonsterPoint)target;
            if (Monsterpoint.MonsterPointdataList == null)
                Monsterpoint.MonsterPointdataList = new List<MonsterPointData>();
        }
        public override void OnInspectorGUI()
        {
            // DrawDefaultInspector();

            //EditorGUILayout.BeginHorizontal();
            //Monsterpoint.MyPos = EditorGUILayout.Vector3Field("自身坐标", Monsterpoint.MyPos);
            //Monsterpoint.MyPos = Monsterpoint.transform.position;
            //EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("出场行为个数");
            Monsterpoint.EntertypeListCount = EditorGUILayout.IntSlider(Monsterpoint.EntertypeListCount, 1, 10);
            EditorGUILayout.EndHorizontal();
            AddEnterType();
            //====================================================###=================================================
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("怪物入场延迟时间");
            Monsterpoint.EnterWaitTime = EditorGUILayout.Slider(Monsterpoint.EnterWaitTime, 0, 10);
            EditorGUILayout.EndHorizontal();
            //====================================================###=================================================
            if (GUILayout.Button("Return Pos"))
            {
                Monsterpoint.transform.localPosition = Vector3.zero;
            }
        }
        private void OnSceneGUI()
        {
            Handles.color = Color.yellow;
            SwichEnterTypeInSceneGUI();
        }
        private void AddEnterType()
        {
            if (Monsterpoint.MonsterPointdataList.Count != Monsterpoint.EntertypeListCount)
            {
                if (Monsterpoint.EntertypeListCount > Monsterpoint.MonsterPointdataList.Count)
                {
                    for (int i = 0; i < Monsterpoint.EntertypeListCount - Monsterpoint.MonsterPointdataList.Count; i++)
                    {
                        Monsterpoint.MonsterPointdataList.Add(new MonsterPointData());
                    }
                }
                if (Monsterpoint.EntertypeListCount < Monsterpoint.MonsterPointdataList.Count)
                {
                    for (int i = 0; i < Monsterpoint.MonsterPointdataList.Count - Monsterpoint.EntertypeListCount; i++)
                    {
                        Monsterpoint.MonsterPointdataList.RemoveAt(Monsterpoint.MonsterPointdataList.Count - 1 - i);
                    }
                }
            }
            for (int i = 0; i < Monsterpoint.MonsterPointdataList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("怪物出场方式");
                Monsterpoint.MonsterPointdataList[i].Entertype = (MonsterEnterType)EditorGUILayout.EnumPopup(Monsterpoint.MonsterPointdataList[i].Entertype);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("怪物行为循环模式");
                Monsterpoint.MonsterPointdataList[i].MonsterRuntype = (MonsterRunType)EditorGUILayout.EnumPopup(Monsterpoint.MonsterPointdataList[i].MonsterRuntype);
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("动作名称索引");
                Monsterpoint.MonsterPointdataList[i].ActionName = EditorGUILayout.TextField(Monsterpoint.MonsterPointdataList[i].ActionName);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("怪物动作循环模式");
                Monsterpoint.MonsterPointdataList[i].MonsterActiontype = (MonsterActionType)EditorGUILayout.EnumPopup(Monsterpoint.MonsterPointdataList[i].MonsterActiontype);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("特效名称");
                Monsterpoint.MonsterPointdataList[i].Effname = EditorGUILayout.TextField(Monsterpoint.MonsterPointdataList[i].Effname);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                Monsterpoint.MonsterPointdataList[i].Effpos = EditorGUILayout.Vector3Field("特效位置", Monsterpoint.MonsterPointdataList[i].Effpos);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("特效持续时间");
                Monsterpoint.MonsterPointdataList[i].EffTime = EditorGUILayout.Slider(Monsterpoint.MonsterPointdataList[i].EffTime, 0, 10);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("音效名称");
                Monsterpoint.MonsterPointdataList[i].Soundname = EditorGUILayout.TextField(Monsterpoint.MonsterPointdataList[i].Soundname);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("行为持续时间");
                Monsterpoint.MonsterPointdataList[i].ActionTime = EditorGUILayout.Slider(Monsterpoint.MonsterPointdataList[i].ActionTime, 0, 20);
                EditorGUILayout.EndHorizontal();

                SwichEnterTypeInInspectorGUI(i);
            }
        } 
        private void SwichEnterTypeInInspectorGUI(int j)
        {
            switch (Monsterpoint.MonsterPointdataList[j].Entertype)
            {
                case MonsterEnterType.StayIdle:
                    //EditorGUILayout.BeginHorizontal();
                    //EditorGUILayout.LabelField("原地待机自由移动的区域半径");
                    //Monsterpoint.MonsterPointdataList[j].StayIdleSize = EditorGUILayout.Slider(Monsterpoint.MonsterPointdataList[j].StayIdleSize, 0, 2);
                    //EditorGUILayout.EndHorizontal();
                    break;
                case MonsterEnterType.RunAround:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("怪物目标类型");
                    Monsterpoint.MonsterPointdataList[j].MonsterLooktype = (MonsterLookType)EditorGUILayout.EnumPopup(Monsterpoint.MonsterPointdataList[j].MonsterLooktype);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("行为速度");
                    Monsterpoint.MonsterPointdataList[j].ActionSpeed = EditorGUILayout.Slider(Monsterpoint.MonsterPointdataList[j].ActionSpeed, 0, 20);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("巡逻路径点数");
                    Monsterpoint.MonsterPointdataList[j].RunAroundCount = EditorGUILayout.IntSlider(Monsterpoint.MonsterPointdataList[j].RunAroundCount, 0, 5);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("是否瞬间移动到寻路路径初始位置");
                    Monsterpoint.MonsterPointdataList[j].IsMovetoFirstPoint = EditorGUILayout.Toggle(Monsterpoint.MonsterPointdataList[j].IsMovetoFirstPoint);
                    EditorGUILayout.EndHorizontal();

                    if (Monsterpoint.MonsterPointdataList[j].RunAroundPoints == null)
                    {
                        Monsterpoint.MonsterPointdataList[j].RunAroundPoints = new List<Vector3>();
                    }
                    if (Monsterpoint.MonsterPointdataList[j].RunAroundPoints.Count != Monsterpoint.MonsterPointdataList[j].RunAroundCount)
                    {
                        if (Monsterpoint.MonsterPointdataList[j].RunAroundCount > Monsterpoint.MonsterPointdataList[j].RunAroundPoints.Count)
                        {
                            for (int i = 0; i < Monsterpoint.MonsterPointdataList[j].RunAroundCount - Monsterpoint.MonsterPointdataList[j].RunAroundPoints.Count; i++)
                            {
                                Monsterpoint.MonsterPointdataList[j].RunAroundPoints.Add(new Vector3(Monsterpoint.transform.position.x, Monsterpoint.transform.position.y, Monsterpoint.transform.position.z));
                            }
                        }
                        if (Monsterpoint.MonsterPointdataList[j].RunAroundCount < Monsterpoint.MonsterPointdataList[j].RunAroundPoints.Count)
                        {
                            for (int i = 0; i < Monsterpoint.MonsterPointdataList[j].RunAroundPoints.Count - Monsterpoint.MonsterPointdataList[j].RunAroundCount; i++)
                            {
                                Monsterpoint.MonsterPointdataList[j].RunAroundPoints.RemoveAt(Monsterpoint.MonsterPointdataList[j].RunAroundPoints.Count - 1 - i);
                            }
                        }
                    }
                    for (int i = 0; i < Monsterpoint.MonsterPointdataList[j].RunAroundCount; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        Monsterpoint.MonsterPointdataList[j].RunAroundPoints[i] = EditorGUILayout.Vector3Field("巡逻路径点坐标" + (i + 1).ToString(), Monsterpoint.MonsterPointdataList[j].RunAroundPoints[i]);
                        EditorGUILayout.EndHorizontal();
                    }
                    GetRunTime(Monsterpoint.MonsterPointdataList[j].RunAroundPoints, Monsterpoint.MonsterPointdataList[j].ActionSpeed);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("当前位移行为所需要的事件");
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("编辑的时候参考用(不可调)");
                    Monsterpoint.MonsterPointdataList[j].RunAroundTime =  GetRunTime(Monsterpoint.MonsterPointdataList[j].RunAroundPoints, Monsterpoint.MonsterPointdataList[j].ActionSpeed);
                    Monsterpoint.MonsterPointdataList[j].RunAroundTime = EditorGUILayout.Slider(Monsterpoint.MonsterPointdataList[j].RunAroundTime, 0, 100);
                    EditorGUILayout.EndHorizontal();
                    break;
                case MonsterEnterType.Bench:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("怪物替补出场前怪物死亡数量");
                    Monsterpoint.MonsterPointdataList[j].BenchCount = EditorGUILayout.IntSlider(Monsterpoint.MonsterPointdataList[j].BenchCount, 0, 10);
                    EditorGUILayout.EndHorizontal();
                    break;
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("==================================================================");
            EditorGUILayout.EndHorizontal();
        }
        //计算移动行为所需要的时间
        private float GetRunTime(List<Vector3> temp,float speed)
        {
            float RunLine = 0;
            for (int i = 0; i < temp.Count; ++i)
            {
                if (i == temp.Count - 1)
                {
                    //..
                }
                else
                   RunLine += Vector3.Distance(temp[i], temp[i + 1]);
            }
            return  RunLine / speed;
        }
        private void SwichEnterTypeInSceneGUI()
        {
            for (int i = 0; i < Monsterpoint.MonsterPointdataList.Count; i++)
            {
                Handles.CubeCap(0, Monsterpoint.MonsterPointdataList[i].Effpos, Quaternion.identity, 0.3f);
                switch (Monsterpoint.MonsterPointdataList[i].Entertype)
                {
                    case MonsterEnterType.StayIdle:
                        //Handles.CircleCap(0, Monsterpoint.transform.position, Quaternion.EulerRotation(20.45f,0,0), Monsterpoint.MonsterPointdataList[i].StayIdleSize);
                        break;
                    case MonsterEnterType.RunAround:
                        for (int j = 0; j < Monsterpoint.MonsterPointdataList[i].RunAroundCount; j++)
                        {
                            Handles.SphereCap(0,Monsterpoint.MonsterPointdataList[i].RunAroundPoints[j], Quaternion.identity, 0.3f);
                        }
                        break;
                }
            }
        }
    }
}

