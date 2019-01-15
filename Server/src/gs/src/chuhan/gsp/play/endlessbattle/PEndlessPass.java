package chuhan.gsp.play.endlessbattle;

import chuhan.gsp.msg.Message;

public class PEndlessPass extends xdb.Procedure{
	private final long roleid;
	public final java.util.LinkedList<chuhan.gsp.fightInfo> fightinfolist;
	
	public PEndlessPass(long roleid, java.util.LinkedList<chuhan.gsp.fightInfo> fightinfolist) {
		this.roleid = roleid;
		this.fightinfolist = fightinfolist;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
//			return false;
		}
		EndlessinfoColumns endcol = EndlessinfoColumns.getEndLessColumn(roleid, false);
		if(endcol.xcolumn.getIsend() != 1){
			SEndlessPass psend = new SEndlessPass();
			psend.result = SEndlessPass.END_ERROR;
			xdb.Procedure.psend(roleid, psend);
//			Message.psendMsgNotify(roleid, 135);
			return false;
		}
		
		boolean result = endcol.endlessPass(this.fightinfolist);
		if(!result){
			endcol.endlessEnd();
		}
			
		/*
		SBeginEndless snd = new SBeginEndless();

		if(!result){
			//判断如果是继续，则走继续流程，这边不发消息
			if(endcol.xcolumn.getIsend() == 1){
				return false;
			}
			snd.result = SBeginEndless.END_ERROR;
			xdb.Procedure.psendWhileCommit(roleid, snd);
		}
		else{
			snd.result = SBeginEndless.END_OK;
			snd.battleinfo = endcol.getProtocolEndlessInfoBegin();
			xdb.Procedure.psend(roleid, snd);
		}
		
		*/
		return true;
		
	}
	
}
