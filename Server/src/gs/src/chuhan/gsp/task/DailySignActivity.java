package chuhan.gsp.task;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.OctetsStream;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.AwardManager;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;

public class DailySignActivity extends AbstractActivity{
	
	private PropRole prole;
	
	public DailySignActivity(long roleId, SActivityConfig config, boolean readonly) {
		super(roleId, config, readonly);
		prole = PropRole.getPropRole(roleId, readonly);
	}

	@Override
	public Octets getDataOctets() {
		QiandaoActivity data = new QiandaoActivity();
//		data.qiandaodays = Conv.toByte(Math.min(3, prole.getContinueLoginDays()));
		long now = GameTime.currentTimeMillis();
		data.getreward = getTodayReward(now)? (byte)1 : (byte)0;
		return data.marshal(new OctetsStream());
	}
	
	@Override
	public int getType() {
		return ActivityRole.ACTIVITY_DAILY_SIGN;
	}
	
	public boolean getTodayReward(long now)
	{
		return true;
//		return DateUtil.inTheSameDay(now, prole.getProperties().getLastloginawardtime());
	}
	
	public boolean getReward()
	{
		long now = GameTime.currentTimeMillis();
		if(getTodayReward(now))
			return false;
		
		int id = 0;//Math.min(prole.getContinueLoginDays(), 3);
		
		sqiandaoconfig cfg = ConfigManager.getInstance().getConf(sqiandaoconfig.class).get(id);
		
		AwardManager.getInstance().distributeAllAward(roleId, cfg.reward, null, true);	
		
//		prole.getProperties().setLastloginawardtime(now);
		
		notifyRefresh();
		return true;
	}
}
