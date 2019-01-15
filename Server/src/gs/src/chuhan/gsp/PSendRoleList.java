package chuhan.gsp;

import java.util.Collections;
import java.util.Comparator;

import chuhan.gsp.util.Conv;

import gnet.link.Onlines;
import gnet.link.User;

public class PSendRoleList extends xdb.Procedure
{
	private final User user;
	public PSendRoleList(User user)
	{
		this.user = user;
	}
	
	@Override
	protected boolean process() throws Exception
	{
		xbean.AUUserInfo xuserinfo = xtable.Auuserinfo.get(user.getUserid());
		if(xuserinfo == null)
		{//没有auuserinfo不让登入
			Onlines.getInstance().getOnlineUsers().addAuuserInfoId(user.getUserid(),user);
			return true;
		}
		final xbean.User u = xtable.User.select(user.getUserid());
		if(u == null || u.getIdlist().isEmpty())
		{
			SRoleList sRoleList = new SRoleList();
			user.send(sRoleList);
			return false;
		}
		SRoleList snd = new SRoleList();
		for(long rid : u.getIdlist())
		{
			final chuhan.gsp.RoleInfo info = new chuhan.gsp.RoleInfo();
			info.roleid = rid;
			final xbean.Properties pro = xtable.Properties.select(info.roleid);
			if (null == pro)
				continue;
			info.rolename = pro.getRolename();
			info.rolelevel = Conv.toShort(pro.getLevel());
			snd.role.add(info);
		}
		Collections.sort(snd.role,new Comparator<chuhan.gsp.RoleInfo>() {
			@Override
			public int compare(RoleInfo arg0, RoleInfo arg1) {
				return arg1.rolelevel - arg0.rolelevel;
			}
		});
		int size = snd.role.size();
		for(int i = 4 ; i < size; i++)
			snd.role.remove(4);//超过4个不显示
		user.send(snd);
		return true;
	}
	
}
