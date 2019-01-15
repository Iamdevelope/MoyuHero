
package chuhan.gsp.exchange;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SReplyGoods__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SReplyGoods extends __SReplyGoods__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788135;

	public int getType() {
		return 788135;
	}

	public byte goodtype; // GoodType
	public java.util.LinkedList<chuhan.gsp.exchange.GoodInfo> goods;

	public SReplyGoods() {
		goods = new java.util.LinkedList<chuhan.gsp.exchange.GoodInfo>();
	}

	public SReplyGoods(byte _goodtype_, java.util.LinkedList<chuhan.gsp.exchange.GoodInfo> _goods_) {
		this.goodtype = _goodtype_;
		this.goods = _goods_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.exchange.GoodInfo _v_ : goods)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(goodtype);
		_os_.compact_uint32(goods.size());
		for (chuhan.gsp.exchange.GoodInfo _v_ : goods) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		goodtype = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.exchange.GoodInfo _v_ = new chuhan.gsp.exchange.GoodInfo();
			_v_.unmarshal(_os_);
			goods.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SReplyGoods) {
			SReplyGoods _o_ = (SReplyGoods)_o1_;
			if (goodtype != _o_.goodtype) return false;
			if (!goods.equals(_o_.goods)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)goodtype;
		_h_ += goods.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(goodtype).append(",");
		_sb_.append(goods).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

