
package chuhan.gsp.play.tanxian;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CTanxianBegin__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CTanxianBegin extends __CTanxianBegin__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				TanXianColumns col = TanXianColumns.getTanXianColumn(roleId, false);
				
				boolean result = col.tanxianBeginEntry(team, tanxianid);
				if(!result){
					STanxianBegin snd = new STanxianBegin();
					snd.result = STanxianBegin.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788984;

	public int getType() {
		return 788984;
	}

	public java.util.LinkedList<Integer> team; // 小队英雄key列表
	public int tanxianid; // 探险id

	public CTanxianBegin() {
		team = new java.util.LinkedList<Integer>();
	}

	public CTanxianBegin(java.util.LinkedList<Integer> _team_, int _tanxianid_) {
		this.team = _team_;
		this.tanxianid = _tanxianid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(team.size());
		for (Integer _v_ : team) {
			_os_.marshal(_v_);
		}
		_os_.marshal(tanxianid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			team.add(_v_);
		}
		tanxianid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CTanxianBegin) {
			CTanxianBegin _o_ = (CTanxianBegin)_o1_;
			if (!team.equals(_o_.team)) return false;
			if (tanxianid != _o_.tanxianid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += team.hashCode();
		_h_ += tanxianid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(team).append(",");
		_sb_.append(tanxianid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

