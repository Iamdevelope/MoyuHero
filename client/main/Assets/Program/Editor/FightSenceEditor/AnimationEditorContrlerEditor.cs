using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(AnimationEditorContrler))]
    public class AnimationEditorContrlerEditor : Editor
    {
        private AnimationEditorContrler Anim;

        private void OnEnable()
        {
            Anim = (AnimationEditorContrler)target;
           
        }
        public override void OnInspectorGUI()
        {

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("英雄ID");
            Anim.HeroID = EditorGUILayout.IntField(Anim.HeroID);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("SaveData"))
            {
                SaveData();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("读取动作名称");
            Anim.LoadAnimName = EditorGUILayout.TextField(Anim.LoadAnimName);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("LoadData"))
            {
                LoadData();
            }
            //if (GUILayout.Button("DelAllEvents"))
            //{
            //    DelAllEvents();
            //}
            EditorGUILayout.Space();
            if(GUILayout.Button("创建绑点空物体"))
            {
                CreatChild();
            }
        }
        //保存动作信息
        private void SaveData()
        {
            foreach(AnimationState item in Anim.transform.GetComponent<Animation>())
            {
                AnimationEvent[] Events = AnimationUtility.GetAnimationEvents(item.clip);
                if(Events.Length!=0)
                {
                    Debug.Log(item.clip.name);
                    AnimationDataObj AnimData = ScriptableObject.CreateInstance<AnimationDataObj>();
                    AnimData.HeroID = Anim.HeroID;
                    AnimData.AnimMame = item.clip.name;
                    AnimData.AnimationFuntionDataList = new System.Collections.Generic.List<AnimationFuntionData>();
                    Anim.AnimationFuntiondata = new AnimationFuntionData[Events.Length];
                    for (int i = 0; i < Events.Length; i++)
                    {
                        Anim.AnimationFuntiondata[i] = new AnimationFuntionData();
                        Anim.AnimationFuntiondata[i].FuntionTime = Events[i].time;
                        Anim.AnimationFuntiondata[i].FuntionName = Events[i].functionName;
                        Anim.AnimationFuntiondata[i].FuntionParameter = Events[i].stringParameter;
                        AnimData.AnimationFuntionDataList.Add(Anim.AnimationFuntiondata[i]);
                    }
                    string name = "Assets/Art/Characters/AnimationEvnents/" + Anim.transform.name + item.clip.name;
                    AssetDatabase.CreateAsset(AnimData, name + ".asset");
                }
                else
                {
                    string name = "Assets/Art/Characters/AllAnimationEventFile/" + Anim.transform.name + item.clip.name;
                    AssetDatabase.DeleteAsset(name+".asset");
                }
            }
        }
        //读取动作信息
        private void LoadData()
        {
            AnimationDataObj o = AssetDatabase.LoadAssetAtPath("Assets/Art/Characters/AnimationEvnents/" + Anim.transform.name + Anim.LoadAnimName + ".asset", typeof(AnimationDataObj)) as AnimationDataObj;
            AnimationEvent[] e = new AnimationEvent[o.AnimationFuntionDataList.Count];
            for(int i=0;i<o.AnimationFuntionDataList.Count;i++)
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
            AnimationUtility.SetAnimationEvents(Anim.transform.GetComponent<Animation>().GetClip(Anim.LoadAnimName), e);
        }
        //删除事件
        private void DelAllEvents()
        {
            foreach(AnimationState item in Anim.transform.GetComponent<Animation>())
            {
                AnimationEvent[] e = null;
                AnimationUtility.SetAnimationEvents(item.clip, e);
            }
        }
        //创建子物体
        private void CreatChild()
        {
            CreatChild("Bip001 Footsteps", "Bottom_EffectPoint");
            CreatChild("Bip001 Footsteps", "Special_HurtPoint");
            CreatChild(Anim.name, "Head_T_EffectPoint");
            CreatChild("Bip001 Spine1", "Chest_EffectPoint");
            CreatChild("Bip001 L Toe0", "Foot_L_EffectPoint");
            CreatChild("Bip001 R Toe0", "Foot_R_EffectPoint");
            CreatChild("Bip001 Head", "Head_C_EffectPoint");
            CreatChild("Bip001 L Hand", "Hand_L_EffectPoint");
            CreatChild("Bip001 R Hand", "Hand_R_EffectPoint");
            CreatChild("Bip001 Weapon", "Weapon01_EffectPoint");
            CreatChild("Bip001 Pelvis", "Hip_EffectPoint");
            CreatChild("Bip001 Spine1", "Normal_HurtPoint");
        }
        private void CreatChild(string TagName,string AddName)
        {
            foreach (Transform Tag in Anim.GetComponentsInChildren<Transform>())
            {
                if (Tag.name == TagName)
                {
                    GameObject obj = new GameObject(AddName);
                    obj.transform.parent = Tag;
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localEulerAngles = Vector3.zero;
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    break;
                }
            }
        }
    }
}

