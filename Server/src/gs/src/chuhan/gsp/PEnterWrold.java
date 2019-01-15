package chuhan.gsp;

import java.util.Formatter;
import java.util.List;

public class PEnterWrold extends xdb.Procedure {

	private xio.Protocol protocol;
	private String mac;
	private long roleid;
	public PEnterWrold(xio.Protocol protocol, long roleid, String mac) {
		this.protocol = protocol;
		this.mac = mac;
		this.roleid = roleid;
	}
	@Override
	protected boolean process() throws Exception {
		final int userID=((gnet.link.Dispatch)protocol.getContext()).userid;
		xbean.User u = xtable.User.get(userID);
		if(null == u || u.getIdlist().isEmpty())
		{
			chuhan.gsp.state.StateManager.logger.error("CEnterWorld:账号（Id = " + userID + "） 登录失败。");
			return false;
		}
//		Formatter fo = new Formatter();
		
//		for(byte b : mac)
//		{
//			fo.format("%02X", b);
//		}
		boolean hasrole = false;
		for(long rid : u.getIdlist())
		{
			if(rid == roleid)
			{
				hasrole = true;
				break;
			}
		}
		if(!hasrole)
			return false;
		chuhan.gsp.state.StateManager.logger.info("CEnterWorld: 角色（Id = " + roleid + "）开始进入世界");
		//加入新的角色,这一句必须放在角色进入场景前
		gnet.link.Onlines.getInstance().insert(protocol, roleid);
		
		chuhan.gsp.state.PRoleOnline p = new chuhan.gsp.state.PRoleOnline(userID,roleid,mac);
		p.call();
		if(p.isSuccess())
			return true;
		long roleId = p.getRoleId();
		gnet.link.Onlines.getInstance().kick(roleId,KickErrConst.ERR_OLD_ONLINE_KICKOUT);//踢下线,TODO kick error code
		chuhan.gsp.state.StateManager.logger.error("角色（Id = " + p.getRoleId() + "）上线失败。 ");
		return true;
	}
}
