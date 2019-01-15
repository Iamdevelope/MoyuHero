
package gnet;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __QueryOrderResponse__ extends xio.Protocol { }

/** 返回订单状态，auany 经 link 给 gs，当 errorcode == no_error，vars有意义
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
		
	25pp.com:
		account	varchar(150)	通行证账号
		amount	double(8,2)	兑换PP币数量
		status	tinyint(1)	状态:
				0正常状态
				1已兑换过并成功返回
		roleid	varchar(50)	厂商应用角色id
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class QueryOrderResponse extends __QueryOrderResponse__ {
	@Override
	protected void process() {
		// protocol handle
		chuhan.gsp.exchange.PQueryOrderResponse p = chuhan.gsp.exchange.PQueryOrderResponse.getPQueryOrderResponse(this);
		p.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 8905;

	public int getType() {
		return 8905;
	}

	public final static int restype_query = 0; // 查询返回结果
	public final static int restype_notify = 0; // 主动通告返回结果

	public int errorcode;
	public int restype;
	public java.lang.String platid; // 平台唯一标识
	public java.lang.String orderserialplat; // 平台用的订单号
	public java.lang.String orderserialgame; // 游戏内自己用的订单号
	public int userid; // 如果返回的结果中，并没有userid的信息，该值为-1
	public com.goldhuman.Common.Octets vars;

	public QueryOrderResponse() {
		errorcode = ErrorCodes.error_succeed;
		restype = restype_query;
		platid = "";
		orderserialplat = "";
		orderserialgame = "";
		userid = -1;
		vars = new com.goldhuman.Common.Octets();
	}

	public QueryOrderResponse(int _errorcode_, int _restype_, java.lang.String _platid_, java.lang.String _orderserialplat_, java.lang.String _orderserialgame_, int _userid_, com.goldhuman.Common.Octets _vars_) {
		this.errorcode = _errorcode_;
		this.restype = _restype_;
		this.platid = _platid_;
		this.orderserialplat = _orderserialplat_;
		this.orderserialgame = _orderserialgame_;
		this.userid = _userid_;
		this.vars = _vars_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(errorcode);
		_os_.marshal(restype);
		_os_.marshal(platid, "UTF-16LE");
		_os_.marshal(orderserialplat, "UTF-16LE");
		_os_.marshal(orderserialgame, "UTF-16LE");
		_os_.marshal(userid);
		_os_.marshal(vars);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		errorcode = _os_.unmarshal_int();
		restype = _os_.unmarshal_int();
		platid = _os_.unmarshal_String("UTF-16LE");
		orderserialplat = _os_.unmarshal_String("UTF-16LE");
		orderserialgame = _os_.unmarshal_String("UTF-16LE");
		userid = _os_.unmarshal_int();
		vars = _os_.unmarshal_Octets();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof QueryOrderResponse) {
			QueryOrderResponse _o_ = (QueryOrderResponse)_o1_;
			if (errorcode != _o_.errorcode) return false;
			if (restype != _o_.restype) return false;
			if (!platid.equals(_o_.platid)) return false;
			if (!orderserialplat.equals(_o_.orderserialplat)) return false;
			if (!orderserialgame.equals(_o_.orderserialgame)) return false;
			if (userid != _o_.userid) return false;
			if (!vars.equals(_o_.vars)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += errorcode;
		_h_ += restype;
		_h_ += platid.hashCode();
		_h_ += orderserialplat.hashCode();
		_h_ += orderserialgame.hashCode();
		_h_ += userid;
		_h_ += vars.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(errorcode).append(",");
		_sb_.append(restype).append(",");
		_sb_.append("T").append(platid.length()).append(",");
		_sb_.append("T").append(orderserialplat.length()).append(",");
		_sb_.append("T").append(orderserialgame.length()).append(",");
		_sb_.append(userid).append(",");
		_sb_.append("B").append(vars.size()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

