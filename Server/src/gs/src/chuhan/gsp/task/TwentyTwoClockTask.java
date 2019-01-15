package chuhan.gsp.task;

import java.util.Calendar;
import java.util.HashMap;
import java.util.Map;
import java.util.TimerTask;


import chuhan.gsp.award.AwardManager;
import chuhan.gsp.battle.BloodRole;
import chuhan.gsp.msg.Message;
import chuhan.gsp.msg.MsgRole;

public class TwentyTwoClockTask extends TimerTask{

	@Override
	public void run() {
//		giveLadderReward();
//		CampRole.dayTask();
//	BloodRole.sendRankAward();
	}
	
	
	/*void giveLadderReward()
	{
		try
		{
			Calendar c = Calendar.getInstance();
			if (c.get(Calendar.YEAR) != 2013)
				return;
			if (c.get(Calendar.MONTH) != Calendar.JUNE)
				return;
			int dayofmonth = c.get(Calendar.DAY_OF_MONTH);
			if (dayofmonth < 1 || dayofmonth > 12)
				return;
			
			final Map<Integer,Long> roleids = new HashMap<Integer,Long>();
			for(int i = 1; i <= 200; i++)
			{
				if(xtable.Pvpladder.select(i) == null)
					continue;
				final int rank = i;
				new xdb.Procedure() {
					protected boolean process() throws Exception {
						xbean.LadderInfo ladder = xtable.Pvpladder.get(rank);
						if (ladder == null)
							return true;
						int awardId = 0;
						String msg = null;
						if(rank == 1)
						{
							awardId = 101427;
							msg = Message.getMessage(12)+rank+Message.getMessage(13);
						}
						else if(rank == 2)
						{
							awardId = 101428;
							msg = Message.getMessage(12)+rank+Message.getMessage(14);
						}
						else if(rank == 3)
						{
							awardId = 101429;
							msg = Message.getMessage(12)+rank+Message.getMessage(15);
						}
						else if(rank >= 4 && rank <= 10)
						{
							awardId = 101430;
							msg = Message.getMessage(12)+rank+Message.getMessage(16);
						}
						else if(rank >= 11 && rank <= 20)
						{
							awardId = 101431;
							msg = Message.getMessage(12)+rank+Message.getMessage(17);
						}
						else if(rank >= 21 && rank <= 50)
						{
							awardId = 101432;
							msg = Message.getMessage(12)+rank+Message.getMessage(18);
						}
						else if(rank >= 51 && rank <= 100)
						{
							awardId = 101433;
							msg = Message.getMessage(12)+rank+Message.getMessage(19);
						}
						else if(rank >= 101 && rank <= 200)
						{
							awardId = 101434;
							msg = Message.getMessage(12)+rank+Message.getMessage(20);
						}
						if(awardId == 0 || msg == null)
							return true;
						AwardManager.getInstance().distributeAllAward(ladder.getRoleid(), awardId, null, false);
						MsgRole msgrole = MsgRole.getMsgRole(ladder.getRoleid(), false);
						msgrole.addSysMsgWithSP(0, null, msg, 0, MsgRole.MST_TYPE_SYS);
						roleids.put(rank,ladder.getRoleid());
						return true;
					};
				}.submit().get();
			}
			xdb.Trace.info("天梯发奖："+roleids);
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
	*/
	
}
