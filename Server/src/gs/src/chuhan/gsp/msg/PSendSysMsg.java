package chuhan.gsp.msg;

public class PSendSysMsg extends xdb.Procedure{
	
	private final long roleId;
	public PSendSysMsg(long roleId) {
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		MsgRole msgrole = MsgRole.getMsgRole(roleId, false);
		msgrole.sendSysMsgs();
		return true;
	}

}
