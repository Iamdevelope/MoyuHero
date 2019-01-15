
package chuhan.gsp.battle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class LadderRoleInfo implements Marshal {
	public short ladderrank; // 排名
	public long roleid;
	public java.lang.String rolename;
	public short rolelevel;
	public short addsoul;
	public byte fighttype; // 1:可挑战，2：仇敌，3：只读，4：自己
	public java.util.LinkedList<Integer> troopheros;
	public int firstminit; // 本周在第一名的时间 单位：分

	public LadderRoleInfo() {
		rolename = "";
		troopheros = new java.util.LinkedList<Integer>();
	}

	public LadderRoleInfo(short _ladderrank_, long _roleid_, java.lang.String _rolename_, short _rolelevel_, short _addsoul_, byte _fighttype_, java.util.LinkedList<Integer> _troopheros_, int _firstminit_) {
		this.ladderrank = _ladderrank_;
		this.roleid = _roleid_;
		this.rolename = _rolename_;
		this.rolelevel = _rolelevel_;
		this.addsoul = _addsoul_;
		this.fighttype = _fighttype_;
		this.troopheros = _troopheros_;
		this.firstminit = _firstminit_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(ladderrank);
		_os_.marshal(roleid);
		_os_.marshal(rolename, "UTF-16LE");
		_os_.marshal(rolelevel);
		_os_.marshal(addsoul);
		_os_.marshal(fighttype);
		_os_.compact_uint32(troopheros.size());
		for (Integer _v_ : troopheros) {
			_os_.marshal(_v_);
		}
		_os_.marshal(firstminit);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		ladderrank = _os_.unmarshal_short();
		roleid = _os_.unmarshal_long();
		rolename = _os_.unmarshal_String("UTF-16LE");
		rolelevel = _os_.unmarshal_short();
		addsoul = _os_.unmarshal_short();
		fighttype = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			troopheros.add(_v_);
		}
		firstminit = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof LadderRoleInfo) {
			LadderRoleInfo _o_ = (LadderRoleInfo)_o1_;
			if (ladderrank != _o_.ladderrank) return false;
			if (roleid != _o_.roleid) return false;
			if (!rolename.equals(_o_.rolename)) return false;
			if (rolelevel != _o_.rolelevel) return false;
			if (addsoul != _o_.addsoul) return false;
			if (fighttype != _o_.fighttype) return false;
			if (!troopheros.equals(_o_.troopheros)) return false;
			if (firstminit != _o_.firstminit) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += ladderrank;
		_h_ += (int)roleid;
		_h_ += rolename.hashCode();
		_h_ += rolelevel;
		_h_ += addsoul;
		_h_ += (int)fighttype;
		_h_ += troopheros.hashCode();
		_h_ += firstminit;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(ladderrank).append(",");
		_sb_.append(roleid).append(",");
		_sb_.append("T").append(rolename.length()).append(",");
		_sb_.append(rolelevel).append(",");
		_sb_.append(addsoul).append(",");
		_sb_.append(fighttype).append(",");
		_sb_.append(troopheros).append(",");
		_sb_.append(firstminit).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

