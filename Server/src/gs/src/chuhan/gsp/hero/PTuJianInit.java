package chuhan.gsp.hero;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import org.apache.log4j.Logger;

import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.item26;
import chuhan.gsp.main.ConfigManager;
import xbean.Hero;
import xbean.HeroColumn;
import xbean.Item;

/**
 * 登录时初始化图鉴功能得到过的武将
 *
 */
public class PTuJianInit extends xdb.Procedure {
	public static final Logger logger = Logger.getLogger(PTuJianInit.class);
	private final long roleId;
	
	public PTuJianInit(long roleId) {
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		xbean.TuJianHeros tjheros = xtable.Tujianheros.select(roleId);
		if(null != tjheros && tjheros.getTujianhero().size() > 0) {//已经初始化过
			if(logger.isDebugEnabled()) {
				logger.debug("roleId = " + roleId + " 已经初始化图鉴,不再执行!");
			}
			
//			xbean.TuJianHeros tjheros = xtable.Tujianheros.select(roleId);
			java.util.LinkedList<chuhan.gsp.HeroTuJian> returntjlist = new java.util.LinkedList<chuhan.gsp.HeroTuJian>();
			for(Map.Entry<Integer, xbean.TuJianHero> entry : tjheros.getTujianhero().entrySet())
			{
				chuhan.gsp.HeroTuJian herotj = new chuhan.gsp.HeroTuJian();
				herotj.heroid = entry.getValue().getHeroid();
				herotj.flag = entry.getValue().getFlag();
				returntjlist.addFirst(herotj);
			}
			java.util.LinkedList<Integer> boxlist = new java.util.LinkedList<Integer>();
			for(java.util.Map.Entry<Integer, Integer> entry : tjheros.getTujianbox().entrySet()){
				boxlist.add(entry.getKey());
			}
			STuJianHeros sTuJianHeros = new STuJianHeros();
			sTuJianHeros.herotujian = returntjlist;
			sTuJianHeros.isnew = STuJianHeros.NOT_NEW;
			sTuJianHeros.tujianbox = boxlist;
			sTuJianHeros.tjheromaxlevel.addAll(tjheros.getTjheromaxlevel());

			
			gnet.link.Onlines.getInstance().send(roleId, sTuJianHeros);
			
			return true;
		}
		//取英雄
		HeroColumn herocol = xtable.Herocolumns.select(roleId);
		List<xbean.Hero> herolist = herocol.getHeroes();
	
		new PAddTuJianHero(roleId, herolist, STuJianHeros.IS_NEW).call();
		return true;
	}
}
