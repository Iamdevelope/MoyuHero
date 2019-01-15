package chuhan.gsp.gm;


public class Cmd_clearonecharge extends GMCommand {

	@Override
	boolean exec(String[] args) {
		if(args.length < 2){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int activityId = Integer.valueOf(args[0]);
		final long roleId = GMInterface.getTargetRoleId(args[1]);
		try {
			new xdb.Procedure(){
				protected boolean process() throws Exception {
					xbean.ChargeActivityRole chargeActivityRole = xtable.Chargeactivities.get(roleId);
					if(null != chargeActivityRole) {
						chargeActivityRole.getActivities().remove(activityId);
					}
					
					return true;
				}
			}.submit().get();
			sendToGM("执行成功!");
		} catch(Exception e) {
			e.printStackTrace();
		}
		
		return true;
	}

	@Override
	String usage() {
		return "//clrcharge activityId roleId";
	}

}
