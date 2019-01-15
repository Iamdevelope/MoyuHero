package chuhan.gsp.battle;




public class PEndBattle extends xdb.Procedure{
	private final long roleid;
	public final int pass; // 0未通过，1通过1，2通过2，3全通
	
//	public int herokey;
	
	public PEndBattle(long roleid, int pass) {
		this.roleid = roleid;
		this.pass = pass;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null)
		{
//			ErrorManager.getInstance().SendError(roleid, ErrorType.ERR_NOT_ONLINE);
			return false;
		}
		
		boolean isEnd = BattleManager.getInstance().EndBattle(roleid, pass);
//		if(isEnd)
		{
			SEndBattle snd = new SEndBattle();
			if(isEnd)
				snd.endtype = SEndBattle.END_OK;
			else
				snd.endtype = SEndBattle.END_ERROR;
			xdb.Procedure.psend(roleid, snd);
		}

		return isEnd;
	}
	
}
