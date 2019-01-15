////////////////////////////////////////////////////////////////////////////////
//  
// @module 事件系统
// @author 金奇
////////////////////////////////////////////////////////////////////////////////
/// <namespace>
/// <summary>事件系统</summary>
/// <remarks>我自定义的事件系统，仿照AS3写的！</remarks>
/// </namespace>
namespace DreamFaction.GameEventSystem
{
    /// <summary>
    /// 游戏事件系统所有的事件ID定义都在这个Struct里<br/> 
    /// DreamFaction.NetWork 网络数据事件    ID区间 100000 - 199999  "Net_"开头表示 NetEvent<br/> 
    /// DreamFaction.UI UI模块触发的事件     ID区间 200000 - 299999  "U_"开头表示 UserInputEvent<br/> 
    /// DreamFaction.GameCore 游戏内触发事件 ID区间 300000 - 399999  "G_"开头表示 GameEvent<br/> 
    /// DreamFaction.GameSceneEditor 关卡编辑器触发事件 ID区间 400000 - 499999 "SE_"开头表示 SceneEditor<br/> 
    /// 注意：事件ID在UI的LUA中要从新定义并使用！为了避免错误，这里都要显示的写出ID值！<br/> 
    /// </summary>
    public enum GameEventID
    {
        //--------------------------------------
        // DreamFaction.NetWork 网络数据事件，事件ID在UI中要从新定义为了避免错误，这里都要显示的写出ID值
        // ID区间 100000 - 199999
        // "N_"开头表示 NetEvent
        //--------------------------------------
        #region WWW连接web账号服务器，验证账号信息相关的回调事件！ 起始ID：100000
        /// <summary>
        /// 事件：账号注册成功
        /// </summary>
        Net_RegistOK                = 100000,
        /// <summary>
        /// 事件：账号注册失败，失败原因在事件参数中!
        /// </summary>
        Net_RegistUnOK              = 100001,
        /// <summary>
        /// 事件：连接账号注册服务器地址失败！失败原因在事件参数中！
        /// </summary>
        Net_RegistError             = 100002,
        /// <summary>
        /// 事件：登陆账号服务器成功！
        /// </summary>
        Net_LoginOK                 = 100003,
        /// <summary>
        /// 事件：登陆账号服务器失败，失败原因在事件参数中!
        /// </summary>
        Net_LoginUnOK               = 100004,
        /// <summary>
        /// 事件：连接账号登陆服务器地址失败！失败原因在事件参数中！
        /// </summary>
        Net_LoginError              = 100005,
        /// <summary>
        /// 事件：请求设备ID账号信息出现错误！ <br/>
        /// 1 代表已绑定账户 0代表没有绑定过账户 -1代表没有注册过 -2代表数据异常 -3代表机器码是空
        /// </summary>
        Net_RequestError            = 100006,
        /// <summary>
        /// 事件：请求设备ID账号信息OK！ <br/>
        /// 1 代表已绑定账户 0代表没有绑定过账户 -1代表没有注册过 -2代表数据异常 -3代表机器码是空
        /// </summary>
        Net_RequestOK               = 100007,
        #endregion

        #region 网络连接相关的回调事件！起始ID：100010
        /// <summary>
        /// 连接游戏服务器OK
        /// </summary>
        Net_ConnectGameServerOK     = 100010,
        /// <summary>
        /// 网络连接出错，断开连接！
        /// </summary>
        Net_ConnectGameServerUnOK   = 100011,
        /// <summary>
        /// 连接游戏服务器时出错！
        /// </summary>
        Net_ConnectGameServerError  = 100012,
        /// <summary>
        /// 登陆到游戏服务器成功！
        /// </summary>
        Net_LoginGameServerOK       = 100013,
        /// <summary>
        /// 登陆到游戏服务器失败！事件参数带有具体的失败原因！
        /// </summary>
        Net_LoginGameServerUnOK     = 100014,
        #endregion

