package chuhan.gsp.battle;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import xbean.FirstLadderInfo;
import xbean.FirstLadderInfoRole;

import chuhan.gsp.attr.PAddExpProc;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.player03;
import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.task.STiantiConfig;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.Misc;

public class LadderRole {
	
	public static final int MAX_RANK = 20000;
	public static final int TOPRANK_NUM = 50;//排行榜显示前50名
	public static final int FIRSTLADDERINFO_ID = 1;//firstladderinfo表的唯一ID
	public static LadderRole getLadderRole(long roleId, boolean readonly)
	{
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造LadderRole时，角色 "+roleId+" 不存在。");
		
		xbean.LadderRole xrole = null;
		if(readonly)
			xrole = xtable.Ladderroles.select(roleId);
		else
			xrole = xtable.Ladderroles.get(roleId);
		if(xrole == null)
		{
			if(readonly)
				xrole = xbean.Pod.newLadderRoleData();
			else
			{
				xrole = xbean.Pod.newLadderRole();
				xtable.Ladderroles.insert(roleId, xrole);
			}
		}
		return new LadderRole(roleId, xrole, readonly);
	}
	
	public static FirstLadderInfoRole getFirstLadderInfoRole(boolean readonly) {
		xbean.FirstLadderInfoRole firstLadderInfoRole;
		if(readonly) {
			firstLadderInfoRole = xtable.Firstladderinforole.select(FIRSTLADDERINFO_ID);
		} else {
			firstLadderInfoRole = xtable.Firstladderinforole.get(FIRSTLADDERINFO_ID);
		}
		if(null == firstLadderInfoRole) {
			if(readonly) {
				firstLadderInfoRole = xbean.Pod.newFirstLadderInfoRoleData();
			} else {
				firstLadderInfoRole = xbean.Pod.newFirstLadderInfoRole();
				xtable.Firstladderinforole.insert(FIRSTLADDERINFO_ID, firstLadderInfoRole);
			}
		}
		
		return firstLadderInfoRole;
	}
	
	private final long roleId;
	public final boolean readonly;
	private final xbean.LadderRole xrole;
	public LadderRole(long roleId, xbean.LadderRole xrole,boolean readonly) {
		this.roleId = roleId;
		this.readonly = readonly;
		this.xrole = xrole;
		processData();
	}
	
	public long getRoleId()
	{
		return roleId;
	}
	
	public xbean.LadderRole getData()
	{
		return xrole;
	}
	
	public void processData()
	{
		long now = GameTime.currentTimeMillis();
		if(!DateUtil.inTheSameDay(now, xrole.getLastfighttime()))
		{
			xrole.setLastfighttime(now);
			xrole.setFighttimes(0);
		}
		if(xrole.getLastsoulchangetime() <= 0)
			xrole.setLastsoulchangetime(now);
		//计算当前元魂值
		xrole.setLaddersoul(xrole.getLaddersoul() + Conv.toInt(getCurAddScore() * (now/1200000 - xrole.getLastsoulchangetime()/1200000)));
		xrole.setLastsoulchangetime(now);
	}
	
	public int getCurAddScore()
	{
		STiantiConfig scfg = getRankConfig();
		int addv = (int)((scfg == null)? 0 : scfg.jifena + scfg.jifenb * getRank());
		return addv;
	}
	
	public int getLadderScore()
	{
		return xrole.getLaddersoul();
	}
	
	public int addLadderSocre(int v)
	{
		if(v == 0)
			return 0;
		if((v+xrole.getLaddersoul()) < 0)
			return 0;
		xrole.setLaddersoul(xrole.getLaddersoul()+v);
		xrole.setLastsoulchangetime(GameTime.currentTimeMillis());
		xdb.Procedure.psendWhileCommit(roleId, new SRefreshLadderScore(xrole.getLaddersoul()));
		return v;
	}
	
