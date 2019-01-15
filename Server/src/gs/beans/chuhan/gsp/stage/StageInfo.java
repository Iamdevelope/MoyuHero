
package chuhan.gsp.stage;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class StageInfo implements Marshal {
	public int id;
	public byte starsum; // 暂时是总数，无用
	public java.util.LinkedList<chuhan.gsp.stage.StageBattle> stagebattles;
	public int rewardgot; // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取

	public StageInfo() {
		stagebattles = new java.util.LinkedList<chuhan.gsp.stage.StageBattle>();
	}

	public StageInfo(int _id_, byte _starsum_, java.util.LinkedList<chuhan.gsp.stage.StageBattle> _stagebattles_, int _rewardgot_) {
		this.id = _id_;
		this.starsum = _starsum_;
		this.stagebattles = _stagebattles_;
		this.rewardgot = _rewardgot_;
	}

	public final boolean _validator_() {
		if (id < 1) return false;
		for (chuhan.gsp.stage.StageBattle _v_ : stagebattles)
			if (!_v_._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(id);
		_os_.marshal(starsum);
		_os_.compact_uint32(stagebattles.size());
		for (chuhan.gsp.stage.StageBattle _v_ : stagebattles) {
			_os_.marshal(_v_);
		}
		_os_.marshal(rewardgot);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		starsum = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.stage.StageBattle _v_ = new chuhan.gsp.stage.StageBattle();
			_v_.unmarshal(_os_);
			stagebattles.add(_v_);
		}
		rewardgot = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof StageInfo) {
			StageInfo _o_ = (StageInfo)_o1_;
			if (id != _o_.id) return false;
			if (starsum != _o_.starsum) return false;
			if (!stagebattles.equals(_o_.stagebattles)) return false;
			if (rewardgot != _o_.rewardgot) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += (int)starsum;
		_h_ += stagebattles.hashCode();
		_h_ += rewardgot;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(starsum).append(",");
		_sb_.append(stagebattles).append(",");
		_sb_.append(rewardgot).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

