package chuhan.gsp.battle.realtime;

import chuhan.gsp.DataInit;




public class PAddRealTime extends xdb.Procedure{
	private final long roleid;

	
//	public int herokey;
	
	public PAddRealTime(long roleid) {
		this.roleid = roleid;

	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null)
		{
//			ErrorManager.getInstance().SendError(roleid, ErrorType.ERR_NOT_ONLINE);
			return false;
		}
		
		int result = RoomManager.getInstance().RealTimeBattleEntry(roleid);
		if(result == DataInit.ERROR_RESULT)
		{
			xdb.Procedure.psend(roleid, new SAddRealTime(SAddRealTime.END_ERROR,0));
			return false;
		}
		xdb.Procedure.psend(roleid, new SAddRealTime(SAddRealTime.END_OK,0));
		return true;
	}
	
}
