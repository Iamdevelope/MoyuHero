
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class ChargeActivityRole extends xdb.XBean implements xbean.ChargeActivityRole {
	private java.util.HashMap<Integer, xbean.ChargeActivity> activities; // key=活动id

	ChargeActivityRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		activities = new java.util.HashMap<Integer, xbean.ChargeActivity>();
	}

	public ChargeActivityRole() {
		this(0, null, null);
	}

	public ChargeActivityRole(ChargeActivityRole _o_) {
		this(_o_, null, null);
	}

	ChargeActivityRole(xbean.ChargeActivityRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof ChargeActivityRole) assign((ChargeActivityRole)_o1_);
		else if (_o1_ instanceof ChargeActivityRole.Data) assign((ChargeActivityRole.Data)_o1_);
		else if (_o1_ instanceof ChargeActivityRole.Const) assign(((ChargeActivityRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(ChargeActivityRole _o_) {
		_o_._xdb_verify_unsafe_();
		activities = new java.util.HashMap<Integer, xbean.ChargeActivity>();
		for (java.util.Map.Entry<Integer, xbean.ChargeActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new ChargeActivity(_e_.getValue(), this, "activities"));
	}

	private void assign(ChargeActivityRole.Data _o_) {
		activities = new java.util.HashMap<Integer, xbean.ChargeActivity>();
		for (java.util.Map.Entry<Integer, xbean.ChargeActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new ChargeActivity(_e_.getValue(), this, "activities"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(activities.size());
		for (java.util.Map.Entry<Integer, xbean.ChargeActivity> _e_ : activities.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				activities = new java.util.HashMap<Integer, xbean.ChargeActivity>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.ChargeActivity _v_ = new ChargeActivity(0, this, "activities");
				_v_.unmarshal(_os_);
				activities.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.ChargeActivityRole copy() {
		_xdb_verify_unsafe_();
		return new ChargeActivityRole(this);
	}

	@Override
	public xbean.ChargeActivityRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ChargeActivityRole toBean() {
		_xdb_verify_unsafe_();
		return new ChargeActivityRole(this); // same as copy()
	}

	@Override
	public xbean.ChargeActivityRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ChargeActivityRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.ChargeActivity> getActivities() { // key=活动id
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "activities"), activities);
	}

	@Override
	public java.util.Map<Integer, xbean.ChargeActivity> getActivitiesAsData() { // key=活动id
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.ChargeActivity> activities;
		ChargeActivityRole _o_ = this;
		activities = new java.util.HashMap<Integer, xbean.ChargeActivity>();
		for (java.util.Map.Entry<Integer, xbean.ChargeActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new ChargeActivity.Data(_e_.getValue()));
		return activities;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		ChargeActivityRole _o_ = null;
		if ( _o1_ instanceof ChargeActivityRole ) _o_ = (ChargeActivityRole)_o1_;
		else if ( _o1_ instanceof ChargeActivityRole.Const ) _o_ = ((ChargeActivityRole.Const)_o1_).nThis();
		else return false;
		if (!activities.equals(_o_.activities)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += activities.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(activities);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("activities"));
		return lb;
	}

	private class Const implements xbean.ChargeActivityRole {
		ChargeActivityRole nThis() {
			return ChargeActivityRole.this;
		}

		@Override
		public xbean.ChargeActivityRole copy() {
			return ChargeActivityRole.this.copy();
		}

		@Override
		public xbean.ChargeActivityRole toData() {
			return ChargeActivityRole.this.toData();
		}

		public xbean.ChargeActivityRole toBean() {
			return ChargeActivityRole.this.toBean();
		}

		@Override
		public xbean.ChargeActivityRole toDataIf() {
			return ChargeActivityRole.this.toDataIf();
		}

		public xbean.ChargeActivityRole toBeanIf() {
			return ChargeActivityRole.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.ChargeActivity> getActivities() { // key=活动id
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(activities);
		}

		@Override
		public java.util.Map<Integer, xbean.ChargeActivity> getActivitiesAsData() { // key=活动id
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.ChargeActivity> activities;
			ChargeActivityRole _o_ = ChargeActivityRole.this;
			activities = new java.util.HashMap<Integer, xbean.ChargeActivity>();
			for (java.util.Map.Entry<Integer, xbean.ChargeActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new ChargeActivity.Data(_e_.getValue()));
			return activities;
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
			return ChargeActivityRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return ChargeActivityRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return ChargeActivityRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return ChargeActivityRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return ChargeActivityRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return ChargeActivityRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return ChargeActivityRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return ChargeActivityRole.this.hashCode();
		}

		@Override
		public String toString() {
			return ChargeActivityRole.this.toString();
		}

	}

	public static final class Data implements xbean.ChargeActivityRole {
		private java.util.HashMap<Integer, xbean.ChargeActivity> activities; // key=活动id

		public Data() {
			activities = new java.util.HashMap<Integer, xbean.ChargeActivity>();
		}

		Data(xbean.ChargeActivityRole _o1_) {
			if (_o1_ instanceof ChargeActivityRole) assign((ChargeActivityRole)_o1_);
			else if (_o1_ instanceof ChargeActivityRole.Data) assign((ChargeActivityRole.Data)_o1_);
			else if (_o1_ instanceof ChargeActivityRole.Const) assign(((ChargeActivityRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(ChargeActivityRole _o_) {
			activities = new java.util.HashMap<Integer, xbean.ChargeActivity>();
			for (java.util.Map.Entry<Integer, xbean.ChargeActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new ChargeActivity.Data(_e_.getValue()));
		}

		private void assign(ChargeActivityRole.Data _o_) {
			activities = new java.util.HashMap<Integer, xbean.ChargeActivity>();
			for (java.util.Map.Entry<Integer, xbean.ChargeActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new ChargeActivity.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(activities.size());
			for (java.util.Map.Entry<Integer, xbean.ChargeActivity> _e_ : activities.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					activities = new java.util.HashMap<Integer, xbean.ChargeActivity>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.ChargeActivity _v_ = xbean.Pod.newChargeActivityData();
					_v_.unmarshal(_os_);
					activities.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.ChargeActivityRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.ChargeActivityRole toData() {
			return new Data(this);
		}

		public xbean.ChargeActivityRole toBean() {
			return new ChargeActivityRole(this, null, null);
		}

		@Override
		public xbean.ChargeActivityRole toDataIf() {
			return this;
		}

		public xbean.ChargeActivityRole toBeanIf() {
			return new ChargeActivityRole(this, null, null);
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
		public java.util.Map<Integer, xbean.ChargeActivity> getActivities() { // key=活动id
			return activities;
		}

		@Override
		public java.util.Map<Integer, xbean.ChargeActivity> getActivitiesAsData() { // key=活动id
			return activities;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof ChargeActivityRole.Data)) return false;
			ChargeActivityRole.Data _o_ = (ChargeActivityRole.Data) _o1_;
			if (!activities.equals(_o_.activities)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += activities.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(activities);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
