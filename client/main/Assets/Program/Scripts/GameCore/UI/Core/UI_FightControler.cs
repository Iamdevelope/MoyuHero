using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DG.Tweening;
using GNET;
using System;
using Object =UnityEngine.Object;
using DreamFaction.GameAudio;


namespace DreamFaction.UI.Core
{
    /// <summary>
    /// 主场景的UI控制器继承自BaseUIControler，用来控制主场景的UI加载，删除！
    /// Canvas3 放置伤害数字和血条
    /// Canvas2 按钮、技能图标和战斗Tip
    /// Canvas1 战斗弹出页
    /// Canvas0 系统提示框
    /// </summary>
    public class UI_FightControler : BaseUIControler
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static UI_FightControler Inst;
        public bool isGameOver = false;
        public bool isWin;             //战斗胜利还是失败
        private bool isSpecialStage;   //是否出现特殊关卡
        private bool isMysteriousShop; //是否出现神秘商店

        private CanvasGroup mMessageTip;
        private RectTransform mMessageRect;
        private Image mShowMessage;         // 显示信息
        private Text mShowMessageText;      // 显示信息的文本字
        private Sequence mShowMsgAni;        // 显示信息的动作
        private Sprite mShowMessageBackGround;  //显示信息的默认背景
        private readonly int mMoveDistance = 100;      // 移动距离

        private UI_BloodPanel mBloodControl;    // 血條
        private Transform mFrontLayout;    // 前置的布景
        private UI_TouchControler mTouchControl = null; //  触摸管理
        private UI_ShowPanel mNumberMgr;       // HP数字管理
        private UI_MenuPanel mMenuPanel;         // 按钮层
        private UI_SkipAnimationPanel mSkipAnimationPanel;         // 剧情跳过
        private UI_BuffMgr mBuffMgr;        // buff管理器
        private bool m_IsShowFailUI = true;

        public AudioClip m_VictoryAudio = null;       //战斗胜利背景音乐
        public AudioClip m_LoseAudio = null;          //战斗失败背景音乐
        


        [HideInInspector]
        public Object mFlagPre;     // 技能标识
        [HideInInspector]
        public Object heroBloodPre = null;
        [HideInInspector]
        public Object monsterBloodPre = null;
        [HideInInspector]
        public Object bossBloodBar = null;
        [HideInInspector]
        public Object pausePanelPrefab = null;

        public void SetIsSpecialStage(bool isSpecia)
        {
            isSpecialStage = isSpecia;
        }

        public bool GetIsSpecialStage()
        {
            return isSpecialStage;
        }

        public void SetIsMysteriousShop(bool isShop)
        {
            isMysteriousShop = isShop;
        }

        public bool GetIsMysteriousShop()
        {
            return isMysteriousShop;
        }


