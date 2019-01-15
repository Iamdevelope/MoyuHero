
package chuhan.gsp.stage;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class StageBattle implements Marshal , Comparable<StageBattle>{
	public int id;
	public byte maxstar; // 0-3
	public short fightnum; // 已战次数
	public int buybattlenum; // 已购买次数
	public int resetnum; // 已重置次数
	public int sweepnum; // 已扫荡次数

	public StageBattle() {
	}

	public StageBattle(int _id_, byte _maxstar_, short _fightnum_, int _buybattlenum_, int _resetnum_, int _sweepnum_) {
		this.id = _id_;
		this.maxstar = _maxstar_;
		this.fightnum = _fightnum_;
		this.buybattlenum = _buybattlenum_;
		this.resetnum = _resetnum_;
		this.sweepnum = _sweepnum_;
	}

	public final boolean _validator_() {
		if (id < 1) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(id);
		_os_.marshal(maxstar);
		_os_.marshal(fightnum);
		_os_.marshal(buybattlenum);
		_os_.marshal(resetnum);
		_os_.marshal(sweepnum);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		maxstar = _os_.unmarshal_byte();
		fightnum = _os_.unmarshal_short();
		buybattlenum = _os_.unmarshal_int();
		resetnum = _os_.unmarshal_int();
		sweepnum = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof StageBattle) {
			StageBattle _o_ = (StageBattle)_o1_;
			if (id != _o_.id) return false;
			if (maxstar != _o_.maxstar) return false;
			if (fightnum != _o_.fightnum) return false;
			if (buybattlenum != _o_.buybattlenum) return false;
			if (resetnum != _o_.resetnum) return false;
			if (sweepnum != _o_.sweepnum) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += (int)maxstar;
		_h_ += fightnum;
		_h_ += buybattlenum;
		_h_ += resetnum;
		_h_ += sweepnum;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(maxstar).append(",");
		_sb_.append(fightnum).append(",");
		_sb_.append(buybattlenum).append(",");
		_sb_.append(resetnum).append(",");
		_sb_.append(sweepnum).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(StageBattle _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = id - _o_.id;
		if (0 != _c_) return _c_;
		_c_ = maxstar - _o_.maxstar;
		if (0 != _c_) return _c_;
		_c_ = fightnum - _o_.fightnum;
		if (0 != _c_) return _c_;
		_c_ = buybattlenum - _o_.buybattlenum;
		if (0 != _c_) return _c_;
		_c_ = resetnum - _o_.resetnum;
		if (0 != _c_) return _c_;
		_c_ = sweepnum - _o_.sweepnum;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

