package chuhan.gsp.award;

import java.util.Collection;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.util.AccountUtil;

public class CompenseRole {

	
	public static CompenseRole getCompenseRole(long roleId, boolean readonly)
	{
		/*xbean.CompenseRole xrole = null;
		if(readonly)
			xrole = xtable.Compenseroles.select(roleId);
		else
			xrole = xtable.Compenseroles.get(roleId);
		if(xrole == null)
		{
			if(readonly)
				xrole = xbean.Pod.newCompenseRoleData();
			else
			{
				xrole = xbean.Pod.newCompenseRole();
				xtable.Compenseroles.insert(roleId, xrole);
			}
		}*/
		return new CompenseRole(roleId, readonly);
	}
	
	
	public final long roleId;
//	public final xbean.CompenseRole xrole;
	public final boolean readonly;
	private CompenseRole(long roleId, boolean readonly) {
		this.roleId = roleId;
		this.readonly = readonly;
	}
	
	public void processWhileOnline()
	{
		giveCompenses(CompenseManager.getInstance().getCompenseConfigs().values(), false);
	}
	
	public void giveCompenses(Collection<CompenseConfig> compenses, boolean isnotify)
	{
		long now = GameTime.currentTimeMillis();
		String username = AccountUtil.getUserNameByRoleId(roleId);
		//if(username == null)
		//	return;
		xbean.Properties xprop = xtable.Properties.get(roleId);
		int lv = xprop.getLevel();
		int vip = xprop.getViplv();
		MsgRole msgrole = MsgRole.getMsgRole(roleId, false);
		for(CompenseConfig cfg : compenses)
		{
			if(!cfg.enable)
				continue;
			if(!cfg.plattypes.isEmpty() && !cfg.plattypes.contains(xprop.getPlattypestr()))
				continue;
//			if(xrole.getFetchedcompenses().contains(cfg.id))
//				continue;
			if(now < cfg.starttime || now > cfg.endtime)
				continue;
			if(!cfg.roleids.isEmpty() && !cfg.roleids.contains(roleId))
				continue;
			if(!cfg.accounts.isEmpty() && username != null && !username.isEmpty() && !cfg.accounts.contains(username))
				continue;
			if(!cfg.serverids.isEmpty() && !cfg.serverids.contains(ConfigManager.getGsZoneId()))
				continue;
			if(lv < cfg.minlevel)
				continue;
			if(cfg.maxlevel > 0 && lv > cfg.maxlevel)
				continue;
			if(vip < cfg.minviplv)
				continue;
			if(cfg.maxviplv > 0 && vip > cfg.maxviplv)
				continue;
			
			//合法,可以发放了
			if(cfg.awardid > 0)
				AwardManager.getInstance().distributeAllAward(roleId, cfg.awardid, null, false);
//			xrole.getFetchedcompenses().add(cfg.id);
			msgrole.addSysMsg(cfg.msgid, null, cfg.msgcontent, 0, MsgRole.MST_TYPE_SYS);
		}
		if(isnotify)
			msgrole.notifySysNewNum();
	}
}
