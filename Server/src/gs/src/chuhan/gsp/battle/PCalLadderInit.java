package chuhan.gsp.battle;

import chuhan.gsp.main.GameTime;
import xbean.FirstLadderInfoRole;

/**
 * 初始化天梯排行第一在位时间数据
 *
 */
public class PCalLadderInit extends xdb.Procedure {
	@Override
	protected boolean process() throws Exception {
		FirstLadderInfoRole firstLadderInfoRole = LadderRole.getFirstLadderInfoRole(false);
		if(firstLadderInfoRole.getRoleinfos().size() == 0) {
			xbean.LadderInfo curfirst = xtable.Pvpladder.select(1);//找到第一名的信息
			if(null != curfirst) {
				xbean.FirstLadderInfo firstLadderInfo = xbean.Pod.newFirstLadderInfo();
				firstLadderInfo.setStarttime(GameTime.currentTimeMillis());
				firstLadderInfo.setZaiweimilsec(0);
				firstLadderInfoRole.getRoleinfos().put(curfirst.getRoleid(), firstLadderInfo);
			}
		}
		
		return true;
	}
}
