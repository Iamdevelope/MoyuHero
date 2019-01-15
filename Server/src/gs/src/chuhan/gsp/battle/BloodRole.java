package chuhan.gsp.battle;

import xdb.Transaction;
import chuhan.gsp.attr.AttrType;
import chuhan.gsp.attr.PAddExpProc;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoConsumeType;
import chuhan.gsp.buff.BuffConstant;
import chuhan.gsp.buff.ContinualBuff;
import chuhan.gsp.item.AddItemResult;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.task.shuifuconfig;
import chuhan.gsp.task.sxuezhanguding;
import chuhan.gsp.task.sxuezhanxunhuan;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.Misc;

public class BloodRole {
	public static int LEVEL_ITEM_AWARD = 100700; 
	
	
	public static BloodRole getBloodRole(long roleId, boolean readonly)
	{
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造BloodRole时，角色 "+roleId+" 不存在。");
		
		xbean.BloodRole xrole = null;
		if(readonly)
			xrole = xtable.Bloodroles.select(roleId);
		else
			xrole = xtable.Bloodroles.get(roleId);
		if(xrole == null)
		{
			if(readonly)
				xrole = xbean.Pod.newBloodRoleData();
			else
			{
				xrole = xbean.Pod.newBloodRole();
				xtable.Bloodroles.insert(roleId, xrole);
			}
		}
		return new BloodRole(roleId, xrole, readonly);
	}
	
	
	
	private final long roleId;
	public final boolean readonly;
	private final xbean.BloodRole xrole;
	public BloodRole(long roleId, xbean.BloodRole xrole,boolean readonly) {
		this.roleId = roleId;
		this.readonly = readonly;
		this.xrole = xrole;
	}
	
	public long getRoleId()
	{
		return roleId;
	}
	
	public xbean.BloodRole getData()
	{
		return xrole;
	}
	
	public boolean checkChangeDay(long now)
	{
		int nowday = DateUtil.getCurrentDay(now);
		int lastday = DateUtil.getCurrentDay(xrole.getLastfighttime());
		if(lastday == nowday)
			return false;
		xrole.setBattle1(0);
		xrole.setBattle2(0);
		xrole.setBattle3(0);
		xrole.setFailed(0);
		xrole.setCurstar(0);
		xrole.setRelivetimes(0);
		xrole.setLasthard(0);
		xrole.setItemlevel(0);
		xrole.getEffects().clear();
		if(xrole.getCurlevel() > 1)
		{
			if(nowday-lastday == 1)
			{
				xrole.setLasthard(xrole.getCurlevel());
				xrole.setCurstar(xrole.getCurlevel()/3);
			}
			xrole.setCurlevel(1);
		}
		xrole.setLastfighttime(now);
		return true;
	}
	
	public boolean relive()
	{
		if(checkChangeDay(GameTime.currentTimeMillis()))
			enterBloodBattle();
		if(xrole.getFailed() == 0)
			return false;
		if(xrole.getRelivetimes() > 0)
		{
			shuifuconfig cfg = ConfigManager.getInstance().getConf(shuifuconfig.class).get(2000);
			if(xrole.getRelivetimes() > cfg.price.size())
				return false;
			int price = cfg.price.get(xrole.getRelivetimes()-1);
			PropRole prole = PropRole.getPropRole(roleId, false);
			if(prole.delYuanBao(-price,YuanBaoConsumeType.OTHER) != -price)
				return false;
		}
		xrole.setFailed(0);
		xrole.setRelivetimes(xrole.getRelivetimes()+1);
		enterBloodBattle();
		return true;
	}
	
