package chuhan.gsp.hero;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import com.goldhuman.Common.Marshal.MarshalException;
import com.goldhuman.Common.Marshal.OctetsStream;

import chuhan.gsp.attr.player03;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.types.ArmorItem;
import chuhan.gsp.item.types.EquipItem;
import chuhan.gsp.item.types.HorseItem;
import chuhan.gsp.item.types.SkillItem;
import chuhan.gsp.item.types.WeaponItem;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;

public class OldHeroColumn {
	
	public final static int MAX_TROOP_NUM = 5;
	
	public static Logger logger = Logger.getLogger(OldHeroColumn.class);
	
	public static OldHeroColumn getHeroColumn(long roleId, boolean readonly)
	{
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造HeroColumn时，角色 "+roleId+" 不存在。");
		
		xbean.HeroColumn herocol = null;
		if(readonly)
			herocol = xtable.Herocolumns.select(roleId);
		else
			herocol = xtable.Herocolumns.get(roleId);
		if(herocol == null)
		{
			if(readonly)
				herocol = xbean.Pod.newHeroColumnData();
			else
			{
				herocol = xbean.Pod.newHeroColumn();
				xtable.Herocolumns.insert(roleId, herocol);
			}
		}
		return new OldHeroColumn(roleId, herocol, readonly);
	}
	
	final public long roleId;
	final xbean.HeroColumn xcolumn;
	final boolean readonly;
	
	private OldHeroColumn(long roleId, xbean.HeroColumn xcolumn, boolean readonly) {
		this.roleId = roleId;
		this.xcolumn = xcolumn;
		this.readonly = readonly;
	}
	

	public OldHero getHero(int herokey)
	{
		xbean.Hero xhero = xcolumn.getHeroes().get(herokey);
		if(xhero == null)
			return null;
		return OldHero.getHero(xhero, readonly);
	}
	
	
	public OldHero createAddHero(int heroId, int lv)
	{
		xbean.Hero xhero = OldHero.createHero(heroId, lv);
		OldHero hero = this.addHero(xhero);
		xdb.Procedure.psendWhileCommit(roleId, new SRefreshHero(hero.getProtocolHero()));
		return hero;
	}
	
	/**
	 * 如果已经存在，返回旧的
	 * @param Heroinfo
	 * @return
	 */
	public OldHero addHero(xbean.Hero xhero)
	{
		/*//by yanglk  hero
		xhero.setKey(nextKey());
		xcolumn.getHeroes().put(xhero.getKey(), xhero);
		Hero.logger.debug("Role："+roleId+"添加武将："+xhero.getId());
		BeautyRole.activeBeauty(roleId, xhero.getId());
		//设置图鉴数据
		List<Integer> heroIds = new ArrayList<Integer>();
		heroIds.add(xhero.getId());
		new PAddTuJianHero(roleId, heroIds).call();
		
		return getHero(xhero.getKey());
		*/
		return null;
	}
	
	public int nextKey()
	{
		/*//by yanglk  hero
		xcolumn.setNextkey(xcolumn.getNextkey()+1);
		return xcolumn.getNextkey();
		*/
		return 0;
	}
	
	public Map<Integer,OldHero> getHeros()
	{
		Map<Integer, OldHero> heros = new HashMap<Integer, OldHero>();
		/*//by yanglk  hero
		for(xbean.Hero xhero : xcolumn.getHeroes().values())
		{
			heros.put(xhero.getKey(), Hero.getHero(xhero, readonly));
		}
		*/
		return heros;
	}
	
	public boolean containHero(int heroId)
	{
		for(OldHero hero : getHeros().values())
		{
			//by yanglk  hero			if( heroId == hero.getId())
			//by yanglk  hero				return true;
		}
		return false;
	}
	
	public List<chuhan.gsp.Hero> getProtocolHeros()
	{
		List<chuhan.gsp.Hero> datas = new LinkedList<chuhan.gsp.Hero>();
		for(OldHero hero : getHeros().values())
		{
			datas.add(hero.getProtocolHero());
		}
		return datas;
	}
	
