package chuhan.gsp.attr;

import java.util.HashMap;







import com.pwrd.op.LogOpChannel;

import xdb.Transaction;
import chuhan.gsp.DataInit;
import chuhan.gsp.MsgType;
import chuhan.gsp.SRefreshSweep;
import chuhan.gsp.SShowedBeginnerTips;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.game.vip39;
import chuhan.gsp.item.SRefreshVipBuyInfo;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.log.Logger;
import chuhan.gsp.log.OpLogManager;
import chuhan.gsp.log.RemoteLogID;
import chuhan.gsp.log.RemoteLogParam;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.lottery.CLottery;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.LogUtil;

public class PropRole {
	
	public static final int FIRST_CHARGE_VIP_LV = 3;
	public static final long SHOP_REFRESH_TIME = -3*60*60*1000;	//每天商店购买刷新时间凌晨三点
	public static final long SIGN_REFRESH_TIME = -3*60*60*1000;	//每月签到刷新时间凌晨三点	
	public static final long REALTIME_REFRESH_TIME = -3*60*60*1000;	//实时战斗计数刷新时间
	public static Logger logger = Logger.getLogger(PropRole.class);
	
	public static PropRole getPropRole(long roleId,boolean readonly)
	{
		if(roleId <= 0)
			return null;
		xbean.Properties xprop;
		if(readonly)
			xprop = xtable.Properties.select(roleId);
		else
			xprop = xtable.Properties.get(roleId);
		
		if(xprop == null)
			return null;
		
		return new PropRole(roleId, xprop, readonly);
	}
	
	private final long roleId;
	private final xbean.Properties xprop;
	public final boolean readonly;
	
	private PropRole(long roleId, xbean.Properties xprop, boolean readonly)
	{
		this.roleId = roleId;
		this.xprop = xprop;
		this.readonly = readonly;
//		processData();
	}
	
	public void processData()
	{
		long now = GameTime.currentTimeMillis();
		timeOverDay(now);
	}
	
	public long getRoleId()
	{
		return roleId;
	}
	
	public xbean.Properties getProperties()
	{
		return xprop;
	}
	
	public int getLevel()
	{
		return xprop.getLevel();
	}
	
	public player03 getLevelConfig()
	{
		return ConfigManager.getInstance().getConf(player03.class).get(getLevel());
	}
	
	public int getVipLevel()
	{
		return xprop.getViplv();
	}
	
	public int setVipLevel(int lv)
	{
//		if(lv < 0 || lv > 20)
//			return xprop.getViplv();
		xprop.setViplv(lv);
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		this.refreshSweep(now);
//		xdb.Procedure.psendWhileCommit(roleId, new SRefreshVipLevel(Conv.toByte(xprop.getViplv())));
		
		return xprop.getViplv();
	}
	
	/**
	 * 增加vip经验
	 * @param num
	 * @param isBuy
	 */
	public void addVipExp(double num, int addType){
		vip39 vipInit = this.getVipInit();
		if(vipInit.getVipExp() == -1){
			return;
		}
		int addexp = 0;
		if(addType == 1){	//充值
			int ratio = Integer.parseInt(ConfigManager.getInstance().
					getConf(config10.class).get(1112).configvalue);
			addexp = Math.abs((int)( num * 100.0f / ratio));
		}else if(addType == 2){		//消耗元宝
			int ratio = Integer.parseInt(ConfigManager.getInstance().
					getConf(config10.class).get(1113).configvalue);
			addexp = Math.abs((int)(num / ratio));
		}else{				//直接加经验
			addexp = (int)num;
		}
		xprop.setVipexp(xprop.getVipexp() + addexp);
		boolean isSend = false;
		while( xprop.getVipexp() >= vipInit.getVipExp() ){
			isSend = true;
			xprop.setVipexp( xprop.getVipexp() - vipInit.getVipExp() );
			this.setVipLevel(this.getVipLevel() + 1);
			vipInit = this.getVipInit();
			if(vipInit.getVipExp() == -1){
				break;
			}
		}
		if(isSend){
			//跑马灯
			ActivityManager.getInstance().addMsgNotice(roleId,0,ActivityManager.VIPLEVELUP,"");
		}
		xdb.Procedure.psendWhileCommit(roleId, new SRefreshVipLevel(
				Conv.toByte(xprop.getViplv()),xprop.getVipexp()));
//		}
		return;	
	}
	

	
	public int getExp()
	{
		return xprop.getExp();
	}
	
