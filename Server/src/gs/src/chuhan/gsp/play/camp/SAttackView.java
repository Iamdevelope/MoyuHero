
package chuhan.gsp.play.camp;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SAttackView__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SAttackView extends __SAttackView__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788650;

	public int getType() {
		return 788650;
	}

	public short mycamp;
	public int gongxun;
	public int jifen;
	public int rank;
	public int continuwin;
	public chuhan.gsp.play.camp.Enemy enemy;

	public SAttackView() {
		enemy = new chuhan.gsp.play.camp.Enemy();
	}

	public SAttackView(short _mycamp_, int _gongxun_, int _jifen_, int _rank_, int _continuwin_, chuhan.gsp.play.camp.Enemy _enemy_) {
		this.mycamp = _mycamp_;
		this.gongxun = _gongxun_;
		this.jifen = _jifen_;
		this.rank = _rank_;
		this.continuwin = _continuwin_;
		this.enemy = _enemy_;
	}

	public final boolean _validator_() {
		if (!enemy._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(mycamp);
		_os_.marshal(gongxun);
		_os_.marshal(jifen);
		_os_.marshal(rank);
		_os_.marshal(continuwin);
		_os_.marshal(enemy);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		mycamp = _os_.unmarshal_short();
		gongxun = _os_.unmarshal_int();
		jifen = _os_.unmarshal_int();
		rank = _os_.unmarshal_int();
		continuwin = _os_.unmarshal_int();
		enemy.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SAttackView) {
			SAttackView _o_ = (SAttackView)_o1_;
			if (mycamp != _o_.mycamp) return false;
			if (gongxun != _o_.gongxun) return false;
			if (jifen != _o_.jifen) return false;
			if (rank != _o_.rank) return false;
			if (continuwin != _o_.continuwin) return false;
			if (!enemy.equals(_o_.enemy)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += mycamp;
		_h_ += gongxun;
		_h_ += jifen;
		_h_ += rank;
		_h_ += continuwin;
		_h_ += enemy.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(mycamp).append(",");
		_sb_.append(gongxun).append(",");
		_sb_.append(jifen).append(",");
		_sb_.append(rank).append(",");
		_sb_.append(continuwin).append(",");
		_sb_.append(enemy).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

