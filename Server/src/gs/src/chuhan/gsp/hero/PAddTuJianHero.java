package chuhan.gsp.hero;

import java.util.List;
import java.util.Map;

import chuhan.gsp.DataInit;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.game.illustratehandbook40;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.CollectionUtil;

/**
 * 添加图鉴中获得的武将
 * 在添加武将或魂魄的时候调用
 *
 */
public class PAddTuJianHero extends xdb.Procedure {
	private final long roleId;
	private final List<xbean.Hero> herolist;
	private final int isNew;
	
	public PAddTuJianHero(long roleId, List<xbean.Hero> herolist, int isNew) {
		this.roleId = roleId;
		this.herolist = herolist;
		this.isNew = isNew;
	}
	
	@Override
	protected boolean process() throws Exception {
		xbean.Properties xprop = xtable.Properties.get(roleId);
		if(xprop == null)
		{
			throw new IllegalArgumentException("构造角色时，角色 "+roleId+" 不存在。");
//			return false;
		}
		xbean.TuJianHeros tuJianHeros = xtable.Tujianheros.get(roleId);
		if(null == tuJianHeros) {
			tuJianHeros = xbean.Pod.newTuJianHeros();
			xtable.Tujianheros.insert(roleId, tuJianHeros);
		}
		
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
		java.util.LinkedList<chuhan.gsp.HeroTuJian> returntjlist = new java.util.LinkedList<chuhan.gsp.HeroTuJian>();
		for(xbean.Hero hero : this.herolist){
			xbean.TuJianHero tjheroNew = xbean.Pod.newTuJianHero();
			tjheroNew.setHeroid(hero.getHeroid());
			Hero heroall = herocol.getHByHKey(hero.getKey());
			if( hero.getHerolevel() >= heroall.getiHeroInfo().getMaxLevel() )		
				tjheroNew.setFlag(1);
			else
				tjheroNew.setFlag(0);
			
			boolean isChange = false;
			xbean.TuJianHero tjheroOld = tuJianHeros.getTujianhero().get(hero.getHeroid());
			if(tjheroOld == null){
				tuJianHeros.getTujianhero().put(tjheroNew.getHeroid(), tjheroNew);
				returntjlist.addFirst(new chuhan.gsp.HeroTuJian(tjheroNew.getHeroid(),tjheroNew.getFlag()));
				isChange = true;
			}else{
				if(tjheroOld.getFlag() != tjheroNew.getFlag()){
					tjheroOld.setFlag(tjheroNew.getFlag());
					returntjlist.addFirst(new chuhan.gsp.HeroTuJian(tjheroNew.getHeroid(),tjheroNew.getFlag()));
					isChange = true;
				}
			}
			//满级增加相应勋章
			if(isChange && tjheroNew.getFlag() == 1){
				java.util.TreeMap<Integer, illustratehandbook40> initMap = ConfigManager.getInstance().
						getConf(illustratehandbook40.class);
				for(Map.Entry<Integer, illustratehandbook40> entry : initMap.entrySet()){
					if( entry.getValue().getContentId() == tjheroNew.getHeroid() ){
						DropManager.getInstance().dropAddByOther(entry.getValue().getReward(), 1, 0, 0, roleId, LogBehavior.TUJIANGET);
						tuJianHeros.getTjheromaxlevel().add(tjheroNew.getHeroid());
						break;
					}
				}
			}
		}
		if(returntjlist.size() != 0){
			STuJianHeros send = new STuJianHeros();
			send.herotujian = returntjlist;
			send.isnew = this.isNew;
			send.tjheromaxlevel.addAll(tuJianHeros.getTjheromaxlevel());
			xdb.Procedure.psendWhileCommit(this.roleId, send);
		}
		
		return true;
	}
	
	
}
