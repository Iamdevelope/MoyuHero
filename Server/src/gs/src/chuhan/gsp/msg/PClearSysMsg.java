package chuhan.gsp.msg;

public class PClearSysMsg extends xdb.Procedure {
	private final long roleId;
	
	public PClearSysMsg(long roleId) {
		this.roleId = roleId;
	}

	@Override
	protected boolean process() throws Exception {
		MsgRole msgrole = MsgRole.getMsgRole(roleId, false);
		return msgrole.clearSysMsg();
	}
}
