package chuhan.gsp.state;

import org.apache.log4j.Logger;

public class StateManager
{
	public static final Logger logger = Logger.getLogger(StateManager.class);
	/**
	 * 必须在Procedure内调用
	 * 获取IState实例
	 * @param roleId
	 * @return
	 */
	public static IState getStateByRoleId(final long roleId)
	{
		Integer state =xtable.Roleonoffstate.get(roleId);
		if(state == null)
		{
			state = State.UNENTRY_STATE;
			xtable.Roleonoffstate.add(roleId, state);
		}
		switch(state)
		{
		case State.UNENTRY_STATE:// 未登录状态
			return new UnEntryState(roleId);
		case State.PRE_ENTRY_STATE: // 准备登录状态
			return new PreEntryState(roleId);
		case State.ENTRY_STATE: // 已登录状态		
			return new EntryState(roleId);
		case State.PRE_OFFLINE_PROTECT_STATE: // 准备下线保护状态
			return new PreOfflineProtectState(roleId);
		/*case State.OFFLINE_PROTECT_STATE: // 下线保护状态
			return new OfflineProtectState(roleId);
		case State.BREAK_OFFLINE_PROTECT_STATE: // 中断下线保护状态
			return new BreakOfflineProtectState(roleId);
		case State.END_OFFLINE_PROTECT_STATE: // 结束下线保护状态
			return new EndOfflineProtectState(roleId);
		case State.PRE_TRUSTEESHIP_STATE: // 准备战斗托管状态
			return new PreTrusteeShipState(roleId);
		case State.TRUSTEESHIP_STATE: // 准备战斗托管状态
			return new TrusteeShipState(roleId);
		case State.BREAK_TRUSTEESHIP_STATE: // 中断战斗托管状态
			return new BreakTrusteeShipState(roleId);*/
		default:
			throw new IllegalStateException("错误的状态类型："+state);
		}
	}
	
	/**
	 * 必须在Procedure内调用
	 * 获取StateId
	 * 参考State内StateId的定义
	 * @param roleId
	 * @return
	 */
	public static int getStateIdByRoleId(final long roleId)
	{
		Integer state =xtable.Roleonoffstate.get(roleId);
		if(state == null)
			return State.UNENTRY_STATE;
		return state;
	}
	
	/**
	 * 必须在Procedure内调用
	 * 获取StateId
	 * 参考State内StateId的定义
	 * @param roleId
	 * @return
	 */
	public static int selectStateIdByRoleId(final long roleId)
	{
		Integer state =xtable.Roleonoffstate.select(roleId);
		if(state == null)
			return State.UNENTRY_STATE;
		return state;
	}
	
	/**
	 * @param roleId
	 * @return
	 */
	public static boolean isEntry(final long roleId)
	{
		return selectStateIdByRoleId(roleId) == State.ENTRY_STATE;
	}
	
	/**
	 *  停服时的处理，所有在OfflineProtect的角色转入UnEntry状态
	 */
	public static void serverShutdown()
	{
		
	}
	
}
