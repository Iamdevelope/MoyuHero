package chuhan.gsp.play.endlessbattle;

public class PBuyPact extends xdb.Procedure{
	private final long roleid;
	public final int pactid; // 强者之约ID
	
	public PBuyPact(long roleid, int pactid) {
		this.roleid = roleid;
		this.pactid = pactid;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
//			return false;
		}
		EndlessinfoColumns endcol = EndlessinfoColumns.getEndLessColumn(roleid, false);
		
		boolean result = endcol.buyPact(this.pactid);

		SBuyPact snd = new SBuyPact();

		if(!result){
			snd.result = SBuyPact.END_ERROR;
			xdb.Procedure.psend(roleid, snd);
		}
		else{
			snd.result = SBuyPact.END_OK;
			snd.pactid = this.pactid;
			xdb.Procedure.psend(roleid, snd);
		}
		
		
		return result;
	}
	
}
