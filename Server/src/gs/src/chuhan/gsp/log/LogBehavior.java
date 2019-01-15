package chuhan.gsp.log;
public interface LogBehavior{
	//日志输出的类型
	public final static String DROPMADD = "dropMAdd";	//物品添加(掉落)
	public final static String USEMDEL = "useMdel";		//物品使用消耗
	public final static String DROPMTOMAIL = "dropMtoMail";	//物品掉落发邮件
	
	//物品添加的日志类型
	public final static String SHOPBUY = "shopbuy";		//商城购买
	public final static String LOTTERYFREE = "lotteryfree";		//招募英雄免费
	public final static String LOTTERYONE = "lotteryone";		//招募英雄一次
	public final static String LOTTERYTEN = "lotteryten";		//招募英雄10次
	public final static String LOTTERYDREAM = "lotterydream";		//招募英雄梦想兑换
	public final static String LOTTERYITEM = "lotteryitem";			//遗迹宝藏
	public final static String LOTTERYITEMFREE = "lotteryitemfree";			//遗迹宝藏免费
	public final static String LOTTERYITEMREFRESH = "lotteryitemrefresh";		//遗迹宝藏刷新
	public final static String LOTTERYITEMONE = "lotteryitemone";		//遗迹宝藏1次
	public final static String LOTTERYITEMTEN = "lotteryitemten";		//遗迹宝藏10次
	public final static String LOTTERYITEMSUPER10 = "lotteryitemsuper10";	//遗迹宝藏事件10
	public final static String HEROCLONE = "heroclone";				//英雄克隆
	public final static String BUYSMSHOP = "buysmshop";		//神秘商店购买
	public final static String BOSSPASS = "bosspass";			//世界战斗奖励
	public final static String BOSSRANK = "bossrank";		//BOSS排行奖励
	public final static String GAMEACT = "gameact";			//游戏活动
	public final static String MONTHCARDTODAY = "monthcardtoday";	//月卡奖励领取
	public final static String OPENMOHE = "openmohe";			//开魔盒
	public final static String TUJIANGET = "tujianget";			//图鉴获得
	public final static String TUJIANBOX = "tujianbox";			//图鉴宝箱
	public final static String MAILGET = "mailget";			//邮件获取
	public final static String BOSSSHOPBUY = "bossshopbuy";		//boss商城购买
	public final static String DUIHUANLP = "duihuanlp";			//兑换礼品
	public final static String TANXIANENDGET = "tanxianendget";	//探险结束
	public final static String ENDLESSOVER = "endlessover";		//极限试炼结束
	public final static String HUOYUEBOX = "huoyuebox";			//活跃度礼包
	public final static String STAGEREWARD = "stagereward";		//关卡战斗奖励
	public final static String STAGEBOX = "stagebox";		//关卡完美宝箱
	public final static String QIYUANTAI = "qiyuantai";			//祈愿台
	public final static String FOODUSEITEM = "fooduseitem";				//使用物品
	public final static String PIECEUSEITEM = "pieceuseitem";			//使用礼包
	public final static String NEWYINDAO = "newyindao";				//新手引导
	public final static String LOGINDROP = "logindrop";			//登录奖励
	public final static String HEROCOMPOSE = "herocompose";		//英雄合成
	
	//物品消耗的日志类型
	public final static String SHOPBUYCOST = "shopbuy_cost";		//商城购买
	public final static String SKILLUPCOST = "skillup_cost";		//技能升级
	public final static String BEGINBOSSCOST = "beginboss_cost";	//boss战快速进入
	public final static String BOSSBUYSWZLCOST = "bossbuyswzl_cost";	//boss购买守望之灵
	public final static String BUYSMSHOPCOST = "buysmshop_cost";		//神秘商店购买
	public final static String TANXIANSPEEDCOST = "tanxianspeed_cost";		//探险加速
	public final static String HEROCLONECOST = "heroclone_cost";			//英雄克隆
	public final static String LOTTERYDREAMCHANGECOST = "lotterydreamchange_cost";	//梦想兑换更换
	public final static String LOTTERYITEMREFRESHCOST = "lotteryitemrefresh_cost";		//遗迹宝藏刷新
	public final static String LOTTERYITEMONECOST = "lotteryitemone_cost";		//遗迹宝藏1次
	public final static String LOTTERYITEMTENCOST = "lotteryitemten_cost";		//遗迹宝藏10次
	public final static String LOTTERYONECOST = "lotteryone_cost";		//招募英雄一次
	public final static String LOTTERYTENCOST = "lotteryten_cost";		//招募英雄10次
	public final static String OPENMOHECOST = "openmohe_cost";			//开启魔盒
	public final static String RUNEIDENTIFYCOST = "runeIdentify_cost";		//符文鉴定
	public final static String RUNELEVELUPCOST = "runelevelup_cost";	//符文升级
	public final static String TANXIANREFRESHCOST = "tanxianRefresh_cost";	//探险刷新
	public final static String HEROSTARUPCOST = "herostarup_cost";			//英雄升星消耗
	public final static String HEROJINJIECOST = "herojinjie_cost";		//英雄进阶
	public final static String HEROPEIYANGCOST = "heropeiyang_cost";		//英雄培养
	public final static String HEROMSCOST = "heroms_cost";		//秘术升级
	public final static String HEROCOMPOSECOST = "herocompose_cost";		//英雄合成
	public final static String HEROEQUIPUPCOST = "heroequipup_cost";		//英雄装备升级和强化
	public final static String BOSSSHOPBUYCOST = "bossshopbuy_cost";		//boss商城购买

}