        #region 玩家属性，数据发生变化的通知事件 MCHumanDetailAttribute 起始ID：100020
        /// <summary>
        /// 玩家“姓名”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_Name = 100020,
        /// <summary>
        /// 玩家“等级”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_Level = 100021,
        /// <summary>
        /// 玩家“经验”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_Exp  = 100022,
        /// <summary>
        /// 玩家“行动力/体力”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_ActionPoint  = 100023,
        /// <summary>
        /// 玩家“金币”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_Money        = 100024,
        /// <summary>
        /// 玩家“充值币”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_Gold         = 100025,
        /// <summary>
        /// 玩家“VIP等级”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_VipLevel     = 100026,
        /// <summary>
        /// 玩家“VIP经验”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_VipExp       = 100027,
        /// <summary>
        /// 人物信息获取完毕准备进入游戏
        /// </summary>
        Net_MCHumanDetailAttribute_OK           = 100028,
        /// <summary>
        /// 玩家“熔炼点”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_RuneMoney    = 100029,
        /// <summary>
        /// 玩家“熔灵点”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_HeroMoney    = 100030,
        /// <summary>
        /// 玩家“经验结晶”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_ExpFruit     = 100031,
        /// <summary>
        /// 玩家“生命结晶”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_HeroFruit    = 100032,
        /// <summary>
        /// 玩家“背包购买次数”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_BagBuyCount  = 100033,
        /// <summary>
        /// 玩家“英雄购买次数”发生变化
        /// </summary>
        Net_MCHumanDetailAttribute_HeroBuyCount = 100034,
        
        #endregion

        #region 创建人物时可选的角色列表 MCNotifySelectRolePacket 起始ID：100050
        Net_MCNotifySelectRolePacket_SourceName = 100050, 
        #endregion

        #region 英雄 相关消息ID MCNewHeroListPacket MCHeroDetailAttributePacket MCHeroDeletePacket起始ID：100080
        /// <summary>
        /// 英雄列表初始化数据OK
        /// </summary>
        Net_MCNewHeroListPacket_OK              = 100080,
        #endregion

        #region 物品/道具 相关消息ID MCNewItemListPacket MCItemInfoPacket MCItemDeletePacket 起始ID：100280
        /// <summary>
        /// 物品列表初始化数据OK
        /// </summary>
        Net_MCNewItemListPacket_OK              = 100280,
        #endregion

        #region 阵型/阵法 相关消息ID MCTeamInfoPacket MCSetTeamObjectRetPacket 起始ID：100480
        /// <summary>
        /// 阵型/阵法 初始化数据OK
        /// </summary>
        Net_MCTeamInfoPacket_OK             = 100480,
        /// <summary>
        /// 阵型设置OK
        /// </summary>
        Net_MCSetTeamObjectRetPacket_OK     = 100481, 
        #endregion

        #region 战斗场景 相关消息ID MCDetailCampaignPacket MCCampaignAwardPacket 起始ID：100680
        /// <summary>
        /// 战斗场景 初始化数据OK
        /// </summary>
        Net_MCDetailCampaignPacket_OK       = 100680, 
        #endregion

        #region 服务器消息通知 起始ID：101000
        /// <summary>
        /// 战斗场景 初始化数据OK
        /// </summary>
        Net_MCGameResultPacket = 101000, 
        #endregion

        // ======= java服务器新定义的网络消息 ============
        /// <summary>
        /// 正常的提示信息
        /// </summary>
        U_NetTips = 101003,
        /// <summary>
        /// 重新激活登陆按钮
        /// </summary>
        U_ReActive = 10104,
        /// <summary>
        /// 服务器消息
        /// </summary>
        U_MsgNotify = 10105,
        /// <summary>
        /// 服务器英雄数据改变;
        /// </summary>
        Net_RefreshHero = 10106,
        /// <summary>
        /// 服务器物品数据改变---KE_KnapsackUpdateShow;
        /// </summary>
        Net_RefreshItem = 10107,

        /// <summary>
        /// 通用提示
        /// </summary>
        U_GameTips = 10108,

        /// <summary>
        /// 移除英雄;
        /// </summary>
        Net_RemoveHero = 10109,

