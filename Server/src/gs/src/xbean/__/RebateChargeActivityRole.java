
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class RebateChargeActivityRole extends xdb.XBean implements xbean.RebateChargeActivityRole {
	private java.util.HashMap<Integer, xbean.RebateChargeActivity> activities; // key=活动id

	RebateChargeActivityRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>();
	}

	public RebateChargeActivityRole() {
		this(0, null, null);
	}

	public RebateChargeActivityRole(RebateChargeActivityRole _o_) {
		this(_o_, null, null);
	}

	RebateChargeActivityRole(xbean.RebateChargeActivityRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof RebateChargeActivityRole) assign((RebateChargeActivityRole)_o1_);
		else if (_o1_ instanceof RebateChargeActivityRole.Data) assign((RebateChargeActivityRole.Data)_o1_);
		else if (_o1_ instanceof RebateChargeActivityRole.Const) assign(((RebateChargeActivityRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(RebateChargeActivityRole _o_) {
		_o_._xdb_verify_unsafe_();
		activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>();
		for (java.util.Map.Entry<Integer, xbean.RebateChargeActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new RebateChargeActivity(_e_.getValue(), this, "activities"));
	}

	private void assign(RebateChargeActivityRole.Data _o_) {
		activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>();
		for (java.util.Map.Entry<Integer, xbean.RebateChargeActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new RebateChargeActivity(_e_.getValue(), this, "activities"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(activities.size());
		for (java.util.Map.Entry<Integer, xbean.RebateChargeActivity> _e_ : activities.entrySet())
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
				activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.RebateChargeActivity _v_ = new RebateChargeActivity(0, this, "activities");
				_v_.unmarshal(_os_);
				activities.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.RebateChargeActivityRole copy() {
		_xdb_verify_unsafe_();
		return new RebateChargeActivityRole(this);
	}

	@Override
	public xbean.RebateChargeActivityRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.RebateChargeActivityRole toBean() {
		_xdb_verify_unsafe_();
		return new RebateChargeActivityRole(this); // same as copy()
	}

	@Override
	public xbean.RebateChargeActivityRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.RebateChargeActivityRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.RebateChargeActivity> getActivities() { // key=活动id
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "activities"), activities);
	}

	@Override
	public java.util.Map<Integer, xbean.RebateChargeActivity> getActivitiesAsData() { // key=活动id
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.RebateChargeActivity> activities;
		RebateChargeActivityRole _o_ = this;
		activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>();
		for (java.util.Map.Entry<Integer, xbean.RebateChargeActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new RebateChargeActivity.Data(_e_.getValue()));
		return activities;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		RebateChargeActivityRole _o_ = null;
		if ( _o1_ instanceof RebateChargeActivityRole ) _o_ = (RebateChargeActivityRole)_o1_;
		else if ( _o1_ instanceof RebateChargeActivityRole.Const ) _o_ = ((RebateChargeActivityRole.Const)_o1_).nThis();
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

	private class Const implements xbean.RebateChargeActivityRole {
		RebateChargeActivityRole nThis() {
			return RebateChargeActivityRole.this;
		}

		@Override
		public xbean.RebateChargeActivityRole copy() {
			return RebateChargeActivityRole.this.copy();
		}

		@Override
		public xbean.RebateChargeActivityRole toData() {
			return RebateChargeActivityRole.this.toData();
		}

		public xbean.RebateChargeActivityRole toBean() {
			return RebateChargeActivityRole.this.toBean();
		}

		@Override
		public xbean.RebateChargeActivityRole toDataIf() {
			return RebateChargeActivityRole.this.toDataIf();
		}

		public xbean.RebateChargeActivityRole toBeanIf() {
			return RebateChargeActivityRole.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.RebateChargeActivity> getActivities() { // key=活动id
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(activities);
		}

		@Override
		public java.util.Map<Integer, xbean.RebateChargeActivity> getActivitiesAsData() { // key=活动id
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.RebateChargeActivity> activities;
			RebateChargeActivityRole _o_ = RebateChargeActivityRole.this;
			activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>();
			for (java.util.Map.Entry<Integer, xbean.RebateChargeActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new RebateChargeActivity.Data(_e_.getValue()));
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
			return RebateChargeActivityRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return RebateChargeActivityRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return RebateChargeActivityRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return RebateChargeActivityRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return RebateChargeActivityRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return RebateChargeActivityRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return RebateChargeActivityRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return RebateChargeActivityRole.this.hashCode();
		}

		@Override
		public String toString() {
			return RebateChargeActivityRole.this.toString();
		}

	}

	public static final class Data implements xbean.RebateChargeActivityRole {
		private java.util.HashMap<Integer, xbean.RebateChargeActivity> activities; // key=活动id

		public Data() {
			activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>();
		}

		Data(xbean.RebateChargeActivityRole _o1_) {
			if (_o1_ instanceof RebateChargeActivityRole) assign((RebateChargeActivityRole)_o1_);
			else if (_o1_ instanceof RebateChargeActivityRole.Data) assign((RebateChargeActivityRole.Data)_o1_);
			else if (_o1_ instanceof RebateChargeActivityRole.Const) assign(((RebateChargeActivityRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(RebateChargeActivityRole _o_) {
			activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>();
			for (java.util.Map.Entry<Integer, xbean.RebateChargeActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new RebateChargeActivity.Data(_e_.getValue()));
		}

		private void assign(RebateChargeActivityRole.Data _o_) {
			activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>();
			for (java.util.Map.Entry<Integer, xbean.RebateChargeActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new RebateChargeActivity.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(activities.size());
			for (java.util.Map.Entry<Integer, xbean.RebateChargeActivity> _e_ : activities.entrySet())
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
					activities = new java.util.HashMap<Integer, xbean.RebateChargeActivity>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.RebateChargeActivity _v_ = xbean.Pod.newRebateChargeActivityData();
					_v_.unmarshal(_os_);
					activities.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.RebateChargeActivityRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.RebateChargeActivityRole toData() {
			return new Data(this);
		}

		public xbean.RebateChargeActivityRole toBean() {
			return new RebateChargeActivityRole(this, null, null);
		}

		@Override
		public xbean.RebateChargeActivityRole toDataIf() {
			return this;
		}

		public xbean.RebateChargeActivityRole toBeanIf() {
			return new RebateChargeActivityRole(this, null, null);
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
		public java.util.Map<Integer, xbean.RebateChargeActivity> getActivities() { // key=活动id
			return activities;
		}

		@Override
		public java.util.Map<Integer, xbean.RebateChargeActivity> getActivitiesAsData() { // key=活动id
			return activities;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof RebateChargeActivityRole.Data)) return false;
			RebateChargeActivityRole.Data _o_ = (RebateChargeActivityRole.Data) _o1_;
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