	/**
	 * 返回排行前50名的玩家信息
	 */
	public void sendSGetLadderRankList() {
		SEnterLadder snd = new SEnterLadder();
		int myrank = getMyRank();
		snd.myrank = Conv.toShort(myrank);
		snd.laddersoul = xrole.getLaddersoul();
		snd.todayfightnum = Conv.toByte(xrole.getFighttimes());
		List<Long> rankers = getAllTopRankers(TOPRANK_NUM);
		int rank = 1;
		for(long roleid : rankers) {
			LadderRoleInfo ladderinfo = getLadderRoleInfo(rank, roleid, 3);
			rank ++;
			if(ladderinfo == null)
				continue;
			snd.ladderroles.add(ladderinfo);
		}
		if(xdb.Transaction.current() != null)
			xdb.Procedure.psendWhileCommit(roleId, snd);
		else
			gnet.link.Onlines.getInstance().send(roleId, snd);
	}
	
	public void sendSEnterLadder()
	{
		//xbean.Properties xprop = xtable.Properties.select(roleId);
		SEnterLadder snd = new SEnterLadder();
		int myrank = getMyRank();
		snd.myrank = Conv.toShort(myrank);
		snd.laddersoul = xrole.getLaddersoul();
		snd.todayfightnum = Conv.toByte(xrole.getFighttimes());
		Map<Integer,Long> enermies = getEnermyRankers();
		Map<Integer,Long> fronts = getCanFightRankers();
		Map<Integer,Long> backs = getBackRankers();
		Map<Integer,Long> tops = getTopRankers();
		snd.ladderroles.add(getLadderRoleInfo(myrank, roleId, 4));
		for(Map.Entry<Integer, Long> entry : enermies.entrySet())
		{
			LadderRoleInfo ladderinfo = getLadderRoleInfo(entry.getKey(), entry.getValue(), 2);
			if(ladderinfo == null)
				continue;
			snd.ladderroles.add(ladderinfo);
		}
		for(Map.Entry<Integer, Long> entry : fronts.entrySet())
		{
			if(checkInList(snd.ladderroles, entry.getKey()))
				continue;
			LadderRoleInfo ladderinfo = getLadderRoleInfo(entry.getKey(), entry.getValue(), 1);
			if(ladderinfo == null)
				continue;
			snd.ladderroles.add(ladderinfo);
		}
		for(Map.Entry<Integer, Long> entry : tops.entrySet())
		{
			if(checkInList(snd.ladderroles, entry.getKey()))
				continue;
			LadderRoleInfo ladderinfo = getLadderRoleInfo(entry.getKey(), entry.getValue(), 3);
			if(ladderinfo == null)
				continue;
			snd.ladderroles.add(ladderinfo);
		}
		for(Map.Entry<Integer, Long> entry : backs.entrySet())
		{
			if(checkInList(snd.ladderroles, entry.getKey()))
				continue;
			LadderRoleInfo ladderinfo = getLadderRoleInfo(entry.getKey(), entry.getValue(), 5);
			if(ladderinfo == null)
				continue;
			snd.ladderroles.add(ladderinfo);
		}
		Collections.sort(snd.ladderroles, new Comparator<LadderRoleInfo>() {
			@Override
			public int compare(LadderRoleInfo arg0, LadderRoleInfo arg1) {
				return arg0.ladderrank - arg1.ladderrank;
			}
		});
		if(xdb.Transaction.current() != null)
			xdb.Procedure.psendWhileCommit(roleId, snd);
		else
			gnet.link.Onlines.getInstance().send(roleId, snd);
	}
	
