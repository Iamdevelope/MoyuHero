package chuhan.gsp.exchange;

import java.util.HashMap;
import java.util.Map;

import chuhan.gsp.PlatformTypeStr;
import chuhan.gsp.main.ConfigManager;

import com.goldhuman.Common.Marshal.MarshalException;
import com.goldhuman.Common.Marshal.OctetsStream;

import gnet.QueryOrderResponse;

public abstract class PQueryOrderResponse extends xdb.Procedure{
	protected QueryOrderResponse protocol;

	public static PQueryOrderResponse getPQueryOrderResponse(QueryOrderResponse protocol)
	{
		if(protocol.platid.equals(PlatformTypeStr.Tieren25pp))
			return new PQueryOrderResponsePP(protocol);
		else if(PlatformTypeStr.isTieren26pp(protocol.platid))
			return new PQueryOrderResponsePP(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.Wanglong91))
			return new PQueryOrderResponse91(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.AppStore)
				|| protocol.platid.equals(PlatformTypeStr.LaHu)
				|| protocol.platid.equals(PlatformTypeStr.AppT))
			return new PQueryOrderResponseAppStore(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.Tongbutui))
			return new PQueryOrderResponseTBT(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.YoushiUC))
			return new PQueryOrderResponseUC(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.Xiaomi))
			return new PQueryOrderResponseXiaoMi(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.BaiduDuoku))
			return new PQueryOrderResponseDuoKu(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.Downjoy))
			return new PQueryOrderResponseDangLe(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.Qihoo360))
			return new PQueryOrderResponseQihoo360(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.WanglongAD91))
			return new PQueryOrderResponse91(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.Oppo))
			return new PQueryOrderResponseOppo(protocol);
		else if(PlatformTypeStr.isWanmei173(protocol.platid))
			return new PQueryOrderResponseWM173(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.HuaWei))
			return new PQueryOrderResponseHuaWei(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.GooglePlay)) 
			return new PQueryOrderResponseGooglePlay(protocol);
		else if(protocol.platid.equals(PlatformTypeStr.Unicom))
			return new PQueryOrderResponseUnicom(protocol);
		return null;
	}
	
	public PQueryOrderResponse(QueryOrderResponse protocol) {
		this.protocol = protocol;
	}
	
	protected long getBillId()
	{
		return Long.valueOf(protocol.orderserialgame);
	}
	
	public long getChargeRoleId(long billid)
	{
		xbean.AppReceiptData xreceipt = xtable.Appreceiptes.select(billid);
		if(xreceipt == null)
			return -1;
		return xreceipt.getRoleid();
	}
	
	protected Map<String,String> getParams() throws MarshalException
	{
		final Map<String,String> params = new HashMap<String, String>();
		OctetsStream stream = new OctetsStream(protocol.vars);
		for (int size = stream.uncompact_uint32(); size > 0; --size) {
			String _k_ = stream.unmarshal_String("UTF-16LE");
			String _v_ = stream.unmarshal_String("UTF-16LE");
			params.put(_k_, _v_);
		}
		return params;
	}
	
	protected void logInfo(Map<String,String> params)
	{
		ConfigManager.logger.info(protocol);
		ConfigManager.logger.info(params);
	}
	
	protected void logError(String error)
	{
		ConfigManager.logger.error(error);
		ConfigManager.logger.error(protocol);
		try {
			ConfigManager.logger.error(getParams());
		} catch (MarshalException e) {
		}
	}
	
}
