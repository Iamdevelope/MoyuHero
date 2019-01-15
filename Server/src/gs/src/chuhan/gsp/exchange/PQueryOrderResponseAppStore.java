package chuhan.gsp.exchange;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;
/**
 *  * 返回订单状态，auany 经 deliver 给 gs，当 errorcode == no_error，vars有意义
 * 	{ "product_id", 
 * "original_purchase_date_ms", 
 * "transaction_id", 
 * "unique_identifier", 
 * "item_id", 
 * "quantity", 
 * "bid", 
 * "unique_vendor_identifier"};
 * @author 刘琛
 *
 */
public class PQueryOrderResponseAppStore extends PQueryOrderResponse {

	public PQueryOrderResponseAppStore(QueryOrderResponse protocol) {
		super(protocol);
	}
	@Override
	protected boolean process() throws Exception {
		try {
			if(protocol.errorcode != ErrorCodes.error_succeed)
			{
				logError("protocol.errorcode != ErrorCodes.error_succeed");
				return false;
			}
			final Map<String,String> params = getParams();
			logInfo(params);
			String transaction_id = params.get("transaction_id");
			if(!protocol.orderserialgame.equals(transaction_id))
			{
				logError("orderserialgame != transaction_id");
				return false;
			}
			
			long billid = getBillId();
			long roleId = getChargeRoleId(billid);
			if(roleId <= 0)
			{
				logError("AppReceiptData == null");
				return false;
			}
			String product_id = params.get("product_id");
			String unique_identifier = params.get("unique_identifier");
			ChargeRole crole = ChargeRole.getChargeRole(roleId, false);
			return crole.confirmChargeByAppStore(billid, product_id, 0, unique_identifier);
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