	private static boolean checkInList(List<chuhan.gsp.battle.LadderRoleInfo> alreadys, int newRank)
	{
		for(chuhan.gsp.battle.LadderRoleInfo info : alreadys)
		{
			if(info.ladderrank == newRank)
				return true;
		}
		return false;
	}
	
	
	private LadderRoleInfo getLadderRoleInfo(int rank, long roleId, int fighttype)
	{
		LadderRoleInfo roleinfo = new LadderRoleInfo();
		roleinfo.ladderrank = Conv.toShort(rank);
		roleinfo.fighttype = (byte)fighttype;
		STiantiConfig cfg = getRankConfig(rank);
		if(cfg == null)
			throw new IllegalArgumentException("Wrong rank : "+rank);
		roleinfo.addsoul = Conv.toShort((int)(cfg.jifena + cfg.jifenb* rank));
		if(roleId <= 0)
		{
			roleinfo.roleid = Misc.getRandomBetween(-1, -3);
			int battleId = 0;
			if(roleinfo.roleid == -1)
				battleId = cfg.battleID.get(0);
			else if(roleinfo.roleid == -2)
				battleId = cfg.battleID.get(1);
			else if(roleinfo.roleid == -3)
				battleId = cfg.battleID.get(2);
			SBattleConfig battlecfg = ConfigManager.getInstance().getConf(SBattleConfig.class).get(battleId);
			if(battlecfg == null)
				return null;
			roleinfo.rolelevel = Conv.toShort(cfg.getLevel());
			roleinfo.rolename = "NPC"+rank;
			// 根据battleId来初始化头像
			roleinfo.troopheros.addAll(battlecfg.getSpot());
		}
		else
		{
			roleinfo.roleid = roleId;
			xbean.Properties xp = xtable.Properties.select(roleId);
			roleinfo.rolelevel = Conv.toShort(xp.getLevel());
			roleinfo.rolename = xp.getRolename();
			OldHeroColumn col = OldHeroColumn.getHeroColumn(roleId, true);
			roleinfo.troopheros.addAll(col.getHeroIds());
			FirstLadderInfo firstLadderInfo = getFirstLadderInfoRole(true).getRoleinfos().get(roleId);
			if(null != firstLadderInfo) {
				if(rank == 1) {
					int thisTime = (int) ((GameTime.currentTimeMillis() - firstLadderInfo.getStarttime()) / PCalLadder.ONE_MINUT);
					int oldTime = firstLadderInfo.getZaiweimilsec() / PCalLadder.ONE_MINUT;
					if(thisTime + oldTime < 1) {//小于1分钟 算成1分钟
						roleinfo.firstminit = 1;
					} else {
						roleinfo.firstminit = thisTime + oldTime;
					}
				} else {
					int oldTime = firstLadderInfo.getZaiweimilsec() / PCalLadder.ONE_MINUT;
					if(oldTime < 1) {//小于1分钟 算成1分钟
						roleinfo.firstminit = 1;
					} else {
						roleinfo.firstminit = oldTime;
					}
				}
			}
		}
		return roleinfo;
	}
	
	/**
	 * 当在榜外时，排名返回0
	 * @return
	 */
	public int getRank()
	{
		return xrole.getLadderrank();
	}
	
	/**
	 * 当在榜外时，排名返回MAX_RANK+1
	 * @return
	 */
	public int getMyRank()
	{
		if(onLadder(xrole.getLadderrank()))
			return xrole.getLadderrank();
		else
			return MAX_RANK+1;
	}
	
	private STiantiConfig getRankConfig()
	{
		return getRankConfig(getMyRank());
	}
	
	public static STiantiConfig getRankConfig(int rank)
	{
		Map<Integer,STiantiConfig> cfgs = ConfigManager.getInstance().getConf(STiantiConfig.class);
		for(Map.Entry<Integer, STiantiConfig> entry :cfgs.entrySet())
		{
			if(rank >= entry.getValue().srartrank && rank <= entry.getValue().endrank)
				return entry.getValue();
		}
		return null;
	}
	
	private Map<Integer,Long> getTopRankers()
	{
		Map<Integer,Long> ranks = new HashMap<Integer, Long>();
		int myrank = getMyRank();
		//前10
		for(int i = 1; i <= 10; i++)
		{
			if(i == myrank)
				continue;
			xbean.LadderInfo xinfo = xtable.Pvpladder.select(i);
			long rid = (xinfo != null)? xinfo.getRoleid() : 0;
			ranks.put(i, rid);
		}
		return ranks;
	}
	
