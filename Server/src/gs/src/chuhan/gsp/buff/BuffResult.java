package chuhan.gsp.buff;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import chuhan.gsp.buff.ContinualBuff;

/**
 * 此Result包括buff和属性的变化
 * 广泛用于buff的添加，技能的释放等buff和属性变化的场合
 * Result的叠加使用updateResult方法
 *
 */
public class BuffResult
{
	private boolean success;
	private Map<Integer,Float> changedAttrs;
	
	private Map<Integer,chuhan.gsp.attr.Buff> addedBuffMap;
	private List<Integer> deletedBuffs;

	
	public BuffResult(boolean success)
	{
		this.success = success;
		if(success)
		{
			changedAttrs = new HashMap<Integer, Float>();
			addedBuffMap = new HashMap<Integer, chuhan.gsp.attr.Buff>();
			deletedBuffs = new ArrayList<Integer>();
		}
	}

	public void clear()
	{
		changedAttrs.clear();
		addedBuffMap.clear();
		deletedBuffs.clear();
	}
	
	/**
	 * 更新Result，当buff状态冲突时：
	 * 先 Add 再 Add = Add
	 * 先Add 再 Delete = 全都删除
	 * 先Delete 再 Add = Add
	 * 先Delete 再 Delete = Delete
	 * @param result
	 */
	public void updateResult(BuffResult result)
	{
		if(result.isSuccess())
		{	
			if(result.getChangedAttrs().size() != 0)
				changedAttrs.putAll(result.getChangedAttrs());
			for(Map.Entry<Integer, chuhan.gsp.attr.Buff> buff : result.getAddedBuffMap().entrySet())
			{
				this.addAddedBuff(buff.getKey(),buff.getValue());
			}
			for(Integer buffId : result.getDeletedBuffs())
			{
				this.addDeletedBuff(buffId);
			}
		}
	}
	
	public void updateChangedAttrs(Map<Integer, Float> newChangedAttrs)
	{
		this.changedAttrs.putAll(newChangedAttrs);
	}
	
	public boolean isSuccess()
	{
		return success;
	}

	public void setSuccess(boolean success)
	{
		this.success = success;
	}

	/**
	 * 获取改变的属性
	 * @return
	 */
	public Map<Integer, Float> getChangedAttrs()
	{
		return changedAttrs;
	}


	public void setChangedAttrs(Map<Integer, Float> changedAttrs)
	{
		this.changedAttrs = changedAttrs;
	}
	
	public Map<Integer,chuhan.gsp.attr.Buff> getAddedBuffMap()
	{
		return addedBuffMap;
	}
	
	public List<Integer> getDeletedBuffs()
	{
		return deletedBuffs;
	}

	/**
	 * 更新Result AddedfBuffs list，当buff状态冲突时：
	 * 先 Delete 再 Add = Add
	 * 先 Add 再 Add = Add
	 * @param ContinualBuff
	 */
	public void addAddedBuff(ContinualBuff buff)
	{
		addAddedBuff(buff.getBuffBean());
	}
	
	public void addAddedBuff(xbean.Buff xbuff)
	{
		//先delete 后add
		if(deletedBuffs.contains((Integer)xbuff.getId()))
		{
			deletedBuffs.remove(xbuff.getId());
			addedBuffMap.put(xbuff.getId(),getPrtlBuffFromXBuff(xbuff));
			return;
		}
		//先add 又add
		//先前不存在
		addedBuffMap.put(xbuff.getId(),getPrtlBuffFromXBuff(xbuff));
	}
	
	/**
	 * 更新Result AddedfBuffs list，当buff状态冲突时：
	 * 先 Delete 再 Add = Add
	 * 先 Add 再 Add = Add
	 * @param ContinualBuff
	 */
	private void addAddedBuff(int buffId , chuhan.gsp.attr.Buff buff)
	{
		//先delete 后add
		if(deletedBuffs.contains(buffId))
		{
			deletedBuffs.remove((Integer)buffId);
			addedBuffMap.put(buffId,buff);
			return;
		}
		//先add 又add
		//先前不存在
		addedBuffMap.put(buffId,buff);
	}
	
	
	/**
	 * 更新Result DeletedfBuffs List，当buff状态冲突时：
	 * 先 Add 再 Delete = 全都删除
	 * 先 Delete 再 Delete = Delete
	 * @param ContinualBuff
	 */
	public void addDeletedBuff(int buffId)
	{
		//先add 后delete
		if(addedBuffMap.containsKey(buffId))
		{
			addedBuffMap.remove(buffId);
			return;
		}
		//先delete 后delete
		if(deletedBuffs.contains((Integer)buffId))
			return;
		//先前不存在
		deletedBuffs.add(buffId);
	}
	
	/**
	 * 更新Result DeletedfBuffs List，当buff状态冲突时：
	 * 先 Add 再 Delete = 全都删除
	 * 先 Delete 再 Delete = Delete
	 * @param ContinualBuff
	 */
	public void addDeletedBuff(ContinualBuff buff)
	{
		//先add 后delete
		if(addedBuffMap.containsKey(buff.getId()))
		{
			addedBuffMap.remove(buff.getId());
			return;
		}
		//先delete 后delete
		if(deletedBuffs.contains(buff.getId()))
			return;
		//先前不存在
		deletedBuffs.add(buff.getId());
	}

	
	private chuhan.gsp.attr.Buff getPrtlBuffFromXBuff(xbean.Buff buff)
	{
		chuhan.gsp.attr.Buff ptrlbuff = new chuhan.gsp.attr.Buff();
		ptrlbuff.count = buff.getRound();
		//剩余时间，以秒为单位
		if(buff.getTime() <= 0)
			ptrlbuff.time = 0;
		else
		{
			ptrlbuff.time = (buff.getTime() - (chuhan.gsp.main.GameTime.currentTimeMillis() - buff.getAttachtime())) / 1000;
			if (ptrlbuff.time < 0)
				ptrlbuff.time = 0;
		}
		return ptrlbuff;
	}
}
