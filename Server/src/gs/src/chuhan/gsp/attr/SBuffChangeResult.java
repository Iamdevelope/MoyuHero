
package chuhan.gsp.attr;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SBuffChangeResult__ extends xio.Protocol { }

/** buff改变结果更新协议
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SBuffChangeResult extends __SBuffChangeResult__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787435;

	public int getType() {
		return 787435;
	}

	public int agenttype;
	public long id;
	public int petid;
	public java.util.HashMap<Integer,chuhan.gsp.attr.Buff> addedbuffs; // 添加的buff, key=buff type id,更新buff时也用此部分
	public java.util.LinkedList<Integer> deletedbuffs; // 删除的buff id

	public SBuffChangeResult() {
		addedbuffs = new java.util.HashMap<Integer,chuhan.gsp.attr.Buff>();
		deletedbuffs = new java.util.LinkedList<Integer>();
	}

	public SBuffChangeResult(int _agenttype_, long _id_, int _petid_, java.util.HashMap<Integer,chuhan.gsp.attr.Buff> _addedbuffs_, java.util.LinkedList<Integer> _deletedbuffs_) {
		this.agenttype = _agenttype_;
		this.id = _id_;
		this.petid = _petid_;
		this.addedbuffs = _addedbuffs_;
		this.deletedbuffs = _deletedbuffs_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.attr.Buff> _e_ : addedbuffs.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(agenttype);
		_os_.marshal(id);
		_os_.marshal(petid);
		_os_.compact_uint32(addedbuffs.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.attr.Buff> _e_ : addedbuffs.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(deletedbuffs.size());
		for (Integer _v_ : deletedbuffs) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		agenttype = _os_.unmarshal_int();
		id = _os_.unmarshal_long();
		petid = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.attr.Buff _v_ = new chuhan.gsp.attr.Buff();
			_v_.unmarshal(_os_);
			addedbuffs.put(_k_, _v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			deletedbuffs.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SBuffChangeResult) {
			SBuffChangeResult _o_ = (SBuffChangeResult)_o1_;
			if (agenttype != _o_.agenttype) return false;
			if (id != _o_.id) return false;
			if (petid != _o_.petid) return false;
			if (!addedbuffs.equals(_o_.addedbuffs)) return false;
			if (!deletedbuffs.equals(_o_.deletedbuffs)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += agenttype;
		_h_ += (int)id;
		_h_ += petid;
		_h_ += addedbuffs.hashCode();
		_h_ += deletedbuffs.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(agenttype).append(",");
		_sb_.append(id).append(",");
		_sb_.append(petid).append(",");
		_sb_.append(addedbuffs).append(",");
		_sb_.append(deletedbuffs).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

