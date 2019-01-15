
package chuhan.gsp.play.turntable;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __STurnTable__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class STurnTable extends __STurnTable__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788438;

	public int getType() {
		return 788438;
	}

	public byte itemcheck; // 随机到的道具格
	public byte ishavefree; // 是否有免费转的次数
	public int key; // 获得的奖励key
	public int id; // 获得的奖励id
	public int num; // 获得的奖励数量

	public STurnTable() {
	}

	public STurnTable(byte _itemcheck_, byte _ishavefree_, int _key_, int _id_, int _num_) {
		this.itemcheck = _itemcheck_;
		this.ishavefree = _ishavefree_;
		this.key = _key_;
		this.id = _id_;
		this.num = _num_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(itemcheck);
		_os_.marshal(ishavefree);
		_os_.marshal(key);
		_os_.marshal(id);
		_os_.marshal(num);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		itemcheck = _os_.unmarshal_byte();
		ishavefree = _os_.unmarshal_byte();
		key = _os_.unmarshal_int();
		id = _os_.unmarshal_int();
		num = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof STurnTable) {
			STurnTable _o_ = (STurnTable)_o1_;
			if (itemcheck != _o_.itemcheck) return false;
			if (ishavefree != _o_.ishavefree) return false;
			if (key != _o_.key) return false;
			if (id != _o_.id) return false;
			if (num != _o_.num) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)itemcheck;
		_h_ += (int)ishavefree;
		_h_ += key;
		_h_ += id;
		_h_ += num;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(itemcheck).append(",");
		_sb_.append(ishavefree).append(",");
		_sb_.append(key).append(",");
		_sb_.append(id).append(",");
		_sb_.append(num).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(STurnTable _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = itemcheck - _o_.itemcheck;
		if (0 != _c_) return _c_;
		_c_ = ishavefree - _o_.ishavefree;
		if (0 != _c_) return _c_;
		_c_ = key - _o_.key;
		if (0 != _c_) return _c_;
		_c_ = id - _o_.id;
		if (0 != _c_) return _c_;
		_c_ = num - _o_.num;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

