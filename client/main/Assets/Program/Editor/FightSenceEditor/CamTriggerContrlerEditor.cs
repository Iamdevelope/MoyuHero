using UnityEngine;
using UnityEditor;
using System.Collections;
namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(CamTriggerEvent))]
    public class CamTriggerContrlerEditor : Editor
    {
        private CamTriggerEvent CamtriggerContrler;

        private void OnEnable()
        {
            CamtriggerContrler = (CamTriggerEvent)target;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("摄像机事件ID");
            CamtriggerContrler.info.EventID = EditorGUILayout.TextField(CamtriggerContrler.info.EventID);
            CamtriggerContrler.transform.name = CamtriggerContrler.info.EventID;
            EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("触发事件移动到的静态点");
            //CamtriggerContrler.info.CamMovetoPos = (Transform)EditorGUILayout.ObjectField(CamtriggerContrler.info.CamMovetoPos, typeof(GameObject), true);
            //CamtriggerContrler.info.CamMovetoPos = CamtriggerContrler.transform.GetChild(0);
            //EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            CamtriggerContrler.info.CamStaticPos = EditorGUILayout.Vector3Field("触发事件移动到的静态点坐标", CamtriggerContrler.info.CamStaticPos);
            CamtriggerContrler.info.CamStaticPos = CamtriggerContrler.transform.GetChild(0).position;
            CamtriggerContrler.info.CamStaticAngles = CamtriggerContrler.transform.GetChild(0).rotation;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("摄像机开关");
            CamtriggerContrler.info.CamHoldtype = (CamHoldType)EditorGUILayout.EnumPopup(CamtriggerContrler.info.CamHoldtype);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("攝像機看向方式");
            CamtriggerContrler.info.Camlooktype = (CamLookType)EditorGUILayout.EnumPopup(CamtriggerContrler.info.Camlooktype);
            EditorGUILayout.EndHorizontal();
            SwichCamLookTypeGUI();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("摄像机移动模式");
            CamtriggerContrler.info.CamType = (CamType)EditorGUILayout.EnumPopup(CamtriggerContrler.info.CamType);
            EditorGUILayout.EndHorizontal();
            SwichCamSenceTypeGUI();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("摄像机移动方式");
            CamtriggerContrler.info.CamPosMovetype = (CamMoveType)EditorGUILayout.EnumPopup(CamtriggerContrler.info.CamPosMovetype);
            EditorGUILayout.EndHorizontal();
            SwichCamPosMovetypeInInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("摄像机看向点");
            CamtriggerContrler.info.CamTagType = (CamTagType)EditorGUILayout.EnumPopup(CamtriggerContrler.info.CamTagType);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("摄像机看向点移动方式");
            CamtriggerContrler.info.CamAnglesMovetype = (CamMoveType)EditorGUILayout.EnumPopup(CamtriggerContrler.info.CamAnglesMovetype);
            EditorGUILayout.EndHorizontal();
            SwichCamAnglesMovetypeInInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            CamtriggerContrler.info.CamCenterDeviant = EditorGUILayout.Vector3Field("摄像机视角偏移值", CamtriggerContrler.info.CamCenterDeviant);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            CamtriggerContrler.info.CamPosDeviant = EditorGUILayout.Vector3Field("摄像机位置偏移值", CamtriggerContrler.info.CamPosDeviant);
            EditorGUILayout.EndHorizontal();
        }
        private void SwichCamLookTypeGUI()
        {
             switch(CamtriggerContrler.info.Camlooktype)
             {
                 case CamLookType.Slow:
                     {
                         EditorGUILayout.BeginHorizontal();
                         EditorGUILayout.LabelField("摄像机看向速度");
                         CamtriggerContrler.info.CamLookSpeed = EditorGUILayout.Slider(CamtriggerContrler.info.CamLookSpeed, 0.1f, 10);
                         EditorGUILayout.EndHorizontal();
                     }
                     break;
             }
        }
        //选择运动模式
        private void SwichCamSenceTypeGUI()
        {
            switch(CamtriggerContrler.info.CamType)
            {
                case CamType.Animation:
                    EditorGUILayout.BeginHorizontal();
                    CamtriggerContrler.info.CamAnimationID = EditorGUILayout.IntSlider("摄像机动画ID", CamtriggerContrler.info.CamAnimationID,1, 10);
                    EditorGUILayout.EndHorizontal();

                    //EditorGUILayout.BeginHorizontal();
                    //EditorGUILayout.LabelField("摄像机移动到播放动画位置的时间");
                    //CamtriggerContrler.info.CamAnimReadyTime = EditorGUILayout.Slider(CamtriggerContrler.info.CamAnimReadyTime, 0, 30);
                    //EditorGUILayout.EndHorizontal();
                    break;
            }
        }
        //坐标移动
        private void SwichCamPosMovetypeInInspectorGUI()
        {
            switch (CamtriggerContrler.info.CamPosMovetype)
            {
                case CamMoveType.NormalMove:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("摄像机坐标移动速度");
                    CamtriggerContrler.info.CamPosMoveSpeed = EditorGUILayout.Slider(CamtriggerContrler.info.CamPosMoveSpeed, 0, 10);
                    EditorGUILayout.EndHorizontal();
                    break;
                case CamMoveType.NormalMoveTime:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("摄像机坐标平滑阻尼时间");
                    CamtriggerContrler.info.CamPosMoveTime = EditorGUILayout.Slider(CamtriggerContrler.info.CamPosMoveTime, 0, 10);
                    EditorGUILayout.EndHorizontal();
                    break;
            }
        }
        //角度移动
        private void SwichCamAnglesMovetypeInInspectorGUI()
        {
            switch (CamtriggerContrler.info.CamAnglesMovetype)
            {
                case CamMoveType.NormalMove:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("摄像机朝向点移动速度");
                    CamtriggerContrler.info.CamAnglesMoveSpeed = EditorGUILayout.Slider(CamtriggerContrler.info.CamAnglesMoveSpeed, 0, 10);
                    EditorGUILayout.EndHorizontal();
                    break;
                case CamMoveType.NormalMoveTime:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("摄像机角度平滑阻尼时间");
                    CamtriggerContrler.info.CamAnglesMoveTime = EditorGUILayout.Slider(CamtriggerContrler.info.CamAnglesMoveTime, 0, 10);
                    EditorGUILayout.EndHorizontal();
                    break;
            }
        }
    }
}

