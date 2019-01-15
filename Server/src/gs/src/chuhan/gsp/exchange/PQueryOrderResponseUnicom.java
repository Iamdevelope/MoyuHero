package chuhan.gsp.exchange;

import gnet.ErrorCodes;
import gnet.QueryOrderResponse;

import java.util.Map;

import com.goldhuman.Common.Marshal.MarshalException;
/**
	result	int	4	调用结果
			0 用户购买成功。
			1 用户购买失败。购买失败时只有errorstr有值。
	paymentid	String	订单id	字符串，唯一值，长度不定，100字符限制
	errorstr	String	UTF（限制100汉字）	执行订购内容为空 失败：失败原因
	channelid	String	UTF（3位字符）	游戏厂商自己的各渠道号id
	softgood	String	UTF（限制24个英文字符，12汉字）	商品名（成功则不为空）
	money	String	UTF（限制待定）	商品价格，单位：分
		例：商品为1元时候，发送100
		（成功则不为空）
	playername	String	UTF（限制待定）	该服务器上的用户所选人物名（主要用于网游，没有则为空）
	date	String	UTF	yyyy-MM-dd HH:mm:ss
	gamename	String	UTF（限制24个英文字符，12汉字）	游戏名称
	gamecode	String	UTF（8英文字符）	游戏id
	inputchannelid	String	UTF	渠道别名
	goodinputid	String	UTF（限制30个英文字符）	商品别名
	customorderno	String	UTF(16位字符）	自定义订单号
*/
public class PQueryOrderResponseUnicom extends PQueryOrderResponse {

	public PQueryOrderResponseUnicom(QueryOrderResponse protocol) {
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
			String amount = params.get("money");
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
