
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class RebateChargeActivity extends xdb.XBean implements xbean.RebateChargeActivity {
	private java.util.HashMap<Integer, Integer> awardinfo; // key=rmb valeu=num

	RebateChargeActivity(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		awardinfo = new java.util.HashMap<Integer, Integer>();
	}

	public RebateChargeActivity() {
		this(0, null, null);
	}

	public RebateChargeActivity(RebateChargeActivity _o_) {
		this(_o_, null, null);
	}

	RebateChargeActivity(xbean.RebateChargeActivity _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof RebateChargeActivity) assign((RebateChargeActivity)_o1_);
		else if (_o1_ instanceof RebateChargeActivity.Data) assign((RebateChargeActivity.Data)_o1_);
		else if (_o1_ instanceof RebateChargeActivity.Const) assign(((RebateChargeActivity.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(RebateChargeActivity _o_) {
		_o_._xdb_verify_unsafe_();
		awardinfo = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.awardinfo.entrySet())
			awardinfo.put(_e_.getKey(), _e_.getValue());
	}

	private void assign(RebateChargeActivity.Data _o_) {
		awardinfo = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.awardinfo.entrySet())
			awardinfo.put(_e_.getKey(), _e_.getValue());
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(awardinfo.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : awardinfo.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				awardinfo = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				awardinfo.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.RebateChargeActivity copy() {
		_xdb_verify_unsafe_();
		return new RebateChargeActivity(this);
	}

	@Override
	public xbean.RebateChargeActivity toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.RebateChargeActivity toBean() {
		_xdb_verify_unsafe_();
		return new RebateChargeActivity(this); // same as copy()
	}

	@Override
	public xbean.RebateChargeActivity toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.RebateChargeActivity toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, Integer> getAwardinfo() { // key=rmb valeu=num
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "awardinfo"), awardinfo);
	}

	@Override
	public java.util.Map<Integer, Integer> getAwardinfoAsData() { // key=rmb valeu=num
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> awardinfo;
		RebateChargeActivity _o_ = this;
		awardinfo = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.awardinfo.entrySet())
			awardinfo.put(_e_.getKey(), _e_.getValue());
		return awardinfo;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		RebateChargeActivity _o_ = null;
		if ( _o1_ instanceof RebateChargeActivity ) _o_ = (RebateChargeActivity)_o1_;
		else if ( _o1_ instanceof RebateChargeActivity.Const ) _o_ = ((RebateChargeActivity.Const)_o1_).nThis();
		else return false;
		if (!awardinfo.equals(_o_.awardinfo)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += awardinfo.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(awardinfo);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("awardinfo"));
		return lb;
	}

	private class Const implements xbean.RebateChargeActivity {
		RebateChargeActivity nThis() {
			return RebateChargeActivity.this;
		}

		@Override
		public xbean.RebateChargeActivity copy() {
			return RebateChargeActivity.this.copy();
		}

		@Override
		public xbean.RebateChargeActivity toData() {
			return RebateChargeActivity.this.toData();
		}

		public xbean.RebateChargeActivity toBean() {
			return RebateChargeActivity.this.toBean();
		}

		@Override
		public xbean.RebateChargeActivity toDataIf() {
			return RebateChargeActivity.this.toDataIf();
		}

		public xbean.RebateChargeActivity toBeanIf() {
			return RebateChargeActivity.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, Integer> getAwardinfo() { // key=rmb valeu=num
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(awardinfo);
		}

		@Override
		public java.util.Map<Integer, Integer> getAwardinfoAsData() { // key=rmb valeu=num
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> awardinfo;
			RebateChargeActivity _o_ = RebateChargeActivity.this;
			awardinfo = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.awardinfo.entrySet())
				awardinfo.put(_e_.getKey(), _e_.getValue());
			return awardinfo;
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
			return RebateChargeActivity.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return RebateChargeActivity.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return RebateChargeActivity.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return RebateChargeActivity.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return RebateChargeActivity.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return RebateChargeActivity.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return RebateChargeActivity.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return RebateChargeActivity.this.hashCode();
		}

		@Override
		public String toString() {
			return RebateChargeActivity.this.toString();
		}

	}

	public static final class Data implements xbean.RebateChargeActivity {
		private java.util.HashMap<Integer, Integer> awardinfo; // key=rmb valeu=num

		public Data() {
			awardinfo = new java.util.HashMap<Integer, Integer>();
		}

		Data(xbean.RebateChargeActivity _o1_) {
			if (_o1_ instanceof RebateChargeActivity) assign((RebateChargeActivity)_o1_);
			else if (_o1_ instanceof RebateChargeActivity.Data) assign((RebateChargeActivity.Data)_o1_);
			else if (_o1_ instanceof RebateChargeActivity.Const) assign(((RebateChargeActivity.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(RebateChargeActivity _o_) {
			awardinfo = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.awardinfo.entrySet())
				awardinfo.put(_e_.getKey(), _e_.getValue());
		}

		private void assign(RebateChargeActivity.Data _o_) {
			awardinfo = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.awardinfo.entrySet())
				awardinfo.put(_e_.getKey(), _e_.getValue());
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(awardinfo.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : awardinfo.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					awardinfo = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					awardinfo.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.RebateChargeActivity copy() {
			return new Data(this);
		}

		@Override
		public xbean.RebateChargeActivity toData() {
			return new Data(this);
		}

		public xbean.RebateChargeActivity toBean() {
			return new RebateChargeActivity(this, null, null);
		}

		@Override
		public xbean.RebateChargeActivity toDataIf() {
			return this;
		}

		public xbean.RebateChargeActivity toBeanIf() {
			return new RebateChargeActivity(this, null, null);
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
		public java.util.Map<Integer, Integer> getAwardinfo() { // key=rmb valeu=num
			return awardinfo;
		}

		@Override
		public java.util.Map<Integer, Integer> getAwardinfoAsData() { // key=rmb valeu=num
			return awardinfo;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof RebateChargeActivity.Data)) return false;
			RebateChargeActivity.Data _o_ = (RebateChargeActivity.Data) _o1_;
			if (!awardinfo.equals(_o_.awardinfo)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += awardinfo.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(awardinfo);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
