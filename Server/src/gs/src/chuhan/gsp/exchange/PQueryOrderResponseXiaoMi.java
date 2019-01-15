package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
 * appId
 * cpOrderId 开发商订单 ID
 * cpUserInfo 开发商透传信息
 * uid 用户 ID
 * orderId 游戏平台订单 ID
 * orderStatus 订单状态 TRADE_SUCCESS 代表成功
 * payFee 支付金额，单位为分
 * productCode 商品代码
 * productName 商品名称
 * productCount 商品数量
 * payTime 支付时间，格式 yyyy-MM-dd HH:mm:ss
 */
public class PQueryOrderResponseXiaoMi extends PQueryOrderResponse {

	public PQueryOrderResponseXiaoMi(QueryOrderResponse protocol) {
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
			} else if(!"TRADE_SUCCESS".equals(orderStatus)) {
				logError("order failed! ");
				return false;
			}
			long billid = getBillId();
			long chargeroleId = getChargeRoleId(billid);
			if(chargeroleId <= 0) {
				logError("chargeroleId not exist");
				return false;
			}
			String amount = params.get("payFee");
			double price = Double.valueOf(amount) / 100;//转换为元
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
