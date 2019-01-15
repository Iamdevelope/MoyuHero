using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Xml;
using DreamFaction.Utils;
using System.Text;
namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(HeroPathData))]
    public class HeroPathDataEditor : Editor
    {
        HeroPathData Heropathtcontrler;
        private void OnEnable()
        {
            Heropathtcontrler = (HeroPathData)target;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("保存场景名称");
            string str = EditorApplication.currentScene;
            string scenename = str.Substring(str.LastIndexOf("/") + 1, str.LastIndexOf(".") - str.LastIndexOf("/") - 1);
            Heropathtcontrler.SceneName = EditorGUILayout.TextField(scenename);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Heor阵型移动速度");
            Heropathtcontrler.MoveSpeed = EditorGUILayout.Slider(Heropathtcontrler.MoveSpeed, 0, 100);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("前排死亡对方移动相对距离");
            Heropathtcontrler.MoveDistance = EditorGUILayout.FloatField(Heropathtcontrler.MoveDistance);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("DONE"))
            {
                ExportData();
                ExportXml();
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if (GUILayout.Button("Load"))
            {
                LoadHeroData window = (LoadHeroData)EditorWindow.GetWindow(typeof(LoadHeroData));
               // LoadXml();
            }
        }
        private void  LoadXml()
        {
            //XmlDocument xml = new XmlDocument();
            //XMLHelper.LoadXML("Assets/StreamingAssets/ScenceEditor/PC/" + Heropathtcontrler.SceneName + "/Path/" + Heropathtcontrler.SceneName + "Path.xml", ref xml);
            //if(xml==null)
            //{
            //    EditorUtility.DisplayDialog("", "路径不存在文件", "确定");
            //    return;
            //}
            string str = File.ReadAllText(@"Assets/StreamingAssets/ScenceEditor/PC/" + Heropathtcontrler.SceneName + "/Path/" + Heropathtcontrler.SceneName + "Path.xml", Encoding.UTF8); 
            Heropathtcontrler.transform.FindChild("HerosPath").GetComponent<CameraPath>().FromXML(str);
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("", "加载完成", "OK");
        }
        //生成Xml
        private void ExportXml()
        {
            string saveurl = "Assets/StreamingAssets/ScenceEditor/PathXml/";
            if (Directory.Exists(saveurl) == false)
                Directory.CreateDirectory(saveurl);
            string defaultName = Heropathtcontrler.name;
            defaultName.Replace(" ", "_");
            string filepath = EditorUtility.SaveFilePanel("Export Camera Path Animator to XML", saveurl, Heropathtcontrler.SceneName + "Path", "xml");

            if (filepath != "")
            {
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    sw.Write(Heropathtcontrler.GetComponentInChildren<CameraPath>().ToXML());//write out contents of data to XML
                }
            }
        }
        //生成数据
        private void ExportData()
        {
            HeroPathDataObj HeroPathdata = ScriptableObject.CreateInstance<HeroPathDataObj>();
            //添加数据给HeroPathdata
            HeroPathdata.InitPos = Heropathtcontrler.GetComponentInChildren<CameraPath>().GetPoint(0).worldPosition;
            HeroPathdata.Tension = Heropathtcontrler.GetComponentInChildren<CameraPath>().hermiteTension;
            HeroPathdata.HeroPathWaitTime = Heropathtcontrler.Waittime;
            HeroPathdata.LineUpFollowCamPos = GameObject.Find("LineUpFollowCam").transform.localPosition;
            HeroPathdata.FightFollowCamPos = GameObject.Find("FightFollowCam").transform.localPosition;
          //  HeroPathdata.FightDefaultCamPos = GameObject.Find("FightDefaultCam").transform.localPosition;
            HeroPathdata.InitAngles = GameObject.Find("FormationCenter").transform.rotation;
            HeroPathdata.MoveDistance = Heropathtcontrler.MoveDistance;
#if UNITY_ANDROID
        Debug.Log("这里是安卓设备^_^");
        string saveurl = "Assets/StreamingAssets/ScenceEditor/" + "Android/" + Heropathtcontrler.SceneName + "/Path/";
        if (Directory.Exists(saveurl) == false)
            Directory.CreateDirectory(saveurl);
        string name = saveurl + Heropathtcontrler.SceneName + "Path" + ".asset";
        AssetDatabase.CreateAsset(HeroPathdata, name);
        Object o = AssetDatabase.LoadAssetAtPath(name, typeof(HeroPathDataObj));
        BuildPipeline.BuildAssetBundle(o, null, saveurl + Heropathtcontrler.SceneName + "Path" + ".assetbundle",
        BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);

        BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/Android/" + Heropathtcontrler.SceneName + "Path" + ".assetbundle",
        BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);
