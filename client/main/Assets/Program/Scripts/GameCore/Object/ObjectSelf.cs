using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork.Data;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;

namespace DreamFaction.GameNetWork
{
    public class ObjectSelf : BaseControler
    {
        public static bool IsInit = false;  // 是否初始化

        private X_GUID m_Guid = new X_GUID ();				//guid
        private string m_Account;	        //账号
        private string m_UserID;		    //UID
        private string m_Name;	            //名字
        private short m_Level;			    //当前等级
        private int m_Exp;				    //当前经验
        private int m_ActionPoint;		    //当前活力
        private int m_ActionPointMax;       //当前等级活力
        private int m_PowerMax;             //最大怒气值
        private int m_InitPower;            //入场初始怒气
        private int m_WaveFury;             //战斗中每波怒气的奖励
        private byte m_VipLevel;	        //vip等级
        private int m_VipExp;			    // Vip经验
        private short m_BagBuyCount;        //背包购买次数               
        private byte m_HeroBuyCount;        //英雄购买次数
        private byte m_ContinuesLoginCount; //连续登陆次数
        private int m_LoginDayCount;       //登陆天数
        private byte m_bIsGod;              //0:no 1: yes 是否是GM账号
        private int m_ChargeSum;           //充值总金额
        private int m_Titime;              //体力更新时间剩余
        private int m_Battlenum;           //关卡记录
        private long m_ServerTime;          //服务器时间
        private byte m_ServerTimeZone;      //服务器时区[-12,+12] Fix要处理负数
        private int m_Hammer;               //锤子
        private int m_Week;                 //周几
        private int m_ExplorePoint = -1;         //探险消耗的“行动力”;
        private int m_ExplorePointMax = -1;      //最大行动力;
        private int m_ExplorePointRefreshTime = -1; //探险消耗行动力下次恢复的时间;
        private int m_SkillPoint = -1;      //当前技能点
        private const int m_SkillPointMax = 20;
        private int m_SkillPointRefreshTime = -1;


                                                    //////////////////////////////////////////////////////////////////////////////////////////
                                                    //货币分类：
        private long m_Money;			//金钱
        private int m_Gold;		    //水晶
        private int m_Jingji;			//竞技点 ----------没用;
        private int m_RuneMoney;       //熔炼点
        private int m_HeroMoney;       //圣灵之泉
        private int m_HeroFruit;       //生命精华---------道具;
        private int m_ExpFruit;	    //经验结晶
        private int m_HuangjinXZ;      //黄金勋章
        private int m_BaiJinXZ;        //白金勋章
        private int m_QingTongXZ;      //青铜勋章
        private int m_ChiTieXZ;        //赤铁勋章

        private int m_Friendly;     //友情点-----------没用
                                    /// //////////////////////////////////////////////////////////////////////////////////////

        private int m_FreeGoldTime;         // 免费金币剩余时间
        private int m_FreeYBtime;           // 免费元宝剩余时间
        private int m_GoldBuyNum;           // 金币购买次数
        private int m_TiBuyNum;             // 体力购买次数
        private int m_SignNum;              // 签到次数
        private int m_TodayIsSigh;          // 本日是否签到，0为未签，1为已签
        private int m_IsGetPower;           //是否已领取过体力
        private byte m_MailSize;            // 邮件数量
        private int m_bagSurplus;           //背包剩余量
        private int m_SacredAltarNum;       //祈愿次数
        private int m_SacredAltarNumMax;    //最大祈愿次数
        private int m_SacredAltarTypeNum;   //今天是否祈愿 1为祈愿 2为未祈愿
        private byte m_troopnum;             //默认编队号
        //英雄
        private HeroContainer m_HeroContainer = new HeroContainer ();    //英雄背包
        private ItemContainer m_CommonItemContainer = new ItemContainer (); //普通道具包
        private Team m_Team = new Team (); //队伍
        private List<float> heroOldExp = new List<float> ();//英雄进入战斗前经验，每次进入战斗时赋值
        private List<int> heroOldLevel = new List<int> ();  //英雄进入战斗前等级，每次进去战斗时赋值
        private float playOldExp = 0;                   //玩家进入战斗前经验，每次进入战斗时赋值
        private int playOldLevel = 0;                   //玩家进入战斗前等级，每次进入战斗时赋值
        private Relation m_Reation = new Relation (); //好友
        private ArtifactContainer m_ArtifactContainer = new ArtifactContainer ();//新的神器数据结构
        private List<int> m_HeroSkinList = new List<int> ();                          //英雄皮肤
        private List<int> m_HeroCloneList = new List<int> ();                         //玩家拥有英雄之血的克隆英雄
        private List<HeroTuJian> m_HeroHandBookList = new List<HeroTuJian> ();        //当前拥有的图鉴
        private List<int> m_HandBookBoxList = new List<int> ();                       //已领取的图鉴奖励
        private Dictionary<int, Shopbuy> m_Shop = new Dictionary<int, Shopbuy> (); //所有商城物品购买信息;
        private Dictionary<int, Monthcard> m_MonthCard = new Dictionary<int, Monthcard> (); //月卡数据信息;
        //private List<BaseStore> m_StoreList = new List<BaseStore>();//所有的商店
        private StoreContainer m_StoreContainer = new StoreContainer();

        private BattleStageMgr m_BattleStageData = new BattleStageMgr ();
        private LimitFightManager m_LimitFightMgr = new LimitFightManager ();//极限试炼
        private MailManager m_MailManager = new MailManager (); //邮件
        private ActivityOverviewMar m_ActivityOverviewMar = new ActivityOverviewMar (); //活动总览数据


        private SettingData m_SettingData = new SettingData ();//系统设置数据

        private WorldBossManager m_WorldBossMgr = new WorldBossManager ();

        //探险所有任务列表<章节id（从1开始）,该章节对应的所有任务（Explorequest表id）>;
        private Dictionary<int, stagetanxian> m_ExploreTaskDic = new Dictionary<int, stagetanxian> ();
        //探险所有小队列表<小队id（从1开始）,小队id对应的所有英雄id>;
        private Dictionary<int, teamtanxian> m_ExploreTeamDic = new Dictionary<int, teamtanxian> ();

        //当前要进入战斗的怪物组信息，每次请求进入战场时赋值 [3/25/2015 Zmy]
        private CampaignMonsterGroupData m_pBattleMonsterGroupData = new CampaignMonsterGroupData ();
        // 当前要进入的战斗场景ID，每次请求进入战场时赋值 [3/25/2015 Zmy]
        private int m_CurCampaignID = -1;
        //当前要进入的限时关卡战斗ID，每次请求进入战场时赋值[6/1/2015 Mj]
        private int m_PromptCurCampaignID = -1;
        // 当前要进入的战斗场景的章节ID，每次请求进入战场时赋值
        private int m_CurChapterID;
        // 当前进入的章节难度1,2,3
        private int m_CharpterLevel = 1;
        // 上一次打的关卡
        private int m_iCurStageID;
        //是否是限时关卡
        private bool isPrompt;
        //记录关卡的类型
        private int promptNum;
        //是否跳到极限试炼界面
        public bool isLimitWindow;

        //扫荡
        private int m_rapidClearNums; // 今日扫荡剩余次数
        private int m_rapidClearBuyTimes; // 今日扫荡剩余购买次数
        private int m_buyBattleHaveNum; // 购买关卡剩余次数

        //签到
        private int m_SignIn7;
        private int m_SignIn28;

        //活跃度
        private int m_Liveness; // 活跃度
        private int m_LivenessClaimNum; // 领取记录，个位第一个，十位第二个~~ 每位为1时表示已经领取


        private DropBoxData m_BattleDropBoxData = new DropBoxData (); //战场掉落奖励数据包
        private List<Mohe> m_MoheData = new List<Mohe> ();