	/**
	 * 获取排行前num名的roleId
	 * @param num
	 * @return
	 */
	private List<Long> getAllTopRankers(int num) {
		List<Long> rankers = new ArrayList<Long>();
		for(int i = 1; i <= num; i ++) {
			xbean.LadderInfo xinfo = xtable.Pvpladder.select(i);
			long rid = (xinfo != null)? xinfo.getRoleid() : 0;
			rankers.add(rid);
		}
		
		return rankers;
	}
	
	private Map<Integer,Long> getCanFightRankers()
	{
		Map<Integer,Long> ranks = new HashMap<Integer, Long>();
		int myrank = getMyRank();
		int interval =  Math.max(1 ,(int)((myrank*(0.9+0.2*Math.random()))/25));
		//前位的8个人
		for(int i = 0 ; i <= 5;i++)
		{
			int r = 0;
			if(i == 0)
				r = myrank - 1;
			else
				r = Math.max(1, myrank - 1 - interval*i);
			if(!onLadder(r) || ranks.containsKey(r) || r == myrank)
				continue;
			xbean.LadderInfo xinfo = xtable.Pvpladder.select(r);
			long rid = (xinfo != null)? xinfo.getRoleid() : 0;
			ranks.put(r, rid);
		}
		return ranks;
	}
	
	private Map<Integer,Long> getBackRankers()
	{
		Map<Integer,Long> ranks = new HashMap<Integer, Long>();
		int myrank = getMyRank();
		int interval = Math.max(1 ,(int)(myrank/50));
		//后位的4个人
		for(int i = 0 ; i <= 3; i++)
		{
			int r = 0;
			if(i == 0)
				r = myrank + 1;
			else
				r = Math.max(1, myrank + 1 + interval*i);
			if(!onLadder(r) || ranks.containsKey(r)|| r == myrank)
				continue;
			xbean.LadderInfo xinfo = xtable.Pvpladder.select(r);
			long rid = (xinfo != null)? xinfo.getRoleid() : 0;
			ranks.put(r, rid);
		}
		return ranks;
	}
	
	private Map<Integer,Long> getEnermyRankers()
	{
		Map<Integer,Long> ranks = new HashMap<Integer, Long>();
		int myrank = getMyRank();
		for(long enermyid :  xrole.getEnermies())
		{
			 LadderRole r = LadderRole.getLadderRole(enermyid, true);
			 if(r == null || !r.onLadder() || r.getMyRank() >= myrank)
				 continue;
			 ranks.put(r.getMyRank(), enermyid);
		}
		
		return ranks;
	}
	
	public boolean canChallenge()
	{
		if(xrole.getFighttimes() >= 6)
			return false;
		
		return true;
	}
	
	public int getMaxChallengeTimes()
	{
		player03 cfg = ConfigManager.getInstance().getConf(player03.class).get(xtable.Properties.get(roleId).getLevel());
		return 0;//cfg.invitetimes;
	}
	
