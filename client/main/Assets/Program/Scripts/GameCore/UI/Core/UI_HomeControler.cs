using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
namespace DreamFaction.UI.Core
{
    /// <summary>
    /// 主场景的UI控制器继承自BaseUIControler，用来控制主场景的UI加载，删除！
    /// </summary>
    public class UI_HomeControler : BaseUIControler
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static UI_HomeControler Inst;
        public static bool NeedShowWorldBossPanel = false;
        public static bool NeedShowMysteriousShop = false;
        public static bool NeedShowVipLvUpTips = false;
        public static bool NeedShowSignInPanel = false;
        public static bool NeedShowMedardGotoObj = false;

        public List<GameObject> homeObjects = new List<GameObject>();
        public List<GameObject> recruitObjects = new List<GameObject>();

        //private bool isRunningFrameDelayCoroutine = false;
        private sbyte m_FrameDelayCount;
        //public GameObject bag;
        // =====================  重载 ============================
        /// <summary>
        /// 1: 初始化
        /// </summary>
        protected override void InitData()
        {
 	        base.InitData();
            Inst = this;

            // 测试数据：
             AddUI(UI_MainHome.UI_ResPath);
             //AddUI(UI_MsgBox1.UI_ResPath).GetComponent<UI_MsgBox1>().AddMsg("您有一封信邮件，请注意查收！");
             //AddUI(UI_ZouMaDeng.UI_ResPath);
              //bag = AddUI(UI_Bag.UI_ResPath);
              //bag.SetActive(false);
             
        }
        protected override void InitView()
        {
            base.InitView();
            if (NeedShowMysteriousShop)
            {
                AddUI(UI_MysteriousShop.Path);
                NeedShowMysteriousShop = false;
            }

            if (NeedShowWorldBossPanel)
            {
//                var _go = AddUI(UI_TestPanel.GetPath());
//                _go.GetComponent<UI_TestPanel>().GotoWorldBoss();
//                _go = AddUI(UI_WorldBoss.GetPath());
                var ctrl = new WorldBossPanelController();
                ctrl.OpenWorldPanel(true);
                NeedShowWorldBossPanel = false;
            }
 //           AddUI(UI_SignInManager.Path);

        }

        protected override void UpdateView()
        {
            base.UpdateView();

            if (NeedShowVipLvUpTips && IsInHomeMain())
            {
                NeedShowVipLvUpTips = false;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_VipLevelUp); 
            }
            if (NeedShowSignInPanel && IsInHomeMain())
            {
                GameObject go = AddUI(UI_SignInManager.Path);
                if (go != null)
                {
                    go.GetComponent<UI_SignInManager>().InitSignInManager(true);
                    NeedShowSignInPanel = false;
                }

            }
            if (NeedShowMedardGotoObj && IsInHomeMain())
            {
                AddUI(UI_MedardGotoObj.UI_ResPath);
                NeedShowMedardGotoObj = false;
            }

            if (m_FrameDelayCount >0)//GameEventID.UI_InterfaceChange需要延迟2帧抛出，在此处倒计时
            {
                m_FrameDelayCount--;
                if (m_FrameDelayCount == 0)
                {
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_InterfaceChange);
                }
            }
            if(IsInHomeMain())
            {
                HomeControler.Inst.PlayFunly();
            }
        }



        
        // ====================== 公共接口 =========================
        public bool IsInHomeMain()
        {
            return canvasList[(int)UICanvasFlag.Canvas2].childCount <= 0 && canvasList[(int)UICanvasFlag.Canvas1].childCount <= 1;
        }
   
        /// <summary>
        /// idx 范围[0,3]
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Canvas GetCanvasByIdx(int idx)
        {
            return canvasList[idx].GetComponent<Canvas>();
        }

        public override GameObject AddUI(string UIName)
        {
            GameObject go = base.AddUI(UIName);
            OpenRecruitScene(UIName);
            FrameDelayControl();
            return go;
        }
        public override void ReMoveUI(string UIName)
        {
            base.ReMoveUI(UIName);
            LeaveRecruitScene(UIName);
            FrameDelayControl();
        }
        public override void ReMoveUI(GameObject UIobj)
        {
            base.ReMoveUI(UIobj);
            LeaveRecruitScene(UIobj.name);
            FrameDelayControl();
        }
        public BaseUI GetCanvas2LastChildBaseUI()
        {
            Transform trans = canvasList[(int)UICanvasFlag.Canvas2];
            if (trans.childCount > 0)
            {
                trans = trans.GetChild(trans.childCount - 1);
            }
            else
            {
                trans = null;
            }

            if (trans != null)
                return trans.GetComponent<BaseUI>();
            else
                return null;
        }
        public BaseUI GetCanvas2FirstChildBaseUI()
        {
            Transform trans = canvasList[(int)UICanvasFlag.Canvas2];
            if (trans.childCount > 0)
            {
                trans = trans.GetChild(0);
            }
            else
            {
                trans = null;
            }

            if (trans != null)
                return trans.GetComponent<BaseUI>();
            else
                return null;
        }
        public int GetCanvas2ChildCount()
        { 
            return canvasList[(int)UICanvasFlag.Canvas2].childCount;
        }

        /// <summary>
        /// 延迟2帧，抛出事件GameEventID.UI_InterfaceChange
        /// 在延迟未结束时重复调用会刷新延迟倒计时
        /// </summary>
        private void FrameDelayControl()
        {
            m_FrameDelayCount = 2;  //刷新延迟倒计时
            
            //if (!isRunningFrameDelayCoroutine)
            //{
            //    isRunningFrameDelayCoroutine = true;
            //    StartCoroutine(FrameDelay());
            //}
        }

        // ui 相关  进入招募
        void OpenRecruitScene(string name)
        {
            if (name == "UI_Home/UI_Recruit_2_0")
            {
                foreach(var item in AddedUIList)
                {
                    if (item.Key == "UI_Caption/UI_CaptionPanel_1_1" || item.Key == "UI_Home/UI_Recruit_2_0" || item.Key == "Guide/UI_Guide_0_5")
                        continue;
                    
                    item.Value.gameObject.SetActive(false);
                }

                for(int i = 0;i < homeObjects.Count; ++i)
                {
                    homeObjects[i].gameObject.SetActive(false);
                }

                for (int i = 0; i < recruitObjects.Count; ++i)
                {
                    recruitObjects[i].gameObject.SetActive(true);
                }
            }
        }

        // ui 相关  离开招募
        void LeaveRecruitScene(string name)
        {
            if (name == "UI_Home/UI_Recruit_2_0")
            {
                foreach (var item in AddedUIList)
                {
                    item.Value.gameObject.SetActive(true);
                }

                for (int i = 0; i < homeObjects.Count; ++i)
                {
                    homeObjects[i].gameObject.SetActive(true);
                }

                for (int i = 0; i < recruitObjects.Count; ++i)
                {
                    recruitObjects[i].gameObject.SetActive(false);
                }
            }
        }

        //private IEnumerator FrameDelay()
        //{
        //    while (m_FrameDelayCount > 0)
        //    {
        //        yield return null;
        //        m_FrameDelayCount--;
        //    }
        //    isRunningFrameDelayCoroutine = false;
        //    GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_InterfaceChange);
        //}
    }
}
