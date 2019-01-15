package chuhan.gsp.state;

/**
 * 角色的已登录状态
 * @author lc
 *
 */

public class EntryState extends State {

	public EntryState(long roleId)
	{
		super(roleId);
	}
	
	@Override
	public boolean enter(int trigger)
	{
		Integer oldstate = xtable.Roleonoffstate.get(roleId);
		if (oldstate == null)
			oldstate = State.UNENTRY_STATE;
		boolean valid = false;
		if(trigger == State.TRIGGER_PROCESS_DONE)
			if(oldstate == State.PRE_ENTRY_STATE)
				valid = true;
		if(!valid)
		{
			enterErrorLog(oldstate, trigger);
			return false;
		}
		xtable.Roleonoffstate.remove(roleId);
		xtable.Roleonoffstate.add(roleId, State.ENTRY_STATE);
		StateManager.logger.info("角色（Id = " + roleId + "）进入状态：" + this.getClass());
		
		return execute();
	}
	
	
	@Override
	public boolean execute() {
		//TODO 进入已登录状态时的处理
			
		return true;
	}

//	@Override
//	public boolean exit() {
//		return false;
//		// TODO 状态结束时服务器所做的处理放在此处
//	}



	@Override
	public boolean trigger(int trigger)
	{
		//yanglk测试登录
//		trigger = State.TRIGGER_ONLINE;
//		if(trigger == State.TRIGGER_ONLINE)
//			return new PreEntryState(roleId).enter(trigger);
		
		if(trigger==State.TRIGGER_OFFLINE)
			return new PreOfflineProtectState(roleId).enter(trigger);
		/*if(trigger==State.TRIGGER_OFFLINE_BATTLE)
			return new PreTrusteeShipState(roleId).enter(trigger);*/
		if(trigger!=State.TRIGGER_BATTLE_END)
			triggerErrorLog(trigger);
		return false;
	}

	@Override
	public int getState(){
		return State.ENTRY_STATE;
	}
	
}
