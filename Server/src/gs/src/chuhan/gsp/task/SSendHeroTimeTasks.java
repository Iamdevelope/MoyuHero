
package chuhan.gsp.task;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSendHeroTimeTasks__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSendHeroTimeTasks extends __SSendHeroTimeTasks__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788040;

	public int getType() {
		return 788040;
	}

	public java.util.ArrayList<chuhan.gsp.task.HeroTimeTask> proceedtasks;
	public java.util.ArrayList<Byte> endtasks;

	public SSendHeroTimeTasks() {
		proceedtasks = new java.util.ArrayList<chuhan.gsp.task.HeroTimeTask>();
		endtasks = new java.util.ArrayList<Byte>();
	}

	public SSendHeroTimeTasks(java.util.ArrayList<chuhan.gsp.task.HeroTimeTask> _proceedtasks_, java.util.ArrayList<Byte> _endtasks_) {
		this.proceedtasks = _proceedtasks_;
		this.endtasks = _endtasks_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.task.HeroTimeTask _v_ : proceedtasks)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(proceedtasks.size());
		for (chuhan.gsp.task.HeroTimeTask _v_ : proceedtasks) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(endtasks.size());
		for (Byte _v_ : endtasks) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.task.HeroTimeTask _v_ = new chuhan.gsp.task.HeroTimeTask();
			_v_.unmarshal(_os_);
			proceedtasks.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			byte _v_;
			_v_ = _os_.unmarshal_byte();
			endtasks.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSendHeroTimeTasks) {
			SSendHeroTimeTasks _o_ = (SSendHeroTimeTasks)_o1_;
			if (!proceedtasks.equals(_o_.proceedtasks)) return false;
			if (!endtasks.equals(_o_.endtasks)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += proceedtasks.hashCode();
		_h_ += endtasks.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(proceedtasks).append(",");
		_sb_.append(endtasks).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

