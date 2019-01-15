using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using DreamFaction.GameSceneEditor;
using DreamFaction.LogSystem;
namespace DreamFaction.GameSceneEditorText
{
    /// <summary>
    /// 测试用英雄控制器
    /// </summary>
    public class HeroGroupContrler : MonoBehaviour
    {
        private float Xcenter = 0;
        private float Zcenter = 0;
        private float Ycenter = 0;
        private Vector3 Centercount = new Vector3();

        /// <summary>
        /// 单例
        /// </summary>
        public static HeroGroupContrler inst;
        /// <summary>
        /// 英雄数组
        /// </summary>
        public List<GameObject> HerosList;
        /// <summary>
        /// 英雄中心点坐标
        /// </summary>
        public Vector3 HeroCenterPos;
        public int CountHero;
        private void Awake()
        {
            inst = this;
            HerosList = new List<GameObject>();
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_FightEditorLoadDone, FightBigan);
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_HeroPathMomentMoveExit, HeroPathMomentMoveExit);
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_HeroPathMomentMoveEnter, HeroPathMomentMoveEnter);
        }
        private void Start()
        {
          
        }
        private void Update()
        {
            HeroCenterUpdate();
        }
        private void HeroCenterUpdate()
        {
            HeroCenterPos = GetCenterCount(HerosList);
        }
        //战斗开始
        private void FightBigan()
        {
            LogManager.Log("战斗开始");
            //CreatHero();
            Invoke("CreatHero", 0.2f);
        }
        private void HeroPathMomentMoveExit()
        {
            Invoke("HeroWait", 3);
        }
        private void HeroWait()
        {
            for (int i = 0; i < HerosList.Count; ++i)
            {
                HerosList[i].GetComponent<ForllowPoint>().SetGo();
            }
            FightEditorContrler.GetInstantiate().HeroPathNormalMove();
        }
        private void HeroPathMomentMoveEnter()
        {
            for (int i = 0; i < HerosList.Count; ++i)
            {
                HerosList[i].GetComponent<ForllowPoint>().MomentMoveStop();
            }
            Invoke("HeroPathMomentMoveIng",3.0f);
        }
        private void HeroPathMomentMoveIng()
        {
            for (int i = 0; i < HerosList.Count; ++i)
            {
                HerosList[i].GetComponent<ForllowPoint>().MomentMove();
            }
            FightEditorContrler.GetInstantiate().HeroPathNormalMove();
            Invoke("HeroWait", 3);
        }
        private void CreatHero()
        {
            for (int i = 0; i < HeroPathtContrler.GetInstantiate().GetFormationObj().transform.childCount; i++)
            {
                GameObject hero = Instantiate(Resources.Load("Hero"), HeroPathtContrler.GetInstantiate().GetFormationObj().transform.GetChild(i).position, Quaternion.identity) as GameObject;
                hero.transform.parent=this.transform;
                hero.AddComponent<ForllowPoint>().Init(HeroPathtContrler.GetInstantiate().GetFormationObj().transform.GetChild(i));
                HerosList.Add(hero);
            }
            FightEditorContrler.GetInstantiate().CamPlay();
            FightEditorContrler.GetInstantiate().HeroPathPlay();
            FightEditorContrler.GetInstantiate().HeroPathNormalMove();
        }
        private Vector3 GetCenterCount(List<GameObject> tag)
        {
            Xcenter = 0;
            Zcenter = 0;
            Ycenter = 0;
            Centercount = Vector3.zero;
            if (tag.Count != 0 && tag != null)
            {
                for (int i = 0; i < tag.Count; i++)
                {
                    Xcenter += tag[i].transform.position.x;
                    Zcenter += tag[i].transform.position.z;
                    Ycenter += tag[i].transform.position.y;
                }
                Centercount = new Vector3(Xcenter / tag.Count, Ycenter / tag.Count, Zcenter / tag.Count);
            }
            return Centercount;
        }
        private void OnDestroy()
        {
            inst = this;
           // GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_FightEditorLoadDone);
        }
    }
}

