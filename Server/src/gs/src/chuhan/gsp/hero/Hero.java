package chuhan.gsp.hero;


import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.DataInit;
import chuhan.gsp.attr.config10;
import chuhan.gsp.attr.heroexp02;
import chuhan.gsp.item.equipmentquality71;
import chuhan.gsp.item.equipmentstrength72;
import chuhan.gsp.item.hero01;
import chuhan.gsp.item.heroaddstage67;
import chuhan.gsp.item.heroculture70;
import chuhan.gsp.item.heroupgradexp68;
import chuhan.gsp.item.ms73;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.util.ParserString;


public class Hero {
	
	public static final Logger logger = Logger.getLogger(Hero.class);
//	private final static double addnum = 1.0029;
	private final static int SAME_HERO_MODE = 1000;
		
	private final xbean.Hero xhero;
	public final boolean readonly;
	private hero01 ihero;
	
	public static final String SPLITSTR = ":";	//数据保存用分隔符
	public static final String SPLITSTR2 = "|";	//数据保存用分隔符
	
	public static float attackAdd = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1346)
			.getConfigvalue());
	public static float defenceAdd = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1347)
			.getConfigvalue());
	public static float hpAdd = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1348)
			.getConfigvalue());
	
	
	public static Hero getHero(xbean.Hero xhero, boolean readonly)
	{
		return new Hero(xhero, readonly);
	}
	
	public xbean.Hero getxHeroInfo()
	{
		return xhero;
	}
	
	public hero01 getiHeroInfo()
	{
		return ihero;
	}
	
	
	
	public Hero(xbean.Hero heroinfo, boolean readonly) {
		ihero = ConfigManager.getInstance().getConf(hero01.class).get(heroinfo.getHeroid());
		this.xhero = heroinfo;
		this.readonly = readonly;
	}
	
	

	public chuhan.gsp.Hero getProtocolHero(){
		chuhan.gsp.Hero phero = new chuhan.gsp.Hero();
		phero.key = xhero.getKey();
		phero.heroid = xhero.getHeroid();
		phero.heroviewid = xhero.getHeroviewid();
		phero.heroexp = xhero.getHeroexp();
		phero.herolevel = xhero.getHerolevel();
//		phero.heroallexp = this.getExpMax(xhero.getHerolevel(),ihero.getExpNum());
//		phero.qianghualevel = xhero.getQianghualevel();
//		phero.weapon = xhero.getWeapon();
//		phero.barde = xhero.getBarde();
//		phero.ornament = xhero.getOrnament();
		phero.weapon = this.getHeroZl();
		List<Integer> pyList = ParserString.parseString2Int(xhero.getHeropeiyang(),HeroColumn.SPLITSTR);
		if(pyList == null || pyList.size() < 4){
			phero.peiyang1 = 0;
			phero.peiyang2 = 0;
			phero.peiyang3 = 0;
			phero.peiyang4 = 0;
		}else{
			phero.peiyang1 = pyList.get(0);
			phero.peiyang2 = pyList.get(1);
			phero.peiyang3 = pyList.get(2);
			phero.peiyang4 = pyList.get(3);
		}
//		phero.peiyang1 = xhero.getPeiyang1();
//		phero.peiyang2 = xhero.getPeiyang2();
//		phero.peiyang3 = xhero.getPeiyang3();
//		phero.peiyang4 = xhero.getPeiyang4();
		phero.skill1 = ihero.getSkill1ID();
		phero.skill2 = ihero.getSkill2ID();
		phero.skill3 = ihero.getSkill3ID();
//		phero.items.putAll(xhero.getItems());
		
		phero.herojinjiestar = xhero.getHerojinjiestar();
		phero.herojinjiesmall = xhero.getHerojinjiesmall();
		phero.heropinji = xhero.getHeropinji();
		phero.heroskill = xhero.getHeroskill();
		phero.heromishu = xhero.getHeromishu();
		phero.heropeiyang = xhero.getHeropeiyang();
		phero.heroequip = xhero.getHeroequip();

		return phero;
	}
	
	public int getHeroZl(){
		float num = 0f;
		try{
			float initNum = (float)this.ihero.getInitMaxHP() * hpAdd 
					+ (float)this.ihero.getInitPhysicalAttack() * attackAdd
					+ (float)this.ihero.getInitPhysicalDefence() * defenceAdd;
			num = initNum + getJinjieZl() + getMSZl() + getPeiYangZl() + getEquipZl();
		}catch(Exception e){
			e.printStackTrace();
			return 0;
		}
		return (int)num;
	}
	
	public float getEquipZl(){
		float result = 0f;
		List<String> xEquipList = ParserString.parseString(this.getxHeroInfo().getHeroequip(),SPLITSTR);
		if(xEquipList == null || xEquipList.size() == 0){
			return 0f;
		}
		for(int i = 0;i<xEquipList.size();i++){
			List<Integer> xEquipData = ParserString.parseString2Int(xEquipList.get(i),SPLITSTR2);
			if(xEquipData == null || xEquipData.size() != 2){
				continue;
			}
			equipmentquality71 init = ConfigManager.getInstance().getConf(equipmentquality71.class).
					get(xEquipData.get(0));
			List<Integer> typeList = ParserString.parseString2Int(init.getQualityAttribute());
			List<Integer> numList = ParserString.parseString2Int(init.getNumerical());
			if(typeList == null || numList == null || typeList.size() != numList.size()){
				continue;
			}
			for(int j = 0;j<typeList.size();j++){
				result += getZL(typeList.get(j),numList.get(j));
			}
			
			TreeMap<Integer,equipmentstrength72> map = ConfigManager.getInstance().getConf(equipmentstrength72.class);
			equipmentstrength72 init72 = null;
			for(Map.Entry<Integer, equipmentstrength72> entry : map.entrySet()){
				if(this.getiHeroInfo().getQosition() == entry.getValue().getQosition() &&
						i + 1 == entry.getValue().getParts() &&
						xEquipData.get(1) == entry.getValue().getSthequipmentlevel() ){
					init72 = entry.getValue();
					break;
				}
			}
			if(init72 == null){
				continue;
			}
			typeList = ParserString.parseString2Int(init72.getAttribute());
			numList = ParserString.parseString2Int(init72.getValue());
			if(typeList == null || numList == null || typeList.size() != numList.size()){
				continue;
			}
			for(int j = 0;j<typeList.size();j++){
				result += getZL(typeList.get(j),numList.get(j));
			}
			
		}
		return result;
	}
	
	public float getPeiYangZl(){
		float result = 0f;
		List<Integer> pyList = ParserString.parseString2Int(this.getxHeroInfo().getHeropeiyang(),SPLITSTR);
		if(pyList == null || pyList.size() == 0){
			return 0f;
		}
		for(int i = 0;i<pyList.size();i++){
			if(pyList.get(i) == 0){
				continue;
			}
			heroculture70 init = null;
			TreeMap<Integer, heroculture70> map = ConfigManager.getInstance().getConf(heroculture70.class);
			for(Map.Entry<Integer, heroculture70> entry : map.entrySet()){
				if(entry.getValue().getBorn() == this.getiHeroInfo().getBorn() &&
						entry.getValue().getQosition() == this.getiHeroInfo().getQosition() &&
						entry.getValue().getElement() == i + 1 &&
						entry.getValue().getElementLeve() == pyList.get(i)){
					init = entry.getValue();
					break;
				}
			}
			if(init == null){
				continue;
			}
			List<Integer> typeList = ParserString.parseString2Int(init.getAttribute());
			List<Integer> numList = ParserString.parseString2Int(init.getValue());
			if(typeList == null || numList == null || typeList.size() != numList.size()){
				continue;
			}
			for(int j = 0;j<typeList.size();j++){
				result += getZL(typeList.get(j),numList.get(j));
			}
			
		}
		return result;
	}
	
	
	public float getMSZl(){
		float result = 0f;
		List<String> xMsList = ParserString.parseString(this.getxHeroInfo().getHeromishu(),SPLITSTR);
		List<Integer> iMsList = ParserString.parseString2Int(this.getiHeroInfo().getMsid());
		if(xMsList == null || iMsList == null || xMsList.size() != iMsList.size() ){
			return 0f;
		}
		for(int i = 0;i<xMsList.size();i++){
			List<Integer> xMsNum = ParserString.parseString2Int(xMsList.get(0),SPLITSTR2);
			ms73 iMsInit = ConfigManager.getInstance().getConf(ms73.class).get(iMsList.get(i));
			if(xMsNum == null || xMsNum.size() != 2 || xMsNum.get(1) < 1 || iMsInit == null ){
				continue;
			}
			if(iMsInit.getMstype() == 6 || iMsInit.getMstype() == 7 && iMsInit.getMstype() == 8 ){
				List<Integer> addList = ParserString.parseString2Int(iMsInit.getValue());
				if(addList == null || addList.size() < xMsNum.get(1)){
					continue;
				}
				switch(iMsInit.getMstype()){
				case 6:
					result += getZL(1,addList.get(xMsNum.get(1) - 1));
					break;
				case 7:
					result += getZL(3,addList.get(xMsNum.get(1) - 1));
					break;
				case 8:
					result += getZL(5,addList.get(xMsNum.get(1) - 1));
					break;
				}
			}
			
		}
		
		return result;
	}
	
	public float getJinjieZl(){
		heroaddstage67 init = null;
		TreeMap<Integer, heroaddstage67> map = ConfigManager.getInstance().getConf(heroaddstage67.class);
		//先根据阶级找
		for(Map.Entry<Integer, heroaddstage67> entry : map.entrySet()){
			if(entry.getValue().getBorn() == this.getiHeroInfo().getBorn() &&
					entry.getValue().getQosition() == this.getiHeroInfo().getQosition() &&
					entry.getValue().getQuality() == this.getxHeroInfo().getHerojinjiestar() &&
					entry.getValue().getHalosPn() == this.getxHeroInfo().getHerojinjiesmall()){
				init = entry.getValue();
				break;
			}
		}
		if(init == null){
			return 0f;
		}
		List<Integer> typeList = ParserString.parseString2Int(init.getAttribute());
		List<Integer> numList = ParserString.parseString2Int(init.getNumbers());
		if(typeList == null || numList == null || typeList.size() != numList.size()){
			return 0f;
		}
		float result = 0f;
		for(int i = 0;i<typeList.size();i++){
			result += getZL(typeList.get(i),numList.get(i));
		}
		return result;
	}
	public float getZL(int type,float num){
		switch(type){
		case 1:
			return num * hpAdd;
		case 3:
			return num * attackAdd;
		case 5:
			return num * defenceAdd;
		}
		return 0;
	}
	
	/**
	 * 换英雄配表ID（英雄升星）
	 * @param heroid
	 * @return
	 */
	public boolean changeihero(int heroid){
		hero01 iheronew = ConfigManager.getInstance().getConf(hero01.class).get(heroid);
		if(iheronew == null)
			return false;
		this.ihero = iheronew;
		xhero.setHeroid(heroid);
//		xhero.setHeroexp(0);
//		xhero.setHerolevel(1);
		xhero.setHeropinji(ihero.getQuality());
		xhero.setHeroviewid(ihero.getArtresources());
		return true;
	}
	
	public boolean isSameModeHero(Hero targetHero) {
		if(null == targetHero) {
			return false;
		}
		
		if (targetHero.ihero.getId() / 10 == this.xhero.getHeroid() / 10)
		{
			return true;
		}
		return false;
	}
	
	public boolean isSameHero(Hero targetHero) {
		if(null == targetHero) {
			return false;
		}
		
		if (targetHero.xhero.getKey() == this.xhero.getKey())
		{
			return true;
		}
		return false;
	}
	
	public static xbean.Hero createHero(int heroId, int level)
	{
		
		hero01 cfg = ConfigManager.getInstance().getConf(hero01.class).get(heroId);
//		heroexp02 iherolevel = ConfigManager.getInstance().getConf(heroexp02.class).get(level);
		if(cfg == null)
			return null;
		if(level > cfg.getMaxLevel() || level == -1 || level == 0)
			level = 1;
		xbean.Hero xhero = xbean.Pod.newHero();
		
		xhero.setHeroid(heroId);
		xhero.setHerolevel(level);
//		xhero.setHeroallexp(getExpMax(level,cfg.getExpNum()));
//		xhero.setSkill1(cfg.getSkill1ID());
//		xhero.setSkill2(cfg.getSkill2ID());
//		xhero.setSkill3(cfg.getSkill3ID());
		xhero.setHeropinji(cfg.getQuality());
		xhero.setHeroviewid(cfg.getArtresources());
		xhero.setHeropeiyang("0:0:0:0");
		xhero.setHeromishu("0|0:0|0:0|0:0|0:0|0:0|0");
		List<Integer> iequip = ParserString.parseString2Int(cfg.getEquipmentid());
		StringBuffer xequip = new StringBuffer();
		for(int i = 0;i<iequip.size();i++){
			xequip.append(iequip.get(i)).append("|").append("0");
			if(i <iequip.size() - 1){
				xequip.append(":");
			}
		}
		xhero.setHeroequip(xequip.toString());
		
		List<Integer> iskill = ParserString.parseString2Int(cfg.totalskill);
		StringBuffer xskill = new StringBuffer();
		for(int i = 0;i<iskill.size();i++){
			xskill.append(iskill.get(i));
			if(i <iequip.size() - 1){
				xskill.append(":");
			}
		}
		xhero.setHeroskill(xskill.toString());

		return xhero;
	}
	
	public int getLevel()
	{
		return this.xhero.getHerolevel();
	}
	public void setExp(int num)
	{
		this.xhero.setHeroexp(num);
	}
	public int getExp()
	{
		return this.xhero.getHeroexp();
	}
	public static int getExpMax(int level,int born){
		TreeMap<Integer,heroupgradexp68> map = ConfigManager.getInstance().getConf(heroupgradexp68.class);
		for(Map.Entry<Integer, heroupgradexp68> entry : map.entrySet()){
			if(entry.getValue().getBorn() == born && entry.getValue().getLevel() == level){
				return entry.getValue().getExp();
			}
		}
		
/*		heroexp02 iHeroLevel = ConfigManager.getInstance().getConf(heroexp02.class).get(level);
		if(iHeroLevel != null){
			List<Integer> expList = ParserString.parseString2Int(iHeroLevel.getExp());
			if(expList != null && expList.size() >= explie && explie != 0){
				if(expList.size() > explie)
					return expList.get(explie - 1);
			}
		}*/

		return 0;
	}
	
	/**
	 * 返回英雄全部经验
	 * @return
	 */
	public int getAllExp(){
		int result = 0;
		for(int i = 1;i<xhero.getHerolevel();i++){
			int exp = Hero.getExpMax(i, this.getiHeroInfo().getBorn());
			result = result + exp;
		}
		return result + xhero.getHeroexp();
	}
	
	/**
	 * 返回分解后经验结晶数量
	 * @return
	 */
	public int getSplitNum(){
		int result = this.getiHeroInfo().getReturnBack();
		int allexp = this.getAllExp();
		int bili = Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1245).configvalue);
		float rate = Float.parseFloat(ConfigManager.getInstance().getConf(config10.class).get(1246).configvalue);
		int extra = (int) ((float)allexp * rate / (float)bili);
		return result + extra;
	}
	
	public boolean levelUp(int roleLevel)
	{
		final int nexp = Hero.getExpMax(this.xhero.getHerolevel(),this.getiHeroInfo().getBorn());
		if (getExp() < nexp)
			return false;
		if (getLevel() >= ihero.getMaxLevel() || getLevel() >= roleLevel){
			return false;
		}
		
		setExp(getExp()-nexp);
		xhero.setHerolevel(getLevel() + 1);

		return true;
	}
	
	public boolean isCloseAttack(){
		return ParserString.parseString(this.ihero.getClientSignType()).get(0).equals("0");
	}
	
	/* 
	//根据等级计算数值（攻击，防御，血量）
	private int calculateBylevel(int level,int num)
	{
//		int rnum = 0;
		for(;level > 1; level--)
		{
			num = calculate(num);
		}
		return num;
	}
	
	//计算数值
	private int calculate(int num)
	{
		double returnnum = (double)(num)*addnum;
		return (int)returnnum;
	}
	
	//获取攻击值
	public int getHeroAttack()
	{
		return calculateBylevel(xhero.getHerolevel(),ihero.getAttack());
	}
	
	//获取防御值
	public int getHeroDefense()
	{
		return calculateBylevel(xhero.getHerolevel(),ihero.getDefense());
	}
	
	//获取血量值
	public int getHeroFullhp()
	{
		return calculateBylevel(xhero.getHerolevel(),ihero.getFullhp());
	}
	*/
	//刷新单个英雄
	public void refreshHero(long roleId)
	{
		SRefreshHero snd = new SRefreshHero();
		snd.heroinfo = this.getProtocolHero();
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
}
