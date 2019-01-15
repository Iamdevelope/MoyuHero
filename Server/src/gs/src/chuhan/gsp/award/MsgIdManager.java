package chuhan.gsp.award;

/**
 * 消息提醒管理类
 * @author aa
 *
 */
public enum MsgIdManager {	

	HEROBAG_IS_FULL(100000001,"英雄背包已满"),
	ITEMBAG_IS_FULL(100000002,"物品背包已满");


	private int type;
	private String actionName;
	
	private MsgIdManager(int type, String actionName){
		this.type = type;
		this.actionName = actionName;
	}
	
	public int getType() {
		return type;
	}

	public String getActionName() {
		return actionName;
	}
	
}
