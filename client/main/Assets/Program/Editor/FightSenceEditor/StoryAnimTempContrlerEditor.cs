using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameSceneEditor;
namespace DreamFaction.GameSceneEditorText
{
    [CustomEditor(typeof(StoryAnimTempContrler))]
    public class StoryAnimTempContrlerEditor : Editor
    {
        private StoryAnimTempContrler StoryAnimTemp;
        private void OnEnable()
        {
            StoryAnimTemp = (StoryAnimTempContrler)target;
        }
        public override void OnInspectorGUI()
        {

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("保存场景名称");
            string str = EditorApplication.currentScene;
            string scenename = str.Substring(str.LastIndexOf("/") + 1, str.LastIndexOf(".") - str.LastIndexOf("/") - 1);
            StoryAnimTemp.SceneName = EditorGUILayout.TextField(scenename);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("创建动画编辑组件"))
            {
                GameObject obj = new GameObject("StoryAnim");
                obj.transform.parent = StoryAnimTemp.transform;
                obj.AddComponent<StoryAnim>();
            }

            if (GUILayout.Button("DONE"))
            {
                DonePre();
                DoneObj();
            }
        }
        private void DoneObj()
        {
            StoryAnimDataObj StoryAnimData = ScriptableObject.CreateInstance<StoryAnimDataObj>();
            StoryAnimData.StoryCamAnimsName = StoryAnimTemp.transform.GetChild(0).name;
            StoryAnimData.StoryAnimGroupList=new List<StoryAnimGroup>();
            for(int i=1;i<StoryAnimTemp.transform.childCount;++i)
            {
                StoryAnimData.StoryAnimGroupList.Add(StoryAnimTemp.transform.GetChild(i).GetComponent<StoryAnim>().Adddata());
            }
#if UNITY_ANDROID
            string saveurl = "Assets/StreamingAssets/ScenceEditor/" + "Android/" + StoryAnimTemp.SceneName + "/StoryAnim/";
            if (Directory.Exists(saveurl) == false)
                Directory.CreateDirectory(saveurl);
            string name = saveurl + StoryAnimTemp.SceneName + "StoryAnim";
            AssetDatabase.CreateAsset(StoryAnimData, name + ".asset");
            Object o = AssetDatabase.LoadAssetAtPath(name + ".asset", typeof(StoryAnimDataObj));
            BuildPipeline.BuildAssetBundle(o, null, name + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);

            BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/Android/" + StoryAnimTemp.SceneName + "StoryAnim" + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);
#endif

#if UNITY_IPHONE
            string saveurl = "Assets/StreamingAssets/ScenceEditor/" + "IPhonePlayer/" + StoryAnimTemp.SceneName + "/StoryAnim/";
            if (Directory.Exists(saveurl) == false)
                Directory.CreateDirectory(saveurl);
            string name = saveurl + StoryAnimTemp.SceneName + "StoryAnim";
            AssetDatabase.CreateAsset(StoryAnimData, name + ".asset");
            Object o = AssetDatabase.LoadAssetAtPath(name + ".asset", typeof(StoryAnimDataObj));

            BuildPipeline.BuildAssetBundle(o, null, name + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.iPhone);
            
            BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/IOS/" + StoryAnimTemp.SceneName + "StoryAnim" + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.iPhone);
#endif

#if UNITY_STANDALONE_WIN
            string saveurl = "Assets/StreamingAssets/ScenceEditor/" + "WindowsEditor/" + StoryAnimTemp.SceneName + "/StoryAnim/";
            if (Directory.Exists(saveurl) == false)
                Directory.CreateDirectory(saveurl);
            string name = saveurl + StoryAnimTemp.SceneName + "StoryAnim";
            AssetDatabase.CreateAsset(StoryAnimData, name + ".asset");
            Object o = AssetDatabase.LoadAssetAtPath(name + ".asset", typeof(StoryAnimDataObj));

            BuildPipeline.BuildAssetBundle(o, null, name + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);

            BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/PC/" + StoryAnimTemp.SceneName + "StoryAnim" + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);
#endif 
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("", "保存完成", "OK");
        }
        private void DonePre()
        {
#if UNITY_IPHONE
			GameObject This = StoryAnimTemp.gameObject;
			PrefabUtility.CreatePrefab("Assets/StreamingAssets/ScenceEditor/" + "IPhonePlayer" + "/" + StoryAnimTemp.SceneName + "/StoryAnim/" + This.transform.name + ".prefab", This);
			GameObject temp = StoryAnimTemp.transform.GetChild(0).gameObject;
			temp.transform.name = StoryAnimTemp.SceneName + "StoryCamAnim";
			PrefabUtility.CreatePrefab("Assets/StreamingAssets/ScenceEditor/" + "IPhonePlayer" + "/" + StoryAnimTemp.SceneName + "/StoryAnim/" + temp.transform.name + ".prefab", temp);
#endif 
#if UNITY_STANDALONE_WIN
            GameObject This = StoryAnimTemp.gameObject;
            PrefabUtility.CreatePrefab("Assets/StreamingAssets/ScenceEditor/" + Application.platform.ToString() + "/" + StoryAnimTemp.SceneName + "/StoryAnim/" + This.transform.name + ".prefab", This);
            GameObject temp = StoryAnimTemp.transform.GetChild(0).gameObject;
            temp.transform.name = StoryAnimTemp.SceneName + "StoryCamAnim";
            PrefabUtility.CreatePrefab("Assets/StreamingAssets/ScenceEditor/" + Application.platform.ToString() + "/" + StoryAnimTemp.SceneName + "/StoryAnim/" + temp.transform.name + ".prefab", temp);
#endif 
        }
    }
}

