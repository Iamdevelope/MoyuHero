
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SFinishWithBeauty__ extends xio.Protocol { }

/** 完成缠绵时发送
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SFinishWithBeauty extends __SFinishWithBeauty__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787761;

	public int getType() {
		return 787761;
	}

	public short beautyid;
	public java.util.HashMap<Byte,Byte> beautyprocess;
	public byte success;
	public chuhan.gsp.item.ShowItemData item;

	public SFinishWithBeauty() {
		beautyprocess = new java.util.HashMap<Byte,Byte>();
		item = new chuhan.gsp.item.ShowItemData();
	}

	public SFinishWithBeauty(short _beautyid_, java.util.HashMap<Byte,Byte> _beautyprocess_, byte _success_, chuhan.gsp.item.ShowItemData _item_) {
		this.beautyid = _beautyid_;
		this.beautyprocess = _beautyprocess_;
		this.success = _success_;
		this.item = _item_;
	}

	public final boolean _validator_() {
		if (!item._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(beautyid);
		_os_.compact_uint32(beautyprocess.size());
		for (java.util.Map.Entry<Byte, Byte> _e_ : beautyprocess.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(success);
		_os_.marshal(item);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		beautyid = _os_.unmarshal_short();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			byte _k_;
			_k_ = _os_.unmarshal_byte();
			byte _v_;
			_v_ = _os_.unmarshal_byte();
			beautyprocess.put(_k_, _v_);
		}
		success = _os_.unmarshal_byte();
		item.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SFinishWithBeauty) {
			SFinishWithBeauty _o_ = (SFinishWithBeauty)_o1_;
			if (beautyid != _o_.beautyid) return false;
			if (!beautyprocess.equals(_o_.beautyprocess)) return false;
			if (success != _o_.success) return false;
			if (!item.equals(_o_.item)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += beautyid;
		_h_ += beautyprocess.hashCode();
		_h_ += (int)success;
		_h_ += item.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(beautyid).append(",");
		_sb_.append(beautyprocess).append(",");
		_sb_.append(success).append(",");
		_sb_.append(item).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

