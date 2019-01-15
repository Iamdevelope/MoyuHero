package chuhan.gsp.task;

import java.util.Collections;
import java.util.Comparator;
import java.util.Map;

import xdb.Transaction;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.Conv;

public class ActivityRole {
	
	
	public static final int ACTIVITY_FEED = 1;
	public static final int ACTIVITY_DAILY_SIGN = 2;
	public static final int ACTIVITY_LEVEL_REWARD = 3;
	public static final int ACTIVITY_WEEK_DAY = 4;
	public static final int ACTIVITY_CHARGE = 5;//充值活动
	public static final int ACTIVITY_REBATE_CHARGE = 6;//充值返利活动
	public static final int ACTIVITY_FIRST_CHARGE_FEED = 7; //首次充值累计返利
	
	public static ActivityRole getActivityRole(long roleId, boolean readonly)
	{
		if(xtable.Properties.select(roleId) == null)
			return null;
		
		return new ActivityRole(roleId,readonly);
	}
	
	 
	
	private final long roleId;
	private final boolean readonly;
	private ActivityRole(long roleId, boolean readonly) {
		this.roleId = roleId;
		this.readonly = readonly;
	}

	public void processWhileOnline()
	{
		chuhan.gsp.attr.PropRole proprole = chuhan.gsp.attr.PropRole.getPropRole(roleId,false);
//		if(proprole.getProperties().getAlreadytips().isEmpty())
//			return;
		DailySignActivity act = (DailySignActivity)getActivity(ACTIVITY_DAILY_SIGN);
		if(act == null)
			return;
		if(!act.getTodayReward(GameTime.currentTimeMillis()))
		{
			//今日没领
			sendActivities(ACTIVITY_DAILY_SIGN);
		}
	}
	
	public void sendActivities()
	{
		sendActivities(0);
	}
	
	public void sendActivities(int showkey)
	{
		SSendActivities snd = new SSendActivities();
		snd.showactivityid = Conv.toByte(showkey);
		Map<Integer, SActivityConfig> cfgs = ConfigManager.getInstance().getConf(SActivityConfig.class);
		for(SActivityConfig cfg : cfgs.values())
		{
			AbstractActivity activity = createActivity(cfg);
			if(activity == null)
				continue;
			if(!activity.isActive())
				continue;
			snd.activities.add(activity.getActivityInfo());
		}
		
		Collections.sort(snd.activities, new Comparator<ActivityInfo>() {
			@Override
			public int compare(ActivityInfo arg0, ActivityInfo arg1) {
				return arg0.id - arg1.id;
			}
		});
		if(Transaction.current() == null)
			gnet.link.Onlines.getInstance().send(roleId, snd);
		else
			xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	private AbstractActivity createActivity(SActivityConfig cfg)
	{
		if(cfg.getType() == ACTIVITY_FEED)
			return new FeedActivity(roleId, cfg, readonly);
		else if(cfg.getType() == ACTIVITY_DAILY_SIGN)
			return new DailySignActivity(roleId, cfg, readonly);
		else if(cfg.getType() == ACTIVITY_LEVEL_REWARD)
			return new LevelRewardActivity(roleId, cfg, readonly);
		else if(cfg.getType() == ACTIVITY_WEEK_DAY)
			return new WeekdayActivity(roleId, cfg, readonly);
		else if(cfg.getType() == ACTIVITY_CHARGE) {
			return ChargeActivity.getChargeActivity(roleId, cfg, readonly);
		}
		
		return null;
	}
	
	public AbstractActivity getActivity(int activityid)
	{
		SActivityConfig cfg = ConfigManager.getInstance().getConf(SActivityConfig.class).get(activityid);
		if(cfg == null)
			return null;
		
		return createActivity(cfg);
			
	}
}