	public void setExp(int v)
	{
		xprop.setExp(v);
	}
	
	public int getYuanBao()
	{
		return xprop.getYuanbao();
	}
	
	
	/**
	 * 增加元宝
	 * @param v 只能为正
	 * @param reason 参考YuanBaoAddType
	 * @param hint 其他参数
	 * @return
	 */
	public int addYuanBao(int v, int reason, Object hint)
	{
		if(v <= 0)
			return 0;
		
		int r = addYuanBao(v);
		if(r == 0)
			return r;
		try{//do log
			java.util.Map<String, Object> params = LogUtil.putRoleBasicParams(roleId, false, new HashMap<String, Object>());
			params.put(RemoteLogParam.TYPEINFO, reason);
			params.put(RemoteLogParam.YUANBAO, v);
			params.put(RemoteLogParam.HINT, hint);
			LogManager.getInstance().doLogWhileCommit(RemoteLogID.ADDYUANBAO, params);
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		return r;
	}
	
	/**
	 * 增加元宝
	 * @param v 只能为正
	 * @param reason 参考YuanBaoAddType
	 * @return
	 */
	public int addYuanBao(int v, int reason)
	{
		if(v <= 0)
			return 0;
		
		int r = addYuanBao(v);
		if(r == 0)
			return r;
		try{//do log
			java.util.Map<String, Object> params = LogUtil.putRoleBasicParams(roleId, false, new HashMap<String, Object>());
			params.put(RemoteLogParam.TYPEINFO, reason);
			params.put(RemoteLogParam.YUANBAO, v);
			LogManager.getInstance().doLogWhileCommit(RemoteLogID.ADDYUANBAO, params);
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		return r;
	}
	
	/**
	 * 减少元宝
	 * @param v 只能为负
	 * @param reason 参考YuanBaoConsumeType
	 * @return
	 */
	public int delYuanBao(int v, int reason)
	{
		if(v >= 0)
			return 0;
		int r = addYuanBao(v);
		if(r == 0)
			return r;
		try{//do log
			PropRole propRole = PropRole.getPropRole(roleId, true);
			if(null != propRole) {
				OpLogManager.getInstance().doLogWhileCommit(LogOpChannel.COST, roleId, GameTime.currentTimeMillis(),
						DateUtil.getCurrentStringFormatEn(GameTime.currentTimeMillis()), 
						reason, null, null, 0, v, 0, propRole.getProperties().getUsername(),
						propRole.getProperties().getRolename(), propRole.getProperties().getLevel());
			}
			
			java.util.Map<String, Object> params = LogUtil.putRoleBasicParams(roleId, false, new HashMap<String, Object>());
			params.put(RemoteLogParam.TYPEINFO, reason);
			params.put(RemoteLogParam.YUANBAO, -v);
			LogManager.getInstance().doLogWhileCommit(RemoteLogID.COSTYUANBAO, params);
			
//			PConsumeActivity.process(roleId, Math.abs(r));
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		this.addVipExp(r, 2);
		//消费体力相关活动数据统计
		ActivityGameManager.getInstance().addXFActivity(roleId, r, ActivityGameManager.XF_YUANBAO);
		return r;
	}
	
	/**
	 * 元宝变化
	 * @param v
	 * @return
	 */
	private int addYuanBao(int v)
	{
		if(v == 0)
			return 0;
		if((xprop.getYuanbao() + v) < 0)
		{
			xdb.Procedure.psend(roleId, new SYuanbaoNotEnough());
			//Message.psendMsgNotify(roleId, 108);
			return 0;
		}
		
		xprop.setYuanbao(xprop.getYuanbao() + v);
		
		// 刷新元宝数量
		xdb.Procedure.psendWhileCommit(roleId, new SRefreshYuanBao(xprop.getYuanbao())); 
		return v;
	}
	
	public int addGold(int v, int reason)
	{
		if(v < 0)
			return 0;
		
		int r = addGold(v);
		if(r == 0)
			return r;
		
		return r;
	}
	public int delGold(int v, int reason)
	{
		if(v > 0)
			return 0;
		int r = addGold(v);
		if(r == 0)
			return r;
		
		return r;
	}
		
	private int addGold(int v)
	{
		if(v == 0)
			return 0;
		if((xprop.getGold() + v) < 0)
		{
//			Message.psendMsgNotify(roleId, 135);
			return 0;
		}
		xprop.setGold(xprop.getGold() + v);
		xdb.Procedure.psendWhileCommit(roleId, new SRefreshGold(xprop.getGold()));
		return v;
	}
	
	
	public int addZiYuan(int v, int reason,int zyType){
		if(v < 0)
			return 0;	
		int r = addZiYuan(v,zyType);
		if(r == 0)
			return r;
		return r;
	}
	public int delZiYuan(int v, int reason,int zyType){
		if(v > 0)
			return 0;
		int r = addZiYuan(v,zyType);
		if(r == 0)
			return r;
		return r;
	}	
	private int addZiYuan(int v,int zyType){
		if(v == 0)
			return 0;
		switch(zyType){
		case IDManager.SHENGLINGZQ:
			return this.addShengLingzq(v);
		case IDManager.RONGLIAN:
			return this.addRolelian(v);
		case IDManager.HUANGJINXZ:
			return this.addHuangJinxz(v);
		case IDManager.BAIJINXZ:
			return this.addBaiJinxz(v);
		case IDManager.QINGTONGXZ:
			return this.addQingTongxz(v);
		case IDManager.CHITIEXZ:
			return this.addQingTongxz(v);
		case IDManager.EXPJIEJING:
			return this.addExpjiejing(v);
		case IDManager.GOLD:
			return this.addGold(v);
		}
		return 0;
	}
	
	private void refreshZiYuan(){
		SRefreshZiYuan psend = new SRefreshZiYuan();
		psend.shenglingzq = this.getProperties().getShenglingzq();
		psend.ronglian = this.getProperties().getRonglian();
		psend.huangjinxz = this.getProperties().getHuangjinxz();
		psend.baijinxz = this.getProperties().getBaijinxz();
		psend.qingtongxz = this.getProperties().getQingtongxz();
		psend.chitiexz = this.getProperties().getChitiexz();
		psend.jyjiejing = this.getProperties().getJyjiejing();
		xdb.Procedure.psendWhileCommit(roleId, psend);
	}
	
	private int addShengLingzq(int v){
		if(v == 0)
			return 0;
		if((xprop.getShenglingzq() + v) < 0){
			Message.psendMsgNotify(roleId, 135);
			return 0;
		}
		xprop.setShenglingzq(xprop.getShenglingzq() + v);
		refreshZiYuan();
		return v;
	}
	
	private int addRolelian(int v){
		if(v == 0)
			return 0;
		if((xprop.getRonglian() + v) < 0){
//			xdb.Procedure.psend(roleId, new SErrorType(ErrorType.ERR_NOT_ENOUGH_SHENGLINGZQ));
			return 0;
		}
		xprop.setRonglian(xprop.getRonglian() + v);
		refreshZiYuan();
		return v;
	}
	
	private int addHuangJinxz(int v){
		if(v == 0)
			return 0;
		if((xprop.getHuangjinxz() + v) < 0){
//			xdb.Procedure.psend(roleId, new SErrorType(ErrorType.ERR_NOT_ENOUGH_SHENGLINGZQ));
			return 0;
		}
		xprop.setHuangjinxz(xprop.getHuangjinxz() + v);
		refreshZiYuan();
		return v;
	}
	
	private int addBaiJinxz(int v){
		if(v == 0)
			return 0;
		if((xprop.getBaijinxz() + v) < 0){
//			xdb.Procedure.psend(roleId, new SErrorType(ErrorType.ERR_NOT_ENOUGH_SHENGLINGZQ));
			return 0;
		}
		xprop.setBaijinxz(xprop.getBaijinxz() + v);
		refreshZiYuan();
		return v;
	}
	
	private int addQingTongxz(int v){
		if(v == 0)
			return 0;
		if((xprop.getQingtongxz() + v) < 0){
//			xdb.Procedure.psend(roleId, new SErrorType(ErrorType.ERR_NOT_ENOUGH_SHENGLINGZQ));
			return 0;
		}
		xprop.setQingtongxz(xprop.getQingtongxz() + v);
		refreshZiYuan();
		return v;
	}
	
	private int addChiTiexz(int v){
		if(v == 0)
			return 0;
		if((xprop.getChitiexz() + v) < 0){
//			xdb.Procedure.psend(roleId, new SErrorType(ErrorType.ERR_NOT_ENOUGH_SHENGLINGZQ));
			return 0;
		}
		xprop.setChitiexz(xprop.getChitiexz() + v);
		refreshZiYuan();
		return v;
	}
	
	private int addExpjiejing(int v){
		if(v == 0)
			return 0;
		if((xprop.getJyjiejing() + v) < 0){
//			xdb.Procedure.psend(roleId, new SErrorType(ErrorType.ERR_NOT_ENOUGH_SHENGLINGZQ));
			return 0;
		}
		xprop.setJyjiejing(xprop.getJyjiejing() + v);
		refreshZiYuan();
		return v;
	}
	

	//本级最大经验值
	public int getExpMax(){
		return ConfigManager.getInstance().getConf(player03.class).get(getLevel()).exp;
	}
	
	public boolean levelUp()
	{
		final int nexp = this.getExpMax();
		if (xprop.getExp() < nexp)
			return false;
/*		if (getLevel() >= DataInit.ROLE_LEVEL_MAX){
			LogManager.logger.error("玩家超过等级上限。roleid："+getRoleId()+"level:"+getLevel()+"maxLevel:"+DataInit.ROLE_LEVEL_MAX);
			return false;
		}*/
		
		
		
//		Message.broadcastMsgNotifyWithDelay(5000, MsgType.MSG_ROLE_LEVEL,getProperties().getRolename(),String.valueOf(xprop.getLevel()));
		
		player03 initplayer = ConfigManager.getInstance().getConf(player03.class).get(getLevel() + 1);
		if(initplayer != null){
			addTili(initplayer.getApRecover());
			xprop.setExp(xprop.getExp()-nexp);
			xprop.setLevel(xprop.getLevel() + 1);
/*			if(xprop.getLevel() >= 300){
				System.out.println(xprop.getLevel());
			}*/
		}else{
			return false;
		}
		
//		fullTili();
		//回活力体力
		//addHuoli(5);
		//addTili(5);
		/*		
		if(getLevel() == 5)
		{
			if(getVipLevel() < 2)
				setVipLevel(2);
		}
		else if(getLevel() == 10)
		{
			xdb.Procedure.pexecuteWhileCommit(new PBeginLadder(roleId));//开启天梯玩法
		}
		*/
		
//		addYuanBao(10);
		logLevelUp();
		
		//登录活动统计数据
		ActivityGameManager.getInstance().addRoleLevelActivity(roleId,this.getLevel());
		
		return true;
	}
	
	private void logLevelUp()
	{
		try {
			PropRole propRole = PropRole.getPropRole(roleId, true);
			if(null != propRole) {
				OpLogManager.getInstance().doLogWhileCommit(LogOpChannel.UPGRADE, roleId, 
						propRole.getProperties().getRolename(), propRole.getProperties().getLevel(),
						GameTime.currentTimeMillis(), DateUtil.getCurrentStringFormatEn(GameTime.currentTimeMillis()),
						propRole.getProperties().getUsername());
			}
			
			java.util.Map<String, Object> params = LogUtil.putRoleBasicParams(roleId, false, new HashMap<String, Object>());
			LogManager.getInstance().doLogWhileCommit(RemoteLogID.LEVELUP, params);
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	
	/*
	public int getYbNum()
	{
		int num = ConfigManager.getInstance().getConf(CJMain.class).get(CLottery.yb).getPeoplenum();
		int result = num - xprop.getCjybnum();
		if(result < 0)
			result = 0;
		return result;
	}
	*/
	
	public int addTXTili(int v){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int oldti = getTXTi(now);
		int newti = setTXTili(getTXTi(now)+v, now);
		return newti - oldti;
	}
	public int setTXTili(int v, long now){
		if(v < 0)
			return xprop.getTanxianti();
		int max = getMaxTXTi();
		xprop.setTanxianti(v);
		xprop.setTanxiantitime(now);
		
		refreshTili(now);
		return v;
	}
	public int fullTXTili(){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int max = getMaxTXTi();
		if(xprop.getTanxianti() < max){
			setTXTili(max,now);
		}
		return xprop.getTanxianti();
	}
	public int getTXTi(long now) {
		if (xprop.getTanxianti() >= this.getMaxTXTi()) {
			return xprop.getTanxianti();
		}
		int tirecovery = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1014).configvalue) * 1000;
		long addtili = (now - xprop.getTanxiantitime()) / tirecovery;
		if (xprop.getTanxianti() + addtili >= getMaxTXTi()) {
			xprop.setTanxianti(getMaxTXTi());
			xprop.setTanxiantitime(now);
		} else {
			xprop.setTanxianti(xprop.getTanxianti() + (int) addtili);
			xprop.setTanxiantitime(xprop.getTanxiantitime() + addtili * tirecovery);
		}
		return xprop.getTanxianti();
	}
	public int getTXTiTime(long now) {
		int ti = getTXTi(now);
		if (xprop.getTanxiantitime() == 0 || ti == getMaxTXTi())
			return 0;
		int tirecovery = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1014).configvalue) * 1000;
		long tiTime = (tirecovery - (now - xprop.getTanxiantitime())) / 1000;
		return (int) tiTime;
	}
	public boolean useTXTili(int usenum){
		if(usenum > 0)
			usenum = usenum*(-1);
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int tilihave = getTXTi(now);
		if(tilihave + usenum >= 0){
			xprop.setTanxianti(tilihave + usenum);
			if(tilihave >= getMaxTXTi() || xprop.getTanxiantitime() == 0){
				xprop.setTanxiantitime(now);
			}
		}else{
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		if(usenum != 0){
			refreshTili(now);
		}
		
		return true;
	}
	public int getTili(long now) {
		if (xprop.getTi() >= getMaxTili()) {
			return xprop.getTi();
		}
		int tirecovery = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1004).configvalue) * 1000;
		long addtili = (now - xprop.getTichangetime()) / tirecovery;
		if (xprop.getTi() + addtili >= getMaxTili()) {
			xprop.setTi(getMaxTili());
			xprop.setTichangetime(now);
		} else {
			xprop.setTi(xprop.getTi() + (int) addtili);
			xprop.setTichangetime(xprop.getTichangetime() + addtili * tirecovery);
		}
		return xprop.getTi();
	}
	
