
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 抽奖 by yanglk
*/
public class Lotty implements Marshal , Comparable<Lotty>{
	public int freetime; // 免费抽奖的剩余时间（秒）
	public int firstget; // 首抽是否已经完成
	public int dreamexp; // 梦想值
	public int dreamfree; // 梦想改变是否免费
	public int dream; // 梦想兑换展示
	public int normalrecruitnum; // 普通招募累计次数
	public int normalrecruittime; // 最后普通招募剩余时间（秒）
	public int toprecruitnum; // 顶级招募累计次数
	public int toprecruittime; // 最后顶级招募剩余时间（秒）
	public int toprecruitheronum; // 顶级招募累计次数，为招十次必得英雄准备
	public int toptentime; // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的

	public Lotty() {
		firstget = 0;
	}

	public Lotty(int _freetime_, int _firstget_, int _dreamexp_, int _dreamfree_, int _dream_, int _normalrecruitnum_, int _normalrecruittime_, int _toprecruitnum_, int _toprecruittime_, int _toprecruitheronum_, int _toptentime_) {
		this.freetime = _freetime_;
		this.firstget = _firstget_;
		this.dreamexp = _dreamexp_;
		this.dreamfree = _dreamfree_;
		this.dream = _dream_;
		this.normalrecruitnum = _normalrecruitnum_;
		this.normalrecruittime = _normalrecruittime_;
		this.toprecruitnum = _toprecruitnum_;
		this.toprecruittime = _toprecruittime_;
		this.toprecruitheronum = _toprecruitheronum_;
		this.toptentime = _toptentime_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(freetime);
		_os_.marshal(firstget);
		_os_.marshal(dreamexp);
		_os_.marshal(dreamfree);
		_os_.marshal(dream);
		_os_.marshal(normalrecruitnum);
		_os_.marshal(normalrecruittime);
		_os_.marshal(toprecruitnum);
		_os_.marshal(toprecruittime);
		_os_.marshal(toprecruitheronum);
		_os_.marshal(toptentime);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		freetime = _os_.unmarshal_int();
		firstget = _os_.unmarshal_int();
		dreamexp = _os_.unmarshal_int();
		dreamfree = _os_.unmarshal_int();
		dream = _os_.unmarshal_int();
		normalrecruitnum = _os_.unmarshal_int();
		normalrecruittime = _os_.unmarshal_int();
		toprecruitnum = _os_.unmarshal_int();
		toprecruittime = _os_.unmarshal_int();
		toprecruitheronum = _os_.unmarshal_int();
		toptentime = _os_.unmarshal_int();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Lotty) {
			Lotty _o_ = (Lotty)_o1_;
			if (freetime != _o_.freetime) return false;
			if (firstget != _o_.firstget) return false;
			if (dreamexp != _o_.dreamexp) return false;
			if (dreamfree != _o_.dreamfree) return false;
			if (dream != _o_.dream) return false;
			if (normalrecruitnum != _o_.normalrecruitnum) return false;
			if (normalrecruittime != _o_.normalrecruittime) return false;
			if (toprecruitnum != _o_.toprecruitnum) return false;
			if (toprecruittime != _o_.toprecruittime) return false;
			if (toprecruitheronum != _o_.toprecruitheronum) return false;
			if (toptentime != _o_.toptentime) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += freetime;
		_h_ += firstget;
		_h_ += dreamexp;
		_h_ += dreamfree;
		_h_ += dream;
		_h_ += normalrecruitnum;
		_h_ += normalrecruittime;
		_h_ += toprecruitnum;
		_h_ += toprecruittime;
		_h_ += toprecruitheronum;
		_h_ += toptentime;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(freetime).append(",");
		_sb_.append(firstget).append(",");
		_sb_.append(dreamexp).append(",");
		_sb_.append(dreamfree).append(",");
		_sb_.append(dream).append(",");
		_sb_.append(normalrecruitnum).append(",");
		_sb_.append(normalrecruittime).append(",");
		_sb_.append(toprecruitnum).append(",");
		_sb_.append(toprecruittime).append(",");
		_sb_.append(toprecruitheronum).append(",");
		_sb_.append(toptentime).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(Lotty _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = freetime - _o_.freetime;
		if (0 != _c_) return _c_;
		_c_ = firstget - _o_.firstget;
		if (0 != _c_) return _c_;
		_c_ = dreamexp - _o_.dreamexp;
		if (0 != _c_) return _c_;
		_c_ = dreamfree - _o_.dreamfree;
		if (0 != _c_) return _c_;
		_c_ = dream - _o_.dream;
		if (0 != _c_) return _c_;
		_c_ = normalrecruitnum - _o_.normalrecruitnum;
		if (0 != _c_) return _c_;
		_c_ = normalrecruittime - _o_.normalrecruittime;
		if (0 != _c_) return _c_;
		_c_ = toprecruitnum - _o_.toprecruitnum;
		if (0 != _c_) return _c_;
		_c_ = toprecruittime - _o_.toprecruittime;
		if (0 != _c_) return _c_;
		_c_ = toprecruitheronum - _o_.toprecruitheronum;
		if (0 != _c_) return _c_;
		_c_ = toptentime - _o_.toptentime;
		if (0 != _c_) return _c_;
		return _c_;
	}

}

