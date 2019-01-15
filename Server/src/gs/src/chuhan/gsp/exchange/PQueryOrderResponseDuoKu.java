package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
 * amount 成功充值金额 单位为元
 * cardtype 充值类型
 * orderid 订单 ID
 * result 充值结果，1  表示成功，2 表示失败
 * timetamp 订单成功时间，北京 UNIX 时间 
 * aid 商品描述（原文返回）,对应客户端传入的字段
 */
public class PQueryOrderResponseDuoKu extends PQueryOrderResponse {

	public PQueryOrderResponseDuoKu(QueryOrderResponse protocol) {
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
