
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEnterBloodEffect__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEnterBloodEffect extends __SEnterBloodEffect__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787842;

	public int getType() {
		return 787842;
	}

	public chuhan.gsp.battle.BloodBaseInfo bloodinfo;
	public short battlehard; // 战斗难度1-3，当是第一关加属性时，是昨天的战绩
	public short addstar; // 要加的星数，战斗结果用addstar除以battlehard

	public SEnterBloodEffect() {
		bloodinfo = new chuhan.gsp.battle.BloodBaseInfo();
	}

	public SEnterBloodEffect(chuhan.gsp.battle.BloodBaseInfo _bloodinfo_, short _battlehard_, short _addstar_) {
		this.bloodinfo = _bloodinfo_;
		this.battlehard = _battlehard_;
		this.addstar = _addstar_;
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
		_os_.marshal(battlehard);
		_os_.marshal(addstar);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bloodinfo.unmarshal(_os_);
		battlehard = _os_.unmarshal_short();
		addstar = _os_.unmarshal_short();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEnterBloodEffect) {
			SEnterBloodEffect _o_ = (SEnterBloodEffect)_o1_;
			if (!bloodinfo.equals(_o_.bloodinfo)) return false;
			if (battlehard != _o_.battlehard) return false;
			if (addstar != _o_.addstar) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += bloodinfo.hashCode();
		_h_ += battlehard;
		_h_ += addstar;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bloodinfo).append(",");
		_sb_.append(battlehard).append(",");
		_sb_.append(addstar).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

