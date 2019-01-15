package chuhan.gsp.play.endlessbattle;

public class PBeginEndless extends xdb.Procedure{
	private final long roleid;
	public final short troopid; // 战队ID
	
	public PBeginEndless(long roleid, short troopid) {
		this.roleid = roleid;
		this.troopid = troopid;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
//			return false;
		}
		EndlessinfoColumns endcol = EndlessinfoColumns.getEndLessColumn(roleid, false);
		
		boolean result = endcol.beginEndLess(this.troopid);

		SBeginEndless snd = new SBeginEndless();

		if(!result){
			//判断如果是继续，则走继续流程，这边不发消息
			if(endcol.xcolumn.getIsend() == 1){
				return false;
			}
			snd.result = SBeginEndless.END_ERROR;
			xdb.Procedure.psend(roleid, snd);
		}
		else{
			snd.result = SBeginEndless.END_OK;
			snd.battleinfo = endcol.getProtocolEndlessInfoBegin();
			snd.attrinfo = endcol.getProtocolEndlessInfoAttr();
			xdb.Procedure.psend(roleid, snd);
		}
		
		
		return result;
	}
	
}
