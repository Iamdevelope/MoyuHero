package chuhan.gsp.msg;

import java.util.List;

public class PAddSysMsg extends xdb.Procedure{
	
	private final long roleId;
	private final int msgId;
	private final List<String> params;
	private final String text;
	public PAddSysMsg(long roleId, int msgId, List<String> params, String text) {
		this.roleId = roleId;
		this.msgId = msgId;
		this.params = params;
		this.text = text;
	}
	
	@Override
	protected boolean process() throws Exception {
		MsgRole msgrole = MsgRole.getMsgRole(roleId, false);
		return msgrole.addSysMsgWithSP(msgId, params, text, 0, MsgRole.MST_TYPE_SYS);
	}

}
