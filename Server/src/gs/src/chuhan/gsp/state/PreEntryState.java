
package chuhan.gsp.state;

import java.util.Calendar;

import chuhan.gsp.PlatformTypeStr;
import chuhan.gsp.SEnterWorld;
import chuhan.gsp.SQihoo360ExtraInfo;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.CompenseRole;
import chuhan.gsp.exchange.ChargeRole;
import chuhan.gsp.gm.GMInterface;
import chuhan.gsp.hero.HeroArtifactColumn;
import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.hero.HeroSkinColumn;
import chuhan.gsp.hero.PSelectHero;
import chuhan.gsp.hero.TroopColumn;
import chuhan.gsp.item.Bag;
import chuhan.gsp.item.EquipColumn;
import chuhan.gsp.item.SSendTodayRecoverd;
import chuhan.gsp.mail.MailColumn;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.endlessbattle.EndlessinfoColumns;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.play.lottery.LotteryColumns;
import chuhan.gsp.play.lotteryitem.LotteryItemColumns;
import chuhan.gsp.play.shop.ShopBuyColumn;
import chuhan.gsp.play.tanxian.TanXianColumns;
import chuhan.gsp.play.wordboss.PGetBossRank;
import chuhan.gsp.play.wordboss.Module;
import chuhan.gsp.stage.StageRole;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;



/**
 * 角色准备登录状态
 * 
 * @author DevUser
 * 
 */
public class PreEntryState extends State {

	public PreEntryState(long roleId) {

		super(roleId);
	}

	@Override
	public boolean enter(int trigger) {

		Integer oldstate = xtable.Roleonoffstate.get(roleId);
		if (oldstate == null)
			oldstate = State.UNENTRY_STATE;
		boolean valid = false;
		if (trigger == State.TRIGGER_ONLINE && oldstate == State.UNENTRY_STATE)
			valid = true;
		if (!valid)
		{
			//yanglk测试登录
//			valid = true;
//			oldstate = State.UNENTRY_STATE;
			enterErrorLog(oldstate, trigger);
			return false;
		}
		xtable.Roleonoffstate.remove(roleId);
		xtable.Roleonoffstate.add(roleId, getState());
		StateManager.logger.info("角色（Id = " + roleId + "）进入状态：" + this.getClass());

		return execute();
	}

	@Override
	public boolean execute() {

		if (!beforeEnterWorld())
			return false;
		sendEnterWorld();
		return trigger(State.TRIGGER_PROCESS_DONE);

	}