        private static ObjectSelf Inst = null;
        private bool isChangeLevel = false;                      //是否是由选择关卡跳入
        private bool isHeroJoin = false;                         //是否跳入英雄招募界面
        private bool isProperty = false;                         //是否跳入属性强化界面
        private bool isExUp = false;                             //是否跳入英雄信息界面
        private bool isSkillStr = false;                         //是否跳入技能强化界面
        private bool isPromptFight = false;                    //是否跳入限时关卡界面
        private bool isPromptTime = false;                     //是否跨天
        private bool isPromptFome = false;                     //是否是在整备界面
        private bool isPromptBttleend = false;                 //是否在结算界面
        private bool isFight = false;                          //是否是战斗结束后
        private bool isOpenSealBox;                            //是否可以开启封印魔盒
        private bool isHandBook = false;                       //是否跳入图鉴界面
        public float freetime; // 免费抽奖的剩余时间（秒）
        public int firstget; // 首抽是否已经完成
        public int dreamexp; // 梦想值
        public int dreamfree;  // 梦想值改变是否免费
        public int dream; // 梦想兑换展示
        public bool isTipsDream = true;
        public bool m_isOpenZhiXian = false;//打开支线关卡
        public bool m_isOpenPerfectReward = false;//打开完美奖励
        public EM_GETMAIL_TYPE CurGetDataType = EM_GETMAIL_TYPE.GETNEW;//获得邮件刷新的类型
        public Bring_Type CurBringType = Bring_Type.HUO;//培养所选中的类型
        private bool isMysticMaxLevel = false;                       //秘术是否升级到了最大级别

        // 遗迹宝藏信息
        public int mapkey; // 第几层 从1开始
        public int mapvalue; // 第几个 从1开始
        public LinkedList<int> superlist;    //遗迹宝藏特殊list
        public int ismonthfirsthave; // 是否有月卡首刷，0没有，1有
        public int ishavefree; // 是否有免费抽奖，0有，非0则为倒计时（秒）
        public Hashtable lotteryitemmap;        // 遗迹宝藏总信息（key为层数，value为LotteryItemlayer）

        public bool isSkillShow = false;//是否技能展示
        private bool isNewMap;

        float _tempTiTime = 0.0f;
        float _tempStageTime = 0.0f;
        float _tempFreeTime = 0.0f;

        float _oneSecTime = 0f;

        public string m_NewGuidePath = string.Empty; //新手引导跳转路径 [大家在跳转完界面记得赋值为空  Lyq  7/13]
        public bool isGotoRuneUI = false;            //新手引导  是否跳转到符文界面
        public bool isGotoRelicCowry = false;        //新手引导  是否跳转到遗迹宝藏

        // 酒馆信息;
        private int normalDrawNum = 0;       //普通抽奖累计次数;
        private int normalDrawTimeSec = 0;   //普通抽奖剩余时间(秒);
        private int topDrawNum = 0;          //顶级抽奖累计次数;
        private int topDrawTimeSec = 0;      //顶级抽奖剩余时间(秒)
        private int topDrawTimes = 0;        //顶级招募累计次数(单次抽奖计数，十连抽不算在内);
        private int topDrawTenTimes = 0;     //顶级十连抽次数,首次十连抽有奖励;

        public static ObjectSelf GetInstance ()
        {
            return Inst;
        }
        protected override void InitData ()
        {
            if ( Inst == null )
            {
                Inst = this;
            }
            else
            {
                GameObject.Destroy ( this );
            }

            m_ActionPoint = m_ActionPointMax = 0;
        }

        protected override void UpdateData ()
        {
            UpdateTimeActionPoint ();
            UpdateTimeSpecialStage ();

            UpdateTimeHandler ();
            UpdateFreeTime ();

            WorldBossMgr.UpdateWorldBossData ();

            for (int i = 0; i < m_StoreContainer.GetStoreList().Count; i++)
            {
                m_StoreContainer.GetStoreList()[i].UpdateRefTime();
            }
        }
        public void SetChangeLevel ( bool value )
        {
            isChangeLevel = value;
        }
        public bool GetChangeLevel ()
        {
            return isChangeLevel;
        }

        public void SetHeroJoin ( bool value )
        {
            isHeroJoin = value;
        }

        public bool GetHeroJoin ()
        {
            return isHeroJoin;
        }

        public void SetProperty ( bool value )
        {

            isProperty = value;
        }

        public bool GetProperty ()
        {
            return isProperty;
        }

        public void SetExUp ( bool value )
        {

            isExUp = value;
        }

        public bool GetExUp ()
        {
            return isExUp;
        }

        public void SetSkillStr ( bool value )
        {

            isSkillStr = value;
        }

        public bool GetSkillStr ()
        {
            return isSkillStr;
        }

        public void SetPromptFight ( bool value )
        {
            isPromptFight = value;
        }

        public bool GetPromoptFight ()
        {
            return isPromptFight;
        }

        public void SetPromptTime ( bool value )
        {
            isPromptTime = value;
        }

        public bool GetPromptTime ()
        {
            return isPromptTime;
        }

        public void SetPromptFome ( bool value )
        {
            isPromptFome = value;
        }

        public bool GetPromptFome ()
        {
            return isPromptFome;
        }

        public void SetPromptBttleend ( bool value )
        {
            isPromptBttleend = value;
        }


        public bool GetPromptBttleend ()
        {
            return isPromptBttleend;
        }

        public void SetIsFight ( bool value )
        {
            isFight = value;
        }


        public bool GetIsFight ()
        {
            return isFight;
        }

        public void SetIsOpenSealBox ( bool value )
        {
            isOpenSealBox = value;
        }

        public bool GetIsOpenSealBox ()
        {
            return isOpenSealBox;
        }

        public void SetIsNewMap ( bool value )
        {
            isNewMap = value;
        }

        public bool GetIsNewMap ()
        {
            return isNewMap;
        }
        public List<int> GetHeroSkinList ()
        {
            return m_HeroSkinList;
        }
        public void SetHandBook(bool value)
        {
            isHandBook = value;
        }
        public bool GetHandBook()
        {
            return isHandBook;
        }

        public bool GetIsMysticMaxLevel
        {
            get
            {
                return isMysticMaxLevel;
            }
            set
            {
                isMysticMaxLevel = value;
            }
            
        }
        /// <summary>
        /// 是否拥有该时装;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsHaveSkin ( int id )
        {
            return m_HeroSkinList.Contains ( id );
        }

