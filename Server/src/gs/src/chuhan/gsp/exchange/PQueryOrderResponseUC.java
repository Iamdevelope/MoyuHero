package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
 * amount 支付金额  单位：元
 * callbackInfo
 * failedDesc 订单失败原因详细描述 如果是成功支付，则为空串
 * gameId 游戏编号  由 UC 分配
 * orderId 充值订单号
 * orderStatus 订单状态  S-成功支付 F-支付失败
 * payWay 支付通道代码 支付通道
 * serverId 服务器编号 由 UC 分配
 * ucid UC 账号  
 *	
 */
public class PQueryOrderResponseUC extends PQueryOrderResponse {

	public PQueryOrderResponseUC(QueryOrderResponse protocol) {
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
			String orderStatus = params.get("orderStatus");
			if(null == orderStatus) {
				logError("orderStatus == null");
				return false;
			} else if(!"S".equals(orderStatus)) {
				logError("order failed! desc = " + params.get("failedDesc"));
				return false;
			}
			long billid = getBillId();
			long chargeroleId = getChargeRoleId(billid);
			if(chargeroleId <= 0) {
				logError("chargeroleId not exist");
				return false;
			}
			String amount = params.get("amount");
			double price = Double.valueOf(amount);
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
