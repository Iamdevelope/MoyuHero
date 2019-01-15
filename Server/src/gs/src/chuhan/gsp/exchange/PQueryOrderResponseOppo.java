package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
 * notify_id 通知 id
 * amount 金额， 游戏客户端传入的商品单价或者总金额 Double 类型的 String例如：1.0，2.33，100.0
 * partner_code 开发者的 AppKey
 * partner_order 开发者的订单
 * orders 订单信息
 * pay_result 支付结果，成功为 OK
 */
public class PQueryOrderResponseOppo extends PQueryOrderResponse {

	public PQueryOrderResponseOppo(QueryOrderResponse protocol) {
		super(protocol);
	}
	@Override
	protected boolean process() throws Exception {
		try {
			if(protocol.errorcode != ErrorCodes.error_succeed) {
				logError("protocol.errorcode != ErrorCodes.error_succeed");
				return false;
			}
			
			final Map<String,String> params = getParams();
			logInfo(params);
			String result = params.get("pay_result");
			if(null == result) {
				logError("result == null");
				return false;
			} else if(!"OK".equals(result)) {
				logError("order failed! ");
				return false;
			}
			long billid = getBillId();
			long chargeroleId = getChargeRoleId(billid);
			if(chargeroleId <= 0) {
				logError("chargeroleId not exist");
				return false;
			}
			String amount = params.get("amount");
			double price = Double.valueOf(amount) / 100;
			if(price <= 0) {
				logError("price <= 0");
				return false;
			}
			
			ChargeRole exchangerole = ChargeRole.getChargeRole(chargeroleId, false);
			return exchangerole.confirmCharge(billid,price,protocol.orderserialplat);
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
