package chuhan.gsp.exchange;

import chuhan.gsp.SQihoo360ExtraInfo;

import com.goldhuman.Common.Marshal.MarshalException;
import com.goldhuman.Common.Marshal.OctetsStream;

public class PReceiveDataFromAuany extends xdb.Procedure
{
	
//	private DataBetweenAuAnyAndGS protocol;
//
//	public PReceiveDataFromAuany(DataBetweenAuAnyAndGS protocol) {
//		this.protocol = protocol;
//	}
//	
//	@Override
//	protected boolean process() throws Exception {
//		
//		if(protocol.qtype == DataBetweenAuAnyAndGS.REFRESH_QIHOO_ACCESS_TOKEN)
//		{
//			refreshQihooAccessToken();
//		}
//		
//		return true;
//	}
//	
//	
//	private void refreshQihooAccessToken() throws MarshalException
//	{
//		xbean.User xuser = xtable.User.select(protocol.userid);
//		if(xuser == null || xuser.getIdlist().isEmpty())
//			return;
//		
//		long roleid = xuser.getIdlist().get(0);
//		OctetsStream stream = new OctetsStream(protocol.info);
//		String str = stream.unmarshal_String("UTF-8");
//		String[] strs = str.split("#");
//		SQihoo360ExtraInfo snd = new SQihoo360ExtraInfo(strs[0], strs[1], strs[2]);
//		gnet.link.Onlines.getInstance().send(roleid, snd);
//		
//	}
}