        public void Copy ( RoleDetail pData )
        {
            m_Guid.GUID_value = pData.roleid;
            m_Name = pData.name;
            m_bIsGod = pData.isgm;
            m_Level = pData.level;
            m_Exp = pData.exp;
            m_VipLevel = pData.viplv;
            m_VipExp = pData.vipexp;
            m_ActionPoint = pData.ti;
            m_Titime = pData.titime;
            m_Money = pData.money;
            m_Gold = pData.yuanbao;
            m_Battlenum = pData.battlenum;
            m_ServerTime = pData.servertime;
            m_ServerTimeZone = pData.timezone;
            m_Hammer = pData.hammer;
            m_FreeGoldTime = pData.freegoldtime;
            m_FreeYBtime = pData.freeybtime;
            m_GoldBuyNum = pData.goldbuynum;
            m_TiBuyNum = pData.tibuynum;
            //           m_SignNum = pData.signnum;
            m_MailSize = pData.mailsize;
            m_BagBuyCount = pData.buybagnum;
            m_HeroBuyCount = ( byte ) pData.buyherobagnum;
            m_RuneMoney = pData.ronglian;
            m_HeroFruit = pData.shenglingzq;
            m_ExpFruit = pData.jyjiejing;
            m_HuangjinXZ = pData.huangjinxz;
            m_BaiJinXZ = pData.baijinxz;
            m_QingTongXZ = pData.qingtongxz;
            m_ChiTieXZ = pData.chitiexz;
            m_HeroMoney = pData.shenglingzq;
            m_troopnum = pData.troopnum;
            m_HeroSkinList = pData.heroskins.ToList<int> ();
            m_rapidClearNums = pData.sweepnum;
            m_rapidClearBuyTimes = pData.sweepbuynum;
            m_IsGetPower = pData.mszqgetnum;
            m_SacredAltarNum = pData.qiyuannum;
            m_SacredAltarNumMax = pData.qiyuanallnum;
            m_SacredAltarTypeNum = pData.isqiyuantoday;

            m_SignIn7 = pData.signnum7;
            m_SignIn28 = pData.signnum28;

            m_ExplorePoint = pData.txti;
            m_ExplorePointRefreshTime = pData.txtitime;
            GuideManager.GetInstance ().guideIDList.Clear ();
            foreach ( var item in pData.newyindao )
            {
                GuideManager.GetInstance ().guideIDList.Add ( item );
            }
            if ( GuideManager.GetInstance ().guideIDList.Count > 0 )
            {
                List<int> idlist = GuideManager.GetInstance ().guideIDList;
                GuideManager.GetInstance ().interruptID = idlist [ idlist.Count - 1 ];
            }

            //

            //m_buyBattleHaveNum = pData.buybattlenum; 
            if ( pData.smid > 0 )// 神秘关卡数据 [4/21/2015 Zmy]
            {
                m_BattleStageData.m_IsOpenSpecialStage = true;

                m_BattleStageData.m_SpecialStage.CopyData ( pData.smid, pData.smtime, pData.smzhangjie );

                if ( pData.smshop != null )
                {
                    m_BattleStageData.LoadMysteriousShop ( pData.smshop );
                }
            }
            else
            {
                m_BattleStageData.m_IsOpenSpecialStage = false;
            }
            m_CommonItemContainer.ClearUp();
            if ( pData.baginfo.ContainsKey ( ( int ) EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON ) )
            {
                Bag pCommonInfo = pData.baginfo [ ( int ) EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON ] as Bag;
                if ( pCommonInfo != null )
                {
                    m_CommonItemContainer.InitItemList ( ( int ) EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, pCommonInfo.items );
                }
            }
            if ( pData.baginfo.ContainsKey ( ( int ) EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP ) )
            {
                Bag pEquipInfo = pData.baginfo [ ( int ) EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP ] as Bag;
                if ( pEquipInfo != null )
                {
                    m_CommonItemContainer.InitItemList ( ( int ) EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, pEquipInfo.items );
                }
            }

            m_ArtifactContainer.ClearArtifactMap();
            for ( int nType = 0; nType <= pData.artifacts.Count; nType++ )//神器 [5/26/2015 Zmy]
            {
                if ( pData.artifacts.ContainsKey ( nType ) )
                {
                    GNET.Artifact _db = pData.artifacts [ nType ] as GNET.Artifact;
                    m_ArtifactContainer.InitArtifactMap ( nType, _db );
                }
            }
            m_HeroContainer.ClearUp();
            m_HeroContainer.SetContainerSize ( pData.heroes.Count );
            foreach ( Hero heroData in pData.heroes )// 英雄背包信息 [3/31/2015 Zmy]
            {
                ObjectCard pHero = new ObjectCard ();
                pHero.GetHeroData ().Init ( heroData );
                pHero.UpdateItemEffectValue ();
                pHero.UpdateTeamEffectValue ();
                pHero.UpdateTrainEffectValue();
                m_HeroContainer.AddIHero ( pHero );
            }

            m_Team.ClearUp();
            int nCount = 0;
            X_GUID pGUID = new X_GUID ();
            foreach ( Troop item in pData.troops )// 编队信息 [3/31/2015 Zmy]
            {
                if ( nCount >= 0 && nCount < GlobalMembers.MAX_MATRIX_COUNT )
                {
                    m_Team.SetDefaultGroup ( m_troopnum );
                    m_Team.SetNumTypeDic ( item.troopnum, item.trooptype );
                    pGUID.GUID_value = item.location1;
                    m_Team.SetTeamMember ( item.troopnum, pGUID, 0 );
                    pGUID.GUID_value = item.location2;
                    m_Team.SetTeamMember ( item.troopnum, pGUID, 1 );
                    pGUID.GUID_value = item.location3;
                    m_Team.SetTeamMember ( item.troopnum, pGUID, 2 );
                    pGUID.GUID_value = item.location4;
                    m_Team.SetTeamMember ( item.troopnum, pGUID, 3 );
                    pGUID.GUID_value = item.location5;
                    m_Team.SetTeamMember ( item.troopnum, pGUID, 4 );

                    m_Team.m_GodSoulID1 = item.sh1;
                    m_Team.m_GodSoulID2 = item.sh2;
                    m_Team.m_GodSoulID3 = item.sh3;
                    m_Team.m_GodSoulID4 = item.sh4;
                    nCount++;
                }
            }
            pGUID = null;

            m_Shop.Clear ();

            foreach ( DictionaryEntry kvp in pData.shopbuys )
            {
                m_Shop.Add ( ( int ) kvp.Key, kvp.Value as Shopbuy );
            }


            UpdateExplorePointMax();
            UpdateActionPointMax ();
        }

        public StoreContainer StoreContainer
        {
            get { return m_StoreContainer; }
            set { m_StoreContainer = value; }
        }
        

        public void MoheDataClear ()
        {
            m_MoheData.Clear ();
        }


        public void MoheDataAdd ( Mohe mohe )
        {
            m_MoheData.Add ( mohe );
        }

