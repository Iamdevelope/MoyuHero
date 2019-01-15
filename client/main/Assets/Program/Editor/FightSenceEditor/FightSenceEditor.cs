using UnityEngine;
using UnityEditor;
using System.Collections;
using DreamFaction.GameSceneEditorText;
using DreamFaction.GameCore;
namespace DreamFaction.GameSceneEditor
{
    public class FightSenceEditor : EditorWindow
    {
        [MenuItem("FightSenceEditor/场景编辑/策划特供版")]
        static void Init()
        {
            if (Camera.main!=null)
                Camera.main.fieldOfView = 50;
            FightSenceEditor window = (FightSenceEditor)EditorWindow.GetWindow(typeof(FightSenceEditor));
        }
        private void OnGUI()
        {
            if (GUI.Button(new Rect(80f, 20, 200f, 50f), "创建英雄场景线路编辑器"))
            {
                CreatHeroPathEditor();
            }
            if (GUI.Button(new Rect(80f, 260, 200f, 50f), "战斗模拟器"))
            {
                CreatFightEditor();
            }
            if (GUI.Button(new Rect(80f, 100, 200f, 50f), "创建摄像机场景线路编辑器"))
            {
                CreatCameraEditor();
            }
            if (GUI.Button(new Rect(80f, 180, 200f, 50f), "创建怪物组编辑组件"))
            {
                CreatMonsterEditor();
            }
            if (GUI.Button(new Rect(80f, 340, 200f, 50f), "创建阵型编辑器"))
            {
                CreatFormationEditor();
            }
        }
        private void CreatHeroPathEditor()
        {
            GameObject HeroPathtContrler = Instantiate(Resources.Load("HeroPathtContrler")) as GameObject;
            HeroPathtContrler.name = "HeroPathtContrler";
        }
        private void CreatFightEditor()
        {
            GameObject FightContrler = new GameObject("FightContrler");
            FightContrler.AddComponent<FightTempContrler>();
            FightContrler.AddComponent<FightEditorContrler>();
            FightContrler.AddComponent<AppManager>();
            GameObject HeroGroupContrler = new GameObject("HeroGroupContrler");
            HeroGroupContrler.AddComponent<HeroGroupContrler>();
            HeroGroupContrler.transform.parent = FightContrler.transform;
            GameObject MonsterGroupContrler = new GameObject("MonsterGroupContrler");
            MonsterGroupContrler.AddComponent<MonsterGroupContrler>();
            MonsterGroupContrler.transform.parent = FightContrler.transform;
        }
        private void CreatCameraEditor()
        {
            GameObject CameraContrler = new GameObject("CameraContrler");
            CameraContrler.AddComponent<CameraContrler>();
            CameraContrler.AddComponent<CameraData>();
            GameObject CamTriggerPointGroup = new GameObject("CamTriggerPointGroup");
            CamTriggerPointGroup.transform.parent = CameraContrler.transform;
            GameObject CamAnimationGroup = new GameObject("CamAnimationGroup");
            CamAnimationGroup.transform.parent = CameraContrler.transform;
            GameObject CamEnterPos = GameObject.CreatePrimitive(PrimitiveType.Cube);
            CamEnterPos.name = "CamEnterPos";
            CamEnterPos.transform.parent = CameraContrler.transform;
            GameObject CamCenter = GameObject.CreatePrimitive(PrimitiveType.Cube);
            CamCenter.name = "CamCenter";
            CamCenter.transform.parent = CameraContrler.transform;
            GameObject AnimationFollowCam = new GameObject("AnimationFollowCam");
            AnimationFollowCam.transform.parent = CameraContrler.transform;
        }
        private void CreatMonsterEditor()
        {
            GameObject MonsterGroup = new GameObject("MonsterGroupManager");
            MonsterGroup.AddComponent<MonsterGroupDataManager>();
        }
        private void CreatFormationEditor()
        {
            GameObject Formation = Instantiate(Resources.Load("Formation")) as GameObject;
            Formation.name = "Formation";
        }
    }

}
