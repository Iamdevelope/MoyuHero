
package chuhan.gsp.stage;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __SEndFightStageBattle__ extends xio.Protocol { }

/** 通关返回S by yanglk
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class SEndFightStageBattle extends __SEndFightStageBattle__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787943;

	public int getType() {
		return 787943;
	}

	public final static int END_OK = 1; // 成功
	public final static int END_ERROR = 2; // 失败

	public int endtype;
	public int smid; // 神秘关卡或神秘商店ID
	public int time; // 倒计时时间（秒）
	public int zhangjie; // 所属章节
	public java.util.HashMap<Integer,chuhan.gsp.stage.mohe> moheshop; // 魔盒列表
	public java.util.HashMap<Integer,chuhan.gsp.stage.smshopdata> smshop; // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）

	public SEndFightStageBattle() {
		moheshop = new java.util.HashMap<Integer,chuhan.gsp.stage.mohe>();
		smshop = new java.util.HashMap<Integer,chuhan.gsp.stage.smshopdata>();
	}

	public SEndFightStageBattle(int _endtype_, int _smid_, int _time_, int _zhangjie_, java.util.HashMap<Integer,chuhan.gsp.stage.mohe> _moheshop_, java.util.HashMap<Integer,chuhan.gsp.stage.smshopdata> _smshop_) {
		this.endtype = _endtype_;
		this.smid = _smid_;
		this.time = _time_;
		this.zhangjie = _zhangjie_;
		this.moheshop = _moheshop_;
		this.smshop = _smshop_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.mohe> _e_ : moheshop.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.smshopdata> _e_ : smshop.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(endtype);
		_os_.marshal(smid);
		_os_.marshal(time);
		_os_.marshal(zhangjie);
		_os_.compact_uint32(moheshop.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.mohe> _e_ : moheshop.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(smshop.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.smshopdata> _e_ : smshop.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		endtype = _os_.unmarshal_int();
		smid = _os_.unmarshal_int();
		time = _os_.unmarshal_int();
		zhangjie = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.stage.mohe _v_ = new chuhan.gsp.stage.mohe();
			_v_.unmarshal(_os_);
			moheshop.put(_k_, _v_);
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.stage.smshopdata _v_ = new chuhan.gsp.stage.smshopdata();
			_v_.unmarshal(_os_);
			smshop.put(_k_, _v_);
		}
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof SEndFightStageBattle) {
			SEndFightStageBattle _o_ = (SEndFightStageBattle)_o1_;
			if (endtype != _o_.endtype) return false;
			if (smid != _o_.smid) return false;
			if (time != _o_.time) return false;
			if (zhangjie != _o_.zhangjie) return false;
			if (!moheshop.equals(_o_.moheshop)) return false;
			if (!smshop.equals(_o_.smshop)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += endtype;
		_h_ += smid;
		_h_ += time;
		_h_ += zhangjie;
		_h_ += moheshop.hashCode();
		_h_ += smshop.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(endtype).append(",");
		_sb_.append(smid).append(",");
		_sb_.append(time).append(",");
		_sb_.append(zhangjie).append(",");
		_sb_.append(moheshop).append(",");
		_sb_.append(smshop).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

