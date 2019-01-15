
package chuhan.gsp.friends;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class FriendInfo implements Marshal {
	public long roleid;
	public java.lang.String name;
	public int level; // 等级
	public long lastlogintime; // 上次登录时间
	public int ladderrankid; // 天梯排名
	public int bloodlv; // 血战关卡等级
	public byte istili; // 是否赠送过体力

	public FriendInfo() {
		name = "";
	}

	public FriendInfo(long _roleid_, java.lang.String _name_, int _level_, long _lastlogintime_, int _ladderrankid_, int _bloodlv_, byte _istili_) {
		this.roleid = _roleid_;
		this.name = _name_;
		this.level = _level_;
		this.lastlogintime = _lastlogintime_;
		this.ladderrankid = _ladderrankid_;
		this.bloodlv = _bloodlv_;
		this.istili = _istili_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(roleid);
		_os_.marshal(name, "UTF-16LE");
		_os_.marshal(level);
		_os_.marshal(lastlogintime);
		_os_.marshal(ladderrankid);
		_os_.marshal(bloodlv);
		_os_.marshal(istili);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		name = _os_.unmarshal_String("UTF-16LE");
		level = _os_.unmarshal_int();
		lastlogintime = _os_.unmarshal_long();
		ladderrankid = _os_.unmarshal_int();
		bloodlv = _os_.unmarshal_int();
		istili = _os_.unmarshal_byte();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof FriendInfo) {
			FriendInfo _o_ = (FriendInfo)_o1_;
			if (roleid != _o_.roleid) return false;
			if (!name.equals(_o_.name)) return false;
			if (level != _o_.level) return false;
			if (lastlogintime != _o_.lastlogintime) return false;
			if (ladderrankid != _o_.ladderrankid) return false;
			if (bloodlv != _o_.bloodlv) return false;
			if (istili != _o_.istili) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += name.hashCode();
		_h_ += level;
		_h_ += (int)lastlogintime;
		_h_ += ladderrankid;
		_h_ += bloodlv;
		_h_ += (int)istili;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append("T").append(name.length()).append(",");
		_sb_.append(level).append(",");
		_sb_.append(lastlogintime).append(",");
		_sb_.append(ladderrankid).append(",");
		_sb_.append(bloodlv).append(",");
		_sb_.append(istili).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

