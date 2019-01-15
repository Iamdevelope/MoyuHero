using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameSceneEditor;
using DreamFaction.GameEventSystem;
using DG.Tweening;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.SkillCore;
using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameAudio;
namespace DreamFaction.GameCore
{
    public enum FightState
    {
        none = 0,
        prepareData,					// 初始化战斗 [1/19/2015 Zmy]
        HeroMove,						// 英雄推进[1/19/2015 Zmy]
        HeroMonmentMoveEnter,           // 英雄瞬间进入
        HeroMonmentMoveIng,             // 英雄瞬间移动中 
        HeroMonmentMoveExit,            // 英雄瞬间移动退出
        prepareEnemy,					// 准备敌对对象 [1/19/2015 Zmy]
        prepareEnemy_over,              // 准备敌对对象完成 [3/23/2015 zcd]
        FightStory,                     // 战斗剧情的处理 [1/21/2015 Zmy]
        FightStoryCamera,               // 开场剧情的处理 [10/17/2015 Dpf]
        FightInstantiateHero,               // 开场剧情的处理 [10/17/2015 Dpf]
        prepareFight,					// 准备战斗,用于修正队伍位置 [1/19/2015 Zmy]
        Fighting,						// 战斗状态 [1/19/2015 Zmy]
        FightOver,						// 回合战斗结束 [1/19/2015 Zmy]
        FightWin,						// 战斗胜利结算 [1/19/2015 Zmy]
        FightLose,                      // 战斗失败结算 [3/26/2015 Zmy]
        //载具;
        PrepareBoard,                   // 准备上载具;
        PrepareBoardOver,               // 已经上载具;
        Boarding,                       // 载具移动;
        BoardingOver,                   // 载具移动结束;
    };
    /// <summary>
    ///  战斗控制器，通用 [1/19/2015 Zmy]
    /// </summary>
    public class FightControler : BaseControler
    {
        public AudioClip m_BossAudio = null;     //boss战背景音乐
        public AudioClip m_Battle1Audio = null;  //通常战斗地图背景音乐
        public AudioClip m_Battle2Audio = null;  //限时关和极限试炼

        /// <summary>
        ///   单例[1/19/2015 Zmy]
        /// </summary>
        private static FightControler _inst;

        /// <summary>
        /// 当前关卡ID
        /// </summary>
        private int m_StageID;
        private StageTemplate m_StageRow;//当前关卡数据
        /// <summary>
        ///   战斗控制器当前状态[1/19/2015 Zmy]
        /// </summary>
        private FightState m_CurState;
        /// <summary>
        /// 战斗AI模式
        /// </summary>
        private EM_SPELL_AI_TYPE m_FightAIstate;
        /// <summary>
        ///   战斗控制器上一个状态[1/19/2015 Zmy]
        /// </summary>
        private FightState m_LastState;

        /// <summary>
        ///   场景中战斗回合数[1/20/2015 Zmy]
        /// </summary>
        private int m_RoundInScene;

        /// <summary>
        ///   战斗总回合数;
        /// </summary>
        private int m_RoundInTotal;

        /// <summary>
        ///   进入战斗时候的人物位置修正信息的[1/20/2015 Zmy]
        /// </summary>
        private int m_ReviseHeroPosCount;
        private int m_MonmentMoveHeroPosCount;//进入瞬间移动的时候人物修正信息
        private int m_DisappearCount;//瞬间移动消失个数
        private int m_ShowCount;     //瞬间移动出现个数
        private int m_BoardHeroCount = 0;  //上载具的英雄个数;

        /// <summary>
        ///  战场倒计时
        /// </summary>
        private int m_FightCountDown;
        private float m_ElapsedCountDown;//战场已经过时间
        private bool m_bActiveCountDown;//是否启动战场倒计时

        private BattleAnger m_HeroPower;   //本方怒气 
        private BattleAnger m_EnemyPower;  //敌方怒气 

        private float moveTime = 0.0f;

        // 新手引导相关
        private int m_GuideCount = 1;
        private float m_CurTime = 0.0f;
        private bool m_IsTriger = false;

        public static bool isOpeningAnimation = false;

        // ============================= 公共属性(限制外部修改) ===================
        public static FightControler Inst
        {
            get
            {
                return _inst;
            }
        }
        public int StageID
        {
            get
            {
                return m_StageID;
            }
        }
        public int CurRound
        {
            get
            {
                return m_RoundInScene;
            }

            private set
            {
                m_RoundInScene = value;
                if (ObjectSelf.GetInstance().IsLimitFight)
                {
                    m_RoundInTotal = ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum;
                }
                else
                {
                    m_RoundInTotal = value;
                }
            }
        }

        public int FightCountDown
        {
            get { return m_FightCountDown; }
        }
        public float ElapsedCountDown
        {
            get { return m_ElapsedCountDown; }
        }

        // 获取战斗轮次
        public int getTotalRound()
        {
            return m_StageRow.GetRoundTime();
        }
        // ============================= 公共接口 =================================
        public void SetFightState(FightState state)
        {
            m_CurState = state;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_FightStateUpdate);
        }

