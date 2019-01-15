using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DreamFaction.GameCore;
using DreamFaction.UI.Core;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameSceneEditor;
using DreamFaction.GameAudio;
using DreamFaction.LogSystem;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    /// <summary>
    /// 主场景的主界面，继承自BaseUI
    /// </summary>
    public class UI_MenuPanel : BaseUI
    {
        public static UI_MenuPanel Inst;

        // 初始时是否是自动模式
        private bool isAuto = false;        // 模式是自动吗
        private Button speedBtn = null;
        private Button autoBtn;

        private Button m_EscBtn;
        private Button m_PauseBtn;
        private Transform m_bg;
        private Transform m_TopRight;
        private Transform m_Bottom;

        private Text m_Speed; //显示当前加速倍速

        private Transform m_Auto_Effect;  //自动战斗特效
        private Transform m_Auto_Button;  //自动战斗按钮
        private Transform m_Hand_Effect;  //手动战斗特效
        private Transform m_Hand_Button;  //手动战斗按钮

        private Text      m_TimeLimit; //时间进度
        private Transform m_TimeTransform;

        private GameObject mInfiniteTime;
        //private Transform timeTransfrom;    // 计时
        //private Image mHundred; // 百位
        //private Image mTow;     // 十位
        //private Image mOne;     // 个位
        private Text mLevelName;    // 战场名称
        private UI_BuffAllControl mSelfBuff;        // 我方buff
        private UI_BuffAllControl mEnemyBuff;       // 敌方buff
        private UI_SkillPanel mSkillPanel;      // 英雄技能页
        private RectTransform mSkillPanelTrans = null;
        private RectTransform m_AutoPanelTrans = null;       // 自动选择页
        private UI_AutoPanel m_AutoPanel = null;        // 自动选择页
        private AngerNumber mSelfAnger;    // 我方怒气
       // private AngerNumber mEnemyAnger;   // 敌方怒气
        private Text mStageName;    // 关卡名称
        private UI_SPControl mSPControl;    // 怒气控制器
       // private RoundNumber mCurRound; // 当前回合数
       // private RoundNumber mTotalRound;   // 总回合数
        private Transform[] m_TotalRound=new Transform[3];     //总回合数 最大为3
        private Transform[] m_CurRound = new Transform[3]; //当前回合数 
        private Image m_AngerImager;  //****************怒气  暂时先用静态图片切割

        private bool isUpdateTime = false;  // 是否需要更新时间
        private int mCurTime;               // 当前时间
        private List<Sprite> mNumberSprite = new List<Sprite>(10);
        private GameObject m_LimitFightUIObj;

        //技能图标
        private Transform m_Skill1;
        private Transform m_Skill2;
        private Transform m_Skill3;
        private Transform m_Skill4;
        private Transform m_Skill5;
        private Transform[] m_SkillArys=new Transform[5];//技能对象数组
   
        private GameObject m_TotalDamagePanel;
        private Text m_TotalDamageText;
        private Text m_DamageCountText;

        // 新手引导相关
        float m_CurTime = 0.0f;
        float m_AllTime = 2.0f;
        bool m_IsGuide = false;
        public TimeScaleState mInitSpeed = TimeScaleState.TimeScale_Normal;
        bool m_IsTriger = false;
        private GameObject m_Mask;
        float anglerVaule=0; //总怒气
        public float speed = 0.01f; //怒气的动画速度
        private float lastValue = 0; //记录上个怒气值
        private int m_WholeCount = 0; //重复满一管怒气数量
        private int lasPointValue; //记录下上个单位的值
        public override void OnPlayingEnterAnimation()
        {

        }

        public override void InitUIData()
        {

            Inst = this;
            m_Mask = selfTransform.FindChild("Mask").gameObject;
            m_LimitFightUIObj = selfTransform.FindChild("UI_LimitFight").gameObject;
            m_bg = selfTransform.FindChild("Image(bottombg)");
            m_TopRight = selfTransform.FindChild("topright");
            m_Bottom = selfTransform.FindChild("bottom");
            // 按钮
            speedBtn = selfTransform.FindChild("Button(speed)").GetComponent<Button>();
            speedBtn.onClick.AddListener(onSpeedCall);

            m_PauseBtn = selfTransform.FindChild("Button(pause)").GetComponent<Button>();
            m_PauseBtn.onClick.AddListener(onPauseCall);
            m_EscBtn = selfTransform.FindChild("EscBtn").GetComponent<Button>();
            m_EscBtn.onClick.AddListener(OnClickEscButton);
            autoBtn = selfTransform.FindChild("Button(autoFight)").GetComponent<Button>();
            autoBtn.onClick.AddListener(OnClickSkillIcon);

            // 技能页
            mSkillPanel = selfTransform.FindChild("bottom/SkillPanel").GetComponent<UI_SkillPanel>();
            m_Speed = speedBtn.transform.FindChild("Text").GetComponent<Text>();
            m_Auto_Effect= autoBtn.transform.FindChild("effect/auto");
            m_Auto_Button = autoBtn.transform.FindChild("Image(auto)");
            m_Hand_Effect = autoBtn.transform.FindChild("effect/shoudong");
            m_Hand_Button = autoBtn.transform.FindChild("Image(shoudong)");
            // 战场信息
            Transform infoTrans = selfTransform.FindChild("TopPanel");
            //timeTransfrom = infoTrans.FindChild("time");
            //mHundred = infoTrans.FindChild("time/3").GetComponent<Image>();
            //mTow = infoTrans.FindChild("time/2").GetComponent<Image>();
            //mOne = infoTrans.FindChild("time/1").GetComponent<Image>();
            m_TimeLimit = transform.FindChild("topright/Text(timelimit)").GetComponent<Text>();
            m_TimeTransform = transform.FindChild("topright/Text(timelimit)");
            mLevelName = infoTrans.FindChild("LevelName").GetComponent<Text>();
            //mInfiniteTime = infoTrans.FindChild("Infinite").gameObject;
            mStageName = transform.FindChild("topright/levelname").GetComponent<Text>();
            mSPControl = selfTransform.FindChild("SP").GetComponent<UI_SPControl>();
            //mCurRound = selfTransform.FindChild("RoundTag/curRound").gameObject.AddComponent<RoundNumber>().init(1);
            //mTotalRound = selfTransform.FindChild("RoundTag/totalRound").gameObject.AddComponent<RoundNumber>().init(0);
            m_TotalRound[0] = transform.FindChild("topright/wave_1");
            m_TotalRound[1] = transform.FindChild("topright/wave_2");
            m_TotalRound[2] = transform.FindChild("topright/wave_3");
            m_CurRound[0] = transform.FindChild("topright/process/1");
            m_CurRound[1] = transform.FindChild("topright/process/2");
            m_CurRound[2] = transform.FindChild("topright/process/3");
            m_AngerImager = transform.FindChild("bottom/nuqivalue").GetComponent<Image>();
            m_AngerImager.gameObject.SetActive(false);
            // buff
            mSelfBuff = infoTrans.FindChild("SelfBuffer").gameObject.AddComponent<UI_BuffAllControl>();
            mEnemyBuff = infoTrans.FindChild("EnemyBuffer").gameObject.AddComponent<UI_BuffAllControl>();

            // 怒气
            mSelfAnger = transform.FindChild("bottom/nuqi").gameObject.AddComponent<AngerNumber>();
            //mEnemyAnger = infoTrans.FindChild("right/value").gameObject.AddComponent<AngerNumber>();
            //技能图标
            m_Skill1 = transform.FindChild("bottom/SkillPanel/skill_1");
            m_Skill2 = transform.FindChild("bottom/SkillPanel/skill_2");
            m_Skill3 = transform.FindChild("bottom/SkillPanel/skill_3");
            m_Skill4 = transform.FindChild("bottom/SkillPanel/skill_4");
            m_Skill5 = transform.FindChild("bottom/SkillPanel/skill_5");
            m_Skill1.gameObject.SetActive(false);
            m_Skill2.gameObject.SetActive(false);
            m_Skill3.gameObject.SetActive(false);
            m_Skill4.gameObject.SetActive(false);
            m_Skill5.gameObject.SetActive(false);
            m_SkillArys[0] = m_Skill1;
            m_SkillArys[1] = m_Skill2;
            m_SkillArys[2] = m_Skill3;
            m_SkillArys[3] = m_Skill4;
            m_SkillArys[4] = m_Skill5;
            // 战场倒计时图片资源
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/0"));
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/1"));
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/2"));
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/3"));
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/4"));
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/5"));
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/6"));
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/7"));
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/8"));
            mNumberSprite.Add(UIResourceMgr.LoadSprite("UI/Number/fight_time/9"));
           // SetInitObjectIsShow(false);
            //是否极限试炼
            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                m_LimitFightUIObj.SetActive(true);
                SetStageNameActive(false);
            }

            if (int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.BattleMode)) == 0)//自动 yao 15-6-29
            {
                isAuto = true;
            }
            else
            {
                isAuto = false;//手动模式
            }
            //isAuto = false;
            // 添加更新事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_Anger_Update, onAngerUpdate);
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_PrepareEnemy, onRoundUpdateCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_LimitBoutEnd, ShowLimitFightRoundNum);
            
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Guide_Stop_Type, StopNextGuide);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Guide_Fighting, GuideFinghting);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Guide_Continue, GuideContinue);


            //////////////////////////////////////////////////////////////////////////
            if(GuideManager.GetInstance().IsContentGuideID(100306) == false)
            {
                //m_IsGuide = true;
            }
            else
            {
                m_IsGuide = false;
            }
        }

        void OnDestroy()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_Anger_Update, onAngerUpdate);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_PrepareEnemy, onRoundUpdateCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_LimitBoutEnd, ShowLimitFightRoundNum);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_UI_ChangeHP, UpdateTotalDamagePanel);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Guide_Stop_Type, StopNextGuide);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Guide_Fighting, GuideFinghting);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Guide_Continue, GuideContinue);
        }
        /// <summary>
        /// 设置界面是否显示与隐藏
        /// </summary>
        /// <param name="isShow"></param>
        public void SetInitObjectIsShow(bool isShow)
        {
            speedBtn.gameObject.SetActive(isShow);
            m_PauseBtn.gameObject.SetActive(isShow);
            //m_EscBtn.gameObject.SetActive(isShow);
            autoBtn.gameObject.SetActive(isShow);
            m_bg.gameObject.SetActive(isShow);
            m_TopRight.gameObject.SetActive(isShow);
            m_Bottom.gameObject.SetActive(isShow);

        }

        public override void InitUIView()
        {
            //入场自动战斗
            onModelCall();
            UIProcess();

            int id = FightControler.Inst.StageID;
            //mCurTime = FightControler.Inst.GetRemainingCountTime();
            if (mCurTime != -1)
            {
                isUpdateTime = true;
                //mInfiniteTime.SetActive(false);
            }
            else
            {
                isUpdateTime = false;
                setTime(999);
            }


            //是否极限试炼  初始化回合数            
            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
               // mTotalRound.setMaxValue(DataTemplate.GetInstance().m_GameConfig.getUltimatetrial_max_wave());
                Debug.LogError("极限试炼 回合数代码逻辑要修改");
                ShowLimitFightRoundNum();
            }
            else
            {
                // 设置关卡名
                int iStageId = FightControler.Inst.StageID;
                StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(iStageId);
                ChsTextTemplate template = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData(stageinfo.m_stagename);
                if (stageinfo != null /*&& strData.ContainsKey(stageinfo.m_stagename)*/)
                {
                    //ChsTextTemplate template = (ChsTextTemplate)strData[stageinfo.m_stagename];
                    mStageName.text = template.languageMap["Chinese"];
                }

               // mTotalRound.setValue(FightControler.Inst.getTotalRound());
                SetTotalRoundNum(FightControler.Inst.getTotalRound());
               // mCurRound.setValue(1);
                SetCurRoundNum(1);
            }
            onAutoForwardFinish();
        }

        /// <summary>
        /// 回合数显示
        /// </summary>
        /// <param name="roundNum"></param>
        public void ShowLimitFightRoundNum()
        {
           // mCurRound.setValue(ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum);
            Debug.LogError("极限试炼 回合数代码逻辑要修改");
        }
        /// <summary>
        /// 设置当前回合数
        /// </summary>
        public void SetCurRoundNum(int num)
        {
            switch (num)
            {
                case 1:
                    m_CurRound[0].gameObject.SetActive(true);
                    m_CurRound[1].gameObject.SetActive(false);
                    m_CurRound[2].gameObject.SetActive(false);
                    break;
                case 2:
                    m_CurRound[0].gameObject.SetActive(true);
                    m_CurRound[1].gameObject.SetActive(true);
                    m_CurRound[2].gameObject.SetActive(false);
                    break;
                case 3:
                    m_CurRound[0].gameObject.SetActive(true);
                    m_CurRound[1].gameObject.SetActive(true);
                    m_CurRound[2].gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 设置总回合数显示 只针对于普通战斗
        /// </summary>
        /// <param name="num"></param>
        public void SetTotalRoundNum(int num)
        {
            switch (num)
            {
                case 1:
                    m_TotalRound[0].gameObject.SetActive(true);
                    m_TotalRound[1].gameObject.SetActive(false);
                    m_TotalRound[2].gameObject.SetActive(false);
                    break;
                case 2:
                    m_TotalRound[0].gameObject.SetActive(false);
                    m_TotalRound[1].gameObject.SetActive(true);
                    m_TotalRound[2].gameObject.SetActive(false);
                    break;
                case 3:
                    m_TotalRound[0].gameObject.SetActive(false);
                    m_TotalRound[1].gameObject.SetActive(false);
                    m_TotalRound[2].gameObject.SetActive(true);
                    break;
                default:
                    LogManager.LogError("普通关卡战斗回合数配置超过3回合");
                    break;
            }
        }
        /// <summary>
        /// 切换速度
        /// </summary>
        private void onSpeedCall()
        {
            ForwardFinish();
            //第五版战斗修改 Wyf
        }

        /// <summary>
        /// 暂停
        /// </summary>
        private void onPauseCall()
        {
            UI_FightControler.Inst.onPauseCall();
        }
        private void OnClickEscButton()
        {
            DreamFaction.UI.Core.UI_HomeControler.NeedShowWorldBossPanel = ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter;
            ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter = false;
            GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Normal);
            SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
            
        }

        private void ForwardFinish()
        {
            int max = (int)ExchangeModule.GetMaxTimeScaleState();
            TimeScaleState model = (TimeScaleState)Mathf.Min(max, ((int)GameTimeControler.Inst.curTimeScaleState % max + 1));
            GameTimeControler.Inst.SetState(model);
            // 切换纹理
            switch (GameTimeControler.Inst.curTimeScaleState)
            {
                case TimeScaleState.TimeScale_Double:
                    {
                        m_Speed.text = "x2";
                    }
                    break;
                case TimeScaleState.TimeScale_Triple:
                    {
                        m_Speed.text = "x3";
                    }
                    break;
                case TimeScaleState.TimeScale_Normal:
                    {
                        m_Speed.text = "x1";
                    }
                    break;
            }
        }
        private void onSpeedAniEnd()
        {
            speedBtn.interactable = true;
        }

        /// <summary>
        /// 模式切换
        /// </summary>
        private void onModelCall()
        {
            //autoBtn.interactable = false;
            //isAuto = !isAuto;
            onAutoForwardFinish();
            //第五版战斗改动 Wyf
            //Sequence mySequence = DOTween.Sequence();
            //mySequence.Append(autoBtn.transform.DOScaleX(0, 0.15f).OnComplete(onAutoForwardFinish).SetUpdate(true));
            //mySequence.Append(autoBtn.transform.DOScaleX(1, 0.15f).OnComplete(onAutoAniEnd).SetUpdate(true));
            //mySequence.SetUpdate(true);

            if(m_IsTriger == false)
            {
                m_IsTriger = true;
            }
            else
            {
                if (GuideManager.GetInstance().isGuideUser && GuideManager.GetInstance().GetBackCount(100308))
                {
                    // 引导 点击【切换模式】
                    //GuideManager.GetInstance().ShowNextGuide();
                    GuideManager.GetInstance().ShowGuideWithIndex(100309);
                }       
            }
            
        }

        /// <summary>
        /// 点击技能图标
        /// </summary>
       private  void OnClickSkillIcon()
       {
            isAuto = !isAuto;
            onAutoForwardFinish();
       }
        private void onAutoForwardFinish()
        {
            // 切换显示
            if (!isAuto) //切换到手动模式
            {
                m_Auto_Effect.gameObject.SetActive(false);
                m_Auto_Button.gameObject.SetActive(false);
                m_Hand_Effect.gameObject.SetActive(true);
                m_Hand_Button.gameObject.SetActive(true);
                UI_FightControler.Inst.onReleaseTargetLock();
                FightControler.Inst.SetFightAIState(EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_INVALID);
                //onAutoPanelInto();
            }
            else   //切换到手动模式
            {
                m_Auto_Effect.gameObject.SetActive(true);
                m_Auto_Button.gameObject.SetActive(true);
                m_Hand_Effect.gameObject.SetActive(false);
                m_Hand_Button.gameObject.SetActive(false);
               // onSkillPanelInto();
                FightControler.Inst.SetFightAIState(EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_NORMAL);
            }
        }

        private void onAutoAniEnd()
        {
            //autoBtn.interactable = true;
        }

        private void setTime(int inum)
        {
            int minute = inum / 60;
            int second = inum % 60;
            string secondStr = second.ToString();
            if (second < 10)
            {
               secondStr = "0" + second;
            }
            m_TimeLimit.text = minute + ":" + secondStr;
            if (inum < 60)
            {
                m_TimeTransform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.3f)
                        .OnComplete(resetTimeView)
                        .SetUpdate(true)
                        .SetEase(Ease.OutCubic);
            }
            //第五版战斗改动 Wyf
            //if (inum == mCurTime || inum < 0)
            //{
            //    return;
            //}

            //int i3 = inum / 100;
            //int i2 = (inum / 10) % 10;
            //int i1 = inum % 10;

            //int iC3 = mCurTime / 100;
            //int iC2 = (mCurTime / 10) % 10;
            //int iC1 = mCurTime % 10;

            //mCurTime = inum;

            //if (i3 != 0)
            //{
            //    // 3位数
            //    setSprite(mHundred, ref i3, ref iC3);
            //    setSprite(mTow, ref i2, ref iC2);
            //    setSprite(mOne, ref i1, ref iC1);
            //}
            //else if (i3 == 0 && i2 != 0)
            //{
            //    // 2位数
            //    mHundred.gameObject.SetActive(false);
            //    setSprite(mTow, ref i2, ref iC2);
            //    setSprite(mOne, ref i1, ref iC1);
            //}
            //else
            //{
            //    // 1位数
            //    mHundred.gameObject.SetActive(false);
            //    mTow.gameObject.SetActive(false);
            //    setSprite(mOne, ref i1, ref iC1);
            //}

            //if (mCurTime < 60)
            //{
            //    // 红色
            //    //timeTransfrom.DOScale(new Vector3(1.5f, 1.5f, 1), 0.3f)
            //    //    .OnComplete(resetTimeView)
            //    //    .SetUpdate(true)
            //    //    .SetEase(Ease.OutCubic);
            //}
            //else if (mCurTime == 60)
            //{
            //    mTow.color = new Color(255, 0, 0);
            //    mOne.color = new Color(255, 0, 0);
            //}
        }

        private void resetTimeView()
        {
            m_TimeTransform.localScale = new Vector3(1f, 1f, 1f);
        }

        private void setSprite(Image image, ref int num, ref int cur)
        {
            if (num != cur)
            {
                image.overrideSprite = Instantiate(mNumberSprite[num]) as Sprite;
            }
        }

        void LateUpdate()
        {
            if (isUpdateTime)
            {
                setTime(FightControler.Inst.GetRemainingCountTime());
            }

            // 新手引导相关  等两秒后提示引导
            if (m_IsGuide)
            {
                m_CurTime += Time.deltaTime;

                if (m_CurTime >= m_AllTime)
                {
                    m_IsGuide = false;

                    mInitSpeed = GameTimeControler.Inst.curTimeScaleState;
                    GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Pause);
                    // 新手引导战斗, 战斗教学1-1   100306  - 100307
                    GuideManager.GetInstance().ShowGuideWithIndex(100306);
                }
            }
        }
        
        void StopNextGuide(GameEvent e)
        {
            if ((int)e.data == 100307)
            {
                GameTimeControler.Inst.SetState(mInitSpeed);
            }
        }

        void GuideContinue(GameEvent e)
        {
            int id = (int)e.data;
            if(id == 100307)
            {
                GuideManager.GetInstance().ShowGuideWithIndex(100307);  
            }
            else if(id == 100308)
            {
                GuideManager.GetInstance().StopGuide();
                GameTimeControler.Inst.SetState(mInitSpeed);
            }
            else if(id == 100313)
            {
                GuideManager.GetInstance().ShowGuideWithIndex(100312);
                GuideManager.GetInstance().StopGuide();
                GameTimeControler.Inst.SetState(mInitSpeed);
            }
        }

        void GuideFinghting(GameEvent e)
        {
            if (GuideManager.GetInstance().isGuideUser)
            {
                if ((int)(e.data) == 100308)
                {
                    if(GuideManager.GetInstance().GetBackCount(100307))
                    {
                        mInitSpeed = GameTimeControler.Inst.curTimeScaleState;
                        GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Pause);
                        // 点击【切换模式】 100308
                        GuideManager.GetInstance().ShowGuideWithIndex(100308);
                        //开始引导技能释放 关闭Mask
                        SetMaskObjActive(false);
                    }                  

                }
                else if((int)e.data == 100311)
                {
                    if(GuideManager.GetInstance().GetBackCount(100310))
                    {
                        mInitSpeed = GameTimeControler.Inst.curTimeScaleState;
                        GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Pause);
                        // 点击【切换模式 【  100311
                        GuideManager.GetInstance().ShowGuideWithIndex(100311);
                        //开始引导技能释放 关闭Mask
                        SetMaskObjActive(false);
                    }
                }
                else
                {
                    Debug.Log(e.data);
                }
            }
        }

        /// <summary>
        /// buffer 图标处理
        /// </summary>
        public void onSelfAllBuffRemove(BuffTemplate info)
        {
            mSelfBuff.RemoveBuff(info);
        }

        public void onSelfAllBuffAdd(BuffTemplate info)
        {
            mSelfBuff.AddBuff(info);
        }

        public void onEnemyAllBuffRemove(BuffTemplate info)
        {
            mEnemyBuff.RemoveBuff(info);
        }

        public void onEnemyAllBuffAdd(BuffTemplate info)
        {
            mEnemyBuff.AddBuff(info);
        }

       /// <summary>
      /// 创建技能图标 
      /// 根据英雄数量 显示技能图标 不是动态创建 Wyf
      /// </summary>
      /// <param name="hero"></param>
      /// <param name="postionIndex">英雄的索引</param>
        public void CreateSkillIcon(ObjectHero hero , int indedx)
        {
          m_SkillArys[indedx].gameObject.SetActive(true);
          mSkillPanel.CreateSkillIcon(hero, m_SkillArys[indedx].gameObject);
              
        }
        public bool onSingleSkillCall(ref EventRequestSkillPackage data)
        {
            return mSkillPanel.onSingleSkillCall(ref data);
        }

        public bool onSingleTargetFind(ObjectCreature obj)
        {
            return mSkillPanel.onSingleTargetFind(obj);
        }

        public bool onCheckSkillWaitLock(X_GUID uid)
        {
            return mSkillPanel.onCheckSkillWaitLock(uid);
        }
        // 技能页入场
        private void onSkillPanelInto()
        {
            if (m_AutoPanelTrans)
                m_AutoPanelTrans.gameObject.SetActive(false);

            mSkillPanel.gameObject.SetActive(true);
            if (mSkillPanelTrans == null)
            {
                mSkillPanelTrans = mSkillPanel.GetComponent<RectTransform>();
            }
            mSkillPanelTrans.anchoredPosition3D = new Vector3(0, -315, 0);
            mSkillPanelTrans.DOAnchorPos3D(new Vector3(0, 0, 0), 0.8f).SetEase(Ease.OutCubic).SetUpdate(true);
        }

        // 自动选择页入场
        private void onAutoPanelInto()
        {
            if (m_AutoPanelTrans == null)
            {
                Object obj = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Fight/UI_AutoFightPanel");
                GameObject trans = Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
                trans.transform.SetParent(selfTransform, false);
                m_AutoPanelTrans = trans.gameObject.GetComponent<RectTransform>();
                m_AutoPanel = trans.gameObject.GetComponent<UI_AutoPanel>();
            }
            mSkillPanel.gameObject.SetActive(false);
            m_AutoPanelTrans.gameObject.SetActive(true);
            m_AutoPanel.onReset();
            //m_AutoPanel.localPosition = new Vector3(0, -m_AutoPanel.GetComponent<RectTransform>().sizeDelta.y, 0);
            m_AutoPanelTrans.anchoredPosition3D = new Vector3(0, -315, 0);
            m_AutoPanelTrans.DOAnchorPos3D(new Vector3(0, 0, 0), 0.8f).SetEase(Ease.OutCubic).SetUpdate(true);
        }

        /// <summary>
        /// 怒气值更新
        /// </summary>
        private void onAngerUpdate(GameEvent e)
        {
            EM_OBJECT_TYPE type = (EM_OBJECT_TYPE)e.data;
            int value = FightControler.Inst.GetPowerValue(type);
            switch (type)
            {
                case EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO:
                    {
                        //mSelfAnger.setValue(value / DataTemplate.GetInstance().m_GameConfig.getEach_rage_point());
                        //// 更新怒气进度
                        //anglerVaule = FightControler.Inst.GetPowerValue(type);
                        GameConfig config = DataTemplate.GetInstance().m_GameConfig;
                        //anglerVaule = anglerVaule >= ObjectSelf.GetInstance().GetMaxPowerValue() ? 1 : anglerVaule / ObjectSelf.GetInstance().GetMaxPowerValue();
                        mSelfAnger.setValue(FightControler.Inst.GetPowerPoint(type));
                        anglerVaule = (float)FightControler.Inst.GetPowerPointValue(type)/config.getEach_rage_point();
                        //Debug.Log("anglerVaule:"+anglerVaule);
                        //UpdateAnger(anglerVaule);
                        if (FightControler.Inst.GetPowerPoint(type) == 0)
                        {
                            return;
                        }
                        StartCoroutine(UpdateAnger(lastValue, anglerVaule,FightControler.Inst.GetPowerPoint(type) - lasPointValue));
                        //PlayUpdateAnger(type);
                        lasPointValue = FightControler.Inst.GetPowerPoint(type);
                    }
                    break;
                default:
                    {
                       // mEnemyAnger.setValue(value / DataTemplate.GetInstance().m_GameConfig.getEach_rage_point());
                    }
                    break;
            }
        }

        private void onRoundUpdateCall(GameEvent e)
        {
            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
                return;
            //mCurRound.setValue((int)e.data);
            SetCurRoundNum((int)e.data);
        }

        // 强制清理技能数据
        public void onSkillReleaseData()
        {
            mSkillPanel.onReleaseSkillStruce();
        }

        /// <summary>
        /// 游戏结束，关闭协程
        /// </summary>
        public void onGameOverCall()
        {
            mSPControl.StopAllCoroutines();
            mSPControl.isFightEnd = true;
        }

        public void SetStageNameActive(bool active)
        {
            mStageName.enabled = active;
        }

        public class AngerNumber : MonoBehaviour
        {
            private Image m2;   //十位
            private Image m1;   // 个位
            private int mCurValue;  // 当前值

            private List<Sprite> mNums = new List<Sprite>(10);
            void Start()
            {

            }

            void Awake()
            {
                m2 = transform.FindChild("2").GetComponent<Image>();
                m1 = transform.FindChild("1").GetComponent<Image>();
                m1.gameObject.SetActive(false);
                m2.gameObject.SetActive(false);
                //m2.gameObject.SetActive(false);

                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7011"));
                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7012"));
                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7013"));
                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7014"));
                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7015"));
                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7016"));
                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7017"));
                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7018"));
                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7019"));
                mNums.Add(UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7020"));
            }

            public void setValue(int value)
            {
                if (mCurValue == value)
                { 
                    return;
                }
                mCurValue = value;

                if (value < 99)
                {
                    int i2 = value / 10;
                    int i1 = value % 10;
                    //if (i2 <= 0)
                    //{
                    //    // 十位不存在
                    //    m2.gameObject.SetActive(false);
                    //}
                    //else
                    //{
                    //    m2.gameObject.SetActive(true);
                    //    m2.overrideSprite = Instantiate(mNums[i2]) as Sprite;
                    //}
                    m2.overrideSprite = Instantiate(mNums[i2]) as Sprite;
                    m1.overrideSprite = Instantiate(mNums[i1]) as Sprite;
                    if (!m2.gameObject.activeSelf) { m2.gameObject.SetActive(true); }
                    if (!m1.gameObject.activeSelf) { m1.gameObject.SetActive(true); }
                    if (i2 == 0)  //小于10  不显示个位数
                    {
                        m2.gameObject.SetActive(false);
                    }
                    //m2.SetNativeSize();
                    //m1.SetNativeSize();
                }
                else
                {
                    LogManager.LogError("invalid value");
                }
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.1f));
                mySequence.Append(transform.DOScale(new Vector3(1, 1, 1), 0.1f));
            }
        }

        private class RoundNumber : MonoBehaviour
        {
            private int type;   // 0是总数 1是当前数
            private int curBit = 0; // 当前的位数
            private int curValue;
            private List<Sprite> mNums = new List<Sprite>(10);

            public RoundNumber init(int type)
            {
                this.type = type;
                switch (type)
                {
                    case 0:
                        {
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_00"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_01"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_02"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_03"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_04"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_05"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_06"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_07"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_08"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_09"));
                        }
                        break;
                    case 1:
                        {
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_0"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_1"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_2"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_3"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_4"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_5"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_6"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_7"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_8"));
                            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/round_number/Battle_9"));
                        }
                        break;
                }
                return this;
            }

            void Start()
            {

            }

            void Awake()
            {

            }
            public void setValue(int value)
            {
                var count = transform.childCount;
                var str = value.ToString();
                if (curBit != str.Length)
                {
                    for (int i = curBit; i < str.Length; i++)
                    {
                        GameObject obj = new GameObject("Number");
                        obj.AddComponent<Image>();
                        obj.transform.SetParent(transform, false);
                    }
                    // 需要添加一位
                    curBit = str.Length;
                }
                for (int idx = 0; idx < str.Length; idx++)
                {
                    char c = str[str.Length - idx - 1];
                    int num = int.Parse(c.ToString());
                    Image image = transform.GetChild(idx).gameObject.GetComponent<Image>();
                    image.overrideSprite = Instantiate(mNums[num]) as Sprite;
                }
            }

            public void setMaxValue(int value)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
                var str = value.ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    GameObject obj = new GameObject("Number");
                    obj.AddComponent<Image>();
                    obj.transform.SetParent(transform, false);

                    char c = str[i];
                    int num = int.Parse(c.ToString());
                    Image image = transform.GetChild(i).gameObject.GetComponent<Image>();
                    image.overrideSprite = Instantiate(mNums[num]) as Sprite;
                }
            }
        }

        public void UIProcess()
        {
            m_TotalDamagePanel = selfTransform.FindChild("TotalDamagePanel").gameObject;
            m_TotalDamageText = selfTransform.FindChild("TotalDamagePanel/TotalDamageText").GetComponent<Text>();
            m_DamageCountText = selfTransform.FindChild("TotalDamagePanel/DamageCountText").GetComponent<Text>();

            if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter || ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                selfTransform.FindChild("TopPanel/time").gameObject.SetActive(false);
                selfTransform.FindChild("TopPanel/InfinityImage").gameObject.SetActive(true);
                selfTransform.FindChild("RoundTag").gameObject.SetActive(false);
            }
            else
            {
                selfTransform.FindChild("TopPanel/InfinityImage").gameObject.SetActive(false);
            }
            if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)
            {
                m_TotalDamageText.text = GameUtils.getString("legend_of_the_war_content17");
                m_DamageCountText.text = SceneObjectManager.GetInstance().WorldBossDamageSum.ToString();
                m_TotalDamagePanel.SetActive(true);
                GameEventDispatcher.Inst.addEventListener(GameEventID.F_UI_ChangeHP, UpdateTotalDamagePanel);

                if (m_EscBtn != null)
                    m_EscBtn.gameObject.SetActive(true);
                if (m_PauseBtn != null)
                    m_PauseBtn.gameObject.SetActive(false);
            }
            else
            {
                m_TotalDamagePanel.SetActive(false);

                if (m_EscBtn != null)
                    m_EscBtn.gameObject.SetActive(false);
                if (m_PauseBtn != null)
                    m_PauseBtn.gameObject.SetActive(true);
            }

        }

        public void UpdateTotalDamagePanel()
        {

            m_DamageCountText.text = SceneObjectManager.GetInstance().WorldBossDamageSum.ToString();
        }

        /// <summary>
        /// 设置Mask遮罩是否显示
        /// </summary>
        /// <param name="active"></param>
        public  void SetMaskObjActive(bool active)
        {
            m_Mask.SetActive(active);
            m_Mask.transform.SetAsLastSibling();
        }
        /// <summary>
        /// 更新怒气
        /// </summary>
        /// <param name="value"></param>
        public void UpdateAnger(float value)
        {
            if (!m_AngerImager.gameObject.activeSelf)
            {
                m_AngerImager.gameObject.SetActive(true);
            }
            m_AngerImager.fillAmount = value;
        }
        /// <summary>
        /// 更新怒气动画
        /// </summary>
        /// <param name="nextValue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //IEnumerator UpdateAnger(float nextValue, float value)
        //{
        //    yield return null;
        //    if (!m_AngerImager.gameObject.activeSelf)
        //    {
        //        m_AngerImager.gameObject.SetActive(true);
        //    }
        //    if (lastValue > value)
        //    {
        //        nextValue -= speed;
        //        if (nextValue > value)
        //        {
        //            StopCoroutine(UpdateAnger(0, 0));
        //            m_AngerImager.fillAmount = nextValue;
        //            StartCoroutine(UpdateAnger(nextValue, value));
        //        }
        //        else
        //        {
        //            lastValue = value;
        //        }
        //    }
        //    else
        //    {
        //        nextValue += speed;
        //        if (nextValue < value)
        //        {
        //            StopCoroutine(UpdateAnger(0, 0));
        //            m_AngerImager.fillAmount = nextValue;
        //            StartCoroutine(UpdateAnger(nextValue, value));
        //        }
        //        else
        //        {
        //            lastValue = value;
        //        }
        //    }

        //}
        /// <summary>
        ///  更新怒气动画
        /// </summary>
        /// <param name="nextValue"></param>
        /// <param name="value"></param>
        /// <param name="wholeCount"></param>
        /// <returns></returns>
        IEnumerator UpdateAnger(float nextValue, float value, int wholeCount = 0)
        {
            yield return null;
            if (!m_AngerImager.gameObject.activeSelf)
            {
                m_AngerImager.gameObject.SetActive(true);
            }
            if (wholeCount != 0)
            {
                if (m_WholeCount <= wholeCount)
                {
                    if (lastValue < 1)
                    {
                        if (nextValue < value)
                        {
                            nextValue += 0.1f;
                            m_AngerImager.fillAmount = nextValue;
                            StartCoroutine(UpdateAnger(nextValue, value, wholeCount));
                        }
                        else
                        {
                            m_WholeCount++;
                            lastValue = 0;
                            value = 1;
                            nextValue = 0;
                            StartCoroutine(UpdateAnger(nextValue, value, wholeCount));
                        }

                    }
                }
                else
                {
                    lastValue = 0;
                    m_WholeCount = 0;
                }
            }
            else
            {
                if (lastValue > value)
                {
                    nextValue -= speed;
                    if (nextValue > value)
                    {
                        m_AngerImager.fillAmount = nextValue;
                        StartCoroutine(UpdateAnger(nextValue, value));
                    }
                    else
                    {
                        lastValue = value;
                    }
                }
                else
                {
                    nextValue += speed;
                    if (nextValue < value)
                    {
                        m_AngerImager.fillAmount = nextValue;
                        StartCoroutine(UpdateAnger(nextValue, value));
                    }
                    else
                    {
                        lastValue = value;
                    }
                }
            }

        }
    }

}
