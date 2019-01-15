package chuhan.gsp.battle.realtime;





public class PAttack extends xdb.Procedure{
	private final long roleid;
	private final int roomid;
	private final java.util.LinkedList<chuhan.gsp.battle.realtime.BHeroAttack> bherotypelist;
	private final int attackkey;
	private final int iswin;
	
//	public int herokey;
	
	public PAttack(long roleid, int roomid, java.util.LinkedList<chuhan.gsp.battle.realtime.BHeroAttack> bherotypelist,int attackkey,int iswin) {
		this.roleid = roleid;
		this.roomid = roomid;
		this.bherotypelist = bherotypelist;
		this.attackkey = attackkey;
		this.iswin = iswin;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null)
		{
//			ErrorManager.getInstance().SendError(roleid, ErrorType.ERR_NOT_ONLINE);
			return false;
		}
		
		boolean result = RoomManager.getInstance().AttackEntry(roleid,roomid,bherotypelist,iswin,attackkey);
		if(!result)
		{
			xdb.Procedure.psend(roleid, new SAttack(this.attackkey,SAttack.END_ERROR));
			return false;
		}
		
		xdb.Procedure.psendWhileCommit(roleid, new SAttack(this.attackkey,SAttack.END_OK));
		return true;
	}
	
}
