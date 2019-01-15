/**
 * Class: Item.java
 * Package: knight.gsp.activity.award
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2011-4-23 		yesheng
 *
 * Copyright (c) 2011, Perfect World All Rights Reserved.
*/

package chuhan.gsp.award;

/**
 * ClassName:AddItem
 * Function: 添加的物品，可以是Hero,奖励可能包含多个物品,即有多个Item
 *
 * @see 	 
 */
public class AddItem {
	private int key;
	private int id;
	private int num;
	public AddItem(int key, int id, int num) {
		this.key = key;
		this.id = id;
		this.num = num;
	}
	public int getKey() {
		return key;
	}
	public int getId() {
		return id;
	}
	public int getNum() {
		return num;
	}
}

