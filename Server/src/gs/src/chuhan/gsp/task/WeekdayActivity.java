package chuhan.gsp.task;

import chuhan.gsp.main.ConfigManager;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.OctetsStream;

public class WeekdayActivity extends AbstractActivity{
	
	public WeekdayActivity(long roleId, SActivityConfig config, boolean readonly) {
		super(roleId, config, readonly);
	}

	@Override
	public Octets getDataOctets() {
		return new OctetsStream();
	}
	
	@Override
	public int getType() {
		return ActivityRole.ACTIVITY_WEEK_DAY;
	}
	public boolean isActive()
	{
		if(ConfigManager.inReviewState())
			return false;
		return true;
	}
}