	public boolean enterBloodBattle()
	{
		long now = GameTime.currentTimeMillis();
		checkChangeDay(now);
		if(xrole.getCurlevel() <= 0)
		{
			PNewBattle.logger.error("Roleid = "+roleId+" curbloodlevel = "+xrole.getCurlevel());
			return false;
		}
		
		if(xrole.getFailed() == 1)
		{
			//发失败界面
			SEnterBloodFight snd = new SEnterBloodFight();
			snd.bloodinfo = getBloodBaseInfo();
			snd.battle1 = Conv.toByte(xrole.getBattle1());
			snd.battle2 = Conv.toByte(xrole.getBattle2());
			snd.battle3 = Conv.toByte(xrole.getBattle3());
			snd.relivetimes = Conv.toByte(xrole.getRelivetimes());
			xdb.Procedure.psendWhileCommit(roleId, snd);
			return true;
		}
	
		if(xrole.getCurstar() > 0)
		{
			//有战斗加属性
			SEnterBloodEffect snd = new SEnterBloodEffect();
			snd.bloodinfo = getBloodBaseInfo();
			snd.battlehard = Conv.toShort(xrole.getLasthard());
			snd.addstar = Conv.toShort(xrole.getCurstar());
			xdb.Procedure.psendWhileCommit(roleId, snd);
		}
		else
		{
			SEnterBloodFight snd = new SEnterBloodFight();
			snd.bloodinfo = getBloodBaseInfo();
			if(xrole.getBattle1() <= 0)
			{
				xrole.setBattle1(Misc.getRandomBetween(9, 24));
				xrole.setBattle2(Misc.getRandomBetween(1, 24));
				xrole.setBattle3(Misc.getRandomBetween(1, 18));
			}
			snd.battle1 = Conv.toByte(xrole.getBattle1());
			snd.battle2 = Conv.toByte(xrole.getBattle2());
			snd.battle3 = Conv.toByte(xrole.getBattle3());
			snd.relivetimes = Conv.toByte(-1);
			//item
			if(xrole.getCurlevel()/10 > xrole.getItemlevel())
			{
				//可以给物品
				
			}
			xdb.Procedure.psendWhileCommit(roleId, snd);
		}
			
		return true;
	}
	
	private BloodBaseInfo getBloodBaseInfo()
	{
		BloodBaseInfo baseinfo = new BloodBaseInfo();
		baseinfo.maxlevel = Conv.toShort(xrole.getMaxlevel());
		baseinfo.curlevel = Conv.toShort(xrole.getCurlevel());
		Float value = xrole.getEffects().get(AttrType.ARMY+2);
		baseinfo.army = (value != null)? value : 0f; 
		value = xrole.getEffects().get(AttrType.ATTACK+2);
		baseinfo.attack = (value != null)? value : 0f; 
		value = xrole.getEffects().get(AttrType.DEFEND+2);
		baseinfo.defend = (value != null)? value : 0f;
		value = xrole.getEffects().get(AttrType.SKILL+2);
		baseinfo.wisdom = (value != null)? value : 0f;
		return baseinfo;
	}
	
