package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
 * result 支付结果，固定值。“1”代表成功，“0”代表失败
 * money 支付金额，单位：元。
 * order 本次支付的订单号
 * mid 本次支付用户的乐号，既登录后返回的 mid 参数
 * time 时间戳，格式：yyyymmddHH24mmss 月日小时分秒小于 10 前面补充 0
 * ext 发起支付请求时传递的 eif 参数，此处原样返回
 */
public class PQueryOrderResponseDangLe extends PQueryOrderResponse {

	public PQueryOrderResponseDangLe(QueryOrderResponse protocol) {
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
			} else if(!"1".equals(result)) {
				logError("order failed! ");
				return false;
			}
			long billid = getBillId();
			long chargeroleId = getChargeRoleId(billid);
			if(chargeroleId <= 0) {
				logError("chargeroleId not exist");
				return false;
			}
			String amount = params.get("money");
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
