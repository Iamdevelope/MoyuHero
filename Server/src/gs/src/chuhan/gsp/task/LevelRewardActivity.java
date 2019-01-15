package chuhan.gsp.task;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.AwardManager;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.Conv;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.OctetsStream;

public class LevelRewardActivity extends AbstractActivity{
	
	private PropRole prole;
	
	public LevelRewardActivity(long roleId, SActivityConfig config, boolean readonly) {
		super(roleId, config, readonly);
		prole = PropRole.getPropRole(roleId, readonly);
	}

	@Override
	public Octets getDataOctets() {
		ShengjiActivity data = new ShengjiActivity();
//		data.lastgetrewardlevel = Conv.toShort(prole.getProperties().getLevelaward());
		return data.marshal(new OctetsStream());
	}
	
	@Override
	public int getType() {
		return ActivityRole.ACTIVITY_LEVEL_REWARD;
	}
	
	
	public boolean getReward()
	{
		int nextcfgid = 0;//prole.getProperties().getLevelaward()+10;
		if(nextcfgid > prole.getLevel())
			return false;
		
		sqiandaoconfig cfg = ConfigManager.getInstance().getConf(sqiandaoconfig.class).get(nextcfgid);
		
		AwardManager.getInstance().distributeAllAward(roleId, cfg.reward, null, true);	
//		prole.getProperties().setLevelaward(nextcfgid);
		
		notifyRefresh();
		return true;
	}
}
