
package chuhan.gsp.battle.realtime;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class BHeroMove implements Marshal {
	public int herokey; // 英雄key（包括召唤物）
	public chuhan.gsp.battle.realtime.Place place;
	public chuhan.gsp.battle.realtime.FacePlace faceplace;

	public BHeroMove() {
		place = new chuhan.gsp.battle.realtime.Place();
		faceplace = new chuhan.gsp.battle.realtime.FacePlace();
	}

	public BHeroMove(int _herokey_, chuhan.gsp.battle.realtime.Place _place_, chuhan.gsp.battle.realtime.FacePlace _faceplace_) {
		this.herokey = _herokey_;
		this.place = _place_;
		this.faceplace = _faceplace_;
	}

	public final boolean _validator_() {
		if (!place._validator_()) return false;
		if (!faceplace._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(herokey);
		_os_.marshal(place);
		_os_.marshal(faceplace);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		place.unmarshal(_os_);
		faceplace.unmarshal(_os_);
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BHeroMove) {
			BHeroMove _o_ = (BHeroMove)_o1_;
			if (herokey != _o_.herokey) return false;
			if (!place.equals(_o_.place)) return false;
			if (!faceplace.equals(_o_.faceplace)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += place.hashCode();
		_h_ += faceplace.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(place).append(",");
		_sb_.append(faceplace).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

