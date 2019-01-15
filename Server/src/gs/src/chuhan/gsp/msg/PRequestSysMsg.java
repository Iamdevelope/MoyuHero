package chuhan.gsp.msg;

public class PRequestSysMsg extends xdb.Procedure{
	
	private final long roleId;
	public PRequestSysMsg(long roleId) {
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		MsgRole msgrole = MsgRole.getMsgRole(roleId, false);
		msgrole.sendSysMsgs();
		return true;
	}

}