	public boolean fightBlood(int battlepos)
	{
		PropRole prole = PropRole.getPropRole(roleId, false);
		int level = prole.getProperties().getLevel();
		/*if(level < 20)
			return false;*/
		long now = GameTime.currentTimeMillis();
		if(checkChangeDay(now))
		{
			enterBloodBattle();//跨天了，重刷
			return true;
		}
		int bloodbattleId = 0;
		switch(battlepos)
		{
		case 1:
			bloodbattleId = xrole.getBattle1();
			break;
		case 2:
			bloodbattleId = xrole.getBattle2();
			break;
		case 3:
			bloodbattleId = xrole.getBattle3();
			break;
		}
		if(bloodbattleId <= 0)
			return false;
		SBattleXuezhan bloodcfg = ConfigManager.getInstance().getConf(SBattleXuezhan.class).get(bloodbattleId);
		if(bloodcfg == null)
			return false;
		xrole.setLasthard(battlepos);
		xrole.setLastfighttime(now);
		
		int awardId = 0;
		int curlevelitem = (xrole.getCurlevel()/10)+1;
		boolean giveaward = (curlevelitem > xrole.getItemlevel());
		if(giveaward)
		{
			awardId = Math.min(100750, LEVEL_ITEM_AWARD+curlevelitem-1);
		}
		ContinualBuff bloodbuff = chuhan.gsp.buff.Module.getInstance().createContinualBuff(BuffConstant.BUFF_BLOOD_BATTLE);
		bloodbuff.getEffects().putAll(xrole.getEffects());
		/*double battlelevel = level *((0.65/300)*xrole.getCurlevel() + 0.022*(battlepos-1) +0.35) * (1+xrole.getCurlevel()*0.0125) + xrole.getCurlevel()*xrole.getCurlevel()/500;
		if(xrole.getCurlevel() > 450)
			battlelevel = battlelevel +(xrole.getCurlevel()-450)*(xrole.getCurlevel()-450);*/
		double battlelevel = level
				* ((0.65 / 300) * xrole.getCurlevel() + 0.022 * (battlepos - 1) + 0.35)
				* (1 + xrole.getCurlevel() * 0.0125) + xrole.getCurlevel()
				* xrole.getCurlevel() / 500;
		battlelevel += Math.pow(Math.max(0, xrole.getCurlevel() - 200), 1.1);
		battlelevel += Math.pow(Math.max(0, xrole.getCurlevel() - 250), 1.2);
		battlelevel += Math.pow(Math.max(0, xrole.getCurlevel() - 300), 1.3);
		battlelevel += Math.pow(Math.max(0, xrole.getCurlevel() - 350), 1.4);
		battlelevel += Math.pow(Math.max(0, xrole.getCurlevel() - 400), 1.7);
		PNewBattle p = new PNewBattle(roleId, bloodcfg.battle, awardId, false, 0);
		p.setBattleLevel((int)battlelevel);
		p.getHostBuffs().put(bloodbuff.getId(), bloodbuff);
		if(!p.call())
			return false;

		int winround = p.getSSendBattleScript().result.winround;
		if(winround <= 0)
		{//fail
			xrole.setFailed(1);
		}
		else
		{
			if(giveaward)
			{
				//可以给物品
				xrole.setItemlevel(curlevelitem);
				//给人经验
				int addexp = 0;//((prole.getLevel() +50)/3) * Ploy.getDoubleExp();
				PAddExpProc proc = new PAddExpProc(roleId, addexp, 1, "");//给人物经验
				if(proc.call())
					p.getSSendBattleScript().result.addexp = addexp;
				//给银两
				int addmoney =  (prole.getLevel() +50)*30;
				chuhan.gsp.item.Bag bag = new chuhan.gsp.item.Bag(roleId, false);
				long realAdd = bag.addMoney(addmoney, "blood money award", 1,1);
				p.getSSendBattleScript().result.addmoney = Conv.toInt(realAdd);
			}
			int resultstar = 4-winround;
			xrole.setCurstar(resultstar * xrole.getLasthard());
			xrole.setCurlevel(xrole.getCurlevel()+xrole.getCurstar());
			if(xrole.getCurlevel() > xrole.getMaxlevel())
				xrole.setMaxlevel(xrole.getCurlevel());
			xrole.setTotalstar(xrole.getTotalstar()+ xrole.getCurstar());
			xrole.setBattle1(0);
			xrole.setBattle2(0);
			xrole.setBattle3(0);
			xdb.Procedure.pexecuteWhileCommit(new PAddBloodRank(roleId, xrole.getCurlevel()));
		}
		p.sendSSendBattleScript();
		enterBloodBattle();
		return true;
	}
	
