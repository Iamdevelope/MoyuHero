
package chuhan.gsp.hero;

import java.util.ArrayList;
import java.util.List;

import chuhan.gsp.DataInit;
import chuhan.gsp.hero.OldHero;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.util.Conv;

public class PAddExpHero extends xdb.Procedure {

	public final static int BATTLE = 1;		//战斗奖励
	public final static int OTHER = 99;   //其他
	
	protected final long roleid;
	
	private final java.util.LinkedList<Integer> herokeylist;

	private final int addexp;
	
	private int reason;
	
	private String hint;

	public PAddExpHero(long roleid, java.util.LinkedList<Integer> herokeylist, int addexp, int reason, String hint) {

		this.roleid = roleid;
		this.herokeylist = herokeylist;
		this.addexp = addexp;
		this.reason = reason;
		this.hint = hint;
	}

	@Override
	public boolean process() {

		chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleid, false);
		if(prole == null)
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
		
		if (addexp == 0)
			return false;
		
		HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
		if (null == herocol)
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
//			return false;
		
		for(int herokey : this.herokeylist)
		{
			Hero hero = herocol.getHByHKey(herokey);
			if(hero == null)
				continue;
			if(hero.getLevel() >= hero.getiHeroInfo().getMaxLevel())
			{
				LogManager.logger.error("英雄超过等级上限。roleid："+roleid+"herokey+herolevel:"+hero.getxHeroInfo().getKey()+"+"+hero.getxHeroInfo().getHerolevel()+"maxLevel:"+hero.getiHeroInfo().getMaxLevel());
				//TODO 不再获得经验
				//return false;
				continue;
			}
			
			final int curexp = hero.getExp();
			if (curexp + addexp <= 0){
				hero.setExp(0);
			}
			else if(curexp + addexp >= Hero.getExpMax(hero.getLevel(),hero.getiHeroInfo().getBorn())){
				hero.setExp(curexp + addexp);
				int i = 1;
				
				while(hero.getExp() >= Hero.getExpMax(hero.getLevel(),hero.getiHeroInfo().getBorn()))
				{
					boolean succ = hero.levelUp(prole.getLevel());
					if(succ && hero.getLevel() >= hero.getiHeroInfo().getMaxLevel())
					{
						//设置图鉴数据
						List<xbean.Hero> heros = new ArrayList<xbean.Hero>();
						heros.add(hero.getxHeroInfo());
						new PAddTuJianHero(this.roleid, heros, STuJianHeros.IS_NEW).call();
					}
					if (!succ)
					{
						if(hero.getLevel() >= hero.getiHeroInfo().getMaxLevel() 
								|| hero.getLevel() >=prole.getLevel()){
							LogManager.logger.error("英雄超过等级上限。roleid："+roleid+"herokey+herolevel:"+hero.getxHeroInfo().getKey()+"+"+hero.getxHeroInfo().getHerolevel()+"maxLevel:"+hero.getiHeroInfo().getMaxLevel());
							hero.setExp(Hero.getExpMax(hero.getLevel(),hero.getiHeroInfo().getBorn()));//不成功且超经验了则平经验
						}
						break;
					}
					i++;
					if(i > hero.getiHeroInfo().getMaxLevel())
					{
						Hero.logger.error("hero一次升级次数过多");
						break;
					}
				}
			}
			else{
				hero.setExp(curexp + addexp);
			}
			psendWhileCommit(roleid, new SRefreshHero(hero.getProtocolHero()));	
		}
		return true;	
	}

	public static void logAddExp(long roleId, long addexp, String hint, int reason)
	{
		
	}

}
