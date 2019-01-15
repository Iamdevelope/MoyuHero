package chuhan.gsp.play.endlessbattle;

import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import chuhan.gsp.defenceInfo;
import chuhan.gsp.fightInfo;
import chuhan.gsp.attr.config10;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.battle.BattleManager;
import chuhan.gsp.game.ultimatetrialmonster49;
import chuhan.gsp.hero.Hero;
import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.hero.TroopColumn;
import chuhan.gsp.item.innerdrop16;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.mail.MailColumn;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.play.ranking.endlessRanking;
import chuhan.gsp.task.stage11;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.ParserString;



public class EndlessinfoColumns {	
	public static Logger logger = Logger.getLogger(EndlessinfoColumns.class);
	final static int battleid = 1340300000;
	
	final public long roleId;
	final public xbean.EndlessInfo xcolumn;
	final boolean readonly;
	final stage11 istage;
	final List<Integer> monsterGroupList;
	
	public static EndlessinfoColumns getEndLessColumn(long roleId, boolean readonly){
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造EndlessinfoColumns时，角色 "+roleId+" 不存在。");
		
		xbean.EndlessInfo endlesscol = null;
		if(readonly)
			endlesscol = xtable.Endlesscolumns.select(roleId);
		else
			endlesscol = xtable.Endlesscolumns.get(roleId);
		if(endlesscol == null)
		{
			if(readonly)
				endlesscol = xbean.Pod.newEndlessInfoData();
			else
			{
				endlesscol = xbean.Pod.newEndlessInfo();
				xtable.Endlesscolumns.insert(roleId, endlesscol);
			}
		}
		return new EndlessinfoColumns(roleId, endlesscol, readonly);
	}
	
	private EndlessinfoColumns(long roleId, xbean.EndlessInfo xcolumn, boolean readonly) {
		this.roleId = roleId;
		this.xcolumn = xcolumn;
		this.readonly = readonly;
		this.istage = ConfigManager.getInstance().getConf(stage11.class).get(battleid);
		if(istage != null){
			monsterGroupList = ParserString.parseString2Int(istage.getMonstergroup());
		}else{
			monsterGroupList = null;
		}
		init();
	}
	public void init(){
		long now = GameTime.currentTimeMillis();
		if( !DateUtil.inTheSameDay(now, xcolumn.getTime()) ){
			clearData(now);
		}
	}
	
	public endlessBattleInfo getProtocolEndlessInfoBegin(){
		endlessBattleInfo datas = new endlessBattleInfo();
		datas.battleid = xcolumn.getBattleid();
		datas.groupnum = xcolumn.getGroupnum();
		datas.useherokeylist.putAll(xcolumn.getUseherokeylist());
		datas.monstergroup.addAll(xcolumn.getMonstergroup());
		datas.trooptype = xcolumn.getTrooptype();
		datas.monstertrooptype = xcolumn.getMonstertrooptype();
		datas.pact = xcolumn.getPact();
		return datas;
	}
	
	public endlessAttr getProtocolEndlessInfoAttr(){
		endlessAttr datas = new endlessAttr();
		datas.dropnum = xcolumn.getDropnum();
		datas.alldropnum = xcolumn.getAlldropnum();
		datas.add1 = xcolumn.getAdd1();
		datas.add2 = xcolumn.getAdd2();
		datas.add3 = xcolumn.getAdd3();
		datas.add4 = xcolumn.getAdd4();
		datas.herobloodlist.putAll(xcolumn.getHerobloodlist());
		return datas;
	}
	
