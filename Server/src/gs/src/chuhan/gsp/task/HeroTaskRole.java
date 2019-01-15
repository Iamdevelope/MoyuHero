package chuhan.gsp.task;

import java.util.HashSet;
import java.util.Map;
import java.util.Set;

import gnet.link.Onlines;
import xdb.Transaction;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.SRefreshHeroExtExp;
import chuhan.gsp.attr.YuanBaoConsumeType;
import chuhan.gsp.hero.OldHero;
import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.item.Bag;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;

public class HeroTaskRole {
	
	public static int MAX_POS = 4;
	
	public static HeroTaskRole getHeroTaskRole(long roleId, boolean readonly)
	{
		/*if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造LadderRole时，角色 "+roleId+" 不存在。");
		
		xbean.HeroTaskRole xrole = null;
		if(readonly)
			xrole = xtable.Herotaskroles.select(roleId);
		else
			xrole = xtable.Herotaskroles.get(roleId);
		if(xrole == null)
		{
			if(readonly)
				xrole = xbean.Pod.newHeroTaskRoleData();
			else
			{
				xrole = xbean.Pod.newHeroTaskRole();
				xtable.Herotaskroles.insert(roleId, xrole);
			}
		}*/
		return null;
	}
	
	
	
	private final long roleId;
	public final boolean readonly;
//	private final xbean.HeroTaskRole xrole;
	public HeroTaskRole(long roleId,boolean readonly) {
		this.roleId = roleId;
		this.readonly = readonly;
/*		this.xrole = xrole;
		long now = GameTime.currentTimeMillis();
		Set<Integer> timeoutkeys = new HashSet<Integer>();
		for(Map.Entry<Integer, Long> entry : xrole.getTaskposes().entrySet())
		{
			if(entry.getValue()<= now)
				timeoutkeys.add(entry.getKey());
		}
		for(int key : timeoutkeys)
			xrole.getTaskposes().remove(key);
		checkChangeDay(now);*/
	}
	
	public long getRoleId()
	{
		return roleId;
	}
	
/*	public xbean.HeroTaskRole getData()
	{
		return xrole;
	}*/
	
	public boolean checkChangeDay(long now)
	{
//		if(DateUtil.inTheSameDay(xrole.getRefreshtime(),now))
//			return false;
//		xrole.getEndtask().clear();
//		xrole.setRefreshtime(now);
		return true;
	}
	public void sendTasks()
	{
		SSendHeroTimeTasks snd = new SSendHeroTimeTasks();
/*		for(int taskid : xrole.getEndtask())
		{
			snd.endtasks.add(Conv.toByte(taskid));
		}*/
		
		for(int i = 1; i <= MAX_POS; i ++)
		{
			HeroTimeTask t = new HeroTimeTask();
			t.pos = Conv.toByte(i);
			Long time = 0L;//xrole.getTaskposes().get(i);
			if(time != null)
			{
				t.endtime = time;
				snd.proceedtasks.add(t);
			}
		}
		if(Transaction.current() != null)
			xdb.Procedure.psendWhileCommit(roleId, snd);
		else
			Onlines.getInstance().send(roleId, snd);
	}
	
	public boolean beginTask(int pos, int taskid, int herokey)
	{
		if(!validatePos(pos))
			return false;
		
//		if(xrole.getTaskposes().containsKey(pos))
//			return false;//该位置正在冷却
		
		stimetaskconfig cfg = ConfigManager.getInstance().getConf(stimetaskconfig.class).get(taskid);
		if(cfg == null)
			return false;
		
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		OldHero hero = herocol.getHero(herokey);
		if(hero == null)
			return false;
		
		PropRole prole = PropRole.getPropRole(roleId, false);
		int viplv = prole.getVipLevel();
		if(viplv < cfg.vip)
			return false;
		
		if(cfg.pricetype == 1)
		{
			int price = cfg.price * prole.getLevel();
			Bag bag = new Bag(roleId, false);
			if(bag.addMoney(-price, "hero_task") != -price)
			{
				//TODO  notify
				return false;
			}
		}
		else
		{
			int price = cfg.price;
			if(prole.delYuanBao(-price,YuanBaoConsumeType.OTHER) != -price)
				return false;
		}
		
		long endtime = GameTime.currentTimeMillis()+cfg.time * 60000;
//		xrole.getTaskposes().put(pos, endtime);
//		xrole.getEndtask().add(taskid);
		int addexp = (cfg.grow * prole.getLevel())/2;
		addexp = specialActivity(addexp);
		/*//by yanglk  hero
		hero.addExtExp(addexp);
		SRefreshHeroExtExp sndexp = new SRefreshHeroExtExp(herokey,hero.getHeroInfo().getExtexp());
		xdb.Procedure.psendWhileCommit(roleId, sndexp);
		SRefreshHeroTimeTask snd = new SRefreshHeroTimeTask();
		snd.taskinfo.pos = Conv.toByte(pos);
		snd.taskinfo.taskid = Conv.toByte(taskid);
		snd.taskinfo.endtime = endtime;
		snd.herokey = herokey;
		xdb.Procedure.psendWhileCommit(roleId, snd);
		*/
		return true;
	}
	
	public void clearAll()
	{
//		xrole.getTaskposes().clear();
//		xrole.getEndtask().clear();
		sendTasks();
	}
	
	private int specialActivity(int exp)
	{
		long now = GameTime.currentTimeMillis();
		if(DateUtil.getCurrentWeekDay(now) == 3)
			return 2*exp;
		return exp;
	}
	
	public boolean validatePos(int pos)
	{
		if(pos <= 0)
			return false;
		if(pos > MAX_POS)
			return false;
		xbean.Properties xprop = xtable.Properties.get(roleId);
		/*if(xprop.getLevel() < 15)
			return false;*/
		if(pos == 3)
		{
			return xprop.getLevel() >= 32;
		}
		if(pos == 4)
		{
			return xprop.getViplv() >= 6;
		}
		return true;
	}
}
