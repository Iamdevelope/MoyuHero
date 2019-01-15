package chuhan.gsp.util;

public abstract class AccountUtil {

	
	/*public static long getRoleIdByUsername(String username)
	{
		Integer userid = xtable.Username2id.select(username);
		if(userid == null)
			return -1;
		xbean.User user = xtable.User.select(userid);
		if(user == null)
			return -1;
		if(user.getIdlist().isEmpty())
			return -1;
		Long roleId = user.getIdlist().get(0);
		if(roleId == null)
			return -1;
		return roleId;
	}*/
	
	public static String getUserNameByRoleId(long roleId)
	{
		Integer userid = xtable.Properties.selectUserid(roleId);
		if(userid == null)
			return null;
		String username = xtable.User.selectUsername(userid);
		if(username == null || username.isEmpty())
			return null;
		return username;
	}
	
	
	
}