	/**
	 * 角色进入游戏前需要进行的处理放在此方法
	 * 三种登入情况，共同的处理放在这里，各自不同的处理覆盖此方法
	 * 
	 * @return
	 */
	protected boolean beforeEnterWorld() {
		try{
			PropRole prole = PropRole.getPropRole(roleId, false);			
			prole.processData();

//			pRole.sendTips();
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		try{		
//			CompenseRole cRole = CompenseRole.getCompenseRole(roleId, false);
//			cRole.processWhileOnline();
		}catch(Exception e)	
		{	
			e.printStackTrace();
		}
		
//		new chuhan.gsp.hero.PProcessXiulianAttr(roleId).call();
		new chuhan.gsp.hero.PTuJianInit(roleId).call();//初始化图鉴数据
//		new chuhan.gsp.hero.PResetSkill(roleId).call();
		
		return true;
	}

	/**
	 * 上线时发送协议写在此方法，注意只是发送协议，不要有任何复杂的可能抛出异常的处理
	 * 
	 * 如果有要同步处理的内容，可包装成Procedure，用call调用
	 * 
	 * @return
	 */
	protected void sendEnterWorld() {
		
		/**
		 * 发送SEnterWolrd到客户端
		 */
		gnet.link.Role linkrole = gnet.link.Onlines.getInstance().find(roleId);
		if (linkrole == null)
			return;
		int userId = linkrole.getUserid();
		final xbean.Properties pro = xtable.Properties.get(roleId);
		if (null == pro)
			return;
		if (pro.getUserid() == 0){
			pro.setUserid(userId);
		}
		xbean.AUUserInfo auuser = xtable.Auuserinfo.get(userId);
		if(auuser != null && !pro.getUsername().equals(auuser.getUsername()))
		{
			pro.setUsername(auuser.getUsername());
		}
		if(auuser != null/* && pro.getPlattypestr().isEmpty()*/)
		{
			String[] strs = auuser.getNickname().split("#");
			pro.setPlattypestr(strs[0]);
		}
		PropRole prole = PropRole.getPropRole(roleId, false);
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		
		int oldday = DateUtil.getCurrentDay(pro.getOfflinetime());
		int nowday = DateUtil.getCurrentDay(now);
		if(nowday - oldday == 1)
		{
//			pro.setContinuelogin(pro.getContinuelogin()+1);
		}
		else if(nowday-oldday > 1)
		{
//			pro.setContinuelogin(1);
		}
		pro.setOnlinetime(now);
		
		SEnterWorld senter = new SEnterWorld();
		senter.mydata.roleid = roleId;
		senter.mydata.name = pro.getRolename();
		if(GMInterface.getGMOn())
			senter.mydata.isgm = 1;
		else
			senter.mydata.isgm = (byte)(GMInterface.gmPriv(userId) ? 1 : 0);
		if(ConfigManager.inReviewState())
			senter.mydata.isgm = 2;
		senter.mydata.level = Conv.toShort(pro.getLevel());
		senter.mydata.exp = pro.getExp();
		senter.mydata.viplv = Conv.toByte(pro.getViplv());
		senter.mydata.vipexp = pro.getVipexp();
		senter.mydata.ti = Conv.toShort(prole.getTili(now));
		senter.mydata.titime = prole.getTiTime(now);
		senter.mydata.txti = Conv.toShort(prole.getTXTi(now));
		senter.mydata.txtitime = prole.getTXTiTime(now);
		senter.mydata.battlenum = pro.getBattlenum();
		senter.mydata.hammer = pro.getShenglingzq();
		senter.mydata.freegoldtime = 0;//prole.getCJFreeGold(now);
		senter.mydata.freeybtime = 0;//prole.getCJFreeYb(now);
		senter.mydata.tibuynum = prole.GetTiBuyNum(now);
		senter.mydata.goldbuynum = prole.GetGoldBuyNum(now);
//		senter.mydata.signnum = prole.GetSignNum(now);
		senter.mydata.signnum7 = pro.getSignnum7();
		senter.mydata.signnum28 = pro.getSignnum28();
/*		if(prole.isTodaySigned(now))
			senter.mydata.issign = 1;
		else
			senter.mydata.issign = 0;	*/
		
//		MailColumn mailcol = MailColumn.getMailColumn(roleId, false);
//		senter.mydata.mailsize = Conv.toByte(mailcol.getxcolumn().getMails().size());
		
		senter.mydata.money = pro.getGold();
		senter.mydata.yuanbao = pro.getYuanbao();
		senter.mydata.servertime = now;
		senter.mydata.timezone = Conv.toByte(Calendar.getInstance().getTimeZone().getRawOffset()/3600000);
		
		senter.mydata.buybagnum = pro.getBuybagnum();
		senter.mydata.buyherobagnum = pro.getBuyherobagnum();
		
		senter.mydata.shenglingzq = pro.getShenglingzq();
		senter.mydata.ronglian = pro.getRonglian();
		senter.mydata.huangjinxz = pro.getHuangjinxz();
		senter.mydata.baijinxz = pro.getBaijinxz();
		senter.mydata.qingtongxz = pro.getQingtongxz();
		senter.mydata.chitiexz = pro.getChitiexz();
		senter.mydata.jyjiejing = pro.getJyjiejing();
		
		senter.mydata.troopnum = Conv.toByte(pro.getTroopnum());
		senter.mydata.sweephavenum = prole.getSweepHaveNum(now);
		senter.mydata.sweepbuyhavenum = prole.getSweepHaveBuyNum(now);
		senter.mydata.mszqgetnum = prole.getProperties().getMszqgetnum();
		senter.mydata.qiyuannum = prole.getProperties().getQiyuannum();
		senter.mydata.qiyuanallnum = prole.getProperties().getQiyuanallnum();
		senter.mydata.isqiyuantoday = prole.isQiyuanToday(now);
		if( !DateUtil.isTodayOrYestoday(now,prole.getProperties().getQiyuantime()) ){
			senter.mydata.isqiyuantoday += 10;
		}
		senter.mydata.newyindao.addAll(prole.getProperties().getNewyindao());
		
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
		senter.mydata.heroes.addAll(herocol.getProtocolHeros());
		
		TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);	
		senter.mydata.troops.addAll(troopcol.getProtocolTroops());
		
		HeroSkinColumn skincol = HeroSkinColumn.getHeroSkinColumn(roleId, false);
		senter.mydata.heroskins.addAll(skincol.getProtocolHeroSkins());
		
		HeroArtifactColumn artifactcol = HeroArtifactColumn.getHeroArtifactColumn(roleId, false);
		senter.mydata.artifacts.putAll(artifactcol.getProtocolHeroArtifacts());
		
		ShopBuyColumn shopbuycol = ShopBuyColumn.getShopBuyColumn(roleId, false);
		senter.mydata.shopbuys.putAll(shopbuycol.getProtocolShopBuys());
		
		senter.mydata.baginfo.putAll(chuhan.gsp.item.Module.getOnlineSendBags(roleId));
		
		senter.mydata.smshop.putAll(StageRole.getSmShopMap(prole));
		
		
		if(pro.getSmendtime() > now){
			senter.mydata.smid = pro.getBattlenum();
			senter.mydata.smtime = (int) ((pro.getSmendtime() - now) / 1000);
			senter.mydata.smzhangjie = pro.getSmzhangjie();
		}else{
			senter.mydata.smid = 0;
			senter.mydata.smtime = (0);
			senter.mydata.smzhangjie = 0;
			if(pro.getBattlenum() != 0){
				pro.setBattlenum(0);
				pro.setSmendtime(0);
				pro.setSmzhangjie(0);
			}	
		}
		
		xdb.Procedure.psendWhileCommit(roleId, senter);
//		CPlayStatus.notifyAll(roleId);
//		BeautyRole.getBeautyRole(roleId, true).loginSendSkinInfo();
		
		StringBuilder sb = new StringBuilder("角色登录,roleid:");
		sb.append(roleId).append(",rolename:").append(senter.mydata.name).append(",userid:").append(userId);
		StateManager.logger.info(sb.toString());
		/**********************************************************************************/
		/**
		 * 为了不影响上线
		 * 以下内容都转移到afterEnterWorld或者knight.gsp.PAfterEnterWorld中
		 */
		
		afterEnterWorld(roleId, userId,now);
	}

