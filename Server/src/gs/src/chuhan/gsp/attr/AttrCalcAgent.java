package chuhan.gsp.attr;

import java.util.HashMap;
import java.util.Map;

public abstract class AttrCalcAgent extends AttrAgent{

	/**
	 * 通过名字获取属性值
	 * 只能拿到CalcAttrType中的属性
	 * @param name
	 * @return
	 */
	public float getAttrByName(String name)
	{
		int attrId = Module.getInstance().getAttrIdByName(name);
		if(attrId <= 0)
			return 0f;
		
		return getAttrById(attrId);
	}
	
	protected float calcEffectBonus(int attrType, float attrValue)
	{
		Float abl = getEffects().get(attrType + 1);
		if (abl == null)
			abl = 0f;
		
		Float pct = getEffects().get(attrType + 2);
		if (pct == null)
			pct = 0f;
		float value = (float) ((attrValue + abl) * (1 +  pct));
		return value;
	}
	
	public void attachEffect(int effectType, float value)
	{
		if(!validEffectId(effectType))
			return;
		Float oldValue = getEffects().get(effectType);
		if (oldValue == null)
		{
			getEffects().put(effectType, value);
		} else
		{
			float newValue = oldValue;
			if(effectType %10 == 1)//值的效果以累加的结果保存
				newValue = oldValue + value;
				//newValue = oldValue + (int)value;  FIXME 记住，这里不再取整，因为有的效果就是小于1的，例如伤害修正类
			else if(effectType %10 == 2)
				newValue = oldValue + value;
				//newValue = (1+oldValue)*(1+value) -1;//百分比的效果以累乘的结果保存
			if(newValue != 0)
				getEffects().put(effectType, newValue);
			else
				getEffects().remove(effectType);
		}
	}
	
	public void detachEffect(int effectType, float value)
	{
		if(!validEffectId(effectType))
			return;
		Float oldValue = getEffects().get(effectType);
		if (oldValue == null)
		{
			getEffects().put(effectType, value);
		} else
		{
			float newValue = oldValue;
			if(effectType %10 == 1)//值的效果以累加的结果保存
				newValue = oldValue - value;
				//newValue = oldValue - (int)value; FIXME 记住，这里不再取整，因为有的效果就是小于1的，例如伤害修正类
			else if(effectType %10 == 2)
				newValue = oldValue - value;
				//newValue = (1+oldValue)/(1+value) -1;//百分比的效果以累乘的结果保存
			if(newValue != 0)
				getEffects().put(effectType, newValue);
			else
				getEffects().remove(effectType);
		}
	}
	
	public void attachEffects(Map<Integer, Float> effects)
	{
		if(effects == null)
			return;
		for (Integer effectType : effects.keySet())
		{
			attachEffect(effectType, effects.get(effectType));
		}
	}

	public void detachEffects(Map<Integer, Float> effects)
	{
		if(effects == null)
			return;
		for (Integer effectType : effects.keySet())
		{
			detachEffect(effectType, effects.get(effectType));
		}
	}
	
	private boolean validEffectId(int effectId) 
	{
		return Module.isCalcAttr((effectId /10) * 10);
	}
	
	public Map<Integer, Float> updateAllFinalAttrs()
	{
		Map<Integer, Float> finalAttrs = getFinalAttrs();
		Map<Integer, Float> changedFinalAttrs = new HashMap<Integer, Float>();
		for (int i = 0; i < Module.CalcAttrTypeIds.length; i++)
		{
			Float newValue = 0f;
			newValue = calcAttr(Module.CalcAttrTypeIds[i]);
			Float oldvalue = finalAttrs.get(Module.CalcAttrTypeIds[i]);
			Float initvalue = Module.getInstance().getInitValueByAttrId(Module.CalcAttrTypeIds[i]);
			//两种情况说明属性有变化：1.没有旧值 && 新值 != 初始值，2.有旧值 && 旧值 != 新值
			if(oldvalue == null && !newValue.equals(initvalue))
			{
				finalAttrs.put(Module.CalcAttrTypeIds[i], newValue);
				changedFinalAttrs.put(Module.CalcAttrTypeIds[i], newValue);
			}
			else if(oldvalue != null && !oldvalue.equals(newValue))
			{
				if(newValue.equals(initvalue))
					finalAttrs.remove(Module.CalcAttrTypeIds[i]);
				else
					finalAttrs.put(Module.CalcAttrTypeIds[i], newValue);
				changedFinalAttrs.put(Module.CalcAttrTypeIds[i], newValue);
			}
		}
		/*if(changedFinalAttrs.containsKey(AttrType.UP_LIMITED_HP))
			changedFinalAttrs.put(AttrType.HP, (float) this.getHp());
		if(changedFinalAttrs.containsKey(AttrType.MAX_HP))
			changedFinalAttrs.put(AttrType.HP,(float)this.getHp());
		if(changedFinalAttrs.containsKey(AttrType.MAX_MP))
			changedFinalAttrs.put(AttrType.MP, (float) this.getMp());*/
		return changedFinalAttrs;
	}
	
	protected abstract Map<Integer,Float> getEffects();
	protected abstract xbean.BasicFightProperties getBfp();
	protected abstract int getSpeed(); 
	
	protected float calcAttr(int attrType)
	{
		switch(attrType)
		{
		case AttrType.ATTACK:
			return calcEffectBonus(attrType, getBfp().getAttack());
		case AttrType.DEFEND:
			return calcEffectBonus(attrType, getBfp().getDefend());
		case AttrType.SKILL:
			return calcEffectBonus(attrType, getBfp().getWisdom());
		case AttrType.ARMY:
			return calcEffectBonus(attrType, getBfp().getHp());
		case AttrType.SPEED:
			return calcEffectBonus(attrType, getSpeed());
		}
		return calcDefaultAttr(attrType);
	}
	
	protected float calcDefaultAttr(int attrType)
	{
		return calcEffectBonus(attrType, Module.getInstance().getInitValueByAttrId(attrType));//放入初值
	}
	

}