	public OldHero removeHero(int key)
	{
		return removeHero(key,true);
	}
	
	/**
	 * 武将是否在阵型中(包括副将)
	 * @param key
	 * @return
	 */
	public boolean heroInTroops(int key) {
		List<OldHero> heros = getAllTropHeros();
		for(OldHero hero : heros) {
			//by yanglk  hero			if(hero.getHeroInfo().getKey() == key) {
			//by yanglk  hero				return true;
			//by yanglk  hero}
		}
		return false;
	}
	
	/**
	 * 删除英雄
	 * @param key
	 * @return
	 */
	public OldHero removeHero(int key, boolean isnotify)
	{
		OldHero hero = getHero(key);
		if(hero == null)
			return null;
		if(heroInTroop(key))
			return null;
		if(heroInTroops(key)) {
			return null;
		}
		
		
		xcolumn.getHeroes().remove(key);
		SRemoveHero remove = new SRemoveHero();
		remove.herokey.add(key);
		xdb.Procedure.psendWhileCommit(roleId, remove);
		return hero;
	}
	
	
	
	/*************************************************************对军队的操作*******************************************************************/
	
	public List<OldTroop> getTroops(){
		List<OldTroop> troops = new LinkedList<OldTroop>();
		//by yanglk  hero		for(int i = 0;i < xcolumn.getTroops().size(); i++)
		//by yanglk  hero			troops.add(getTroop(i+1));
		return troops;
	}
	
	/**
	 * 返回所有军队的所有武将(包括副将)
	 * @return
	 */
	public List<OldHero> getAllTropHeros() {
		List<OldHero> heros = new ArrayList<OldHero>();
		List<OldTroop> troops = getTroops();
		for(OldTroop troop : troops) {
			OldHero hero = null;//*by yanglk trooptroop.getHero();
			if(null != hero) {
				heros.add(hero);//加入主将
			}
			
			/*by yanglk troop
			List<Integer> viceheroKeys = troop.getTroopInfo().getViceheros();
			for(int key : viceheroKeys) {//加入所有副将
				Hero vhero = getHero(key);
				if(null != vhero) {
					heros.add(vhero);
				}
			}
			*/
		}
		
		return heros;
	}
	
	/**
	 * 获取一只军队，位置从1开始
	 * @param trooppos
	 * @return
	 */
	public OldTroop getTroop(int trooppos)
	{
		/*//by yanglk  hero
		if(trooppos <=0 || xcolumn.getTroops().size() < trooppos)
			return null;
		xbean.Troop xtroop = xcolumn.getTroops().get(trooppos-1);
		return TroopOld.getTroop(this, trooppos, xtroop, readonly);
		*/
		return null;
	}
	
	public boolean heroInTroop(int herokey)
	{
		return getTroopByHero(herokey) != null;
	}
	
	public OldTroop getTroopByHero(int herokey)
	{
		for(OldTroop troop : getTroops())
		{
			//by yanglk troop
//			if(troop.getTroopInfo().getHeroid() == herokey)
//				return troop;
		}
		return null;
	}
	
	public OldTroop getTroopByHeroId(int heroId)
	{
		for(OldTroop troop : getTroops())
		{
			/*by yanglk troop
			Hero hero = getHero(troop.getTroopInfo().getHeroid());
			if(hero != null && hero.getId() == heroId)
				return troop;
				*/
		}
		return null;
	}

	public OldHero getHeroByTroop(int trooppos)
	{
		OldTroop troop = getTroop(trooppos);
		if(troop == null)
			return null;
		return null;
///*by yanglk troop		return getHero(troop.getTroopInfo().getHeroid());
	}
	
