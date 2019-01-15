package chuhan.gsp.buff;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import chuhan.gsp.buff.ContinualBuffConfig;

public class BuffConflicts
{
	final private int buffId;
	final private String name;
	private int defaultConflictMsgId = 0; 
	//将实体buff和state分开存放，是为了检查的时候提高效率
	final private List<Integer> conflictStates = new LinkedList<Integer>();//冲突状态
	final private List<Integer> conflictBuffs = new LinkedList<Integer>();//冲突buff，战斗内：这些buff的存在，使此buff不能选之为目标
	final private List<Integer> overrideBuffs = new LinkedList<Integer>();//此buff加载后，要覆盖的buff
	final private List<Integer> invalidBuffs = new LinkedList<Integer>();//无效buff，战斗内：这些buff的存在，使此buff无效
	final private Map<Integer,Integer> conflictMsgIds = new HashMap<Integer, Integer>();
	final private Set<Integer> conflictMapIds = new HashSet<Integer>();
	
	public BuffConflicts(int buffId, String name)
	{
		this.buffId = buffId;
		this.name = name;
	}
	
	public int getBuffId()
	{
		return buffId;
	}
	public void addConflictBuff(ContinualBuffConfig conf)
	{
		//将实体buff和state分开存放，是为了检查的时候提高效率
		if(conf == null || conf.getClassname() == null)
		{
			Module.logger.error("添加冲突buff时出错，没有持续性buff配置或者buff类名为空. BuffID = " + buffId);
			return;
		}
		if(conf.getClassname().equals("knight.gsp.buff.continual.NullBuff"))
			conflictStates.add(conf.getBuffTypeId());
		else
			conflictBuffs.add(conf.getBuffTypeId());
	}
	
	public List<Integer> getConflictStates()
	{
		return conflictStates;
	}
	public List<Integer> getConflictBuffs()
	{
		return conflictBuffs;
	}
	public List<Integer> getOverrideBuffs()
	{
		return overrideBuffs;
	}

	public int getDefaultConflictMsgId()
	{
		return defaultConflictMsgId;
	}

	public void setDefaultConflictMsgId(int defaultConflictMsgId)
	{
		this.defaultConflictMsgId = defaultConflictMsgId;
	}

	public Map<Integer, Integer> getConflictMsgIds()
	{
		return conflictMsgIds;
	}

	
	/**
	 * 从参数中的buffId中检查冲突
	 * @param buffIds
	 * @return 如果返回0，则无冲突；大于0，为冲突的buffID,注意，如果返回的ID小于1000，则返回的是冲突的buff类别
	 */
	public int checkBuffConflict(Set<Integer> buffIds)
	{
		
		for(int id : buffIds)
		{
			int bufftype = Module.getInstance().getDefaultCBuffConfig(id).getBuffType();
			for(int conflict :conflictBuffs)
			{
				if (conflict >= 1000)// buffId符合
				{
					if(conflict == id)
						return id;
				}
				else if (conflict >= 100)// 三级类型符合
				{
					if(conflict == bufftype)
						return id;
				}
				else if (conflict >= 10)// 二级类型符合
				{
					if(conflict == bufftype / 10)
						return id;
				}
				else if (conflict >= 1 )// 一级类型符合
				{
					if(conflict == bufftype / 100)
						return id;
				}
				
			}
		}
		return 0;
	}
	
	/**
	 * 从参数中的buffId中检查冲突
	 * @param buffIds
	 * @return 如果返回0，则无冲突；大于0，为冲突的buffID,注意，如果返回的ID小于1000，则返回的是冲突的buff类别
	 */
	public int checkBuffInvalid(Set<Integer> buffIds)
	{
		
		for(int id : buffIds)
		{
			int bufftype = Module.getInstance().getDefaultCBuffConfig(id).getBuffType();
			for(int invalid :invalidBuffs)
			{
				if (invalid >= 1000)// buffId符合
				{
					if(invalid == id)
						return id;
				}
				else if (invalid >= 100)// 三级类型符合
				{
					if(invalid == bufftype)
						return id;
				}
				else if (invalid >= 10)// 二级类型符合
				{
					if(invalid == bufftype / 10)
						return id;
				}
				else if (invalid >= 1 )// 一级类型符合
				{
					if(invalid == bufftype / 100)
						return id;
				}
				
			}
		}
		return 0;
	}
	
	/**
	 * 从参数中筛选出要覆盖冲掉的buffId
	 * @param buffIds
	 * @return
	 */
	public List<Integer> getOverridedBuffIds(Set<Integer> buffIds)
	{
		List<Integer> rmbuffs = new ArrayList<Integer>();
		for(int id : buffIds)
		{
			int bufftype = Module.getInstance().getDefaultCBuffConfig(id).getBuffType();
			for (int override : overrideBuffs)
			{
				if (override >= 1000)// buffId符合
				{
					if(override == id)
						rmbuffs.add(id);
				}
				else if (override >= 100)// 三级类型符合
				{
					if(override == bufftype)
						rmbuffs.add(id);
				}
				else if (override >= 10)// 二级类型符合
				{
					if(override == bufftype / 10)
						rmbuffs.add(id);
				}
				else if (override >= 1 )// 一级类型符合
				{
					if(override == bufftype / 100)
						rmbuffs.add(id);
				}
			}
		}
		return rmbuffs;
	}

	public String getName() {
		return name;
	}

	public List<Integer> getInvalidBuffs()
	{
		return invalidBuffs;
	}

	public Set<Integer> getConflictMapIds()
	{
		return conflictMapIds;
	}

	public boolean checkMapConflict(int mapId)
	{
		return conflictMapIds.contains(mapId);
	}
	
	
}
