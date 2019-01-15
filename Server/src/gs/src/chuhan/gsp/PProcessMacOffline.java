package chuhan.gsp;

public class PProcessMacOffline extends xdb.Procedure{

	private final long roleId;
	public PProcessMacOffline(long roleId) {
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.select(roleId);
		xbean.MacInfo macinfo = xtable.Macinfos.get(xprop.getMac());
		if(macinfo == null)
		{
			macinfo = xbean.Pod.newMacInfo();
			macinfo.setOnlinetime(xprop.getOnlinetime());
			xtable.Macinfos.add(xprop.getMac(), macinfo);
		}
		macinfo.setOfflinetime(xprop.getOfflinetime());
		return true;
	}
}
