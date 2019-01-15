package chuhan.gsp.main;

import java.util.Calendar;

public class GsInfo implements GsInfoMXBean {
	private long gsStartTime;
	void onServerStart() {
		gsStartTime = Calendar.getInstance().getTimeInMillis();
	}
	
	@Override
	public long getStartTime() {
		return gsStartTime;
	}

}