        //--------------------------------------
        // DreamFaction.UI UI模块触发的事件
        // ID区间 200000 - 299999
        // "U_"开头表示 UserInputEvent
        //--------------------------------------
        /// <summary>
        /// 用户请求登陆
        /// </summary>
        U_Login             = 200000,
        /// <summary>
        /// 切换游戏语言
        /// </summary>
        U_Localize          = 200001,
        /// <summary>
        /// 弹框文本
        /// </summary>
        U_MessageAlert      = 200002,
        /// <summary>
        /// 发送请求
        /// </summary>
        U_MessageConnecting = 200003,
        /// <summary>
        /// 收到应答
        /// </summary>
        U_MessageConnectOK  = 200004,
        /// <summary>
        /// 公用资源加载完成
        /// </summary>
        U_EternalSpriteLoaded = 200005,
        /// <summary>
        /// 切换选中的英雄
        /// </summary>
        U_HeroChangeTarget = 200006,
        /// <summary>
        /// 弹出信息
        /// </summary>
        U_MessageBox = 200007,
        /// <summary>
        /// 通过服务器选择界面，选择服务器事件
        /// </summary>
        U_SelectedServer = 200008,
        /// <summary>
        /// 隐藏UICanvas
        /// </summary>
        U_HidUICanvas = 200009,
        /// <summary>
        /// 显示UICanvas
        /// </summary>
        U_ShowUICanvas = 200010,
        /// <summary>
        /// UICanvas捕获点击操作
        /// </summary>
        U_BlockCanvasRaycasts = 200011,
        /// <summary>
        /// UICanvas不捕获点击操作
        /// </summary>
        U_UnBlockCanvasRaycasts = 200012,
        /// <summary>
        /// 打开UI
        /// </summary>
        U_OpenUI = 200013,
        /// <summary>
        /// 关闭UI
        /// </summary>
        U_CloseUI = 200014,
        /// <summary>
        /// 打开或者关闭UI，互斥操作，如果当前UI打开则关闭，如果当前UI关闭则打开
        /// </summary>
        U_OpenOrCloseUI = 200015,
        /// <summary>
        /// 商城物品刷新;
        /// </summary>
        U_RefreshShopInfo = 200016,

        /// <summary>
        /// 扫荡数据返回事件
        /// </summary>
        U_RapidClearRespond = 200017,

        /// <summary>
        /// 特殊提示框弹出事件，用于神秘商店
        /// </summary>
        UI_MysteriousShopSpecialTips = 200018,
        /// <summary>
        /// 特殊提示框弹出事件，用于特殊关卡
        /// </summary>
        UI_SpecialStageTips = 200019,
        /// <summary>
        /// 神秘商店购买回复事件
        /// </summary>
        UI_MysteriousShopBuyReplay = 200020,
        /// <summary>
        /// 体力领取事件
        /// </summary>
        UI_GetPower                = 200021,
        /// <summary>
        /// 商城广告资源下载完成回调;
        /// </summary>
        UI_ShopAdAssetDownload     = 200022,
        /// <summary>
        /// 祈愿成功后
        /// </summary>
        UI_SacredAltarSuccend      = 200023,
        /// <summary>
        /// 祈愿弹窗
        /// </summary>
        UI_SacredAltarTips         = 200024,
        /// <summary>
        /// 更新UI显示
        /// </summary>
        UI_SacredAltarUIShow       = 200025,
        /// <summary>
        /// 刷新月卡信息;
        /// </summary>
        UI_RefreshMonthCard        = 200026,
        /// <summary>
        /// 领取活力宝箱
        /// </summary>
        UI_GetLivenessBox          = 200027,
        /// <summary>
        /// 更新活力数据
        /// </summary>
        UI_GetLiveness             = 200028,
        /// <summary>
        /// 获取改名信息
        /// </summary>
        UI_ChangeName              = 200029,

        /// <summary>
        /// UI界面发生改变时
        /// </summary>
        UI_InterfaceChange          = 200030,

        /// <summary>
        /// 关卡扫荡数据改变时候;
        /// </summary>
        UI_StageSweepDataChange     = 200031,

        /// <summary>
        /// 收到服务器的跑马灯消息
        /// </summary>
        UI_ReceiveCaptionMessage = 200032,

        /// <summary>
        /// 关卡数据改变消息;
        /// </summary>
        UI_StageDataRefresh = 200033,

        /// <summary>
        /// 活动小红点的显示
        /// </summary>
        UI_ActivityPointShow = 200034,

        /// <summary>
        /// 活动刷新单个数据
        /// </summary>
        UI_ActivityRefreshSingle = 200035,

        /// <summary>
        /// 活动充值后调用
        /// </summary>
        UI_ActivityMoneyChange = 200036,

        /// <summary>
        /// 邮件的刷新
        /// </summary>
        UI_MailRefresh = 200037,

        /// <summary>
        /// 打开邮件 获取数据列表
        /// </summary>
        UI_MailReceiveListData = 200038,

        /// <summary>
        /// 获取更多邮件
        /// </summary>
        UI_MailReceiveMore = 200039,

        /// <summary>
        /// 删除邮件
        /// </summary>
        UI_MailDel = 200040,

