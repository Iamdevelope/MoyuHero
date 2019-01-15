// **********************************************************************************************
// 这个文件中全部是游戏中用到的枚举
// **********************************************************************************************
using UnityEngine;

public static class GlobalMembers
{
	public const int INVALID_ID = -1;
    /// <summary>
    /// 无效Int值 “-1”
    /// </summary>
	public const int INVALID_VALUE = -1;
	public const int INVALID_TYPE = -1;
	public const int INVALID_UVALUE = ~0;
	public const int __BYTE_SIZE = 8;
	//网络相关
	public const int IP_SIZE = 24; //IP地址的字符最大长度
	public const int UNKNOWN_SOCKET_ERROR = ~0; //未知socket error
	public const int SOCKET_ERROR_SIZE = 256; //网络错误
	public const int SOCKET_ERROR_WOULD_BLOCK = -100; //特殊错误(可能依然是正常)
	public const int DEFAULT_SOCKETINPUT_BUFFERSIZE = 256 * 1024; //接收缓存长度
	public const int MAX_SOCKETINPUT_BUFFERSIZE = 512 * 1024; //最大接收缓存长度
	public const int DEFAULT_SOCKETOUTPUT_BUFFERSIZE = 256 * 1024; //发送缓存长度
	public const int MAX_SOCKETOUTPUT_BUFFERSIZE = 512 * 1024; //最大发送缓存长度
	public const int PACKET_HEADER_SIZE = sizeof(int)+sizeof(byte)+sizeof(int);

	//计算服务器组ID使用
	public const ulong __GROUP_ID = 0xFFE0000000000000;				//11位表示2048
	public const ulong __CLEAR_GROUP_ID = 0x001FFFFFFFFFFFFF;
	public const int __GROUP_ID_SHIFT = 53;

	//计算ServerID使用
	public const ulong __SERVER_ID = 0x001F000000000000;					//5位表示32
	public const ulong __CLEAR_SERVER_ID = 0xFFE0FFFFFFFFFFFF;
	public const int __SERVER_ID_SHIFT = 48;

	//计算类型ID使用
	public const ulong __TYPE_ID = 0x0000F00000000000;					//4位表示16
	public const ulong __CLEAR_TYPE_ID = 0xFFFF0FFFFFFFFFFF;
	public const int __TYPE_ID_SHIFT = 44;

	//最大取值
	public const ulong MAX_ULLONG_VALUE = 0xFFFFFFFFFFFFFFFF;

	public const int MAX_CHARACTER_NAME = 32 + 1;
	public const int MAX_USERID_SIZE = 64 + 1;
	public const int MAX_ACCOUNT_SIZE = 64 + 1;
	public const int MAX_SESSIONID_SIZE = 64;
	public const int MAX_DEVICENUMBER_SIZE = 64 + 1;//设备id
    public const int MAX_BYTE_BUFFER_LENGTH = 256 + 1;

    public const int MAX_MATRIX_COUNT = 3;
    public const int MAX_TEAM_CELL_COUNT = 5;	//队员个数(不包括队长)
    public const int MAX_MONSTER_GROUP_COUNT = 20; //每波怪物的最大数量
    
	//战役
	public const int MAX_FIGHT_INFO_COUNT = 360;	//战斗回合
    /// <summary>
    /// //物品列表
    /// </summary>
	public const int MAX_ITEM_LIST_COUNT = 32;	
	public const int MAX_CAMPAIGN_MONSTER_GROUP = 110;//组
	public const int MAX_MONSTERGROUP_CELL_COUNT = 30;   //最多从几个里面随机
	public const int MAX_SPELL_AI_PARAM_COUNT = 5;	//技能ai参数个数
	//英雄
	public const int MAX_HERO_LEVEL_LINE = 6;    //英雄表列数
	public const int MAX_HEROFURY_PARAM_COUNT = 20;	//怒气模板
    public const int MAX_TRAIN_SLOT_COUNT = 4;    //训练槽
	//道具符文
    /// <summary>
    /// //符文附加属性上限
    /// </summary>
	public const int MAX_RUNE_APPEND_ATTRIBUTE_COUNT = 4;
    /// <summary>
    /// //符文基础属性上限
    /// </summary>
    public const int MAX_RUNE_BASE_ATTRIBUTE_COUNT = 3;
    /// <summary>
    /// //伙伴上限
    /// </summary>
	public const int MAX_PARTNER_COUNT = 140;	
	public const int MAX_PACKET_ITEMLIST_COUNT = 128;	//协议传输物品最大大小
    /// <summary>
    /// //熔灵列表上限个数
    /// </summary>
    public const int MAX_HEROSMELT_LIST_COUNT = 15;	
    /// <summary>
    /// 神器，需要消耗的英雄种类
    /// </summary>
    public const int MAX_ARTIFACT_HERO_COUNT = 5;    
    /// <summary>
    /// //神器数量
    /// </summary>
    public const int MAX_ARTIFACT_COUNT = 16;   
    /// <summary>
    /// //神器类型
    /// </summary>
    public const int MAX_ARTIFACT_TYPE = 254;  
    /// <summary>
    /// //神器等级
    /// </summary>
    public const int MAX_ARTIFACT_LEVEL = 10;  
	//frient
	//好友
    /// <summary>
    /// //VIP玩家的最大好友数
    /// </summary>
	public const int MAX_FRIENDS_ALL_NUM = 100;		
    /// <summary>
    /// 申请好友的最大数
    /// </summary>
	public const int MAX_APPLYS_ALLNUM = 50;		
	//impact
	public const int MAX_IMPACT_ATTRIBUTE_COUNT = 3;    //impact最大3属性参数
	public const int MAX_IMPACT_LOGIC_PARAM_COUNT = 13;	//impact参数数量
	public const int MAX_DB_SPELL_NUM = 6;
    public const int MAX_DB_MYSTIC_NUM = 6;     //秘术最大数量
	public const int MAX_IMPACT_NUMBER = 16;	//对象身上最多承受impact数量
	//spell
	public const int MAX_SPELLCAST_CONSUME_COUNT = 3;	//技能消耗类型
	public const int MAX_SPELL_LOGIC_PARAM_COUNT = 50;	//技能逻辑参数个数
	public const int MAX_SPELLUP_COST_COUNT = 4;	//技能消耗
	public const int MAX_SPELL_INTERVERAL_COUNT = 10;   //10段
	public const int MAX_SPELL_COOLDOWN_NUMBER = 10;	//冷却数量
	//others
	public const int MAX_LEVEL_AMENDMENT = 100; //等级参数
	public const int MAX_RAND_TAB_ROWANDLINE = 100; //rand表行列最大数
    //copy scene
    public const int MAX_COPY_SCENE_COUNT = 4096; //副本个数
    public const int MAX_ELITE_COUNT = 128;	//精英关卡数
    public const int INT_FLAG = 32;		//int长度

    //符文特殊属性，每行显示的文字最大数目;
    public const int MAX_RUNE_COUNT_PER_LINE = 19;
    //技能展示相关
    public const int SPELL_SHOW_MONTER_ID = 100;//怪物ID
    public const int SPELL_SHPW_TEAMMATE_ID = 1403210043;//队友ID

    // 最大星级数
    public const int HeroMaxStar = 5;

    /// <summary>
    /// 攻击顺序
    /// 
    /// (老版战斗英雄顺序)---(新版)
    /// 3               3
    ///    1                1
    /// 4               5
    ///    2                2
    /// 5               4
    /// 
    /// 为了不做大的改动，所以将4,5 的对调一下即可;
    /// </summary>
    public static readonly int[,] AttackSort = new int[5, 5]
    {
        {1,2,3,5,4},    //1;
        {2,1,4,5,3},    //2;
        {1,2,3,5,4},    //3;
        {2,1,5,4,3},    //5;
        {2,1,4,5,3},    //4;
    };
}
/// <summary>
/// APP发布类型，用来控制某些功能的开始和关闭！内部版本，外部版本，测试版本！
/// </summary>
public enum SceneEntry
{
    Init,
    Logo,
    Login,  
    Loading,
    Home,
    World,
    Area,
    Fight,
	Battle01_00,
	Battle01_01,
	Battle01_02,
	Battle01_03,
	Battle01_04,
    Battle02_01,
    Battle02_02,
    Battle02_03,
    Battle03_01,
    Battle03_02,
    Battle04_01,
    Battle04_02,
    Battle05_01,
    Battle05_02,
    Null,
    SkillShow
}

public enum AppType
{
    Inside,     // 内部版本
    OutSideA,   // 对外测试版本
    OutSideR,   // 对外正式版
    Preview,    // 限时预览版 默认8小时
}

/// <summary>
///   资源类型[1/19/2015 Zmy]
/// </summary>
public enum SourceType:int
{
	
