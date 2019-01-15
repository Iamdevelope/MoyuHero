package chuhan.gsp.task;

import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;

import com.goldhuman.Common.Octets;

public abstract class AbstractActivity
{
	public final long roleId;
	public final boolean readonly;
	public final SActivityConfig config;
	public final int activityId;
	
	
	
	public AbstractActivity(long roleId, SActivityConfig config, boolean readonly) {
		this.roleId = roleId;
		this.activityId = config.getId();
		this.readonly = readonly;
		this.config =  config;
	}

	public int getType()
	{
		return config.getType();
	}
	
	public abstract Octets getDataOctets();
	
	public boolean isActive(SActivityConfig cfg)
	{
		return true;
	}
	
	public ActivityInfo getActivityInfo() {
		ActivityInfo actinfo = new ActivityInfo();
		actinfo.id = Conv.toByte(activityId);
		actinfo.data = getDataOctets();
		return actinfo;
	}
	public void notifyRefresh()
	{
		SRefreshActivities snd = new SRefreshActivities(getActivityInfo());
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	public boolean isActive()
	{
		return DateUtil.isRunning(config.getStart(), config.getEnd());
	}
}
