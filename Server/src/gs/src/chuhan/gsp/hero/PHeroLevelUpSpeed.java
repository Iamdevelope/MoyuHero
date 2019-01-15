
package chuhan.gsp.hero;


import java.util.LinkedList;

import chuhan.gsp.DataInit;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.award.IDManager;
import chuhan.gsp.item.item26;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;

public class PHeroLevelUpSpeed extends xdb.Procedure {
	
	protected final long roleid;
	
	private final int herokey;

	private int level;// 升级数（根据策划案为1级和5级）填写0即为使用物品
	private final int itemid; // 物品配表ID
	private final int itemnum; // 物品使用数量
	

	public PHeroLevelUpSpeed(long roleid, int herokey, int level, int itemid, int itemnum) {

		this.roleid = roleid;
		this.herokey = herokey;
		this.level = level;
		this.itemid = itemid;
		this.itemnum = itemnum;
	}

	@Override
	public boolean process() {
		chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleid, false);
		if(prole == null)
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
//			return false;

		
		HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
		if (null == herocol)
			return false;
		int result = SHeroLevelUpSpeed.END_OK;
//		for(int herokey : this.herokeylist)
//		{
			Hero hero = herocol.getHByHKey(herokey);
			if(hero == null){
				Message.psendMsgNotify(roleid, 135);
				return false;
			}
			
			//使用物品
			if (level == 0){
				item26 init = ConfigManager.getInstance().getConf(item26.class).get(itemid);
				if(init == null || init.getHeroExp() <= 0){
					Message.psendMsgNotify(roleid, 135);
					return false;
				}
				if(!DropManager.getInstance().useDel(itemid, itemnum, roleid, "herolevelup")){
					Message.psendMsgNotify(roleid, 135);
					return false;
				}
				java.util.LinkedList<Integer> addHeroId = new java.util.LinkedList<Integer>();
				addHeroId.add(herokey);
				PAddExpHero paddexphero = new PAddExpHero(roleid,addHeroId,
						init.getHeroExp() * itemnum,PAddExpHero.OTHER,"");
				boolean addresult = paddexphero.call();
				if( !addresult ){
					psend(roleid, new SHeroLevelUpSpeed(SHeroLevelUpSpeed.END_NOT_OK));	
					return false;
				}else{
					psendWhileCommit(roleid, new SHeroLevelUpSpeed(result));	
					return true;
				}				
			}
			
			if(hero.getLevel() >= hero.getiHeroInfo().getMaxLevel()){
				Message.psendMsgNotify(roleid, 135);
				LogManager.logger.error("英雄超过等级上限。roleid："+roleid+"herokey+herolevel:"+hero.getxHeroInfo().getKey()+"+"+hero.getxHeroInfo().getHerolevel()+"maxLevel:"+hero.getiHeroInfo().getMaxLevel());
				return false;
			}
			if(hero.getLevel() + level > hero.getiHeroInfo().getMaxLevel()){
				level = hero.getiHeroInfo().getMaxLevel() - hero.getLevel();
			}
			if(hero.getLevel() + level > prole.getLevel()){
				level = prole.getLevel() - hero.getLevel();
			}
			//加经验物品(临时写法,应该配置在10表中)
			LinkedList<Integer> expItemList = new LinkedList<Integer>();
			expItemList.add(1402030010);
			expItemList.add(1402030011);
			expItemList.add(1402030012);
			expItemList.add(1402030013);
			expItemList.add(1402030014);
			expItemList.add(1402030015);
			boolean isDelItem = false;
			for(int i = 0;i<level;i++){
				for(Integer addItemId : expItemList){
					item26 init = ConfigManager.getInstance().getConf(item26.class).get(addItemId);
					boolean isLevelUp = false;
					while(DropManager.getInstance().useDel(addItemId, 1, roleid, "herolevelup")){
						isDelItem = true;
						hero.setExp(hero.getExp() + init.getHeroExp());
						if(hero.levelUp(prole.getLevel())){
							isLevelUp = true;
							break;
						}
					}
					if(isLevelUp){
						break;
					}
				}
			}
			if( !isDelItem ){
				psend(roleid, new SHeroLevelUpSpeed(SHeroLevelUpSpeed.END_NOT_OK));	
				return false;
			}

			/*int needExp = 0;
			int firstNeedExp = 0;
			for(int i = 0;i<level;i++){
				int exp = Hero.getExpMax(hero.getxHeroInfo().getHerolevel() + i, hero.getiHeroInfo().getExpNum());
				if(i == 0){
					firstNeedExp = exp;
				}
				needExp = needExp + exp;
			}
			needExp = needExp - hero.getxHeroInfo().getHeroexp();
			firstNeedExp = firstNeedExp - hero.getxHeroInfo().getHeroexp();
			if(needExp > 0){
				if(prole.getProperties().getJyjiejing() < needExp){
					needExp = prole.getProperties().getJyjiejing();
					if(firstNeedExp > needExp){
						result = SHeroLevelUpSpeed.END_NOT_OK;
					}
				}
				if(needExp == 0){
					Message.psendMsgNotify(roleid, 135);
					return false;
				}
				if(needExp * (-1) != prole.delZiYuan(needExp * (-1), 0, IDManager.EXPJIEJING)){
					Message.psendMsgNotify(roleid, 135);
					return false;
				}
				java.util.LinkedList<Integer> addHeroId = new java.util.LinkedList<Integer>();
				addHeroId.add(herokey);
				PAddExpHero paddexphero = new PAddExpHero(roleid,addHeroId,
						needExp,
						PAddExpHero.OTHER,"");
				boolean addresult = paddexphero.call();
				if( !addresult ){
					psend(roleid, new SHeroLevelUpSpeed(SHeroLevelUpSpeed.END_NOT_OK));	
					return false;
				}
			}	*/	
//		}
			herocol.refreshHero(herokey);
		ActivityManager.getInstance().addHYTaskOver(roleid, HuoyueColumns.HERO_LEVELUP, 1);
		psendWhileCommit(roleid, new SHeroLevelUpSpeed(result));	
		return true;	
	}


}
