
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class ConsumeActivityRole extends xdb.XBean implements xbean.ConsumeActivityRole {
	private java.util.HashMap<Integer, xbean.ConsumeActivity> activities; // key=活动id

	ConsumeActivityRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>();
	}

	public ConsumeActivityRole() {
		this(0, null, null);
	}

	public ConsumeActivityRole(ConsumeActivityRole _o_) {
		this(_o_, null, null);
	}

	ConsumeActivityRole(xbean.ConsumeActivityRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof ConsumeActivityRole) assign((ConsumeActivityRole)_o1_);
		else if (_o1_ instanceof ConsumeActivityRole.Data) assign((ConsumeActivityRole.Data)_o1_);
		else if (_o1_ instanceof ConsumeActivityRole.Const) assign(((ConsumeActivityRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(ConsumeActivityRole _o_) {
		_o_._xdb_verify_unsafe_();
		activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>();
		for (java.util.Map.Entry<Integer, xbean.ConsumeActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new ConsumeActivity(_e_.getValue(), this, "activities"));
	}

	private void assign(ConsumeActivityRole.Data _o_) {
		activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>();
		for (java.util.Map.Entry<Integer, xbean.ConsumeActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new ConsumeActivity(_e_.getValue(), this, "activities"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(activities.size());
		for (java.util.Map.Entry<Integer, xbean.ConsumeActivity> _e_ : activities.entrySet())
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
				activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.ConsumeActivity _v_ = new ConsumeActivity(0, this, "activities");
				_v_.unmarshal(_os_);
				activities.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.ConsumeActivityRole copy() {
		_xdb_verify_unsafe_();
		return new ConsumeActivityRole(this);
	}

	@Override
	public xbean.ConsumeActivityRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ConsumeActivityRole toBean() {
		_xdb_verify_unsafe_();
		return new ConsumeActivityRole(this); // same as copy()
	}

	@Override
	public xbean.ConsumeActivityRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ConsumeActivityRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.ConsumeActivity> getActivities() { // key=活动id
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "activities"), activities);
	}

	@Override
	public java.util.Map<Integer, xbean.ConsumeActivity> getActivitiesAsData() { // key=活动id
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.ConsumeActivity> activities;
		ConsumeActivityRole _o_ = this;
		activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>();
		for (java.util.Map.Entry<Integer, xbean.ConsumeActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new ConsumeActivity.Data(_e_.getValue()));
		return activities;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		ConsumeActivityRole _o_ = null;
		if ( _o1_ instanceof ConsumeActivityRole ) _o_ = (ConsumeActivityRole)_o1_;
		else if ( _o1_ instanceof ConsumeActivityRole.Const ) _o_ = ((ConsumeActivityRole.Const)_o1_).nThis();
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

	private class Const implements xbean.ConsumeActivityRole {
		ConsumeActivityRole nThis() {
			return ConsumeActivityRole.this;
		}

		@Override
		public xbean.ConsumeActivityRole copy() {
			return ConsumeActivityRole.this.copy();
		}

		@Override
		public xbean.ConsumeActivityRole toData() {
			return ConsumeActivityRole.this.toData();
		}

		public xbean.ConsumeActivityRole toBean() {
			return ConsumeActivityRole.this.toBean();
		}

		@Override
		public xbean.ConsumeActivityRole toDataIf() {
			return ConsumeActivityRole.this.toDataIf();
		}

		public xbean.ConsumeActivityRole toBeanIf() {
			return ConsumeActivityRole.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.ConsumeActivity> getActivities() { // key=活动id
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(activities);
		}

		@Override
		public java.util.Map<Integer, xbean.ConsumeActivity> getActivitiesAsData() { // key=活动id
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.ConsumeActivity> activities;
			ConsumeActivityRole _o_ = ConsumeActivityRole.this;
			activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>();
			for (java.util.Map.Entry<Integer, xbean.ConsumeActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new ConsumeActivity.Data(_e_.getValue()));
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
			return ConsumeActivityRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return ConsumeActivityRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return ConsumeActivityRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return ConsumeActivityRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return ConsumeActivityRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return ConsumeActivityRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return ConsumeActivityRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return ConsumeActivityRole.this.hashCode();
		}

		@Override
		public String toString() {
			return ConsumeActivityRole.this.toString();
		}

	}

	public static final class Data implements xbean.ConsumeActivityRole {
		private java.util.HashMap<Integer, xbean.ConsumeActivity> activities; // key=活动id

		public Data() {
			activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>();
		}

		Data(xbean.ConsumeActivityRole _o1_) {
			if (_o1_ instanceof ConsumeActivityRole) assign((ConsumeActivityRole)_o1_);
			else if (_o1_ instanceof ConsumeActivityRole.Data) assign((ConsumeActivityRole.Data)_o1_);
			else if (_o1_ instanceof ConsumeActivityRole.Const) assign(((ConsumeActivityRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(ConsumeActivityRole _o_) {
			activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>();
			for (java.util.Map.Entry<Integer, xbean.ConsumeActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new ConsumeActivity.Data(_e_.getValue()));
		}

		private void assign(ConsumeActivityRole.Data _o_) {
			activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>();
			for (java.util.Map.Entry<Integer, xbean.ConsumeActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new ConsumeActivity.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(activities.size());
			for (java.util.Map.Entry<Integer, xbean.ConsumeActivity> _e_ : activities.entrySet())
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
					activities = new java.util.HashMap<Integer, xbean.ConsumeActivity>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.ConsumeActivity _v_ = xbean.Pod.newConsumeActivityData();
					_v_.unmarshal(_os_);
					activities.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.ConsumeActivityRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.ConsumeActivityRole toData() {
			return new Data(this);
		}

		public xbean.ConsumeActivityRole toBean() {
			return new ConsumeActivityRole(this, null, null);
		}

		@Override
		public xbean.ConsumeActivityRole toDataIf() {
			return this;
		}

		public xbean.ConsumeActivityRole toBeanIf() {
			return new ConsumeActivityRole(this, null, null);
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
		public java.util.Map<Integer, xbean.ConsumeActivity> getActivities() { // key=活动id
			return activities;
		}

		@Override
		public java.util.Map<Integer, xbean.ConsumeActivity> getActivitiesAsData() { // key=活动id
			return activities;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof ConsumeActivityRole.Data)) return false;
			ConsumeActivityRole.Data _o_ = (ConsumeActivityRole.Data) _o1_;
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
