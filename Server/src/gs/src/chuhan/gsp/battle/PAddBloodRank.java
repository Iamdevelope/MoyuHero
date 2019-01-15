package chuhan.gsp.battle;

import java.util.Iterator;

import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.DateUtil;

public class PAddBloodRank extends xdb.Procedure{
	public static int MIN_ON_RANK_LEVEL = 2;
	public static int RANK_MAX_NUM = 50;
	private final long roleId;
	private final int maxlevel;
	public PAddBloodRank(long roleId, int maxlevel) {
		this.roleId = roleId;
		this.maxlevel = maxlevel;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		if(maxlevel < MIN_ON_RANK_LEVEL)
			return true;
		
		long now = GameTime.currentTimeMillis();
		/**
		 * edit by ranbo 2013-07-20以前是一周清一次，现在改为一天清一次
		 * 为了不加新字段，下面的xbean.BloodRankList里的curweek字段意义变为了天
		 */
		int curday = DateUtil.getCurrentDay(now);
		xbean.BloodRankList xranklist = xtable.Bloodranklist.get(1);
		if(xranklist == null)
		{
			xranklist = xbean.Pod.newBloodRankList();
			xranklist.setCurweek(curday);
			xtable.Bloodranklist.insert(1, xranklist);
		}
		
		if(xranklist.getCurweek() != curday)
		{
			xranklist.getRankers().clear();
			xranklist.setCurweek(curday);
		}
		
		//先找到以前的删除
		for(Iterator<xbean.BloodRankRole> it = xranklist.getRankers().iterator();it.hasNext();)
		{
			xbean.BloodRankRole xrole = it.next();
			if(xrole.getRoleid() == roleId)
			{
				if(xrole.getMaxlevel() >= maxlevel)
					return true;//原先就在榜上而且比现在还高
				it.remove();
				break;
			}
		}
		
		int i = 0;
		for(xbean.BloodRankRole xrole : xranklist.getRankers())
		{
			if(xrole.getMaxlevel() < maxlevel)
				break;
			i++;
		}
		if(i >= RANK_MAX_NUM)
			return false;//榜上已满
		xbean.BloodRankRole xrole = xbean.Pod.newBloodRankRole();
		xrole.setRoleid(roleId);
		xrole.setMaxlevel(maxlevel);
		xranklist.getRankers().add(i, xrole);
		if(xranklist.getRankers().size() > RANK_MAX_NUM)
		{
			xranklist.getRankers().remove(xranklist.getRankers().size()-1);
		}
		return true;
	}
	
}