	ModelRes = 0,				//模型 [1/19/2015 Zmy]
	UI,					//UI [1/19/2015 Zmy]
	Effect,				//特效 [1/19/2015 Zmy]
	EditScene,			// 场景编辑器所需资源 [1/20/2015 Zmy]
	MaxType,
}
public enum ClientConfigs
{
    BattleMode,          //战斗模式 0自动 1手动
    VitalityRemind,      //活力回满 0关 1开
    FreeRecruit,         //免费招募 0关 1开
    WorldBoss,          //世界Boss 0关 1开
    ExplorationTask,    //探险任务完成 0关 1开
    FreeVitality,        //免费活力领取 0关 1开
    Sound,              // 音效开关
    Music,              // 音乐开关
    Version,            // 版本号
    Quality,             // 游戏画质
    WWWServer_IOS,      // IOS 的 www 服务器地址
    WWWServer_PC,       // PC 的  www 服务器地址
    WWWServer_Android,  // Android 的 www 服务器地址
    UserName,           // 玩家姓名
    State,              // 登陆状态(0:未登录 1：游客登陆 2:注册登陆)
    ServerID,           // 上次选择服务器ID
    GameLanguage,       // 游戏当前语言，不同于系统当前语言
    PlatformIP,         // 平台IP
    DreamFactionNewStages,  // 新开启的关卡数据
    ResoursePlatformIP,  //资源平台IP   
}
//行为
enum EM_TYPE_ACTION
{
    EM_TYPE_ACTION_INVALID = -1,
    EM_TYPE_ACTION_COPYSCENE,				//普通副本
    EM_TYPE_ACTION_FIGHT_BOSSSCENE,			//boss副本
    EM_TYPE_ACTION_FIGHT_WORLDBOSS,			//世界boss
    EM_TYPE_ACTION_SHOP,					//商店购物
    EM_TYPE_ACTION_ARENA_RECHALLENGE,		//竞技场挑战
    EM_TYPE_ACTION_FINDFRIEND,				//查找好友
    EM_TYPE_ACTION_FRIENDOPERATOR,				//好友操作
    EM_TYPE_ACTION_PARTNER_OPERATOR,			//伙伴操作
    EM_TYPE_ACTION_FIGHT_ELITESCENE,			//精英副本
    EM_TYPE_ACTION_PARTNER_COMPOUND,			//伙伴合成
    EM_TYPE_ACTION_RUNE_FORGE,				//符文锻造
    EM_TYPE_ACTION_RUNE_OPERATOR,				//符文操作
    EM_TYPE_ACTION_SELECT_MAINPARTNER,			//选主卡
    EM_TYPE_ACTION_TEAM,					//队伍
    EM_TYPE_ACTION_SPELL_UNLOCK,			//技能解锁
    //EM_TYPE_ACTION_OPENITEMCELL,				//开物品背包
    //EM_TYPE_ACTION_OPENPARTNERCELL,				//开伙伴背包
    //EM_TYPE_ACTION_ADD_GIFT,					//礼包获得
    //EM_TYPE_ACTION_ADD_ACHIEVEMENT,			//成就奖励
    //EM_TYPE_ACTION_REDUCE_PARTNER_IDENTIFY,		//伙伴鉴定
    //EM_TYPE_ACTION_REDUCE_RUNE_LEVELUPQUALITY,	//符文升品
    //EM_TYPE_ACTION_REDUCE_RUNE_SMITH,	    //符文洗炼
    //EM_TYPE_ACTION_REDUCE_SPELL_UNLOCK,		//技能解锁
    //EM_TYPE_ACTION_REDUCE_RUNE_FORGE,		//符文锻造
    //EM_TYPE_ACTION_REDUCE_LOTTERY,			//抽奖消耗
    //EM_TYPE_ACTION_ADD_LOTTERY,				//抽奖获得
    //EM_TYPE_ACTION_REDUCE_ARENA_BUY,			//购买竞技次数
    //EM_TYPE_ACTION_REDUCE_ARENA_FRESH,		//竞技场刷新
    //EM_TYPE_ACTION_ADD_ARENA_WIN,			//竞技场阵营获胜奖励
    //EM_TYPE_ACTION_ADD_ACTIVITIES,			//日常
    //EM_TYPE_ACTION_ADD_MAIL,					//邮件
    //EM_TYPE_ACTION_REDUCE_WORLDBOSS_CLEANCD,				//清除cd
    //EM_TYPE_CURRENCY_MODIFY_SYSTEM_SCRIPT,				//脚本赠与
    //EM_TYPE_ACTION_GIFT,							//礼包
    //EM_TYPE_ACTION_ADD_HONORBUY,					//荣誉值兑换
    //EM_TYPE_ACTION_ADD_YINDAO,						//引导
    //EM_TYPE_CURRENCY_MODIFY_RECHARGE_91,					//充值
    //EM_TYPE_CURRENCY_MODIFY_RECHARGE_PP,					//充值
    //EM_TYPE_CURRENCY_MODIFY_RECHARGE_APPLE,				//充值
    //EM_TYPE_ACTION_REDUCE_TEAMBOSS_FRESH,			//组队boss刷新
    EM_TYPE_ACTION_COUNT,
};
//货币种类
enum EM_CURRENCY_TYPE
{
    EM_CURRENCY_TYPE_INVALID = -1,
    EM_CURRENCY_GOLD = 1,				//魔钻	000001
    EM_CURRENCY_COMMON = 2,				//金币	000002
    EM_CURRENCY_HEROSMELT = 3,			    //熔灵点	000003
    EM_CURRENCY_RUNEMONEY = 4,			    //熔炼点	000004
    EM_CURRENCY_GOLDMEDAL = 5,			    //黄金勋章	000005
    EM_CURRENCY_SILVERMEDAL = 6,		        //白银勋章	000006
    EM_CURRENCY_BRONZEMEDAL = 7,		        //青铜勋章	000007
    EM_CURRENCY_IRONMEDAL = 8,				//赤铁勋章	000008
    EM_CURRENCY_EXPFRUIT = 9,				//经验结晶	000009
    EM_CURRENCY_HEROFRUIT = 10,				//生命精华	000010
    EM_CURRENCY_ACTIONPOINT = 10001,			//活力	010001
    EM_CURRENCY_JINGLI = 10002,		//精力	010002
    EM_CURRENCY_TYPE_NUMBER,
};
/// <summary>
/// 登录时，账号服务器的类型！
/// </summary>
public enum LoginServerType
{
    /// <summary>
    /// 咱们游戏自己的账号服务器！
    /// </summary>
    Server_DramFaction,
    /// <summary>
    /// 腾讯的账号服务器！
    /// </summary>
    Server_Tencent,
    /// <summary>
    /// 91的账号服务器
    /// </summary>
    Server_91,
}

/// <summary>
/// 角色详细属性数据枚举
/// </summary>
public enum EM_HUMAN_DETAIL_ATTR : int
{
    EM_HUMAN_DETAIL_ATTR_INVALID = -1,
    EM_HUMAN_DETAIL_ATTR_NAME,                			//人物的名称                            
    EM_HUMAN_DETAIL_ATTR_LEVEL,               			//当前等级              
    EM_HUMAN_DETAIL_ATTR_EXP,                 			//当前经验                      
    EM_HUMAN_DETAIL_ATTR_ACTIONPOINT,         			//当前行动力                
    EM_HUMAN_DETAIL_ATTR_MONEY,							//金钱
    EM_HUMAN_DETAIL_ATTR_GOLD,							//元宝
    EM_HUMAN_DETAIL_ATTR_RUNEMONEY,                     //熔炼点
    EM_HUMAN_DETAIL_ATTR_HEROMONEY,                     //溶灵点
    EM_HUMAN_DETAIL_ATTR_EXPFRUIT,                      //经验结晶
    EM_HUMAN_DETAIL_ATTR_HEROFRUIT,                     //生命结晶
    EM_HUMAN_DETAIL_ATTR_VIPLEVEL,						//VIP等级
    EM_HUMAN_DETAIL_ATTR_VIPEXP,						//VIP经验
    EM_HUMAN_DETAIL_ATTR_BAGBUYCOUNT,				    //背包购买次数
    EM_HUMAN_DETAIL_ATTR_HEROBUYCOUNT,					//英雄购买次数
    EM_HUMAN_DETAIL_ATTR_CONTINUESLOGINCOUNT,           //连续登陆次数
    EM_HUMAN_DETAIL_ATTR_LOGINDAYCOUNT,                 //登陆天数
    EM_HUMAN_DETAIL_ATTR_GOLDMEDAL,	        		    //黄金勋章	000005
    EM_HUMAN_DETAIL_ATTR_SILVERMEDAL,		            //白银勋章	000006
    EM_HUMAN_DETAIL_ATTR_BRONZEMEDAL,   		        //青铜勋章	000007
    EM_HUMAN_DETAIL_ATTR_IRONMEDAL,     				//赤铁勋章	000008

    EM_HUMAN_DETAIL_ATTR_NUMBER,
}

//GUID结构体

//对象类型定义
public enum EM_OBJECT_TYPE : int
{
	EM_OBJECT_TYPE_INVALID = -1,
	EM_OBJECT_TYPE_HUMAN,           //玩家主城
	EM_OBJECT_TYPE_CARD,            //卡片
	EM_OBJECT_TYPE_MONSTER,         //怪物
    EM_OBJECT_TYPE_HERO,            //战斗英雄
	EM_OBJECT_TYPE_ITEM,            //物品
	EM_OBJECT_TYPE_TEAM,            //军团
	EM_OBJECT_TYPE_NUMBER,
}

//勋章类型
public enum EM_MEDAL_TYPE : int
{
    EM_MEDAL_TYPE_INVALID = -1,
    EM_MEDAL_TYPE_GOLDMEDAL = 0,			    //黄金勋章	000005
    EM_MEDAL_TYPE_SILVERMEDAL = 1,		        //白银勋章	000006
    EM_MEDAL_TYPE_BRONZEMEDAL = 2,		        //青铜勋章	000007
    EM_MEDAL_TYPE_IRONMEDAL = 3,				//赤铁勋章	000008
    EM_MEDAL_TYPE_NUMBER,
};

//伙伴战斗状态
public enum EM_FIGHT_STATE : int
{
    EM_FIGHT_STATE_INVALID = -1,
    EM_FIGHT_STATE_VERTIGO = 1,		//1	眩晕
    EM_FIGHT_STATE_FORBID,			//2	沉默
    EM_FIGHT_STATE_NONORMAL,		//3	无法普攻
    EM_FIGHT_STATE_IDLE,			//4 定身
    EM_FIGHT_STATE_CHAOFENG,		//5 嘲讽
    EM_FIGHT_STATE_IMM,             //6 免疫
    EM_FIGHT_STATE_EGG,				//蛋的状态
	//下面为减少计算而做
	EM_FIGHT_STATE_NOCOMSUMEMP = 99,		//99 不消耗怒气
	EM_FIGHT_STATE_BEATT_EFFECT_HURT_BEF,	// 受击,前部计算
	EM_FIGHT_STATE_CAL_CALCRITICAL,			// 计算完爆击计算
	EM_FIGHT_STATE_AFTER_CAL_CALCRITICAL,	// 爆击计算后生效计算
	EM_FIGHT_STATE_BEFORE_SPELLACTIVE,		// 技能生效前
	EM_FIGHT_STATE_BEFORE_KILLTARGET,		// 杀死目标生效
	EM_FIGHT_STATE_BEFORE_DEAD,				// 死亡生效
	EM_FIGHT_STATE_IMM_IMPACT,				// 免疫buff
	EM_FIGHT_STATE_ATT_EFFECT_HURT_BEF,		// 攻击,前部计算
	EM_FIGHT_STATE_BE_EFFECT_HURT,			// 受伤害
	EM_FIGHT_STATE_AFTER_END_HURT,			// 最终后计算
	EM_FIGHT_STATE_AFTER_DESTEND_HURT,		// 目标最终后计算
	EM_FIGHT_STATE_DODGE,					// 闪避触发逻辑
	EM_FIGHT_STATE_NUMBER,
};

