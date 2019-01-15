
package chuhan.gsp.play.tanxian;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class stagetxall implements Marshal {
	public java.util.HashMap<Integer,chuhan.gsp.play.tanxian.teamtanxian> teamallmap; // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
	public java.util.HashMap<Integer,chuhan.gsp.play.tanxian.stagetanxian> stagetxallmap; // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表

	public stagetxall() {
		teamallmap = new java.util.HashMap<Integer,chuhan.gsp.play.tanxian.teamtanxian>();
		stagetxallmap = new java.util.HashMap<Integer,chuhan.gsp.play.tanxian.stagetanxian>();
	}

	public stagetxall(java.util.HashMap<Integer,chuhan.gsp.play.tanxian.teamtanxian> _teamallmap_, java.util.HashMap<Integer,chuhan.gsp.play.tanxian.stagetanxian> _stagetxallmap_) {
		this.teamallmap = _teamallmap_;
		this.stagetxallmap = _stagetxallmap_;
	}

	public final boolean _validator_() {
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.tanxian.teamtanxian> _e_ : teamallmap.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.tanxian.stagetanxian> _e_ : stagetxallmap.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.compact_uint32(teamallmap.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.tanxian.teamtanxian> _e_ : teamallmap.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(stagetxallmap.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.play.tanxian.stagetanxian> _e_ : stagetxallmap.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.play.tanxian.teamtanxian _v_ = new chuhan.gsp.play.tanxian.teamtanxian();
			_v_.unmarshal(_os_);
			teamallmap.put(_k_, _v_);
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.play.tanxian.stagetanxian _v_ = new chuhan.gsp.play.tanxian.stagetanxian();
			_v_.unmarshal(_os_);
			stagetxallmap.put(_k_, _v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof stagetxall) {
			stagetxall _o_ = (stagetxall)_o1_;
			if (!teamallmap.equals(_o_.teamallmap)) return false;
			if (!stagetxallmap.equals(_o_.stagetxallmap)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += teamallmap.hashCode();
		_h_ += stagetxallmap.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(teamallmap).append(",");
		_sb_.append(stagetxallmap).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

