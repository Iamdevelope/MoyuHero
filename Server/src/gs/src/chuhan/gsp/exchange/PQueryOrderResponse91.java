package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

/**
 * 返回订单状态，auany 经 deliver 给 gs，当 errorcode == no_error，vars有意义
	
	91.com:
		ConsumeStreamId  	消费流水号，平台流水号
		CooOrderSerial  	商户订单号，购买时应用传入，原样返回给应用
		MerchantId  			商户 ID
		GoodsId  					商品 ID，购买时应用传入，原样返回给应用
		GoodsInfo  				商品名称，购买时应用传入，原样返回给应用
		GoodsCount  			商品数量，购买时应用传入，原样返回给应用
		OriginalMoney  		原价(格式：0.00)，购买时应用传入的单价*总数，总原价
		OrderMoney  			实际价格(格式：0.00)，购买时应用传入的单价*总数，总实际价格
		PayStatus  				支付状态：0=失败，1=成功，2=正在处理中(仅当 ErrorCode=1，表示接口调用成功时，才需要检查此字段状态，开发商需要根据此参数状态发放物品)
		CreateTime  			创建时间(yyyy-MM-dd HH:mm:ss)
		ErrorCode  				错误码(0=失败，1=成功，2=  AppId 无效，3=  Act 无效，4=参数无效，5= Sign 无效, 11=没有该订单)
		ErrorDesc  				错误描述
 * @author 刘琛
 *
 */
public class PQueryOrderResponse91 extends PQueryOrderResponse {

	public PQueryOrderResponse91(QueryOrderResponse protocol) {
		super(protocol);
	}
	@Override
	protected boolean process() throws Exception {
		if(protocol.errorcode != ErrorCodes.error_succeed)
		{
			logError("protocol.errorcode != ErrorCodes.error_succeed");
			return false;
		}
		final Map<String,String> params = getParams();
		logInfo(params);
		String PayStatus = params.get("PayStatus");
		if(PayStatus == null)
		{
			logError("PayStatus == null");
			return false;
		}
		
		if(!PayStatus.equals("1"))
		{
			logError("PayStatus ="+PayStatus);
			return false;
		}
		String username = params.get("Uin");
		if(username == null)
		{
			logError("Uin == null");
			return false;
		}
		//long chargeroleId = AccountUtil.getRoleIdByUsername(username);
		long billid = getBillId();
		long chargeroleId = getChargeRoleId(billid);
		if(chargeroleId <= 0)
		{
			logError("chargeroleId not exist");
			return false;
		}
		String ordermoney = params.get("OrderMoney");
		if(ordermoney == null)
		{
			logError("ordermoney == null");
			return false;
		}
		double price = Double.valueOf(ordermoney);
		ChargeRole exchangerole = ChargeRole.getChargeRole(chargeroleId, false);
		return exchangerole.confirmCharge(billid,price,protocol.orderserialplat);
	}
}