/// <summary>
/// 游戏内属性类型
/// </summary>
public enum EM_ATTRIBUTE :int
{
    /// <summary>
    /// 枚举定义初始值 -1
    /// </summary>
	EM_ATTRIBUTE_INVALID = -1,
    /// <summary>
    /// //当前血量
    /// </summary>
    EM_ATTRIBUTE_HP = 0,                 
    /// <summary>
    /// //血上限(生命)
    /// </summary>
	EM_ATTRIBUTE_MAXHP = 1,						
	/// <summary>
    /// //物理攻击
	/// </summary>
	EM_ATTRIBUTE_PHYSICALATTACK,				
	/// <summary>
    /// //法术攻击
	/// </summary>
	EM_ATTRIBUTE_MAGICATTACK,						
    /// <summary>
    /// //物理防御值
    /// </summary>
	EM_ATTRIBUTE_PHYSICALDEFENCE,					
    /// <summary>
    /// //法术防御值
    /// </summary>
	EM_ATTRIBUTE_MAGICDEFENCE,						
    /// <summary>
    /// //命中
    /// </summary>
	EM_ATTRIBUTE_HIT,								
    /// <summary>
    /// //闪避
    /// </summary>
	EM_ATTRIBUTE_DODGE,								
    /// <summary>
    /// //暴击
    /// </summary>
	EM_ATTRIBUTE_CRITICAL,							
    /// <summary>
    /// //韧性(抗暴)
    /// </summary>
	EM_ATTRIBUTE_TENACITY,							
    /// <summary>
    /// //移动速度
    /// </summary>
	EM_ATTRIBUTE_MOVESPEED,							
    /// <summary>
    /// //速度
    /// </summary>
	EM_ATTRIBUTE_SPEED,
	/// <summary>
	/// 命中率
	/// </summary>
	EM_ATTRIBUTE_HIT_RATE,
    /// <summary>
    /// 闪避率
    /// </summary>
	EM_ATTRIBUTE_DODGE_RATE,
    /// <summary>
    /// 暴击率
    /// </summary>
	EM_ATTRIBUTE_CRITICAL_RATE,
    /// <summary>
    /// 韧性率
    /// </summary>
    EM_ATTRIBUTE_TENACITY_RATE,
    /// <summary>
    /// 物理伤害加深率
    /// </summary>
    EM_ATTRIBUTE_PHYSICAL_HURT_ADD_PERMIL,
    /// <summary>
    /// 物理伤害减免率
    /// </summary>
    EM_ATTRIBUTE_PHYSICAL_HRUT_REDUCE_PERMIL,
    /// <summary>
    /// 法术伤害加深率
    /// </summary>
    EM_ATTRIBUTE_MAGIC_HURT_ADD_PERMIL,
    /// <summary>
    /// 法术伤害减免率
    /// </summary>
    EM_ATTRIBUTE_MAGIC_HURT_REDUCE_PERMIL,
    /// <summary>
    /// 暴击伤害增加千分比
    /// </summary>
    EM_ATTRIBUTE_CRITICAL_HURT_ADD_RATE,
    /// <summary>
    /// 暴击伤害减少千分比
    /// </summary>
    EM_ATTRIBUTE_CRITICAL_HURT_REDUCE_RATE,
    /// <summary>
    /// 额外伤害
    /// </summary>
    EM_ATTRIBUTE_EXTRA_HURT,
    /// <summary>
    /// 降低的额外伤害
    /// </summary>
    EM_ATTRIBUTE_REDUCE_HURT_POINT,
	/// <summary>
	/// 生命恢复力
	/// </summary>
    EM_ATTRIBUTE_HPRECOVER,
	/// <summary>
	/// 枚举上限值
	/// </summary>
	EM_ATTRIBUTE_NUMBER,
};

//全局唯一基础属性枚举
public enum EM_EXTEND_ATTRIBUTE :int
{
	EM_EXTEND_ATTRIBUTE_INVALID = -1,

    EM_EXTEND_ATTRIBUTE_POINT_MAXHP = 1,			            //HP上限点数
    EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP = 2,				        //HP上限千分比

    EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK = 3,		        //物理攻击点数
	EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK = 4,		        //物理攻击千分比

    EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE = 5,              //物理防御点数
	EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE = 6,             //物理防御千分比

    EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK = 7,				    //魔法攻击点数
	EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK = 8,			        //魔法攻击千分比

    EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE = 9,				    //魔法防御点数
	EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE = 10,			    //魔法防御千分比

    EM_EXTEND_ATTRIBUTE_POINT_HIT = 11,						    //命中值点数
	EM_EXTEND_ATTRIBUTE_PERMIL_HIT = 12,					    //命中值千分比

    EM_EXTEND_ATTRIBUTE_POINT_DODGE = 13,					    //闪避值点数
	EM_EXTEND_ATTRIBUTE_PERMIL_DODGE = 14,					    //闪避值千分比

    EM_EXTEND_ATTRIBUTE_POINT_CRITICAL = 15,					//暴击值点数
	EM_EXTEND_ATTRIBUTE_PERMIL_CRITICAL = 16,				    //暴击值千分比

    EM_EXTEND_ATTRIBUTE_POINT_TENACITY = 17,					//韧性值点数
    EM_EXTEND_ATTRIBUTE_PERMIL_TENACITY = 18,				    //韧性值千分比

    EM_EXTEND_ATTRIBUTE_POINT_SPEED = 19,					    //速度点
    EM_EXTEND_ATTRIBUTE_PERMIL_SPEED = 20,					    //速度千分比

    EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER = 21,                   //生命恢复点
	EM_EXTEND_ATTRIBUTE_PERMIL_HEAL = 22,					    //生命恢复千分比	

    EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE = 23,				    //命中几率千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE = 24,				    //闪避几率千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE = 25,			    //暴击几率千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE = 26,			    //韧性几率千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_ADDPHYSICALHURT = 27,			//物理伤害加成千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEPHYSICALHURT = 28,		    //物理伤害减免千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_ADDMAGICHURT = 29,			    //魔法伤害增加千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEMAGICHURT = 30,		    //魔法伤害减免千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE = 31,                 //伤害加成千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE = 32,                 //伤害减免千分比
    EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT = 33,			        //暴击伤害加成千分比
                                                                //暴击伤害减免千分比。暂未定义没有用，跳过
    EM_EXTEND_ATTRIBUTE_POINT_EXTRAHURT = 35,		            //附加伤害值
    EM_EXTEND_ATTRIBUTE_POINT_REDUCEHURT = 36,				    //绝对减伤值
    EM_EXTEND_ATTRIBUTE_PREMIL_ATTACKSUCK = 37,                 //普攻吸血千分比
    EM_EXTEND_ATTRIBUTE_PREMIL_SKILLSUCK = 38,                  //技能吸血千分比
    EM_EXTEND_ATTRIBUTE_RECUDE_SPELLCD = 39,                    //冷却缩减千分比
    EM_EXTEND_ATTRIBUTE_ADDMPINIT_PERMIL = 40,                  //初始额外怒气千分比
    EM_EXTEND_ATTRIBUTE_ADDMPATTACK_PERMIL = 41,                //攻击额外怒气千分比
    EM_EXTEND_ATTRIBUTE_ADDMPHIT_PERMIL = 42,                   //受击额外怒气千分比

    EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPA = 43,           //对生灵阵营伤害加成千分比
    EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPB = 44,           //对神族阵营伤害加成千分比
    EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPC = 45,           //对恶魔阵营伤害加成千分比   

    EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPA = 46,         //受生灵阵营伤害减免千分比
    EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPB = 47,         //受神族阵营伤害减免千分比
    EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC = 48,         //受恶魔阵营伤害减免千分比

    EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTNEAR = 49,       //对近战伤害加成千分比
    EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTFAR = 50,        //对远程伤害加成千分比

    EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTNEAR = 51,     //受近战伤害减免千分比
    EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTFAR = 52,      //受远程伤害减免千分比

    EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_BOSS = 53,            //对boss伤害加成千分比
    EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_BOSS = 54,          //对boss伤害减免千分比

    EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE = 56,                 //格挡率千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE = 57,                //破甲率千分比
    EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE = 58,                  //吸血率千分比

/// /////////////////////////////////////////////////////////////////////////以下属性暂未明确先保留
	EM_EXTEND_ATTRIBUTE_PERMIL_MP,						//获得怒气千分比
	EM_EXTEND_ATTRIBUTE_PERMIL_MONEY,					//金币获得千分比
	EM_EXTEND_ATTRIBUTE_PERMIL_EXP,					    //经验获得千分比
	EM_EXTEND_ATTRIBUTE_PERMIL_MPRECOVER,				//获得怒气回复千分比
	EM_EXTEND_ATTRIBUTE_POINT_MPNORMALATT,				//普通攻击获得的怒气点

	EM_EXTEND_ATTRIBUTE_NUMBER,             
};
//impact逻辑标记
public enum EM_IMPACT_FLAG
{
	EM_IMPACT_FLAG_INVALID = -1,
	EM_IMPACT_FLAG_ADD,
	EM_IMPACT_FLAG_REMOVE,
	EM_IMPACT_FLAG_DOT,
	EM_IMPACT_FLAG_NUMBER,
};