        // 默认是否是自动模式
        private bool isInitAuto = false;
        // =====================  重载 ============================
        /// <summary>
        /// 1: 初始化
        /// </summary>
        protected override void InitData()
        {
            base.InitData();
            Inst = this;
            mBloodControl = AddUI("UI_Fight/UI_BloodPanel_3_0").GetComponent<UI_BloodPanel>();
            mNumberMgr = AddUI("UI_Fight/UI_HPPanel_3_1").GetComponent<UI_ShowPanel>();
            mMenuPanel = AddUI("UI_Fight/UI_Menu_2_1").GetComponent<UI_MenuPanel>();
            mSkipAnimationPanel = AddUI("UI_SkipAnimation/UI_SkipAnimationPanel_1_0").GetComponent<UI_SkipAnimationPanel>();
            mMessageTip = AddUI("UI_Fight/UI_MessageAlert_2_0").transform.FindChild("ShowMessage").GetComponent<CanvasGroup>();
            mFrontLayout = AddUI("UI_Fight/UI_FrontLayout_1_0").transform;

            mShowMessage = mMessageTip.transform.FindChild("image").GetComponent<Image>();
            mShowMessageText = mMessageTip.transform.FindChild("Text").GetComponent<Text>();
            mMessageRect = mMessageTip.GetComponent<RectTransform>();
            var position = mMessageRect.anchoredPosition3D;
            mMessageRect.anchoredPosition3D = new Vector3(position.x, position.y + mMoveDistance, position.z);
            mShowMessageBackGround = mShowMessage.overrideSprite;
            // 加載資源
            mFlagPre = UIResourceMgr.LoadPrefab("UI/Prefabs/TargetFlag");
            heroBloodPre = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Fight/HeroBloodBar");
            monsterBloodPre = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Fight/MonsterBloodBar");
            bossBloodBar = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Fight/BossBloodBar");
            pausePanelPrefab = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Fight/UI_FightPausePanel");

            mTouchControl = new UI_TouchControler();


            // 添加buff管理脚本
            mBuffMgr = gameObject.AddComponent<UI_BuffMgr>();
            mBuffMgr.mMenuPanel = mMenuPanel;
            mBuffMgr.mBloodPanel = mBloodControl;

            GameEventDispatcher.Inst.addEventListener(GameEventID.F_FightStateUpdate, onFightStatuChange);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_MessageAlert, onShowMessageCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_OnSupportMonstorBlood, onSupportMonstorBlood);
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryEnter, onStoryCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryCameraEnter, onStoryCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryEnd, onStoryEnd);
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryCameraEnd, onStoryCameraEnd);            
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_ShowSkillTarget, onSingleSkillCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_BattleOver, onFightEndCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_BattleFail, onFightEndCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_CountDownOver, onFightEndCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_UI_ChangeHP, onHpChangeCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_UI_Dodge, onMissCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_IsOpenSealBox, IsShowSealBox);
            GameEventDispatcher.Inst.addEventListener(GameEventID.UI_MysteriousShopSpecialTips, MysteriousShopSpecialTips);
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_BossPass, WorldBossPassTips);
            //GameEventDispatcher.Inst.addEventListener(GameEventID.UI_SpecialStageTips, SpecialStageSpecialTips);
        }

        protected override void DestroyData()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_FightStateUpdate, onFightStatuChange);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_MessageAlert, onShowMessageCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_OnSupportMonstorBlood, onSupportMonstorBlood);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_StoryEnter, onStoryCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_StoryCameraEnter, onStoryCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_StoryEnd, onStoryEnd);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_StoryCameraEnd, onStoryCameraEnd);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_ShowSkillTarget, onSingleSkillCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_BattleOver, onFightEndCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_BattleFail, onFightEndCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_CountDownOver, onFightEndCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_UI_ChangeHP, onHpChangeCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_UI_Dodge, onMissCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_IsOpenSealBox, IsShowSealBox);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_MysteriousShopSpecialTips, MysteriousShopSpecialTips);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_BossPass, WorldBossPassTips);
            //GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_SpecialStageTips, SpecialStageSpecialTips);
        }

        protected override void UpdateView()
        {
            mTouchControl.Update();
        }

        public Camera GetCanvasCamera(UICanvasFlag canvasFlag)
        {
            return canvasList[(int)canvasFlag].GetComponent<Canvas>().worldCamera;
        }

        private void onShowMessageCall(GameEvent _event)
        {
            string resId = _event.data.ToString();
            ChsTextTemplate data = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData(resId);
            //if (table.m_StringData.ContainsKey(resId))
            {
                mMessageTip.alpha = 1;
                //var data = (ChsTextTemplate)table.m_StringData[resId];
                var res = data.languageMap[AppManager.Inst.GameLanguage];
                mShowMessage.gameObject.SetActive(false);
                mShowMessageText.gameObject.SetActive(false);

                switch (data.m_type)
                {
                    case 1:
                        {
                            // 程序字
                            mShowMessageText.gameObject.SetActive(true);
                            mShowMessageText.text = res;
                        }
                        break;
                    case 2:
                        {
                            //图片字
                            if (!string.IsNullOrEmpty(res))
                            {
                                var sprite = Instantiate(UIResourceMgr.LoadSprite(common.defaultPath + res)) as Sprite;
                                if (sprite)
                                {
                                    mShowMessage.gameObject.SetActive(true);
                                    mShowMessage.sprite = sprite;
                                    mShowMessage.SetNativeSize();
                                }
                            }
                        }
                        break;
                }
                

                if (mShowMsgAni != null && mShowMsgAni.IsPlaying())
                {
                    mShowMsgAni.Restart();
                }
                else
                {
                    var position = mMessageRect.anchoredPosition3D;
                    mMessageRect.anchoredPosition3D = new Vector3(position.x, position.y - mMoveDistance, position.z);
                    mShowMsgAni = DOTween.Sequence();
                    mShowMsgAni.Append(mMessageRect.DOAnchorPos3D(position, 0.5f).SetEase(Ease.InOutCubic));
                    mShowMsgAni.Append(mMessageTip.DOFade(0, 0.5f));
                    mShowMsgAni.SetUpdate(true);
                }
            }
            
            //StopCoroutine("delay");
            //StartCoroutine(delay());
        }
        //private IEnumerator delay()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    mMessageTip.gameObject.SetActive(false);
        //}

        private void onFightStatuChange()
        {
            FightState statu = FightControler.Inst.GetFightState();

            switch (statu)
            {
                case FightState.prepareData:
                    {
                        // 初始化完成，创建血条
                        List<ObjectHero> heros = SceneObjectManager.GetInstance().GetSceneHeroList();
                        for (int i = 0; i < heros.Count; i++)
                        {
                            mBloodControl.CreateBloodBar(heros[i]);
                            //mMenuPanel.CreateSkillIcon(heros[i], heros.Count - ObjectSelf.GetInstance().Teams.GetTeamIndexByHeroGuid(heros[i].GetGuid()));
                            mMenuPanel.CreateSkillIcon(heros[i],  i);
                        }
                        //mMenuPanel.SetInitObjectIsShow(true);
                        //mMenuPanel.SkillAutoSort();
                    }
                    break;
                case FightState.prepareEnemy_over:
                    {
                        // 初始化敌人的血条
                        List<ObjectMonster> monsters = SceneObjectManager.GetInstance().GetSceneMonsterList();
                        foreach (ObjectMonster monster in monsters)
                        {
                            if (monster.IsAlive())
                            {
                                UI_Blood blood = mBloodControl.CreateBloodBar(monster);
                                blood.gameObject.SetActive(false);
                            }

                        }
                    }
                    break;
                case FightState.FightStoryCamera:
                    break;
                case FightState.FightStory:
                    {
                        mBloodControl.HideAllBlood();
                    }
                    break;
                case FightState.prepareFight:
                    {
                        mBloodControl.ShowAllBlood();
                    }
                    break;
            }
        }

        void onSupportMonstorBlood()
        {
            // 初始化敌人的血条
            List<ObjectMonster> monsters = SceneObjectManager.GetInstance().GetSceneMonsterList();
            if (monsters.Count > 0)
            {
                ObjectMonster monster = monsters[monsters.Count - 1];
                if (monster.IsAlive())
                {
                    mBloodControl.CreateBloodBar(monster);
                }
            }
        }

        private ObjectHero getHeroByHeroId(int heroid)
        {
            List<ObjectHero> heros = SceneObjectManager.GetInstance().GetSceneHeroList();
            foreach (ObjectHero hero in heros)
            {
                if (hero.GetHeroData().TableID.Equals(heroid) == true)
                {
                    return hero;
                }
            }
            return null;
        }

        // 播放剧情
        private void onStoryCall(GameEvent e)
        {            
            if(UI_MenuPanel.Inst != null)
            {
                UI_MenuPanel.Inst.gameObject.SetActive(false);
            }
            mSkipAnimationPanel.gameObject.SetActive(true);            
        }

        private void onStoryEnd(GameEvent e)
        {
            if (FightControler.isOpeningAnimation == false)
            {
                if (UI_MenuPanel.Inst != null)
                {
                    UI_MenuPanel.Inst.gameObject.SetActive(true);
                }
                mSkipAnimationPanel.gameObject.SetActive(false);
            }            
        }

        private void onStoryCameraEnd(GameEvent e)
        {
            //gameObject.SetActive(true);
            //mMenuPanel.SetInitObjectIsShow(true);
            if (UI_MenuPanel.Inst != null)
            {
                UI_MenuPanel.Inst.gameObject.SetActive(true);
            }
            mSkipAnimationPanel.gameObject.SetActive(false);
            FightControler.isOpeningAnimation = false;
        }

        // 点击单体技能
        public void onSingleSkillCall(GameEvent e)
        {
            EventRequestSkillPackage package = (EventRequestSkillPackage)e.data;
            if (mMenuPanel.onSingleSkillCall(ref package))
            {
                mBloodControl.showFlag(package.isForSelf, package.isMyEff, package.mOwner);
                mTouchControl.ChangeTouchState(TouchState.SelectSkillTarget_state);
            }
            package = null;
        }

        public void onSingleTargetFind(ObjectCreature obj)
        {
            if (mMenuPanel.onSingleTargetFind(obj))
            {   // 找到目标
                mTouchControl.ChangeTouchState(TouchState.FireSign_state);
                mBloodControl.hideFlag();
            }
        }

        /// <summary>
        /// 检查技能是否已经锁定，已经锁定的技能释放锁定
        /// </summary>
        public bool isWaitLock(X_GUID uid)
        {
            if (mMenuPanel.onCheckSkillWaitLock(uid))
            {
                mTouchControl.ChangeTouchState(TouchState.FireSign_state);
                mBloodControl.hideFlag();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 强制移除技能目标选择，适用于切换到自动模式或者切换到别的技能
        /// </summary>
        /// <param name="i"></param>
        public void onReleaseTargetLock()
        {
            mTouchControl.ChangeTouchState(TouchState.FireSign_state);
            mBloodControl.hideFlag();
            mMenuPanel.onSkillReleaseData();
        }
        /// <summary>
        /// 战斗结束
        /// </summary>
        /// <param name="e"></param>
        private void onFightEndCall(GameEvent e)
        {
            if (isGameOver == false && !ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter) 
            {
                isWin = e.id == (int)GameEventID.F_BattleOver;
                StartCoroutine(showFightEnd(isWin));
                //Object prefab = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Fight/UI_FightendLayer");
                //GameObject obj = Instantiate(prefab) as GameObject;
                //obj.transform.SetParent(mFrontLayout.transform, false);
                //obj.AddComponent<UI_BattleendPanel>().setType(isWin);
            }
            
        }

        IEnumerator showFightEnd(bool isWin)
        {
            if (isWin)
            {
                //AudioControler.Inst.PlayMusic(m_VictoryAudio,false);

                //AddUI("UI_Fight/UI_VictoryEFF_2_2");
                //mMenuPanel.transform.SetParent(null, false);
                yield return new WaitForSeconds(2.5f);
                //ReMoveUI("UI_Fight/UI_VictoryEFF_2_2");
            }
            else
            {
                //AudioControler.Inst.PlayMusic(m_LoseAudio,false);

                //AddUI("UI_Fight/UI_DefeatEFF_2_2");
               // mMenuPanel.transform.SetParent(null, false);
                yield return new WaitForSeconds(2f);
              //  ReMoveUI("UI_Fight/UI_DefeatEFF_2_2");
            }
            if (m_IsShowFailUI)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_IsOpenSealBox);
            }
            
        }

        public void IsShowSealBox()
        {
            StageTemplate stageDate = null;
            if (ObjectSelf.GetInstance().GetIsPrompt())
            {
                stageDate = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(ObjectSelf.GetInstance().GetPromptCurCampaignID());
            }
            else
            {
                stageDate = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(ObjectSelf.GetInstance().GetCurCampaignID());
            }
            //if (stageDate.m_bossbox != -1)
            {
                if (ObjectSelf.GetInstance().GetIsOpenSealBox())
                {
                    AddUI("UI_Fight/UI_SealBox_1_1");
                    StopCoroutine(showFightEnd(isWin));
                }
                else
                {
                    if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter == false)
                    {
                        ShowCombat(isWin);
                    }
                }
            }
        }

        public void ShowCombat(bool isWin)
        {
            mMenuPanel.onGameOverCall();
            mMenuPanel.gameObject.SetActive(false);
            //Object prefab = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Fight/UI_FightendLayer_2_7");
            //GameObject obj = Instantiate(prefab) as GameObject;
            GameObject obj= UI_FightControler.Inst.AddUI(UI_BattleendPanel.UI_Res);
            //obj.transform.SetParent(mFrontLayout.transform, false);
            obj.AddComponent<UI_BattleendPanel>().setType(isWin==true?UI_BattleendPanel.PanelType.Win:UI_BattleendPanel.PanelType.Fail);
            //obj.AddComponent<UI_BattleendPanel>().setType(isWin);
        }

        public void MysteriousShopSpecialTips()
        {
            //UI_SpecialTips.CallByFightScene = (bool)data.data;
            UI_SpecialTips.MessageType = UI_SpecialTips.TipsType.MysteriousShop;
            GameObject go = AddUI(UI_SpecialTips.Path);
        }
        public void WorldBossPassTips(GameEvent gameEvent)
        {
            UI_WorldBossEndTips tips = AddUI(UI_WorldBossEndTips.GetPath()).GetComponent<UI_WorldBossEndTips>();
            tips.SetDataPack((BossPassDataPack) gameEvent.data);
            m_IsShowFailUI = false;
        }
        public void SpecialStageSpecialTips()
        {
            //UI_SpecialTips.CallByFightScene = (bool)data.data; ;
            UI_SpecialTips.MessageType = UI_SpecialTips.TipsType.SpecialStage;
            GameObject go = AddUI(UI_SpecialTips.Path);

        }

        /// <summary>
        /// 血量变化
        /// </summary>
        private void onHpChangeCall(GameEvent e)
        {
            UI_HurtInfo info = (UI_HurtInfo)e.data;
            if (info != null)
            {
                ObjectCreature obj = info.pTarget;
                mBloodControl.UpdateBloodValue(obj.GetGuid(), (float)obj.GetHP() / obj.GetMaxHP());
                if (obj.GetHP() < 0) return;
                if (obj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
                {
                    // hero
                    Transform trans = ((ObjectHero)obj).GetAnimation().EventControl.Pre_Head_T_EffectPoint;
                    if (info.nHurt > 0)
                    {
                        // 英雄加血
                        mNumberMgr.showNumber(System.Math.Abs(info.nHurt), HPNumberType.HP_HEAL, trans.position);
                    }
                    else
                    {
                        // 英雄掉血
                        mNumberMgr.showNumber(System.Math.Abs(info.nHurt), HPNumberType.HP_SELF_HURT, trans.position);
                    }
                }
                else if (obj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER)
                {
                    // monster
                    Transform trans = ((ObjectMonster)obj).GetAnimation().EventControl.Pre_Head_T_EffectPoint;
                    if (info.nHurt < 0)
                    {
                        //敌人掉血
                        if (info.bCritical)
                        {
                            // 暴击
                            mNumberMgr.showNumber(System.Math.Abs(info.nHurt), HPNumberType.HP_HEAVY, trans.position);
                        }
                        else
                        {
                            mNumberMgr.showNumber(System.Math.Abs(info.nHurt), HPNumberType.HP_ENEMY_HURT, trans.position);
                        }
                    }
                    else
                    {
                        // 敌人加血
                        mNumberMgr.showNumber(System.Math.Abs(info.nHurt), HPNumberType.HP_HEAL, trans.position);
                    }
                }

            }
        }

        private void onMissCall(GameEvent e)
        {
            ObjectCreature obj = (ObjectCreature)e.data;
            Transform trans = null;
            if (obj != null)
            {
                if (obj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
                {
                    // hero
                    trans = ((ObjectHero)obj).GetAnimation().EventControl.Pre_Head_T_EffectPoint;
                }
                else
                {
                    trans = ((ObjectMonster)obj).GetAnimation().EventControl.Pre_Head_T_EffectPoint;
                }
                if (trans != null)
                    mNumberMgr.showNumber(0, HPNumberType.HP_MISS, trans.position);
            }
        }

        private void onHPNumberCall(int inum, HPNumberType type, Vector3 position)
        {
            mNumberMgr.showNumber(inum, type, position);
        }

        // 显示暂停页
        public void onPauseCall()
        {
            GameObject trans = Instantiate(pausePanelPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            trans.transform.SetParent(mFrontLayout.transform, false);
        }

        // 切换到自动模式
        public void onAutoCall()
        {
            mTouchControl.ChangeTouchState(TouchState.FireSign_state);
            mBloodControl.hideFlag();
        }

    }
}
