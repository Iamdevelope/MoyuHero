
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class FirstFeedActivityRole extends xdb.XBean implements xbean.FirstFeedActivityRole {
	private java.util.HashMap<Integer, xbean.FirstFeedActivity> activities; // key=活动id

	FirstFeedActivityRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>();
	}

	public FirstFeedActivityRole() {
		this(0, null, null);
	}

	public FirstFeedActivityRole(FirstFeedActivityRole _o_) {
		this(_o_, null, null);
	}

	FirstFeedActivityRole(xbean.FirstFeedActivityRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof FirstFeedActivityRole) assign((FirstFeedActivityRole)_o1_);
		else if (_o1_ instanceof FirstFeedActivityRole.Data) assign((FirstFeedActivityRole.Data)_o1_);
		else if (_o1_ instanceof FirstFeedActivityRole.Const) assign(((FirstFeedActivityRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(FirstFeedActivityRole _o_) {
		_o_._xdb_verify_unsafe_();
		activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>();
		for (java.util.Map.Entry<Integer, xbean.FirstFeedActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new FirstFeedActivity(_e_.getValue(), this, "activities"));
	}

	private void assign(FirstFeedActivityRole.Data _o_) {
		activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>();
		for (java.util.Map.Entry<Integer, xbean.FirstFeedActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new FirstFeedActivity(_e_.getValue(), this, "activities"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(activities.size());
		for (java.util.Map.Entry<Integer, xbean.FirstFeedActivity> _e_ : activities.entrySet())
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
				activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.FirstFeedActivity _v_ = new FirstFeedActivity(0, this, "activities");
				_v_.unmarshal(_os_);
				activities.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.FirstFeedActivityRole copy() {
		_xdb_verify_unsafe_();
		return new FirstFeedActivityRole(this);
	}

	@Override
	public xbean.FirstFeedActivityRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FirstFeedActivityRole toBean() {
		_xdb_verify_unsafe_();
		return new FirstFeedActivityRole(this); // same as copy()
	}

	@Override
	public xbean.FirstFeedActivityRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FirstFeedActivityRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.FirstFeedActivity> getActivities() { // key=活动id
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "activities"), activities);
	}

	@Override
	public java.util.Map<Integer, xbean.FirstFeedActivity> getActivitiesAsData() { // key=活动id
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.FirstFeedActivity> activities;
		FirstFeedActivityRole _o_ = this;
		activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>();
		for (java.util.Map.Entry<Integer, xbean.FirstFeedActivity> _e_ : _o_.activities.entrySet())
			activities.put(_e_.getKey(), new FirstFeedActivity.Data(_e_.getValue()));
		return activities;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		FirstFeedActivityRole _o_ = null;
		if ( _o1_ instanceof FirstFeedActivityRole ) _o_ = (FirstFeedActivityRole)_o1_;
		else if ( _o1_ instanceof FirstFeedActivityRole.Const ) _o_ = ((FirstFeedActivityRole.Const)_o1_).nThis();
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

	private class Const implements xbean.FirstFeedActivityRole {
		FirstFeedActivityRole nThis() {
			return FirstFeedActivityRole.this;
		}

		@Override
		public xbean.FirstFeedActivityRole copy() {
			return FirstFeedActivityRole.this.copy();
		}

		@Override
		public xbean.FirstFeedActivityRole toData() {
			return FirstFeedActivityRole.this.toData();
		}

		public xbean.FirstFeedActivityRole toBean() {
			return FirstFeedActivityRole.this.toBean();
		}

		@Override
		public xbean.FirstFeedActivityRole toDataIf() {
			return FirstFeedActivityRole.this.toDataIf();
		}

		public xbean.FirstFeedActivityRole toBeanIf() {
			return FirstFeedActivityRole.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.FirstFeedActivity> getActivities() { // key=活动id
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(activities);
		}

		@Override
		public java.util.Map<Integer, xbean.FirstFeedActivity> getActivitiesAsData() { // key=活动id
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.FirstFeedActivity> activities;
			FirstFeedActivityRole _o_ = FirstFeedActivityRole.this;
			activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>();
			for (java.util.Map.Entry<Integer, xbean.FirstFeedActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new FirstFeedActivity.Data(_e_.getValue()));
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
			return FirstFeedActivityRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return FirstFeedActivityRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return FirstFeedActivityRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return FirstFeedActivityRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return FirstFeedActivityRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return FirstFeedActivityRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return FirstFeedActivityRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return FirstFeedActivityRole.this.hashCode();
		}

		@Override
		public String toString() {
			return FirstFeedActivityRole.this.toString();
		}

	}

	public static final class Data implements xbean.FirstFeedActivityRole {
		private java.util.HashMap<Integer, xbean.FirstFeedActivity> activities; // key=活动id

		public Data() {
			activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>();
		}

		Data(xbean.FirstFeedActivityRole _o1_) {
			if (_o1_ instanceof FirstFeedActivityRole) assign((FirstFeedActivityRole)_o1_);
			else if (_o1_ instanceof FirstFeedActivityRole.Data) assign((FirstFeedActivityRole.Data)_o1_);
			else if (_o1_ instanceof FirstFeedActivityRole.Const) assign(((FirstFeedActivityRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(FirstFeedActivityRole _o_) {
			activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>();
			for (java.util.Map.Entry<Integer, xbean.FirstFeedActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new FirstFeedActivity.Data(_e_.getValue()));
		}

		private void assign(FirstFeedActivityRole.Data _o_) {
			activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>();
			for (java.util.Map.Entry<Integer, xbean.FirstFeedActivity> _e_ : _o_.activities.entrySet())
				activities.put(_e_.getKey(), new FirstFeedActivity.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(activities.size());
			for (java.util.Map.Entry<Integer, xbean.FirstFeedActivity> _e_ : activities.entrySet())
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
					activities = new java.util.HashMap<Integer, xbean.FirstFeedActivity>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.FirstFeedActivity _v_ = xbean.Pod.newFirstFeedActivityData();
					_v_.unmarshal(_os_);
					activities.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.FirstFeedActivityRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.FirstFeedActivityRole toData() {
			return new Data(this);
		}

		public xbean.FirstFeedActivityRole toBean() {
			return new FirstFeedActivityRole(this, null, null);
		}

		@Override
		public xbean.FirstFeedActivityRole toDataIf() {
			return this;
		}

		public xbean.FirstFeedActivityRole toBeanIf() {
			return new FirstFeedActivityRole(this, null, null);
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
		public java.util.Map<Integer, xbean.FirstFeedActivity> getActivities() { // key=活动id
			return activities;
		}

		@Override
		public java.util.Map<Integer, xbean.FirstFeedActivity> getActivitiesAsData() { // key=活动id
			return activities;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof FirstFeedActivityRole.Data)) return false;
			FirstFeedActivityRole.Data _o_ = (FirstFeedActivityRole.Data) _o1_;
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
