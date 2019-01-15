package chuhan.gsp.state;

/**
 * 角色的未登录状态
 * @author lc
 *
 */
public class UnEntryState extends State {
	
	public UnEntryState(long roleId)
	{
		super(roleId);
	}
	
	@Override
	public boolean enter(int trigger) {
		
		Integer oldstate = xtable.Roleonoffstate.get(roleId);
		if (oldstate == null)
			oldstate = State.UNENTRY_STATE;
		boolean valid = false;
		if(trigger == State.TRIGGER_PROCESS_DONE && oldstate == State.PRE_OFFLINE_PROTECT_STATE)
			valid = true;
		if(!valid)
		{
			enterErrorLog(oldstate, trigger);
			return false;
		}
		xtable.Roleonoffstate.remove(roleId);
		xtable.Roleonoffstate.add(roleId, getState());
		StateManager.logger.info("角色（Id = " + roleId + "）进入状态：" + this.getClass());
		
		return execute();
	}

	@Override
	public boolean execute() {
		
		
		
		
		xtable.Roleonoffstate.remove(roleId);
		return true;
	}

//	@Override
//	public boolean exit() {
//		return false;
//		// TODO 退出未登录状态的操作，无。
//		
//	}
	
	public boolean trigger(int trigger)
	{
		if(trigger == State.TRIGGER_ONLINE)
			return new PreEntryState(roleId).enter(trigger);
		triggerErrorLog(trigger);
		return false;
	}
	
	@Override
	public int getState(){
		return State.UNENTRY_STATE;
	}
}
