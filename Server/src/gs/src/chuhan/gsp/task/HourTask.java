package chuhan.gsp.task;

import java.util.TimerTask;
import chuhan.gsp.play.ranking.endlessRanking;

/**
 * 每整点执行的任务
 *
 */
public class HourTask extends TimerTask {

	@Override
	public void run() {
		try {
//			CampRole.genCampInfo();
			endlessRanking.getInstance().endlessRankTime(false);
			
		} catch(Exception e) {
			e.printStackTrace();
		}

	}

}
