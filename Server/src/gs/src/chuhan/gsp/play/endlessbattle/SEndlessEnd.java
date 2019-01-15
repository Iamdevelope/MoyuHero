
package chuhan.gsp.play.endlessbattle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEndlessEnd__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEndlessEnd extends __SEndlessEnd__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788939;

	public int getType() {
		return 788939;
	}

	public int groupnum; // 第几轮
	public int alldropnum; // 勇者证明总数
	public int pact; // 强者之约（没有则为-1）
	public int pactispass; // 强者之约是否达成（0为未达成，1为达成）
	public java.util.HashMap<Integer,Integer> dropmap; // 掉落收益（key为物品或资源ID，value为数量）

	public SEndlessEnd() {
		dropmap = new java.util.HashMap<Integer,Integer>();
	}

	public SEndlessEnd(int _groupnum_, int _alldropnum_, int _pact_, int _pactispass_, java.util.HashMap<Integer,Integer> _dropmap_) {
		this.groupnum = _groupnum_;
		this.alldropnum = _alldropnum_;
		this.pact = _pact_;
		this.pactispass = _pactispass_;
		this.dropmap = _dropmap_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(groupnum);
		_os_.marshal(alldropnum);
		_os_.marshal(pact);
		_os_.marshal(pactispass);
		_os_.compact_uint32(dropmap.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : dropmap.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		groupnum = _os_.unmarshal_int();
		alldropnum = _os_.unmarshal_int();
		pact = _os_.unmarshal_int();
		pactispass = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			int _v_;
			_v_ = _os_.unmarshal_int();
			dropmap.put(_k_, _v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEndlessEnd) {
			SEndlessEnd _o_ = (SEndlessEnd)_o1_;
			if (groupnum != _o_.groupnum) return false;
			if (alldropnum != _o_.alldropnum) return false;
			if (pact != _o_.pact) return false;
			if (pactispass != _o_.pactispass) return false;
			if (!dropmap.equals(_o_.dropmap)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += groupnum;
		_h_ += alldropnum;
		_h_ += pact;
		_h_ += pactispass;
		_h_ += dropmap.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(groupnum).append(",");
		_sb_.append(alldropnum).append(",");
		_sb_.append(pact).append(",");
		_sb_.append(pactispass).append(",");
		_sb_.append(dropmap).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

