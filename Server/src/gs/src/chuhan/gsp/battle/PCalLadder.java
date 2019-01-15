package chuhan.gsp.battle;

import java.util.Iterator;
import java.util.Map.Entry;

import chuhan.gsp.award.AwardManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.MsgRole;

import xbean.FirstLadderInfo;
import xbean.FirstLadderInfoRole;

/**
 * 计算天梯排行第一名在位时间最长的奖励
 *
 */
public class PCalLadder extends xdb.Procedure {
	public static final int ONE_MINUT = 60 * 1000;
	/**
	 * 注意：锁顺序：FIRSTLADDERINFO->PVPLADDER->roleId
	 */
	@Override
	protected boolean process() throws Exception {
		try {
			FirstLadderInfoRole firstLadderInfoRole = LadderRole.getFirstLadderInfoRole(false);//获得锁Locks.FIRSTLADDERINFO
			
			//重新计算当前第一名的在位时间
			xbean.LadderInfo curfirst = xtable.Pvpladder.select(1);//找到第一名的信息 select也会加锁
			if(null != curfirst) {
				xbean.FirstLadderInfo curfirstLadderInfo = firstLadderInfoRole.getRoleinfos().get(curfirst.getRoleid());
				if(null != curfirstLadderInfo) {
					int thisMilSec = (int) (GameTime.currentTimeMillis() - curfirstLadderInfo.getStarttime());
					curfirstLadderInfo.setZaiweimilsec(curfirstLadderInfo.getZaiweimilsec() + thisMilSec);
				}
			}
			
			Iterator<Entry<Long, FirstLadderInfo>> entries = 
					firstLadderInfoRole.getRoleinfos().entrySet().iterator();
			long maxTimeRoleId = -1;
			int maxTime = -1;
			while(entries.hasNext()) {//找出在位时间最长的玩家
				Entry<Long, FirstLadderInfo> entry = entries.next();
				int zaiWeiMinut = entry.getValue().getZaiweimilsec() / ONE_MINUT;//折算成分钟
				if(zaiWeiMinut > maxTime) {
					maxTime = zaiWeiMinut;
					maxTimeRoleId = entry.getKey();
				} else if(maxTimeRoleId > 0 && zaiWeiMinut == maxTime) {//两个人在位时间一样时给当前排名高的发奖
					if(LadderRole.getLadderRole(entry.getKey(), true).getMyRank() 
							< LadderRole.getLadderRole(maxTimeRoleId, true).getMyRank()) {
						maxTimeRoleId = entry.getKey();
					}
				}
			}
			
			//发奖励
			if(maxTimeRoleId > 0) {
				//发奖
				AwardManager.getInstance().distributeAllAward(maxTimeRoleId, 101540, null, true);
				MsgRole msgRole = MsgRole.getMsgRole(maxTimeRoleId, false);
				msgRole.addSysMsgWithSP(293, null, null, 0, MsgRole.MST_TYPE_SYS);
				Iterator<Entry<Long, FirstLadderInfo>> entries2 = 
						firstLadderInfoRole.getRoleinfos().entrySet().iterator();
				while(entries2.hasNext()) {
					Entry<Long, FirstLadderInfo> entry = entries2.next();
					if(entry.getKey() != maxTimeRoleId && entry.getValue().getZaiweimilsec() != 0) {
						pexecuteWhileCommit(new PFirstLadderAward(entry.getKey()));
					}
				}
			}
			
			firstLadderInfoRole.getRoleinfos().clear();//清空本周数据
			//重新设置当前第一名的在位开始时间
			pexecuteWhileCommit(new PCalLadderInit());
			
		} catch(Exception e) {
			e.printStackTrace();
		}
		
		return true;
	}
}