	public List<chuhan.gsp.Troop> getProtocolTroops()
	{
		ItemColumn itemcol = chuhan.gsp.item.Module.getItemColumn(roleId, BagTypes.SKILL, false);
		List<chuhan.gsp.Troop> troops = new LinkedList<chuhan.gsp.Troop>();
		for(OldTroop troop : getTroops())
		{
			List<Integer> skillkeys = new LinkedList<Integer>();
//			skillkeys.addAll(troop.getTroopInfo().getSkills());
			for(Integer skillitemkey : skillkeys)
			{
				/*/*by yanglk troop
				if(itemcol.getItem(skillitemkey) == null)
					troop.getTroopInfo().getSkills().remove(skillitemkey);
					*/
			}
			troops.add(troop.getProtocolTroop());
		}
		return troops;
	}
	
	public void refreshTroops()
	{
		SRefreshTroops snd = new SRefreshTroops();
		snd.troops.addAll(getProtocolTroops());
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	public OldTroop addTroop(int herokey)
	{
		OldHero hero = getHero(herokey);
		if(hero == null)
			return null;
		int pos = getNextEmptyTroopPos();
		if(pos <= 0)
			return null;
		if(heroInTroop(herokey))
			return null;
		int linkHeroId = hero.getLinkHeroId();
		if(getTroopPosByHeroId(linkHeroId) > 0) {
			Message.psendMsgNotify(roleId, 135);
			return null;
		}
		//by yanglk  hero		if(getTroopPosByHeroId(hero.getHeroInfo().getId()) > 0)
		{
			Message.psendMsgNotify(roleId, 135);
			//by yanglk  hero			return null;
		}
		OldTroop vicetroop =null; //by yanglk  hero   getTroopByViceHeroId(hero.getId());
		if(vicetroop != null)
		{
			/*by yanglk troop
			if(!vicetroop.getTroopInfo().getViceheros().remove((Integer)herokey))
			{
				Message.psendMsgNotify(roleId, 135);
				return null;//已经参战过相同类型的副将了
			}
				*/
		}
		xbean.Troop xtroop = OldTroop.createTroop(herokey);
		//by yanglk  hero		xcolumn.getTroops().add(xtroop);
		return getTroop(pos);
	}
	
	public int getTroopPosByHeroId(int heroId)
	{
		int i = 1;
		for(OldTroop troop : getTroops())
		{
			/*by yanglk troop
			Hero hero = getHero(troop.getTroopInfo().getHeroid());
			if(hero != null && hero.getId() == heroId)
				return i;
			i++;
			*/
		}
		return -1;
	}
	
	public boolean setTroopList(List<Integer> trooplist, List<Integer> herokeys, List<Integer> viceheros) throws MarshalException
	{
		//这一段用来判断一个武将不能上多次
		int num_1 = 0;
		for(int heroId : herokeys) {
			if(heroId == -1)
				num_1 ++;
		}
		for(int heroId : viceheros) {
			if(heroId == -1)
				num_1 ++;
		}
		
		/* add by ranbo 2013-05-30 判断所有武将的互斥关系--
		 * start--*/
		Set<Integer> allHeroKeys = new HashSet<Integer>(herokeys);//记录了所有传入的武将 <=0的表示空位
		allHeroKeys.addAll(viceheros);
		allHeroKeys.remove(-1);
		if(allHeroKeys.size() + num_1 !=  + herokeys.size() + viceheros.size()) {
			return false;
		}
		for(int currentKey : allHeroKeys) {
			OldHero currentHero = getHero(currentKey);
			//遍历所有武将判断是否与当前武将互斥
			for(int targetHeroKey : allHeroKeys) {
				if(targetHeroKey == currentKey) {//是自己则不对比
					continue;
				}
				OldHero targetHero = getHero(targetHeroKey);
				if(null == targetHero || null == currentHero) {
					continue;
				}
				if(currentHero.isLinkHero(targetHero)) {//是同质武将则互斥
					return false;
				}
			}
		}
		/* end--*/
		
		Set<Integer> exam = new HashSet<Integer>();
		if(trooplist.size() != herokeys.size())
			return false;
		if(trooplist.size() > getMaxTroopNum())
			return false;
		List<xbean.Troop> newlist = new LinkedList<xbean.Troop>();
		for(int oldpos : trooplist)
		{
			if(oldpos > 0)
			{
				if(exam.contains(oldpos))
					return false;
				exam.add(oldpos);
			}
			OldTroop troop = getTroop(oldpos);
			xbean.Troop xtroop = xbean.Pod.newTroop();
			
			/*by yanglk troop
			if(troop != null)
				xtroop.unmarshal(troop.getTroopInfo().marshal(new OctetsStream()));
				*/
			newlist.add(xtroop);
		}
		//by yanglk  hero		xcolumn.getTroops().clear();
		//by yanglk  hero		xcolumn.getTroops().addAll(newlist);
		
		exam.clear();
		exam.addAll(herokeys);
		//by yanglk  hero		if(exam.size() == trooplist.size() && herokeys.size() == xcolumn.getTroops().size())
		{
			int i = 0;
			//by yanglk  hero		for(xbean.Troop xtroop : xcolumn.getTroops())
			{
				/*by yanglk troop
				xtroop.setHeroid(herokeys.get(i));\
				*/
				i++;
			}
		}
		setViceHeros(viceheros);
		refreshTroops();
		return true;
	}
	
	public OldTroop getTroopByViceHeroKey(int herokey)
	{
		for(OldTroop troop : getTroops())
		{
			/*by yanglk troop
			for(int viceherokey : troop.getTroopInfo().getViceheros())
			{
				if(viceherokey == herokey)
					return troop;
			}
			*/
		}
		return null;
	}
	
	public OldTroop getTroopByViceHeroId(int heroid)
	{
		for(OldTroop troop : getTroops())
		{
			/*by yanglk troop
			for(int viceherokey : troop.getTroopInfo().getViceheros())
			{
				Hero hero = getHero(viceherokey);
				if(hero != null && hero.getId() == heroid)
					return troop;
			}
			*/
		}
		return null;
	}

	public void setViceHeros(List<Integer> viceheros)
	{
		List<Integer> exam = new LinkedList<Integer>();
		for(int hkey : viceheros)
		{
			if(hkey > 0 && exam.contains(hkey))
				return;
			exam.add(hkey);
		}
		for(int viceherokey : viceheros)
		{
			if(viceherokey < 0)
				continue;
			OldHero hero = getHero(viceherokey);
			if(hero == null)
				continue;
			//by yanglk  hero			if(getTroopByHeroId(hero.getHeroInfo().getId()) != null)
			//by yanglk  hero				return;
		}
		int lv = xtable.Properties.get(roleId).getLevel();
		player03 lvcfg = ConfigManager.getInstance().getConf(player03.class).get(lv);
//		if(viceheros.size() != lvcfg.fujiang)
//			return;
		int i = 0;
		//by yanglk  hero		for(xbean.Troop xtroop : xcolumn.getTroops())
		{
			/*by yanglk troop
			xtroop.getViceheros().clear();
			*/
			if(viceheros.size() <= i)
			{
				i++;
				//by yanglk  hero				continue;
			}
			int hero1 = viceheros.get(i);
			if(hero1 <= 0)
			{
				i++;
				//by yanglk  hero				continue;
			}
			/*by yanglk troop
			xtroop.getViceheros().add(hero1);
			if(viceheros.size() <= i+MAX_TROOP_NUM)
			{
				i++;
				continue;
			}
			int hero2 = viceheros.get(i+MAX_TROOP_NUM);
			if(hero2 > 0)
				xtroop.getViceheros().add(hero2);
			i++;
			*/
		}
		
	}
	
	
	public int getNextEmptyTroopPos()
	{
		//by yanglk  hero		if(xcolumn.getTroops().size() >= getMaxTroopNum())
			return -1;
			//by yanglk  hero		return xcolumn.getTroops().size() + 1;
	}
	
	public int getMaxTroopNum()
	{
		int level = xtable.Properties.selectLevel(roleId);
		return 0;//ConfigManager.getInstance().getConf(SLevelConfig.class).get(level).canzhan;
	}
	
	public boolean switchHero(int trooppos, int herokey)
	{
		OldTroop troop = getTroop(trooppos);
		if(troop == null)
			return false;
		OldHero hero = getHero(herokey);
		if(hero == null)
			return false;
		if(heroInTroop(herokey))
			return false;
		
//*by yanglk troop		troop.setHerokey(herokey);
		ItemColumn itemcol = chuhan.gsp.item.Module.getItemColumn(roleId, BagTypes.SKILL, false);
		List<Integer> skills = new LinkedList<Integer>();
		/*by yanglk troop
		skills.addAll(troop.getTroopInfo().getSkills());
		for(int skillkey : skills)
		{
			 SkillItem skillitem = (SkillItem)(itemcol.getItem(skillkey));
			 if(skillitem == null)
				 continue;
			 int skillid = ((SkillItem)skillitem).getItemid();
			 if(skillid == hero.getConfig().getBindskill())
				 troop.getTroopInfo().getSkills().remove((Integer)skillkey);
		}
		/*Edit by ranbo 2013-05-31 换完之后才判断，同质武将不能同时参战*/
		List<OldHero> allHeros = getAllTropHeros();
		for(OldHero h : allHeros) {
			//by yanglk  hero			if(h.getHeroInfo().getKey() == herokey) {// 是自己就不判断了
			//by yanglk  hero				continue;
			//by yanglk  hero			}
			if(hero.isLinkHero(h)) {
				Message.psendMsgNotify(roleId, 135);
				return false;//已经参战过相同类型的武将了
			}
		}
		
		refreshTroops();
		return true;
	}
	
	public OldTroop getTroopByEquip(EquipItem equipitem)
	{
		
		for(OldTroop troop : getTroops())
		{
			/*by yanglk troop
			if(equipitem instanceof WeaponItem)
			{
				if(troop.getTroopInfo().getWeapon() == equipitem.getKey())
					return troop;
			}
			else if(equipitem instanceof ArmorItem)
			{
				if(troop.getTroopInfo().getArmor() == equipitem.getKey())
					return troop;
			}
			else if(equipitem instanceof HorseItem)
			{	
				if(troop.getTroopInfo().getHorse() == equipitem.getKey())
					return troop;
			}
			*/
		}
		return null;
	}
	
	public OldTroop getTroopBySkill(int skillitemkey)
	{
		for(OldTroop troop : getTroops())
		{
			/*by yanglk troop
			if(troop.getTroopInfo().getSkills().contains(skillitemkey))
				return troop;
				*/
		}
		return null;
	}
	
	public boolean switchEquip(int trooppos, int equipkey)
	{
		ItemColumn itecol = Module.getItemColumn(roleId, BagTypes.EQUIP, false);
		EquipItem equipitem = (EquipItem)itecol.getItem(equipkey);
		
		OldTroop newtroop = getTroop(trooppos);
		if(newtroop == null)
			return false;
		//TODO 验证该军队是否能装该物品
		
		/*by yanglk troop
		Troop oldtroop = getTroopByEquip(equipitem);
		if(oldtroop != null)
		{
			if(equipitem instanceof WeaponItem)
				oldtroop.getTroopInfo().setWeapon(0);
			else if(equipitem instanceof ArmorItem)
				oldtroop.getTroopInfo().setArmor(0);
			else if(equipitem instanceof HorseItem)
				oldtroop.getTroopInfo().setHorse(0);
		}
		if(equipitem instanceof WeaponItem)
			newtroop.getTroopInfo().setWeapon(equipkey);
		else if(equipitem instanceof ArmorItem)
			newtroop.getTroopInfo().setArmor(equipkey);
		else if(equipitem instanceof HorseItem)
			newtroop.getTroopInfo().setHorse(equipkey);
		
		*/
		refreshTroops();
		return true;
	}
	
	public static int MAX_SKILL_NUM = 2;
	
	public boolean switchSkill(int trooppos, int skillpos, int skillkey)
	{
		if(skillpos < 1)
			skillpos = 1;
		if(skillpos > MAX_SKILL_NUM)
			skillpos = MAX_SKILL_NUM;
		if(skillpos < 1 || skillpos > MAX_SKILL_NUM)
			return false;
		ItemColumn itecol = Module.getItemColumn(roleId, BagTypes.SKILL, false);
		SkillItem skillitem = (SkillItem)itecol.getItem(skillkey);
		OldTroop newtroop = getTroop(trooppos);
		if(newtroop == null)
			return false;
		
		int j = 1;
		/*by yanglk troop
		for(int alreadyskill : newtroop.getTroopInfo().getSkills())
		{
			SkillItem alreadyskillitem = (SkillItem)itecol.getItem(alreadyskill);
			if(alreadyskillitem.getItemid() == skillitem.getItemid())
			{
				if(skillpos != j)
					return false;
				Message.psendMsgNotifyWhileCommit(roleId, 125);
				break;
			}
			j++;
		}
		
		Hero newhero = getHeroByTroop(newtroop.getPos());
		if(newhero == null)
			return false;
		if(newhero.getConfig().getBindskill() == skillitem.getItemid())
		{
			Message.psendMsgNotifyWhileCommit(roleId, 125);
			return false;
		}
		
		//验证该军队是否能装该物品
		int size = newtroop.getTroopInfo().getSkills().size();
		if(size >= MAX_SKILL_NUM && skillpos > size)
		{
			logger.error("装备技能时，角色："+roleId+"的军队："+trooppos+"技能数为："+size);
			for(int i = MAX_SKILL_NUM; i <= size-1; i++)
				newtroop.getTroopInfo().getSkills().remove(i);
			refreshTroops();
			return true;
		}
		
		Troop oldtroop = getTroopBySkill(skillitem.getKey());
		if(oldtroop != null)
		{
			if(oldtroop.getPos() == trooppos)
				return false;
			oldtroop.getTroopInfo().getSkills().remove((Integer)(skillitem.getKey()));
		}
		if(skillpos <= size)
			newtroop.getTroopInfo().getSkills().set(skillpos-1, skillitem.getKey());
		else
		{
			if(size >= MAX_SKILL_NUM)
			{
				logger.error("添加技能时，角色："+roleId+"的军队："+trooppos+"技能数为："+size);
				for(int i = MAX_SKILL_NUM; i <= size-1; i++)
					newtroop.getTroopInfo().getSkills().remove(i);
				refreshTroops();
				return true;
			}
			newtroop.getTroopInfo().getSkills().add(skillitem.getKey());
		}
		*/
		refreshTroops();
		return true;
	}
	
	public void setLevel(int level)
	{
		//by yanglk  hero		for(xbean.Hero xhero : xcolumn.getHeroes().values())
		//by yanglk  hero			xhero.setLevel(level);
	}
	
	public List<Integer> getHeroIds()
	{
		List<Integer> ids = new LinkedList<Integer>();
		//by yanglk  hero		for(xbean.Troop xtroop : xcolumn.getTroops())
		{
			/*by yanglk troop
			Hero h = getHero(xtroop.getHeroid());
			if(h != null)
				ids.add(h.getId());
				*/
		}
		return ids;
	}
	
	/**
	 * 上线时，发送数据之前的处理
	 */
	public void processBeforeOnline()
	{
		//刷掉卡的技能
		for(OldTroop troop : getTroops())
		{
			ItemColumn itemcol = chuhan.gsp.item.Module.getItemColumn(roleId, BagTypes.SKILL, false);
			OldHero hero = null;//*by yanglk troopgetHero(troop.getHerokey());
			if(hero == null)
				continue;
			List<Integer> skills = new LinkedList<Integer>();
			/*by yanglk troop
			skills.addAll(troop.getTroopInfo().getSkills());
			for(int skillkey : skills)
			{
				 SkillItem skillitem = (SkillItem)(itemcol.getItem(skillkey));
				 if(skillitem == null)
					 continue;
				 int skillid = ((SkillItem)skillitem).getItemid();
				 if(skillid == hero.getConfig().getBindskill())
					 troop.getTroopInfo().getSkills().remove((Integer)skillkey);
			}
			*/
		}
	}
	
}
