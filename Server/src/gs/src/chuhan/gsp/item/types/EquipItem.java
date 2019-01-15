package chuhan.gsp.item.types;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.OctetsStream;

import xdb.Transaction;
import chuhan.gsp.attr.attributetrain32;
import chuhan.gsp.attr.player03;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.EquipItemData;
import chuhan.gsp.item.addruneattribute29;
import chuhan.gsp.item.baseruneattribute28;
import chuhan.gsp.main.ConfigManager;


public abstract class EquipItem extends BasicItem{
	public static final int ATTR_NULL = -1;
	public static final int ATTR1 = 1;
	public static final int ATTR2 = 2;
	public static final int ATTR3 = 3;
	public static final int ATTR4 = 4;
	
	public xbean.EquipExtData extdata;
//	SEquip equipcfg;
	
	EquipItem(int itemid) {
		super(itemid);
		//equipcfg = ConfigManager.getInstance().getConf(SEquip.class).get(getItemid());
	}
	
	EquipItem(xbean.Item item) {
		super(item);
		if(Transaction.current() == null)
			extdata = xtable.Equipextdatas.select(dataItem.getUniqueid());
		else
			extdata = xtable.Equipextdatas.get(dataItem.getUniqueid());
		//equipcfg = ConfigManager.getInstance().getConf(SEquip.class).get(getItemid());
	}

	@Override
	protected void afterInsert() {
		if(xtable.Skillextdatas.get(dataItem.getUniqueid()) != null)
			return;
		extdata = xbean.Pod.newEquipExtData();
		initExtBaseData();
		xtable.Equipextdatas.insert(dataItem.getUniqueid(), extdata);
	}

	@Override
	protected void afterDelete() {
		xtable.Skillextdatas.remove(dataItem.getUniqueid());
	}
	
	@Override
	public int getSellPrice() {
		return 0;//super.getWorth() + ConfigManager.getInstance().getConf(SLevelConfig.class).get(getLevel()).bonusworth;
	}
	
	@Override
	public Octets getExtdataOctets() {
		EquipItemData extoct = new EquipItemData();
		extoct.init1 = extdata.getInit1();
		extoct.init2 = extdata.getInit2();
		extoct.init3 = extdata.getInit3();
		extoct.attr1 = extdata.getAttr1();
		extoct.attr2 = extdata.getAttr2();
		extoct.attr3 = extdata.getAttr3();
		extoct.attr4 = extdata.getAttr4();
		return extoct.marshal(new OctetsStream());
	};
	
	/**
	 * 更新符文基础数据
	 */
	public void initExtBaseData(){
		java.util.TreeMap<Integer,baseruneattribute28> baseList = ConfigManager.getInstance().getConf(baseruneattribute28.class);
		for(Map.Entry<Integer, baseruneattribute28> entry : baseList.entrySet()){
			if(entry.getValue().getBagId() == this.attr.getRune_baseAttri1() &&
					entry.getValue().getLevel() == this.extdata.getLevel()){
				this.extdata.setInit1(entry.getKey());
			}
			if(entry.getValue().getBagId() == this.attr.getRune_baseAttri2() &&
					entry.getValue().getLevel() == this.extdata.getLevel()){
				this.extdata.setInit2(entry.getKey());
			}
			if(entry.getValue().getBagId() == this.attr.getRune_baseAttri3() &&
					entry.getValue().getLevel() == this.extdata.getLevel()){
				this.extdata.setInit3(entry.getKey());
			}
		}
	}
	
	/**
	 * 是否有下一级能够强化
	 * @return
	 */
	public boolean isHaveNextLevel(){
		java.util.TreeMap<Integer,baseruneattribute28> baseList = ConfigManager.getInstance().getConf(baseruneattribute28.class);
		for(Map.Entry<Integer, baseruneattribute28> entry : baseList.entrySet()){
			if(entry.getValue().getBagId() == this.attr.getRune_baseAttri1() &&
					entry.getValue().getLevel() == this.extdata.getLevel() + 1){
				return true;
			}	
		}
		return false;
	}
	
	/**
	 * 判断哪个位置还未鉴定
	 * @return
	 */
	public int getWitchAttr(){
		if( this.extdata.getAttr1() == -1 && this.attr.rune_addAttri1 != -1 ){
			return ATTR1;
		}else if( this.extdata.getAttr2() == -1 && this.attr.rune_addAttri2 != -1 ){
			return ATTR2;
		}else if( this.extdata.getAttr3() == -1 && this.attr.rune_addAttri3 != -1 ){
			return ATTR3;
		}else if( this.extdata.getAttr4() == -1 && this.attr.rune_addAttri4 != -1 ){
			return ATTR4;
		}
		return ATTR_NULL;
	}
	