        public List<Mohe> GetMoheData ()
        {
            return m_MoheData;
        }
        /// <summary>
        /// 设置克隆英雄数据
        /// </summary>
        /// <param name="value"></param>
        public void SetHeroCloneList ( List<int> value )
        {
            if ( value.Count <= 0 )
                return;

            foreach ( var item in value )
            {
                if ( m_HeroCloneList.Contains ( item ) )
                    continue;
                m_HeroCloneList.Add ( item );
            }
        }
        /// <summary>
        /// 获取克隆英雄的数据
        /// </summary>
        /// <returns></returns>
        public List<int> GetHeroCloneList ()
        {
            return m_HeroCloneList;
        }
        /// <summary>
        /// 是否拥有该英雄之血 用于外部查询【Lyq】
        /// </summary>
        /// <param name="heroID">英雄ID</param>
        /// <returns>是否拥有</returns>
        public bool IsGetTheHeroBlood ( int heroID )
        {
            for ( int i = 0; i < m_HeroCloneList.Count; i++ )
            {
                if ( m_HeroCloneList.Contains ( heroID ) )
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 设置英雄图鉴数据
        /// </summary>
        /// <param name="value"></param>
        public void SetHeroHandBookList ( List<HeroTuJian> value )
        {
            if ( value.Count <= 0 )
                return;

            foreach ( var item in value )
            {
                if ( m_HeroHandBookList.Contains ( item ) )
                    continue;
                m_HeroHandBookList.Add ( item );
            }
        }
        /// <summary>
        /// 获取英雄图鉴数据
        /// </summary>
        /// <returns></returns>
        public List<HeroTuJian> GetHeroHandBookList ()
        {
            return m_HeroHandBookList;
        }


        /// <summary>
        /// 设置英雄图鉴已领取的奖励数据
        /// </summary>
        /// <param name="value"></param>
        public void SetHandBookBoxList ( List<int> value )
        {
            if ( value.Count <= 0 )
                return;

            foreach ( var item in value )
            {
                if ( m_HandBookBoxList.Contains ( item ) )
                    continue;
                m_HandBookBoxList.Add ( item );
            }
        }
        /// <summary>
        /// 获得英雄图鉴已领取的奖励数据
        /// </summary>
        /// <returns></returns>
        public List<int> GetHandBookBoxList ()
        {
            return m_HandBookBoxList;
        }

        /// <summary>
        /// 获取邮件数据
        /// </summary>
        /// <returns></returns>
        public MailManager GetManager ()
        {
            return m_MailManager;
        }

        /// <summary>
        /// 获取活动整理数据
        /// </summary>
        /// <returns></returns>
        public ActivityOverviewMar GetActivityOverviewMar ()
        {
            return m_ActivityOverviewMar;
        }


        /// <summary>
        /// 获取系统设置配置数据
        /// </summary>
        /// <returns></returns>
        public SettingData GetSettingData ()
        {
            return m_SettingData;
        }


        /// <summary>
        /// 根据shopid获取物品购买次数;
        /// </summary>
        /// <returns></returns>
        public Shopbuy GetShopBuyInfoByShopId ( int shopId )
        {
            if ( m_Shop.ContainsKey ( shopId ) )
                return m_Shop [ shopId ];

            return new Shopbuy ( shopId, 0, 0 );
        }

        public void RefreshShopBuyInfo ( Shopbuy sb )
        {
            if ( sb == null )
                return;

            if ( m_Shop.ContainsKey ( sb.shopid ) )
            {
                m_Shop [ sb.shopid ] = sb;
            }
            else
            {
                m_Shop.Add ( sb.shopid, sb );
            }
        }

        // 临时初始化一些数据，以后要删除 [3/3/2015 Zmy]
        //public ObjectSelf()
        //{
        //}
        //逻辑数据部分
        public HeroContainer HeroContainerBag
        {
            get
            {
                return m_HeroContainer;
            }
        }
        public ItemContainer CommonItemContainer
        {
            get
            {
                return m_CommonItemContainer;
            }
        }

        public ArtifactContainer ArtifactContainerBag
        {
            get
            {
                return m_ArtifactContainer;
            }
        }

        public int GetItemBagSizeMax()
        {
            return CommonItemContainer.GetBagItemSizeMax();
        }

        public int GetBagSurplus ()
        {
            return CommonItemContainer.GetBagItemSizeMax () - CommonItemContainer.GetBagItemSum ();

        }
        public int GetHeroBagSurplus ()
        {
            return HeroContainerBag.GetHeroBagSizeMax () - m_HeroContainer.GetHeroList ().Count;
        }
        public Team Teams
        {
            get
            {
                return m_Team;
            }
        }
        public Relation Relations
        {
            get
            {
                return m_Reation;
            }
        }
        public BattleStageMgr BattleStageData
        {
            get
            {
                return m_BattleStageData;
            }
        }

        public LimitFightManager LimitFightMgr
        {
            get
            {
                return m_LimitFightMgr;
            }
        }

        public WorldBossManager WorldBossMgr
        {
            get
            {
                return m_WorldBossMgr;
            }
        }

        public bool IsInWorldBoss
        {
            get
            {
                if (m_WorldBossMgr != null)
                {
                    return m_WorldBossMgr.m_bStartEnter;
                }

                return false;
            }
        }

        /// <summary>
        /// 是否是极限试炼关卡;
        /// </summary>
        public bool IsLimitFight
        {
            get
            {
                return m_LimitFightMgr != null && m_LimitFightMgr.m_bStartEnter;
            }
        }

        public DropBoxData BattleDropBoxData
        {
            get
            {
                return m_BattleDropBoxData;
            }
        }
        //个人数据部分
        public X_GUID Guid
        {
            get
            {
                return m_Guid;
            }
            set
            {
                LogManager.Log ( "=============Guid================" );
                m_Guid = value;
            }
        }
        /// <summary>
        /// 是否已经领取过体力
        /// </summary>
        /// <returns></returns>
        public int IsGetPower
        {
            get
            {
                return m_IsGetPower;
            }
            set
            {
                m_IsGetPower = value;
            }
        }
        /// <summary>
        /// 当前已祈祷次数
        /// </summary>
        public int SacredAltarNum
        {
            get
            {
                return m_SacredAltarNum;
            }
            set
            {
                m_SacredAltarNum = value;
            }
        }
        /// <summary>
        /// 最大祈愿次数
        /// </summary>
        public int ScaredAltarNumMax
        {
            get
            {
                return m_SacredAltarNumMax;
            }
            set
            {
                m_SacredAltarNumMax = value;
            }
        }

        public int ScaredAltarTypeNum
        {
            get
            {
                return m_SacredAltarTypeNum;
            }
            set
            {
                m_SacredAltarTypeNum = value;
            }
        }

        /// <summary>
        /// 生命结晶
        /// </summary>
        public int HeroFruit
        {
            get
            {
                return m_HeroFruit;
            }
            set
            {
                m_HeroFruit = value;
            }
        }
        /// <summary>
        /// 经验结晶
        /// </summary>
        public int ExpFruit
        {
            get
            {
                return m_ExpFruit;
            }
            set
            {
                m_ExpFruit = value;
            }
        }

        public int HuangjinXZ
        {
            get
            {
                return m_HuangjinXZ;
            }
            set
            {
                m_HuangjinXZ = value;
            }
        }
        public int BaiJinXZ
        {
            get
            {
                return m_BaiJinXZ;
            }
            set
            {
                m_BaiJinXZ = value;
            }
        }
        public int QingTongXZ
        {
            get
            {
                return m_QingTongXZ;
            }
            set
            {
                m_QingTongXZ = value;
            }
        }
        public int ChiTieXZ
        {
            get
            {
                return m_ChiTieXZ;
            }
            set
            {
                m_ChiTieXZ = value;
            }
        }
        /// <summary>
        /// 溶灵点
        /// </summary>
        public int HeroMoney
        {
            get
            {
                return m_HeroMoney;
            }
            set
            {
                m_HeroMoney = value;
            }
        }
        public int RuneMoney
        {
            get
            {
                return m_RuneMoney;
            }
            set
            {
                m_RuneMoney = value;
            }
        }

        public string Account
        {
            get
            {
                return m_Account;
            }
            set
            {
                m_Account = value;
            }
        }
        public string UserID
        {
            get
            {
                return m_UserID;
            }
            set
            {
                m_UserID = value;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public short Level
        {
            get
            {
                return m_Level;
            }
            set
            {
                m_Level = value;
                UpdateSelf_OnLevelup ();
            }
        }

        public byte VipLevel
        {
            get
            {
                return m_VipLevel;
            }
            set
            {
                m_VipLevel = value;
                UpdateActionPointMax ();
                UpdateExplorePointMax();
            }
        }

        public int Exp
        {
            get
            {
                return m_Exp;
            }
            set
            {
                m_Exp = value;
            }
        }

        public int ActionPoint
        {
            get
            {
                return m_ActionPoint;
            }
            set
            {
                m_ActionPoint = value;
            }
        }
        public int ActionPointMax
        {
            get
            {
                return m_ActionPointMax;
            }
            set
            {
                m_ActionPointMax = value;
            }
        }

        public int ExplorePoint
        {
            get
            {
                return m_ExplorePoint;
            }
            set
            {
                m_ExplorePoint = value;
            }
        }


        public int ExplorePointMax
        {
            get
            {
                return m_ExplorePointMax;
                //return ExplorePointModule.GetMaxExplorePoint(m_VipLevel);
            }
        }

        public int ExplorePointRefreshTimes
        {
            get
            {
                return m_ExplorePointRefreshTime;
            }
            set
            {
                m_ExplorePointRefreshTime = value;
            }
        }

        public int SkillPoint
        {
            get
            {
                return m_SkillPoint;
            }
            set
            {
                m_SkillPoint = value;
            }
        }

        public int SkillPointMax
        {
            get
            {
                return m_SkillPointMax;
            }
        }

        public int SkillPointRefreshTime
        {
            get
            {
                return m_SkillPointRefreshTime;
            }
            set
            {
                m_SkillPointRefreshTime = value;
            }
        }

        public bool IsFullActionPoint
        {
            get
            {
                return m_ActionPoint >= m_ActionPointMax;
            }
        }

        public bool IsFullExplorePoint
        {
            get
            {
                return m_ExplorePoint >= m_ExplorePointMax;
            }
        }

        public long Money
        {
            get
            {
                return m_Money;
            }
            set
            {
                m_Money = value;
            }
        }

        public int Gold
        {
            get
            {
                return m_Gold;
            }
            set
            {
                m_Gold = value;
            }
        }

        public int Jingji
        {
            get
            {
                return m_Jingji;
            }
            set
            {
                m_Jingji = value;
            }
        }

        public int Friendly
        {
            get
            {
                return m_Friendly;
            }
            set
            {
                m_Friendly = value;
            }
        }

        public int VipExp
        {
            get
            {
                return m_VipExp;
            }
            set
            {
                m_VipExp = value;
            }
        }

        public short BagBuyCount
        {
            get
            {
                return m_BagBuyCount;
            }
            set
            {
                m_BagBuyCount = value;
            }
        }

        /// <summary>
        /// 英雄购买次数
        /// </summary>
        public byte HeroBuyCount
        {
            get
            {
                return m_HeroBuyCount;
            }
            set
            {
                m_HeroBuyCount = value;
            }
        }

        /// <summary>
        /// 连续登陆次数
        /// </summary>
        public byte ContinuesLoginCount
        {
            get
            {
                return m_ContinuesLoginCount;
            }
            set
            {
                m_ContinuesLoginCount = value;
            }
        }

        /// <summary>
        /// 登陆天数
        /// </summary>
        public int LoginDayCount
        {
            get
            {
                return m_LoginDayCount;
            }
            set
            {
                m_LoginDayCount = value;
            }
        }

        public byte IsGod
        {
            get
            {
                return m_bIsGod;
            }
            set
            {
                m_bIsGod = value;
            }
        }
        public int ChargeSum
        {
            get
            {
                return m_ChargeSum;
            }
            set
            {
                m_ChargeSum = value;
            }
        }
        public int TiTime
        {
            get
            {
                return m_Titime;
            }
            set
            {
                m_Titime = value;
            }
        }
        public int Battlenum
        {
            get
            {
                return m_Battlenum;
            }
            set
            {
                m_Battlenum = value;
            }
        }

        public long ServerTime
        {
            get
            {
                return m_ServerTime;
            }
            set
            {
                m_ServerTime = value;
            }
        }

        public DateTime ServerDateTime
        {
            get
            {
                DateTime dt = new DateTime ( 1970, 1, 1 );
                dt = dt.AddMilliseconds ( m_ServerTime );
                dt = dt.AddHours ( m_ServerTimeZone );
                return dt;
            }
        }

        public byte ServerTimeZone
        {
            get
            {
                return m_ServerTimeZone;
            }
            set
            {
                m_ServerTimeZone = value;
            }
        }

        public int Hammer
        {
            get
            {
                return m_Hammer;
            }
            set
            {
                m_Hammer = value;
            }
        }

        public int FreeGoldTime
        {
            get
            {
                return m_FreeGoldTime;
            }
            set
            {
                m_FreeGoldTime = value;
            }
        }

        public int FreeYBtime
        {
            get
            {
                return m_FreeYBtime;
            }
            set
            {
                m_FreeYBtime = value;
            }
        }

        public int GoldBuyNum
        {
            get
            {
                return m_GoldBuyNum;
            }
            set
            {
                m_GoldBuyNum = value;
            }
        }

        public int TiBuyNum
        {
            get
            {
                return m_TiBuyNum;
            }
            set
            {
                m_TiBuyNum = value;
            }
        }

        public int SignNum
        {
            get
            {
                return m_SignNum;
            }
            set
            {
                m_SignNum = value;
            }
        }
        public int TodayIsSign
        {
            get
            {
                return m_TodayIsSigh;
            }
            set
            {
                m_TodayIsSigh = value;
            }
        }

        public byte MailSize
        {
            get
            {
                return m_MailSize;
            }
            set
            {
                m_MailSize = value;
            }
        }

        public int RapidClearNums
        {
            get
            {
                return m_rapidClearNums;
            }
            set
            {
                m_rapidClearNums = value;
            }
        }
        public int RapidClearBuyTimes
        {
            get
            {
                return m_rapidClearBuyTimes;
            }
            set
            {
                m_rapidClearBuyTimes = value;
            }
        }

        public int SignIn7
        {
            get
            {
                return m_SignIn7;
            }
            set
            {
                m_SignIn7 = value;
            }
        }
        public int SignIn28
        {
            get
            {
                return m_SignIn28;
            }
            set
            {
                m_SignIn28 = value;
            }
        }

        public int Liveness
        {
            get
            {
                return m_Liveness;
            }
            set
            {
                m_Liveness = value;
            }
        }
        public int LivenessClaimNum
        {
            get
            {
                return m_LivenessClaimNum;
            }
            set
            {
                m_LivenessClaimNum = value;
            }
        }

        /// <summary>
        /// 刷新行动力上限;
        /// </summary>
        private void UpdateExplorePointMax()
        {
            m_ExplorePointMax = ExplorePointModule.GetMaxExplorePoint(m_VipLevel);
        }

        /// <summary>
        /// 刷新体力值上限 
        /// </summary>
        private void UpdateActionPointMax ()
        {
            int nInitialPoint = DataTemplate.GetInstance ().m_GameConfig.getInitial_ap_upper_limit ();//初始体力上限
            int vipActionPointMax = 0;//vip加成
            if ( m_VipLevel > 0 )
            {
                VipTemplate pRow = ( VipTemplate ) DataTemplate.GetInstance ().m_VipTable.getTableData ( m_VipLevel );
                vipActionPointMax = pRow.getExtraAp ();
            }
            PlayerTemplate pPlayerData = ( PlayerTemplate ) DataTemplate.GetInstance ().m_PlayerExpTable.getTableData ( m_Level );
            int nActionPointLevelUP = pPlayerData.getExtraAp (); //等级加成

            m_ActionPointMax = nInitialPoint + vipActionPointMax + nActionPointLevelUP;
        }

        /// <summary>
        /// 依据公式 获得当前本方的最大怒气值
        /// </summary>
        /// <returns></returns>
        public int GetMaxPowerValue ()
        {
            PlayerTemplate pRow = ( PlayerTemplate ) DataTemplate.GetInstance ().m_PlayerExpTable.getTableData ( m_Level );

            //公式：当前人物等级所奖励的怒气上限 + 基础最大怒气值 [3/3/2015 Zmy]
            m_PowerMax = pRow.getMaxFury () + DataTemplate.GetInstance ().m_GameConfig.getMax_rage_point ();

            return m_PowerMax;
        }
        /// <summary>
        /// 根据当前等级获取进入战斗时奖励的怒气值
        /// </summary>
        /// <returns></returns>
        public int GetInitPowerValue ()
        {
            PlayerTemplate pRow = ( PlayerTemplate ) DataTemplate.GetInstance ().m_PlayerExpTable.getTableData ( m_Level );

            m_InitPower = pRow.getEntranceFury ();

            return m_InitPower;
        }
        /// <summary>
        /// 根据当前等级获得每波战斗奖励的怒气
        /// </summary>
        /// <returns></returns>
        public int GetWavaPowerValue ()
        {
            PlayerTemplate pRow = ( PlayerTemplate ) DataTemplate.GetInstance ().m_PlayerExpTable.getTableData ( m_Level );

            m_WaveFury = pRow.getWaveFury ();

            return m_WaveFury;
        }
        /// <summary>
        /// 缓存当前请求进入战场的怪物组信息
        /// </summary>
        /// <param name="pdata">怪物组</param>
        /// <param name="nCampaignID">场景ID</param>
        public void OnCacheMonsterGroupData ( CampaignMonsterGroupData pdata, int nCampaignID )
        {
            m_pBattleMonsterGroupData.CleanUp ();
            m_pBattleMonsterGroupData.Copy ( pdata );

            if ( isPrompt )
            {
                m_PromptCurCampaignID = nCampaignID;
            }
            else
            {
                m_CurCampaignID = nCampaignID;
                isFight = true;
            }
            SetOldHeroPlayer ();
        }
        /// <summary>
        /// 缓存当前要进入战场的奖励；此调用只在请求进入战场后，服务器反馈消息中最后一部缓存必要数据
        /// </summary>
        /// <param name="_heroExp">英雄经验</param>
        /// <param name="_teamExp">队伍经验</param>
        /// <param name="_actionPoint">消耗体力</param>
        /// <param name="_dropGold">奖励金币</param>
        public void OnCacheCurrentBattleReward ( int _heroExp, int _teamExp, int _actionPoint, int _dropGold, List<int> _indroplist )
        {
            m_BattleDropBoxData.CleanUp ();
            m_BattleDropBoxData.m_HumanExp = _heroExp;
            m_BattleDropBoxData.m_TeamExp = _teamExp;
            m_BattleDropBoxData.m_DropGold = _dropGold;
            m_BattleDropBoxData.indroplist = _indroplist;
            //UpdateActionPoint(_actionPoint);
        }
        public void SetOldHeroPlayer ()
        {
            heroOldExp.Clear ();
            heroOldLevel.Clear ();
            PlayerTemplate pRow = ( PlayerTemplate ) DataTemplate.GetInstance ().m_PlayerExpTable.getTableData ( Level );
            playOldExp = ( float ) Exp / ( float ) pRow.getExp ();
            playOldLevel = Level;
            int GroupCount = Teams.GetDefaultGroup ();
            int HeroCount = Teams.m_Matrix.GetLength ( 1 );
            for ( int i = 0; i < HeroCount; i++ )
            {
                ObjectCard temp = HeroContainerBag.FindHero ( Teams.m_Matrix [ GroupCount, i ] );
                if ( temp != null )
                {

                    heroOldExp.Add ( temp.GetHeroData ().GetExpPercentage () );
                    heroOldLevel.Add ( temp.GetHeroData ().Level );
                }
            }
        }
        public List<float> HeroOldExp ()
        {
            return heroOldExp;
        }
        public List<int> HeroOldLevel ()
        {
            return heroOldLevel;
        }
        public float PlayOldExp ()
        {
            return playOldExp;
        }
        public int GetPlayOldLevel ()
        {
            return playOldLevel;
        }
        public CampaignMonsterGroupData GetMonsterGroupData ()
        {
            return m_pBattleMonsterGroupData;
        }
        public int GetCurCampaignID ()
        {
            return m_CurCampaignID;
        }
        public void SetCurCampaignID ( int value )
        {
            m_CurCampaignID = value;
        }
        public int GetPromptCurCampaignID ()
        {
            return m_PromptCurCampaignID;
        }

        public int GetCurChapterID ()
        {
            return m_CurChapterID;
        }
        public void SetCurChapterID ( int iChapterID )
        {
            LogManager.LogToFile ( "... SetCurChapterID : " + iChapterID );
            if ( m_CurChapterID != iChapterID )
            {
                m_CurChapterID = iChapterID;
                CurChapterLevel = 1;
            }
        }
        public bool GetIsPrompt ()
        {
            return isPrompt;
        }
        public void SetIsPrompt ( bool isPrompt )
        {
            this.isPrompt = isPrompt;
        }
        public int GetPromptNum ()
        {
            return promptNum;
        }
        public void SetPromptNum ( int promptNum )
        {
            this.promptNum = promptNum;
        }

        public int Week
        {
            get
            {
                return m_Week;
            }
            set
            {
                m_Week = value;
            }
        }

        public int GetWeek ()
        {
            string str = ServerDateTime.DayOfWeek.ToString ();
            // string str =  dt.ToString();
            // Debug.Log("今天是"+str);
            switch ( str )
            {
                case "Monday":
                    return 1;

                case "Tuesday":
                    return 2;

                case "Wednesday":
                    return 1;
                    ;

                case "Thursday":
                    return 2;

                case "Friday":
                    return 1;

                case "Saturday":
                    return 2;

                case "Sunday":
                    return 3;

                default:
                    break;
            }
            return -1;
        }


        public int CurChapterLevel
        {
            set
            {
                m_CharpterLevel = value;
            }
            get
            {
                return m_CharpterLevel;
            }
        }
        public int CurStageID
        {
            set
            {
                m_iCurStageID = value;
            }
            get
            {
                return m_iCurStageID;
            }
        }



        /// <summary>
        /// 体力值更新
        /// </summary>
        /// <param name="_value">需要更新的数值，有正负</param>
        public void UpdateActionPoint ( int _value )
        {
            m_ActionPoint -= _value;
            if ( m_ActionPoint < 0 )
            {
                m_ActionPoint = 0;
            }
            GameEventDispatcher.Inst.dispatchEvent ( GameEventID.G_ActionPoint_Update );
        }
        /// <summary>
        /// 更新主角经验值，算出等级和当前经验
        /// </summary>
        /// <param name="nExp"></param>
        public void UpdateHumanExp ( int nExp )
        {
            m_Exp = nExp;
            UpdateActionPointMax ();
            UpdateExplorePointMax();
            GameEventDispatcher.Inst.dispatchEvent ( GameEventID.G_HumanExp_Update );
        }
        /// <summary>
        /// 更新阵型小队里的Hero数据 .以后形参的英雄对象要换成英雄卡牌的数据对象
        /// </summary>
        /// <param name="nExp"></param>
        public void UpdateTeamData ( int nExp, int nExpNum, ref ObjectCard pHero )
        {
            HeroexpTemplate pRow = ( HeroexpTemplate ) DataTemplate.GetInstance ().m_HeroExpTable.getTableData ( pHero.GetHeroData ().Level );
            if ( nExpNum < 0 || nExpNum >= pRow.getExp ().Length )
            {
                LogManager.LogError ( "!!!!!!index out range" );
                return;
            }
            int plusExp = pRow.getExp () [ nExpNum - 1 ] - pHero.GetHeroData ().Exp;
            int nSumExp = nExp - plusExp;

            if ( nSumExp >= 0 )
            {
                pHero.GetHeroData ().Level++;
                pHero.GetHeroData ().Exp = 0;
                UpdateTeamData ( nSumExp, nExpNum, ref pHero );
            }
            else
            {
                pHero.GetHeroData ().Exp = nExp;
            }
        }
        /// <summary>
        /// 因为战斗奖励在进入战场时已经下发并存储，所以战斗胜利 后直接刷新数据。
        /// </summary>
        public void UpdateDataBattleWin ()
        {
            if ( m_BattleDropBoxData.m_HumanExp > 0 )
            {
                // UpdateHumanExp(m_BattleDropBoxData.m_HumanExp);
            }
            if ( m_BattleDropBoxData.m_TeamExp > 0 )
            {
                for ( int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i )
                {
                    X_GUID guid = m_Team.m_Matrix [ m_Team.m_DefaultGroup, i ];
                    ObjectCard pHeroData = m_HeroContainer.FindHero ( guid );
                    if ( pHeroData != null )
                    {
                        HeroTemplate pData = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( pHeroData.GetHeroData ().TableID );
                        UpdateTeamData ( m_BattleDropBoxData.m_TeamExp, pData.getExpNum (), ref pHeroData );
                    }
                }
            }
            if ( m_BattleDropBoxData.m_DropGold > 0 )
            {
                m_Money += m_BattleDropBoxData.m_DropGold;
            }
        }

        private void UpdateTimeHandler ()
        {
            _oneSecTime += Time.deltaTime;

            if ( _oneSecTime >= 1f )
            {
                OneSecTimeHandler ( _oneSecTime );
                _oneSecTime = 0f;
            }
        }

        /// <summary>
        /// 所有以1秒为基础增量的处理函数，省的每个人单独处理时间逻辑;
        /// </summary>
        /// <param name="deltaTime"></param>
        private void OneSecTimeHandler ( float deltaTime )
        {
            //假的同步服务器计时;
            AddServerTime ( deltaTime );
            //行动力更新;
            UpdateExplorePointTime ();
            //技能点更新
            UpdateSkillPointTime();
            //酒馆抽奖计时;
            UpdateJiuGuanTime(deltaTime);
        }

        private void UpdateExplorePointTime ()
        {
            if ( m_ExplorePoint == -1 || m_ExplorePointRefreshTime == -1 )
            {
                return;
            }

            if ( m_ExplorePoint < ExplorePointMax )
            {
                if ( m_ExplorePointRefreshTime > 0 )
                {
                    m_ExplorePointRefreshTime--;
                }
                else
                {
                    m_ExplorePoint++;
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_ExplorePoint_Update);
                    m_ExplorePointRefreshTime = DataTemplate.GetInstance().m_GameConfig.getPer_ep_recovery_sec();
                }
            }
        }

        private void UpdateSkillPointTime()
        {
            if (m_SkillPoint == -1 || m_SkillPointRefreshTime == -1)
            {
                return;
            }

            if (m_SkillPoint < m_SkillPointMax)
            {
                if (m_SkillPointRefreshTime > 0)
                {
                    m_SkillPointRefreshTime--;
                }
                else
                {
                    m_SkillPoint++;
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_SkillPoint_Update);
                    VipTemplate vipT = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(m_VipLevel);
                    m_SkillPointRefreshTime = vipT.getReSkillTime();
                }
            }
        }

        /// <summary>
        /// 服务器时间客户端假同步;
        /// </summary>
        /// <param name="deltaTime"></param>
        private void AddServerTime ( float deltaTime )
        {
            m_ServerTime += ( int ) ( deltaTime * 1000 );
        }

        //体力倒计时刷新 [4/21/2015 Zmy]
        private void UpdateTimeActionPoint ()
        {
            if ( m_ActionPoint == 0 || m_ActionPointMax == 0 )
                return;

            if ( m_ActionPoint < m_ActionPointMax )
            {
                if ( m_Titime > 0 )
                {
                    _tempTiTime += Time.deltaTime;
                    if ( _tempTiTime > 1f )
                    {
                        m_Titime--;
                        _tempTiTime = 0;

                    }
                }
                else
                {
                    UpdateActionPoint ( -1 );
                    m_Titime = DataTemplate.GetInstance ().m_GameConfig.getPer_ap_recovery_sec ();
                }
            }
        }
        // 特殊关卡或者神秘商店倒计时刷新 [4/21/2015 Zmy]
        private void UpdateTimeSpecialStage ()
        {
            if ( m_BattleStageData.m_IsOpenSpecialStage == false )
                return;

            if ( m_BattleStageData.m_SpecialStage.m_Time > 0 )
            {
                _tempStageTime += Time.deltaTime;
                if ( _tempStageTime > 1f )
                {
                    m_BattleStageData.m_SpecialStage.m_Time--;
                    _tempStageTime = 0;
                }
            }
            else
            {
                m_BattleStageData.m_IsOpenSpecialStage = false;
                m_BattleStageData.m_SpecialStage.ClearUp ();
            }
        }

        // 更新招募倒计时
        private void UpdateFreeTime ()
        {
            if ( freetime > 0 )
            {
                _tempFreeTime += Time.deltaTime;
                if ( _tempFreeTime > 1.0f )
                {
                    _tempFreeTime -= 1;
                    freetime -= 1.0f;
                }

            }
        }


        public bool TryGetResourceCountById ( EM_RESOURCE_TYPE resourceType, ref long count )
        {
            return TryGetResourceCountById ( ( int ) resourceType, ref count );
        }

        /// <summary>
        /// 根据资源id获取人物资源数量;
        /// 魔钻	1400 000001
        /// 金币	1400 000002
        /// 圣灵之泉	1400 000003
        /// 熔炼点	1400000004
        /// 黄金勋章	1400000005
        /// 白银勋章	1400000006
        /// 青铜勋章	1400000007
        /// 赤铁勋章	1400000008
        /// 经验结晶	1400000009
        /// 技能点 1400000012
        /// </summary>
        /// <returns></returns>
        public bool TryGetResourceCountById ( int id, ref long count )
        {

            switch ( id )
            {
                case 1400000001:
                    count = Gold;
                    return true;
                case 1400000002:
                    count = Money;
                    return true;
                case 1400000003:
                    count = HeroMoney;
                    return true;
                case 1400000004:
                    count = RuneMoney;
                    return true;
                case 1400000005:
                    count = HuangjinXZ;
                    return true;
                case 1400000006:
                    count = BaiJinXZ;
                    return true;
                case 1400000007:
                    count = QingTongXZ;
                    return true;
                case 1400000008:
                    count = ChiTieXZ;
                    return true;
                case 1400000009:
                    count = ExpFruit;
                    return true;
                case 1400000012:
                    count = SkillPoint;
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 根据道具id,获得物品数量，不是物品占用的背包格子数;
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool TryGetItemCountById ( EM_BAG_HASHTABLE_TYPE bagType, int id, ref int count )
        {
            List<BaseItem> baseItemList = ObjectSelf.GetInstance ().CommonItemContainer.GetItemList ( bagType );

            if ( baseItemList == null || baseItemList.Count == 0 )
                return false;

            count = 0;
            int baseItemCount = baseItemList.Count;
            for ( int i = 0; i < baseItemCount; ++i )
            {
                int baseItemId = baseItemList [ i ].GetItemTableID ();
                if ( baseItemId == id )
                {
                    int tempNum = baseItemList [ i ].GetItemCount ();
                    count += tempNum;
                }
            }

            return true;
        }

        //等级刷新需要更新的逻辑 [5/27/2015 Zmy]
        private void UpdateSelf_OnLevelup ()
        {
            GameEventDispatcher.Inst.dispatchEvent ( GameEventID.G_HumanLevel_Update );

            if ( ArtifactContainerBag.UpdateArtifactActivateState () == true )//等级改变，刷新一下神器激活状态.如果存在新激活的神器。刷新所有英雄属性
            {
                for ( int i = 0; i < HeroContainerBag.GetHeroList ().Count; i++ )
                {
                    ObjectCard _data = HeroContainerBag.GetHeroDataByIndex ( i );
                    if ( _data != null )
                    {
                        _data.UpdateTeamEffectValue ();
                    }
                }
            }
        }


        #region 月卡信息
        public void ClearMonthData ()
        {
            m_MonthCard.Clear ();
        }

        public void SetMonthCardData ( LinkedList<Monthcard> data )
        {
            ClearMonthData ();

            foreach ( Monthcard card in data )
            {
                if ( m_MonthCard.ContainsKey ( card.monthcardid ) )
                {
                    LogManager.LogError ( "月卡数据有问题，重复的月卡id=" + card.monthcardid );
                    continue;
                }

                m_MonthCard.Add ( card.monthcardid, card );
            }
        }

        public Monthcard GetMontCardInfoById ( int id )
        {
            if ( m_MonthCard != null && m_MonthCard.ContainsKey ( id ) )
            {
                return m_MonthCard [ id ];
            }

            return null;
        }

        #endregion

        #region 探险任务
        public void SetExploreData ( stagetxall data )
        {
            ClearExploreData ();

            foreach ( DictionaryEntry de in data.teamallmap )
            {
                m_ExploreTeamDic.Add ( ( int ) de.Key, de.Value as teamtanxian );
            }

            foreach ( DictionaryEntry task in data.stagetxallmap )
            {
                m_ExploreTaskDic.Add ( ( int ) task.Key, task.Value as stagetanxian );
            }

            //调试用的;
            //foreach (KeyValuePair<int, teamtanxian> de in m_ExploreTeamDic)
            //{
            //    teamtanxian stx = de.Value as teamtanxian;
            //    LinkedList<int>.Enumerator ie = stx.team.GetEnumerator();
            //    while (ie.MoveNext())
            //    {
            //        Debug.Log(ie.Current);
            //    }
            //}

            //foreach (KeyValuePair<int, stagetanxian> de in m_ExploreTaskDic)
            //{
            //    stagetanxian stx = de.Value as stagetanxian;
            //    LinkedList<tanxianinit>.Enumerator ie = stx.stagetx.GetEnumerator();
            //    while (ie.MoveNext())
            //    {
            //        //Debug.Log(ie.Current.tanxianid + "" + ie.Current.teamnum);
            //    }
            //}

        }

        /// <summary>
        /// 获取探险小队数据;
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, teamtanxian> GetExploreTeamData ()
        {
            return m_ExploreTeamDic;
        }

        /// <summary>
        /// 根据小队id获得当前的英雄列表;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<int> GetHeroListById ( int id )
        {
            if ( m_ExploreTeamDic.ContainsKey ( id ) )
            {
                return m_ExploreTeamDic [ id ].team.ToList<int> ();
            }

            return null;
        }

        /// <summary>
        /// 根据探险任务id获得当前的出战英雄列表;
        /// </summary>
        /// <param name="exploreid"></param>
        /// <returns></returns>
        public List<X_GUID> GetHeroListByExploreId ( int exploreid )
        {
            foreach ( KeyValuePair<int, teamtanxian> item in m_ExploreTeamDic )
            {
                teamtanxian tt = item.Value as teamtanxian;
                if ( tt.tanxianid != exploreid )
                {
                    continue;
                }

                List<X_GUID> res = new List<X_GUID> ();

                foreach ( int k in tt.team )
                {
                    X_GUID guid = new X_GUID ();
                    guid.GUID_value = k;

                    res.Add ( guid );
                }

                return res;
            }

            return null;
        }

        /// <summary>
        /// 判断某个ObjectCard是否在探险任务中;
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool IsInExploring ( X_GUID guid )
        {
            foreach ( KeyValuePair<int, teamtanxian> kvp in m_ExploreTeamDic )
            {
                teamtanxian tx = kvp.Value as teamtanxian;
                if ( tx == null )
                {
                    continue;
                }

                foreach ( int id in tx.team )
                {
                    if ( id == ( int ) ( guid.GUID_value ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 获取探险任务数据;
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, stagetanxian> GetAllExploreTaskData ()
        {
            return m_ExploreTaskDic;
        }

        /// <summary>
        /// 获取指定章节对应的任务数据;
        /// </summary>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        public List<tanxianinit> GetExploreTaskDataByChapterId ( int chapterId )
        {
            if ( m_ExploreTaskDic.ContainsKey ( chapterId ) )
            {
                return m_ExploreTaskDic [ chapterId ].stagetx.ToList ();
            }

            return null;
        }

        /// <summary>
        /// 根据章节id,探险id获取当前探险的任务数据;
        /// </summary>
        /// <param name="chapterId"></param>
        /// <param name="exploreId"></param>
        /// <returns></returns>
        public tanxianinit GetExploreTaskData ( int chapterId, int exploreId )
        {
            List<tanxianinit> items = GetExploreTaskDataByChapterId ( chapterId );

            if ( items != null )
            {
                foreach ( tanxianinit init in items )
                {
                    if ( init.tanxianid == exploreId )
                    {
                        return init;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 根据任务id获取当前的任务数据，一个任务id不可能出现在多个章节任务中;
        /// </summary>
        /// <param name="exploreId"></param>
        /// <returns></returns>
        public tanxianinit GetExploreTaskDataById ( int exploreId )
        {
            foreach ( KeyValuePair<int, stagetanxian> de in m_ExploreTaskDic )
            {
                stagetanxian stx = de.Value as stagetanxian;
                LinkedList<tanxianinit>.Enumerator ie = stx.stagetx.GetEnumerator ();
                while ( ie.MoveNext () )
                {
                    if ( ie.Current.tanxianid == exploreId )
                    {
                        return ie.Current;
                    }
                }
            }

            return null;
        }

        void ClearExploreData ()
        {
            m_ExploreTaskDic.Clear ();
            m_ExploreTeamDic.Clear ();
        }
        #endregion

#region 酒馆--原招募;

        private void UpdateJiuGuanTime(float deltaTime)
        {
            if (this.normalDrawTimeSec > 0)
            {
                this.normalDrawTimeSec--;
            }

            if (this.topDrawTimeSec > 0)
            {
                this.topDrawTimeSec--;
            }
        }

        /// <summary>
        /// normalDrawNum = 0;       //普通抽奖累计次数;
        /// normalDrawTimeSec = 0;   //普通抽奖剩余时间(秒);
        /// topDrawNum = 0;          //顶级抽奖累计次数;
        /// topDrawTimeSec = 0;      //顶级抽奖剩余时间(秒)
        /// topDrawTimes = 0;        //顶级招募累计次数(单次抽奖计数，十连抽不算在内);
        /// topDrawTenTimes = 0;     //顶级十连抽次数,首次十连抽有奖励;
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="?"></param>
        public void SetJiuGuanData(Lotty data)
        {
            this.normalDrawNum = data.normalrecruitnum;
            this.normalDrawTimeSec = data.normalrecruittime;   //普通抽奖剩余时间(秒);
            this.topDrawNum = data.toprecruitnum;              //顶级抽奖累计次数;
            this.topDrawTimeSec = data.toprecruittime;         //顶级抽奖剩余时间(秒)
            this.topDrawTimes = data.toprecruitheronum;        //顶级招募累计次数(单次抽奖计数，十连抽不算在内);
            this.topDrawTenTimes = data.toptentime;            //顶级十连抽次数,首次十连抽有奖励;
        }

        /// <summary>
        /// 是否已经进行过钻石十连抽;
        /// </summary>
        /// <returns></returns>
        public bool IsFirstTopTenDrawDone()
        {
            return this.topDrawTenTimes > 0;
        }

        /// <summary>
        /// 获得距离免费抽奖时间;
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetDrawTimeSecToFree(LOTTERY_TYPE type)
        {
            switch (type)
            {
                case LOTTERY_TYPE.Nor_One:
                    return this.normalDrawTimeSec;
                case LOTTERY_TYPE.Top_One:
                    return this.topDrawTimeSec;
                case LOTTERY_TYPE.Nor_Ten:
                case LOTTERY_TYPE.Top_Ten:
                    break;
                default:
                    break;
            }

            return -1;
        }

        /// <summary>
        /// 获得已经抽奖次数;
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetDrawTimes(LOTTERY_TYPE type)
        {
            switch (type)
            {
                case LOTTERY_TYPE.Nor_One:
                    return this.normalDrawNum;
                case LOTTERY_TYPE.Top_One:
                    return this.topDrawNum;
                case LOTTERY_TYPE.Nor_Ten:
                    break;
                case LOTTERY_TYPE.Top_Ten:
                    return this.topDrawTenTimes;
                default:
                    break;
            }

            return -1;
        }
#endregion
    }
}