	public boolean addEffect(int effectpos)
	{
		long now = GameTime.currentTimeMillis();
		if(checkChangeDay(now))
		{
			enterBloodBattle();//跨天了，重刷
			return true;
		}
		
		int effectId = 0;
		switch(effectpos)
		{
		case 1:
			effectId = AttrType.ARMY+2;
			break;
		case 2:
			effectId = AttrType.ATTACK+2;
			break;
		case 3:
			effectId = AttrType.DEFEND+2;
			break;
		case 4:
			effectId = AttrType.SKILL+2;
			break;
		}
		if(effectId <= 0)
			return false;
		
		int curstar = xrole.getCurstar();
		
		Float oldvalue = xrole.getEffects().get(effectId);
		if(oldvalue == null)
			xrole.getEffects().put(effectId, (float)(curstar/100.0));
		else
		{
			float addvalue = (float)(((4 - oldvalue)/4)*(curstar/100.0));
			xrole.getEffects().put(effectId, oldvalue+addvalue);
		}
		xrole.setCurstar(0);
		enterBloodBattle();
		return true;
	}
	
	public void enterBloodAward()
	{
		SEnterBloodAward snd = new SEnterBloodAward();
		snd.totalstar = xrole.getTotalstar();
		for(sxuezhanxunhuan repeatcfg : ConfigManager.getInstance().getConf(sxuezhanxunhuan.class).values())
		{
			int maxnum = snd.totalstar /repeatcfg.price;
			Integer alreadynum = xrole.getRepeatstaraward().get(repeatcfg.id);
			if(alreadynum == null)
				alreadynum = 0;
			if(maxnum - alreadynum > 0)
				snd.repeatnum.put(Conv.toByte(repeatcfg.id), maxnum - alreadynum);
		}
		for(sxuezhanguding fixcfg : ConfigManager.getInstance().getConf(sxuezhanguding.class).values())
		{
			if(fixcfg.price <= snd.totalstar && !xrole.getFixstaraward().containsKey(fixcfg.id))
			{
				snd.fixednum.add(Conv.toByte(fixcfg.id));
			}
		}
		if(Transaction.current()!= null)
			xdb.Procedure.psendWhileCommit(roleId, snd);
		else
			gnet.link.Onlines.getInstance().send(roleId, snd);
	}
	
	public boolean getBloodAward(int id, boolean isrepeat)
	{
		int sumstar = xrole.getTotalstar();
		AddItemResult addresult = null;
		if(isrepeat)
		{	
			sxuezhanxunhuan repeatcfg = ConfigManager.getInstance().getConf(sxuezhanxunhuan.class).get(id);
			int maxnum = sumstar /repeatcfg.price;
			Integer alreadynum = xrole.getRepeatstaraward().get(repeatcfg.id);
			if(alreadynum == null)
				alreadynum = 0;
			int curnum = maxnum - alreadynum;
			if(curnum <= 0)
				return false;
			xrole.getRepeatstaraward().put(repeatcfg.id, maxnum);
			ItemColumn itemColumn = Module.getItemColumnByItemId(roleId, repeatcfg.item, false);
			addresult = itemColumn.addItem(repeatcfg.item, curnum, "get_blood_award", 0);
			
		}
		else
		{
			sxuezhanguding cfg = ConfigManager.getInstance().getConf(sxuezhanguding.class).get(id);
			if(sumstar < cfg.price)
				return false;
			if(xrole.getFixstaraward().containsKey(cfg.id))
				return false;
			xrole.getFixstaraward().put(cfg.id, 1);
			ItemColumn itemColumn = Module.getItemColumnByItemId(roleId, cfg.item, false);
			addresult = itemColumn.addItem(cfg.item, 1, "get_blood_award", 0);
		}
		enterBloodAward();
		if(addresult != null)
			xdb.Procedure.psendWhileCommit(roleId, addresult.getSShowAddItem());
		return true;
	}
	
	public static void sendRankAward() {
		xbean.BloodRankList xranklist = xtable.Bloodranklist.select(1);
		if(null == xranklist) {
			return;
		}
		int i = 1;
		for(xbean.BloodRankRole bloodRankRole : xranklist.getRankers()) {
			new PBloodRankAward(bloodRankRole.getRoleid(), i).submit();
			i ++;
		}
	}
	
}