	/**
	 * 设置附加属性
	 * @param attrtype
	 * @param attrid
	 * @return
	 */
	public boolean setAttrId(int attrtype, int attrid){
		switch(attrtype){
		case ATTR1:
			this.extdata.setAttr1(attrid);
			break;
		case ATTR2:
			this.extdata.setAttr2(attrid);
			break;
		case ATTR3:
			this.extdata.setAttr3(attrid);
			break;
		case ATTR4:
			this.extdata.setAttr4(attrid);
			break;
		default:
			return false;
		}
		return true;
	}
	
	/**
	 * 鉴定随机附加属性
	 * @param typeid
	 * @return
	 */
	public int getAttrRandom(int typeid){
		List<addruneattribute29> ranList = new ArrayList<addruneattribute29>();
		java.util.TreeMap<Integer,addruneattribute29> attrList = ConfigManager.getInstance().getConf(addruneattribute29.class);
		for(Map.Entry<Integer, addruneattribute29> entry : attrList.entrySet()){
			if(entry.getValue().getBagId() == typeid ){
				ranList.add(entry.getValue());
			}	
		}
		if(ranList.size() == 0){
			return ATTR_NULL;
		}
		List<Integer> allDrop = new ArrayList<Integer>();
		List<Integer> allProb = new ArrayList<Integer>();
		for(addruneattribute29 attr : ranList){
			allDrop.add(attr.getId());
			allProb.add(attr.getWeight());
		}
		
		List<Integer> result = DropManager.getInstance().getDropIdList(
				DropManager.getInstance().getDropMap(allDrop,allProb,0),1);
		if(result.size() < 1){
			return ATTR_NULL;
		}
		return result.get(0);
		
	}
	
	public int getLevel()
	{
		return extdata.getLevel();
	}
	
	public void setLevel(int v)
	{
		extdata.setLevel(v);
	}
	
	/*
	public int getGrade()
	{
		return extdata.getGrade();
	}
	
	public int getFinalColor()
	{
		if(getGrade() < 1)
			return getColor();
		else if(getGrade() < 9)
			return getColor()+1;
		else
			return getColor()+2;
	}
	
	public void setGrade(int v)
	{
		extdata.setGrade(v);
	}
	
	public int getGradeExp()
	{
		return extdata.getGradeexp();
	}
	
	public int getAllGradeExp()
	{
		int expsum = getGradeExp();
		for(int i = 0 ; i < getGrade(); i++)
		{
			expsum += getNeedExp(i);
		}
		return expsum;
	}
	
	public int getNeedExp()
	{
		return getNeedExp(getGrade());
	}
	
	public static int getNeedExp(int curlevel)
	{
		return 0;//ConfigManager.getInstance().getConf(SLevelConfig.class).get(curlevel+1).equipexp;
	}
	
	public void setGradeExp(int v)
	{
		extdata.setGradeexp(v);
	}
	
	public int getHp()
	{
		//return (int)(equipcfg.army + equipcfg.army_grow * (getLevel()-1) + equipcfg.army_remake_grow *(getLevel()/10)*getGrade());
		return 0;
	}
	public int getAttack()
	{
		//return (int)(equipcfg.atta + equipcfg.atta_grow * (getLevel()-1) + equipcfg.atta_remake_grow *(getLevel()/10)*getGrade());
		return 0;
	}
	public int getDefend()
	{
		//return (int)(equipcfg.denf + equipcfg.denf_grow * (getLevel()-1) + equipcfg.denf_remake_grow *(getLevel()/10)*getGrade());
		return 0;
	}
	public int getWisdom()
	{
		//return (int)(equipcfg.wise + equipcfg.wise_grow * (getLevel()-1) + equipcfg.wise_remake_grow *(getLevel()/10)*getGrade());
		return 0;
	}
	public int getSpeed()
	{
		//return (int)(equipcfg.speed + equipcfg.speed_grow * (getLevel()-1) + equipcfg.speed_remake_grow *getGrade());
		return 0;
	}
	@Override
	public boolean canSell()
	{
		return true;
	}
	
//	public SEquip getEquipConfig()
//	{
//		return equipcfg;
//	}
	
	public Map<Integer,Float> getEffects()
	{
		Map<Integer,Float> effects = new HashMap<Integer, Float>();
		effects.put(AttrType.ARMY+1, (float)getHp());
		effects.put(AttrType.ATTACK+1, (float)getAttack());
		effects.put(AttrType.DEFEND+1, (float)getDefend());
		effects.put(AttrType.SKILL+1, (float)getWisdom());
		effects.put(AttrType.SPEED+1, (float)getSpeed());
		return effects;
	}
	*/
}
