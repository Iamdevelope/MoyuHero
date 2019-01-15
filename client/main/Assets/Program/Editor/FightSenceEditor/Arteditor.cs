using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using DreamFaction.GameSceneEditor;
using DreamFaction.GameSceneEditorText;
public class ArtEditor : EditorWindow 
{
    private StreamWriter sw;
    [MenuItem("FightSenceEditor/场景编辑/美术特供版")]
    private static void Init()
    {
        ArtEditor window = (ArtEditor)EditorWindow.GetWindow(typeof(ArtEditor));
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(80f, 20, 200f, 50f), "首字母大写"))
        {
            Capital();
        }
        if (GUI.Button(new Rect(350, 20, 200f, 50f), "删除动画事件"))
        {
            DelAllEvents();
        }
        if (GUI.Button(new Rect(350, 100, 200f, 50f), "读取动画事件"))
        {
            LoadAnimData();
        }
        if (GUI.Button(new Rect(80f, 100, 200f, 50f), "动作序列化文件生成TXT"))
        {
            GetNames();
        }
        if (GUI.Button(new Rect(80f, 180, 200f, 50f), "保存动作信息"))
        {
            SaveAnimData window = (SaveAnimData)EditorWindow.GetWindow(typeof(SaveAnimData));
        }
        if (GUI.Button(new Rect(350, 180, 200f, 50f), "创建空绑点"))
        {
            CreatChildren();
        }
        if (GUI.Button(new Rect(350, 260, 200f, 50f), "过场动画编辑器"))
        {
            CreatStoryAnim();
        }
    }
    private void Capital()
    {
        Object[] slecets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object item in slecets)
        {
            string path = AssetDatabase.GetAssetPath(item);
            string Path = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.name);
            AssetDatabase.RenameAsset(path, Path);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("", "完成", "OK");
    }
   //[MenuItem("Art/动作序列化文件生成TXT")]
   // private static void AnimationToTxt()
   // {
   //    Object[] slecets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
   //    FileInfo t = new FileInfo(Application.dataPath + "//" + "34_animationevent" + ".txt");
   //    sw = t.CreateText();
   //    //写入表头
   //    WriteValue("ID");
   //    WriteValue("playerID");
   //    WriteValue("AnimName");
   //    WriteValue("HitTime");
   //    WriteValue("FunctionName");
   //    WriteValue("Param 1");
   //    WriteValue("Param 2");
   //    WriteValue("Param 3");
   //    WriteValue("Param 4");
   //    WriteValue("Param 5");
   //    sw.Write("\r");

   //    WriteValue("索引");
   //    WriteValue("英雄/怪物ID");
   //    WriteValue("动作名称");
   //    WriteValue("命中时间");
   //    WriteValue("回调函数");
   //    WriteValue("参数1");
   //    WriteValue("参数2");
   //    WriteValue("参数3");
   //    WriteValue("参数4");
   //    WriteValue("参数5");
   //    sw.Write("\r");

   //    WriteValue("int");
   //    WriteValue("int");
   //    WriteValue("string");
   //    WriteValue("float");
   //    WriteValue("string");
   //    WriteValue("string");
   //    WriteValue("string");
   //    WriteValue("string");
   //    WriteValue("string");
   //    WriteValue("string");
   //    sw.Write("\r");
   //    int count = 0;
   //    foreach (Object item in slecets)
   //    {
   //        AnimationDataObj o = item as AnimationDataObj;
   //        for (int i = 0; i < o.AnimationFuntionDataList.Count; ++i)
   //        {
   //            WriteValue(count);
   //            count++;
   //            WriteValue(o.HeroID);
   //            WriteValue(o.AnimMame);
   //            WriteValue(o.AnimationFuntionDataList[i].FuntionTime);
   //            WriteValue(o.AnimationFuntionDataList[i].FuntionName);
   //            if (o.AnimationFuntionDataList[i].FuntionParameter != string.Empty)
   //            {
   //                WriteValue(o.AnimationFuntionDataList[i].FuntionParameter);
   //            }
   //            else
   //            {
   //                WriteValue(-1);
   //            }
   //            WriteValue(-1);
   //            WriteValue(-1);
   //            WriteValue(-1);
   //            WriteValue(-1);
   //            sw.Write("\r");
   //        }
   //    }
   //    //关闭流
   //    sw.Close();
   //    //销毁流
   //    sw.Dispose();
   // }
   private void WriteValue(object value)
   {
       sw.Write(value+"\t");
   }
   private void GetNames()
   {
       string url = Application.dataPath + "/Art/Characters/AnimationEvnents/";
       if (Directory.Exists(url) == false)
       {
           EditorUtility.DisplayDialog("", "无效目录", "确定");
           return;
       }
       FileInfo t = new FileInfo(Application.dataPath + "/StreamingAssets/CSV/" + "48_animationevent" + ".txt");
       sw = t.CreateText();
       //写入表头
       WriteValue("id");
       WriteValue("playerID");
       WriteValue("AnimName");
       WriteValue("HitTime");
       WriteValue("FunctionName");
       WriteValue("Param1");
       WriteValue("Param2");
       WriteValue("Param3");
       WriteValue("Param4");
       WriteValue("Param5");
       sw.Write("\r");

       WriteValue("索引");
       WriteValue("英雄/怪物ID");
       WriteValue("动作名称");
       WriteValue("命中时间");
       WriteValue("回调函数");
       WriteValue("参数1");
       WriteValue("参数2");
       WriteValue("参数3");
       WriteValue("参数4");
       WriteValue("参数5");
       sw.Write("\r");

       WriteValue("int");
       WriteValue("int");
       WriteValue("string");
       WriteValue("float");
       WriteValue("string");
       WriteValue("string");
       WriteValue("string");
       WriteValue("string");
       WriteValue("string");
       WriteValue("string");
       sw.Write("\r");
       int count = 0;
       string[] files = Directory.GetFiles(url);
       foreach(string item in files)
       {
           if(item.Contains("meta"))
           {
               continue;
           }
           else
           {
               string[] names = item.Split('/');
               AnimationDataObj o = AssetDatabase.LoadAssetAtPath("Assets/Art/Characters/AnimationEvnents/" + names[names.Length - 1], typeof(AnimationDataObj)) as AnimationDataObj;
               for (int i = 0; i < o.AnimationFuntionDataList.Count; ++i)
               {
                   WriteValue(count);
                   count++;
                   WriteValue(o.HeroID);
                   WriteValue(o.AnimMame);
                   WriteValue(o.AnimationFuntionDataList[i].FuntionTime);
                   WriteValue(o.AnimationFuntionDataList[i].FuntionName);
                   if (o.AnimationFuntionDataList[i].FuntionParameter != string.Empty)
                   {
                       WriteValue(o.AnimationFuntionDataList[i].FuntionParameter);
                   }
                   else
                   {
                       WriteValue(-1);
                   }
                   WriteValue(-1);
                   WriteValue(-1);
                   WriteValue(-1);
                   WriteValue(-1);
                   sw.Write("\r");
               }
           }
       }
       //关闭流
       sw.Close();
       //销毁流
       sw.Dispose();
       AssetDatabase.Refresh();
       EditorUtility.DisplayDialog("", "生成完成", "OK");
   }
   private void DelAllEvents()
   {
       GameObject[] TagObjs = Selection.gameObjects as GameObject[];
       foreach (GameObject item in TagObjs)
       {
           if (item.transform.GetComponent<Animation>()==null)
           {
               EditorUtility.DisplayDialog("",item.name+"无动画组件", "确定");
               continue;
           }
           foreach (AnimationState state in item.transform.GetComponent<Animation>())
           {
               AnimationEvent[] e = null;
               AnimationUtility.SetAnimationEvents(state.clip, e);
           }
       }
       AssetDatabase.Refresh();
       EditorUtility.DisplayDialog("", "完成", "OK");
   }
   private void LoadAnimData()
   {
       GameObject[] TagObjs = Selection.gameObjects as GameObject[];
       string url = Application.dataPath + "/Art/Characters/AnimationEvnents/";
       if (Directory.Exists(url) == false)
       {
           EditorUtility.DisplayDialog("", "无效目录", "确定");
           return;
       }
       string[] files = Directory.GetFiles(url);
       foreach(string item in files)
       {
           if (item.Contains("meta"))
           {
               continue;
           }
           else
           {
               foreach (GameObject itemobj in TagObjs)
               {
                   if (itemobj.transform.GetComponent<Animation>() == null)
                   {
                       continue;
                   }
                   if(item.Contains(itemobj.transform.name))
                   {
                       string[] names = item.Split('/');
                       string name = item.Substring(item.LastIndexOf("/") + 1, item.LastIndexOf(".") - item.LastIndexOf("/") - 1);
                       string name1 = name.Substring(itemobj.name.Length);
                       AnimationDataObj o = AssetDatabase.LoadAssetAtPath("Assets/Art/Characters/AnimationEvnents/" + names[names.Length - 1], typeof(AnimationDataObj)) as AnimationDataObj;
                       AnimationEvent[] e = new AnimationEvent[o.AnimationFuntionDataList.Count];
                       for (int i = 0; i < o.AnimationFuntionDataList.Count; i++)
                       {
                           AnimationEvent even = new AnimationEvent();
                           even.time = o.AnimationFuntionDataList[i].FuntionTime;
                           even.functionName = o.AnimationFuntionDataList[i].FuntionName;
                           if (o.AnimationFuntionDataList[i].FuntionParameter == string.Empty)
                               Debug.Log("无参数");
                           else
                               even.stringParameter = o.AnimationFuntionDataList[i].FuntionParameter;
                           e[i] = even;
                       }
                       if (itemobj.transform.GetComponent<Animation>().GetClip(name1) != null)
                           AnimationUtility.SetAnimationEvents(itemobj.transform.GetComponent<Animation>().GetClip(name1), e);
                       else
                           EditorUtility.DisplayDialog("", "未找动作" + name1, "确定");
                   }
               }
           }
       }
       AssetDatabase.Refresh();
       EditorUtility.DisplayDialog("", "资源加密完成", "OK");
   }
   private void CreatChildren()
   {
       GameObject[] TagObjs = Selection.gameObjects as GameObject[];
       foreach (GameObject item in TagObjs)
       {
           CreatChild(item, item.name, "Bottom_EffectPoint");
           CreatChild(item, "Bip001 Footsteps", "Special_HurtPoint");
           CreatChild(item, "Bip001 Footsteps", "Footsteps_EffectPoint");
           CreatChild(item, item.name, "Head_T_EffectPoint");
           CreatChild(item, "Bip001 Spine1", "Chest_EffectPoint");
           CreatChild(item, "Bip001 L Toe0", "Foot_L_EffectPoint");
           CreatChild(item, "Bip001 R Toe0", "Foot_R_EffectPoint");
           CreatChild(item, "Bip001 Head", "Head_C_EffectPoint");
           CreatChild(item, "Bip001 L Hand", "Hand_L_EffectPoint");
           CreatChild(item, "Bip001 R Hand", "Hand_R_EffectPoint");
           CreatChild(item, "Bip001 Weapon", "Weapon01_EffectPoint");
           CreatChild(item, "Bip001 Weapon", "Weapon02_EffectPoint");
           CreatChild(item, "Bip001 Pelvis", "Hip_EffectPoint");
           CreatChild(item, "Bip001 Spine1", "Normal_HurtPoint");
       }
       AssetDatabase.Refresh();
       EditorUtility.DisplayDialog("", "创建完成", "OK");
   }
   private static void CreatChild(GameObject tag, string TagName, string AddName)
   {
       foreach (Transform Tag in tag.GetComponentsInChildren<Transform>())
       {
           if (Tag.name == TagName)
           {
               GameObject obj = new GameObject(AddName);
               obj.transform.parent = Tag;
               obj.transform.localPosition = Vector3.zero;
               obj.transform.localEulerAngles = Vector3.zero;
               obj.transform.localScale = new Vector3(1, 1, 1);
               return;
           }
       }
       EditorUtility.DisplayDialog("", tag.name + "无" + TagName + "绑点", "确定");
   }
   private void CreatStoryAnim()
   {
       GameObject StoryTempContrler = new GameObject("StoryTempContrler");
       StoryTempContrler.AddComponent<StoryAnimTempContrler>();
       StoryTempContrler.AddComponent<StoryAnimEditorContrler>();
       GameObject StoryCamAnims = new GameObject("StoryCamAnims");
       StoryCamAnims.AddComponent<Animation>();
       StoryCamAnims.transform.parent = StoryTempContrler.transform;
   }
}
public class SaveAnimData : EditorWindow
{
    private int HeroID;
    private AnimationFuntionData[] AnimationFuntiondata;
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("输入英雄资源ID");
        HeroID = EditorGUILayout.IntField(HeroID);
        EditorGUILayout.EndHorizontal();

