package chuhan.gsp.mbean.beans;

import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import chuhan.gsp.mbean.AbstractRequestHandler;

public class MoveRole extends AbstractRequestHandler {

	public MoveRole(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String useridStr = (String) paras.get("userid");
			if(null != roleidStr && null != useridStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer userid = Integer.valueOf(useridStr);
				if(null == xtable.User.select(userid)) {
					return failedMsg("该帐号未在该服务器登录过");
				}
				xbean.Properties properties =xtable.Properties.select(roleid); 
				if (null == properties){
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				if(properties.getUserid() == userid.intValue()) {
					return failedMsg("不能将自己的角色转移到该角色已在的帐号上");
				}
				new xdb.Procedure() {
					protected boolean process() throws Exception {
						final xbean.User user = xtable.User.get(userid);
						final xbean.Properties targetprop = xtable.Properties.get(roleid);
						clearInvalidRoleIds(user, userid);
						int olduserid = targetprop.getUserid();
						if(!user.getIdlist().contains(roleid)) {
							user.getIdlist().add(0,roleid);
						}
						targetprop.setUserid(userid);
						
						xbean.User olduser = xtable.User.get(olduserid);
						if(olduser != null)
							olduser.getIdlist().remove((Long)roleid);
						clearInvalidRoleIds(olduser, olduserid);
						return true;
					}
				}.submit().get();
				return successMsg();
			} else {
				return failedMsg("需要参数roleid userid");
			}
		} catch (Exception e) {
			return failedMsg("执行出错");
		}
	}
	
	private void clearInvalidRoleIds(xbean.User user, int userId) {
		Set<Long> alreadyhaveids = new HashSet<Long>();
		for(Iterator<Long> it = user.getIdlist().iterator();it.hasNext();) {
			Long roleid = it.next();
			final xbean.Properties pro = xtable.Properties.select(roleid);
			if(null == pro) {
				it.remove();
				continue;
			}
			if(pro.getUserid() != userId) {
				it.remove();
				continue;
			}
			if(alreadyhaveids.contains(roleid)) {
				it.remove();
				continue;
			}
			alreadyhaveids.add(roleid);
		}
	}

}
