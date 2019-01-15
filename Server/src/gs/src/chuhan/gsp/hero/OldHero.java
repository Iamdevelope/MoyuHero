package chuhan.gsp.hero;

import java.util.Collection;
import java.util.Map;

import com.goldhuman.Common.Marshal.OctetsStream;

import chuhan.gsp.ColorType;
import chuhan.gsp.attr.player03;
import chuhan.gsp.item.SHero;
import chuhan.gsp.item.item26;
import chuhan.gsp.item.SkillItemData;
import chuhan.gsp.item.sherolinkconfig;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.Conv;

public class OldHero {
	
	public static OldHero getHero(long roleId, int heroId, boolean readonly)
	{
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, readonly);
		return herocol.getHero(heroId);
	}
	
	public static OldHero getHero(xbean.Hero xhero, boolean readonly)
	{
		return new OldHero(xhero, readonly);
	}
	
	/**
	 * 根据id和等级创建一个hero
	 * 这个方法的参数可能会变化，暂时只要两个就足够
	 * @param xiakeId
	 * @param level
	 * @return
	 */
	public static xbean.Hero createHero(int heroId, int level)
	{
		SHero cfg = ConfigManager.getInstance().getConf(SHero.class).get(heroId);
		if(cfg == null)
			return null;
		xbean.Hero xhero = xbean.Pod.newHero();
		/*//by yanglk  hero
		xhero.setId(heroId);
		xhero.setLevel(level);
		SItemAttr itemattr = ConfigManager.getInstance().getConf(SItemAttr.class).get(cfg.bindskill);
		int color =  itemattr.getColor();
		SLevelConfig lvcfg = ConfigManager.getInstance().getConf(SLevelConfig.class).get(1);
		switch(color)
		{
		case ColorType.WHITE:
			xhero.setSkillexp(lvcfg.skillexp1/2);
			break;
		case ColorType.GREEN:
			xhero.setSkillexp(lvcfg.skillexp2/2);
			break;
		case ColorType.BLUE:
			xhero.setSkillexp(lvcfg.skillexp3/2);
			break;
		case ColorType.PURPLE:
			xhero.setSkillexp(lvcfg.skillexp4/2);
			break;
		case ColorType.ORANGE:
			xhero.setSkillexp(lvcfg.skillexp5/2);
			break;
		}
		*/
		return xhero;
	}
	
	public static final Logger logger = Logger.getLogger("Hero");
	private final xbean.Hero xhero;
	private final SHero cfg;
	public final boolean readonly;
	public OldHero(xbean.Hero heroinfo, boolean readonly) {
		//by yanglk  hero		cfg = ConfigManager.getInstance().getConf(SHero.class).get(heroinfo.getId());
		cfg = new SHero();
		
		this.xhero = heroinfo;
		this.readonly = readonly;
	}

	public chuhan.gsp.Hero getProtocolHero()
	{
		chuhan.gsp.Hero phero = new chuhan.gsp.Hero();
		/*//by yanglk  hero
		phero.key = Conv.toShort(xhero.getKey());
		phero.id = Conv.toShort(xhero.getId());
		//phero.level = Conv.toShort(xhero.getLevel());
		phero.grade = Conv.toByte(xhero.getGrade());
		phero.extexp = xhero.getExtexp();
		phero.gradeexp = Conv.toByte(xhero.getGradeexp());
		phero.xiuliannum = Conv.toShort(getHeroInfo().getXiuliantimes());
		phero.hp = Conv.toShort(xhero.getBfp().getHp());
		phero.attack = Conv.toShort(xhero.getBfp().getAttack());
		phero.defend = Conv.toShort(xhero.getBfp().getDefend());
		phero.wisdom = Conv.toShort(xhero.getBfp().getWisdom());
		SkillItemData skilldata = new SkillItemData();
		skilldata.level = Conv.toByte(xhero.getSkilllv());
		skilldata.exp = xhero.getSkillexp();
		phero.skillinfo = skilldata.marshal(new OctetsStream());
		*/
		return phero;
	}
	
	/*//by yanglk  hero
	public int getScore() {
		return (int) (getArmy() + getAttack()*2.4 + getDefend()*3 + getWisdom()*1.5 + getSpeed()*10);
	}
	*/
	
	/**
	 * 是否同质的武将(西施与西施，神西施与神西施，西施与神西施)
	 * @param targetHero
	 * @return
	 */
	public boolean isLinkHero(OldHero targetHero) {
		if(null == targetHero) {
			return false;
		}
		/*//by yanglk  hero
		if(this.xhero.getId() == targetHero.xhero.getId()//是同一种武将
				|| getLinkHeroId() == targetHero.xhero.getId()//我的同质武将是你
				|| this.xhero.getId() == targetHero.getLinkHeroId()) {//你的同质武将是我
			return true;
			
		}*/
		
		return false;
	}
	
	/**
	 * 获取同质的武将的ID
	 * @return 没有返回-1
	 */
	public int getLinkHeroId() {
		Map<Integer, sherolinkconfig> links = ConfigManager.getInstance().getConf(sherolinkconfig.class);
		Collection<sherolinkconfig>  cs = links.values();
		for(sherolinkconfig cf : cs) {
			/*//by yanglk  hero
			if(cf.getHeroid() == this.xhero.getId()) {
				return cf.getLinkid();
			}
			*/
		}
		
		return -1;
	}
	
	/**
	 * 获取同质的武将的ID
	 * @param heroId 目标武将的ID
	 * @return 没有返回-1
	 */
	public static int getLinkHeroId(int heroId) {
		Map<Integer, sherolinkconfig> links = ConfigManager.getInstance().getConf(sherolinkconfig.class);
		Collection<sherolinkconfig>  cs = links.values();
		for(sherolinkconfig cf : cs) {
			if(cf.getHeroid() == heroId) {
				return cf.getLinkid();
			}
		}
		
		return -1;
	}
	
	/**
	 * 是否神将 
	 * @return true-是
	 */
	public boolean isDeify() {
		return cfg.deify == 1;
	}
	
	public xbean.Hero getHeroInfo()
	{
		return xhero;
	}
	
	/*
	public int getId()
	{
		return xhero.getId();
	}
	
	public SHero getConfig()
	{
		return cfg;
	}
	
	public int getGrade()
	{
		return xhero.getGrade();
	}
	
	public int getRoleLevel()
	{
		return xhero.getLevel();
	}
	
	public void addExtExp(int addv)
	{
		xhero.setExtexp(xhero.getExtexp()+addv);
	}
	
	public int getExtExp()
	{
		return xhero.getExtexp();
	}
	*/
	public int getExtLevel()
	{
		Map<Integer,player03> lvcfgs = ConfigManager.getInstance().getConf(player03.class);
		int expsum = 0;
		int lv = 0;
		for(int i = 1; i <= 100; i++)
		{
//			expsum += lvcfgs.get(i).herolevelexp;
			//by yanglk  hero			if(expsum > xhero.getExtexp())
			//	break;
			lv = i;
		}
		return lv;
	}
	
	/*//by yanglk  hero
	public int getLevel()
	{
		return xhero.getLevel() + getExtLevel();
	}
	
	public int getInitColor()
	{
		return getConfig().getColor();
	}
	
	public int getColor()
	{
		if(xhero.getGrade() < 1)
			return getInitColor();
		if(xhero.getGrade() < 4)
			return getInitColor() + 1;
		else
			return getInitColor() + 2;
	}
	
	public int getMaxXiulian()
	{
		return (getLevel() - 1)*getInitColor();
	}
	
	public int getAllGradeExp()
	{
		int expsum = xhero.getGradeexp();
		for(int i = 0; i < xhero.getGrade();i++)
		{
			expsum += getNeedGradeExp(i);
		}
		return expsum;
	}
	
	public int canXiulianTimes()
	{
		return Math.max(0, getMaxXiulian() - getHeroInfo().getXiuliantimes());
	}
	
	public int getNeedGradeExp()
	{
		return getNeedGradeExp(xhero.getGrade());
	}
	
	public int getGroupType()
	{
		return cfg.chuhan;
	}
	
	public static int getNeedGradeExp(int curgrade)
	{
		if(curgrade < 1)
			return 1;
		if(curgrade == 1)
			return 2;
		if(curgrade == 2)
			return 4;
		return 8;
	}
	
	public int getArmy()
	{
		return (int)(cfg.army + cfg.army_grow * (getLevel() - 1) + getGrade() * cfg.evo_army_grow + getHeroInfo().getBfp().getHp());
	}
	
	public int getAttack()
	{
		return (int)(cfg.atta + cfg.atta_grow * (getLevel() - 1) + getGrade() * cfg.evo_atta_grow + getHeroInfo().getBfp().getAttack());
	}
	
	public int getDefend()
	{
		return (int)(cfg.denf + cfg.denf_grow * (getLevel() - 1) + getGrade() * cfg.evo_denf_grow + getHeroInfo().getBfp().getDefend());
	}
	
	public int getWisdom()
	{
		return (int)(cfg.wise + cfg.wise_grow * (getLevel() - 1) + getGrade() * cfg.evo_wise_grow + getHeroInfo().getBfp().getWisdom());
	}
	public int getSpeed()
	{
		return (int)(cfg.speed + cfg.evo_speed_grow * getGrade());
	}
	
	public int getMaxGrade()
	{
		return isDeify()? 5 : 4;
			
	}
	*/
}
