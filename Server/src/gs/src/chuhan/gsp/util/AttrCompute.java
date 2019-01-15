package chuhan.gsp.util;


import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;

import chuhan.gsp.item.hero01;

/**
 * 属性计算
 * @author aa
 *
 */
public class AttrCompute
{
	
	public static final Logger logger = Logger.getLogger(AttrCompute.class);
	/*
	1.	生命、物攻、物防、法攻、法防、速度
	2.	命中、闪避、暴击、韧性
	3.	物伤加成率、物伤减免率、法伤加成率、法伤减免率、暴击伤害率
	4.	附加伤害值、绝对减伤值
	*/
	private static final int HP = 11;			//生命
	private static final int PATTACK = 12;		//物攻
	private static final int PDEFENCE = 13;		//物防
	private static final int MATTACK = 14;		//法攻
	private static final int MDEFENCE = 15;		//法防
	private static final int SPEED = 16;		//速度
	
	private static final int HIT = 21;			//命中
	private static final int DODGE = 22;		//闪避
	private static final int CRITICAL = 23;		//暴击
	private static final int TENACITY = 24;		//韧性
	
	private static final int PADD_PERSENT = 31;		//物伤加成率
	private static final int PDEL_PERSENT = 32;		//物伤减免率
	private static final int MADD_PERSENT = 33;		//法伤加成率
	private static final int MDEL_PERSENT = 34;		//法伤减免率
	private static final int CRITICAL_PERSENT = 35;		//暴击伤害率
	
	private static final int DAMAGEADD = 41;		//附加伤害值
	private static final int DAMAGEDEL = 42;		//绝对减伤值
	
	///////现在 有问题,等级提升是有另一个等级提升比例表,还未改过来呢
	public static float getbase(hero01 ih,int level,int gettype,int attradd){
		switch(gettype){
		case HP:
			return base(ih.getInitMaxHP(),strToFloat(ih.getHPGrowth()),level,ih.getHPGrowthMultiple(),attradd);
		case PATTACK:
			return base(ih.getInitPhysicalAttack(),strToFloat(ih.getPhysicalAttackGrowth()),level,ih.getPhysicalAttackGrowthMultiple(),attradd);
		case PDEFENCE:
			return base(ih.getInitPhysicalDefence(),strToFloat(ih.getPhysicalDefenceGrowth()),level,ih.getPhysicalDefenceGrowthMultiple(),attradd);
		case MATTACK:
			return base(ih.getInitMagicAttack(),strToFloat(ih.getMagicAttackGrowth()),level,ih.getMagicAttackGrowthMultiple(),attradd);
		case MDEFENCE:
			return base(ih.getInitMagicDefence(),strToFloat(ih.getMagicDefenceGrowth()),level,ih.getMagicDefenceGrowthMultiple(),attradd);
		case SPEED:
			return base(ih.getInitSpeed(),strToFloat(ih.getSpeedGrowth()),level,ih.getSpeedGrowthMultiple(),attradd);
		
		case HIT:
			return base(ih.getInitHit(),strToFloat(ih.getHitGrowth()),level,ih.getHitGrowthMultiple(),attradd);
		case DODGE:
			return base(ih.getInitDodge(),strToFloat(ih.getDodgeGrowth()),level,ih.getDodgeGrowthMultiple(),attradd);
		case CRITICAL:
			return base(ih.getInitCritical(),strToFloat(ih.getCriticalGrowth()),level,ih.getCriticalGrowthMultiple(),attradd);
		case TENACITY:
			return base(ih.getInitTenacity(),strToFloat(ih.getTenacityGrowth()),level,ih.getTenacityGrowthMultiple(),attradd);
		
/*		case PADD_PERSENT:
			return strToFloat(ih.getBasePhyDamageIncrease());
		case PDEL_PERSENT:
			return strToFloat(ih.getBasePhyDamageDecrease());
		case MADD_PERSENT:
			return strToFloat(ih.getBaseMagDamageIncrease());
		case MDEL_PERSENT:
			return strToFloat(ih.getBaseMagDamageDecrease());
		case CRITICAL_PERSENT:
			return strToFloat(ih.getBaseCriticalDamage());*/
		
		case DAMAGEADD:
			return (float)ih.getDamageIncrease();
		case DAMAGEDEL:
			return (float)ih.getDamageDecrease();

		}
		return (float) 0;
	}
	
	
	/**
	 * 角色基础值+等级成长值*等级*等级修正+属性培养值
	 * @param init
	 * @param growth
	 * @param level
	 * @param growthmu
	 * @param attradd
	 * @return
	 */
	public static float base(int init,float growth,int level,int growthmu,int attradd){
		float result = (float)init + growth * (float)level * (float)growthmu + (float)attradd;
		return result;
	}
	
	
	private static Float strToFloat(String str){
		try{
			return Float.parseFloat(str);
		}catch(Exception e){
			logger.error("AttrCompute strToFloat error. String ="+str , e);
			e.printStackTrace();
		}
		return (float) 0;
	} 	
}