	public boolean challenge(long rankerId, int rank, boolean isinvite)
	{
		processData();
		if(!onLadder(rank))
			return false;
		if(isinvite && xrole.getFighttimes() >= getMaxChallengeTimes())
		{
			Message.psendMsgNotify(roleId, 127);
			return false;
		}
		if(rankerId == roleId || rank == getRank())
			return false;
		PropRole prole = PropRole.getPropRole(roleId, false);
		
//		if(prole.addHuoli(-1) != -1)
//			return false;
		PNewBattle pbattle;
		if(rankerId > 0)
			pbattle = new PNewBattle(roleId, rankerId, 0, false, isinvite? -1: BattleUtil.DEFAULT_DIRECT_END_SECOND);
		else
		{
			//随机战斗
			STiantiConfig cfg = getRankConfig(rank);
			int battleId = 0;
			if(rankerId == -1)
				battleId = cfg.battleID.get(0);
			else if(rankerId == -2)
				battleId = cfg.battleID.get(1);
			else if(rankerId == -3)
				battleId = cfg.battleID.get(2);
			if(battleId == 0)
				return false;
			pbattle = new PNewBattle(roleId, battleId, 0, false,isinvite? -1: BattleUtil.DEFAULT_DIRECT_END_SECOND);
		}
		pbattle.call();
		if(!pbattle.isSuccess())
			return false;
		boolean iswin = (pbattle.getSSendBattleScript().result.winround > 0);
		//给人经验
		int addexp = iswin? (prole.getLevel() +50)/3 : (prole.getLevel() +50)/9;
		PAddExpProc proc = new PAddExpProc(roleId, addexp, 1, "");//给人物经验
		if(proc.call())
			pbattle.getSSendBattleScript().result.addexp = addexp;
		//给银两
		int addmoney = iswin? (prole.getLevel() +50)*30 : (prole.getLevel() +50)*10;
		chuhan.gsp.item.Bag bag = new chuhan.gsp.item.Bag(roleId, false);
		long realAdd = bag.addMoney(addmoney, "ladder money award", 1,1);
		pbattle.getSSendBattleScript().result.addmoney = Conv.toInt(realAdd);
		if(!isinvite && rankerId > 0)
		{
			try{
				pbattle.getSSendBattleScript().enermyrank = 0;//Conv.toShort(rank);
				xbean.Properties enermyprop = xtable.Properties.get(rankerId);
				pbattle.getSSendBattleScript().enermyname =enermyprop.getRolename();
			}catch(Exception e){
				e.printStackTrace();
			}
		}
		if(iswin)
		{
			int oldrank = getMyRank();
			if(rank < getMyRank())
			{//换位
				setRank(rank);
			}
			if(rankerId > 0)
			{//仇敌
				xrole.getEnermies().remove(rankerId);
				LadderRole targetlrole = LadderRole.getLadderRole(rankerId, false);
				if(targetlrole != null)
					targetlrole.onDefeated(roleId, oldrank,isinvite);
			}
		}
		else
		{
			//未胜利
			if(rankerId > 0)
			{//仇敌
				LadderRole targetlrole = LadderRole.getLadderRole(rankerId, false);
				if(targetlrole != null)
					targetlrole.onDefend(roleId, isinvite);
			}
		}
		
		if(isinvite)
		{
			pbattle.getSSendBattleScript().result.itemkey = -1;
			pbattle.getSSendBattleScript().result.itemid = 3812;
			pbattle.getSSendBattleScript().result.num = getMyRank();
		}
		else
		{
			int addv = getCurAddScore();
			if(DateUtil.getCurrentWeekDay() == 2)
				addv = addv * 3;
			addLadderSocre(addv);
			pbattle.getSSendBattleScript().result.itemkey = -1;
			pbattle.getSSendBattleScript().result.itemid = 3813;
			pbattle.getSSendBattleScript().result.num = addv;
		}
		xrole.setLastfighttime(GameTime.currentTimeMillis());
		if(isinvite)
			xrole.setFighttimes(xrole.getFighttimes()+1);
		pbattle.sendSSendBattleScript();
		sendSEnterLadder();
		return true;
	}
	
	public void onDefend(long enermyId, boolean isinvite)
	{
		xbean.Properties enermyprop = xtable.Properties.get(enermyId);
		MsgRole msgrole = MsgRole.getMsgRole(roleId,false);
		List<String> params = new LinkedList<String>();
		params.add(enermyprop.getRolename());
		int msgId = (isinvite)?187:188;
		msgrole.addSysMsg(msgId, params, null, 0, MsgRole.MST_TYPE_SYS);
	}
	
