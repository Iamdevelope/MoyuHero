package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
 * result  string(3)  支付结果，取固定值“0”，表示支付成功
 * userName  string
	开发者社区用户名或联盟用户编号
	终端 sdk 上报了联盟用户编号，即支付 ID 时，填写的内容是用户编
	号，反之是社区用户名。
 * productName  string(100)  商品名称
 * payType  int 支付类型
 * amount  string(10)  商品支付金额  (格式为：元.角分，最小金额为分，  例如：20.00)
 * orderId  string(50)  华为订单号
 * notifyTime  string(15)  通知时间。  (自 1970 年 1 月 1 日 0 时起的毫秒数)
 * requestId  string(50)
	开发者支付请求 ID，原样返回开发者 App 调用支付 SDK 时填写的
	requestId 参数值。
	
 */
public class PQueryOrderResponseHuaWei extends PQueryOrderResponse {

	public PQueryOrderResponseHuaWei(QueryOrderResponse protocol) {
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
			String result = params.get("result");
			if(null == result) {
				logError("result == null");
				return false;
			} else if(!"0".equals(result)) {
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
