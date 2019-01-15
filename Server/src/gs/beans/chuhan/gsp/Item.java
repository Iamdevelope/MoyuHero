
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class Item implements Marshal {
	public final static int BIND = 0x00000001; // 不可交易给玩家，不可卖店
	public final static int YUANBAO = 0x0000002; // 用元宝购买而来
	public final static int ONSTALL = 0x0000004; // 摆摊出售中
	public final static int ONEQUIP = 0x0000008; // 装备
	public final static int CANNOTONSTALL = 0x10; // 不能卖店
	public final static int LOCK = 0x0000020; // 锁定

	public int id; // 物品id
	public int key; // key
	public short timer; // 每天使用次数
	public short number; // 数量
	public com.goldhuman.Common.Octets extdata; // 额外数据

	public Item() {
		extdata = new com.goldhuman.Common.Octets();
	}

	public Item(int _id_, int _key_, short _timer_, short _number_, com.goldhuman.Common.Octets _extdata_) {
		this.id = _id_;
		this.key = _key_;
		this.timer = _timer_;
		this.number = _number_;
		this.extdata = _extdata_;
	}

	public final boolean _validator_() {
		if (id < 1) return false;
		if (key < 1) return false;
		if (number < 1) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(id);
		_os_.marshal(key);
		_os_.marshal(timer);
		_os_.marshal(number);
		_os_.marshal(extdata);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		id = _os_.unmarshal_int();
		key = _os_.unmarshal_int();
		timer = _os_.unmarshal_short();
		number = _os_.unmarshal_short();
		extdata = _os_.unmarshal_Octets();
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Item) {
			Item _o_ = (Item)_o1_;
			if (id != _o_.id) return false;
			if (key != _o_.key) return false;
			if (timer != _o_.timer) return false;
			if (number != _o_.number) return false;
			if (!extdata.equals(_o_.extdata)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += id;
		_h_ += key;
		_h_ += timer;
		_h_ += number;
		_h_ += extdata.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id).append(",");
		_sb_.append(key).append(",");
		_sb_.append(timer).append(",");
		_sb_.append(number).append(",");
		_sb_.append("B").append(extdata.size()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