	/**
	 * 不是同一天则重置数据
	 * @param now
	 */
	public void clearData(long now){
		xcolumn.setBattleid(battleid);
		xcolumn.setGroupnum(0);
		xcolumn.getUseherokeylist().clear();
		xcolumn.getMonstergroup().clear();
		xcolumn.setTrooptype(1);
		xcolumn.setMonstertrooptype(1);
		xcolumn.setPact(-1);
		xcolumn.setDropnum(0);
		xcolumn.setAlldropnum(0);
		xcolumn.setAdd1(0);
		xcolumn.setAdd2(0);
		xcolumn.setAdd3(0);
		xcolumn.setAdd4(0);
		xcolumn.getHerobloodlist().clear();
		xcolumn.setIsend(0);
		xcolumn.setTime(now);
		xcolumn.setEndtime(0);
		xcolumn.setExpectedrank(-1);
		xcolumn.getHeroattribute().clear();
	}
	
	/**
	 * 极限试炼终止
	 */
	public void endlessEnd(){
		SEndlessEnd snd = new SEndlessEnd();
		snd.groupnum = xcolumn.getGroupnum();
		snd.alldropnum = xcolumn.getAlldropnum();
		snd.pact = xcolumn.getPact();
		snd.pactispass = 0;
		snd.dropmap.putAll(getDrop(xcolumn.getGroupnum()));
		if(snd.pact != -1){
			String pactNum = ConfigManager.getInstance().getConf(config10.class).get(1261).configvalue;
			List<Integer> pactList = ParserString.parseString2Int(pactNum);
			if(snd.pact < pactList.size()){
				if(snd.groupnum >= pactList.get(snd.pact)){
					snd.pactispass = 1;
				}
			}
		}
		xdb.Procedure.psend(roleId, snd);
		
		this.xcolumn.setIsend(2);
		long now = GameTime.currentTimeMillis();
		this.xcolumn.setEndtime(now);
		if(snd.pactispass == 1){
			this.xcolumn.setIshalfcostpact(0);
			//强者之约奖励
			String pactRewardNum = ConfigManager.getInstance().getConf(config10.class).get(1262).configvalue;
			List<Integer> pactRewardList = ParserString.parseString2Int(pactRewardNum);
			if(snd.pact < pactRewardList.size()){
				int rewardNum = pactRewardList.get(snd.pact);
				MailColumn col = MailColumn.getMailColumn(roleId, false);
				col.addMail(col.createMail("mail_tips1","mail_content23","mail_content27", null,
						IDManager.SHENGLINGZQ,rewardNum, now+MailColumn.DEFAULT_TIME,null),false);
				//暂时直接加到人物身上，案子里写的是通过邮件
//				chuhan.gsp.attr.PropRole proprole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
//				proprole.addZiYuan(rewardNum, 0, IDManager.SHENGLINGZQ);
			}
		}else{
			if(snd.pact != -1){
				this.xcolumn.setIshalfcostpact(1);
			}
		}
		this.sendTodayEndless();
		endlessRanking.getInstance().addInRank(this);	
	}
	/**
	 * 获取掉落列表
	 * @param groupnum
	 * @return
	 */
	public Map<Integer,Integer> getDrop(int groupnum){
		List<Integer> innerDropList = new LinkedList<Integer>();
		for(int i = 1; i <= groupnum ; i++){
			ultimatetrialmonster49 init = ConfigManager.getInstance().getConf(ultimatetrialmonster49.class).get(i);
			if(init != null && init.getWavereward() != -1){
				innerDropList.addAll(DropManager.getInstance().drop(roleId, 
						String.valueOf(init.getWavereward()), LogBehavior.ENDLESSOVER));
			}
		}
		return DropManager.getInstance().getInnerDropMap(innerDropList);
	}
	
