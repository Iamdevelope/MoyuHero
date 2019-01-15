
package chuhan.gsp.battle;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class BattleResult implements Marshal {
	public byte winround; // -3=3回合主方败，-1=1回合主方败，3=3回合主方胜。。。
	public int addmoney; // 添加的钱，可能为负
	public int addexp; // 添加的人物经验
	public int itemkey; // 获得的物品key
	public short itemid; // 获得的物品id
	public int num; // 获得的数量
	public java.lang.String extinfo; // 额外信息

	public BattleResult() {
		extinfo = "";
	}

	public BattleResult(byte _winround_, int _addmoney_, int _addexp_, int _itemkey_, short _itemid_, int _num_, java.lang.String _extinfo_) {
		this.winround = _winround_;
		this.addmoney = _addmoney_;
		this.addexp = _addexp_;
		this.itemkey = _itemkey_;
		this.itemid = _itemid_;
		this.num = _num_;
		this.extinfo = _extinfo_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(winround);
		_os_.marshal(addmoney);
		_os_.marshal(addexp);
		_os_.marshal(itemkey);
		_os_.marshal(itemid);
		_os_.marshal(num);
		_os_.marshal(extinfo, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		winround = _os_.unmarshal_byte();
		addmoney = _os_.unmarshal_int();
		addexp = _os_.unmarshal_int();
		itemkey = _os_.unmarshal_int();
		itemid = _os_.unmarshal_short();
		num = _os_.unmarshal_int();
		extinfo = _os_.unmarshal_String("UTF-16LE");
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BattleResult) {
			BattleResult _o_ = (BattleResult)_o1_;
			if (winround != _o_.winround) return false;
			if (addmoney != _o_.addmoney) return false;
			if (addexp != _o_.addexp) return false;
			if (itemkey != _o_.itemkey) return false;
			if (itemid != _o_.itemid) return false;
			if (num != _o_.num) return false;
			if (!extinfo.equals(_o_.extinfo)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)winround;
		_h_ += addmoney;
		_h_ += addexp;
		_h_ += itemkey;
		_h_ += itemid;
		_h_ += num;
		_h_ += extinfo.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(winround).append(",");
		_sb_.append(addmoney).append(",");
		_sb_.append(addexp).append(",");
		_sb_.append(itemkey).append(",");
		_sb_.append(itemid).append(",");
		_sb_.append(num).append(",");
		_sb_.append("T").append(extinfo.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

