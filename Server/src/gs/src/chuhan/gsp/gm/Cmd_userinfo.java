package chuhan.gsp.gm;

public class Cmd_userinfo extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		int userid = Integer.valueOf(args[0]);
		
		String username = xtable.Auuserinfo.selectUsername(userid);
		
		if(username == null)
		{
			sendToGM("没有该账号："+userid);
			return false;
		}
		
		StringBuilder sb = new StringBuilder("账号："+username+"，账号ID："+userid+"，");
		
		xbean.User xuser = xtable.User.select(userid);
		if(xuser.getIdlist().isEmpty())
		{
			sendToGM(sb.toString());
			return false;
		}
		
		long roleid = xuser.getIdlist().get(0);
		
		xbean.Properties xprop = xtable.Properties.select(roleid);
		
		sb.append("角色：").append(xprop.getRolename()).append("，ID：").append(roleid).append("，等级：")
		.append(xprop.getLevel()).append("，元宝：").append(xprop.getYuanbao());
		
		sendToGM(sb.toString());
		return true;
	}

	@Override
	String usage() {
		return "//userinfo [userid]";
	}

}