	public int getTiTime(long now) {
		int ti = getTili(now);
		if (xprop.getTichangetime() == 0 || ti == getMaxTili())
			return 0;
		int tirecovery = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1004).configvalue) * 1000;
		long tiTime = (tirecovery - (now - xprop.getTichangetime())) / 1000;
		return (int) tiTime;
	}
	
	public boolean useTili(int usenum){
		if(usenum > 0)
			usenum = usenum*(-1);
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int tilihave = getTili(now);
		if(tilihave + usenum >= 0)
		{
			xprop.setTi(tilihave + usenum);
			if(tilihave >= getMaxTili() || xprop.getTichangetime() == 0)
			{
				xprop.setTichangetime(now);
			}
		}
		else
		{
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		if(usenum != 0)
		{
			refreshTili(now);
		}
		//消费体力相关活动数据统计
		ActivityGameManager.getInstance().addXFActivity(roleId, usenum, ActivityGameManager.XF_TILI);
		
		return true;
	}
	
	public void refreshTili(long now){
		SRefreshTili snd = new SRefreshTili();
		snd.tili = getTili(now);
		snd.titime = getTiTime(now);
		snd.xingdongti = this.getTXTi(now);
		snd.xingdongtitime = this.getTXTiTime(now);
		snd.jineng = this.getJinengdian(now);
		snd.jinengtime = this.getJinengdianTime(now);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	
	public int addJinengdian(int v){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int old = getJinengdian(now);
		int newNum = setJinengdian(getJinengdian(now)+v, now);
		return newNum - old;
	}
	
	public int setJinengdian(int v, long now){

		if(v < 0)
			return xprop.getJinengdian();
		int max = getMaxJinengdian();
		if(v > max)
			v = max;
		xprop.setJinengdian(v);
		xprop.setJinengdiantime(now);
		
		refreshTili(now);
//		xdb.Procedure.psendWhileCommit(roleId, new SRefreshTili(Conv.toByte(v)));
		return v;
	}
	
	public int getJinengdian(long now) {
		if (xprop.getJinengdian() >= getMaxJinengdian()) {
			xprop.setJinengdian(getMaxJinengdian());
			return xprop.getJinengdian();
		}
		int tirecovery = 300*1000;
		vip39 vipinit = this.getVipInit();
		if(vipinit != null){
			tirecovery = vipinit.getReSkillTime() * 1000;
		}
//		int tirecovery = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1004).configvalue) * 1000;
		long addjinengdian = (now - xprop.getJinengdiantime()) / tirecovery;
		if (xprop.getJinengdian() + addjinengdian >= getMaxJinengdian()) {
			xprop.setJinengdian(getMaxJinengdian());
			xprop.setJinengdiantime(now);
		} else {
			xprop.setJinengdian(xprop.getJinengdian() + (int) addjinengdian);
			xprop.setJinengdiantime(xprop.getJinengdiantime() + addjinengdian * tirecovery);
		}
		return xprop.getJinengdian();
	}
	
	public int getJinengdianTime(long now) {
		int jinengdian = getJinengdian(now);
		if (xprop.getJinengdiantime() == 0 || jinengdian == getMaxJinengdian())
			return 0;
		int tirecovery = 300*1000;
		vip39 vipinit = this.getVipInit();
		if(vipinit != null){
			tirecovery = vipinit.getReSkillTime() * 1000;
		}
//		int tirecovery = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1004).configvalue) * 1000;
		long Time = (tirecovery - (now - xprop.getJinengdiantime())) / 1000;
		return (int) Time;
	}
	
	public boolean useJinengdian(int usenum){
		if(usenum > 0)
			usenum = usenum*(-1);
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int have = getJinengdian(now);
		if(have + usenum >= 0)
		{
			xprop.setJinengdian(have + usenum);
			if(have >= getMaxJinengdian() || xprop.getJinengdiantime() == 0)
			{
				xprop.setJinengdiantime(now);
			}
		}
		else
		{
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		if(usenum != 0)
		{
			refreshTili(now);
		}
		return true;
	}
	public int getMaxJinengdian(){

		return 20;
		/*try{
		int result = ConfigManager.getInstance().getConf(player03.class).get(getLevel()).extraAp +
				Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1002).configvalue);
		vip39 vipinit = this.getVipInit();
		if(vipinit != null){
			result += vipinit.getExtraAp();
		}
		return result;
		}catch(Exception e){
			return 0;
		}*/
	}
	
	
	/**
	 * 刷新扫荡等数据
	 * @param now
	 */
	public void refreshSweep(long now){
		SRefreshSweep snd = new SRefreshSweep();
		snd.sweephavenum = getSweepHaveNum(now);
		snd.sweepbuyhavenum = getSweepHaveBuyNum(now);
		snd.mszqgetnum = this.getProperties().getMszqgetnum();
		snd.qiyuannum = this.getProperties().getQiyuannum();
		snd.qiyuanallnum = this.getProperties().getQiyuanallnum();
		snd.isqiyuantoday = this.isQiyuanToday(now);
		if( !DateUtil.isTodayOrYestoday(now,this.getProperties().getQiyuantime()) ){
			snd.isqiyuantoday += 10;
		}
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 今日是否祈愿过
	 * @param now
	 * @return
	 */
	public int isQiyuanToday(long now){
		if(DateUtil.inTheSameDay(now, this.getProperties().getQiyuantime()))
			return 1;
		return 0;
	}
	/**
	 * 获得最大体力数
	 * @return
	 */
	public int getMaxTili(){
		try{
		int result = ConfigManager.getInstance().getConf(player03.class).get(getLevel()).extraAp +
				Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1002).configvalue);
		vip39 vipinit = this.getVipInit();
		if(vipinit != null){
			result += vipinit.getExtraAp();
		}
		return result;
		}catch(Exception e){
			return 0;
		}
	}
	/**
	 * 获取最大探险行动力数
	 * @return
	 */
	public int getMaxTXTi(){
		try{
		int result = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1013).configvalue);
		vip39 vipinit = this.getVipInit();
		if(vipinit != null){
			result += vipinit.getExtraEp();
		}
		return result;
		}catch(Exception e){
			return 0;
		}
	}
	
	/**
	 * 获得最大英雄背包数
	 * @return
	 */
	public int getMaxHeroSize(){
		try{
		int result = ConfigManager.getInstance().getConf(player03.class).get(getLevel()).extraHeroPackset +
				Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1007).configvalue) +
				Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1009).configvalue) * xprop.getBuyherobagnum();
		return result;
		}catch(Exception e){
			return 0;
		}
	}
	
	/**
	 * 获得最大物品背包数
	 * @return
	 */
	public int getMaxItemSize(){
		try{
		int result = ConfigManager.getInstance().getConf(player03.class).get(getLevel()).extraCommonItemPackset +
				Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1008).configvalue) + 
				Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1010).configvalue) * xprop.getBuybagnum();
		return result;
		}catch(Exception e){
			return 0;
		}
	}
	
	/**
	 * 获取人物相关VIP配表
	 * @return
	 */
	public vip39 getVipInit(){
		try{
			return ConfigManager.getInstance().getConf(vip39.class).get(this.getVipLevel());
		}catch(Exception e){
			return null;
		}
	}
	
	/**
	 * 获取扫荡剩余数量
	 * @param now
	 * @return
	 */
	public int getSweepHaveNum(long now){
		timeOverDay(now);
		vip39 vipinit = getVipInit();
		if(vipinit == null){
			return 0;
		}
		int result = vipinit.getRapidClearNums() - this.getProperties().getSweepnum();
		if(result < 0){
			return 0;
		}
		return result;
	}
	/**
	 * 消耗一次扫荡
	 * @param now
	 * @return
	 */
	public boolean useSweep(long now){
		if( getSweepHaveNum(now) > 0 ){
			this.getProperties().setSweepnum(this.getProperties().getSweepnum() + 1);
			this.refreshSweep(now);
			return true;
		}
		return false;
	}
	
	/**
	 * 获取购买关卡次数最大次数（根据VIP）
	 * @param now
	 * @return
	 */
	public int getBuyBattleMaxNum(){
		vip39 vipinit = getVipInit();
		if(vipinit == null){
			return 0;
		}
		int result = vipinit.getStageResetBuyTimes();
		if(result < 0){
			return 0;
		}
		return result;
	}
	
	/**
	 * 扫荡数据判断重置
	 * @param now
	 */
	public void timeOverDay(long now){
		if( !DateUtil.inTheSameDay(this.getProperties().getTodaylasttime(), now) ){
			this.getProperties().setTodaylasttime(now);
			this.getProperties().setSweepnum(0);
			this.getProperties().setSweepbuynum(0);
			this.getProperties().setMszqgetnum(0);
			this.refreshSweep(now);
		}
		//祈愿台当满了时重置为0(当天不重置)
		if( !DateUtil.inTheSameDay(now, this.getProperties().getQiyuantime())){	
			if(this.getProperties().getQiyuannum() == this.getProperties().getQiyuanallnum()){
				this.getProperties().setQiyuanallnum(ActivityManager.getInstance().QIYUAN_MAX);
				this.getProperties().setQiyuannum(0);
			}
			if( !DateUtil.isYestoday(now, this.getProperties().getQiyuantime()) ){
				this.getProperties().setQiyuannum(0);
			}
		}
		//连续登陆签到处理
		if( !DateUtil.inTheSameDay(now, this.getProperties().getSigntime()) ){
			ActivityManager.getInstance().loginNumAdd(this, now);
		}
	}
	
	/**
	 * 获取扫荡购买剩余次数
	 * @param now
	 * @return
	 */
	public int getSweepHaveBuyNum(long now){
		timeOverDay(now);
		vip39 vipinit = getVipInit();
		if(vipinit == null){
			return 0;
		}
		int result = vipinit.getRapidClearBuyTimes() - this.getProperties().getSweepbuynum();
		if(result < 0){
			return 0;
		}
		return result;
	}
	/**
	 * 消耗一次购买扫荡
	 * @param now
	 * @return
	 */
	public boolean useSweepBuy(long now){
		if( getSweepHaveBuyNum(now) > 0 ){
			this.getProperties().setSweepnum(0);
			this.getProperties().setSweepbuynum(this.getProperties().getSweepbuynum() + 1);
			this.refreshSweep(now);
			return true;
		}
		return false;
	}
	
	
	
	
	public int addTili(int v){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int oldti = getTili(now);
		int newti = setTili(getTili(now)+v, now);
		return newti - oldti;
	}
	
	public int setTili(int v, long now){
		/*int max = getMaxTili();
		if(v > max)
			v = max;*/
		if(v < 0)
			return xprop.getTi();
		int max = getMaxTili();
//		if(v > max)
//			v = max;
		xprop.setTi(v);
		xprop.setTichangetime(now);
		
		refreshTili(now);
//		xdb.Procedure.psendWhileCommit(roleId, new SRefreshTili(Conv.toByte(v)));
		return v;
	}
	
	public int fullTili(){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		int max = getMaxTili();
		if(xprop.getTi() < max)
		{
			setTili(max,now);
//			xprop.setTi(getMaxTili());
//			xprop.setTichangetime(now);
//			xdb.Procedure.psendWhileCommit(roleId,new SRefreshTili(Conv.toByte(xprop.getTi())));
		}
		return xprop.getTi();
	}
	
	public int GetGoldBuyNum(long now)
	{
		int num = 0;
		if(DateUtil.inTheSameDay(now, xprop.getGoldbuytime(), SHOP_REFRESH_TIME))
		{
			num = xprop.getGoldbuynum();
		}
		else
		{
			num = 0;
			xprop.setGoldbuynum(num);
			xprop.setGoldbuytime(now);
		}
		return num;
	}
	public void addGoldBuyNum()
	{
		xprop.setGoldbuynum(xprop.getGoldbuynum() +1);
	}
	
	public int GetTiBuyNum(long now)
	{
		int num = 0;
		if(DateUtil.inTheSameDay(now, xprop.getTibuytime(), SHOP_REFRESH_TIME))
		{
			num = xprop.getTibuynum();
		}
		else
		{
			num = 0;
			xprop.setTibuynum(num);
			xprop.setTibuytime(now);
		}
		return num;
	}
	public void addTiBuyNum()
	{
		xprop.setTibuynum(xprop.getTibuynum() + 1);
	}
	
