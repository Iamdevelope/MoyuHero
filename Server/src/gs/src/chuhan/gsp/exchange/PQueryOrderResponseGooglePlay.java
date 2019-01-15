package chuhan.gsp.exchange;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;
public class PQueryOrderResponseGooglePlay extends PQueryOrderResponse {

	public PQueryOrderResponseGooglePlay(QueryOrderResponse protocol) {
		super(protocol);
	}
	@Override
	protected boolean process() throws Exception {
		try {
			if(protocol.errorcode != ErrorCodes.error_succeed) {
				logError("GooglePlay protocol.errorcode != ErrorCodes.error_succeed");
				return false;
			}
			final Map<String,String> params = getParams();
			logInfo(params);
			
			long billid = getBillId();
//			long roleId = getChargeRoleId(billid);
			xbean.GoogleReceiptData xreceipt = xtable.Googlereceiptes.select(billid);
			if(xreceipt == null) {
				logError("GooglePlay ReceiptData == null");
				return false;
			}
			ChargeRole crole = ChargeRole.getChargeRole(xreceipt.getRoleid(), false);
			return crole.confirmChargeByAppStore(billid, xreceipt.getProductid(), 0, xreceipt.getToken());
		} catch (MarshalException e) {
			e.printStackTrace();
			logError("MarshalError");
		}catch (Exception e) {
			e.printStackTrace();
			logError("NumberConvError");
		}
		
		return false;
	}
}
