package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
 * userId	是	Long	老虎用户唯一标示
 * appId	是	Integer	游戏appId
 * pOrder	是	Long	平台订单号
 * t	是	Long	时间戳（秒）
 * appOrder	是	String	游戏订单
 * amount	是	Integer	金额【单位：分】
 * payStatus	是	String	支付结果【0：失败，1：成功】
 */
public class PQueryOrderResponseWM173 extends PQueryOrderResponse {

	public PQueryOrderResponseWM173(QueryOrderResponse protocol) {
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
			String result = params.get("payStatus");
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
			return exchangerole.confirmCharge(billid,price/100,protocol.orderserialplat);
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
