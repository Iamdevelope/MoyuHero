package chuhan.gsp.battle;

import java.util.LinkedList;
import java.util.List;

import org.apache.log4j.Logger;

import chuhan.gsp.award.AwardManager;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.task.sxuezhanpaiming;
import xdb.Procedure;

public class PBloodRankAward extends Procedure {
	private static final Logger logger = Logger.getLogger(PBloodRankAward.class);
	private final long roleId;
	private final int rank;
	
	public PBloodRankAward(long roleId, int rank) {
		this.roleId = roleId;
		this.rank = rank;
	}
	
	@Override
	protected boolean process() {
		sxuezhanpaiming conf = ConfigManager.getInstance().getConf(sxuezhanpaiming.class).get(rank);
		if(null == conf) {
			return false;
		}
		AwardManager.getInstance().distributeAllAward(roleId, conf.reward, null, false);
		MsgRole msgRole = MsgRole.getMsgRole(roleId, false);
		List<String> strs = new LinkedList<String>();
		strs.add(String.valueOf(rank));
		strs.add(String.valueOf(conf.describe));
		msgRole.addSysMsgWithSP(387, strs, null, 0, MsgRole.MST_TYPE_SYS);
		logger.info("血战排行奖励：rank = " + rank + ",roleId = " + roleId);
		return true;
	}
}
