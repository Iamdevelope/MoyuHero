/**
 * Class: AwardItem.java
 * Package: knight.gsp.activity.award
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2011-1-28 		yesheng
 *
 * Copyright (c) 2011, Perfect World All Rights Reserved.
 */

package chuhan.gsp.award;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

/**
 * ClassName:AwardItem Function:
 * 不同于FirstClassAwardItem和secondClassAwardItem,该类所代表的是真正奖励到玩家手中的
 * 物品.而FirstClassAwardItem和secondClassAwardItem的id都只是一个索引,索引指向物品类型库里的id
 * 
 * @version
 * @since
 * 
 * @see FirstClassAwardItem, SecondClassAwardItem
 */
public class AwardItem {

	private long value;//经验,金钱的数值
	
	private List<AddItem> items = new LinkedList<AddItem>();
	public AwardItem(long value){
		this.value = value;
	}
	public AwardItem() {
	}
	public long getValue() {
		return value;
	}
	public void setValue(long value) {
		this.value = value;
	}
	public List<AddItem> getItems() {
		return items;
	}
	
	public boolean itemNotEmpty(){
		if (items!=null&&items.size()>0)
			return true;
		return false;
	}

}
