
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __STuJianHeros__ extends xio.Protocol { }

/** 获得过的英雄（图鉴） by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class STuJianHeros extends __STuJianHeros__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787762;

	public int getType() {
		return 787762;
	}

	public final static int IS_NEW = 1; // 新增
	public final static int NOT_NEW = 0; // 主动获取

	public java.util.LinkedList<chuhan.gsp.HeroTuJian> herotujian;
	public int isnew; // 是否新增，0为否（上线、主动获取），1为有新增
	public java.util.LinkedList<Integer> tujianbox;
	public java.util.LinkedList<Integer> tjheromaxlevel; // 满级图鉴列表

	public STuJianHeros() {
		herotujian = new java.util.LinkedList<chuhan.gsp.HeroTuJian>();
		tujianbox = new java.util.LinkedList<Integer>();
		tjheromaxlevel = new java.util.LinkedList<Integer>();
	}

	public STuJianHeros(java.util.LinkedList<chuhan.gsp.HeroTuJian> _herotujian_, int _isnew_, java.util.LinkedList<Integer> _tujianbox_, java.util.LinkedList<Integer> _tjheromaxlevel_) {
		this.herotujian = _herotujian_;
		this.isnew = _isnew_;
		this.tujianbox = _tujianbox_;
		this.tjheromaxlevel = _tjheromaxlevel_;
	}

	public final boolean _validator_() {
		for (chuhan.gsp.HeroTuJian _v_ : herotujian)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.compact_uint32(herotujian.size());
		for (chuhan.gsp.HeroTuJian _v_ : herotujian) {
			_os_.marshal(_v_);
		}
		_os_.marshal(isnew);
		_os_.compact_uint32(tujianbox.size());
		for (Integer _v_ : tujianbox) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(tjheromaxlevel.size());
		for (Integer _v_ : tjheromaxlevel) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.HeroTuJian _v_ = new chuhan.gsp.HeroTuJian();
			_v_.unmarshal(_os_);
			herotujian.add(_v_);
		}
		isnew = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			tujianbox.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			tjheromaxlevel.add(_v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof STuJianHeros) {
			STuJianHeros _o_ = (STuJianHeros)_o1_;
			if (!herotujian.equals(_o_.herotujian)) return false;
			if (isnew != _o_.isnew) return false;
			if (!tujianbox.equals(_o_.tujianbox)) return false;
			if (!tjheromaxlevel.equals(_o_.tjheromaxlevel)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herotujian.hashCode();
		_h_ += isnew;
		_h_ += tujianbox.hashCode();
		_h_ += tjheromaxlevel.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herotujian).append(",");
		_sb_.append(isnew).append(",");
		_sb_.append(tujianbox).append(",");
		_sb_.append(tjheromaxlevel).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