//逻辑,一个逻辑对应一类计算方式,如果计算方式不同则另外增加逻辑
public enum EM_IMPACT_LOGIC
{
    EM_IMPACT_LOGIC_INVALID = -1,
    EM_IMPACT_LOGIC101 = 101,
    EM_IMPACT_LOGIC102 = 102,
    EM_IMPACT_LOGIC103 = 103,
    EM_IMPACT_LOGIC104 = 104,
    EM_IMPACT_LOGIC105 = 105,
    EM_IMPACT_LOGIC106 = 106,
    EM_IMPACT_LOGIC107 = 107,
    EM_IMPACT_LOGIC108 = 108,
    EM_IMPACT_LOGIC109 = 109,
    EM_IMPACT_LOGIC110 = 110,
    EM_IMPACT_LOGIC111 = 111,
    EM_IMPACT_LOGIC112 = 112,
    EM_IMPACT_LOGIC113 = 113,
    EM_IMPACT_LOGIC114 = 114,
    EM_IMPACT_LOGIC115 = 115,
    EM_IMPACT_LOGIC116 = 116,
    EM_IMPACT_LOGIC117 = 117,
    EM_IMPACT_LOGIC118 = 118,
    EM_IMPACT_LOGIC119 = 119,
    EM_IMPACT_LOGIC1001 = 1001,
    EM_IMPACT_LOGIC1002 = 1002,
    EM_IMPACT_LOGIC1003 = 1003,
    EM_IMPACT_LOGIC1004 = 1004,
    EM_IMPACT_LOGIC1005 = 1005,
    EM_IMPACT_LOGIC1006 = 1006,
    EM_IMPACT_LOGIC1101 = 1101,
    EM_IMPACT_LOGIC1102 = 1102,
    EM_IMPACT_LOGIC1103 = 1103,
    EM_IMPACT_LOGIC1201 = 1201,
    EM_IMPACT_LOGIC1301 = 1301,
    EM_IMPACT_LOGIC1401 = 1401,
    EM_IMPACT_LOGIC1402 = 1402,
    EM_IMPACT_LOGIC2001 = 2001,

    EM_IMPACT_LOGIC2002 = 2002,
    EM_IMPACT_LOGIC2003 = 2003,
    EM_IMPACT_LOGIC2004 = 2004,
    EM_IMPACT_LOGIC2005 = 2005,
    EM_IMPACT_LOGIC2006 = 2006,
    EM_IMPACT_LOGIC2007 = 2007,
    EM_IMPACT_LOGIC2008 = 2008,
    EM_IMPACT_LOGIC2009 = 2009,
    EM_IMPACT_LOGIC2010 = 2010,
    EM_IMPACT_LOGIC2011 = 2011,
    EM_IMPACT_LOGIC2012 = 2012,
    EM_IMPACT_LOGIC2013 = 2013,
    EM_IMPACT_LOGIC2014 = 2014,
    EM_IMPACT_LOGIC2015 = 2015,
    EM_IMPACT_LOGIC2016 = 2016,
    EM_IMPACT_LOGIC2017 = 2017,
    EM_IMPACT_LOGIC2018 = 2018,
    EM_IMPACT_LOGIC2019 = 2019,
    EM_IMPACT_LOGIC2020 = 2020,
    EM_IMPACT_LOGIC2021 = 2021,
    EM_IMPACT_LOGIC2022 = 2022,
    EM_IMPACT_LOGIC2023 = 2023,
    EM_IMPACT_LOGIC2024 = 2024,
    EM_IMPACT_LOGIC2025 = 2025,
    EM_IMPACT_LOGIC2026 = 2026,
    EM_IMPACT_LOGIC2027 = 2027,
    EM_IMPACT_LOGIC2028 = 2028,
    EM_IMPACT_LOGIC2029 = 2029,
    EM_IMPACT_LOGIC2030 = 2030,
    EM_IMPACT_LOGIC2031 = 2031,
    EM_IMPACT_LOGIC2032 = 2032,
    EM_IMPACT_LOGIC2033 = 2033,
    EM_IMPACT_LOGIC2034 = 2034,
    EM_IMPACT_LOGIC2035 = 2035,
    EM_IMPACT_LOGIC2036 = 2036,
    EM_IMPACT_LOGIC2037 = 2037,
    EM_IMPACT_LOGIC2038 = 2038,
    EM_IMPACT_LOGIC2039 = 2039,
    EM_IMPACT_LOGIC2040 = 2040,
    EM_IMPACT_LOGIC2041 = 2041,
    EM_IMPACT_LOGIC2042 = 2042,
    EM_IMPACT_LOGIC2043 = 2043,
    EM_IMPACT_LOGIC2044 = 2044,
    EM_IMPACT_LOGIC2045 = 2045,
    EM_IMPACT_LOGIC2046 = 2046,
    EM_IMPACT_LOGIC2047 = 2047,
	EM_IMPACT_LOGIC_NUMBER,
};

//技能修正
public enum EM_SPELL_REFIX
{
	EM_SPELL_REFIX_INVALID = -1,
	EM_SPELL_REFIX_TIME,				//技能时间
	EM_SPELL_REFIX_SPELLCOOLDOWN,		//技能冷却时间
	EM_SPELL_REFIX_SPELLMAXDISTANCE,	//技能释放距离
	EM_SPELL_REFIX_SPELLTARGETCOUNT,	//技能目标个数
	EM_SPELL_REFIX_NUMBER,
};
//技能释放判断类型
public enum EM_SPELL_CONDITION_TYPE
{
    EM_SPELL_CONDITION_TYPE_INVALID= -1,
    EM_SPELL_CONDITION_TYPE_LESSVALUE=1,//小于等于固定值
    EM_SPELL_CONDITION_TYPE_MOREVALUE,  //大于等于固定值
    EM_SPELL_CONDITION_TYPE_LESSPERCENT,//小于等于百分比
    EM_SPELL_CONDITION_TYPE_MOREPERCENT,//大于等于百分比
    EM_SPELL_CONDITION_TYPE_NUMBER,
};
//技能打断类型
public enum EM_SPELL_BREAK_TYPE
{
    EM_SPELL_BREAK_TYPE_NOBREAK=0,//能打断
    EM_SPELL_BREAK_TYPE_BREAK,//不能打断
};
//人物进程还是远程标记
public enum EM_DIST_TYPE
{
    EM_DIST_TYPE_NEAR = 0,//近战
    EM_DIST_TYPE_FAR = 1,//远程
}
//技能被打断类型
public enum EM_SPELL_BEBREAK_TYPE
{
    EM_SPELL_BREAK_TYPE_NOBEBREAK = 0,//不能被打断
    EM_SPELL_BREAK_TYPE_BEBREAKVALUE,//伤害达到一定数值能被打断
    EM_SPELL_BREAK_TYPE_BEBREAKVPERCENT,//伤害达到生命最大上线的百分比被打断
};
public enum EM_SPELL_CASTING_TYPE
{
	EM_SPELL_CASTING_TYPE_INVALID = 0,
	EM_SPELL_CASTING_TYPE_IMMIDI1,		    // 瞬发
	EM_SPELL_CASTING_TYPE_IMMIDI2,		    // 瞬发
	EM_SPELL_CASTING_TYPE_MULTISECTION,		// 分段
	EM_SPELL_CASTING_TYPE_CHANNEL,		    // 引导
    EM_SPELL_CASTING_TYPE_CHARGE,		    // 吟唱
	EM_SPELL_CASTING_TYPE_NUMBER,
};
//技能震屏类型
public enum EM_SPELL_SHAKE_TYPE
{
    EM_SPELL_SHAKE_TYPE_INVALID=-1,
    EM_SPELL_SHAKE_TYPE_RELEASE=1,         //释放技能时候震屏
    EM_SPELL_SHAKE_TYPE_HIT,               //命中震屏
}
public enum EM_SPELL_AI_TYPE
{
    EM_SPELL_AI_TYPE_INVALID=0,         //手动模式
    EM_SPELL_AI_TYPE_NORMAL,            //标准模式
    EM_SPELL_AI_TYPE_ATTACK,            //优先战斗模式
    EM_SPELL_AI_TYPE_CURE,              //优先治疗模式
}
//技能AI逻辑类型
public enum EM_SPELL_AI_LOGIC
{
    EM_SPELL_AI_INVALID=-1,
    EM_SPELL_AI_LOGIC1=1,
    EM_SPELL_AI_LOGIC2,
    EM_SPELL_AI_LOGIC3,
    EM_SPELL_AI_LOGIC4,
    EM_SPELL_AI_LOGIC5,
    EM_SPELL_AI_LOGIC6,
    EM_SPELL_AI_LOGIC7,
    EM_SPELL_AI_LOGIC8,
    EM_SPELL_AI_LOGIC9,
    EM_SPELL_AI_LOGIC10,
    EM_SPELL_AI_LOGIC11,
    EM_SPELL_AI_LOGIC12,
    EM_SPELL_AI_LOGIC13,
    EM_SPELL_AI_LOGIC14,
    EM_SPELL_AI_LOGIC15,
    EM_SPELL_AI_LOGIC16,
    EM_SPELL_AI_LOGIC17,
    EM_SPELL_AI_LOGIC18,
    EM_SPELL_AI_LOGIC19,
    EM_SPELL_AI_LOGIC20,
    EM_SPELL_AI_LOGIC21,
    EM_SPELL_AI_LOGIC22,
    EM_SPELL_AI_LOGIC23,
    EM_SPELL_AI_LOGIC24,
    EM_SPELL_AI_LOGIC25,
    EM_SPELL_AI_LOGIC26,
    EM_SPELL_AI_LOGIC27,
    EM_SPELL_AI_MAX
}
//技能AI目标类型
public enum EM_SPELL_AI_TAGGRET
{
    EM_SPELL_AI_INVALID     =   -1,
    EM_SPELL_AI_TAGGRET1    =   1,
    EM_SPELL_AI_TAGGRET2,
    EM_SPELL_AI_TAGGRET3,
    EM_SPELL_AI_TAGGRET4,
    EM_SPELL_AI_TAGGRET5,
    EM_SPELL_AI_TAGGRET6,
    EM_SPELL_AI_TAGGRET7,
    EM_SPELL_AI_TAGGRET8,
    EM_SPELL_AI_TAGGRET9,
    EM_SPELL_AI_TAGGRET10,
    EM_SPELL_AI_TAGGRET11,
    EM_SPELL_AI_TAGGRET12,
    EM_SPELL_AI_TAGGRET13,
    EM_SPELL_AI_TAGGRET14,
    EM_SPELL_AI_MAX
}
//影响属性因素
public enum EM_EFFECT_SOURCE_TYPE
{
	EM_EFFECT_SOURCE_TYPE_INVALID = -1,
	EM_EFFECT_SOURCE_TYPE_IMPACT,					//impact
	EM_EFFECT_SOURCE_TYPE_RUNE,					    //装备
	EM_EFFECT_SOURCE_TYPE_TEAM,						//神器
    EM_EFFECT_SOURCE_TYPE_DOWER,                    //星图
    EM_EFFECT_SOURCE_TYPE_TRAIN,                    //培养
	EM_EFFECT_SOURCE_TYPE_NUMBER,
};
public enum EM_EXTRAITEM_TYPE
{
	EM_EXTRAITEM_TYPE_INVALID = -1,
	EM_EXTRAITEM_COMMON = 1,			//游戏币
	EM_EXTRAITEM_GOLD,					//充值币
	EM_EXTRAITEM_RONGHUN,				//熔魂点
	EM_EXTRAITEM_MP,					//怒气
	EM_EXTRAITEM_MP_PERCENT,			//怒气千分比
	EM_EXTRAITEM_HP = 21,			    //生命值
	EM_EXTRAITEM_CURRENTHP_PERCENT = 22,//当前生命千分比
    EM_EXTRAITEM_MAXHP_PERCENT,         //生命最大值百分比
	EM_EXTRAITEM_RUNEPOINT = 31,		//符文点
	EM_EXTRAITEM_PARTNER = 32,			//伙伴
	EM_EXTRAITEM_ITEM,					//材料
	EM_EXTRAITEM_EQUIP,					//符文

