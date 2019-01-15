using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(StoryAnimDate))]
    public class StoryAnimDataEditor : Editor
    {
        private StoryAnimDate StoryAnimdata;
        private float RunAroundTime;//编辑参考用位移时间
        //StoryAnimdata
        private void OnEnable()
        {
            StoryAnimdata = (StoryAnimDate)target;
            if (StoryAnimdata.StoryAnimdata == null)
                StoryAnimdata.StoryAnimdata = new StoryAnimdate();
            if (StoryAnimdata.StoryAnimdata.MonsterData == null)
                StoryAnimdata.StoryAnimdata.MonsterData = new Monsterdata();
            if (StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList == null)
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList = new List<MonsterPointData>();
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("事件模式类型");
            StoryAnimdata.StoryAnimdata.StoryAnimaHoldtype = (StoryAnimaHoldType)EditorGUILayout.EnumPopup(StoryAnimdata.StoryAnimdata.StoryAnimaHoldtype);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("当前事件串联事件ID");
            StoryAnimdata.StoryAnimdata.CascadeEventID = EditorGUILayout.TextField(StoryAnimdata.StoryAnimdata.CascadeEventID);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            StoryAnimdata.StoryAnimdata.EventID = EditorGUILayout.IntSlider("当前事件ID", StoryAnimdata.StoryAnimdata.EventID, 1,50);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("事件时间点");
            StoryAnimdata.StoryAnimdata.EventTime = EditorGUILayout.Slider(StoryAnimdata.StoryAnimdata.EventTime,0,100);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("事件功能类型");
            StoryAnimdata.StoryAnimdata.StoryAnimEventtype = (StoryAnimEventType)EditorGUILayout.EnumPopup(StoryAnimdata.StoryAnimdata.StoryAnimEventtype);
            EditorGUILayout.EndHorizontal();
            SwichStoryAnimEventType();
           

        }
        private void OnSceneGUI()
        {
            Handles.color = Color.yellow;
            SwichEnterTypeInSceneGUI();
        }
        private void AddEnterType()
        {
            if (StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.Count != StoryAnimdata.StoryAnimdata.MonsterData.EntertypeListCount)
            {
                if (StoryAnimdata.StoryAnimdata.MonsterData.EntertypeListCount > StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.Count)
                {
                    for (int i = 0; i < StoryAnimdata.StoryAnimdata.MonsterData.EntertypeListCount - StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.Count; i++)
                    {
                        StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.Add(new MonsterPointData());
                    }
                }
                if (StoryAnimdata.StoryAnimdata.MonsterData.EntertypeListCount < StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.Count)
                {
                    for (int i = 0; i < StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.Count - StoryAnimdata.StoryAnimdata.MonsterData.EntertypeListCount; i++)
                    {
                        StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.RemoveAt(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.Count - 1 - i);
                    }
                }
            }
            for (int i = 0; i < StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("怪物出场方式");
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Entertype = (MonsterEnterType)EditorGUILayout.EnumPopup(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Entertype);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("怪物行为循环模式");
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].MonsterRuntype = (MonsterRunType)EditorGUILayout.EnumPopup(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].MonsterRuntype);
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("动作名称索引");
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].ActionName = EditorGUILayout.TextField(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].ActionName);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("怪物动作循环模式");
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].MonsterActiontype = (MonsterActionType)EditorGUILayout.EnumPopup(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].MonsterActiontype);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("特效名称");
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Effname = EditorGUILayout.TextField(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Effname);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Effpos = EditorGUILayout.Vector3Field("特效位置", StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Effpos);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("特效持续时间");
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].EffTime = EditorGUILayout.Slider(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].EffTime, 0, 10);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("行为持续时间");
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].ActionTime = EditorGUILayout.Slider(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].ActionTime, 0, 20);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("播放音效名称");
                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Soundname = EditorGUILayout.TextField(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Soundname);
                EditorGUILayout.EndHorizontal();

                SwichEnterTypeInInspectorGUI(i);
            }
        }
        private void SwichEnterTypeInInspectorGUI(int j)
        {
            switch (StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].Entertype)
            {
                case MonsterEnterType.StayIdle:
                    //EditorGUILayout.BeginHorizontal();
                    //EditorGUILayout.LabelField("原地待机自由移动的区域半径");
                    //StoryAnimdata.MonsterPointdataList[j].StayIdleSize = EditorGUILayout.Slider(StoryAnimdata.MonsterPointdataList[j].StayIdleSize, 0, 2);
                    //EditorGUILayout.EndHorizontal();
                    break;
                case MonsterEnterType.RunAround:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("怪物目标类型");
                    StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].MonsterLooktype = (MonsterLookType)EditorGUILayout.EnumPopup(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].MonsterLooktype);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("行为速度");
                    StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].ActionSpeed = EditorGUILayout.Slider(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].ActionSpeed, 0, 20);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("巡逻路径点数");
                    StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundCount = EditorGUILayout.IntSlider(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundCount, 0, 5);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("是否瞬间移动到寻路路径初始位置");
                    StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].IsMovetoFirstPoint = EditorGUILayout.Toggle(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].IsMovetoFirstPoint);
                    EditorGUILayout.EndHorizontal();
                    if (StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints == null)
                    {
                        StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints = new List<Vector3>();
                    }
                    if (StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints.Count != StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundCount)
                    {
                        if (StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundCount > StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints.Count)
                        {
                            for (int i = 0; i < StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundCount - StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints.Count; i++)
                            {
                                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints.Add(new Vector3(StoryAnimdata.transform.position.x, StoryAnimdata.transform.position.y, StoryAnimdata.transform.position.z));
                            }
                        }
                        if (StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundCount < StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints.Count)
                        {
                            for (int i = 0; i < StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints.Count - StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundCount; i++)
                            {
                                StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints.RemoveAt(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints.Count - 1 - i);
                            }
                        }
                    }
                    for (int i = 0; i < StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundCount; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints[i] = EditorGUILayout.Vector3Field("巡逻路径点坐标" + (i + 1).ToString(), StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints[i]);
                        EditorGUILayout.EndHorizontal();

                    }
                   // GetRunTime(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints, StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].ActionSpeed);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("当前位移行为所需要的时间");
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("编辑的时候参考用(不可调)");
                    StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundTime =
                        GetRunTime(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundPoints, StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].ActionSpeed);
                    StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundTime = EditorGUILayout.Slider(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].RunAroundTime, 0, 100);
                    EditorGUILayout.EndHorizontal();
                    break;
                case MonsterEnterType.Bench:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("怪物替补出场前怪物死亡数量");
                    StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].BenchCount = EditorGUILayout.IntSlider(StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[j].BenchCount, 0, 10);
                    EditorGUILayout.EndHorizontal();
                    break;
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("==================================================================");
            EditorGUILayout.EndHorizontal();
        }
        //计算移动行为所需要的时间
        private float GetRunTime(List<Vector3> temp, float speed)
        {
            float RunLine = 0;
            for (int i = 0; i < temp.Count; ++i)
            {
                if (i == temp.Count - 1)
                {

                }
                else
                    RunLine += Vector3.Distance(temp[i], temp[i + 1]);
            }
            return RunLine / speed;
        }
        //选择事件功能类型
        private void SwichStoryAnimEventType()
        {
            switch(StoryAnimdata.StoryAnimdata.StoryAnimEventtype)
            {
                case StoryAnimEventType.CreatStaticObj:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("创建Obj名字");
                    StoryAnimdata.StoryAnimdata.ObjName = EditorGUILayout.TextField(StoryAnimdata.StoryAnimdata.ObjName);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    StoryAnimdata.StoryAnimdata.ObjPos = EditorGUILayout.Vector3Field("实例化OBJ坐标", StoryAnimdata.StoryAnimdata.ObjPos);
                    StoryAnimdata.StoryAnimdata.ObjPos = StoryAnimdata.transform.position;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    StoryAnimdata.StoryAnimdata.ObjAngle = EditorGUILayout.Vector3Field("实例化OBJ角度", StoryAnimdata.StoryAnimdata.ObjAngle);
                    StoryAnimdata.StoryAnimdata.ObjAngle = StoryAnimdata.transform.eulerAngles;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("出场行为个数");
                    StoryAnimdata.StoryAnimdata.MonsterData.EntertypeListCount = EditorGUILayout.IntSlider(StoryAnimdata.StoryAnimdata.MonsterData.EntertypeListCount, 0, 10);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("第一个行为延迟播放时间");
                    StoryAnimdata.StoryAnimdata.MonsterData.EnterWaitTime = EditorGUILayout.Slider(StoryAnimdata.StoryAnimdata.MonsterData.EnterWaitTime, 0, 10);
                    EditorGUILayout.EndHorizontal();
                    AddEnterType();
                    break;

                case StoryAnimEventType.GetDynamicObj:
                    EditorGUILayout.BeginHorizontal();
                    StoryAnimdata.StoryAnimdata.ObjDynamicID = EditorGUILayout.IntSlider("获取动态物体索引ID", StoryAnimdata.StoryAnimdata.ObjDynamicID, 1, 10);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("动态获取Obj名称(仅限测试用)");
                    StoryAnimdata.StoryAnimdata.ObjDynamicName = EditorGUILayout.TextField(StoryAnimdata.StoryAnimdata.ObjDynamicName);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    StoryAnimdata.StoryAnimdata.ObjDynamicPos = EditorGUILayout.Vector3Field("实例化OBJ坐标(仅限测试用)", StoryAnimdata.StoryAnimdata.ObjDynamicPos);
                    StoryAnimdata.StoryAnimdata.ObjDynamicPos = StoryAnimdata.transform.position;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    StoryAnimdata.StoryAnimdata.ObjDynamicAngle = EditorGUILayout.Vector3Field("实例化OBJ角度(仅限测试用)", StoryAnimdata.StoryAnimdata.ObjDynamicAngle);
                    StoryAnimdata.StoryAnimdata.ObjDynamicAngle = StoryAnimdata.transform.eulerAngles;
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("出场行为个数");
                    StoryAnimdata.StoryAnimdata.MonsterData.EntertypeListCount = EditorGUILayout.IntSlider(StoryAnimdata.StoryAnimdata.MonsterData.EntertypeListCount, 0, 10);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("第一个行为延迟播放时间");
                    StoryAnimdata.StoryAnimdata.MonsterData.EnterWaitTime = EditorGUILayout.Slider(StoryAnimdata.StoryAnimdata.MonsterData.EnterWaitTime, 0, 10);
                    EditorGUILayout.EndHorizontal();
                    AddEnterType();
                    break;
                case StoryAnimEventType.PlayCamAnim:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("播放摄像机动画的名字");
                    StoryAnimdata.StoryAnimdata.CamAnimName = EditorGUILayout.TextField(StoryAnimdata.StoryAnimdata.CamAnimName);
                    EditorGUILayout.EndHorizontal();
                    break;
                case StoryAnimEventType.PlayCamAnimWait:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("播放摄像机动画的名字");
                    StoryAnimdata.StoryAnimdata.CamAnimName = EditorGUILayout.TextField(StoryAnimdata.StoryAnimdata.CamAnimName);
                    EditorGUILayout.EndHorizontal();
                    break;
            }
        }
        private void SwichEnterTypeInSceneGUI()
        {
            for (int i = 0; i < StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList.Count; i++)
            {
                Handles.CubeCap(0, StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Effpos, Quaternion.identity, 0.3f);
                switch (StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].Entertype)
                {
                    case MonsterEnterType.StayIdle:
                        //Handles.CircleCap(0, StoryAnimdata.transform.position, Quaternion.EulerRotation(20.45f, 0, 0), StoryAnimdata.MonsterPointdataList[i].StayIdleSize);
                        break;
                    case MonsterEnterType.RunAround:
                        for (int j = 0; j < StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].RunAroundCount; j++)
                        {
                            Handles.SphereCap(0, StoryAnimdata.StoryAnimdata.MonsterData.MonsterPointdataList[i].RunAroundPoints[j], Quaternion.identity, 0.3f);
                        }
                        break;
                }
            }
        }
    }
}

