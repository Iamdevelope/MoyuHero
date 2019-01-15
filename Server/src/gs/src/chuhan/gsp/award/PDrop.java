package chuhan.gsp.award;

public class PDrop extends xdb.Procedure{
	private final long roleid;
	private final String dropStr;
	private final String reason;
	
	
	public PDrop(long roleid, String dropStr, String reason) {
		this.roleid = roleid;
		this.dropStr = dropStr;
		this.reason = reason;
	}
	
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null)
		{
//			ErrorManager.getInstance().SendError(roleid, ErrorType.ERR_NOT_ONLINE);
			return false;
		}
		
//		List<Integer> nmDropIdList = ParserString.parseString2Int(dropStr);
		DropManager.getInstance().drop(roleid,dropStr,reason);
				
		return true;
	}
	
}
