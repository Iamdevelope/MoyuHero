package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
		
 * @author 刘琛
 *
 */
public class PQueryOrderResponseTBT extends PQueryOrderResponse {

	public PQueryOrderResponseTBT(QueryOrderResponse protocol) {
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
			long billid = getBillId();
			long chargeroleId = getChargeRoleId(billid);
			if(chargeroleId <= 0)
			{
				logError("chargeroleId not exist");
				return false;
			}
			String amount = params.get("amount");
			double price = Double.valueOf(amount);
			if(price <= 0)
			{
				logError("price <= 0");
				return false;
			}
			
			//TODO roleid暂时未用，将来可能使用
			
			ChargeRole exchangerole = ChargeRole.getChargeRole(chargeroleId, false);
			//price = price/100;
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
