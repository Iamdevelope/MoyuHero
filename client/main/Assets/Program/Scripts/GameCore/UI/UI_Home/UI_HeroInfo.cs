using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork.Data;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DG.Tweening;

namespace DreamFaction.UI
{
    /// <summary>
    /// 人物信息界面，继承自BaseUI
    /// </summary>
    public class UI_HeroInfo : BaseUI
    {
        public static UI_HeroInfo _instance;

        public static string UI_ResPath = "UI_Home/UI_HeroInfo_3_2";
       // public List<UI_TitleBtnItem> TitleBtnList = new List<UI_TitleBtnItem>();        //标题按钮列表
      //  public List<ObjectCard> heroList;
        private Button Back_btn;//返回按钮
        private bool isInit = true;

        private Transform MsgBoxGroup;

        private GameObject m_HeroInfoTipsImage;
        private GameObject m_RuneTipsImage;
        private GameObject m_AttributeTipsImage;
        private GameObject m_SkillUpImage;
        private GameObject m_LevelUpTipsImage;
        private GameObject m_UpgradeTipsImage;
        private IFunctionTipsController m_TipsController;

        public Transform m_captionPoston;//常驻跑马灯位置

        UI_HeroRuneManager mUIHeroRuneMgr = null;

        UI_HeroRuneManager HRMgr
        {
            get
            {
                if (mUIHeroRuneMgr == null)
                {
                    GameObject go = (GameObject)Instantiate(UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_HeroInfo/Rune"));
                    if (go != null)
                    {
                        go.transform.parent = this.transform;
                        go.transform.localPosition = new Vector3(-1115,285,0);
                        go.transform.localScale = Vector3.one;
                        mUIHeroRuneMgr = go.GetComponent<UI_HeroRuneManager>();
                    }

                }
                return mUIHeroRuneMgr;
            }
        }
        UI_SkillUpManager mUISkillUpMgr = null;
        UI_SkillUpManager SUMgr
        {
            get
            {
                if (mUISkillUpMgr == null)
                {
                    GameObject go = (GameObject)Instantiate(UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_HeroInfo/SkillUP"));
                    if (go != null)
                    {
                        go.transform.parent = this.transform;
                        go.transform.localPosition = Vector3.zero;
                        go.transform.localScale = Vector3.one;
                        mUISkillUpMgr = go.GetComponent<UI_SkillUpManager>();
                    }
                }
                return mUISkillUpMgr;
            }
        }


        UI_AttributeManager mUIAttributeMgr = null;
        UI_AttributeManager ABMgr
        {
            get
            {
                if (mUIAttributeMgr == null)
                {
                    GameObject go = (GameObject)Instantiate(UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_HeroInfo/Attribute"));
                    if (go != null)
                    {
                        go.transform.parent = this.transform;
                        go.transform.localPosition = new Vector3(-730,-80,0);
                        go.transform.localScale = Vector3.one;
                        mUIAttributeMgr = go.GetComponent<UI_AttributeManager>();
                    }
                }
                return mUIAttributeMgr;
            }
        }
       


        // =====================  ========================
        public override void InitUIData()
        {
            _instance = this;
            base.InitUIData();

            //m_HeroInfoTipsImage = selfTransform.FindChild("TopPanel/BtnGroup/TitleButton_0/TipsImage").gameObject;
            //m_RuneTipsImage = selfTransform.FindChild("TopPanel/BtnGroup/TitleButton_1/TipsImage").gameObject;
            //m_AttributeTipsImage = selfTransform.FindChild("TopPanel/BtnGroup/TitleButton_2/TipsImage").gameObject;
            //m_SkillUpImage = selfTransform.FindChild("TopPanel/BtnGroup/TitleButton_3/TipsImage").gameObject;
            //m_LevelUpTipsImage = selfTransform.FindChild("HeroInfo/LevelUP_btn/TipsImage").gameObject;
            //m_UpgradeTipsImage = selfTransform.FindChild("HeroInfo/OrderUP_btn/TipsImage").gameObject;

            Back_btn = selfTransform.FindChild("TopPanel/TopTittle/BackBtn").GetComponent<Button>();
            MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
            //m_captionPoston = selfTransform.FindChild("captionPostion");
            //事件监听
            Back_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));

            ////m_TipsController = CreateFunctionTipsController();
            //HomeControler.Inst.PushFunly(0, 68);
            //GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, RefreshTipsController);
            //GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroLevelUpSucceed, RefreshTipsController);
            //GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroBeginnerUpdateShow, RefreshTipsController);
        }
        protected void OnDestroy()
        {
            //GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, RefreshTipsController);
            //GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_HeroLevelUpSucceed, RefreshTipsController);
            //GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_HeroBeginnerUpdateShow, RefreshTipsController);
        }
        public override void InitUIView()
        {
            base.InitUIView();

            ////设置常驻跑马灯位置
            //UI_CaptionManager cap = UI_CaptionManager.GetInstance();
            //if(cap!=null)
            //  cap.AwakeUp(m_captionPoston);

            //HeroModelBack.Inst.ChangePanel("HeroBg");
        }

        public void DefeatShow(int id)
        {
            //SelectedShow(0, true);
            //UI_HeroInfoManager._instance.InstantiateCardDetailedData();
            //UI_HeroInfoManager._instance.ShowInfo(ObjectSelf.GetInstance().HeroContainerBag.GetHeroList()[0]);

           
            //SelectedShow(id,true);
           
        }
        //根据选择显示
        public void SelectedShow(int id,bool isFirst=false)
        {

            ////按钮字体的选择显示
            //for (int i = 0; i < TitleBtnList.Count; i++)
            //{
            //    if (i==id)
            //    {
            //        TitleBtnList[i].HighlightShow();
            //    }
            //    else
            //    {
            //        TitleBtnList[i].GeneralShow();
            //    }
            //}
            ////窗口的选择显示
            //switch (id)
            //{
            //    case 0 :
            //        UI_HeroInfoManager._instance.ShowUI(isFirst);
            //        if(!isInit)
            //        {
            //            HRMgr.HideUI();
            //            SUMgr.HideUI();
            //            ABMgr.HideUI();
            //        }
            //        break;
            //    case 1:
            //        UI_HeroInfoManager._instance.HideUI();
            //        if (!isInit)
            //        {
            //            HRMgr.ShowUI();
            //            ABMgr.HideUI();
            //            SUMgr.HideUI();
            //        }
            //        break;
            //    case 2:
            //        UI_HeroInfoManager._instance.HideUI();
            //        if (!isInit)
            //        {
            //            HRMgr.HideUI();
            //            ABMgr.ShowUI();
            //            SUMgr.HideUI();
            //        }
            //        break;
            //    case 3:
            //        UI_HeroInfoManager._instance.HideUI();
            //        if (!isInit)
            //        {
            //            HRMgr.HideUI();
            //            ABMgr.HideUI();
            //            SUMgr.ShowUI();
            //        }
            //        break;
            //    default:
            //        break;
            //}

            //isInit = false;
        }
        // 1：准备播放进场动画
        public override void OnPlayingEnterAnimation() 
        {
           // transform.localScale = new Vector3(0, 0, 0);
        }

        //// 2: 准备删除UI
        //public override void OnReadyForClose()
        //{
        //    UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        //    if (cap != null)
        //        cap.Release(m_captionPoston);
        //    UI_HomeControler.Inst.ReMoveUI(gameObject);

        //}


        // 3: 更新UI显示
        public override void UpdateUIView()
        {
            //if (UIState == UIStateEnum.PlayingEnterAnimation)
            //{
            //    transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
            //    if (transform.localScale.x >= 1.0f)
            //    {
            //        UIState = UIStateEnum.PlayingEnterAnimationOver;
            //    }
            //}
            //else if (UIState == UIStateEnum.PlayingExitAnimation)
            //{
            //    transform.position += new Vector3(0.1f, 0.00f, 0.00f);
            //    if(transform.position.x > -20)
            //    {
            //        UIState = UIStateEnum.PlayingExitAnimationOver;
            //    }
            //}

            //m_captionPoston.SetAsLastSibling();
        }


        public void AddMsgBox(string text)
        {
            DreamFaction.GameCore.InterfaceControler.GetInst().AddMsgBox(text);
        }
  

        //返回按钮
        private void OnClickBackBtn()
        {
            //HeroModelBack.Inst.ClearBg();
            //UI_EffectManager._instance.DisableEffect("PropertyUp01");
            //UIState = UIStateEnum.PlayingExitAnimationOver;
            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }



        //private void RefreshTipsController()
        //{
        //    if (m_TipsController != null)
        //        m_TipsController.Refresh();
        //}
        //生成功能提示控制器
        //IFunctionTipsController CreateFunctionTipsController()
        //{
        //    //var _manager = FunctionTipsManager.GetInstance();
        //    //if (_manager == null)
        //    //    return null;

        //    //FunctionTipsController _controller = new FunctionTipsController();

        //    //_controller.AddControlledObject(m_HeroInfoTipsImage, _manager.CheckUpgradableHeroInTeam,
        //    //                                    _manager.CheckAdvancedHeroInTeam
        //    //                                );
        //    //_controller.AddControlledObject(m_RuneTipsImage, _manager.CheckHeroRuneInTeam);
        //    //_controller.AddControlledObject(m_AttributeTipsImage, _manager.CheckAttributeTrainInTeam);
        //    //_controller.AddControlledObject(m_SkillUpImage, _manager.CheckSkillUpgradeInTeam);

        //    //_controller.AddControlledObject(m_LevelUpTipsImage, _manager.CheckCurrentUpgradableHero);
        //    //_controller.AddControlledObject(m_UpgradeTipsImage, _manager.CheckCurrentAdvancedHero);

        //    //return _controller;
        //}
    }
}