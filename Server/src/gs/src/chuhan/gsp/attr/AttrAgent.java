package chuhan.gsp.attr;

import java.util.Map;

public abstract class AttrAgent
{

	/**
	 * 只能拿到CalcAttrType中的属性
	 */
	public float getAttrById(int attrId)
	{
		Float value = getFinalAttrs().get(attrId);
		if(value == null)
		{
			value = Module.getInstance().getInitValueByAttrId(attrId);
			if(value == null)
				return 0f;
			else
				return value;
		}
		else
			return value;
	}
	protected abstract Map<Integer,Float> getFinalAttrs();
	public abstract float getAttrByName(String name);
	protected abstract float calcEffectBonus(int attrType, float attrValue);
	public abstract void attachEffects(Map<Integer, Float> effects);
	public abstract void detachEffects(Map<Integer, Float> effects);
	public abstract Map<Integer, Float> updateAllFinalAttrs();
	public abstract void addHp(int v);
	public abstract int getHp();
}