	EM_EXTRAITEM_TYPE_NUMBER,
};

//目标选择逻辑: 
//目标类型
public enum EM_TARGET_TYPE
{
	EM_TARGET_INVALID = -1,					    //默认选择
	EM_TARGET_FRIEND = 1,						//己方
	EM_TARGET_ENEMY = 2,						//2=敌方
    EM_TARGET_SELF = 3,					        //3=自己
    EM_TARGET_ALL = 4,					        //4=全体
    EM_TARGET_FRIEND_MIN_HPPERCENT,		        //5.生命千分比最低的友方单位
    EM_TARGET_ALL_NO_SELF,				        //6.全体 - 除去自己
    EM_TARGET_ENEMY_MIN_HPPERCENT,		        //7.生命千分比最低的敌方单位
    EM_TARGET_FRIEND_NO_SELF,		            //8.友方除去自己
    EM_TARGET_ATTACK_ME = 11,			        //11.攻击我的目标(子技能专用)
    EM_TARGET_ENEMY_RAND = 12,			        //12.随机的一个敌方目标(子技能专用)
    EM_TARGET_IMPACT_CASTER = 13,		        //13.调用该子技能的buff / debuff的释放者
    EM_TARGET_FRIEND_RAND = 20,                 //20,友方除去自己随机N个目标
    EM_TARGET_ENEMY_RANDOM =21,                 //21,敌方目标加随机N个目标
    EM_TARGET_FRIEND_RANDOM,                    //22,己方目标加随机N个目标
    EM_TARGET_SELF_RANDOM,                      //23,己方目标加随机N个目标(强制添加自己)
	EM_TARGET_TYPE_NUMBER,
};

//目标高级类型
public enum EM_SPELL_TARGET_SENIOR_TYPE
{
	EM_SPELL_TARGET_SENIOR_TYPE_INVALID = 0,
	EM_SEPLL_TARGET_REQUIRE_IMPACTEFFECTTYPE,	//效果类型
	EM_SEPLL_TARGET_REQUIRE_IMPACTID,	    //效果id
	EM_SPELL_TARGET_SENIOR_TYPE_NUMBER,
};

public enum EM_SPELL_PASSIVE_INDEX
{
    EM_SPELL_PASSIVE_INITIATIVE = 0,          //主动技能索引
    EM_SPELL_PASSIVE_FIRST = 1,               //第一个被动触发主动技能的索引
    EM_SPELL_PASSIVE_SECOND = 3,              //第二个被动触发主动技能的索引
}
public enum ENUM_HURT_TYPE
{
    HURT_TYPE_ALL = 0,			//所有伤害
    HURT_TYPE_PHY,				//物理伤害
    HURT_TYPE_MAGIC,			//法术伤害
    HURT_TYPE_DIRECT,			//直接伤害
    HURT_TYPE_NODIRECT,			//非直接伤害
};
enum ENUM_ATTACKHURT_TYPE
{
    HURT_ATTACKTYPE_COMMON = 1,		//普通攻击伤害
    //HURT_ATTACKTYPE_SPELL,			//技能攻击伤害
    HURT_ATTACKTYPE_ALL = 3,		    //所有伤害
};

/// <summary>
/// 技能结果
/// </summary>
public enum EM_SPELL_RESULT
{
	EM_SPELL_RESULT_NORMAL = 0,					// 正常
	EM_SPELL_RESULT_MISS,						// 未命中
    EM_SPELL_RESULT_CRITICAL,                   // 暴击
	EM_SPELL_RESULT_FAIL,						// 失败
};

public enum SPELL_EVENT_ID
{
    SPELL_EVENT_ID_INVALID = -1,
    SPELL_EVENT_ID_EFFECTATTACK_HURT,	//伤害加持
    SPELL_EVENT_ID_EFFECTBEATTACK_HURT, //受到伤害
    SPELL_EVENT_ID_BEFOREABSORB_HURT,	//伤害吸收前介入
    SPELL_EVENT_ID_ABSORB_HURT,			//伤害吸收
    SPELL_EVENT_ID_AFTERABSORB_HURT,	//伤害吸收后介入
    SPELL_EVENT_ID_SPELLACTIVE,			//技能激活
    SPELL_EVENT_ID_IMM_IMPACT,			//免疫buff
    SPELL_EVENT_ID_HURTDISTRIBUTE,		//伤害分摊
    SPELL_EVENT_ID_HURT_BACK,			//反弹伤害
    SPELL_EVENT_ID_BEFORE_DEAD,			//死亡
    SPELL_EVENT_ID_DODGE,				//闪避
    SPELL_EVENT_ID_BEDODGE,				//被闪避
    SPELL_EVENT_ID_CRITICAL,			//爆击
    SPELL_EVENT_ID_BECRITICAL,			//被爆击
    SPELL_EVENT_ID_BEHEAL,				//被治疗
    SPELL_EVENT_ID_KILLTARGET,			//杀死目标
    SPELL_EVENT_ID_SELFHPCHANGE,		//自身血量变化
    SPELL_EVENT_ID_BEADDIMPACT,			//增加buff
    SPELL_EVENT_ID_HURT,			    //产生伤害结算后(死后就不管了)
    SPELL_EVENT_ID_AFTERDAMAGE,			//伤害结算后(死后就不管了)
    SPELL_EVENT_ID_ADDIMPACT,			//加入impact
    SPELL_EVENT_ID_HITTARGET,           //命中目标
    SPELL_EVENT_ID_NOHITTARGET,         //未命中
    SPELL_EVENT_ID_USESPELL,            //使用技能
    SPELL_EVENT_ID_BEKILL,			    //被杀死
    SPELL_EVENT_ID_SPELLCONUMEMP,       //技能怒气消耗
    SPELL_EVENT_ID_HURTORHEALDELAY,     //伤害或治疗延时
    SPELL_EVENT_ID_CHANGEHURTEFFECT,    //改变对目标的伤害
    SPELL_EVENT_ID_NUMBER,
};

/// <summary>
/// impact结果
/// </summary>
public enum EM_IMPACT_RESULT
{
	EM_IMPACT_RESULT_NORMAL = 0,	//正常加目标身上
	EM_IMPACT_RESULT_FAIL,			//不能加在目标身上
	EM_IMPACT_RESULT_DISSAPEAR,		//抵消
};

/// <summary>
/// 技能逻辑,一个逻辑对应一类计算方式,如果计算方式不同则另外增加逻辑
/// </summary>
public enum EM_SPELL_LOGIC
{
	EM_SPELL_LOGIC_INVALID = -1,
	EM_SPELL_LOGIC1 = 1,				//
	EM_SPELL_LOGIC2,					//
	EM_SPELL_LOGIC3,					//
	EM_SPELL_LOGIC4,					//
	EM_SPELL_LOGIC5,					//
	EM_SPELL_LOGIC6,					//
	EM_SPELL_LOGIC7,					//
	EM_SPELL_LOGIC8,					//
	EM_SPELL_LOGIC9,					//
	EM_SPELL_LOGIC10,					//
	EM_SPELL_LOGIC11,					//
	EM_SPELL_LOGIC12,
    EM_SPELL_LOGIC14 = 14,					//
    EM_SPELL_LOGIC15,					//
    EM_SPELL_LOGIC16,
    EM_SPELL_LOGIC17,					//
    EM_SPELL_LOGIC18,		
	EM_SPELL_LOGIC_NUMBER,
};