        public FightState GetFightState()
        {
            return m_CurState;
        }
        public void SetFightAIState(EM_SPELL_AI_TYPE AItype)
        {
            m_FightAIstate = AItype;
        }
        public EM_SPELL_AI_TYPE GetFightAIState()
        {
            return m_FightAIstate;
        }
        // 战场剩余时间 [3/26/2015 Zmy]
        public int GetRemainingCountTime()
        {
            return m_FightCountDown - (int)m_ElapsedCountDown;
        }
        /// <summary>
        /// 更新战斗怒气值。
        /// </summary>
        /// <param name="nGroupType">要更新的所属阵营</param>
        /// <param name="nValue">要更新的值</param>
        public void OnUpdatePowerValue(EM_OBJECT_TYPE nGroupType, int nValue)
        {
            if (nValue == 0)
                return;

            if (nGroupType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                m_HeroPower.OnUpdatePowerValue(nValue);
            }
            else
            {
                m_EnemyPower.OnUpdatePowerValue(nValue);
            }
        }
        /// <summary>
        /// 根据百分比，更新怒气值
        /// </summary>
        /// <param name="nGroupType">所属阵营</param>
        /// <param name="nPercent">百分比</param>
        public void OnUpdatePowerPercent(EM_OBJECT_TYPE nGroupType, float nPercent)
        {
            if (nGroupType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                m_HeroPower.OnUpdatePowerPercent(nPercent);
            }
            else
            {
                m_EnemyPower.OnUpdatePowerPercent(nPercent);
            }
        }
        // 根据阵营返回当前怒气值 [3/2/2015 Zmy]
        public int GetPowerValue(EM_OBJECT_TYPE nGroupType)
        {
            if (nGroupType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                return m_HeroPower.GetPowerValue();
            }
            else if (nGroupType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER)
            {
                return m_EnemyPower.GetPowerValue(); ;
            }
            else
                return -1;
        }
        /// <summary>
        /// 获取当前单位怒气数量
        /// </summary>
        /// <param name="nGroupType"></param>
        /// <returns></returns>
        public int GetPowerPoint(EM_OBJECT_TYPE nGroupType)
        {
            if (nGroupType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                return m_HeroPower.GetCurPowerPoint();
            }
            return -1;
        }
        /// <summary>
        /// 获取当前单位怒气值
        /// </summary>
        /// <param name="nGroupType"></param>
        /// <returns></returns>
        public int GetPowerPointValue(EM_OBJECT_TYPE nGroupType)
        {
            if (nGroupType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                return m_HeroPower.GetCurPowerPointValue();
            }
            return -1;
        }
        public List<GameObject> GetHeroGameObjectList()
        {
            return SceneObjectManager.GetInstance().GetGameObjectListForHero();
        }
        public List<GameObject> GetEnemyGameObjectList()
        {
            return SceneObjectManager.GetInstance().GetGameObjectListForMonster();
        }

