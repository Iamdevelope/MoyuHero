/**
 * Class: SecondClassAwardItem.java
 * Package: knight.gsp.activity.award
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2011-1-27 		yesheng
 *
 * Copyright (c) 2011, Perfect World All Rights Reserved.
 */

package chuhan.gsp.award;

/**
 * ClassName:SecondClassAwardItem 
 * Function: 随机奖励物品类
 * 
 * @version
 * @since
 * @see
 */
public class SecondClassAwardItem {

	private int itemID;

	private int itemNum;

	private String ratio;  //分子
	

	public SecondClassAwardItem(int itemID, int itemNum, String ratio) {

		super();
		this.itemID = itemID;
		this.itemNum = itemNum;
		this.ratio = ratio;
	}

	public int getItemID() {

		return itemID;
	}

	public void setItemID(int itemID) {

		this.itemID = itemID;
	}

	public int getItemNum() {

		return itemNum;
	}

	public void setItemNum(int itemNum) {

		this.itemNum = itemNum;
	}

	public String getRatio() {

		return ratio;
	}

	public void setRatio(String ratio) {

		this.ratio = ratio;
	}

	
}
