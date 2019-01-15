using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(CameraData))]
    public class CameraDataEditor : Editor
    {
        CameraData Cameracontrler;
        void OnEnable()
        {
            Cameracontrler = (CameraData)target;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("摄像机入场时间");
            Cameracontrler.CamEnterTime = EditorGUILayout.Slider(Cameracontrler.CamEnterTime, 0, 10);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("创建摄像机触发事件"))
            {
                GameObject CamTriggerPointID = new GameObject("CamTriggerPointID");
                CamTriggerPointID.transform.parent = Cameracontrler.transform.GetChild(0);
                CamTriggerPointID.transform.localPosition = Vector3.zero;
                GameObject cam = new GameObject("cam");
                cam.AddComponent<Camera>().fieldOfView = 50;
                cam.GetComponent<Camera>().enabled = false;
                cam.transform.parent = CamTriggerPointID.transform;
                CamTriggerPointID.AddComponent<CamTriggerEvent>().info = new Caminfo();
            }
            //if (GUILayout.Button("创建摄像机动画"))
            //{
            //    GameObject CameraAnimation = new GameObject("CameraAnimation");
            //    CameraAnimation.transform.parent = Cameracontrler.transform.GetChild(1);
            //    CameraAnimation.transform.localPosition = Vector3.zero;
            //    CameraAnimation.AddComponent<CamAnimContrler>();
            //    CameraAnimation.AddComponent<CameraPath>();
            //    CameraAnimation.AddComponent<CameraPathAnimator>().playOnStart = false;
            //}
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("保存场景名称");
            string str = EditorApplication.currentScene;
            string scenename = str.Substring(str.LastIndexOf("/") + 1, str.LastIndexOf(".") - str.LastIndexOf("/")-1);
            Cameracontrler.ScenceName = EditorGUILayout.TextField(scenename);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("DONE"))
            {
                //PrefabUtility.CreatePrefab("Assets/Resources/Done/" + Cameracontrler.Name + ".prefab", Cameracontrler.gameObject);

                //实例化monsterdata
                CameraDataObj CamData = ScriptableObject.CreateInstance<CameraDataObj>();
                //添加数据给Camdata
                CamData.CamPointdataList = new List<Caminfo>();
                Transform CamTriggerPointGroup = Cameracontrler.transform.FindChild("CamTriggerPointGroup");
                Transform CamEnterPos = Cameracontrler.transform.FindChild("CamEnterPos");
                Transform CamCenter = Cameracontrler.transform.FindChild("CamCenter");
                Transform CamAnimationGroup=Cameracontrler.transform.FindChild("CamAnimationGroup");
                for (int i = 0; i < CamTriggerPointGroup.childCount; i++)
                {
                    CamData.CamPointdataList.Add(CamTriggerPointGroup.GetChild(i).GetComponent<CamTriggerEvent>().info);
                }
                CamData.CamEnterPos = CamEnterPos.position;
                CamData.CamCenter = CamCenter.position;
                CamData.CamEnterTime = Cameracontrler.CamEnterTime;
                CamData.CameraPos = Camera.main.transform.position;
                CamData.CameraAngles = Camera.main.transform.eulerAngles;
                CamData.CamAnimationsCount = CamAnimationGroup.childCount;
                //SysData将创建为一个对象,这时在project面板上会看到这个对象
#if UNITY_ANDROID
        Debug.Log("FUCK!!!!!这里是安卓设备");
        string saveurl = "Assets/StreamingAssets/ScenceEditor/" + "Android/" + Cameracontrler.ScenceName + "/Camera/";
        if (Directory.Exists(saveurl) == false)
            Directory.CreateDirectory(saveurl);
        string name = saveurl + Cameracontrler.ScenceName + "Cam" + ".asset";
        AssetDatabase.CreateAsset(CamData, name);
        Object o = AssetDatabase.LoadAssetAtPath(name, typeof(CameraDataObj));
        BuildPipeline.BuildAssetBundle(o, null, saveurl + Cameracontrler.ScenceName + "Cam" + ".assetbundle"
        , BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);

        BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/Android/" + Cameracontrler.ScenceName + "Cam" + ".assetbundle"
         , BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);
#endif

#if UNITY_IPHONE
        Debug.Log("FUCK!!!!!这里是苹果设备");
        string saveurl = "Assets/StreamingAssets/ScenceEditor/" + "IPhonePlayer/" + Cameracontrler.ScenceName + "/Camera/";
        if (Directory.Exists(saveurl) == false)
            Directory.CreateDirectory(saveurl);
        string name = saveurl + Cameracontrler.ScenceName + "Cam" + ".asset";
        AssetDatabase.CreateAsset(CamData, name);
        Object o = AssetDatabase.LoadAssetAtPath(name, typeof(CameraDataObj));
        BuildPipeline.BuildAssetBundle(o, null, saveurl + Cameracontrler.ScenceName + "Cam" + ".assetbundle"
        , BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.iPhone);

        BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/IOS/" + Cameracontrler.ScenceName + "Cam" + ".assetbundle"
        , BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.iPhone);
#endif

#if UNITY_STANDALONE_WIN
                Debug.Log("FUCK!!!!!这是PC设备");
        string saveurl = "Assets/StreamingAssets/ScenceEditor/" + "WindowsEditor/" + Cameracontrler.ScenceName + "/Camera/";
        if (Directory.Exists(saveurl) == false)
            Directory.CreateDirectory(saveurl);
        string name = saveurl + Cameracontrler.ScenceName + "Cam" + ".asset";
        AssetDatabase.CreateAsset(CamData, name);
        Object o = AssetDatabase.LoadAssetAtPath(name, typeof(CameraDataObj));
        BuildPipeline.BuildAssetBundle(o, null, saveurl + Cameracontrler.ScenceName + "Cam" + ".assetbundle"
            , BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);

        BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/PC/" + Cameracontrler.ScenceName + "Cam" + ".assetbundle"
        , BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);
#endif   
                string SavePath = "Assets/CEHUAYONG/ScenesPreb/" + Cameracontrler.ScenceName + "/";
                if (Directory.Exists(SavePath) == false)
                    Directory.CreateDirectory(SavePath);
                PrefabUtility.CreatePrefab(SavePath + Cameracontrler.name + ".prefab", Cameracontrler.gameObject);
                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("", "保存完成", "OK");
                //删除面板上的临时对象
                //AssetDatabase.DeleteAsset(name);
            }
        }
    }
}