        // ============================= 继承接口 =================================
        // 第一步初始化数据 [1/19/2015 Zmy]
        protected override void InitData()
        {
            if (_inst == null)
            {
                _inst = this;
            }
            else
            {
                GameObject.Destroy(this);
            }
            CurRound = 0;
            if (ObjectSelf.GetInstance().GetIsPrompt())
            {
                m_StageID = ObjectSelf.GetInstance().GetPromptCurCampaignID();
            }
            else
            {
                m_StageID = ObjectSelf.GetInstance().GetCurCampaignID();
            }
            m_StageRow = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(m_StageID);
            m_FightCountDown = m_StageRow.m_fightTime;
            m_ReviseHeroPosCount = 0;
            m_MonmentMoveHeroPosCount = 0;
            m_DisappearCount = 0;
            m_HeroPower = new BattleAnger(EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO);
            m_EnemyPower = new BattleAnger(EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER);

            SceneManager.Inst.EndChangeScene(SceneEntry.Fight.ToString());
            this.gameObject.AddComponent<FightEditorContrler>();
            this.gameObject.AddComponent<SceneObjectManager>();
            this.gameObject.AddComponent<EffectManager>();
            this.gameObject.AddComponent<GameTimeControler>();
            //初始化英雄和怪物AI检测时间间隔
            AILogicHero.GetInstance().SetAItime(DataTemplate.GetInstance().m_GameConfig.getPlayer_ai_check());
            AILogicMonster.GetInstance().SetAItime(m_StageRow.m_aiCheck);
            SetFightAIState(EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_INVALID);

            // 添加UI
            Instantiate(Resources.Load("UI/Prefabs/Core/UI_FightControler"), Vector3.zero, Quaternion.identity);

            //监听初始化编辑器完成事件 [1/20/2015 Zmy]
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_FightEditorLoadDone, CallBack_InitHeroEffect);
            //监听触发战斗事件准备创建怪物事件 [1/20/2015 Zmy]
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_PrepareEnemy, CallBack_PrepareEnemy);
            //监听进入战斗状态事件 [1/20/2015 Zmy]
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_EnterFightState, CallBack_EnterFightState);
            //监听剧情播放事件 [1/21/2015 Zmy]
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryEnter, CallBack_StoryEnter);
            //监听剧情开场播放事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryCameraEnter, CallBack_StoryCameraEnter);
            //监听瞬间移动进入事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_HeroPathMomentMoveEnter, CallBack_eroPathMomentMoveEnter);
            //监听瞬间移动退出事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_HeroPathMomentMoveExit, CallBack_eroPathMomentMoveExit);
            //准备登上载具;
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_PrepareBoard, CallBack_PrepareBoard);
            //下船;
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_BoardingOver, CallBack_BoardingOver);

            m_BossAudio = UIResourceMgr.LoadPrefab(common.AudioPath + "boss1") as AudioClip;
            m_Battle1Audio = UIResourceMgr.LoadPrefab(common.AudioPath + "battle1") as AudioClip;
            m_Battle2Audio = UIResourceMgr.LoadPrefab(common.AudioPath + "battle2") as AudioClip;
            
        }
        //第二步初始化状态 [1/19/2015 Zmy]
        protected override void InitState()
        {
            SetFightState(FightState.none);
            m_LastState = FightState.none;
        }
        //第三步初始化显示对象 [1/19/2015 Zmy]
        protected override void InitView()
        {
            InitEditObject();
            InitUIObject();
            //InitHeroObject();

            //等待编辑器初始化完成 [1/20/2015 Zmy]

            if (ObjectSelf.GetInstance().LimitFightMgr.Activate || ObjectSelf.GetInstance().GetIsPrompt())
            {
                AudioControler.Inst.PlayMusic(m_Battle2Audio);
            }
            else if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)
            {
                AudioControler.Inst.PlayMusic(m_BossAudio);
            }
            else
            {
                AudioControler.Inst.PlayMusic(m_Battle1Audio);
            }
        }

        protected override void DestroyData()
        {
            GameEventDispatcher.Inst.clearEvents();
            GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Normal);
        }

        //数据更新。状态修改时，只进入一次，基类调用顺序优先级最高。 [1/19/2015 Zmy]
        protected override void UpdateData()
        {
            ObjectSelf.GetInstance().LimitFightMgr.UpdateData();

            if (m_CurState != m_LastState)
            {
                m_LastState = m_CurState;
                switch (m_CurState)
                {
                    case FightState.prepareData:
                        SetFightState(FightState.HeroMove);
                        m_bActiveCountDown = m_FightCountDown < 0 ? false : true;
                        break;
                    case FightState.HeroMove:
                        //HeroForward();
                        break;
                    case FightState.HeroMonmentMoveEnter:
                        break;
                    case FightState.HeroMonmentMoveIng:

                        break;
                    case FightState.HeroMonmentMoveExit:
                        break;
                    case FightState.prepareEnemy:
                        InstantiateMonsterObj();

                        break;
                    case FightState.prepareEnemy_over:

                        break;
                    case FightState.FightStory:
                        break;
                    case FightState.FightStoryCamera:
                        FightEditorContrler.GetInstantiate().CamPlay();
                        break;
                    case FightState.prepareFight:

                        break;
                    case FightState.Fighting:

                        break;
                    case FightState.FightOver:
                        OnRoundOver();
                        break;
                    case FightState.FightWin:
                        OnSceneFightWin();
                        break;
                    case FightState.FightLose:
                        OnSceneFightLose();
                        break;
                    case FightState.PrepareBoard:
                        break;
                    case FightState.PrepareBoardOver:
                        SetFightState(FightState.Boarding);
                        break;
                    case FightState.Boarding:
                        break;
                    case FightState.BoardingOver:
                        break;
                    case FightState.FightInstantiateHero:
                        if (FightControler.isOpeningAnimation)
                        {
                            FightControler.isOpeningAnimation = false;
                            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StoryEnd);
                            //FightEditorContrler.GetInstantiate().CamPause();
                            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_FightEditorLoadDone);
                        }
                        else
                        {
                            SetFightState(FightState.HeroMove);
                        }
                        break;
                }
            }
        }
        // 状态更新，实时状态刷新逻辑 [1/19/2015 Zmy]
        protected override void UpdateState()
        {
            if (m_bActiveCountDown)
            {
                m_ElapsedCountDown += Time.deltaTime;
                if (m_ElapsedCountDown > m_FightCountDown)
                {
                    if (m_CurState != FightState.FightWin)
                    {
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_CountDownOver);
                        SetFightState(FightState.FightLose);
                        m_bActiveCountDown = false;
                    }
                }
            }
            switch (m_CurState)
            {
                case FightState.prepareData:
                    break;
                case FightState.HeroMove:
                    HeroForward();
                    UpdateHeroForward();
                    break;
                case FightState.HeroMonmentMoveEnter:
                    break;
                case FightState.HeroMonmentMoveIng:
                    UpdateMomentMoveIng();
                    break;
                case FightState.HeroMonmentMoveExit:
                    UpdateMomentMoveExit();
                    break;
                case FightState.prepareEnemy:
                    //准备怪物阶段，英雄依然处于移动状态 [1/20/2015 Zmy]
                    UpdateHeroForward();
                    break;
                case FightState.FightStory:
                    //UpdateHeroForward();
                    break;
                case FightState.FightStoryCamera:
                    //UpdateHeroForward();
                    break;                    
                case FightState.prepareEnemy_over:
                    UpdateHeroForward();
                    break;
                case FightState.prepareFight:
                    break;
                case FightState.Fighting:
                    break;
                case FightState.FightOver:
                    break;
                case FightState.FightWin:
                    break;
                case FightState.FightLose:
                    break;
                case FightState.PrepareBoard:
                    UpdateHeroForward();
                    break;
                case FightState.PrepareBoardOver:
                    break;
                case FightState.Boarding:
                    UpdateHeroBoarding();
                    break;
                case FightState.BoardingOver:
                    UpdateHeroForward();
                    break;
                case FightState.FightInstantiateHero:
                    break;
            }
        }

        // 新手引导添加
        protected override void UpdateView()
        {
            base.UpdateView();
            if (m_IsTriger)
            {
                m_CurTime += Time.deltaTime;
                if (m_CurTime > 1.0f)
                {
                    m_CurTime = 0.0f;
                    m_IsTriger = false;

                    if (m_GuideCount == 1)
                    {
                        m_GuideCount += 1;
                        // 点击【切换模式】 100308
                        if(GuideManager.GetInstance().IsContentGuideID(100308) == false)
                            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Guide_Fighting, 100308);
                    }
                    else
                    {
                        // 点击【众神之主技能】 100311 
                        if(GuideManager.GetInstance().IsContentGuideID(100311) == false)
                            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Guide_Fighting, 100311);
                    }
                }
            }
        }

        // ============================= 私有函数 =================================
        //实例化编辑器对象
        private void InitEditObject()
        {

        }
        //实例化UI对象
        private void InitUIObject()
        {

        }

        // 英雄移动 [1/20/2015 Zmy]
        private void HeroForward()
        {
            FightEditorContrler.GetInstantiate().CamPlay();
            FightEditorContrler.GetInstantiate().HeroPathPlay();
            FightEditorContrler.GetInstantiate().HeroPathNormalMove();
            FightEditorContrler.GetInstantiate().HeroPathSetFormation();
        }

        //移动中更新obj位置 [1/20/2015 Zmy]
        private void UpdateHeroForward()
        {
            moveTime += Time.deltaTime;
            if (moveTime < 0.1f)
            {
                //控制刷新延迟 [1/21/2015 Zmy]
                return;
            }
            moveTime = 0.0f;
            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {
                ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                if (HeroObj != null && HeroObj.IsAlive())
                {
                    NavMeshAgent Nav = HeroObj.GetNavMesh();
                    Nav.speed = HeroObj.GetMoveSpeed();
                    Nav.SetDestination(FightEditorContrler.GetInstantiate().GetFormationPos(HeroObj));

                    HeroObj.SetObjectActionState(ObjectCreature.ObjectActionState.forward);

                }
            }
        }

        private void UpdateHeroBoarding()
        {
            moveTime += Time.deltaTime;

            if (moveTime < 0.1f)
            {
                return;
            }

            moveTime = 0f;

            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; i++ )
            {
                ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                if (HeroObj != null && HeroObj.IsAlive())
                {
                    NavMeshAgent Nav = HeroObj.GetNavMesh();
                    Nav.speed = HeroObj.GetMoveSpeed();
                    Nav.SetDestination(FightEditorContrler.GetInstantiate().GetFormationPos(HeroObj));
                    //HeroObj.SetWorldPosRotation(FightEditorContrler.GetInstantiate().GetFormationPos(HeroObj), FightEditorContrler.GetInstantiate().GetFormationAngle(HeroObj));
                    HeroObj.SetObjectActionState(ObjectCreature.ObjectActionState.boarding);
                    HeroObj.GetAnimation().Anim_Fidle(false);
                }
            }
        }

        //实例化怪物 [1/20/2015 Zmy]
        private void InstantiateMonsterObj()
        {
            int MonsterNum = FightEditorContrler.GetInstantiate().GetMonsterGroupEditorData(m_RoundInScene).Count;
            FightEditorContrler.GetInstantiate().SetMonsterTroopType(ObjectSelf.GetInstance().LimitFightMgr.m_MonsterTroopType);
            FightEditorContrler.GetInstantiate().SetBeginFightCount(ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum);

            for (int i = 0; i < MonsterNum; ++i)
            {
                int nTableID = SceneObjectManager.GetInstance().GetMosnterBundleRes(m_RoundInTotal, i);
                MonsterTemplate pRow = (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(nTableID);
                ArtresourceTemplate art = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(pRow.getArtresources());
                string _res = art.getArtresources();
                GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(_res);
                GameObject obj = Instantiate(_AssetRes,
                                               FightEditorContrler.GetInstantiate().GetMonsterGroupEditorData(m_RoundInScene)[i].MyPos,
                                               FightEditorContrler.GetInstantiate().GetMonsterGroupEditorData(m_RoundInScene)[i].MyAngle) as GameObject;
                GameUtils.SetLayerRecursively(obj, FightEditorContrler.GetInstantiate().GetShadowCullMaskLayer());
                float _zoom = art.getArtresources_zoom() * pRow.getMonsterEnlarge();
                obj.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);

                FightEditorContrler.GetInstantiate().SetMonsterBirthState(obj, FightEditorContrler.GetInstantiate().GetMonsterGroupEditorData(m_RoundInScene)[i], false);
                SceneObjectManager.GetInstance().SceneObjectAddMonster(obj, m_RoundInTotal, i);

                
                //Transform _body = null;
                //_body = obj.transform.FindChild("Body");
                //if (_body != null)
                //{
                //    _body.gameObject.SetActive(false);
                //    StartCoroutine(OnShowMonsterObj(_body));
                //}

                for (int m = 0, n = obj.transform.childCount; m < n; m++ )
                {
                    obj.transform.GetChild(m).gameObject.SetActive(false);
                }
                StartCoroutine(OnShowMonsterObj(obj.transform));

                //怪物入场怒气加成 [10/17/2015 Zmy]
                AngertableTemplate _data = (AngertableTemplate)DataTemplate.GetInstance().m_AngerTable.getTableData(pRow.getFuryId());
                if (_data == null)
                    continue;

                m_EnemyPower.OnUpdatePowerValue(_data.getStartFury());
            }

//             //每波怪物初始化时候，初始敌方怒气值 [3/3/2015 Zmy]
//             if (m_RoundInScene - 1 >= 0 && m_RoundInScene - 1 < m_StageRow.m_waveFury.Length)
//             {
//                 int nValue = m_StageRow.m_waveFury[m_RoundInScene - 1];
//                 m_EnemyPower.OnUpdatePowerValue(nValue);
//             }

            //初始化释放怪物的被动技能 [7/31/2015 Zmy]
            SceneObjectManager.GetInstance().OnMonsterFreePassiveSpell();

            // add by zcd
            SetFightState(FightState.prepareEnemy_over);
        }
        //防止策划种怪时候摆放不准(坑爹啊)
        private IEnumerator OnShowMonsterObj(Transform obj)
        {
            yield return new WaitForSeconds(0.5f);
            //obj.gameObject.SetActive(true);
            for (int i = 0, j = obj.childCount; i < j;  i++)
            {
                obj.GetChild(i).gameObject.SetActive(true);
            }
        }
        //当前战斗结束 [1/21/2015 Zmy]
        private void OnRoundOver()
        {
            //每回合结束 清空敌方的怒气值 [3/3/2015 Zmy]
            m_EnemyPower.OnUpdatePowerValue(-m_EnemyPower.GetPowerValue());
            //击杀一波怪物后，对本方怒气进行奖励刷新 [3/3/2015 Zmy]
            EachRewardPower();


            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_BattleRoundOver);

            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                ObjectSelf.GetInstance().LimitFightMgr.SendRoundOver();
            }
            else
            {
                //胜利条件
                if (m_RoundInScene == FightEditorContrler.GetInstantiate().GetMonsterGroupCount())
                {
                    SetFightState(FightState.FightWin);
                }
                else
                {
                    FightEditorContrler.GetInstantiate().CamPause();
                    SetFightState(FightState.HeroMove);
                }
            }
            //每回合结束 清空战斗数据。 [6/26/2015 Zmy]
            SceneObjectManager.GetInstance().UpdateRoundOver();
        }
        //当前场景战斗结束 [3/13/2015 Zmy]
        private void OnSceneFightWin()
        {
            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {

                ObjectHero pHero = SceneObjectManager.GetInstance().GetHeroObject(i);
                NavMeshAgent Nav = pHero.GetNavMesh();
                Nav.SetDestination(pHero.GetGameObject().transform.position);
                pHero.GetAnimation().Anim_Fidle(false);
            }
            // 战斗胜利先刷新奖励数据 [4/2/2015 Zmy]
            //ObjectSelf.GetInstance().UpdateDataBattleWin();
            // 战斗胜利后刷新关卡数据 [4/8/2015 Zmy]
            SceneObjectManager.GetInstance().UpdateBattleWinStageData();
            GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Normal);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_BattleOver);
        }

        private void OnSceneFightLose()
        {
            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {

                ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                if (HeroObj != null && HeroObj.IsAlive())
                {
                    NavMeshAgent Nav = HeroObj.GetNavMesh();
                    Nav.SetDestination(HeroObj.GetGameObject().transform.position);
                    HeroObj.GetAnimation().Anim_Fidle(false);
                }
            }

            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                ObjectSelf.GetInstance().LimitFightMgr.SendRoundOver();
            }
            else
            {
                if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)
                {
                    ObjectSelf.GetInstance().WorldBossMgr.SendRoundOver();
                }
                else
                {
                    CEndBattle battle = new CEndBattle();
                    battle.pass = 0;// 未通过
                    IOControler.GetInstance().SendProtocol(battle);
                }
            }

            GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Normal);

        }
        // ============================= 事件响应回调函数 =================================

        //实例化hero出场特效;
        private void CallBack_InitHeroEffect()
        {
            for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
            {
                int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                if (nGroup < 0 && nGroup >= GlobalMembers.MAX_MATRIX_COUNT)
                    continue;
                X_GUID pMemberGuiD = null;

                if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
                {
                    pMemberGuiD = ObjectSelf.GetInstance().LimitFightMgr.m_HeroInfo[i];
                    if (ObjectSelf.GetInstance().LimitFightMgr.m_HeroHp[i] == 0)
                    {
                        //初始英雄时，如果血量记录是0 代表本次是继续开启的试炼。并且上次的英雄已死亡。那么本次不再实例化英雄 [6/19/2015 Zmy]
                        pMemberGuiD.CleanUp();
                    }
                }
                else
                {
                    pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i]; //暂时默认上场第一组阵型的英雄[3/25/2015 Zmy]
                }
                if (!pMemberGuiD.IsValid())
                    continue;
                ObjectCard pHero = ObjectSelf.GetInstance().HeroContainerBag.FindHero(pMemberGuiD);
                if (pHero == null)
                    continue;
                ArtresourceTemplate _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(pHero.GetHeroData().GetHeroViewID());
                if (_Artresourcedata == null)
                    continue;

                GameObject pHeroObject = AssetLoader.Inst.GetAssetRes(_Artresourcedata.getArtresources());
                if (pHeroObject != null)
                {
                    //创建出场特效;
                    GameObject eff = Instantiate(Resources.Load("Effect/Effect_Chuchang01"), FightEditorContrler.GetInstantiate().GetFormationCenterPos(i), FightEditorContrler.GetInstantiate().GetFormationCenterAngle(i)) as GameObject;
                    Destroy(eff, 0.3f);
                }
            }

            Invoke("CallBack_InitHeroObject", 0.3f);
        }

        //实例化Hero对象
        private void CallBack_InitHeroObject()
        {
            for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
            {
                int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                if (nGroup < 0 && nGroup >= GlobalMembers.MAX_MATRIX_COUNT)
                    continue;
                X_GUID pMemberGuiD = null;

                if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
                {
                    pMemberGuiD = ObjectSelf.GetInstance().LimitFightMgr.m_HeroInfo[i];
                    if (ObjectSelf.GetInstance().LimitFightMgr.m_HeroHp[i] == 0)
                    {
                        //初始英雄时，如果血量记录是0 代表本次是继续开启的试炼。并且上次的英雄已死亡。那么本次不再实例化英雄 [6/19/2015 Zmy]
                        pMemberGuiD.CleanUp();
                    }
                }
                else
                {
                    pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i]; //暂时默认上场第一组阵型的英雄[3/25/2015 Zmy]
                }
                if (!pMemberGuiD.IsValid())
                    continue;
                ObjectCard pHero = ObjectSelf.GetInstance().HeroContainerBag.FindHero(pMemberGuiD);
                if (pHero == null)
                    continue;
                ArtresourceTemplate _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(pHero.GetHeroData().GetHeroViewID());
                if (_Artresourcedata == null)
                    continue;

                GameObject pHeroObject = AssetLoader.Inst.GetAssetRes(_Artresourcedata.getArtresources());
                if (pHeroObject != null)
                {
                    GameObject obj1 = Instantiate(pHeroObject, FightEditorContrler.GetInstantiate().GetFormationCenterPos(i), FightEditorContrler.GetInstantiate().GetFormationCenterAngle(i)) as GameObject;
                    GameUtils.SetLayerRecursively(obj1, FightEditorContrler.GetInstantiate().GetShadowCullMaskLayer());
                    float _zoom = _Artresourcedata.getArtresources_zoom();
                    obj1.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
                    SceneObjectManager.GetInstance().SceneObjectAddHero(obj1, pHero.GetHeroRow().getId(), pHero);
                    FightEditorContrler.GetInstantiate().InitFormationPos(pHero, i);


                }
            }
            FightEditorContrler.GetInstantiate().HeroPathInitSpeed();

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StoryCameraEnd);

            //初始化完成后，切换状态 [1/20/2015 Zmy]
            SetFightState(FightState.prepareData);

            //初始入场怒气
            InitFightPower();


        }

        //实例化怪物 [1/20/2015 Zmy]
        private void CallBack_PrepareEnemy(GameEvent _event)
        {
            //编辑器传递的第几回合怪物
            CurRound = (int)_event.data;
            SetFightState(FightState.prepareEnemy);
        }

        //进入战斗状态开始战斗，在这之前先修正一下人物位置 [1/20/2015 Zmy]
        private void CallBack_EnterFightState()
        {
            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {
                //                 ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                //                 if (HeroObj != null)
                //                 {
                //                     GameObject obj = HeroObj.GetGameObject();
                //                     if (obj != null)
                //                     {
                //                         obj.GetComponent<NavMeshAgent>().Stop();
                //                         float _heroSpeed = HeroObj.GetMoveSpeed();
                //                         float _moveTime = Vector3.Distance(obj.transform.position, FightEditorContrler.inst.GetFormationPos(HeroObj.GetGameObject())) / _heroSpeed;
                //                         //修正高度值 [1/22/2015 Zmy]
                //                         Vector3 targetPos = new Vector3(FightEditorContrler.inst.GetFormationPos(HeroObj.GetGameObject()).x, obj.transform.position.y, FightEditorContrler.inst.GetFormationPos(HeroObj.GetGameObject()).z);
                //                         obj.transform.DOMove(targetPos, _moveTime).SetUpdate(true).SetEase(Ease.Linear).OnComplete(()=>CallBack_RevisepPos(obj,HeroObj));
                //                     }
                //                 }

                ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                if (HeroObj != null && HeroObj.IsAlive())
                {
                    HeroObj.GetAnimation().Anim_Run();
                    NavMeshAgent Nav = HeroObj.GetNavMesh();
                    Nav.speed = HeroObj.GetMoveSpeed();
                    Nav.SetDestination(FightEditorContrler.GetInstantiate().GetFormationPos(HeroObj));

                    StartCoroutine(DelayCall(Nav, HeroObj.GetGameObject(), HeroObj, CallBack_RevisepPos));

                }
            }

            //切换准备战斗状态
            SetFightState(FightState.prepareFight);

            //重算一下移动位置偏移量;
            HeroPathtContrler.GetInstantiate().ResetMoveTargetOffset();
        }
        /// <summary>
        /// 修正位置的回调
        /// </summary>
        private void CallBack_RevisepPos(GameObject obj, ObjectHero heroObj)
        {
            m_ReviseHeroPosCount++;
            SetFightState(FightState.Fighting);
            heroObj.SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
            if (m_ReviseHeroPosCount == 1)
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StopMonsterBirth);
            if (m_ReviseHeroPosCount == SceneObjectManager.GetInstance().GetObjectHeroCount())
            {
                m_ReviseHeroPosCount = 0;
                m_IsTriger = true;
            }

            //TODO::修正所有怪物状态为攻击状态---（先这么写以后待细细考虑）;
            SceneObjectManager.GetInstance().ObjectMonsterAllAttack();
        }
        //瞬间移动进入前位置修正
        private void CallBack_MomentMoveEnter(GameObject obj, ObjectHero heroObj)
        {
            m_MonmentMoveHeroPosCount++;
            heroObj.SetObjectActionState(ObjectCreature.ObjectActionState.monmentmoveIng);
            //heroObj.GetAnimation().Anim_Fidle(false);
            if (m_MonmentMoveHeroPosCount == SceneObjectManager.GetInstance().GetObjectHeroCount())
            {
                m_MonmentMoveHeroPosCount = 0;
                SetFightState(FightState.HeroMonmentMoveIng);
            }
        }
        //英雄慢慢消失后瞬间移动
        private void UpdateMomentMoveIng()
        {
            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {
                ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                if (HeroObj != null && HeroObj.IsAlive())
                {
                    //..慢慢消失
                    m_DisappearCount++;
                    HeroObj.GetNavMesh().enabled = false;
                    HeroObj.OnConcealThis();
                }
            }
            if (m_DisappearCount == SceneObjectManager.GetInstance().GetObjectHeroCount())
            {
                m_DisappearCount = 0;
                FightEditorContrler.GetInstantiate().HeroPathNormalMove();
                SetFightState(FightState.HeroMonmentMoveEnter);
            }
        }
        //英雄瞬间移动结束慢慢出现
        private void UpdateMomentMoveExit()
        {
            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {
                ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                if (HeroObj != null && HeroObj.IsAlive())
                {
                    //..慢慢出现
                    m_ShowCount++;
                }
            }
            if (m_ShowCount == SceneObjectManager.GetInstance().GetObjectHeroCount())
            {
                m_ShowCount = 0;
                FightEditorContrler.GetInstantiate().HeroPathNormalMove();
                SetFightState(FightState.HeroMove);
                for (int i = 0; i < nHeroCount; ++i)
                {
                    ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                    if (HeroObj != null && HeroObj.IsAlive())
                    {
                        HeroObj.GetNavMesh().enabled = true;
                        HeroObj.SetObjectActionState(ObjectCreature.ObjectActionState.forward);
                    }
                }
            }
        }
        /// <summary>
        /// 剧情播放进入
        /// </summary>
        private void CallBack_StoryEnter()
        {
            SetFightState(FightState.FightStory);
            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {
                ObjectHero pHero = SceneObjectManager.GetInstance().GetHeroObject(i);
                pHero.GetAnimation().Anim_Fidle(false);
            }
        }

        /// <summary>
        /// 开场剧情播放进入
        /// </summary>
        private void CallBack_StoryCameraEnter()
        {
            SetFightState(FightState.FightStoryCamera);
            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {
                ObjectHero pHero = SceneObjectManager.GetInstance().GetHeroObject(i);
                pHero.GetAnimation().Anim_Fidle(false);
            }
        }
        //瞬间移动事件进入
        public void CallBack_eroPathMomentMoveEnter()
        {
            SetFightState(FightState.HeroMonmentMoveEnter);
            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {
                ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                if (HeroObj != null && HeroObj.IsAlive())
                {
                    HeroObj.GetAnimation().Anim_Run();
                    NavMeshAgent Nav = HeroObj.GetNavMesh();
                    Nav.speed = HeroObj.GetMoveSpeed();
                    Nav.SetDestination(FightEditorContrler.GetInstantiate().GetFormationPos(HeroObj));
                    //Debug.Log(FightEditorContrler.GetInstantiate().GetFormationPos(HeroObj));
                    StartCoroutine(DelayCallPos(FightEditorContrler.GetInstantiate().GetFormationPos(HeroObj), HeroObj.GetGameObject(), HeroObj, CallBack_MomentMoveEnter));
                }
            }
        }
        private void CallBack_eroPathMomentMoveExit()
        {
            SetFightState(FightState.HeroMonmentMoveExit);
        }

        private void CallBack_PrepareBoard()
        {
            SetFightState(FightState.PrepareBoard);

            int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            for (int i = 0; i < nHeroCount; ++i)
            {
                ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
                if (HeroObj != null && HeroObj.IsAlive())
                {
                    HeroObj.GetAnimation().Anim_Run();
                    NavMeshAgent Nav = HeroObj.GetNavMesh();
                    Nav.speed = HeroObj.GetMoveSpeed();
                    Nav.SetDestination(FightEditorContrler.GetInstantiate().GetFormationPos(HeroObj));

                    StartCoroutine(DelayCall(Nav, HeroObj.GetGameObject(), HeroObj, CallBack_PrepareBoardPos));
                }
            }
        }

        private void CallBack_BoardingOver()
        {
            SetFightState(FightState.BoardingOver);

            //int nHeroCount = SceneObjectManager.GetInstance().GetObjectHeroCount();
            //for (int i = 0; i < nHeroCount; ++i)
            //{
            //    ObjectHero HeroObj = SceneObjectManager.GetInstance().GetHeroObject(i);
            //    if (HeroObj != null && HeroObj.IsAlive())
            //    {
            //        HeroObj.GetAnimation().Anim_Run();
            //        HeroObj.SetObjectActionState(ObjectCreature.ObjectActionState.forward);
            //    }
            //}
        }

        /// <summary>
        /// 准备上载具的位置修正;
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="heroObj"></param>
        private void CallBack_PrepareBoardPos(GameObject obj, ObjectHero heroObj)
        {
            m_BoardHeroCount++;
            heroObj.SetObjectActionState(ObjectCreature.ObjectActionState.boarding);
            if (m_BoardHeroCount >= SceneObjectManager.GetInstance().GetObjectHeroCount())
            {
                m_BoardHeroCount = 0;
                SetFightState(FightState.PrepareBoardOver);
            }
        }

        /// <summary>
        /// 进入战场后初始化怒气
        /// </summary>
        private void InitFightPower()
        {
            int nSelf_value = ObjectSelf.GetInstance().GetInitPowerValue();

            int nHero_value = 0;
            for (int i = 0; i < SceneObjectManager.GetInstance().GetObjectHeroCount(); i++)
            {
                ObjectHero pHero = SceneObjectManager.GetInstance().GetHeroObject(i);
                int nCurLevel = pHero.GetHeroData().Level;
                int nInitFuryTemplateID = pHero.GetHeroRow().getEntranceFury() - 1;

//                 HerofuryTemplate pRow = (HerofuryTemplate)DataTemplate.GetInstance().m_HeroFuryTable.getTableData(nCurLevel);
//                 if (nInitFuryTemplateID >= 0 && nInitFuryTemplateID < GlobalMembers.MAX_HEROFURY_PARAM_COUNT)
//                 {
//                     nHero_value += (int)((float)pRow.getTemplate()[nInitFuryTemplateID] * (1f + pHero.GetInitPowerAddition() + pHero.GetInitPowerAdditionRate()));
//                 }
                for (int n = 0; n < pHero.GetHeroData().HeroCabalaDB.CabalaList.Count; ++n )
                {
                    int _tableID = pHero.GetHeroData().HeroCabalaDB.CabalaList[n].TableID;
                    MsTemplate _row = (MsTemplate)DataTemplate.GetInstance().m_MsTable.getTableData(_tableID);
                    if (_row.getMstype() == 1)//开场怒气额外增加
                    {
                        int nLev = pHero.GetHeroData().HeroCabalaDB.CabalaList[n].IntensifyLev;
                        if (nLev <= 0)
                            continue;
                        nHero_value += _row.getValue()[nLev - 1];
                    }
                }
                //释放一次英雄的被动技能。此逻辑只需执行一次,且需要所有英雄都已实例化完成并添加到场景对象管理器中 [7/31/2015 Zmy]
                pHero.UpdateSpellEffectValue();
            }
            //主角等级影响 + 英雄等级影响 * （1 + 初始加成怒气） [3/3/2015 Zmy]
            //int nSum = nSelf_value + nHero_value;
            m_HeroPower.OnUpdatePowerValue(nHero_value);
        }
        /// <summary>
        /// 每击杀一波敌人后，我方奖励怒气 [3/3/2015 Zmy]
        /// </summary>
        private void EachRewardPower()
        {
            // 奖励怒气 = 主角等级影响 + 英雄等级影响 + 装备符文等其他能增加英雄属性系统影响 + config中配置的全局变量 + 地图奖励 [3/3/2015 Zmy]
            //int nSelf_value = ObjectSelf.GetInstance().GetWavaPowerValue();

            int nHero_value = 0;
            for (int i = 0; i < SceneObjectManager.GetInstance().GetObjectHeroCount(); i++)
            {
                ObjectHero pHero = SceneObjectManager.GetInstance().GetHeroObject(i);
                if ( pHero == null || pHero.IsAlive() == false)
                    continue;
                
//                 int nCurLevel = pHero.GetHeroData().Level;
//                 int nInitFuryTemplateID = pHero.GetHeroRow().getWaveFury() - 1;
// 
//                 HerofuryTemplate pRow = (HerofuryTemplate)DataTemplate.GetInstance().m_HeroFuryTable.getTableData(nCurLevel);
//                 if (nInitFuryTemplateID >= 0 && nInitFuryTemplateID < GlobalMembers.MAX_HEROFURY_PARAM_COUNT)
//                 {
//                     nHero_value += pRow.getTemplate()[nInitFuryTemplateID];
//                 }
                AngertableTemplate _data = (AngertableTemplate)DataTemplate.GetInstance().m_AngerTable.getTableData(pHero.GetHeroRow().getFuryId());
                nHero_value += _data.getWaveFury();
            }

            //int nGlobalWavaFury = DataTemplate.GetInstance().m_GameConfig.getWave_fury();

            //int nSum = nSelf_value + nHero_value + nGlobalWavaFury;
            m_HeroPower.OnUpdatePowerValue(nHero_value);
        }
        IEnumerator DelayCallPos(Vector3 pos, GameObject obj, ObjectHero heroObj, HandleFinishDelayCall function)
        {
            //尼玛 这个坑比组件累死爹 属性居然还会无效。使用下面的原始距离计算可破 [3/13/2015 Zmy]
            //while (nav.remainingDistance > 0.1f)
            //    yield return new WaitForEndOfFrame();

            while (Vector3.Distance(obj.transform.position, pos) > 1.0f)
                yield return new WaitForEndOfFrame();

            if (function != null)
            {
                function(obj, heroObj);
            }
        }
        IEnumerator DelayCall(NavMeshAgent nav, GameObject obj, ObjectHero heroObj, HandleFinishDelayCall function)
        {
            //尼玛 这个坑比组件累死爹 属性居然还会无效。使用下面的原始距离计算可破 [3/13/2015 Zmy]
            //while (nav.remainingDistance > 0.1f)
            //    yield return new WaitForEndOfFrame();

            while (Vector3.Distance(obj.transform.position, nav.destination) > 0.3f)
                yield return new WaitForEndOfFrame();

            if (function != null)
            {
                function(obj, heroObj);
            }
        }
        public delegate void HandleFinishDelayCall(GameObject obj, ObjectHero heroObj);
        //测试用AI按钮
        private void OnGUI()
        {
            //if (GUI.Button(new Rect(50, 10, 100, 30), "优先攻击"))
            //    SetFightAIState(EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_ATTACK);
            //if (GUI.Button(new Rect(50, 60, 100, 30), "优先治疗"))
            //    SetFightAIState(EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_CURE);
            //if (GUI.Button(new Rect(50, 110, 100, 30), "标准模式"))
            //    SetFightAIState(EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_NORMAL);
            //if (GUI.Button(new Rect(50, 160, 100, 30), "手动模式"))
            //    SetFightAIState(EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_INVALID);
            //if (GUI.Button(new Rect(350, 10, 100, 30), "正常速度"))
            //{
            //    GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Normal);
            //}
            //if (GUI.Button(new Rect(350, 60, 100, 30), "2倍速度"))
            //{
            //    GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Double);
            //}
            //if (GUI.Button(new Rect(350, 110, 100, 30), "3倍速度"))
            //{
            //    GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Pause);
            //}

            //if (GUI.Button(new Rect(500, 10, 100, 30), "重新开始"))
            //{
            //    SceneManager.Inst.StartChangeScene(SceneEntry.Battle01_00);
            //}

        }
    }
}

