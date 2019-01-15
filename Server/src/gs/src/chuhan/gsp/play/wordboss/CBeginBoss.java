
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CBeginBoss__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CBeginBoss extends __CBeginBoss__ {
	@Override
	protected void process() {
		// protocol handle
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				
				boolean result = Module.getInstance().beginBossEntry(roleId, bossid, troopid,iscost);
				if( !result ){
					SBeginBoss snd = new SBeginBoss();
					snd.result = SBeginBoss.END_ERROR;
					xdb.Procedure.psend(roleId, snd);
				}
				return result;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788887;

	public int getType() {
		return 788887;
	}

	public short troopid; // 战队ID
	public int bossid; // 值为1234，代表第几个boss
	public int iscost; // 是否刷新进入，0否，1是

	public CBeginBoss() {
	}

	public CBeginBoss(short _troopid_, int _bossid_, int _iscost_) {
		this.troopid = _troopid_;
		this.bossid = _bossid_;
		this.iscost = _iscost_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(troopid);
		_os_.marshal(bossid);
		_os_.marshal(iscost);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		troopid = _os_.unmarshal_short();
		bossid = _os_.unmarshal_int();
		iscost = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CBeginBoss) {
			CBeginBoss _o_ = (CBeginBoss)_o1_;
			if (troopid != _o_.troopid) return false;
			if (bossid != _o_.bossid) return false;
			if (iscost != _o_.iscost) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += troopid;
		_h_ += bossid;
		_h_ += iscost;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(troopid).append(",");
		_sb_.append(bossid).append(",");
		_sb_.append(iscost).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CBeginBoss _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = troopid - _o_.troopid;
		if (0 != _c_) return _c_;
		_c_ = bossid - _o_.bossid;
		if (0 != _c_) return _c_;
		_c_ = iscost - _o_.iscost;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

