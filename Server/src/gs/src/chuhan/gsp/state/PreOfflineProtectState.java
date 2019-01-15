package chuhan.gsp.state;

import java.util.HashMap;

import com.pwrd.op.LogOpChannel;

import chuhan.gsp.PProcessMacOffline;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.battle.realtime.RoomManager;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.log.OpLogManager;
import chuhan.gsp.log.RemoteLogID;
import chuhan.gsp.log.RemoteLogParam;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.LogUtil;


/**
 * 下线保护准备状态
 * @author DevUser
 *
 */
public class PreOfflineProtectState extends State{

	public PreOfflineProtectState(long roleId)
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
		if(trigger == State.TRIGGER_OFFLINE && oldstate == State.ENTRY_STATE)
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
	
	/**
	 * 因为下线不可逆，下线的清理要用Procedure.call()或者直接xdb.Procedure.pexecute(xdb.Procedure)
	 * 这样不会影响到状态转换的正常进行
	 */
	@Override
	public boolean execute() {

		try
		{
			PropRole prole = PropRole.getPropRole(roleId, false);
//			prole.getProperties().setContinuelogin(prole.getContinueLoginDays());
			prole.getProperties().setOfflinetime(GameTime.currentTimeMillis());
			
			StringBuilder sb = new StringBuilder("角色登出,roleid:");
			sb.append(roleId).append(",rolename:").append(prole.getProperties().getRolename()).append(",userid:").append(prole.getProperties().getUserid());
			StateManager.logger.info(sb.toString());
			
			//在等待队列删除此人
			RoomManager.getInstance().removeWait(roleId);
			
			doRoleLogoutLog(prole);
		} catch (Exception e)
		{
			StateManager.logger.error("下线保护准备状态：" + this.getClass() + " 执行出错，可能有部分下线处理没有执行。", e);
		}
		pexecute();
		this.trigger(State.TRIGGER_PROCESS_DONE);
		return true;//下线的状态转换必须执行成功，不可逆
	}
	
	/**
	 * 异步执行的模块下线处理写在此
	 * 注意，此时地图模块和link模块的role已经移除完毕，异步执行的其他模块处理无法用到这俩模块
	 * 如果下线时，这些处理有很小的几率会未执行完
	 */
	private void pexecute()
	{
		xdb.Procedure.pexecute(new PProcessMacOffline(roleId));
	}

	@Override
	public boolean trigger(int trigger)
	{
		if(trigger==State.TRIGGER_PROCESS_DONE)
			return new UnEntryState(roleId).enter(trigger);
		triggerErrorLog(trigger);
		return false;
	}
	
	@Override
	public int getState(){
		return State.PRE_OFFLINE_PROTECT_STATE;
	}
	
	private void doRoleLogoutLog(PropRole prole)
	{
		try{
			PropRole propRole = PropRole.getPropRole(roleId, true);
			if(null != propRole) {
				long period = prole.getProperties().getOfflinetime() - prole.getProperties().getOnlinetime();
				OpLogManager.getInstance().doLogWhileCommit(LogOpChannel.LOGIN, roleId, 
						GameTime.currentTimeMillis(), propRole.getProperties().getUsername(),
						propRole.getProperties().getRolename(), propRole.getLevel(),
						DateUtil.getCurrentStringFormatEn(GameTime.currentTimeMillis()),
						period / 1000, null, null, null, null, null, null);
			}
			
			
			java.util.Map<String, Object> params = LogUtil.putRoleBasicParams(roleId, false, new HashMap<String, Object>());
			long period = prole.getProperties().getOfflinetime() - prole.getProperties().getOnlinetime();
			params.put(RemoteLogParam.TIME, period/1000);
			LogManager.getInstance().doLogWhileCommit(RemoteLogID.ROLELOGOUT, params);
		}catch(Exception e)
		{
			e.printStackTrace();
		}
	}
}
