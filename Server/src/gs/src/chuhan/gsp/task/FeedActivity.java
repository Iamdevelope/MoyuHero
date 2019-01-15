package chuhan.gsp.task;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.DateUtil;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.OctetsStream;

public class FeedActivity extends AbstractActivity{
	
//	private xbean.SupplyActRole xrole;
	
	

	public FeedActivity(long roleId, SActivityConfig config, boolean readonly) {
		super(roleId, config, readonly);
		// TODO Auto-generated constructor stub
	}

	@Override
	public Octets getDataOctets() {
		LiangCaoActivity data = new LiangCaoActivity();
//		data.firstuse = xrole.getFirstsupplyed()? (byte)1 : (byte)0;
//		data.senconduse = xrole.getSecondsupplyed()? (byte)1 : (byte)0;
		return data.marshal(new OctetsStream());
	}
	
	@Override
	public int getType() {
		return ActivityRole.ACTIVITY_FEED;
	}

	public boolean supply()
	{
		long now = GameTime.currentTimeMillis();
		long daytime = now - DateUtil.getDayFirstSecond(now);
		int weekday = DateUtil.getCurrentWeekDay();
		int addti = (weekday == 6 || weekday == 7)? 15 : 10;
		if(daytime >= 12*3600000 && daytime <= 13*3600000)
		{
//			if(xrole.getFirstsupplyed())
				return false;
//			PropRole.getPropRole(roleId, false).addTili(addti);
//			xrole.setFirstsupplyed(true);
//			xrole.setLastsupplytime(now);
		}
		else if(daytime > 13*3600000 && daytime < 20*3600000)
		{
//			if(xrole.getFirstsupplyed())
//				return false;
			addti = (addti * 6)/10;
			PropRole.getPropRole(roleId, false).addTili(addti);
//			xrole.setFirstsupplyed(true);
//			xrole.setLastsupplytime(now);
		}
		else if(daytime >= 20*3600000 && daytime <= 21*3600000)
		{
//			if(xrole.getSecondsupplyed())
//				return false;
			PropRole.getPropRole(roleId, false).addTili(addti);
//			xrole.setSecondsupplyed(true);
//			xrole.setLastsupplytime(now);
		}
		else if(daytime > 21*3600000 && daytime < 24*3600000)
		{
//			if(xrole.getSecondsupplyed())
//				return false;
			addti = (addti * 6)/10;
			PropRole.getPropRole(roleId, false).addTili(addti);
//			xrole.setSecondsupplyed(true);
//			xrole.setLastsupplytime(now);
		}
		else
			return false;
		Message.psendMsgNotifyWhileCommit(roleId, 92, addti);
		notifyRefresh();
		return true;
	}
	
	public void processData(long now)
	{
/*		if(xrole.getFirstsupplyed() || xrole.getSecondsupplyed())
		{
			if(!DateUtil.inTheSameDay(xrole.getLastsupplytime(), now))
			{
				xrole.setSecondsupplyed(false);
				xrole.setFirstsupplyed(false);
				xrole.setLastsupplytime(0);
			}
		}*/
	}
}
