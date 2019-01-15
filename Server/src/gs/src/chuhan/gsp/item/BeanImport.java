
package chuhan.gsp.item;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __BeanImport__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class BeanImport extends __BeanImport__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787533;

	public int getType() {
		return 787533;
	}

	public chuhan.gsp.item.BagTypes bagtypes;
	public chuhan.gsp.item.ShopTypes shoptypes;
	public chuhan.gsp.item.SkillItemData skillitemdata;
	public chuhan.gsp.item.EquipItemData equipitemdata;

	public BeanImport() {
		bagtypes = new chuhan.gsp.item.BagTypes();
		shoptypes = new chuhan.gsp.item.ShopTypes();
		skillitemdata = new chuhan.gsp.item.SkillItemData();
		equipitemdata = new chuhan.gsp.item.EquipItemData();
	}

	public BeanImport(chuhan.gsp.item.BagTypes _bagtypes_, chuhan.gsp.item.ShopTypes _shoptypes_, chuhan.gsp.item.SkillItemData _skillitemdata_, chuhan.gsp.item.EquipItemData _equipitemdata_) {
		this.bagtypes = _bagtypes_;
		this.shoptypes = _shoptypes_;
		this.skillitemdata = _skillitemdata_;
		this.equipitemdata = _equipitemdata_;
	}

	public final boolean _validator_() {
		if (!bagtypes._validator_()) return false;
		if (!shoptypes._validator_()) return false;
		if (!skillitemdata._validator_()) return false;
		if (!equipitemdata._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(bagtypes);
		_os_.marshal(shoptypes);
		_os_.marshal(skillitemdata);
		_os_.marshal(equipitemdata);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		bagtypes.unmarshal(_os_);
		shoptypes.unmarshal(_os_);
		skillitemdata.unmarshal(_os_);
		equipitemdata.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BeanImport) {
			BeanImport _o_ = (BeanImport)_o1_;
			if (!bagtypes.equals(_o_.bagtypes)) return false;
			if (!shoptypes.equals(_o_.shoptypes)) return false;
			if (!skillitemdata.equals(_o_.skillitemdata)) return false;
			if (!equipitemdata.equals(_o_.equipitemdata)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += bagtypes.hashCode();
		_h_ += shoptypes.hashCode();
		_h_ += skillitemdata.hashCode();
		_h_ += equipitemdata.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bagtypes).append(",");
		_sb_.append(shoptypes).append(",");
		_sb_.append(skillitemdata).append(",");
		_sb_.append(equipitemdata).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(BeanImport _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = bagtypes.compareTo(_o_.bagtypes);
		if (0 != _c_) return _c_;
		_c_ = shoptypes.compareTo(_o_.shoptypes);
		if (0 != _c_) return _c_;
		_c_ = skillitemdata.compareTo(_o_.skillitemdata);
		if (0 != _c_) return _c_;
		_c_ = equipitemdata.compareTo(_o_.equipitemdata);
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

