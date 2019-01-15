/**
 * Class: SecondClassAwardItems.java
 * Package: knight.gsp.activity.award
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2011-4-20 		yesheng
 *
 * Copyright (c) 2011, Perfect World All Rights Reserved.
*/

package chuhan.gsp.award;

import java.util.List;

/**
 * ClassName:SecondClassAwardItems
 * Function: 随机奖励物品,包含随机类型,上限,和一个物品的list
 *
 * @author   yesheng
 * @version  
 * @since    
 * @Date	 2011-4-20		上午11:55:29
 *
 * @see 	 
 */
public class SecondClassAwardItems {
	
	public static final int UNRELATED_RANDOM=1;//不关联随机
	
	public static final int RELATED_RANDOM=0;//关联随机
	//随机奖励物品的总概率
	private int base;
	
	//随机奖励物品的随机方式
	private int randomType;
	
	//是否给予物品奖励的总开关,可能是公式
	private String totalProb;
	
	private List<SecondClassAwardItem> items;

	
	public int getBase() {
	
		return base;
	}

	
	public void setBase(int base) {
	
		this.base = base;
	}

	
	public int getRandomType() {
	
		return randomType;
	}

	
	public void setRandomType(int randomType) {
	
		this.randomType = randomType;
	}

	
	public List<SecondClassAwardItem> getItems() {
	
		return items;
	}

	
	public void setItems(List<SecondClassAwardItem> items) {
	
		this.items = items;
	}


	
	public String getTotalProb() {
	
		return totalProb;
	}


	
	public void setTotalProb(String totalProb) {
	
		this.totalProb = totalProb;
	}
}

