package chuhan.gsp.battle;

import chuhan.gsp.award.AwardManager;
import chuhan.gsp.msg.MsgRole;

public class PFirstLadderAward extends xdb.Procedure {
	private final long roleId;
	public PFirstLadderAward(long roleId) {
		this.roleId = roleId;
	}
	@Override
	protected boolean process() throws Exception {
		AwardManager.getInstance().distributeAllAward(roleId, 101541, null, true);
		MsgRole msgRole = MsgRole.getMsgRole(roleId, false);
		msgRole.addSysMsgWithSP(395, null, null, 0, MsgRole.MST_TYPE_SYS);
		return true;
	}
}