        /// <summary>
        /// 英雄进阶成功
        /// </summary>
        UI_AdvancedSuccess = 200041,

        /// <summary>
        /// 英雄秘术升级成功
        /// </summary>
        UI_MysticSuccess = 200042,
        /// <summary>
        /// 碎片合成英雄成功
        /// </summary>
        UI_FragmentComposeHeroSuccess =200043,
        /// <summary>
        /// 关卡宝箱领取;
        /// </summary>
        UI_ChapterBoxGot = 200044,
        /// <summary>
        /// 酒馆数据改变;
        /// </summary>
        UI_JiuGuanDataUpdate = 200045,
        //--------------------------------------
        // DreamFaction.GameCore 游戏内触发事件 : 游戏内资源加载事件
        // ID区间 300000 - 300499
        // "G_"开头表示 GameEvent
        //--------------------------------------
        /// <summary>
        /// 客户端初始资源准备就绪
        /// </summary>
        G_Clent_ResOK       = 300000,
        /// <summary>
        /// 目标场景资源准备就绪 
        /// </summary>
        G_Scene_ResOK       = 300001,
        /// <summary>
        /// 场景切换完毕
        /// </summary>
        G_ChangeScene_Over  = 300002,
		/// <summary>
		///  目标场景事件完成，准备删除loading场景对象 [1/20/2015 Zmy]
		/// </summary>
        G_DestroyLoadingObj = 300003,
        /// <summary>
        /// 体力更新时间
        /// </summary>
        G_ActionPoint_Update = 300004,
        /// <summary>
        /// 关卡记录更新
        /// </summary>
        G_BattleNum_Update = 300005, 
        /// <summary>
        /// 金币更新
        /// </summary>
        G_Money_Update = 300006,
        /// <summary>
        /// 主角等级更新
        /// </summary>
        G_HumanLevel_Update = 300007,
        /// <summary>
        /// 主角经验更新
        /// </summary>
        G_HumanExp_Update = 300008,
        /// <summary>
        /// VIP等级更新
        /// </summary>
        G_VipLevel_Update = 300009,
        /// <summary>
        /// 元宝更新
        /// </summary>
        G_Gold_Update = 300010,
        /// <summary>
        /// 阵型更新
        /// </summary>
        G_Formation_Update=300011,
        /// <summary>
        /// 显示品质发生变化
        /// </summary>
        G_GameQualityChanged = 300012,
        /// <summary>
        /// 货币资源更新
        /// </summary>
        G_MoneyResource_Update = 300013,
        /// <summary>
        /// 开启激活新的神器
        /// </summary>
        G_Aritfact_Enable = 300014,

		/// <summary>
		/// 熔灵之后英雄数量更新
		/// </summary>
		G_Lith_Hero_Update = 3000015,

		/// <summary>
		/// 铸魂之后的英雄数量更新
		/// </summary>
		G_Soul_Hero_Update = 3000038,
        /// <summary>
        /// 挑战次数购买成功后
        /// </summary>
        G_FightNumSucceed  = 3000016,

        /// <summary>
        /// VIP等级提升触发该事件
        /// </summary>
        G_VipLevelUp = 300017,

        /// <summary>
        /// 探险体力事件;
        /// </summary>
        G_ExplorePoint_Update = 300018,

        /// <summary>
        /// 刷新世界BOSS数据
        /// </summary>
        G_GetWorldBoss = 300019,

