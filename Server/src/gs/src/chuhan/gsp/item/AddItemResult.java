package chuhan.gsp.item;

import java.util.LinkedList;
import java.util.List;

import chuhan.gsp.award.AddItem;
import chuhan.gsp.util.Conv;

public class AddItemResult
{
	public enum AddResultEnum {
		SUCC, FULL, MAX_OWN_NUM, BIND_ITEM, POS_NOT_AVAILABLE, TIMEOUT_CANNOT_TRADE, CODE_EXCEPTION, FAIL
	}
	
	public AddResultEnum result;
	private List<AddItem> addItems = new LinkedList<AddItem>();
	
	public boolean isSuccess()
	{
		return result == AddResultEnum.SUCC;
	}
	
	public AddItemResult()
	{
		result = AddResultEnum.SUCC;
	}
	public AddItemResult(AddItemResult.AddResultEnum result)
	{
		this.result = result;
	}
	
	public AddResultEnum getResult()
	{
		return result;
	}
	
	public List<AddItem> getAddItems()
	{
		return addItems;
	}
	
	public int getSumNum()
	{
		int sum = 0;
		for(AddItem add : addItems)
			sum += add.getNum();
		return sum;
	}
	
	public SShowAddItem getSShowAddItem()
	{
		SShowAddItem snd = new SShowAddItem();
		for(AddItem add : addItems)
			snd.data.add(getShowItemData(add));
		return snd;
	}
	public ShowItemData getShowItemData(AddItem add )
	{
		return new ShowItemData(add.getKey(), Conv.toShort(add.getId()), Conv.toShort(add.getNum()));
	}
	
}