	/**
	 * 购买附加属性
	 * @param addnum
	 * @return
	 */
	public boolean buyAddAttr(int addnum){
		if(this.xcolumn.getIsend() != 1){
			return false;
		}
		String addCostNum = ConfigManager.getInstance().getConf(config10.class).get(1256).configvalue;
		int addCost = 0;
		try{
			addCost = Integer.parseInt(addCostNum);
		}catch(Exception e){
			e.printStackTrace();
			return false;
		}
		//每关通过能得到6个
		if(this.xcolumn.getDropnum() + 6 < addCost){
			return false;
		}
		this.xcolumn.setDropnum(this.xcolumn.getDropnum() - addCost);
		switch(addnum){
		case 1:
			this.xcolumn.setAdd1(this.xcolumn.getAdd1() + 1);
			break;
		case 2:
			this.xcolumn.setAdd2(this.xcolumn.getAdd2() + 1);
			break;
		case 3:
			this.xcolumn.setAdd3(this.xcolumn.getAdd3() + 1);
			break;
		case 4:
			this.xcolumn.setAdd4(this.xcolumn.getAdd4() + 1);
			break;
		default:
			return false;
		}
		return true;
	}
	
	/**
	 * 购买强者之约
	 * @param pactid
	 * @return
	 */
	public boolean buyPact(int pactid){
		if(xcolumn.getIsend() != 0 || xcolumn.getPact() != -1 || pactid < 0){
			return false;
		}
		String pactCostNum = ConfigManager.getInstance().getConf(config10.class).get(1262).configvalue;
		List<Integer> pactCostList = ParserString.parseString2Int(pactCostNum);
		if(pactid >= pactCostList.size()){
			return false;
		}
		int cost = pactCostList.get(pactid);
		if(xcolumn.getIshalfcostpact() == 1){
			cost = cost / 2;
			xcolumn.setIshalfcostpact(0);
		}
		chuhan.gsp.attr.PropRole proprole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		if(proprole.delYuanBao(-cost, 0) == -cost){
			xcolumn.setPact(pactid);
			this.sendTodayEndless();
			
			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.ENDLESS_BUY, 1);

			return true;
		}
		return false;
	}
	
	/**
	 * 每一波战斗结束判断
	 * @param fightinfolist
	 * @return
	 */
	public boolean endlessPass(java.util.LinkedList<fightInfo> fightinfolist){
		int mosterdieNum = 0;
		logger.debug("----------"+fightinfolist.size()+"----------");
		for(fightInfo fInfo: fightinfolist){
			if(BattleManager.isHeroAttacker(fInfo.m_attacker)){
				mosterdieNum += heroAttack(fInfo);
			}else{
				monsterAttack(fInfo);
			}	
		}
		if(isHeroAllDie()){
			//每关都是最多5个怪
			if(mosterdieNum > 5){
				mosterdieNum = 5;
			}
			xcolumn.setDropnum(xcolumn.getDropnum() + mosterdieNum);
			xcolumn.setAlldropnum(xcolumn.getAlldropnum() + mosterdieNum);
			xcolumn.setGroupnum(xcolumn.getGroupnum() - 1);
			return false;
		}
		
		int groupnum = xcolumn.getGroupnum() + 1;			
		ultimatetrialmonster49 imonster = ConfigManager.getInstance().getConf(ultimatetrialmonster49.class).get(groupnum);
		if(imonster == null){
			return false;
		}
		xcolumn.getMonstergroup().clear();
		int monstertroop = chuhan.gsp.util.Misc.getRandomBetween(1,2);
		xcolumn.setMonstertrooptype(monstertroop);
		if(!setMonster(imonster,monstertroop)){
			return false;
		}
			
		xcolumn.setGroupnum(groupnum);
		xcolumn.setDropnum(xcolumn.getDropnum() + 6);
		xcolumn.setAlldropnum(xcolumn.getAlldropnum() + 6);
		this.sendNext(SEndlessPass.END_OK);
		
		return true;
	}
	
	/**
	 * 敌人攻击数据处理
	 * @param fInfo
	 */
	public void monsterAttack(fightInfo fInfo){
		for(defenceInfo dinfo : fInfo.m_defenceinfo){
			if( !BattleManager.isHeroAttacker(dinfo.m_defencer)){
				for(int i = 1; i<=5;i++){
					if(dinfo.m_defencer>>(i-1) == 1){
						xcolumn.getHerobloodlist().put( i, (int)(dinfo.m_remainhp));
						break;
					}
				}
			}
		}
	}
	
	/**
	 * 英雄攻击数据处理
	 * @param fInfo
	 */
	public int heroAttack(fightInfo fInfo){
		int result = 0;
		for(defenceInfo dinfo : fInfo.m_defenceinfo){
			if(dinfo.m_remainhp == 0){
				result++;
			}
		}
		return result;
	}
	
	/**
	 * 英雄是否全挂了
	 * @return
	 */
	public boolean isHeroAllDie(){
		int num = 0;
		for(Map.Entry<Integer, Integer> entry : xcolumn.getUseherokeylist().entrySet()){
			if(entry.getValue() != 0){
				num++;
			}
		}
		if( xcolumn.getHerobloodlist().size() != num ){
			return false;
		}
		for(Map.Entry<Integer, Integer> entry : xcolumn.getHerobloodlist().entrySet()){
			if(entry.getValue() > 0){
				return false;
			}
		}
		return true;
	}
	
	
	
	/**
	 * 获取攻击者位置编号
	 * @param m_attacker
	 * @param isHero
	 * @return
	 */
	public int attackerPlace(byte m_attacker, boolean isHero){
		int place = 0;
		if((m_attacker & 1) == 1){
			place = 0;
		}else if((m_attacker & 2) == 2){
			place = 1;
		}else if((m_attacker & 4) == 4){
			place = 2;
		}else if((m_attacker & 8) == 8){
			place = 3;
		}else if((m_attacker & 16) == 16){
			place = 4;
		}else{
			place = -1;
		}
		//英雄位置从1开始，怪物位置从0开始
		if(isHero && place != -1){
			place += 1;
		}
		return place;
	}
	
	/**
	 * 开始极限试炼入口
	 * @param troopid
	 * @return
	 */
	public boolean beginEndLess(int troopid){
		init();

		if(xcolumn.getIsend() == 1){
//			sendNext(SEndlessPass.END_OK);
			return true;
		}else if(xcolumn.getIsend() != 0){
			return false;
		}
		xcolumn.setGroupnum(1);
		ultimatetrialmonster49 imonster = ConfigManager.getInstance().getConf(ultimatetrialmonster49.class).get(xcolumn.getGroupnum());
		if(imonster == null){
			return false;
		}
		if(istage == null){
			return false;
		}
		
		TroopColumn troopcol = TroopColumn.getTroopColumn(roleId, false);
		xcolumn.getUseherokeylist().putAll(troopcol.getHkeyLolistByTrid(troopid));
		xcolumn.setTrooptype(troopcol.getTroopTypeByTrid(troopid));
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
		for(Map.Entry<Integer, Integer> entry : xcolumn.getUseherokeylist().entrySet()){
			Hero hero = herocol.getHByHKey(entry.getValue());
			if(hero != null){
				xbean.OtherHero otherHero = xbean.Pod.newOtherHero();
				otherHero.setHeroid(hero.getiHeroInfo().getId());
				otherHero.setExp(hero.getxHeroInfo().getHeroexp());
				otherHero.setHerolevel(hero.getLevel());
				otherHero.setHp(100000);			//测试
				otherHero.setPhysicalattack(1000);	//测试
				otherHero.setPhysicaldefence(1000);	//测试
				otherHero.setMagicattack(1000);		//测试
				otherHero.setMagicdefence(1000);	//测试
//				otherHero.setSkill1(hero.getxHeroInfo().getSkill1());
//				otherHero.setSkill2(hero.getxHeroInfo().getSkill2());
//				otherHero.setSkill3(hero.getxHeroInfo().getSkill3());
				otherHero.setHeroviewid(hero.getxHeroInfo().getHeroviewid());
				xcolumn.getHeroattribute().put(entry.getKey(), otherHero);	
			}
		}
		xcolumn.getMonstergroup().clear();
		int monstertroop = chuhan.gsp.util.Misc.getRandomBetween(1,2);
		xcolumn.setMonstertrooptype(monstertroop);
		if(!setMonster(imonster,monstertroop)){
			return false;
		}
		xcolumn.setGroupnum(1);
		xcolumn.setIsend(1);
		xcolumn.setIsnotfirst(1);
		
		ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.ENDLESS_BEGIN, 1);

		return true;
	}
	
	/**
	 * 设置怪物组
	 * @param imonster
	 * @param monstertroop
	 * @return
	 */
	public boolean setMonster(ultimatetrialmonster49 imonster,int monstertroop){
		List<Integer> probability = ParserString.parseString2Int(imonster.getProbability());
		for(int i = 0; i < 5; i++){
			int monsterTeam = chuhan.gsp.util.Misc.getProbability(probability);
//			int monsterid = -1;
			int monstergroup = getMonsterGroup(i,monstertroop,monsterTeam);
			if(monstergroup == -1){
				return false;
			}
			//最大随机20次，防止死循环
			for(int j = 0; j < 20; j++){
				int monsterid = BattleManager.getInstance().getMonsterList(monstergroup);
				if(this.xcolumn.getMonstergroup().contains(Integer.valueOf(monsterid))){
					continue;
				}
				xcolumn.getMonstergroup().add(monsterid);
				break;
			}
			if(xcolumn.getMonstergroup().size() != i + 1)
				return false;
		}
		return true;
	}
	
	/**
	 * 获取怪物组
	 * @param i
	 * @param monstertroop
	 * @param monsterTeam
	 * @return
	 */
	public int getMonsterGroup(int i,int monstertroop,int monsterTeam){
		switch(i){
		case 0:
			return getMonsterGroup(monsterTeam,true);
		case 1:
			return getMonsterGroup(monsterTeam,true);
		case 2:
			if(monstertroop == 1){
				return getMonsterGroup(monsterTeam,false);
			}else{
				return getMonsterGroup(monsterTeam,true);
			}
		case 3:
			return getMonsterGroup(monsterTeam,false);
		case 4:
			return getMonsterGroup(monsterTeam,false);
		}
		return -1;
	}
	
	/**
	 * 获取怪物组
	 * @param monsterTeam
	 * @param isClose
	 * @return
	 */
	public int getMonsterGroup(int monsterTeam,boolean isClose){
		if(this.monsterGroupList.size() < 8)
			return -1;
		switch(monsterTeam){
		case 0:
			if(isClose){
				return monsterGroupList.get(0);
			}else{
				return monsterGroupList.get(1);
			}
		case 1:
			if(isClose){
				return monsterGroupList.get(3);
			}else{
				return monsterGroupList.get(4);
			}
		case 2:
			if(isClose){
				return monsterGroupList.get(6);
			}else{
				return monsterGroupList.get(7);
			}
		}
		return -1;
	}
	
	
	/**
	 * 发送继续关卡消息
	 * @param value
	 */
	public void sendNext(int value){
		SEndlessPass snd = new SEndlessPass();
		snd.result = value;
		snd.battleinfo = this.getProtocolEndlessInfoBegin();
		snd.attrinfo = this.getProtocolEndlessInfoAttr();
		xdb.Procedure.psend(roleId, snd);
	}
	
	/**
	 * 发送今天试炼信息
	 */
	public void sendTodayEndless(){
		this.init();
		STodayEndless snd = new STodayEndless();
		snd.isend = xcolumn.getIsend();
		snd.groupnum = xcolumn.getGroupnum();
		snd.alldropnum = xcolumn.getAlldropnum();
		snd.pact = xcolumn.getPact();
		snd.ishalfcostpact = xcolumn.getIshalfcostpact();
		snd.paiming = xcolumn.getExpectedrank();
		snd.isnotfirst = xcolumn.getIsnotfirst();
		xdb.Procedure.psend(roleId, snd);
	}
	
	
	
}
