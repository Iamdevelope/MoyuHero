
package chuhan.gsp.play.wordboss;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class bossdata implements Marshal {
	public int bossid1; // bossid(第一个守门人)
	public int bossid2; // bossid(第一个boss)
	public int bossid3; // bossid(第二个守门人)
	public int bossid4; // bossid(第二个boss)
	public int bossiskill; // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
	public java.lang.String boss1killname; // 击杀1者名称
	public java.lang.String boss2killname; // 击杀2者名称
	public int openboss; // 值为1234，代表第几个boss,没有则为-1
	public int openendtime; // 倒计时，秒
	public int shouwangzl; // 守望之灵
	public int chuanshuozs; // 传说之石

	public bossdata() {
		boss1killname = "";
		boss2killname = "";
	}

	public bossdata(int _bossid1_, int _bossid2_, int _bossid3_, int _bossid4_, int _bossiskill_, java.lang.String _boss1killname_, java.lang.String _boss2killname_, int _openboss_, int _openendtime_, int _shouwangzl_, int _chuanshuozs_) {
		this.bossid1 = _bossid1_;
		this.bossid2 = _bossid2_;
		this.bossid3 = _bossid3_;
		this.bossid4 = _bossid4_;
		this.bossiskill = _bossiskill_;
		this.boss1killname = _boss1killname_;
		this.boss2killname = _boss2killname_;
		this.openboss = _openboss_;
		this.openendtime = _openendtime_;
		this.shouwangzl = _shouwangzl_;
		this.chuanshuozs = _chuanshuozs_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(bossid1);
		_os_.marshal(bossid2);
		_os_.marshal(bossid3);
		_os_.marshal(bossid4);
		_os_.marshal(bossiskill);
		_os_.marshal(boss1killname, "UTF-16LE");
		_os_.marshal(boss2killname, "UTF-16LE");
		_os_.marshal(openboss);
		_os_.marshal(openendtime);
		_os_.marshal(shouwangzl);
		_os_.marshal(chuanshuozs);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bossid1 = _os_.unmarshal_int();
		bossid2 = _os_.unmarshal_int();
		bossid3 = _os_.unmarshal_int();
		bossid4 = _os_.unmarshal_int();
		bossiskill = _os_.unmarshal_int();
		boss1killname = _os_.unmarshal_String("UTF-16LE");
		boss2killname = _os_.unmarshal_String("UTF-16LE");
		openboss = _os_.unmarshal_int();
		openendtime = _os_.unmarshal_int();
		shouwangzl = _os_.unmarshal_int();
		chuanshuozs = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof bossdata) {
			bossdata _o_ = (bossdata)_o1_;
			if (bossid1 != _o_.bossid1) return false;
			if (bossid2 != _o_.bossid2) return false;
			if (bossid3 != _o_.bossid3) return false;
			if (bossid4 != _o_.bossid4) return false;
			if (bossiskill != _o_.bossiskill) return false;
			if (!boss1killname.equals(_o_.boss1killname)) return false;
			if (!boss2killname.equals(_o_.boss2killname)) return false;
			if (openboss != _o_.openboss) return false;
			if (openendtime != _o_.openendtime) return false;
			if (shouwangzl != _o_.shouwangzl) return false;
			if (chuanshuozs != _o_.chuanshuozs) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += bossid1;
		_h_ += bossid2;
		_h_ += bossid3;
		_h_ += bossid4;
		_h_ += bossiskill;
		_h_ += boss1killname.hashCode();
		_h_ += boss2killname.hashCode();
		_h_ += openboss;
		_h_ += openendtime;
		_h_ += shouwangzl;
		_h_ += chuanshuozs;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bossid1).append(",");
		_sb_.append(bossid2).append(",");
		_sb_.append(bossid3).append(",");
		_sb_.append(bossid4).append(",");
		_sb_.append(bossiskill).append(",");
		_sb_.append("T").append(boss1killname.length()).append(",");
		_sb_.append("T").append(boss2killname.length()).append(",");
		_sb_.append(openboss).append(",");
		_sb_.append(openendtime).append(",");
		_sb_.append(shouwangzl).append(",");
		_sb_.append(chuanshuozs).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

