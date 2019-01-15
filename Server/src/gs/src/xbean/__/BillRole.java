
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class BillRole extends xdb.XBean implements xbean.BillRole {
	private java.util.HashMap<Long, xbean.BillData> bills; // 
	private int firstcharge; // 是否已首充

	BillRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		bills = new java.util.HashMap<Long, xbean.BillData>();
	}

	public BillRole() {
		this(0, null, null);
	}

	public BillRole(BillRole _o_) {
		this(_o_, null, null);
	}

	BillRole(xbean.BillRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof BillRole) assign((BillRole)_o1_);
		else if (_o1_ instanceof BillRole.Data) assign((BillRole.Data)_o1_);
		else if (_o1_ instanceof BillRole.Const) assign(((BillRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(BillRole _o_) {
		_o_._xdb_verify_unsafe_();
		bills = new java.util.HashMap<Long, xbean.BillData>();
		for (java.util.Map.Entry<Long, xbean.BillData> _e_ : _o_.bills.entrySet())
			bills.put(_e_.getKey(), new BillData(_e_.getValue(), this, "bills"));
		firstcharge = _o_.firstcharge;
	}

	private void assign(BillRole.Data _o_) {
		bills = new java.util.HashMap<Long, xbean.BillData>();
		for (java.util.Map.Entry<Long, xbean.BillData> _e_ : _o_.bills.entrySet())
			bills.put(_e_.getKey(), new BillData(_e_.getValue(), this, "bills"));
		firstcharge = _o_.firstcharge;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(bills.size());
		for (java.util.Map.Entry<Long, xbean.BillData> _e_ : bills.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		_os_.marshal(firstcharge);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				bills = new java.util.HashMap<Long, xbean.BillData>(size * 2);
			}
			for (; size > 0; --size)
			{
				long _k_ = 0;
				_k_ = _os_.unmarshal_long();
				xbean.BillData _v_ = new BillData(0, this, "bills");
				_v_.unmarshal(_os_);
				bills.put(_k_, _v_);
			}
		}
		firstcharge = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.BillRole copy() {
		_xdb_verify_unsafe_();
		return new BillRole(this);
	}

	@Override
	public xbean.BillRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BillRole toBean() {
		_xdb_verify_unsafe_();
		return new BillRole(this); // same as copy()
	}

	@Override
	public xbean.BillRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BillRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Long, xbean.BillData> getBills() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "bills"), bills);
	}

	@Override
	public java.util.Map<Long, xbean.BillData> getBillsAsData() { // 
		_xdb_verify_unsafe_();
		java.util.Map<Long, xbean.BillData> bills;
		BillRole _o_ = this;
		bills = new java.util.HashMap<Long, xbean.BillData>();
		for (java.util.Map.Entry<Long, xbean.BillData> _e_ : _o_.bills.entrySet())
			bills.put(_e_.getKey(), new BillData.Data(_e_.getValue()));
		return bills;
	}

	@Override
	public int getFirstcharge() { // 是否已首充
		_xdb_verify_unsafe_();
		return firstcharge;
	}

	@Override
	public void setFirstcharge(int _v_) { // 是否已首充
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "firstcharge") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, firstcharge) {
					public void rollback() { firstcharge = _xdb_saved; }
				};}});
		firstcharge = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		BillRole _o_ = null;
		if ( _o1_ instanceof BillRole ) _o_ = (BillRole)_o1_;
		else if ( _o1_ instanceof BillRole.Const ) _o_ = ((BillRole.Const)_o1_).nThis();
		else return false;
		if (!bills.equals(_o_.bills)) return false;
		if (firstcharge != _o_.firstcharge) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += bills.hashCode();
		_h_ += firstcharge;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(bills);
		_sb_.append(",");
		_sb_.append(firstcharge);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("bills"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("firstcharge"));
		return lb;
	}

	private class Const implements xbean.BillRole {
		BillRole nThis() {
			return BillRole.this;
		}

		@Override
		public xbean.BillRole copy() {
			return BillRole.this.copy();
		}

		@Override
		public xbean.BillRole toData() {
			return BillRole.this.toData();
		}

		public xbean.BillRole toBean() {
			return BillRole.this.toBean();
		}

		@Override
		public xbean.BillRole toDataIf() {
			return BillRole.this.toDataIf();
		}

		public xbean.BillRole toBeanIf() {
			return BillRole.this.toBeanIf();
		}

		@Override
		public java.util.Map<Long, xbean.BillData> getBills() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(bills);
		}

		@Override
		public java.util.Map<Long, xbean.BillData> getBillsAsData() { // 
			_xdb_verify_unsafe_();
			java.util.Map<Long, xbean.BillData> bills;
			BillRole _o_ = BillRole.this;
			bills = new java.util.HashMap<Long, xbean.BillData>();
			for (java.util.Map.Entry<Long, xbean.BillData> _e_ : _o_.bills.entrySet())
				bills.put(_e_.getKey(), new BillData.Data(_e_.getValue()));
			return bills;
		}

		@Override
		public int getFirstcharge() { // 是否已首充
			_xdb_verify_unsafe_();
			return firstcharge;
		}

		@Override
		public void setFirstcharge(int _v_) { // 是否已首充
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean toConst() {
			_xdb_verify_unsafe_();
			return this;
		}

		@Override
		public boolean isConst() {
			_xdb_verify_unsafe_();
			return true;
		}

		@Override
		public boolean isData() {
			return BillRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return BillRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return BillRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return BillRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return BillRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return BillRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return BillRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return BillRole.this.hashCode();
		}

		@Override
		public String toString() {
			return BillRole.this.toString();
		}

	}

	public static final class Data implements xbean.BillRole {
		private java.util.HashMap<Long, xbean.BillData> bills; // 
		private int firstcharge; // 是否已首充

		public Data() {
			bills = new java.util.HashMap<Long, xbean.BillData>();
		}

		Data(xbean.BillRole _o1_) {
			if (_o1_ instanceof BillRole) assign((BillRole)_o1_);
			else if (_o1_ instanceof BillRole.Data) assign((BillRole.Data)_o1_);
			else if (_o1_ instanceof BillRole.Const) assign(((BillRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(BillRole _o_) {
			bills = new java.util.HashMap<Long, xbean.BillData>();
			for (java.util.Map.Entry<Long, xbean.BillData> _e_ : _o_.bills.entrySet())
				bills.put(_e_.getKey(), new BillData.Data(_e_.getValue()));
			firstcharge = _o_.firstcharge;
		}

		private void assign(BillRole.Data _o_) {
			bills = new java.util.HashMap<Long, xbean.BillData>();
			for (java.util.Map.Entry<Long, xbean.BillData> _e_ : _o_.bills.entrySet())
				bills.put(_e_.getKey(), new BillData.Data(_e_.getValue()));
			firstcharge = _o_.firstcharge;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(bills.size());
			for (java.util.Map.Entry<Long, xbean.BillData> _e_ : bills.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			_os_.marshal(firstcharge);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					bills = new java.util.HashMap<Long, xbean.BillData>(size * 2);
				}
				for (; size > 0; --size)
				{
					long _k_ = 0;
					_k_ = _os_.unmarshal_long();
					xbean.BillData _v_ = xbean.Pod.newBillDataData();
					_v_.unmarshal(_os_);
					bills.put(_k_, _v_);
				}
			}
			firstcharge = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.BillRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.BillRole toData() {
			return new Data(this);
		}

		public xbean.BillRole toBean() {
			return new BillRole(this, null, null);
		}

		@Override
		public xbean.BillRole toDataIf() {
			return this;
		}

		public xbean.BillRole toBeanIf() {
			return new BillRole(this, null, null);
		}

		// xdb.Bean interface. Data Unsupported
		public boolean xdbManaged() { throw new UnsupportedOperationException(); }
		public xdb.Bean xdbParent() { throw new UnsupportedOperationException(); }
		public String xdbVarname()  { throw new UnsupportedOperationException(); }
		public Long    xdbObjId()   { throw new UnsupportedOperationException(); }
		public xdb.Bean toConst()   { throw new UnsupportedOperationException(); }
		public boolean isConst()    { return false; }
		public boolean isData()     { return true; }

		@Override
		public java.util.Map<Long, xbean.BillData> getBills() { // 
			return bills;
		}

		@Override
		public java.util.Map<Long, xbean.BillData> getBillsAsData() { // 
			return bills;
		}

		@Override
		public int getFirstcharge() { // 是否已首充
			return firstcharge;
		}

		@Override
		public void setFirstcharge(int _v_) { // 是否已首充
			firstcharge = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof BillRole.Data)) return false;
			BillRole.Data _o_ = (BillRole.Data) _o1_;
			if (!bills.equals(_o_.bills)) return false;
			if (firstcharge != _o_.firstcharge) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += bills.hashCode();
			_h_ += firstcharge;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(bills);
			_sb_.append(",");
			_sb_.append(firstcharge);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