		/// <summary>
        /// 探险任务数据改变;
        /// </summary>
        G_ExploreData_Update = 300020,
        /// <summary>
        /// 收到SGetMyWordBoss
        /// </summary>
        G_GetMyWorldBoss = 300021,
        /// <summary>
        /// 收到服务器购买世界BOSS商店物品返回信息
        /// </summary>
        G_SBuyBossShop = 300022,
        /// <summary>
        /// 收到服务器购买世界BOSS祝福返回信息
        /// </summary>
        G_SBuyBossBlessing = 300023,
        /// <summary>
        /// 收到服务器购买世界BOSS，守望之灵返回信息
        /// </summary>
        G_SBuyWatcherSoul = 300024,
        /// <summary>
        /// 收到服务器世界BOSS排行榜返回信息
        /// </summary>
        G_SGetBossRank = 300025,
        /// <summary>
        /// 收到SBossShop
        /// </summary>
        G_SBossShop = 300026,
        /// <summary>
        /// 组队探险队伍召回;
        /// </summary>
        G_ExploreTeamCallBack = 300027,
        /// <summary>
        /// 组队探险任务时间加速;
        /// </summary>
        G_ExploreTeamTimeUp = 300028,
        /// <summary>
        /// 组队探险领取奖励;
        /// </summary>
        G_ExploreTeamGetReward = 300029,
        /// <summary>
        /// 组队探险任务刷新任务;
        /// </summary>
        G_ExploreTeamRefreshTasks = 300030,
        /// <summary>
        /// 组队探险任务--开始探险成功;
        /// </summary>
        G_ExploreTeamBeginTasks = 300031,
        //--------------------------------------
        // DreamFaction.GameCore 游戏内触发事件 : 关于战斗的
        // ID区间 300500 - 300999
        // "G_"开头表示 GameEvent
        //--------------------------------------
        /// <summary>
        /// 英雄被伤害
        /// </summary>
        F_HeroBeHurt        = 300500,
        /// <summary>
        /// 敌方被伤害
        /// </summary>
        F_EnemyBeHurt       = 300501,
        /// <summary>
        /// 英雄死亡
        /// </summary>
        F_HeroOnDie         = 300502,
        /// <summary>
        /// 敌方死亡
        /// </summary>
        F_EnemyOnDie        = 300503,
        /// <summary>
        /// 当前战斗回合结束
        /// </summary>
        F_BattleRoundOver   = 300504,
        /// <summary>
        /// 当前场景战斗结束
        /// </summary>
        F_BattleOver        = 300505,
        /// <summary>
        /// 英雄全部阵亡，战斗失败，
        /// </summary>
        F_BattleFail        = 300506,
        /// <summary>
        /// 战斗状态改变
        /// </summary>
        F_FightStateUpdate  = 300507,
        /// <summary>
        /// 被治疗[3/9/2015 Zmy]
        /// </summary>
        F_OnBeHeal_Hero          = 300508,
        F_OnBeHeal_Monster          = 300509,
        /// <summary>
        /// 支援野怪添加血条[3/23/2015 zcd]
        /// </summary>
        F_OnSupportMonstorBlood = 300510,
        /// <summary>
        /// 显示技能flag
        /// </summary>
        SE_ShowSkillTarget = 300511,
        /// <summary>
        /// 请求释放技能
        /// </summary>
        SE_RequestReleaseSkill = 300512,
        /// <summary>
        /// 技能释放完成,重新读秒
        /// </summary>
        SE_ResetSkillCD = 300513,
        /// <summary>
        /// 战斗倒计时结束，战斗失败
        /// </summary>
        F_CountDownOver = 300514,
        /// <summary>
        /// 血量变化
        /// </summary>
        F_UI_ChangeHP = 300515,
        /// <summary>
        /// 未命中
        /// </summary>
        F_UI_Dodge = 300516,
        /// <summary>
        /// 选择集火目标
        /// </summary>
        F_Select_FireSign = 300517,
        /// <summary>
        /// buff更新，显示UI
        /// </summary>
        F_BuffEvent_ShowUI = 300518,
        /// <summary>
        /// 怒气更变
        /// </summary>
        F_Anger_Update = 300519,
        /// <summary>
        /// 技能释放显示技能名
        /// </summary>
        F_ShowSkillName = 300520,
        /// <summary>
        /// 刷新宝箱显示
        /// </summary>
        F_ShowBox       =300521,
        /// <summary>
        /// 技能点事件;
        /// </summary>
        G_SkillPoint_Update = 300522,
        /// <summary>
        /// 获取商店
        /// </summary>
        G_SGetStore = 300523,
        //--------------------------------------
        // DreamFaction.GameSceneEditor 关卡编辑器触发事件
        // ID区间 400000 - 499999
        // "SE_"开头表示 SceneEditor
        //--------------------------------------
        /// <summary>
        /// 触发战斗事件准备创建怪物(第几波怪物)
        /// </summary>
		SE_PrepareEnemy     = 400000,
        /// <summary>
        /// 触发进入战斗状态事件，人物停止移动(第几波怪物)
        /// </summary>
        SE_EnterFightState  = 400001,
        /// <summary>
        /// 触发剧情事件(第几波怪物)
        /// </summary>
        SE_StoryEnter       = 400002,
        /// <summary>
        /// 触发剧情事件结束(剧情ID Int)
        /// </summary>
        SE_StoryEnd         = 400003,
        /// <summary>
        /// 英雄瞬间移动进入
        /// </summary>
        SE_HeroPathMomentMoveEnter  = 400004,
        /// <summary>
        /// 英雄瞬间移动退出
        /// </summary>
        SE_HeroPathMomentMoveExit   = 400005,
        /// <summary>
        /// 英雄正常移动
        /// </summary>
        SE_HeroPathNormalMove       = 400006,
        /// <summary>
        /// 英雄原地待机
        /// </summary>
        SE_HeroPathIdle             = 400007,
        /// <summary>
        /// 战斗场景编辑器加载完毕
        /// </summary>
        SE_FightEditorLoadDone      = 400008,
        /// <summary>
        /// 阵型移动到战斗结束后整队点
        /// </summary>
        SE_LineUpReady              = 400009,
        /// <summary>
        /// 停止怪物出生动作
        /// </summary>
        SE_StopMonsterBirth         = 400010,
        /// <summary>
        /// 英雄瞬间移动中
        /// </summary>
        SE_HeroPathMomentMoveIng    = 400011,
        /// <summary>
        /// 战斗胜利
        /// </summary>
        SE_FightWin                 = 400012,

