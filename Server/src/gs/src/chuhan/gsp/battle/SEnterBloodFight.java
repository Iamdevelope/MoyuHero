
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEnterBloodFight__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEnterBloodFight extends __SEnterBloodFight__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787841;

	public int getType() {
		return 787841;
	}

	public chuhan.gsp.battle.BloodBaseInfo bloodinfo;
	public byte battle1; // 战斗1
	public byte battle2; // 战斗2
	public byte battle3; // 战斗3
	public byte relivetimes; // 复活次数，当大于等于0时进入战斗失败界面

	public SEnterBloodFight() {
		bloodinfo = new chuhan.gsp.battle.BloodBaseInfo();
	}

	public SEnterBloodFight(chuhan.gsp.battle.BloodBaseInfo _bloodinfo_, byte _battle1_, byte _battle2_, byte _battle3_, byte _relivetimes_) {
		this.bloodinfo = _bloodinfo_;
		this.battle1 = _battle1_;
		this.battle2 = _battle2_;
		this.battle3 = _battle3_;
		this.relivetimes = _relivetimes_;
	}

	public final boolean _validator_() {
		if (!bloodinfo._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bloodinfo);
		_os_.marshal(battle1);
		_os_.marshal(battle2);
		_os_.marshal(battle3);
		_os_.marshal(relivetimes);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bloodinfo.unmarshal(_os_);
		battle1 = _os_.unmarshal_byte();
		battle2 = _os_.unmarshal_byte();
		battle3 = _os_.unmarshal_byte();
		relivetimes = _os_.unmarshal_byte();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEnterBloodFight) {
			SEnterBloodFight _o_ = (SEnterBloodFight)_o1_;
			if (!bloodinfo.equals(_o_.bloodinfo)) return false;
			if (battle1 != _o_.battle1) return false;
			if (battle2 != _o_.battle2) return false;
			if (battle3 != _o_.battle3) return false;
			if (relivetimes != _o_.relivetimes) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += bloodinfo.hashCode();
		_h_ += (int)battle1;
		_h_ += (int)battle2;
		_h_ += (int)battle3;
		_h_ += (int)relivetimes;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bloodinfo).append(",");
		_sb_.append(battle1).append(",");
		_sb_.append(battle2).append(",");
		_sb_.append(battle3).append(",");
		_sb_.append(relivetimes).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