/*	public int GetSignNum(long now)
	{
		int num = 0;
		if(DateUtil.inTheSameMonth(now, xprop.getSigntime(), SIGN_REFRESH_TIME)){
			num = xprop.getSignnum();
		}else{
			num = 0;
			xprop.setSignnum(num);
		}
		return num;
	}
	public void addSignNum(long now)
	{
		xprop.setSignnum(xprop.getSignnum() + 1);
		xprop.setSigntime(now);
	}*/
	public boolean isTodaySigned(long now)
	{
		return DateUtil.inTheSameDay(now, xprop.getSigntime(), SIGN_REFRESH_TIME);
	}
	
	/**
	 * 神秘关卡几率（万分之）
	 * @return
	 */
	public int getSMGuankaAdd(){
		try{
			float base = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1101).configvalue) * 10000l;
			float add = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1115).configvalue) * 10000l;
			int result = (int)base + (int)add * xprop.getSmguanka_nocome();
			return result;
		}catch(Exception e){
			e.printStackTrace();
			return 0;
		}
	}
	/**
	 * 神秘商店几率（万分之）
	 * @return
	 */
	public int getSMShopAdd(){
		try{
			float base = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1100).configvalue) * 10000l;
			float add = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1114).configvalue) * 10000l;
			int result = (int)base + (int)add * xprop.getSmshop_notcome();
			return result;
		}catch(Exception e){
			e.printStackTrace();
			return 0;
		}
	}
	/**
	 * 是否有神秘关卡或商店
	 * @param guankaId
	 * @return
	 */
	public boolean isHaveSM(int guankaId){
//		return false;///return测试用
//		/*
		try{
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		if(xprop.getBattlenum() != 0){
			
			if( (now - xprop.getSmendtime()) > 0){
				xprop.setBattlenum(0);
				xprop.setSmendtime(0);
				xprop.getSmshop().clear();
				return false;
			}else{
				if(xprop.getBattlenum() == guankaId){
					if(IDManager.getInstance().getIdBegin(xprop.getBattlenum()) == IDManager.BEGIN_BATTLE_MOSTER){
						xprop.setBattlenum(0);
						xprop.setSmendtime(0);
						xprop.getSmshop().clear();
					}
				}
				return true;
			}	
		}
		return false;
		}catch(Exception e){
			e.printStackTrace();
			return false;
		}
//		*/
	}
	
	
	
	
	public void addTips(int tipid)
	{
//		xprop.getAlreadytips().add(tipid);
	}
	
	public void sendTips()
	{
		SShowedBeginnerTips snd = new SShowedBeginnerTips();
//		for(int tipid : xprop.getAlreadytips())
//			snd.tipid.add(Conv.toByte(tipid));
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 好友是否到达上限
	 * @return
	 */
	public boolean friendIsFull() {
//		if(xprop.getFriendnum() < (20 + getLevel() / 5)) {
//			return false;
//		}
		
		return true;
	}
	
	/**
	 * 添加背包扩充次数
	 * @param v
	 * @param bagType
	 * @return
	 */
	public short addBagExpNum(short v,byte bagType){
		
		if(v == 0)
			return 0;
		
		if(bagType < 0){
			Message.psendMsgNotify(roleId, 135);
			return 0;
		}
		if(bagType == 1){
			xprop.setBuybagnum(Conv.toShort(xprop.getBuybagnum() + v));
			xdb.Procedure.psendWhileCommit(roleId, new SRefreshBagExp(xprop.getBuybagnum(), bagType));
		}else{
			xprop.setBuyherobagnum(Conv.toShort(xprop.getBuyherobagnum() + v));
			xdb.Procedure.psendWhileCommit(roleId, new SRefreshBagExp(xprop.getBuyherobagnum(), bagType));
		}
		
		return v;
		
	}
	
	public short getBagExpNum(){
		return xprop.getBuybagnum();
	}
	
}
