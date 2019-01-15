<xdb xgenOutput="src" trace="debug" traceTo=":out" corePoolSize="5" procPoolSize="20" schedPoolSize="5" dbhome="xdb" logpages="4096" backupDir="xbackup" checkpointPeriod="60000" backupIncPeriod="600000" backupFullPeriod="3600000" angelPeriod="5000" xdbVerify="true" allowBackup="false">
	<ProcedureConf executionTime="3000" maxExecutionTime="0" retryTimes="3" retryDelay="100"/>
sinclude(`conf.m4')

	<xbean name="ServerInfo">
		<variable name="firsttime" type="long" /> 第一次起服时间
		<variable name="starttime" type="long"/> 本次起服时间
	</xbean>
	<table name="serverinfo" key="int" value="ServerInfo" cacheCapacity="1" cachehigh="512" cachelow="256" />

	<xbean name="User">
		<variable name="username" type="string" /> 帐号名称
		<variable name="idlist" type="vector" value="long"/> 用户的角色列表 value是roleid
		<variable name="createtime" type="long" /> 帐号第一次进入游戏的时间
	</xbean>
	<table name="user" key="int" value="User" cacheCapacity="1024" cachehigh="512" cachelow="256" lock="userlock"/>


	<xbean name="AUUserInfo">		
		<variable name="retcode" type="int"/>
		<variable name="loginip" type="int"/>
		<variable name="blisgm" type="int"/>
		<variable name="nickname" type="string"/>
		<variable name="username" type="string"/>
	</xbean>		
	<table name="auuserinfo" key="int" value="AUUserInfo" cacheCapacity="4000" cachehigh="512" cachelow="256" lock="userlock"/>
	
	<!--<table name="username2id" key="string" value="int" cacheCapacity="4000" cachehigh="512" cachelow="256"/>-->
	
	<xbean name="mohe">
		<variable name="id" type="int"/>		id
		<variable name="isopen" type="int"/>	是否开启（1开启，0未开启）
		<variable name="place" type="int"/>		排序（0为随机排序，123为正常排序）
	</xbean>
	
	<xbean name="smshopdata">
		<variable name="id" type="int"/>		id
		<variable name="isopen" type="int"/>	是否购买（1购买，0未购买）
		<variable name="price" type="int"/>		价格
	</xbean>

	<xbean name="Properties">
		<variable name="rolename" type="string"/> 角色名
		<variable name="userid" type="int"/>  所属角色id
		<variable name="username" type="string"/>  上次登录的账号名称
		<variable name="plattypestr" type="string"/>  上次登录的平台string
		<variable name="mac" type="string"/>  上次登录的MAC地址
		<variable name="ostype" type="int"/>  系统类型
		<variable name="level" type="int" default="1"/>	 等级
		<variable name="exp" type="int"/>	 	 经验
		<variable name="viplv" type="int" default="1"/>	 vip等级
		<variable name="vipexp" type="int"/>	 vip经验
		<!--<variable name="vipitems" type="map" key="int" value="int"/> vip已经购买的物品-->
		<!--<variable name="vipdailyitems" type="map" key="int" value="int"/> vip今天购买的物品-->
		<variable name="ti" type="int"/>							体力
		<variable name="tichangetime" type="long"/>		体力更新时间
		
		<variable name="gold" type="int"/>			金币
		<variable name="yuanbao" type="int"/>			元宝（水晶）
		
		<variable name="shenglingzq" type="int"/>			圣灵之泉
		<variable name="ronglian" type="int"/>			熔炼点
		<variable name="huangjinxz" type="int"/>			黄金勋章
		<variable name="baijinxz" type="int"/>			白金勋章
		<variable name="qingtongxz" type="int"/>			青铜勋章
		<variable name="chitiexz" type="int"/>			赤铁勋章
		<variable name="jyjiejing" type="int"/>			经验结晶

		<variable name="pvpti" type="int"/>			PVP精力
		<variable name="pvptitime" type="long"/>		PVP精力更新时间
		<variable name="tanxianti" type="int"/>			探险行动力
		<variable name="tanxiantitime" type="long"/>		探险行动力更新时间
		
		<variable name="moheshop" type="map" key="int" value="mohe"/> 魔盒列表
		<variable name="smzhangjie" type="int"/>		神秘关卡或商店的所属章节记录
		<variable name="battlenum" type="int"/>			神秘关卡或商店记录
		<variable name="smendtime" type="long"/>			神秘关卡或商店结束时间
		<variable name="smshop" type="map" key="int" value="smshopdata"/> 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
		<variable name="smguanka_nocome" type="int"/>			神秘关卡未出现次数
		<variable name="smshop_notcome" type="int"/>			神秘商店未出现次数
		<variable name="createtime" type="long" /> 创建时间
		<variable name="onlinetime" type="long"/> 上线时间
		<variable name="offlinetime" type="long"/> 下线时间
		
		<variable name="tibuynum" type="int" default="0"/> 体力购买次数
		<variable name="tibuytime" type="long" default="0"/> 上次记录的体力购买时间
		<variable name="goldbuynum" type="int" default="0"/> 金币购买次数
		<variable name="goldbuytime" type="long" default="0"/> 上次记录的金币购买时间
		
		<variable name="signnum7" type="int" default="0"/> 连续签到ID
		<variable name="signnum28" type="int" default="0"/> 累计签到ID
		<variable name="signtime" type="long" default="0"/> 最后签到时间
		
		<variable name="qiyuannum" type="int" default="0"/> 累计祈愿台次数
		<variable name="qiyuantime" type="long" default="0"/> 最后祈愿时间
		<variable name="qiyuanallNum" type="int" default="3"/> 祈愿回合次数，第一次为3，完成后均为5
		
		<variable name="buybagnum" type="short" default="0"/> 扩充背包次数
		<variable name="buyherobagnum" type="short" default="0"/> 扩充英雄背包次数
		
		<variable name="troopNum" type="short" default="1"/> 		默认编队号
		
		<variable name="sweepnum" type="int" default="0"/> 今日扫荡次数
		<variable name="todaylasttime" type="long" default="0"/> 今日计时时间
		<variable name="sweepbuynum" type="int" default="0"/> 今日扫荡购买次数
		<variable name="mszqGetNum" type="int" default="0"/> 缪斯奏曲：个位为中午，十位为晚上	
		
		<variable name="newyindao" type="list" value="int"/> 新手引导

		
		<!--<variable name="selectvalue" type="float"/> 点将值-->
		<!--<variable name="goldenFreeSelect" type="int"/> 金牌免费将次数-->
		<!--<variable name="goldenYuanSelect" type="int"/> 金牌付费点将次数-->
		<!--<variable name="goldenFreetime" type="long"/> 下次金牌免费时间-->
		<!--<variable name="silverFreeSelect" type="int"/> 银牌点将次数-->
		<!--<variable name="silverYuanSelect" type="int"/> 银牌点将次数-->
		<!--<variable name="silverFreetime" type="long"/> 下次银牌免费时间-->
		<!--<variable name="bronzeFreeselect" type="int"/> 铜牌点将次数-->
		<!--<variable name="bronzeYuanselect" type="int"/> 铜牌点将次数-->
		<!--<variable name="bronzeFreetime" type="long"/> 上次铜牌免费领取时间-->
		<!--<variable name="todayFreetimes" type="int"/> 今天铜牌已免费次数-->
		
		<!--<variable name="alreadytips" type="set" value="int"/> 已经用过的tips-->
		<!--<variable name="buychestvalue" type="int"/> 买箱子权值-->
		<!--<variable name="getgoodvalue" type="int"/> 获取好东西的权值-->
		<!--<variable name="openchestvalue" type="int"/> 开箱子权值-->
		<!--<variable name="getFriTiliNum" type="int"/> 今日领取好友赠送体力次数-->
		<!--<variable name="friendNum" type="int"/>好友数量-->
		<!--<variable name="turntableFreeNum" type="int"/> 今日免费转盘的次数-->
    <!--    <variable name="cardName" type="string"/> 周卡或月卡的商品名称-->
		<!--<variable name="cardBuyTime" type="long"/>周卡或月卡的购买时间-->
		<!--<variable name="cardRebateTime" type="long"/>周卡或月卡的返利时间-->
	</xbean>
	<table name="properties" key="long" value="Properties" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock" autoIncrement="true"/> yanglk


	<xbean name="Buff">
		<variable name="id" type="int" />buff类型Id，一种类型的buff只能有一个
		<variable name="attachTime" type="long" default="0"/>buff attach时的时间，用于计算剩余时间和检测到期
		<variable name="time" type="long" default="0"/>计时buff总持续时间（period时的period）
		<variable name="round" type="int" default="0"/>计数buff剩余回合（period时的count）
		<variable name="amount" type="long" default="0"/>buff的剩余量（period时的initDelay）
		<variable name="effects" type="map" key="int" value="float"  /> key = effect type id
		<variable name="extdata" type="map" key="int" value="float"  /> 额外数据，由buff实现者自己定义和使用
	</xbean>
	<xbean name="BuffAgent">
		<variable name="buffs" type="map" key="int" value="Buff" />key为buffId
	</xbean>


	
	<xbean name="Item">
		<variable name="id" type="int"/> 物品编号 
		<variable name="flags" type="int"/> 标志，叠加的时候，flags 也 OR 叠加
		<variable name="position" type="int" default="-1"/> 包裹属性，位置。从0开始编号
		<variable name="number" type="int"/>数量
		<variable name="numbermap" type="map" key="int" value="int" capacity="8"/>数量
		<variable name="uniqueid" type="long"/> 物品的唯一id
	</xbean>
	<xbean name="Bag">		
		<variable name="money" type="long"/>
		<variable name="capacity" type="int"/>
		<variable name="nextid" type="int"/>
		<variable name="items" type="map" key="int" value="Item"/>
		<variable name="removedkeys" type="list" value="int"/>
	</xbean>
	<table name="bag" key="long" value="Bag" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> liuchen
	<table name="skillbag" key="long" value="Bag" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> liuchen

	<table name="equipbag" key="long" value="Bag" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> liuchen
	<table name="soulbag" key="long" value="Bag" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> liuchen
	<table name="collectbag" key="long" value="Bag" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> liuchen
	
	<xbean name="SkillExtData">		
		<variable name="level" type="int" default="1"/>
		<variable name="grade" type="int" default="0"/>
		<variable name="exp" type="int" default="0"/>
	</xbean>
	<table name="skillextdatas" key="long" value="SkillExtData" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> liuchen
	
	<xbean name="EquipExtData">		
		<variable name="level" type="int" default="0"/>强化等级
		<variable name="init1" type="int" default="-1"/>基础属性1，默认-1
		<variable name="init2" type="int" default="-1"/>基础属性2，默认-1
		<variable name="init3" type="int" default="-1"/>基础属性3，默认-1
		<variable name="attr1" type="int" default="-1"/>附属属性1，默认-1
		<variable name="attr2" type="int" default="-1"/>附属属性2，默认-1
		<variable name="attr3" type="int" default="-1"/>附属属性3，默认-1
		<variable name="attr4" type="int" default="-1"/>附属属性4，默认-1
	</xbean>
	<table name="equipextdatas" key="long" value="EquipExtData" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> liuchen
	
	<table name="roleonoffstate" type="map" key="long" value="int" cacheCapacity="10240" lock="rolelock" persistence="MEMORY"/> key是roleid,value是state
	
	<xbean name="BasicFightProperties">
		<variable name="hp" type="float" />
		<variable name="attack" type="float" />
		<variable name="defend" type="float" />
		<variable name="wisdom" type="float" />
	</xbean>
	
	
	<!-- 战队信息 by yanglk -->
	<xbean name="Troop">
		<variable name="troopNum" type="int"/>  战队编号
		<variable name="trooptype" type="int"/> 战队类型，1为前2后3，2为前3后2
		<variable name="location1" type="int"/> 0没装
		<variable name="location2" type="int"/> 0没装
		<variable name="location3" type="int"/> 0没装
		<variable name="location4" type="int"/> 0没装
		<variable name="location5" type="int"/> 0没装
	</xbean>
	<xbean name="Troops">
		<variable name="troops" type="list" value="Troop"/>
	</xbean>
	
	<table name="herotroops" key="long" value="Troops" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 邮件物品掉落信息 by yanglk -->
	<xbean name="MailItem">
		<variable name="objectid" type="int"/> 物品ID
		<variable name="dropnum" type="int"/> 数量
		<variable name="dropparameter1" type="int"/> 附加条件1
		<variable name="dropparameter2" type="int"/> 附加条件2
	</xbean>
	<!-- 邮件信息 by yanglk -->
	<xbean name="Mail">
		<variable name="key" type="int"/> 邮件唯一ID
		<variable name="sender" type="string"/> 发送者
		<variable name="title" type="string"/> 邮件标题
		<variable name="msg" type="string"/> 消息内容
		<variable name="innerdropIdList" type="list" value="int"/> 掉落包ID
		<variable name="items" type="list" value="MailItem"/> 掉落物品（非掉落包内容）
		<variable name="endtime" type="long"/> 结束时间
		<variable name="isopen" type="int"/> 是否打开过 0未打开，1已打开
		<variable name="strlist" type="list" value="string"/> 参数列表
	</xbean>
	<xbean name="Mails">
		<variable name="mails" type="list" value="Mail"/>
		<variable name="nextkey" type="int"/>
	</xbean>
	
	<table name="maillist" key="long" value="Mails" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 抽奖信息 by yanglk -->
	<xbean name="lotty">
		<variable name="freetime" type="long"/> 可以免费抽奖的时间
		<variable name="firstget" type="int" default="0"/> 首抽是否已经完成
		<variable name="dreamexp" type="int"/> 梦想值
		<variable name="dreamfree" type="int"/> 梦想改变是否免费
		<variable name="dream" type="int"/> 梦想兑换展示
		<variable name="singlelotty" type="map" key="int" value="int"/> 单抽增加值
		<variable name="tenlotty" type="map" key="int" value="int"/>	十连抽增加值
		<variable name="tensinglelotty" type="map" key="int" value="int"/>	十连抽大奖增加值
		<variable name="getherolotty" type="map" key="int" value="int"/>	梦想兑换增加值	
	</xbean>
	<table name="lottylist" key="long" value="lotty" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 遗迹宝藏信息 by yanglk -->
	<xbean name="LotteryItem">
		<variable name="id" type="int"/> 遗迹宝藏ID
		<variable name="isget" type="int"/> 是否领取
		<variable name="viewnum" type="int"/> 显示位置
		<variable name="superid" type="int"/> 激活的特殊事件
	</xbean>
	<xbean name="LotteryItemlayer">
		<variable name="LotteryItemlist" type="list" value="LotteryItem"/> 遗迹宝藏每层list
	</xbean>
	<xbean name="LotteryItemAll">
		<variable name="mapkey" type="int"/> 第几层
		<variable name="mapvalue" type="int"/> 第几个
		<variable name="superlist" type="list" value="int"/> 遗迹宝藏特殊list
		<variable name="monthfirsttime" type="long" /> 月卡首刷时间
		<variable name="freelotterytime" type="long" /> 免费单抽到期时间
		<variable name="lastrefreshtime" type="long" /> 上次刷新时间（每日刷新）
		<variable name="LotteryItemMap" type="map" key="int" value="LotteryItemlayer"/>	遗迹宝藏总信息
	</xbean>
	<table name="lotteryitemlist" key="long" value="LotteryItemAll" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 活跃度信息 by yanglk -->
	<xbean name="huoyue">
		<variable name="huoyueid" type="int"/> 活跃id
		<variable name="num" type="int"/> 发生次数
		<variable name="numAll" type="int"/> 总次数
		<variable name="huoyuetype" type="int"/> 任务类型
		<variable name="isok" type="int"/> 是否完成
	</xbean>
	<xbean name="huoyues">
		<variable name="huoyuenum" type="int" default="0"/> 活跃度
		<variable name="getnum" type="int" default="0"/> 领取记录，个位第一个，十位第二个~~
		<variable name="huoyuetime" type="long"/> 刷新时间，跨天用
		<variable name="huoyuemap" type="map" key="int" value="huoyue"/> 活跃任务列表，key为选择类型
	</xbean>
	<table name="huoyuelist" key="long" value="huoyues" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 世界boss信息 by yanglk -->
	<xbean name="boss">
		<variable name="lasthpall" type="long"/> 上次总血量
		<variable name="lastiskill" type="int"/> 上次是否杀掉，0未杀，1已杀
		<variable name="lastkillnum" type="long"/> 杀掉则为用时（毫秒），未杀则为受到的伤害
		<variable name="newhpall" type="long"/> 最新总血量
		<variable name="nowhp" type="long"/> 现在血量
		<variable name="bossId1" type="int"/> bossid(第一个守门人)
		<variable name="bossId2" type="int"/> bossid(第一个boss)
		<variable name="bossId3" type="int"/> bossid(第二个守门人)
		<variable name="bossId4" type="int"/> bossid(第二个boss)
		<variable name="bossiskill" type="int"/> 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
		<variable name="boss1killName" type="string"/> 击杀1者名称
		<variable name="boss2killName" type="string"/> 击杀2者名称
		<variable name="time" type="long"/> 上次刷新时间
	</xbean>
	<table name="bossdata" key="int" value="boss" cacheCapacity="10000" cachehigh="512" cachelow="256"/> yanglk
	
	<xbean name="bossshop">
		<variable name="time" type="long"/> 刷新时间
		<variable name="shoplist" type="list" value="int"/> 今天可买的物品表
		<variable name="hunternum" type="int"/> 今日猎人集市累计兑换次数
	</xbean>
	<xbean name="bossrole">
		<variable name="killhpall" type="long"/> 击杀总血量
		<variable name="killboss" type="int"/> 攻击boss类型，值为1234，代表4个boss
		<variable name="bossnowhp" type="long"/> 本次攻击前boss血量
		<variable name="time" type="long"/> 上次攻击时间
		<variable name="zhufunum" type="int"/> 祝福次数
		<variable name="shouwangzl" type="int"/> 守望之灵
		<variable name="chuanshuozs" type="int"/> 传说之石
		<variable name="bshop" type="bossshop"/> 猎人集市
	</xbean>
	<table name="bossrolelist" key="long" value="bossrole" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 世界boss排行榜信息 by yanglk -->
	<xbean name="bossRankInfo">
		<variable name="roleid" type="long"/>	玩家guid
		<variable name="rolename" type="string"/>	玩家名称
		<variable name="num" type="long"/>		伤害
		<variable name="rankid" type="int"/>		名次
	</xbean>
	<xbean name="bossRankList">
		<variable name="rankList" type="list" value="bossRankInfo"/> 排名列表
		<variable name="ranktime" type="long"/>	排名时间
		<variable name="bossid" type="int"/>	bossid：1234
	</xbean>
	<table name="bossranklists" key="int" value="bossRankList" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="bossrank"/> yanglk
	
	<!-- 探险信息 by yanglk -->
	<xbean name="tanxian">
		<variable name="tanxianid" type="int"/> 探险id
		<variable name="tanxiantype" type="int"/> 状态，0未开启，1进行中，2已完成
		<variable name="endtime" type="long"/> 结束时间
		<variable name="teamnum" type="int"/> 队伍号
	</xbean>
	<xbean name="stagetanxian">
		<variable name="stagetanxian" type="list" value="tanxian"/> 每章节探险列表
	</xbean>
	<xbean name="teamtanxian">
		<variable name="tanxianid" type="int"/> 探险id
		<variable name="team" type="list" value="int"/> 小队英雄key列表
	</xbean>
	<xbean name="stagetxall">
		<variable name="txtime" type="long"/> 探险每日刷新时间
		<variable name="teamallmap" type="map" key="int" value="teamtanxian"/> 探险任务小队表，key小队id（从1开始），value小队英雄key列表
		<variable name="stagetxallmap" type="map" key="int" value="stagetanxian"/> 探险任务总表，key是章节ID(从1开始)，value是章节探险列表
	</xbean>
	<table name="stagetxalllist" key="long" value="stagetxall" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	
	<!-- 月卡信息 by yanglk -->
	<xbean name="monthcard">
		<variable name="monthcardId" type="int"/> 月卡id
		<variable name="overtime" type="long"/> 到期时间
		<variable name="getboxlasttime" type="long"/> 领取奖励最后一次时间
	</xbean>
	<xbean name="monthcards">
		<variable name="rolemonthcards" type="map" key="int" value="monthcard"/>
	</xbean>
	
	<table name="monthcardlist" key="long" value="monthcards" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 活动信息 by yanglk -->
	<xbean name="gameactivity">
		<variable name="id" type="int"/> 活动id
		<variable name="time" type="long"/> 最近一次时间
		<variable name="todaynum" type="int"/> 今日次数
		<variable name="allnum" type="int"/> 累计次数
		<variable name="cangetnum" type="int"/> 可以领取次数（）
		<variable name="activitynum" type="int"/>活动计数
		<variable name="allactivitynum" type="int"/>累计计数
		<variable name="issee" type="int"/> 是否看过（提示用，0未看，1已看）
	</xbean>
	<xbean name="gameactivitys">
		<variable name="gameactivitymap" type="map" key="int" value="gameactivity"/>
	</xbean>
	
	<table name="gameactivitylist" key="long" value="gameactivitys" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 装备信息 by yanglk -->
	<xbean name="Equip">
		<variable name="key" type="int"/> 物品唯一ID
		<variable name="equipid" type="int"/> 物品ID
		<variable name="qianghualevel" type="int"/>   强化等级
		<variable name="attr1odds" type="int"/>   属性1几率
		<variable name="attr2odds" type="int"/>   属性2几率
		<variable name="qhadd" type="int"/>   强化增加几率
	</xbean>
	<xbean name="EquipColumn">
		<variable name="equips" type="list" value="Equip"/>
		<variable name="nextkey" type="int"/>
	</xbean>
	
	<table name="equipcolumns" key="long" value="EquipColumn" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	
	<!-- 英雄信息 by yanglk -->
	<xbean name="Hero">
		<variable name="key" type="int"/> 英雄唯一ID
		<variable name="heroid" type="int"/> 英雄配表ID
		<variable name="heroexp" type="int"/> 英雄本级经验
		<variable name="herolevel" type="int"/>         英雄等级
		<variable name="heroallexp" type="int"/>     英雄总经验
		<variable name="qianghualevel" type="int"/>   强化等级
		<variable name="qhadd" type="int"/>   强化增加几率
		<variable name="weapon" type="int"/>   武器
		<variable name="barde" type="int"/>   铠甲
		<variable name="ornament" type="int"/>   饰品
		<variable name="peiyang1" type="int"/>   培养1编号（默认为0）
		<variable name="peiyang2" type="int"/>   培养2编号（默认为0）
		<variable name="peiyang3" type="int"/>   培养3编号（默认为0）
		<variable name="peiyang4" type="int"/>   培养4编号（默认为0）
		<variable name="skill1" type="int"/>   技能1编号（未开通为0）
		<variable name="skill2" type="int"/>   技能2编号（未开通为0）
		<variable name="skill3" type="int"/>   技能3编号（未开通为0）
		<variable name="heroviewid" type="int"/>   英雄外观
		<variable name="items" type="map" key="int" value="int"/>
	</xbean>
	<xbean name="HeroColumn">
		<variable name="heroes" type="list" value="Hero"/>
		<variable name="nextkey" type="int"/>
	</xbean>
	
	<table name="herocolumns" key="long" value="HeroColumn" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 英雄皮肤信息 by yanglk -->
	<xbean name="HeroSkin">
		<variable name="heroskinid" type="int"/>  皮肤ID
		<variable name="createtime" type="long"/>  创建时间
	</xbean>
	<xbean name="HeroSkinColumn">
		<variable name="heroskins" type="list" value="HeroSkin"/>
	</xbean>
	
	<table name="heroskincolumns" key="long" value="HeroSkinColumn" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 魔盒几率增加值信息 by yanglk -->
	<xbean name="moheodds">
		<variable name="moheoddsmap" type="map" key="int" value="int"/>  魔盒几率列表
	</xbean>
	
	<table name="moheoddses" key="long" value="moheodds" cacheCapacity="10000" cachehigh="512" cachelow="256"/> yanglk
	
	<!-- 英雄克隆信息 by yanglk -->
	<xbean name="heroclone">
		<variable name="cloneList" type="list" value="int"/>  英雄克隆信息列表
	</xbean>
	
	<table name="heroclones" key="long" value="heroclone" cacheCapacity="10000" cachehigh="512" cachelow="256"/> yanglk
	
	<!-- 兑换礼券信息 by yanglk -->
	<xbean name="duihuanlq">
		<variable name="lqkey" type="int"/>  兑换礼券key 
		<variable name="typenum" type="int"/>  兑换礼券替换计数
		<variable name="cloneList" type="list" value="string"/> 
	</xbean>
	<table name="duihuanlqs" key="int" value="duihuanlq" cacheCapacity="10000" cachehigh="512" cachelow="256"/> yanglk
	
	<xbean name="roleduihuanlq">
		<variable name="lqkey" type="int"/>  兑换礼券key 
		<variable name="typenum" type="int"/>  兑换礼券替换计数
		<variable name="num" type="int"/>  兑换礼券计数 
	</xbean>
	<xbean name="roledhmap">
		<variable name="dhmap" type="map" key="int" value="roleduihuanlq"/>  兑换礼券计数列表
	</xbean>
	<table name="roledhmaps" key="long" value="roledhmap" cacheCapacity="10000" cachehigh="512" cachelow="256"/> yanglk
	
	<!-- 神器信息 by yanglk -->
	<xbean name="Artifact">
		<variable name="artifactType" type="int"/>  神器类型（key）
		<variable name="artifactId" type="int"/>  神器ID
		<variable name="heroNum1" type="int"/>  英雄数量1
		<variable name="heroNum2" type="int"/>  英雄数量2
		<variable name="heroNum3" type="int"/>  英雄数量3
		<variable name="heroNum4" type="int"/>  英雄数量4
		<variable name="heroNum5" type="int"/>  英雄数量5
	</xbean>
	<xbean name="ArtifactColumn">
		<variable name="artifacts" type="map" key="int" value="Artifact"/>
	</xbean>
	
	<table name="artifactcolumns" key="long" value="ArtifactColumn" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 商城购买记录 by yanglk -->
	<xbean name="Shopbuy">
		<variable name="shopid" type="int"/>  商城ID（key）
		<variable name="todaynum" type="int"/>  今日已购买次数
		<variable name="lasttime" type="long"/>  最后一次购买时间
		<variable name="buyallnum" type="int"/>  总共购买次数
	</xbean>
	<xbean name="ShopbuyColumn">
		<variable name="shopbuys" type="map" key="int" value="Shopbuy"/>
	</xbean>
	
	<table name="shopbuycolumns" key="long" value="ShopbuyColumn" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<!-- 他人查看英雄 by yanglk -->
	<xbean name="OtherHero">
		<variable name="heroid" type="int"/> 英雄配表ID
		<variable name="exp" type="int"/> 当前经验
		<variable name="herolevel" type="int"/>         英雄等级
		<variable name="hp" type="int"/>     		血量
		<variable name="PhysicalAttack" type="int"/>   物理攻击
		<variable name="PhysicalDefence" type="int"/>   物理防御
		<variable name="MagicAttack" type="int"/>    魔法攻击
		<variable name="MagicDefence" type="int"/>   魔法防御
		<variable name="skill1" type="int"/>   技能1编号（未开通为0）
		<variable name="skill2" type="int"/>   技能2编号（未开通为0）
		<variable name="skill3" type="int"/>   技能3编号（未开通为0）
		<variable name="heroviewid" type="int"/>   英雄外观
	</xbean>
	
	<!-- 极限试炼数据 by yanglk -->
	<xbean name="EndlessInfo">
		<variable name="battleid" type="int"/>关卡id
		<variable name="groupnum" type="int"/>第几轮
		<variable name="useherokeyList" type="map" key="int" value="int"/> 使用英雄id和位置（key为位置，value为herokey）
		<variable name="monstergroup" type="list" value="int"/>	怪物组
		<variable name="trooptype" type="int" />				战队类型
		<variable name="monstertrooptype" type="int" />			怪物战队类型
		<variable name="pact" type="int" />				今日战斗强者之约（没有则为-1）
		<variable name="dropnum" type="int" />			剩余勇者证明数量
		<variable name="alldropnum" type="int" />		勇者证明总数量
		<variable name="add1" type="int" />				属性1购买次数
		<variable name="add2" type="int" />				属性2购买次数
		<variable name="add3" type="int" />				属性3购买次数
		<variable name="add4" type="int" />				属性4购买次数（仅计数）
		<variable name="herobloodList" type="map" key="int" value="int"/> 使用英雄的血量（key为位置，value为血量）
		<variable name="isend" type="int" />			0未开始，1进行中，2结束
		<variable name="time" type="long" />			此记录时间
		<variable name="ishalfcostpact" type="int" />		上次购买的强者之约是否达成（0是达成，1是未达成）
		<variable name="endtime" type="long" />			结束时间
		<variable name="expectedrank" type="int" />		预期排名
		<variable name="heroAttribute" type="map" key="int" value="OtherHero"/> 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
		<variable name="onranknum" type="int" />		连续在榜次数
		<variable name="onranklasttime" type="long" />		最后在榜时间
		<variable name="isnotfirst" type="int" />		不是第一次战斗（0是第一次战斗，1不是第一次战斗）
	</xbean>
	<table name="endlesscolumns" key="long" value="EndlessInfo" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/> yanglk
	
	<xbean name="EndlessRankInfo">
		<variable name="roleid" type="long"/>	玩家guid
		<variable name="rolename" type="string"/>	玩家名称
		<variable name="level" type="int"/>		玩家等级
		<variable name="groupnum" type="int"/>第几轮
		<variable name="trooptype" type="int" />				战队类型
		<variable name="alldropnum" type="int" />		勇者证明总数量
		<variable name="heroAttribute" type="map" key="int" value="OtherHero"/> 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）
		<variable name="onranknum" type="int" />		连续在榜次数
	</xbean>
	<xbean name="EndlessRankList">
		<variable name="rankList" type="list" value="EndlessRankInfo"/> 排名列表
		<variable name="ranktime" type="long"/>	排名时间
	</xbean>
	<table name="endlessranklists" key="int" value="EndlessRankList" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="endlessrank"/> yanglk
	
	<xbean name="GameLevel">
		<variable name="battleid" type="int"/> 副本ID
		<variable name="useherokeyList" type="map" key="int" value="int"/> 关卡用到的英雄
		<variable name="dropGold" type="int"/> 掉落金币
		<variable name="dropCrystal" type="int"/> 掉落宝石
		<variable name="equipIdList" type="list" value="int" /> 掉落物品列表
		<variable name="trooptype" type="int"/> 战队类型
	</xbean>
	<table name="gamelevels" key="long" value="GameLevel" lock="rolelock" cacheCapacity="4000" cachehigh="512" cachelow="256"	/>
	
	
	<xbean name="RealTimeRole">
		<variable name="realtimerank" type="int"/> 天梯排名
		<variable name="realtimenum" type="int" default="1000"/> 天梯分数
		<variable name="enermies" type="list" value="long" /> 最近的4个仇敌
		<variable name="fightnum" type="int"/> 今天战斗次数
		<variable name="lastfighttime" type="long"/> 上次战斗时间
	</xbean>
	<table name="realtimeroles" key="long" value="RealTimeRole" lock="rolelock" cacheCapacity="4000" cachehigh="512" cachelow="256"	/>
	
	
	
	<xbean name="XiuLianResult">
		<variable name="herokey" type="int"/>
		<variable name="hp"  type="int"/>
		<variable name="attack" type="int"/>
		<variable name="defend" type="int"/>
		<variable name="wisdom" type="int"/>
	</xbean>
	<table name="xiulianresults" key="long" value="XiuLianResult" cacheCapacity="8000" cachehigh="512" cachelow="256" lock="rolelock" persistence="MEMORY"	/> liuchen
	
	<!--战斗内表 START-->
	<xbean name="FighterInfo">
		<variable name="fighterId" type="int"/>
		<variable name="fightertype" type="int"/>
		<variable name="pos" type="int"/>
		<variable name="heroId" type="int"/>
		<variable name="grouptype" type="int"/>阵营
		<variable name="level" type="int" default="1"/> 等级
		<variable name="color" type="int" default="0"/> 颜色
		<variable name="grade" type="int" default="0"/> 阶
		<variable name="weaponinfo" type="int" default="0"/> 武器信息
		<variable name="armorinfo" type="int" default="0"/> 铠甲信息
		<variable name="horseinfo" type="int" default="0"/> 战马信息
		<variable name="speed" type="int" default="0"/> 速
		<variable name="hp" type="int" /> 兵力
		<variable name="bfp" type="BasicFightProperties"/>   基础战斗属性
		<variable name="effects" type="map" key="int" value="float" /> 效果 key = effect type id
		<variable name="finalattrs" type="map" key="int" value="float" />最终属性 key = attr type
		<variable name="buffAgent" type="BuffAgent" /> buff代理
		<variable name="skills" type="list" value="BattleSkill" /> 技能
		<variable name="shape" type="int"/> 造型ID
		
		<enum name="HERO" value="1" />
		<enum name="MONSTER" value="2" />
	</xbean>
	
	<xbean name="BattleSkill">
		<variable name="id" type="int" />
		<variable name="level" type="int" />
		<variable name="castrate" type="int" />以千为底
	</xbean>
	
	<xbean name="BattleInfo" any="true">
		<variable name="battleid" type="int"/>
		<variable name="battlelevel" type="int"/>
		<variable name="battletype" type="int"/>
		<variable name="fighterInfos" type="map" key="int" value="FighterInfo" /> key=fighterid
		<variable name="fighters" type="map" key="int" value="chuhan.gsp.battle.Fighter" /> key=fighterid
		<variable name="deadfighters" type="map" key="int" value="FighterInfo" /> key=fighterid
		<variable name="battlereulst" type="int"/>
		<variable name="round" type="int"/>
		<variable name="turn" type="int"/>
		<variable name="engine" type="chuhan.gsp.util.FightJSEngine"/> 用于本场战斗的JS引擎
	</xbean>
	<table name="battles" key="long" value="BattleInfo" cacheCapacity="0" cachehigh="512" cachelow="256" persistence="MEMORY"	/> liuchen
	<!--战斗内表 END-->
	
	<xbean name="StageBattleInfo" >
		<variable name="id" type="int"/>
		<variable name="maxstar" type="int"/>
		<variable name="fightnum" type="int"/>
		<variable name="lastfighttime" type="long"/>
		<variable name="allfightnum" type="int" default="1"/>
		<variable name="buybattlenum" type="int" default="0"/> 购买关卡次数
		<variable name="buybattlelasttime" type="long" default="0"/> 最后购买关卡次数时间
	</xbean>
	<xbean name="StageInfo" >
		<variable name="id" type="int"/>
		<variable name="rewardgot" type="int"/>
		<variable name="stagebattles" type="map" key="int" value="StageBattleInfo"/>
	</xbean>
	<xbean name="StageRole" >
		<variable name="stages" type="map" key="int" value="StageInfo"/>
	</xbean>
	<table name="stageroles" key="long" value="StageRole" lock="rolelock" cacheCapacity="4000" cachehigh="512" cachelow="256"	/>
	
	<xbean name="BeautyInfo" >
		<variable name="id" type="int"/>
		<variable name="alreadytimes" type="int"/>
		<variable name="maxtimes" type="int"/>
		<variable name="currentSkin" type="int"/>
		<variable name="haveSkins" type="list" value="int"/>
	</xbean>
	<xbean name="BeautyRole" >
		<variable name="sleeptimes" type="int"/>
		<variable name="lastsleeptime" type="long"/>
		<variable name="beauties" type="map" key="int" value="BeautyInfo"/>
	</xbean>
	<table name="beautyroles" key="long" value="BeautyRole" lock="rolelock" cacheCapacity="4000" cachehigh="512" cachelow="256"	/>
	
	<xbean name="LadderRole">
		<variable name="ladderrank" type="int"/> 天梯排名
		<variable name="laddersoul" type="int"/> 天梯元魂
		<variable name="lastsoulchangetime" type="long"/> 上次天梯元魂变动时间
		<variable name="enermies" type="list" value="long" /> 最近的4个仇敌
		<variable name="fighttimes" type="int"/> 今天战斗次数
		<variable name="lastfighttime" type="long"/> 上次战斗时间
	</xbean>
	<table name="ladderroles" key="long" value="LadderRole" lock="rolelock" cacheCapacity="4000" cachehigh="512" cachelow="256"	/>
	
	<xbean name="LadderInfo">
		<variable name="roleId" type="long"/>
	</xbean>
	<table name="pvpladder" key="int" value="LadderInfo" cacheCapacity="4000" cachehigh="512" cachelow="256"	/>
	
	<xbean name="SupplyActRole">
		<variable name="lastsupplytime" type="long"/>
		<variable name="firstsupplyed" type="boolean" default="false"/>
		<variable name="secondsupplyed" type="boolean" default="false"/>
	</xbean>
	<table name="supplyactroles" key="long" value="SupplyActRole" lock="rolelock" cacheCapacity="4000" cachehigh="512" cachelow="256"	/>
	
	<xbean name="BillData">
		<variable name="billid" type="long" />
		<variable name="goodid" type="int" />
		<variable name="goodnum" type="int" />
		<variable name="present" type="int" />
		<variable name="price" type="float" /> 总价格
		<variable name="createtime" type="long" /> 创建时间
		<variable name="state" type="int" />
		<variable name="confirmtimes" type="int" /> 向au确认订单的次数
		<variable name="platbillid" type="string" /> 平台生成的订单号
		
		<enum name="STATE_SENDED" value="1" /> 已通知客户端
		<enum name="STATE_CONFIRMED" value="2" /> 已确认并且发放
		<enum name="STATE_FAILED" value="4" /> 确认失败的
	</xbean>
	<xbean name="BillRole">
		<variable name="bills" type="map" key="long" value="BillData"/>
		<variable name="firstcharge" type="int" /> 是否已首充
	</xbean>
	<table name="billroles" key="long" value="BillRole" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"	/>
	
	<xbean name="GoogleReceiptData">
		<variable name="roleId" type="long" />
		<variable name="packageName" type="string"/>
		<variable name="productId" type="string"/>
		<variable name="token" type="string"/>
	</xbean>	
	<table name="googlereceiptes" key="long" value="GoogleReceiptData" cacheCapacity="3000" cachehigh="512" cachelow="256"/> key=transactionid
	
	<xbean name="AppReceiptData">
		<variable name="roleId" type="long" />
		<variable name="receipt" type="string" /> 苹果账单
	</xbean>	
	<table name="appreceiptes" key="long" value="AppReceiptData" cacheCapacity="3000" cachehigh="512" cachelow="256"/> key=transactionid

	<!--系统补偿-->
	<xbean name="CompenseRole">
		<variable name="fetchedcompenses" type="set" value="int"/>
	</xbean>
	<table name="compenseroles" key="long" lock="rolelock" value="CompenseRole" cacheCapacity="3000"/>
	
	<!--系统消息-->
	<xbean name="SysMsg">
		<variable name="time" type="long" />
		<variable name="msgid" type="int" />
		<variable name="params" type="list" value="string"/>
		<variable name="text" type="string" />
		<variable name="isnew" type="boolean" />
		<variable name="sended" type="boolean" default="false" />
		<variable name="sendRoleId" type="long"/> 发送者id 系统-0
		<variable name="msgType" type="int"/> 消息类型 0-系统 1-好友
	</xbean>
	<xbean name="MsgRole">
		<variable name="sysmsgs" type="list" value="SysMsg"/>
	</xbean>
	<table name="msgroles" key="long" value="MsgRole" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"	/>
	
	<!--武将任务-->
	<xbean name="HeroTaskRole">
		<variable name="refreshtime" type="long"/>刷新时间
		<variable name="endtask" type="set" value="int"/> 今天已经做完的任务
		<variable name="taskposes" type="map" key="int" value="long"/> 位置的冷却时间
	</xbean>
	<table name="herotaskroles" key="long" value="HeroTaskRole" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"	/>
	
	<!--血战-->
	<xbean name="BloodRole">
		<variable name="curlevel" type="int" default="1"/> 当前层数
		<variable name="lasthard" type="int"/> 上一次战斗的难度
		<variable name="curstar" type="int"/>剩余没用的星
		<variable name="battle1" type="int"/>随机出的战斗
		<variable name="battle2" type="int"/>
		<variable name="battle3" type="int"/>
		<variable name="itemlevel" type="int" />已经获得的物品等级
		<variable name="effects" type="map" key="int" value="float"/> 以前已加成的效果
		<variable name="failed" type="int"/> 1已失败
		<variable name="relivetimes" type="int"/> 今天已复活次数
		<variable name="lastfighttime" type="long"/> 上次战斗时间
		<variable name="totalstar" type="int"/>累计星
		<variable name="maxlevel" type="int"/> 最高层
		<variable name="repeatstaraward" type="map" key="int" value="int"/>
		<variable name="fixstaraward" type="map" key="int" value="int"/>
	</xbean>
	<table name="bloodroles" key="long" value="BloodRole" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"	/>
	
	<xbean name="BloodRankRole">
		<variable name="roleid" type="long"/>
		<variable name="maxlevel" type="int"/>
	</xbean>
	<xbean name="BloodRankList">
		<variable name="curweek" type="int"/>
		<variable name="rankers" type="list" value="BloodRankRole"/> 以前已加成的效果
	</xbean>
	<table name="bloodranklist" key="int" value="BloodRankList" cacheCapacity="1" cachehigh="512" cachelow="256"/>
	
	<!--mac对应的数据-->
	<xbean name="MacInfo">
		<variable name="onlinetime" type="long"/>
		<variable name="offlinetime" type="long"/>
	</xbean>
	<table name="macinfos" key="string" value="MacInfo" cacheCapacity="3000" cachehigh="512" cachelow="256"	/>
	
	<!--AwardLimit-->
	<xbean name="AwardLimitDay">
		<variable name="limititems" type="map" key="int" value="int"/>
	</xbean>
	<xbean name="AwardLimitRole">
		<variable name="limitdays" type="map" key="int" value="AwardLimitDay"/>
	</xbean>
	<table name="awardlimitroles" key="long" value="AwardLimitRole" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>
	
	<!--累计充值活动-->
	<xbean name="ChargeActivity">
		<variable name="activityid" type="int"/> 在该id活动中
		<variable name="totalcharge" type="int"/> 充值的总数
		<variable name="isgainaward" type="map" key="int" value="boolean"/> 是否已经领取奖励 key=元宝数量
	</xbean>
	<xbean name="ChargeActivityRole">
		<variable name="activities" type="map" key="int" value="ChargeActivity"/> key=活动id
	</xbean>
	<table name="chargeactivities" key="long" value="ChargeActivityRole" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

	<!--充值返利活动-->
	<xbean name="RebateChargeActivity">
		<variable name="awardinfo" type="map" key="int" value="int"/> key=rmb valeu=num
	</xbean>
	<xbean name="RebateChargeActivityRole">
		<variable name="activities" type="map" key="int" value="RebateChargeActivity"/> key=活动id
	</xbean>
	<table name="rebatechargeactivities" key="long" value="RebateChargeActivityRole" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>
	
        <!--首冲活动-->
	<xbean name="FirstFeedActivity">
        <variable name="chargetime" type="long"/> 首次充值时间
		<variable name="rebatetime" type="long"/> 领取时间
		<variable name="isgainaward" type="boolean" /> 是否已经参与过
	</xbean>
	<xbean name="FirstFeedActivityRole">
		<variable name="activities" type="map" key="int" value="FirstFeedActivity"/> key=活动id
	</xbean>
	<table name="firstfeedactivities" key="long" value="FirstFeedActivityRole" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

	<!--累计消费活动-->
	<xbean name="ConsumeActivity">
		<variable name="activityid" type="int"/> 在该id活动中
		<variable name="totalconsume" type="int"/> 消费的总数
		<variable name="isgainaward" type="map" key="int" value="boolean"/> 是否已经领取奖励 key=元宝数量
	</xbean>
	<xbean name="ConsumeActivityRole">
		<variable name="activities" type="map" key="int" value="ConsumeActivity"/> key=活动id
	</xbean>
	<table name="consumeactivities" key="long" value="ConsumeActivityRole" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

	<!--图鉴系统-->
	<xbean name="TuJianHero"> 所有曾经获得过的武将(图鉴)
		<variable name="heroId" type="int"/> 获得过的武将
		<variable name="flag" type="int"/> 是否满级，0未满，1满级
	</xbean>
	<xbean name="TuJianHeros"> 所有曾经获得过的武将(图鉴)
		<variable name="tujianbox" type="map" key="int" value="int"/> 宝箱获取（理论上有key则为已获取）
		<variable name="tujianhero" type="map" key="int" value="TuJianHero"/> 获得过的武将
		<variable name="tjheromaxlevel" type="list" value="int"/> 满级图鉴列表
	</xbean>
	<table name="tujianheros" key="long" value="TuJianHeros" lock="rolelock" cacheCapacity="10000" cachehigh="512" cachelow="256"/>
		

	<!--交易次数-->
	<xbean name="TradeNumLimit">
		<variable name="nums" type="map" key="int" value="int"/> 每个交易用过的次数 key-交易ID
	</xbean>
	<table name="tradenums" key="long" value="TradeNumLimit" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>
	
	<!--每天使用道具次数 每天清一次 -->
	<xbean name="ItemNumLimit">
		<variable name="itemnums" type="map" key="int" value="int"/> 每天使用道具次数s
	</xbean>
	<table name="itemlimits" key="long" value="ItemNumLimit" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>
	
	<xbean name="FirstLadderInfo">
		<variable name="startTime" type="long"/> 上一次登上天梯第一名的时间
		<variable name="zaiWeiMilSec" type="int"/> 本周在天梯第一名的总时间 单位：毫秒
	</xbean>
	<!--上过天梯排行第一名的用户信息，每周一清-->
	<xbean name="FirstLadderInfoRole">
		<variable name="roleInfos" type="map" key="long" value="FirstLadderInfo"/> key=roleId
	</xbean>
	<table name="firstladderinforole" key="int" value="FirstLadderInfoRole" cacheCapacity="1" cachehigh="512" cachelow="256"/>
	
	<xbean name="FriendReqs">
		<variable name="byMe" type="set" value="long"/> 我邀请的人
		<variable name="imBy" type="set" value="long"/> 邀请我的人
	</xbean>
	<table name="friendreqs" key="long" value="FriendReqs" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

	<xbean name="FriendInfo">
		<variable name="toTiliNum" type="int"/> 今日赠送给他体力次数
		<variable name="giveTiliNum" type="int"/> 今日给我体力次数
		<variable name="lastdaychangetime" type="long" default="0"/> 上次数据变动时间，为跨天清除用
	</xbean>
	<xbean name="Friends">
		<variable name="mine" type="map" key="long" value="FriendInfo"/> key=好友roldId
	</xbean>
	<table name="friends" key="long" value="Friends" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

	<xbean name="TurntableInfo">
		<variable name="tableId" type="int"/> 当前转盘ID
		<variable name="spaceItemcheck" type="set" value="int"/> 空的道具格
		<variable name="qualityNum" type="int"/> 当前已用精品抽奖次数
	</xbean>
	<table name="turntableinfo" key="long" value="TurntableInfo" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

	<xbean name="Helper">
		<variable name="roleId" type="long"/>
		<variable name="pos" type="short"/> 所在位置 0表示没有选中
	</xbean>
	<xbean name="Helpers">
		<variable name="helpers" type="list" value="Helper"/>
	</xbean>
	<!--所有活动的好友邀请情况-->
	<xbean name="AllHelpers">
		<variable name="allhelpers" type="map" key="int" value="Helpers"/> key=活动类型
	</xbean>
	<table name="allhelpers" key="long" value="AllHelpers" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

	<xbean name="MHelpers">
		<variable name="mhelpers" type="list" value="long"/>
	</xbean>
	<xbean name="AllMHelpers">
		<variable name="allmhelpers" type="map" key="int" value="MHelpers"/>
	</xbean>
	<table name="allmhelpers" key="long" value="AllMHelpers" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256" persistence="MEMORY"/>

	<xbean name="RaidBoss">
		<variable name="playId" type="int"/> 活动ID
		<variable name="bossId" type="int"/> 当前遇到BOSS的ID 跟战斗变化
		<variable name="bossLv" type="int" default="1"/> 当前BOSS等级 跟活动届数变化
		<variable name="bossTime" type="long"/> 遇到BOSS的时间
		<variable name="rongYao" type="int"/> 荣耀
	</xbean>
	<table name="raidboss" key="long" value="RaidBoss" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

	<xbean name="PlayProperties">
		<variable name="playJiFen" type="int"/>
		<variable name="exchangeInfo" type="map" key="int" value="int"/> key=兑换ID value=次数
	</xbean>
	<table name="playproperties" key="long" value="PlayProperties" cacheCapacity="10000" cachehigh="512" cachelow="256" lock="rolelock"/>

	<xbean name="RaidBossRank">
		<variable name="roleId" type="long"/>
		<variable name="rongyao" type="int"/>
	</xbean>
	<xbean name="RaidBossRanks">
		<variable name="rankers" type="list" value="RaidBossRank"/>
	</xbean>
	<table name="raidbossrank" key="int" value="RaidBossRanks" cacheCapacity="1" cachehigh="512" cachelow="256"/>

	<xbean name="CampRoleInfo">
		<variable name="playId" type="int"/>
		<variable name="campId" type="int"/> 所处阵营 0-无阵营
		<variable name="gongxun" type="int"/>
		<variable name="continuWin" type="int"/> 连胜次数
		<variable name="enemyRoleId" type="long"/>
		<variable name="enemyCampId" type="int"/> 攻击的阵营
		<variable name="lastdaychangetime" type="long" default="0"/> 上次数据变动时间，为跨天清除用
		<variable name="attackNum" type="int"/> 今天已使用次数
	</xbean>
	<table name="camproleinfos" key="long" value="CampRoleInfo" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>
	
	<xbean name="CampInfo">
		<variable name="jifen" type="int" default="10000"/> 阵营积分
		<variable name="pkroles" type="list" value="long"/> 用于推荐给别人PK的人(每个阵营都包含了没阵营的人)
		<variable name="roles" type="list" value="long"/> 阵营的人
	</xbean>
	<table name="campinfo" key="int" value="CampInfo" cacheCapacity="3" cachehigh="512" cachelow="256"/>

	<xbean name="CampRank">
		<variable name="roleId" type="long"/>
		<variable name="gongxun" type="int"/>
	</xbean>
	<xbean name="CampRanks">
		<variable name="rankers" type="list" value="CampRank"/>
	</xbean>
	<table name="camprank" key="int" value="CampRanks" cacheCapacity="1" cachehigh="512" cachelow="256"/>

	<xbean name="InviteInfo">
		<variable name="inviteMe" type="long"/> 邀请我的人
		<variable name="amInvites" type="list" value="long"/> 我邀请的人
		<variable name="awardNum" type="int"/> 领奖到第几人了
	</xbean>
	<table name="inviteinfo" key="long" value="InviteInfo" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

	<xbean name="ShareInfo">
		<variable name="infos" type="map" key="int" value="int"/> key=皮肤ID value=分享次数
	</xbean>
	<table name="shareinfo" key="long" value="ShareInfo" lock="rolelock" cacheCapacity="3000" cachehigh="512" cachelow="256"/>

  <TableSysConf name="_sys_" cacheCapacity="1" cachehigh="512" cachelow="256"/>
	<UniqNameConf localId="UN_LOCAL_ID">
		<XioConf name="xdb.util.UniqName">
			<Manager name="Client" keepSize="1" maxSize="1">
				<Coder>
					<Rpc class="xdb.util.UniqName$Allocate"/>
					<Rpc class="xdb.util.UniqName$Confirm"/>
					<Rpc class="xdb.util.UniqName$Release"/>
					<Rpc class="xdb.util.UniqName$Exist"/>
					<Rpc class="xdb.util.UniqName$AllocateId"/>
					<Rpc class="xdb.util.UniqName$ReleaseId"/>
				</Coder>
				<Connector remoteIp="UN_SERVER_IP" remotePort="eval(STARTPORT+32)" sendBufferSize="16384" receiveBufferSize="16384" tcpNoDelay="true" inputBufferSize="131072" outputBufferSize="131072"/>
			</Manager>
		</XioConf>
	</UniqNameConf>  
</xdb>