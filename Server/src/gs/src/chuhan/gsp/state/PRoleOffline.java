package chuhan.gsp.state;

import gnet.link.Onlines;

public class PRoleOffline extends xdb.Procedure
{
	final private long roleId;
	final private int type;
	
	final public static int TYPE_OFFLINE = 1;//主动下线，退出游戏
	final public static int TYPE_LINK_BROKEN = 2;//被动断线
	final public static int TYPE_CHOSEE_ROLE = 3;//返回人物选择界面
	final public static int TYPE_RETURN_LOGIN= 4;//返回登录界面
	
	public PRoleOffline(long roleId,int type)
	{
		this.roleId = roleId;
		this.type = type;
	}
	
	@Override
	protected boolean process()
	{
		xbean.Properties prop = xtable.Properties.select(roleId);
		if(prop == null)
			return true;
		int userid = prop.getUserid();
		try
		{
			boolean succ = false;
			IState state = StateManager.getStateByRoleId(roleId);
			succ = state.trigger(State.TRIGGER_OFFLINE);
			if(!succ)
				return false;
		}
		catch(Exception e)
		{
			StateManager.logger.error("下线处理出错");
		}
		gnet.link.Onlines.getInstance().remove(roleId);
		Onlines.getInstance().getOnlineUsers().offline(userid, true);
		return true;
	}
	
	/*public static void doRoleLogoutLog(PropRole prole)
	{
		Map<String, Object> param = new HashMap<String, Object>();
		LogUtil.putRoleBasicParams(prole, param);
		param.put(RemoteLogParam.MAPID,(int)(prole.getProperties().getSceneid()));
		param.put(RemoteLogParam.X,(int)(prole.getProperties().getPosx()));
		param.put(RemoteLogParam.Y,(int)(prole.getProperties().getPosy()));
		param.put(RemoteLogParam.TIME,(System.currentTimeMillis() - prole.getProperties().getOnlinetime())/1000);
		LogManager.getInstance().doLogWhileCommit(RemoteLogID.ROLELOGOUT, param);
	}
	
	private void lock(int userid)
	{
		Team team = TeamManager.getTeamByRoleId(roleId);
		lock(xdb.Lockeys.get(xtable.Locks.USERLOCK, new Object[] { userid }));// 锁userlock
		if(team != null && team.isTeamLeader(roleId))
			lock(xdb.Lockeys.get(xtable.Locks.ROLELOCK, new Object[] {team.getAllMemberIds()}));// 队长下线要锁正常队员
		else
			lock(xdb.Lockeys.get(xtable.Locks.ROLELOCK, new Object[] { roleId }));// 锁rolelock
	}
	*/
}
