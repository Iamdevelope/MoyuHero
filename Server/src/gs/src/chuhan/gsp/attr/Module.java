package chuhan.gsp.attr;

import java.lang.reflect.Field;
import java.util.HashMap;
import java.util.Map;

import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.ModuleManager;
import chuhan.gsp.main.ReloadResult;

public class Module implements chuhan.gsp.main.ModuleInterface
{
	public static Logger logger = Logger.getLogger(Module.class);
	private java.util.Map<String, Integer> effectName2Ids;
	private java.util.Map<String, Integer> attrName2Ids;
	private java.util.Map<Integer, Float> attrId2InitValue;

	public static Module getInstance()
	{
		return ((Module)ModuleManager.getInstance().getModuleByName("attr"));
	}

	@Override
	public void init() throws Exception {
		
		Map<Integer, SAttributeDesConfig> sAttrEffects = ConfigManager.getInstance().getConf(SAttributeDesConfig.class);
		Map<String, Integer> effectName2Ids = new java.util.HashMap<String, Integer>();
		Map<Integer, Float> attrId2InitValue = new java.util.HashMap<Integer, Float>();
		Map<String, Integer> attrName2Ids = new HashMap<String, Integer>();
		
		for (SAttributeDesConfig attreffect : sAttrEffects.values()) {
			effectName2Ids.put(attreffect.getName(), attreffect.getId());
			effectName2Ids.put(attreffect.getNumName(),
					attreffect.getNumid());
			effectName2Ids.put(attreffect.getPctName(),
					attreffect.getPercentid());
			attrId2InitValue.put(attreffect.getId(),
					(float) attreffect.getInit());
			attrName2Ids.put(attreffect.getName(), attreffect.getId());
		}
		this.effectName2Ids = effectName2Ids;
		this.attrId2InitValue = attrId2InitValue;
		this.attrName2Ids = attrName2Ids;
	}

	@Override
	public void exit()
	{
	}


	public final static int[] clientAttrTypeIds;
	static
	{

		Field[] fields = AttrType.class.getFields();
		clientAttrTypeIds = new int[fields.length];
		for (int i = 0; i < fields.length; i++)
		{
			try
			{
				clientAttrTypeIds[i] = fields[i].getInt(null);
			} catch (IllegalArgumentException e)
			{
				e.printStackTrace();
			} catch (IllegalAccessException e)
			{
				e.printStackTrace();
			}
		}
	}

	public final static int[] CalcAttrTypeIds;// 要计算的属性
	static
	{

		Field[] fields = CalcAttrType.class.getFields();
		CalcAttrTypeIds = new int[fields.length];
		for (int i = 0; i < fields.length; i++)
		{
			try
			{
				CalcAttrTypeIds[i] = fields[i].getInt(null);
			} catch (IllegalArgumentException e)
			{
				e.printStackTrace();
			} catch (IllegalAccessException e)
			{
				e.printStackTrace();
			}
		}
	}
	
	public static boolean isCalcAttr(int attrid)
	{
		if(attrid <= 0)
			return false;
		for(int i = 0 ; i <CalcAttrTypeIds.length; i++)
		{
			if(CalcAttrTypeIds[i] == attrid)
				return true;
		}
		return false;
	}

	
	@Override
	public ReloadResult reload() throws Exception
	{
		return new ReloadResult(false,"module" + this.getClass().getName() + "not support reload");
	}
	
	/**
	 * 根据效果名称获取效果ID
	 * @param effectname
	 * @return
	 */
	public Integer getIdByName(String effectname)
	{
		String name = effectname.trim();
		return effectName2Ids.get(name);
	}
	
	/**
	 * 根据效果ID获取效果名称
	 * @param effectId
	 * @return
	 */
	public String getEffectNameById(int effectId)
	{
		for(Map.Entry<String, Integer> entry: effectName2Ids.entrySet())
		{
			if(entry.getValue() == effectId)
				return entry.getKey();
		}
		return "未知效果:"+effectId;
	}

	public static Map<Integer, Float> parseEffects(String effectstrs) throws Exception
	{
		Map<Integer, Float> effectMap = new HashMap<Integer, Float>();
		if (effectstrs != null && !effectstrs.equals(""))
		{
			String[] effectStr = effectstrs.split(";");
			for (int i = 0; i < effectStr.length; i++)
			{
				String[] tmpstrs = effectStr[i].split("=");
				if (tmpstrs.length < 2)
					throw new Exception("Wrong effects str.");

				int effectId = Module.getInstance().getIdByName(tmpstrs[0].trim());// 第一个“=”之前的字符串是Effectname
				Float effectValue = Float.valueOf((effectStr[i].substring(tmpstrs[0].length() + 1)).trim());// 取第一个“=”之后的字符串，是效果值
				effectMap.put(effectId, effectValue);
			}
		}
		return effectMap;
	}

	public float getInitValueByAttrId(int attrId)
	{
		Float inivalue = attrId2InitValue.get(attrId);
		if (inivalue == null)
			return 0f;
		else
			return inivalue;
	}
	
	public int getAttrIdByName(String name)
	{
		Integer id = attrName2Ids.get(name);
		if(id == null)
			return 0;
		return id;
	}
	
	/**
	 * 获取改变的客户端可见属性，从attrs中过滤出客户端可见的属性
	 * 用于将属性变化发送至客户端前，因为客户端不需要显示隐含属性，将其过滤
	 * @return 需要发给客户端的属性
	 */
	public static Map<Integer, Float> getClientAttrs(Map<Integer, Float> attrs)
	{
		Map<Integer,Float> clientAttrs = new HashMap<Integer, Float>();
		
		for(int attrId : clientAttrTypeIds)
		{
			Float value = attrs.get(attrId);
			if(value != null)
				clientAttrs.put(attrId, value);
		}
		
		return clientAttrs;
	}
}
