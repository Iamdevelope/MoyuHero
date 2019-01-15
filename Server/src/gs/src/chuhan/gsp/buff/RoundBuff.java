package chuhan.gsp.buff;


import xbean.Buff;
/**
 * 回合Buff
 */
public class RoundBuff extends ContinualBuff
{

	public RoundBuff(Buff buffBean)
	{
		super(buffBean);
	}
	
	public RoundBuff(ContinualBuffConfig buffConfig)
	{
		super(buffConfig);
	}
	
	/**
	 * 附加buff，由BuffAgent调用add类方法，不要直接使用
	 */
	/*
	public Result attach(BuffAgent buffAgent)
	{
		//TODO 检查buff冲突和叠加
		buffAgent.addBuffBean(buffBean);
		knight.gsp.effect.Role erole = buffAgent.getERole();
		erole.attachEffects(buffBean.getEffects());
		Result result = new Result(true);
		result.addAddedBuff(this);
		// 更新属性
		result.updateChangedAttrs(erole.updateAllFinalAttrs());
		
		return result;
	}*/
}