public enum ENUM_SPELL_CHILDLOGIC_VALUETYPE
{
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_INVALID = 0,		    //参与计算时候会+1
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_VALUE = 1,				//0数值(固定值)
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_PHYATT_PERCENT = 2,		//1物理攻击百分比
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_MAGATT_PERCENT = 3,		//2法术攻击百分比
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_PHYDEF_PERCENT = 4,		//3物理防御百分比
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_MAGDEF_PERCENT = 5,		//4法术防御百分比
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_MAXHP_PERCENT = 6,		//5生命上限百分比
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_CURHP_PERCENT = 7,		//6当前生命百分比
    ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARPHYATT_PERCENT = 102,    //101目标物理攻击百分比
    ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARMAGATT_PERCENT = 103,    //102目标法术攻击百分比
    ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARPHYDFF_PERCENT = 104,    //103目标物理防御百分比
    ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARMAGDFF_PERCENT = 105,    //104目标物理防御百分比
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARMAXHP_PERCENT  = 106,	//105目标生命上限百分比
	ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARHP_PERCENT     = 107,    //106目标当前生命百分比

};

public enum ENUM_SPELL_CHILD_LOGIC
{
	SPELL_CHILD_LOGIC_INVALID = -1,		//无效
	SPELL_CHILD_LOGIC_HEAL_MAGIC = 1,	//治疗
	SPELL_CHILD_LOGIC_ATT_PHY = 2,		//物理攻击
	SPELL_CHILD_LOGIC_ATT_MAGIC = 3,	//法术攻击
	SPELL_CHILD_LOGIC_REDUCE_MP = 4,	//减少怒气
	SPELL_CHILD_LOGIC_INC_MP = 5,		//增加怒气
	SPELL_CHILD_LOGIC_HURT_POINT = 10,	//直接伤害数值
};
[System.Flags]
public enum ENUM_SPELL_TYPE_FLAG
{ 
    SPELL_NONE = 0,
    SPELL_MAGIC_HEAL = 1,         //治疗
    SPELL_PHY_ATT = 1 << 1,       //物理攻击
    SPELL_MAGIC_ATT = 1 << 2,     //法术攻击
    SPELL_REDUCE_MP = 1 << 3,     //减少怒气
    SPELL_INC_MP = 1 << 4,        //增加怒气
    SPELL_HURT_POINT = 1 << 5,    //直接伤害数值
}

public enum ACTION_TYPE
{
	ACTION_INVALID = -1,

	ACTION_CHARGE = 0,
	ACTION_CHANNEL = 1,
	ACTION_INSTANT = 2,
	ACTION_MULTISECTION = 3,
};
public enum EM_HERO_CAMP_TYPE
{
    EM_HERO_CAMP_INVALID=-1,
    EM_HERO_CAMP_TYPE1=1, //生灵
    EM_HERO_CAMP_TYPE2,   //神族
    EM_HERO_CAMP_TYPE3    //恶魔
}
//socket error step
public enum EM_SOCKET_ERROR_STEP : int
{
	EM_SOCKET_ERROR_STEP0 = -1,
	EM_SOCKET_ERROR_STEP1 = -2,
	EM_SOCKET_ERROR_STEP2 = -3,
	EM_SOCKET_ERROR_STEP3 = -4,
	EM_SOCKET_ERROR_STEP4 = -5,
	EM_SOCKET_ERROR_STEP5 = -6,
	EM_SOCKET_ERROR_STEP6 = -7,
	EM_SOCKET_ERROR_STEP7 = -8,
	EM_SOCKET_ERROR_STEP8 = -9,
	EM_SOCKET_ERROR_STEP9 = -10,
	EM_SOCKET_ERROR_STEP10 = -11,
	EM_SOCKET_ERROR_STEP11 = -12,
	EM_SOCKET_ERROR_STEP12 = -13,
	EM_SOCKET_ERROR_STEP13 = -14,
	EM_SOCKET_ERROR_STEP14 = -15,
	EM_SOCKET_ERROR_STEP15 = -16,
	EM_SOCKET_ERROR_STEP16 = -17,
	EM_SOCKET_ERROR_STEP17 = -18,
}

/// <summary>
/// 网络消息Fill方式
/// </summary>
public enum EM_TYPE_MSG_MODE : int
{
    /// <summary>
    /// 无效操作
    /// </summary>
	EM_TYPE_MSG_MODE_INVALID = -1,
    /// <summary>
    /// 写入操作
    /// </summary>
	EM_TYPE_MSG_MODE_WRITE = 0,
    /// <summary>
    /// 读取操作
    /// </summary>
	EM_TYPE_MSG_MODE_READ = 1,
    /// <summary>
    /// 枚举上限标记位！
    /// </summary>
	EM_TYPE_MSG_MODE_NUMBER,
}

//装备点
public enum EM_RUNE_POINT : int
{
    EM_RUNE_POINT_INVALID = -1,
    EM_RUNE_POINT_COMMON1 = 0,
    EM_RUNE_POINT_COMMON2,
    EM_RUNE_POINT_COMMON3,
    EM_RUNE_POINT_COMMON4,
    EM_RUNE_POINT_COMMON5,
    EM_RUNE_POINT_SPECIAL,		//6=特殊
    EM_RUNE_POINT_NUMBER,
};

//符文类型;
public enum EM_RUNE_TYPE : int
{
    EM_RUNE_TYPE_INVALID = -1,
    EM_RUNE_TYPE_BLUE = 1,              //蓝色;
    EM_RUNE_TYPE_PURPLE = 2,            //紫色;
    EM_RUNE_TYPE_GREEN = 3,             //绿色;
    EM_RUNE_TYPE_RED = 4,               //红色;
    EM_RUNE_TYPE_SPECIAL = 5,           //特殊非专属;
    EM_RUNE_TYPE_SPECIAL_UNIQUE = 6,    //特殊且专属;
}

//符文基础属性类型
public enum EM_RUNE_BASE_ATTRIBUTE_TYPE
{
    EM_RUNE_BASE_ATTRIBUTE_PASSIVE_SKILL = 100,     //被动技能ID
    EM_RUNE_BASE_ATTRIBUTE_ADD_COMMON_SKILL = 101,  //通用技能ID附加值
    EM_RUNE_BASE_ATTRIBUTE_ADD_PASSIVE_SKILL = 102, //被动技能ID附加值
    EM_RUNE_BASE_ATTRIBUTE_ADD_PVP_SKILL =103,      //PVP技能ID 附加值
    EM_RUNE_BASE_ATTRIBUTE_ADD_PVP_COMMON_SKILL = 104,//通用，pvp技能ID附加值
    EM_RUNE_BASE_ATTRIBUTE_ADD_ALL_SKILL = 105,     //三系技能ID附加值

    EM_RUNE_BASE_ATTRIBUTE_NUMBER_MAX = 106,
}

public enum PlatFromTypeNumber : int
{
	PLATFORM_Invilade = -1,
	PLATFORM_TEMP = 254,        //非正常
	PLATFORM_Number,
};
//神器属性影响
public enum EM_ARTIFACT_ATTRIBUTE_TYPE
{
    EM_ARTIFACT_ATTRIBUTE_MAXHP = 1,               //生命上限
    EM_ARTIFACT_ATTRIBUTE_PHYSICALATTACK = 2,      //物理攻击力
    EM_ARTIFACT_ATTRIBUTE_PHYSICALDEFENCE = 3,     //物理防御力
    EM_ARTIFACT_ATTRIBUTE_MAGICATTACK = 4,         //法术攻击力
    EM_ARTIFACT_ATTRIBUTE_MAGICDEFENCE = 5,        //法术防御力
    EM_ARTIFACT_ATTRIBUTE_HIT = 6,                 //命中值
    EM_ARTIFACT_ATTRIBUTE_DODGE = 7,               //闪避值
    EM_ARTIFACT_ATTRIBUTE_CRITICAL = 8,            //暴击值
    EM_ARTIFACT_ATTRIBUTE_TENACITY = 9,            //韧性值
    EM_ARTIFACT_ATTRIBUTE_NUM = 10,
}

/// <summary>
/// 对象类别
/// </summary>
public enum EM_OBJECT_CLASS
{
    EM_OBJECT_CLASS_INVALID = -1,
    EM_OBJECT_CLASS_SPELL = 1000,	    //1000000000 - 1099999999	技能
    EM_OBJECT_CLASS_BUFF = 1100,	    //1100000001 - 1199999999	BUFF
    EM_OBJECT_CLASS_DROPBOX = 1200,	    //1200000000 - 1299999999	掉落包
    EM_OBJECT_CLASS_MONSTER = 1300,	    //1300000000 - 1399999999	关卡与怪物
    EM_OBJECT_CLASS_RES = 1400,         //1400000001 - 1400999999	资源 对应数据表53
    EM_OBJECT_CLASS_RUNE = 1401,        //1401000001 - 1401999999	符文 对应数据表26
    EM_OBJECT_CLASS_COMMON = 1402,	    //1402000001 - 1402999999	道具 对应数据表26
    EM_OBJECT_CLASS_HERO = 1403,	    //1403000001 - 1403999999	英雄 对应数据表01
    EM_OBJECT_CLASS_SKIN = 1404,        //1404000001 - 1404999999	皮肤 对应数据表31
    EM_OBJECT_CLASS_BOX = 1405,      	//1405000001 - 1405999999	宝箱
    EM_OBJECT_CLASS_ARTIFACT = 1406,    //1406000001 - 1406999999	神器
    EM_OBJECT_CLASS_NUMBER,
};

/// -------资源类型------
/// 魔钻	1400 000001
/// 金币	1400 000002
/// 圣灵之泉	1400 000003
/// 熔炼点	1400000004
/// 黄金勋章	1400000005
/// 白银勋章	1400000006
/// 青铜勋章	1400000007
/// 赤铁勋章	1400000008
/// 经验结晶	1400000009
public enum EM_RESOURCE_TYPE : int
{
    Gold = 1400000001,
    Money = 1400000002,
    HeroMoney = 1400000003,
    RuneMoney = 1400000004,
    HuangjinXZ = 1400000005,
    BaiJinXZ = 1400000006,
    QingTongXZ = 1400000007,
    ChiTieXZ = 1400000008,
    ExpFruit = 1400000009,
}