	public void onDefeated(long enermyId, int rank, boolean isinvite)
	{
		//xrole.setLadderrank(rank);
		if(!xrole.getEnermies().contains(enermyId))
		{
			if(xrole.getEnermies().size() >= 4)
				xrole.getEnermies().remove(0);
			xrole.getEnermies().add(enermyId);
		}
		//发送被打败的协议
		if(rank == getRank())
		{
			xbean.Properties enermyprop = xtable.Properties.get(enermyId);
			MsgRole msgrole = MsgRole.getMsgRole(roleId,false);
			List<String> params = new LinkedList<String>();
			params.add(enermyprop.getRolename());
			params.add(String.valueOf(rank));
			int msgId = (isinvite)?164:186;
			msgrole.addSysMsg(msgId, params, null, enermyId, MsgRole.MST_TYPE_DEFEAT);
		}
	}
	
	/**
	 * 设置排位，如果位置有人，则互换
	 * @param dstrank
	 * @return
	 */
	public boolean setRank(int dstrank)
	{
		if(!onLadder(dstrank))
			return false;
		int srcrank = getRank();
		if(dstrank == srcrank)
			return false;
		xbean.LadderInfo srcladder = onLadder()? xtable.Pvpladder.get(srcrank) : null;
		xbean.LadderInfo dstladder = xtable.Pvpladder.get(dstrank);
		if(dstladder != null)
		{//替换
			LadderRole dstrole = getLadderRole(dstladder.getRoleid(),false);
			if(srcladder != null)
				srcladder.setRoleid(dstladder.getRoleid());
			dstrole.getData().setLadderrank(srcrank);
			
			if(dstrank == 1) {//是第一名被替换掉则结算他本次的在位时间
				FirstLadderInfoRole firstLadderInfoRole = getFirstLadderInfoRole(false);
				FirstLadderInfo oldFirstLadderInfo = firstLadderInfoRole.getRoleinfos().get(dstladder.getRoleid());
				FirstLadderInfo newFirstLadderInfo = firstLadderInfoRole.getRoleinfos().get(roleId);
				if(null != oldFirstLadderInfo) {
					int thisMilSec = (int) (GameTime.currentTimeMillis() - oldFirstLadderInfo.getStarttime());
					oldFirstLadderInfo.setZaiweimilsec(oldFirstLadderInfo.getZaiweimilsec() + thisMilSec / 2);
					if(null == newFirstLadderInfo) {
						newFirstLadderInfo = xbean.Pod.newFirstLadderInfo();
						firstLadderInfoRole.getRoleinfos().put(roleId, newFirstLadderInfo);
						newFirstLadderInfo.setZaiweimilsec(thisMilSec / 2);
					} else {
						newFirstLadderInfo.setZaiweimilsec(newFirstLadderInfo.getZaiweimilsec() + thisMilSec / 2);
					}
				}
			}
		}
		else
		{//此处没有玩家
			dstladder = xbean.Pod.newLadderInfo();
			xtable.Pvpladder.insert(dstrank, dstladder);
			xtable.Pvpladder.remove(srcrank);
		}
		dstladder.setRoleid(roleId);
		getData().setLadderrank(dstrank);
		
		if(dstrank == 1) {//如果自己变为第一名则记录开始时间
			FirstLadderInfoRole firstLadderInfoRole = getFirstLadderInfoRole(false);
			FirstLadderInfo firstLadderInfo = firstLadderInfoRole.getRoleinfos().get(roleId);
			if(null == firstLadderInfo) {
				firstLadderInfo = xbean.Pod.newFirstLadderInfo();
				firstLadderInfoRole.getRoleinfos().put(roleId, firstLadderInfo);
			}
			firstLadderInfo.setStarttime(GameTime.currentTimeMillis());
		}
		
		return true;
	}
	
	public boolean onLadder()
	{
		return onLadder(getRank());
	}
	
	public static boolean onLadder(int rank)
	{
		return rank > 0 && rank <= MAX_RANK;
	}
}