#endif

#if UNITY_IPHONE
        Debug.Log("这里是苹果设备>_<");
        string saveurl = "Assets/StreamingAssets/ScenceEditor/" + "IPhonePlayer/" + Heropathtcontrler.SceneName + "/Path/";
        if (Directory.Exists(saveurl) == false)
            Directory.CreateDirectory(saveurl);
        string name = saveurl + Heropathtcontrler.SceneName + "Path" + ".asset";
        AssetDatabase.CreateAsset(HeroPathdata, name);
        Object o = AssetDatabase.LoadAssetAtPath(name, typeof(HeroPathDataObj));
        BuildPipeline.BuildAssetBundle(o, null, saveurl + Heropathtcontrler.SceneName + "Path" + ".assetbundle",
        BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.iPhone);

        BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/IOS/" + Heropathtcontrler.SceneName + "Path" + ".assetbundle",
        BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.iPhone);
#endif

#if UNITY_STANDALONE_WIN
            Debug.Log("我是从Windows的电脑上运行的T_T");
            string saveurl = "Assets/StreamingAssets/ScenceEditor/" + "WindowsEditor/" + Heropathtcontrler.SceneName + "/Path/";
            if (Directory.Exists(saveurl) == false)
                Directory.CreateDirectory(saveurl);
            string name = saveurl + Heropathtcontrler.SceneName + "Path" + ".asset";
            AssetDatabase.CreateAsset(HeroPathdata, name);
            Object o = AssetDatabase.LoadAssetAtPath(name, typeof(HeroPathDataObj));
            BuildPipeline.BuildAssetBundle(o, null, saveurl + Heropathtcontrler.SceneName + "Path" + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);

            BuildPipeline.BuildAssetBundle(o, null, "Assets/StreamingAssets/AssetBundle/PC/" + Heropathtcontrler.SceneName + "Path" + ".assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);
#endif 
            string SavePath = "Assets/CEHUAYONG/ScenesPreb/" + Heropathtcontrler.SceneName + "/";
            if (Directory.Exists(SavePath) == false)
                Directory.CreateDirectory(SavePath);
            PrefabUtility.CreatePrefab(SavePath + Heropathtcontrler.name + ".prefab", Heropathtcontrler.gameObject);
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("", "保存完成", "OK");
            //删除面板上的临时对象
            //AssetDatabase.DeleteAsset(name);
        }
    }
    public class LoadHeroData : EditorWindow
    {
        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("你确定读取吗");
            EditorGUILayout.EndHorizontal();
            if (GUI.Button(new Rect(50, 50, 200f, 50), "确定"))
            {
                LoadXml();
            }
        }
        private void LoadXml()
        {
            string str = EditorApplication.currentScene;
            string scenename = str.Substring(str.LastIndexOf("/") + 1, str.LastIndexOf(".") - str.LastIndexOf("/") - 1);
            //XmlDocument xml = new XmlDocument();
            //XMLHelper.LoadXML("Assets/StreamingAssets/ScenceEditor/PathXml/" + scenename + "Path.xml", ref xml);
            //if (xml == null)
            //{
            //    EditorUtility.DisplayDialog("", "路径不存在文件", "确定");
            //    return;
            //}
            string strPath = File.ReadAllText(@"Assets/StreamingAssets/ScenceEditor/PathXml/" + scenename + "Path.xml", Encoding.UTF8);
            GameObject.Find("HerosPath").GetComponent<CameraPath>().FromXML(strPath);
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("", "加载完成", "OK");
        }
    }
}