        if (GUI.Button(new Rect(50, 50, 200f, 50), "SAVE"))
        {
            SaveData();
        }
    }
    private void SaveData()
    {
        GameObject[] TagObjs = Selection.gameObjects as GameObject[];
        foreach (GameObject item in TagObjs)
        {
            if (item.transform.GetComponent<Animation>()==null)
            {
                EditorUtility.DisplayDialog("", item.name+"无动画组件", "确定");
                continue;
            }
            foreach (AnimationState state in item.transform.GetComponent<Animation>())
            {
                AnimationEvent[] Events = AnimationUtility.GetAnimationEvents(state.clip);
                string saveurl = "Assets/Art/Characters/AnimationEvnents/";
                if (Directory.Exists(saveurl) == false)
                    Directory.CreateDirectory(saveurl);
                if (Events.Length != 0)
                {
                    Debug.Log(state.clip.name);
                    AnimationDataObj AnimData = ScriptableObject.CreateInstance<AnimationDataObj>();
                    AnimData.HeroID = HeroID;
                    AnimData.AnimMame = state.clip.name;
                    AnimData.AnimationFuntionDataList = new System.Collections.Generic.List<AnimationFuntionData>();
                    AnimationFuntiondata = new AnimationFuntionData[Events.Length];
                    for (int i = 0; i < Events.Length; i++)
                    {
                        AnimationFuntiondata[i] = new AnimationFuntionData();
                        AnimationFuntiondata[i].FuntionTime = Events[i].time;
                        AnimationFuntiondata[i].FuntionName = Events[i].functionName;
                        AnimationFuntiondata[i].FuntionParameter = Events[i].stringParameter;
                        AnimData.AnimationFuntionDataList.Add(AnimationFuntiondata[i]);
                    }
                    string name = saveurl + item.name + state.clip.name;
                    AssetDatabase.CreateAsset(AnimData, name + ".asset");
                }
                else
                {
                    string name = saveurl + item.transform.name + state.clip.name;
                    AssetDatabase.DeleteAsset(name + ".asset");
                }
            }
        }
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("", "保存完成", "OK");
    }
}
