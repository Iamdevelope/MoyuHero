package chuhan.gsp.msg;

public class PRemoveSysMsg extends xdb.Procedure{
	
	private final long roleId;
	private final int verseindex;
	public PRemoveSysMsg(long roleId, int verseindex) {
		this.roleId = roleId;
		this.verseindex = verseindex;
	}
	
	@Override
	protected boolean process() throws Exception {
		MsgRole msgrole = MsgRole.getMsgRole(roleId, false);
		return msgrole.removeSysMsg(verseindex);
	}

}