        /// <summary>
        /// 世界BOSS战结束
        /// </summary>
        SE_BossPass                 = 400013,
        
        /// <summary>
        /// 准备上载具;
        /// </summary>
        SE_PrepareBoard = 400021,
        /// <summary>
        /// 上载具结束--全部上载具了;
        /// </summary>
        SE_PrepareBoardOver = 400022,
        /// <summary>
        /// 载具移动中;
        /// </summary>
        SE_Boarding = 400023,
        /// <summary>
        /// 载具移动结束;
        /// </summary>
        SE_BoardingOver = 400024,
        /// <summary>
        /// 进入剧情摄像机用于开场
        /// </summary>
        SE_StoryCameraEnter = 400025,
        /// <summary>
        /// 结束剧情摄像机
        /// </summary>
        SE_StoryCameraEnd = 400026,
        //--------------------------------------
        // DreamFaction.KnapsackEvent 背包
        // ID区间 500000-599999
        // "KE_"开头表示 KnapsackEvent
        //--------------------------------------
        KE_KnapsackUpdateShow       = 500000,
        KE_KnapsackGigtShow         = 500001,
        KE_BagItemSizeShow          = 500002,
        /// <summary>
        /// 英雄背包上线添加
        /// </summary>
        KE_HeroBagItemSizeShow      = 500003,
        KE_KnapsackAdd              = 500004,
        KE_ModItemNum               = 500005,
        KE_ShowGift                 = 500006,
        //--------------------------------------
        // DreamFaction.HeroMassage 英雄信息培养
        // ID区间 600000-699999
        // "HE_"开头表示 HeroMassage
        //--------------------------------------
        HE_HeroLevelUpSucceed     =6000001,
        HE_PeiyangUp              =6000002,
        HE_BeginnerUp             =6000003,
        HE_HeroLevelUpDefeat      =6000004,
        HE_HeroBeginnerUpdateShow =6000005,
        HE_HeroCloneInject        =6000006,
        HE_HeroSkin               =6000007,//英雄换装成功
        HE_GetHeroHp              =6000008,//获得英雄之血
        /// <summary>
        /// 图鉴相关
        /// </summary>
        HB_BoxUpdate                = 7000001,//领取奖励
        HB_GetMedalPop              = 7000003,//用于获得的勋章弹窗
        HB_GetSTuJianHeros          = 7000004,//收到服务器STuJianHeros协议
        /// <summary>
        /// 魔盒
        /// </summary>
        F_SealBox                   = 8000001,
        F_IsOpenSealBox             = 8000002,//是否可以打开魔盒
        /// <summary>
        /// 极限试炼相关
        /// </summary>
        F_LimitBoutEnd              = 9000001,//回合结束
        F_LimitAddEnd               = 9000002,//购买属性加成
        F_LimitClearing             = 9000003,//结算
        F_LimitPactOk               = 9000004,//预约成功
        F_LimitFightEnd             = 9000005,//战斗结束
        F_LimitRankUpdate           = 9000006,//排行榜更新
        
        // 装备强化 and 升品
        I_EquipStrengthen           = 11000000, // 装备强化
        I_EquipLetGood              = 11000001, // 装备升品


        // 新手引导相关
        G_Guide_Stop_Type = 10000001,
        G_Guide_Fighting = 10000002,
        G_Guide_Continue = 10000003,
    }
}