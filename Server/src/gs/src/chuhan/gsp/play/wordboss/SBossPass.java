
package chuhan.gsp.play.wordboss;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SBossPass__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SBossPass extends __SBossPass__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788890;

	public int getType() {
		return 788890;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败
	public final static int END_ISKILL = 3; // BOSS被击杀
	public final static int END_KILLBOSS = 4; // 击杀BOSS

	public int result;
	public int bossid; // 值为1234，代表第几个boss
	public chuhan.gsp.play.wordboss.bossrole mywordboss;
	public java.util.HashMap<Integer,Integer> dropmap; // 收益，key为物品或资源ID，value为数量
	public java.lang.String bosskillname; // 击杀者名称

	public SBossPass() {
		mywordboss = new chuhan.gsp.play.wordboss.bossrole();
		dropmap = new java.util.HashMap<Integer,Integer>();
		bosskillname = "";
	}

	public SBossPass(int _result_, int _bossid_, chuhan.gsp.play.wordboss.bossrole _mywordboss_, java.util.HashMap<Integer,Integer> _dropmap_, java.lang.String _bosskillname_) {
		this.result = _result_;
		this.bossid = _bossid_;
		this.mywordboss = _mywordboss_;
		this.dropmap = _dropmap_;
		this.bosskillname = _bosskillname_;
	}

	public final boolean _validator_() {
		if (!mywordboss._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(result);
		_os_.marshal(bossid);
		_os_.marshal(mywordboss);
		_os_.compact_uint32(dropmap.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : dropmap.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(bosskillname, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		result = _os_.unmarshal_int();
		bossid = _os_.unmarshal_int();
		mywordboss.unmarshal(_os_);
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			int _v_;
			_v_ = _os_.unmarshal_int();
			dropmap.put(_k_, _v_);
		}
		bosskillname = _os_.unmarshal_String("UTF-16LE");
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SBossPass) {
			SBossPass _o_ = (SBossPass)_o1_;
			if (result != _o_.result) return false;
			if (bossid != _o_.bossid) return false;
			if (!mywordboss.equals(_o_.mywordboss)) return false;
			if (!dropmap.equals(_o_.dropmap)) return false;
			if (!bosskillname.equals(_o_.bosskillname)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += result;
		_h_ += bossid;
		_h_ += mywordboss.hashCode();
		_h_ += dropmap.hashCode();
		_h_ += bosskillname.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(result).append(",");
		_sb_.append(bossid).append(",");
		_sb_.append(mywordboss).append(",");
		_sb_.append(dropmap).append(",");
		_sb_.append("T").append(bosskillname.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

