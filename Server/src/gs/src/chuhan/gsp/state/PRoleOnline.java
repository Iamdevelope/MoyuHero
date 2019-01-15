package chuhan.gsp.state;

import java.util.HashMap;

import com.pwrd.op.LogOpChannel;

import chuhan.gsp.PProcessMacOnline;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.log.OpLogManager;
import chuhan.gsp.log.RemoteLogID;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.LogUtil;


public class PRoleOnline extends xdb.Procedure
{
	final private long roleId;
	final private int userId;
	final private String mac;
	public long getRoleId()
	{
		return roleId;
	}

	public int getUserId()
	{
		return userId;
	}

	public PRoleOnline(int userId, long roleId, String mac)
	{
		this.userId = userId;
		this.roleId = roleId;
		this.mac = mac;
	}
	
	@Override
	protected boolean process()
	{
		StateManager.logger.info("角色（Id = " + roleId + "）开始登录：" + this.getClass());
		final xbean.User u = xtable.User.get(userId);
		for(long rid : u.getIdlist())
		{
			if(rid != roleId)
			{
				int stateid = StateManager.getStateIdByRoleId(rid);
				if(stateid != State.UNENTRY_STATE)
				{
					StateManager.logger.error("角色(Id = " + roleId + ")登录时，该账号的另一个角色(id = "+rid+" )还处在状态： "+ stateid);
					return false;
				}
			}
		}
		
		IState state = StateManager.getStateByRoleId(roleId);
		if(state.trigger(State.TRIGGER_ONLINE))
		{
			new PProcessMacOnline(roleId, mac).call();
			//上线成功
			StateManager.logger.info("角色（Id = " + roleId + "）上线成功，MAC:"+mac);
			//LC: 记录远程日志
			doRoleLoginLog(roleId);
			return true;
		}
		else
			return false;//上线失败的处理在DRoleOnlineFail中处理
	}

	private void doRoleLoginLog(long roleId)
	{
		try{
			PropRole propRole = PropRole.getPropRole(roleId, true);
			if(null != propRole) {
				OpLogManager.getInstance().doLogWhileCommit(LogOpChannel.LOGIN, roleId, 
						GameTime.currentTimeMillis(), propRole.getProperties().getMac(),
						DateUtil.getCurrentStringFormatEn(GameTime.currentTimeMillis()),
						propRole.getProperties().getUsername(), 
						propRole.getProperties().getRolename(), propRole.getProperties().getLevel());
			}
			
			java.util.Map<String, Object> params = LogUtil.putRoleBasicParams(roleId, false, new HashMap<String, Object>());
			LogManager.getInstance().doLogWhileCommit(RemoteLogID.ROLELOGIN, params);
		}catch(Exception e)
		{
			e.printStackTrace();
		}
	}
}