/// <summary>
/// 道具类型
/// </summary>
public enum EM_ITEM_TYPE
{
    EM_ITEM_TYPE_INVALID = -1,
    EM_ITEM_TYPE_MATERIAL = 1,              //材料
    EM_ITEM_TYPE_GIFT = 2,                  //礼包
    EM_ITEM_TYPE_CONSUME = 3,               //消耗品
    EM_ITEM_TYPE_FRAGMENT=4,                //碎片
    EM_ITEM_TYPE_RUNE,                      //符文
}
/// <summary>
/// 道具筛选类型
/// </summary>
public enum EM_SORT_COMMON_ITEM
{
    EM_SORT_COMMON_ITEM_ALL,                //全部道具
    EM_SORT_COMMON_ITEM_CONSUME,            //消耗品
    EM_SORT_COMMON_ITEM_GIFT,               //礼包
    EM_SORT_COMMON_ITEM_MATERIAL,           //材料
    EM_SORT_COMMON_ITEM_FRAGMENT,           //英雄碎片
}
/// <summary>
/// 符文筛选类型
/// </summary>
public enum EM_SORT_RUNE_ITEM
{
    EM_SORT_RUNE_INVALID = -1,
    EM_SORT_RUNE_ITEM_ALL,                  //全部符文
    EM_SORT_RUNE_ITEM_SPECIAL = 5,               //橙色
    EM_SORT_RUNE_ITEM_RED = 4,                  //红色
    EM_SORT_RUNE_ITEM_GREEN = 3,                //绿色
    EM_SORT_RUNE_ITEM_PURPLE = 2,               //紫色
    EM_SORT_RUNE_ITEM_BLUE = 1,                 //蓝色
    EM_SORT_RUNE_ITEM_NEWGUIDE = 6,             //新手引导排序      
}

/// <summary>
/// 英雄卡牌排序;
/// </summary>
public enum EM_SORT_OBJECT_CARD
{
    LEVEL,
    QUALITY,
    NONE,
}

public enum EM_BAG_HASHTABLE_TYPE:int
{
    EM_BAG_HASHTABLE_TYPE_EMPTY = 0,
    EM_BAG_HASHTABLE_TYPE_COMMON = 1,       //道具
    EM_BAG_HASHTABLE_TYPE_SKILL  = 2,       //技能
    EM_BAG_HASHTABLE_TYPE_EQUIP  = 3,       //符文
    EM_BAG_HASHTABLE_TYPE_SOUL   = 4,       //魂魄
    EM_BAG_HASHTABLE_TYPE_COLLECTION= 5,   //收集
}

/// <summary>
/// 伙伴/英雄 刷新基本属性
/// </summary>
public enum EM_PARTNER_DETAIL_ATTRIBUTE : int
{
    /// <summary>
    /// 枚举初始无效值
    /// </summary>
	EM_PARTNER_DETAIL_ATTRIBUTE_INVALID = -1,
    /// <summary>
    /// 当前等级
    /// </summary>
	EM_PARTNER_DETAIL_ATTRIBUTE_LEVEL,               			              
    /// <summary>
    /// 当前经验   
    /// </summary>
	EM_PARTNER_DETAIL_ATTRIBUTE_EXP,         
    /// <summary>
    /// 训练,培养
    /// </summary>    			                             
    EM_PARTNER_DETAIL_ATTRIBUTE_TRAIN,		            		
    /// <summary>
    /// 枚举上限值标记为
    /// </summary>
	EM_PARTNER_DETAIL_ATTRIBUTE_NUMBER,
};

/// <summary>
/// 容器类型
/// </summary>
public enum EM_CONTAINER_TYPE : int
{
    /// <summary>
    /// 无效的容器类型
    /// </summary>
	EM_CONTAINER_INVALID = 0,
    /// <summary>
    /// 道具背包
    /// </summary>
	EM_CONTAINER_COMMONBAG,
    /// <summary>
    /// 伙伴背包，英雄列表
    /// </summary>
	EM_CONTAINER_PARTNERBAG,		
    /// <summary>
    /// 英雄符文背包
    /// </summary>
	EM_CONTAINER_PARTNEREQUIPBAG,
	/// <summary>
    /// //装备背包
	/// </summary>
	EM_CONTAINER_EQUIPBAG,		
    /// <summary>
    /// //装备
    /// </summary>
	EM_CONTAINER_EQUIP,			
    /// <summary>
    /// //仓库
    /// </summary>
	EM_CONTAINER_STORAGE,	
	/// <summary>
    /// //任务栏
	/// </summary>
	EM_CONTAINER_QUEST,			
    /// <summary>
    /// //碎片背包
    /// </summary>
	EM_CONTAINER_FRAGMENT,		
    /// <summary>
    /// //特殊道具
    /// </summary>
	EM_CONTAINER_SPECIAL,		
    /// <summary>
    /// 容器类型上限标记
    /// </summary>
	EM_CONTAINER_NUMBER,
};
//货币
enum EM_TYPE_CURRENCY_MODIFY
{
    EM_TYPE_CURRENCY_MODIFY_INVALID = -1,
    EM_TYPE_CURRENCY_MODIFY_REDUCE_SHOP_BUYITEM,			//商店购物
    EM_TYPE_CURRENCY_MODIFY_REDUCE_STRENGTHEN_RUNE,		//符文强化
    EM_TYPE_CURRENCY_MODIFY_ADD_QUEST,						//任务奖励
    EM_TYPE_CURRENCY_MODIFY_ADD_SHOP_SELLITEM,				//出售物品
    EM_TYPE_CURRENCY_MODIFY_ADD_GIFT,						//礼包获得
    EM_TYPE_CURRENCY_MODIFY_ADD_ACHIEVEMENT,				//成就奖励
    EM_TYPE_CURRENCY_MODIFY_ADD_FIGHT,						//战斗奖励
    EM_TYPE_CURRENCY_MODIFY_REDUCE_PARTNER_LEVELUPQUALITY,	//伙伴升品
    EM_TYPE_CURRENCY_MODIFY_REDUCE_PARTNER_IDENTIFY,			//伙伴鉴定
    EM_TYPE_CURRENCY_MODIFY_REDUCE_PARTNER_LEVELUP,			//伙伴升级
    EM_TYPE_CURRENCY_MODIFY_REDUCE_RUNE_LEVELUPQUALITY,	//符文升品
    EM_TYPE_CURRENCY_MODIFY_REDUCE_RESPAWN,				//复活
    EM_TYPE_CURRENCY_MODIFY_REDUCE_RUNE_SMITH,	       //符文洗炼
    EM_TYPE_CURRENCY_MODIFY_ADD_SHOP_SELLPARTNER,				//出售伙伴
    EM_TYPE_CURRENCY_MODIFY_REDUCE_SPELL_UNLOCK,			//技能解锁
    EM_TYPE_CURRENCY_MODIFY_REDUCE_LOTTERY,				//抽奖消耗
    EM_TYPE_CURRENCY_MODIFY_ADD_LOTTERY,					//抽奖获得
    EM_TYPE_CURRENCY_MODIFY_REDUCE_ARENA_FRESH,			//竞技场刷新
    EM_TYPE_CURRENCY_MODIFY_ADD_ARENA_WIN,					//竞技场阵营获胜奖励
    EM_TYPE_CURRENCY_MODIFY_ADD_ACTIVITIES,				//日常
    EM_TYPE_CURRENCY_MODIFY_ADD_MAIL,						//邮件
    EM_TYPE_CURRENCY_MODIFY_SYSTEM_SCRIPT,				//脚本赠与
    EM_TYPE_CURRENCY_MODIFY_REDUCE_RUNE_LEVELUP,			//符文升级
    EM_TYPE_CURRENCY_MODIFY_GIFT,							//礼包
    EM_TYPE_CURRENCY_MODIFY_ADD_HONORBUY,					//荣誉值兑换
    EM_TYPE_CURRENCY_MODIFY_ADD_YINDAO,					//增加引导
    EM_TYPE_CURRENCY_MODIFY_ADD_ARENA,						//竞技场挑战获得
    EM_TYPE_CURRENCY_MODIFY_ADD_LOGINLOTTERY,				//登录抽奖
    EM_TYPE_CURRENCY_MODIFY_ADD_WEBLOG,					//分享
    EM_TYPE_CURRENCY_MODIFY_REDUCE_CHAT,					//聊天
    EM_TYPE_CURRENCY_MODIFY_REDUCE_COMPOUNDMATRIAL,		//材料合成
    EM_TYPE_CURRENCY_MODIFY_REDUCE_RUNEENCHANT,			//符文附魔
    EM_TYPE_CURRENCY_MODIFY_ADD_RUNE_SMELT,	            //符文分解
    EM_TYPE_CURRENCY_MODIFY_REDUCE_RUNE_IDENTIFY,			//符文鉴定
    EM_TYPE_CURRENCY_MODIFY_REDUCE_PARTNER_TRAIN,	        //伙伴训练
    EM_TYPE_CURRENCY_MODIFY_ADD_HERO_SMELT,	            //英雄分解
    EM_TYPE_CURRENCY_MODIFY_ADD_RECHARGE,	            //冲值
    EM_TYPE_CURRENCY_MODIFY_LEVELUP,			        //升级
    EM_TYPE_CURRENCY_MODIFY_ADD_ARENACHALLENGE,			//竞技场挑战奖励
    EM_TYPE_CURRENCY_MODIFY_CLEAR_CLOSE,					//竞技场赛季结束，荣誉点清空
    EM_TYPE_CURRENCY_MODIFY_OPENITEMCELL,               //开格子
    EM_TYPE_CURRENCY_MODIFY_ILLUSTRATEDBOOK,            //图鉴
    EM_TYPE_CURRENCY_MODIFY_COUNT,
};


//竞技场
public enum ARENA_REQUEST : int
{
    ARENA_REQUEST_INVALID = -1,
    ARENA_REQUEST_HUMANARENA,				//角色竞技场数据
    ARENA_REQUEST_ARENA_RANK,				//排行榜
    ARENA_REQUEST_ARENA_CHALLENGE,			//挑战
    ARENA_REQUEST_QUIT,						//退出
};

