package gnet.link;

import java.util.Collection;

import gnet.link.Role;
import gnet.link.Onlines.Handle;

public class RoleLinkBrokenHanle implements Handle {

	@Override
	public void onLinkBroken(Role role, int reason) {
		
		Onlines.logger.info("角色  " + role.getRoleid() + " 意外断开,原因："+ reason);
		new chuhan.gsp.state.PRoleOffline(role.getRoleid(),chuhan.gsp.state.PRoleOffline.TYPE_LINK_BROKEN).submit();

	}

	@Override
	public void onManagerBroken(Collection<Role> roles) {
		// TODO Auto-generated method stub
		for(Role role : roles){
			Onlines.logger.info("角色  " + role.getRoleid() + " 意外断开,原因：link断线");
			new chuhan.gsp.state.PRoleOffline(role.getRoleid(),chuhan.gsp.state.PRoleOffline.TYPE_LINK_BROKEN).submit();
		}
	}

}
