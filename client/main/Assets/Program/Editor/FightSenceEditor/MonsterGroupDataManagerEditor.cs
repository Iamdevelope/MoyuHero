using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(MonsterGroupDataManager))]
    public class MonsterGroupDataManagerEditor : Editor
    {
        private MonsterGroupDataManager MonsterGroupDatamanager;

        private void OnEnable()
        {
            MonsterGroupDatamanager = (MonsterGroupDataManager)target;
        }
        public override void OnInspectorGUI()
        {
            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("怪物组信息导出名称");
            //MonsterGroupDatamanager.MonsterGroupName = EditorGUILayout.TextField(MonsterGroupDatamanager.MonsterGroupName);
            //EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("保存场景名称");
            string str = EditorApplication.currentScene;
            string scenename = str.Substring(str.LastIndexOf("/") + 1, str.LastIndexOf(".") - str.LastIndexOf("/") - 1);
            MonsterGroupDatamanager.SceneName = EditorGUILayout.TextField(scenename);
            EditorGUILayout.EndHorizontal();


            if (GUILayout.Button("创建怪物组出生点"))
            {
                GameObject monsterGroup = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                monsterGroup.name = "MonsterGroup";
                monsterGroup.transform.parent = MonsterGroupDatamanager.transform;
                monsterGroup.AddComponent<MonsterGroup>();
            }
            if (GUILayout.Button("Done"))
            {
                Done();
            }
        }
        private void Done()
        {
            //实例化monsterdata
            MonsterGroupDataObj MonsterGroupDataobj = ScriptableObject.CreateInstance<MonsterGroupDataObj>();
            //添加数据给Monstergroupdata
            MonsterGroupDataobj.MonsterGroupDataList = new List<MonstersGroupData>();
            for (int i = 0; i < MonsterGroupDatamanager.transform.childCount; i++)
            {
                MonsterGroupDataobj.MonsterGroupDataList.Add(MonsterGroupDatamanager.transform.GetChild(i).GetComponent<MonsterGroup>().MonsterGroupdata);
                MonsterGroupDataobj.MonsterGroupDataList[i].MonsterGroupdata = new List<Monsterdata>();
                for(int j=0;j<MonsterGroupDatamanager.transform.GetChild(i).childCount;j++)
                {
                    MonsterGroupDataobj.MonsterGroupDataList[i].MonsterGroupdata.Add(MonsterGroupDatamanager.transform.GetChild(i).GetChild(j).GetComponent<MonsterPoint>().Adddata());
                }
            }
            //SysData将创建为一个对象,这时在project面板上会看到这个对象
            //string saveurl = "Assets/StreamingAssets/ScenceEditor/" + Application.platform.ToString() + "/" + MonsterGroupDatamanager.SceneName + "/Monster/";
            //if (Directory.Exists(saveurl) == false)
            //    Directory.CreateDirectory(saveurl);
            //string name = saveurl + MonsterGroupDatamanager.SceneName + "Monster" + ".asset";
            //AssetDatabase.CreateAsset(MonsterGroupDataobj, name);
            //Object o = AssetDatabase.LoadAssetAtPath(name, typeof(MonsterGroupDataObj));
            //打包为SysData.assetbundle文件选择平台
#if UNITY_ANDROID
        Debug.Log("这里是安卓设备^_^");
        string saveurl = "Assets/StreamingAssets/ScenceEditor/" +"Android/" + MonsterGroupDatamanager.SceneName + "/Monster/";
        if (Directory.Exists(saveurl) == false)
            Directory.CreateDirectory(saveurl);
        string name = saveurl + MonsterGroupDatamanager.SceneName + "Monster" + ".asset";
        AssetDatabase.CreateAsset(MonsterGroupDataobj, name);
        Object o = AssetDatabase.LoadAssetAtPath(name, typeof(MonsterGroupDataObj));
        BuildPipeline.BuildAssetBundle(o, null, saveurl + MonsterGroupDatamanager.SceneName + "Monster" + ".assetbundle",
        BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);

        BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/Android/" + MonsterGroupDatamanager.SceneName + "Monster" + ".assetbundle",
        BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);
#endif

#if UNITY_IPHONE
        Debug.Log("这里是苹果设备>_<");
        string saveurl = "Assets/StreamingAssets/ScenceEditor/" +"IPhonePlayer/" + MonsterGroupDatamanager.SceneName + "/Monster/";
        if (Directory.Exists(saveurl) == false)
            Directory.CreateDirectory(saveurl);
        string name = saveurl + MonsterGroupDatamanager.SceneName + "Monster" + ".asset";
        AssetDatabase.CreateAsset(MonsterGroupDataobj, name);
        Object o = AssetDatabase.LoadAssetAtPath(name, typeof(MonsterGroupDataObj));
        BuildPipeline.BuildAssetBundle(o, null, saveurl + MonsterGroupDatamanager.SceneName + "Monster" + ".assetbundle",
        BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.iPhone);
                    
        BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/IOS/" + MonsterGroupDatamanager.SceneName + "Monster" + ".assetbundle",
        BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.iPhone);
#endif

#if UNITY_STANDALONE_WIN
            Debug.Log("我是从Windows的电脑上运行的T_T");
            string saveurl = "Assets/StreamingAssets/ScenceEditor/" +"WindowsEditor/" + MonsterGroupDatamanager.SceneName + "/Monster/";
            if (Directory.Exists(saveurl) == false)
                Directory.CreateDirectory(saveurl);
            string name = saveurl + MonsterGroupDatamanager.SceneName + "Monster" + ".asset";
            AssetDatabase.CreateAsset(MonsterGroupDataobj, name);
            Object o = AssetDatabase.LoadAssetAtPath(name, typeof(MonsterGroupDataObj));
            BuildPipeline.BuildAssetBundle(o, null, saveurl + MonsterGroupDatamanager.SceneName + "Monster" + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);

            BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/PC/" + MonsterGroupDatamanager.SceneName + "Monster" + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);
#endif 
            string SavePath = "Assets/CEHUAYONG/ScenesPreb/" + MonsterGroupDatamanager.SceneName + "/";
            if (Directory.Exists(SavePath) == false)
                Directory.CreateDirectory(SavePath);
            PrefabUtility.CreatePrefab(SavePath + MonsterGroupDatamanager.name + ".prefab", MonsterGroupDatamanager.gameObject);
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("", "保存完成", "OK");
            //删除面板上的临时对象
            //AssetDatabase.DeleteAsset(name);
        }
    }
}

