package chuhan.gsp.state;
/**
 * 角色状态的虚基类
 * 提供角色状态的定义。
 * @author DevUser
 *
 */

public abstract class State implements IState{

	public final static int UNENTRY_STATE = 0; 			 // 未登录状态
	public final static int PRE_ENTRY_STATE = 1; 		 // 准备登录状态
	public final static int ENTRY_STATE = 2; 			 // 已登录状态
	public final static int PRE_OFFLINE_PROTECT_STATE = 3; // 准备下线保护状态
	/*public final static int OFFLINE_PROTECT_STATE = 4;    // 下线保护状态
	public final static int BREAK_OFFLINE_PROTECT_STATE = 5;    // 中断下线保护状态
	public final static int END_OFFLINE_PROTECT_STATE = 6;    // 结束下线保护状态
	public final static int PRE_TRUSTEESHIP_STATE = 7; // 准备战斗托管状态
	public final static int TRUSTEESHIP_STATE = 8;		 // 战斗托管状态
	public final static int BREAK_TRUSTEESHIP_STATE = 9; // 中断战斗托管状态
*/	
	
	//触发状态迁移的条件
	public final static int TRIGGER_ONLINE = 0;//角色上线
	public final static int TRIGGER_OFFLINE = 1;//非战斗中角色断线
	public final static int TRIGGER_OFFLINE_BATTLE = 2;//战斗中角色断线
	public final static int TRIGGER_PROCESS_DONE = 3;//服务器处理完成
	public final static int TRIGGER_TIME_OUT = 4;//计时器到时（下线保护时间到）
	public final static int TRIGGER_BATTLE_END = 5;//战斗结束（托管中）
	public final static int TRIGGER_OFFLINE_CHOSEE_ROLE = 6;//返回人物选择界面
	public final static int TRIGGER_OFFLINE_LINK_BROKEN = 7;//角色被动断线

	
	protected final long roleId;
	
	public State(long roleId)
	{
		this.roleId = roleId;
	}
	
	// 当进入 状态时执行
	protected abstract boolean execute();
	
	protected void triggerErrorLog(int trigger)
	{
		StateManager.logger.error("角色"+roleId+"状态"+this.getClass().getCanonicalName()+"转移失败，trigger = " + trigger);
	}
	protected void enterErrorLog(int oldstate, int trigger)
	{
		StateManager.logger.error("角色"+roleId+"状态"+this.getClass().getCanonicalName()+"转移失败，oldstate = "+oldstate+", trigger = " + trigger);
	}
}