	/**
	 * 上线处理结束，进入已登录状态，异步提交的存储过程写在此处 注意其执行的结果不能影响登录流程 注意是异步执行，不能保证其顺序性
	 * 
	 * !!注意，如果在此方法内添加上线处理，一定是Procedure异步提交，不要因为一个模块抛异常而影响登录
	 * 
	 *  如果有要同步处理的内容，可包装成Procedure.call调用
	 */
	protected void afterEnterWorld(final long roleId, int userId,long now) {
		PropRole prole = PropRole.getPropRole(roleId, false);
		
/*		try{
			prole.processData();
		}catch(Exception e){
			e.printStackTrace();
		}*/
		
		try{
			StageRole stagerole = StageRole.getStageRole(roleId);
			stagerole.sendAllStages();
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			ActivityManager.getInstance().sRefreshHeroClone(roleId);
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			EndlessinfoColumns endcol = EndlessinfoColumns.getEndLessColumn(roleId,false);
			endcol.sendTodayEndless();
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			ActivityManager.getInstance().sSRefreshMonthCard(roleId, now);
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			LotteryColumns col = LotteryColumns.getLotteryColumn(roleId,false);
			col.sSRefreshLotty(now);
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			HuoyueColumns huoyuecol = HuoyueColumns.getHuoyueColumn(roleId, false);
			huoyuecol.sSRefreshHuoYue();
		}catch(Exception e){
			e.printStackTrace();
		}
		try{
			LotteryItemColumns col = LotteryItemColumns.getLotteryItemColumn(roleId, false);
			col.sSRefreshLottyItem(now);
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			MailColumn col = MailColumn.getMailColumn(roleId, false);
			col.sSendIsHaveNotOpen();
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			TanXianColumns col = TanXianColumns.getTanXianColumn(roleId, false);
			col.sSRefreshTanXian();
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			Module.getInstance().sendSGetWordBoss(roleId);
		}catch(Exception e){
			e.printStackTrace();
		}
		try{
			new PGetBossRank(roleId).call();
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			//登录活动统计数据
			ActivityGameManager.getInstance().addDLActivity(roleId);
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			//登录活动统计数据
			ActivityGameManager.getInstance().addRoleLevelActivity(roleId,prole.getLevel());
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			//活动数据发送
			ActivityGameManager.getInstance().sRefreshGameAct(roleId);
		}catch(Exception e){
			e.printStackTrace();
		}
		
		try{
			//活动数据发送
			prole.refreshTili(now);
		}catch(Exception e){
			e.printStackTrace();
		}
		
		
		
		try{
			/*
			if(PlatformTypeStr.isQihoo360(prole.getProperties().getPlattypestr()))
			{
				xbean.AUUserInfo auuser = xtable.Auuserinfo.get(userId);
				if(auuser != null)
				{
					String[] strs = auuser.getNickname().split("#");
					if(strs.length >= 4)
					{
						SQihoo360ExtraInfo qihooext = new SQihoo360ExtraInfo();
						qihooext.uin = strs[1];
						qihooext.token = strs[2];
						qihooext.url = strs[3];
						xdb.Procedure.psendWhileCommit(roleId, qihooext);
					}
				}
			}
			*/
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		
		try{
//			PSelectHero.sendSSendFreeSelectTime(roleId);
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		
		try{
//			SSendTodayRecoverd snd = new SSendTodayRecoverd();
//			snd.huoli = Conv.toByte(prole.getProperties().getRecoverhuo());
//			snd.tili = Conv.toByte(prole.getProperties().getRecoverti());
//			xdb.Procedure.psendWhileCommit(roleId, snd);
//			prole.sendSRefreshVipBuyInfo();
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		
		try{
//			BeautyRole beautyRole = BeautyRole.getBeautyRole(roleId, false);
//			beautyRole.processWhileOnline();
		}catch(Exception e)
		{
			e.printStackTrace();
		}
		try{		
//			ChargeRole cRole = ChargeRole.getChargeRole(roleId, false);
//			cRole.processWhileOnline();
		}catch(Exception e)	
		{	
			e.printStackTrace();
		}	
		try{		
//			MsgRole cRole = MsgRole.getMsgRole(roleId, false);
//			cRole.processWhileOnline();
		}catch(Exception e)	
		{	
			e.printStackTrace();
		}	
		try{		
//			HeroTaskRole cRole = HeroTaskRole.getHeroTaskRole(roleId, false);
//			cRole.sendTasks();
		}catch(Exception e)	
		{	
			e.printStackTrace();
		}	
		try{		
//			ActivityRole cRole = ActivityRole.getActivityRole(roleId, false);
//			cRole.processWhileOnline();
		}catch(Exception e)	
		{	
			e.printStackTrace();
		}	
		
	}
	
	@Override
	public boolean trigger(int trigger) {
	
		if (trigger == State.TRIGGER_PROCESS_DONE)
			return new EntryState(roleId).enter(trigger);
				
		triggerErrorLog(trigger);
		return false;
	}

	@Override
	public int getState() {

		return State.PRE_ENTRY_STATE;
	}

}