/// <summary>
/// 社会关系操作
/// </summary>
public enum EM_OPERATOR_RELATION : int
{
    /// <summary>
    /// 枚举初始无效值
    /// </summary>
    EM_OPERATOR_INVALID = 0,
    /// <summary>
    /// //获取好友和申请
    /// </summary>
    EM_OPERATOR_FRIEND_GET,						
    /// <summary>
    /// //增加好友
    /// </summary>
    EM_OPERATOR_FRIEND_ADD,						
    /// <summary>
    /// //拒绝添加
    /// </summary>
    EM_OPERATOR_FRIEND_REFUSE,					
    /// <summary>
    /// //删除好友
    /// </summary>
    EM_OPERATOR_FRIEND_DEL,						
    /// <summary>
    /// //添加申请
    /// </summary>
    EM_OPERATOR_APPLY_ADD,						
    /// <summary>
    /// //通过ID查找
    /// </summary>
    EM_OPERATOR_FIND_FRIEND_BYID,				
    /// <summary>
    /// //通过名字查找
    /// </summary>
    EM_OPERATOR_FIND_FRIEND_BYNAME,				
    /// <summary>
    /// //通过ID更新
    /// </summary>
    EM_OPERATOR_UPDATE_FRIEND_BYID,				
    /// <summary>
    /// //删除所有申请
    /// </summary>
    EM_OPERATOR_APPLY_DELALL,				
	/// <summary>
	/// 枚举定义上限值
	/// </summary>
    EM_OPERATOR_NUMBER,
};


public enum LOTTERY_REQUEST : int
{
    LOTTERY_REQUEST_INVALID = -1,
    LOTTERY_REQUEST_ONE,						//抽1次
    LOTTERY_REQUEST_TEN,						//抽10次
};


public enum SHARE_AWARD_REQUEST : int
{
    SHARE_AWARD_REQUEST_INVALID = -1,
    SHARE_AWARD_REQUEST_AWARDDATA, //请求奖励数据
    SHARE_AWARD_REQUEST_SUCCESS    //通知服务器分享成功，服务器判断是否发放奖励(每日首次分享和第一次使用分享功能时发奖励)
};

public enum TEAM_REQUEST : int
{
    TEAM_REQUEST_INVALID = 0, // 请求阵型
    TEAM_REQUEST_SET,  // 设置阵型
    TEAM_REQUEST_SET_DEFAULT, //默认阵型
}

/// <summary>
/// 英雄的操作请求，包括升品，属性培养，容灵！
/// </summary>
enum PARTNER_REQUEST
{
    /// <summary>
    /// 枚举初始值 0
    /// </summary>
    PARTNER_REQUEST_INVALID = 0,
    /// <summary>
    /// //升品
    /// </summary>
    PARTNER_REQUEST_QUALITY_LEVELUP,				
    /// <summary>
    /// //属性培养
    /// </summary>
    PARTNER_REQUEST_TRAIN,							
    /// <summary>
    /// //英雄熔灵
    /// </summary>
    PARTNER_REQUEST_SMELT,
    /// <summary>
    /// 枚举定义上限值
    /// </summary>
    PARTNER_REQUEST_NUMBER,    			
};
/// <summary>
/// 神器的操作，注魂
/// </summary>
enum ARTIFACT_REQUEST
{
    /// <summary>
    /// 请求所有神器信息
    /// </summary>
    ARTIFACT_REQUEST_INVALID = 0,
    /// <summary>
    /// //注魂
    /// </summary>
    ARTIFACT_REQUEST_SET,                          
};
/// <summary>
/// 符文操作类型，强化，鉴定，熔炼
/// </summary>
enum RUNE_REQUEST
{
    /// <summary>
    /// 枚举初始值 -1
    /// </summary>
    RUNE_REQUEST_INVALID = 0,
    /// <summary>
    /// 符文强化
    /// </summary>
    RUNE_REQUEST_LEVELUP,
	/// <summary>
    /// 符文鉴定
	/// </summary>			
    RUNE_REQUEST_IDENTIFY,                          
    /// <summary>
    /// 符文熔炼
    /// </summary>
    RUNE_REQUEST_SMELT,
    /// <summary>
    /// 枚举定义上限值
    /// </summary>
    RUNE_OPERATOR_NUMBER,                          
};

/// <summary>
/// 字体颜色;
/// aqua (same as cyan) 同青色	#00ffffff	
/// black 黑色	#000000ff	
/// blue 蓝色	#0000ffff	
/// brown 棕色	#a52a2aff	
/// cyan (same as aqua) 青色	#00ffffff	
/// darkblue 深蓝色	#0000a0ff	
/// fuchsia (same as magenta) 紫红色（同洋红）	#ff00ffff	
/// green 绿色	#008000ff	
/// grey 灰色	#808080ff	
/// lightblue 浅蓝色	#add8e6ff	
/// lime 青橙绿	#00ff00ff	
/// magenta (same as fuchsia) 洋红色（同紫红色）	#ff00ffff	
/// maroon 褐红色	#800000ff	
/// navy 海军蓝	#000080ff	
/// olive 橄榄色	#808000ff	
/// orange 橙黄色	#ffa500ff	
/// purple 紫色	#800080ff	
/// red 红色	#ff0000ff	
/// silver 银灰色	#c0c0c0ff	
/// teal 蓝绿色	#008080ff	
/// white 白色	#ffffffff	
/// yellow 黄色     #ffff00ff
/// </summary>
public enum TEXT_COLOR
{
    AQUA,
    BLACK,
    BLUE,
    BROWN,
    CYAN,
    DARKBLUE,
    FUCHSIA,
    GREEN,
    GREY,
    LIGHTBLUE,
    LIME,
    MAGENTA,
    MAROON,
    NAVY,
    OLIVE,
    ORANGE,
    PURPLE,
    RED,
    SILVER,
    TEAL,
    WHITE,
    YELLOW,
};

///   BLUE,         66EBFF
//    PURPLE,       FF7BED
//    ORANGE,       FFA48E
//    YELLOW,       FFF58E
//    GREEN,        7EFF8A
//    LIGHTGREEN,   E6FFCC
//    LIGHTORANGE,  FFF2CC
//    LIGHTBLUE,    D9FFF4
public enum GAME_TXT_COLOR
{
    WHITE,
    BLUE,
    PURPLE,
    ORANGE,
    YELLOW,
    GREEN,
    LIGHTGREEN,
    LIGHTORANGE,
    LIGHTBLUE,
}

//商城页签id;
public enum SHOP_TAB
{
    PROP = 1,       //道具;
    GIFT = 2,       //礼包;
    SKIN = 3,       //皮肤;
    CHARGE = 4,     //充值;
    GOLD = 5,       //金币;
}

/// <summary>
/// 商店中物品限购类型;
/// </summary>
public enum SHOP_LIMIT_TYPE
{
    NONE,
    DAILY = 1,
    TOTAL,
}

public enum EM_SCENE_TYPE
{
    NORMAL = 1,
    JIXIANSHILIAN = 2,
}

//世界boss类型 [6/29/2015 Zmy]
public enum EM_WORLD_BOSS_TYPE
{
    EM_WORLD_BOSS_TYPE_1 = 1,//第一守门人
    EM_WORLD_BOSS_TYPE_2 = 2,//第一boss
    EM_WORLD_BOSS_TYPE_3 = 3,//第二守门人
    EM_WORLD_BOSS_TYPE_4 = 4,//第二boss
    EM_WORLD_BOSS_NUM = 5,
}

/// <summary>
/// 当前提示框展示类型枚举;
/// </summary>
public enum EM_RECHARGEBOX_OPEN_TYPE
{
    NONE = -1,
    EXPLORE_TIMEUP_HINT = 1,   //探险任务时间加速;
}

/// <summary>
/// 作者:
//1主线关卡 
//2难度2 
//3难度3 
//4支线关卡
//5特殊关卡 
//6活动关卡地精宝藏
//7活动关卡炎龙巢穴
//8极限试炼（特殊处理）
//9BOSS战之守望者
//10BOSS战之传说之战
/// </summary>
public enum EM_STAGE_TYPE
{
    NONE = -1,
    MAIN_QUEST1 = 1,
    MAIN_QUEST2 = 2,
    MAIN_QUEST3 = 3,
    SIDE_QUEST = 4,
    SPEC_QUEST = 5,
    ACTIVE_QUEST_DIJING = 6,
    ACTIVE_QUEST_YANLONG = 7,
    LIMIT_TEST = 8,
    BOSS_SHOUWANGZHE = 9,
    BOSS_CHUANSHUO = 10,
}

public enum EM_STAGE_DIFFICULTTYPE
{
    NONE= -1,
    NORMAL = 1,
    HARD = 2,
    HARDEST = 3,
}

/// <summary>
/// 主线   --- 分难度
/// 支线   --- 不分
/// 神秘商店--- 不分
/// 特殊关卡--- 不分
/// 活动关卡--- 不分
/// </summary>
public enum EM_STAGE_STAGETYPE
{
    NONE,
    MAIN,
    SIDE,
    MYSTERIOUS,
    SPECIAL,
    ACTIVE,
    BOSS,
    LITMIT_TIMES,
}

public enum EM_GETMAIL_TYPE
{
    GETNEW = 1,//获取新邮件列表
    GETMORE = 2,//获取更多邮件列表
    GETDEL = 3,//删除已读后 返回的新邮件列表
}
/// <summary>
/// 培养的4中元素类型
/// </summary>
public enum Bring_Type
{
    HUO = 0,
    EARTH = 1,
    WATER = 2,
    WIND = 3,
}

/// <summary>
/// 酒馆抽奖类型;
/// </summary>
public enum LOTTERY_TYPE
{
    Nor_One = 5,
    Nor_Ten = 6,
    Top_One = 7,
    Top_Ten = 8,
};