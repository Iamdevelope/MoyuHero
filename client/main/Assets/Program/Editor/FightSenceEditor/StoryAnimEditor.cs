using UnityEngine;
using UnityEditor;
using System.Collections;
namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(StoryAnim))]
    public class StoryAnimEditor : Editor
    {
        private StoryAnim Storyanim;
        private void OnEnable()
        {
            Storyanim = (StoryAnim)target;
            if (Storyanim.StoryAnimgroup == null)
                Storyanim.StoryAnimgroup = new StoryAnimGroup();
        }
        public override void OnInspectorGUI()
        {
            Storyanim.StoryAnimgroup.CamTagPos = Storyanim.transform.parent.GetChild(0).position;
            Storyanim.StoryAnimgroup.CamTagAngle = Storyanim.transform.parent.GetChild(0).rotation;
            EditorGUILayout.BeginHorizontal();
            Storyanim.StoryAnimgroup.ID = EditorGUILayout.IntSlider("触发剧情ID索引", Storyanim.StoryAnimgroup.ID, 1, 10);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("触发剧情时当前摄像机移动到剧情动画摄像机位置的时间");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            Storyanim.StoryAnimgroup.CamToTagTime = 0f; //EditorGUILayout.Slider(Storyanim.StoryAnimgroup.CamToTagTime, 0f, 10);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("创建动画单一事件"))
            {
                GameObject obj = new GameObject("StoryAnimDate");
                obj.transform.parent = Storyanim.transform;
                obj.AddComponent<StoryAnimDate>();
                obj.GetComponent<StoryAnimDate>().StoryAnimdata = new StoryAnimdate();
                obj.GetComponent<StoryAnimDate>().StoryAnimdata.EventID = Storyanim.transform.childCount;
            }
        }
    }
}

