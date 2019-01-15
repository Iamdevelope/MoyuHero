
package chuhan.gsp.play.endlessbattle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __STodayEndless__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class STodayEndless extends __STodayEndless__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788941;

	public int getType() {
		return 788941;
	}

	public int isend; // 0未开始，1进行中，2结束
	public int groupnum; // 第几轮
	public int alldropnum; // 勇者证明总数
	public int pact; // 强者之约（没有则为-1）
	public int ishalfcostpact; // 本次购买是否半价（0是本次全价，1是半价）
	public int paiming; // 预测今日排名（-1未排名，1~20为具体排名，20以上为20名之外）
	public int isnotfirst; // 不是第一次战斗（0是第一次战斗，1不是第一次战斗）

	public STodayEndless() {
	}

	public STodayEndless(int _isend_, int _groupnum_, int _alldropnum_, int _pact_, int _ishalfcostpact_, int _paiming_, int _isnotfirst_) {
		this.isend = _isend_;
		this.groupnum = _groupnum_;
		this.alldropnum = _alldropnum_;
		this.pact = _pact_;
		this.ishalfcostpact = _ishalfcostpact_;
		this.paiming = _paiming_;
		this.isnotfirst = _isnotfirst_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(isend);
		_os_.marshal(groupnum);
		_os_.marshal(alldropnum);
		_os_.marshal(pact);
		_os_.marshal(ishalfcostpact);
		_os_.marshal(paiming);
		_os_.marshal(isnotfirst);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		isend = _os_.unmarshal_int();
		groupnum = _os_.unmarshal_int();
		alldropnum = _os_.unmarshal_int();
		pact = _os_.unmarshal_int();
		ishalfcostpact = _os_.unmarshal_int();
		paiming = _os_.unmarshal_int();
		isnotfirst = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof STodayEndless) {
			STodayEndless _o_ = (STodayEndless)_o1_;
			if (isend != _o_.isend) return false;
			if (groupnum != _o_.groupnum) return false;
			if (alldropnum != _o_.alldropnum) return false;
			if (pact != _o_.pact) return false;
			if (ishalfcostpact != _o_.ishalfcostpact) return false;
			if (paiming != _o_.paiming) return false;
			if (isnotfirst != _o_.isnotfirst) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += isend;
		_h_ += groupnum;
		_h_ += alldropnum;
		_h_ += pact;
		_h_ += ishalfcostpact;
		_h_ += paiming;
		_h_ += isnotfirst;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(isend).append(",");
		_sb_.append(groupnum).append(",");
		_sb_.append(alldropnum).append(",");
		_sb_.append(pact).append(",");
		_sb_.append(ishalfcostpact).append(",");
		_sb_.append(paiming).append(",");
		_sb_.append(isnotfirst).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(STodayEndless _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = isend - _o_.isend;
		if (0 != _c_) return _c_;
		_c_ = groupnum - _o_.groupnum;
		if (0 != _c_) return _c_;
		_c_ = alldropnum - _o_.alldropnum;
		if (0 != _c_) return _c_;
		_c_ = pact - _o_.pact;
		if (0 != _c_) return _c_;
		_c_ = ishalfcostpact - _o_.ishalfcostpact;
		if (0 != _c_) return _c_;
		_c_ = paiming - _o_.paiming;
		if (0 != _c_) return _c_;
		_c_ = isnotfirst - _o_.isnotfirst;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

