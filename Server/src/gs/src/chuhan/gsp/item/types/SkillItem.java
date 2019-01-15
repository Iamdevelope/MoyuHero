package chuhan.gsp.item.types;

import java.util.Map;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.OctetsStream;

import xdb.Transaction;
import chuhan.gsp.ColorType;
import chuhan.gsp.attr.player03;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.SkillItemData;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.Conv;

public class SkillItem extends BasicItem{
	
	xbean.SkillExtData extdata;
	
	SkillItem(int itemid) {
		super(itemid);
	}
	SkillItem(xbean.Item item) {
		super(item);
		if(Transaction.current() == null)
			extdata = xtable.Skillextdatas.select(dataItem.getUniqueid());
		else
			extdata = xtable.Skillextdatas.get(dataItem.getUniqueid());
	}

	@Override
	protected void afterInsert() {
		if(xtable.Skillextdatas.get(dataItem.getUniqueid()) != null)
			return;
		extdata = xbean.Pod.newSkillExtData();
		int color =  0;
		player03 lvcfg = ConfigManager.getInstance().getConf(player03.class).get(1);
		switch(color)
		{
		case ColorType.WHITE:
//			extdata.setExp(lvcfg.skillexp1/2);
			break;
		case ColorType.GREEN:
//			extdata.setExp(lvcfg.skillexp2/2);
			break;
		case ColorType.BLUE:
//			extdata.setExp(lvcfg.skillexp3/2);
			break;
		case ColorType.PURPLE:
//			extdata.setExp(lvcfg.skillexp4/2);
			break;
		case ColorType.ORANGE:
//			extdata.setExp(lvcfg.skillexp5/2);
			break;
		}
		xtable.Skillextdatas.insert(dataItem.getUniqueid(), extdata);
	}
	
	public void addExp(int exp)
	{
		if(exp <= 0)
			return;
		extdata.setExp(extdata.getExp()+exp);
		levelup();
	}
	
	public void levelup()
	{
		int nextexp = getNextExp();
		while(extdata.getExp() >= nextexp)
		{
			extdata.setExp(extdata.getExp() - nextexp);
			extdata.setLevel(extdata.getLevel()+1);
			nextexp = getNextExp();
		}
	}
	
	public int getNextExp()
	{
		return getNextExp(extdata.getLevel(), 0);
	}
	
	public static int getNextExp(int level, int color)
	{
		player03 lvcfg = ConfigManager.getInstance().getConf(player03.class).get(level);
		switch(color)
		{
		case ColorType.WHITE:
//			return lvcfg.skillexp1;
		case ColorType.GREEN:
//			return lvcfg.skillexp2;
		case ColorType.BLUE:
//			return lvcfg.skillexp3;
		case ColorType.PURPLE:
//			return lvcfg.skillexp4;
		case ColorType.ORANGE:
//			return lvcfg.skillexp5;
		}
		throw new IllegalArgumentException("技能的下一级技能有错：level="+level+",color="+color);
	}
	
	public int getSumExp()
	{
		return getSumExp(0, extdata.getLevel(), extdata.getExp());
	}
	
	public static int getSumExp(int skillcolor, int level, int curexp)
	{
		Map<Integer,player03> lvcfg = ConfigManager.getInstance().getConf(player03.class);
		for(int i = 1; i < level; i++)
		{
			/*
			if(skillcolor == ColorType.WHITE)
				curexp += lvcfg.get(i).skillexp1;
			else if(skillcolor == ColorType.GREEN)
				curexp += lvcfg.get(i).skillexp2;
			else if(skillcolor == ColorType.BLUE)
				curexp += lvcfg.get(i).skillexp3;
			else if(skillcolor == ColorType.PURPLE)
				curexp += lvcfg.get(i).skillexp4;
			else if(skillcolor == ColorType.ORANGE)
				curexp += lvcfg.get(i).skillexp5;
				*/
		}
		return curexp;
	}
	
	@Override
	protected void afterDelete() {
		xtable.Skillextdatas.remove(dataItem.getUniqueid());
	}
	
	public xbean.SkillExtData getSkillExtData()
	{
		return extdata;
	}
	
	@Override
	public Octets getExtdataOctets() {
		SkillItemData extoct = new SkillItemData();
		extoct.level = Conv.toByte(extdata.getLevel());
		extoct.grade = Conv.toByte(extdata.getGrade());
		extoct.exp = extdata.getExp();
		return extoct.marshal(new OctetsStream());
	};
}
