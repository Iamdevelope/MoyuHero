package chuhan.gsp;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.AwardManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.util.DateUtil;

public class PProcessMacOnline extends xdb.Procedure{

	private final long roleId;
	private final String mac;
	public PProcessMacOnline(long roleId, String mac) {
		this.roleId = roleId;
		this.mac = mac;
	}
	
	@Override
	protected boolean process() throws Exception {
		long now = GameTime.currentTimeMillis();
		xbean.MacInfo macinfo = xtable.Macinfos.get(mac);
		if(macinfo == null)
		{
			macinfo = xbean.Pod.newMacInfo();
			xtable.Macinfos.add(mac, macinfo);
		}
		macinfo.setOnlinetime(now);
		PropRole prole = PropRole.getPropRole(roleId, false);
		prole.getProperties().setMac(mac);
		if(prole.getProperties().getOfflinetime() <= 0)
			return true;
		int ostype = PlatformTypeStr.getOsType(prole.getProperties().getPlattypestr());
		prole.getProperties().setOstype(ostype);
		long lastoffline = Math.max(prole.getProperties().getOfflinetime(), macinfo.getOfflinetime());
		int lastday = DateUtil.getCurrentDay(lastoffline);
		int curday = DateUtil.getCurrentDay(now);
		boolean passmidday = (now - DateUtil.getDayFirstSecond(now))>=(DateUtil.dayMills/2);
		int dayinterval = curday - lastday;
		
		/* yanglk 登录奖励，注释掉
		if(dayinterval < 3)
			return true;
		MsgRole msgrole = MsgRole.getMsgRole(roleId, false);
		if(dayinterval > 15 || (dayinterval == 15 && passmidday) )
		{
			AwardManager.getInstance().distributeAllAward(roleId, 100698, null, false);
			msgrole.addSysMsg(174, null, null, 0, MsgRole.MST_TYPE_SYS);
			return true;
		}
		if(dayinterval > 6 || (dayinterval == 6 && passmidday) )
		{
			AwardManager.getInstance().distributeAllAward(roleId, 100697, null, false);
			msgrole.addSysMsg(173, null, null, 0, MsgRole.MST_TYPE_SYS);
			return true;
		}
		if(dayinterval > 3 || (dayinterval == 3 && passmidday) )
		{
			AwardManager.getInstance().distributeAllAward(roleId, 100696, null, false);
			msgrole.addSysMsg(172, null, null, 0, MsgRole.MST_TYPE_SYS);
			return true;
		}
		*/
		return true;
	}
}
