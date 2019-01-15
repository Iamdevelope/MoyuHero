package chuhan.gsp.award;

import java.text.ParseException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.item.innerdrop16;
import chuhan.gsp.item.normaldrop15;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.ParserString;

/**
 * ID管理类
 * @author aa
 *
 */
public class IDManager {	
	public static IDManager instance = new IDManager();
	
	public static final int BEGIN_SKILL = 10;	//技能分类首几位
	public static final int BEGIN_BUFF = 11;	//buff分类首几位
	public static final int BEGIN_DROP = 12;	//掉落包分类首几位
	public static final int BEGIN_BATTLE_MOSTER = 13;	//关卡与怪物分类首几位
	public static final int BEGIN_OTHER = 14;	//其他分类首几位
	
	public static final int BEGIN_ZIYUAN = 1400;	//资源分类首几位
	public static final int BEGIN_FUWEN = 1401;	//符文分类首几位
	public static final int BEGIN_ITEM = 1402;	//道具分类首几位
	public static final int BEGIN_HERO = 1403;	//英雄分类首几位
	public static final int BEGIN_SKIN = 1404;	//皮肤分类首几位
	public static final int BEGIN_BAOXIANG = 1405;	//宝箱分类首几位
	public static final int BEGIN_SHENQI = 1406;	//神器分类首几位
	public static final int BEGIN_PUGONG = 1407;	//普攻分类首几位

	public static final int BEGIN_HEROCLONE = 140002;		//英雄克隆
	
	public static final int YUANBAO = 1400000001;	//元宝
	public static final int GOLD = 1400000002;	//金币
	public static final int SHENGLINGZQ = 1400000003;	//圣灵之泉
	public static final int RONGLIAN = 1400000004;	//熔炼点
	public static final int HUANGJINXZ = 1400000005;	//黄金勋章
	public static final int BAIJINXZ = 1400000006;	//白金勋章
	public static final int QINGTONGXZ = 1400000007;	//青铜勋章
	public static final int CHITIEXZ = 1400000008;	//赤铁勋章
	public static final int EXPJIEJING = 1400000009;	//经验结晶
	public static final int CHUANSHUOZS = 1400000010;	//传说之石
	public static final int SHOUWANGZL = 1400000011;	//守望之灵
	public static final int JINENGDIAN = 1400000012;	//技能点
	
	public static final int TILI = 1400010001;	//活力
	public static final int PVPTILI = 1400010002;	//PVP精力
	public static final int TANXIANTILI = 1400010003;	//探险行动力

	
	
	public int getIdBegin(int id){
		int begin = id / 100000000;
		switch(begin){
		case BEGIN_SKILL:
			return BEGIN_SKILL;
		case BEGIN_BUFF:
			return BEGIN_BUFF;
		case BEGIN_DROP:
			return BEGIN_DROP;
		case BEGIN_BATTLE_MOSTER:
			return BEGIN_BATTLE_MOSTER;
		case BEGIN_OTHER:
			return getIdBegin4(id); 
		}
		return -1;
	}
	
	public int getIdBegin4(int id){
		int begin = id / 1000000;
		switch(begin){
		case BEGIN_ZIYUAN:
			return BEGIN_ZIYUAN;
		case BEGIN_FUWEN:
			return BEGIN_FUWEN;
		case BEGIN_ITEM:
			return BEGIN_ITEM;
		case BEGIN_HERO:
			return BEGIN_HERO;
		case BEGIN_SKIN:
			return BEGIN_SKIN;
		case BEGIN_BAOXIANG:
			return BEGIN_BAOXIANG;
		case BEGIN_SHENQI:
			return BEGIN_SHENQI;
		case BEGIN_PUGONG:
			return BEGIN_PUGONG;
		}
		return -1;
	}
	
	public boolean isHeroClone(int id){
		int begin = id / 10000;
		return begin == this.BEGIN_HEROCLONE;
	}

	public static IDManager getInstance() {
		return instance;
	}
	
	public static void reload() throws Exception{

	}
	
	public void init() throws ParseException{
		
	}
	
	
	
}
