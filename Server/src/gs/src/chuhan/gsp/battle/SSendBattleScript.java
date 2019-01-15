
package chuhan.gsp.battle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SSendBattleScript__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SSendBattleScript extends __SSendBattleScript__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787834;

	public int getType() {
		return 787834;
	}

	public short enermyrank; // 天梯排名 0不显示
	public java.lang.String enermyname; // 敌人名称
	public byte directend; // -1不能结束，0可以结束，大于0时为中途结束，离结束的秒数（可以暂不处理，默认7s）
	public java.util.HashMap<Integer,chuhan.gsp.battle.FighterInfo> fighters; // 所有的战斗者信息
	public short hostspeed; // 主方速度
	public short guestspeed; // 客方速度
	public byte battletype; // 参考BattleType
	public java.util.LinkedList<chuhan.gsp.battle.BattleDemo> preround; // 回合前加buff流程
	public java.util.LinkedList<chuhan.gsp.battle.BattleDemo> round1; // 第一回合
	public java.util.LinkedList<chuhan.gsp.battle.BattleDemo> round2; // 第二回合，为空则没有
	public java.util.LinkedList<chuhan.gsp.battle.BattleDemo> round3; // 第三回合，为空则没有
	public chuhan.gsp.battle.BattleResult result;

	public SSendBattleScript() {
		enermyname = "";
		fighters = new java.util.HashMap<Integer,chuhan.gsp.battle.FighterInfo>();
		preround = new java.util.LinkedList<chuhan.gsp.battle.BattleDemo>();
		round1 = new java.util.LinkedList<chuhan.gsp.battle.BattleDemo>();
		round2 = new java.util.LinkedList<chuhan.gsp.battle.BattleDemo>();
		round3 = new java.util.LinkedList<chuhan.gsp.battle.BattleDemo>();
		result = new chuhan.gsp.battle.BattleResult();
	}

	public SSendBattleScript(short _enermyrank_, java.lang.String _enermyname_, byte _directend_, java.util.HashMap<Integer,chuhan.gsp.battle.FighterInfo> _fighters_, short _hostspeed_, short _guestspeed_, byte _battletype_, java.util.LinkedList<chuhan.gsp.battle.BattleDemo> _preround_, java.util.LinkedList<chuhan.gsp.battle.BattleDemo> _round1_, java.util.LinkedList<chuhan.gsp.battle.BattleDemo> _round2_, java.util.LinkedList<chuhan.gsp.battle.BattleDemo> _round3_, chuhan.gsp.battle.BattleResult _result_) {
		this.enermyrank = _enermyrank_;
		this.enermyname = _enermyname_;
		this.directend = _directend_;
		this.fighters = _fighters_;
		this.hostspeed = _hostspeed_;
		this.guestspeed = _guestspeed_;
		this.battletype = _battletype_;
		this.preround = _preround_;
		this.round1 = _round1_;
		this.round2 = _round2_;
		this.round3 = _round3_;
		this.result = _result_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.battle.FighterInfo> _e_ : fighters.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		for (chuhan.gsp.battle.BattleDemo _v_ : preround)
			if (!_v_._validator_()) return false;
		for (chuhan.gsp.battle.BattleDemo _v_ : round1)
			if (!_v_._validator_()) return false;
		for (chuhan.gsp.battle.BattleDemo _v_ : round2)
			if (!_v_._validator_()) return false;
		for (chuhan.gsp.battle.BattleDemo _v_ : round3)
			if (!_v_._validator_()) return false;
		if (!result._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(enermyrank);
		_os_.marshal(enermyname, "UTF-16LE");
		_os_.marshal(directend);
		_os_.compact_uint32(fighters.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.battle.FighterInfo> _e_ : fighters.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(hostspeed);
		_os_.marshal(guestspeed);
		_os_.marshal(battletype);
		_os_.compact_uint32(preround.size());
		for (chuhan.gsp.battle.BattleDemo _v_ : preround) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(round1.size());
		for (chuhan.gsp.battle.BattleDemo _v_ : round1) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(round2.size());
		for (chuhan.gsp.battle.BattleDemo _v_ : round2) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(round3.size());
		for (chuhan.gsp.battle.BattleDemo _v_ : round3) {
			_os_.marshal(_v_);
		}
		_os_.marshal(result);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		enermyrank = _os_.unmarshal_short();
		enermyname = _os_.unmarshal_String("UTF-16LE");
		directend = _os_.unmarshal_byte();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.battle.FighterInfo _v_ = new chuhan.gsp.battle.FighterInfo();
			_v_.unmarshal(_os_);
			fighters.put(_k_, _v_);
		}
		hostspeed = _os_.unmarshal_short();
		guestspeed = _os_.unmarshal_short();
		battletype = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.battle.BattleDemo _v_ = new chuhan.gsp.battle.BattleDemo();
			_v_.unmarshal(_os_);
			preround.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.battle.BattleDemo _v_ = new chuhan.gsp.battle.BattleDemo();
			_v_.unmarshal(_os_);
			round1.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.battle.BattleDemo _v_ = new chuhan.gsp.battle.BattleDemo();
			_v_.unmarshal(_os_);
			round2.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.battle.BattleDemo _v_ = new chuhan.gsp.battle.BattleDemo();
			_v_.unmarshal(_os_);
			round3.add(_v_);
		}
		result.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SSendBattleScript) {
			SSendBattleScript _o_ = (SSendBattleScript)_o1_;
			if (enermyrank != _o_.enermyrank) return false;
			if (!enermyname.equals(_o_.enermyname)) return false;
			if (directend != _o_.directend) return false;
			if (!fighters.equals(_o_.fighters)) return false;
			if (hostspeed != _o_.hostspeed) return false;
			if (guestspeed != _o_.guestspeed) return false;
			if (battletype != _o_.battletype) return false;
			if (!preround.equals(_o_.preround)) return false;
			if (!round1.equals(_o_.round1)) return false;
			if (!round2.equals(_o_.round2)) return false;
			if (!round3.equals(_o_.round3)) return false;
			if (!result.equals(_o_.result)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += enermyrank;
		_h_ += enermyname.hashCode();
		_h_ += (int)directend;
		_h_ += fighters.hashCode();
		_h_ += hostspeed;
		_h_ += guestspeed;
		_h_ += (int)battletype;
		_h_ += preround.hashCode();
		_h_ += round1.hashCode();
		_h_ += round2.hashCode();
		_h_ += round3.hashCode();
		_h_ += result.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(enermyrank).append(",");
		_sb_.append("T").append(enermyname.length()).append(",");
		_sb_.append(directend).append(",");
		_sb_.append(fighters).append(",");
		_sb_.append(hostspeed).append(",");
		_sb_.append(guestspeed).append(",");
		_sb_.append(battletype).append(",");
		_sb_.append(preround).append(",");
		_sb_.append(round1).append(",");
		_sb_.append(round2).append(",");
		_sb_.append(round3).append(",");
		_sb_.append(result).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

