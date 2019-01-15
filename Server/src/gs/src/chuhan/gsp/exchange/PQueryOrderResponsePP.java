package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
 *  * 返回订单状态，auany 经 deliver 给 gs，当 errorcode == no_error，vars有意义
 * 	25pp.com:
		account	varchar(150)	通行证账号
		amount	double(8,2)	兑换PP币数量
		status	tinyint(1)	状态:
				0正常状态
				1已兑换过并成功返回
		roleid	varchar(50)	厂商应用角色id
		
 * @author 刘琛
 *
 */
public class PQueryOrderResponsePP extends PQueryOrderResponse {

	public PQueryOrderResponsePP(QueryOrderResponse protocol) {
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
			String username = params.get("account");
			if(username == null)
			{
				logError("username == null");
				return false;
			}
			long billid = getBillId();
			long chargeroleId = getChargeRoleId(billid);
			//long chargeroleId = AccountUtil.getRoleIdByUsername(username);
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
			String status = params.get("status");
			int state = Integer.valueOf(status);

			
			//TODO roleid暂时未用，将来可能使用
			
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
