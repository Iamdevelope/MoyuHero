
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class ChargeActivity extends xdb.XBean implements xbean.ChargeActivity {
	private int activityid; // 在该id活动中
	private int totalcharge; // 充值的总数
	private java.util.HashMap<Integer, Boolean> isgainaward; // 是否已经领取奖励 key=元宝数量

	ChargeActivity(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		isgainaward = new java.util.HashMap<Integer, Boolean>();
	}

	public ChargeActivity() {
		this(0, null, null);
	}

	public ChargeActivity(ChargeActivity _o_) {
		this(_o_, null, null);
	}

	ChargeActivity(xbean.ChargeActivity _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof ChargeActivity) assign((ChargeActivity)_o1_);
		else if (_o1_ instanceof ChargeActivity.Data) assign((ChargeActivity.Data)_o1_);
		else if (_o1_ instanceof ChargeActivity.Const) assign(((ChargeActivity.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(ChargeActivity _o_) {
		_o_._xdb_verify_unsafe_();
		activityid = _o_.activityid;
		totalcharge = _o_.totalcharge;
		isgainaward = new java.util.HashMap<Integer, Boolean>();
		for (java.util.Map.Entry<Integer, Boolean> _e_ : _o_.isgainaward.entrySet())
			isgainaward.put(_e_.getKey(), _e_.getValue());
	}

	private void assign(ChargeActivity.Data _o_) {
		activityid = _o_.activityid;
		totalcharge = _o_.totalcharge;
		isgainaward = new java.util.HashMap<Integer, Boolean>();
		for (java.util.Map.Entry<Integer, Boolean> _e_ : _o_.isgainaward.entrySet())
			isgainaward.put(_e_.getKey(), _e_.getValue());
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(activityid);
		_os_.marshal(totalcharge);
		_os_.compact_uint32(isgainaward.size());
		for (java.util.Map.Entry<Integer, Boolean> _e_ : isgainaward.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		activityid = _os_.unmarshal_int();
		totalcharge = _os_.unmarshal_int();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				isgainaward = new java.util.HashMap<Integer, Boolean>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				boolean _v_ = false;
				_v_ = _os_.unmarshal_boolean();
				isgainaward.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.ChargeActivity copy() {
		_xdb_verify_unsafe_();
		return new ChargeActivity(this);
	}

	@Override
	public xbean.ChargeActivity toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ChargeActivity toBean() {
		_xdb_verify_unsafe_();
		return new ChargeActivity(this); // same as copy()
	}

	@Override
	public xbean.ChargeActivity toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ChargeActivity toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getActivityid() { // 在该id活动中
		_xdb_verify_unsafe_();
		return activityid;
	}

	@Override
	public int getTotalcharge() { // 充值的总数
		_xdb_verify_unsafe_();
		return totalcharge;
	}

	@Override
	public java.util.Map<Integer, Boolean> getIsgainaward() { // 是否已经领取奖励 key=元宝数量
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "isgainaward"), isgainaward);
	}

	@Override
	public java.util.Map<Integer, Boolean> getIsgainawardAsData() { // 是否已经领取奖励 key=元宝数量
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Boolean> isgainaward;
		ChargeActivity _o_ = this;
		isgainaward = new java.util.HashMap<Integer, Boolean>();
		for (java.util.Map.Entry<Integer, Boolean> _e_ : _o_.isgainaward.entrySet())
			isgainaward.put(_e_.getKey(), _e_.getValue());
		return isgainaward;
	}

	@Override
	public void setActivityid(int _v_) { // 在该id活动中
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "activityid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, activityid) {
					public void rollback() { activityid = _xdb_saved; }
				};}});
		activityid = _v_;
	}

	@Override
	public void setTotalcharge(int _v_) { // 充值的总数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "totalcharge") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, totalcharge) {
					public void rollback() { totalcharge = _xdb_saved; }
				};}});
		totalcharge = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		ChargeActivity _o_ = null;
		if ( _o1_ instanceof ChargeActivity ) _o_ = (ChargeActivity)_o1_;
		else if ( _o1_ instanceof ChargeActivity.Const ) _o_ = ((ChargeActivity.Const)_o1_).nThis();
		else return false;
		if (activityid != _o_.activityid) return false;
		if (totalcharge != _o_.totalcharge) return false;
		if (!isgainaward.equals(_o_.isgainaward)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += activityid;
		_h_ += totalcharge;
		_h_ += isgainaward.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(activityid);
		_sb_.append(",");
		_sb_.append(totalcharge);
		_sb_.append(",");
		_sb_.append(isgainaward);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("activityid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("totalcharge"));
		lb.add(new xdb.logs.ListenableMap().setVarName("isgainaward"));
		return lb;
	}

	private class Const implements xbean.ChargeActivity {
		ChargeActivity nThis() {
			return ChargeActivity.this;
		}

		@Override
		public xbean.ChargeActivity copy() {
			return ChargeActivity.this.copy();
		}

		@Override
		public xbean.ChargeActivity toData() {
			return ChargeActivity.this.toData();
		}

		public xbean.ChargeActivity toBean() {
			return ChargeActivity.this.toBean();
		}

		@Override
		public xbean.ChargeActivity toDataIf() {
			return ChargeActivity.this.toDataIf();
		}

		public xbean.ChargeActivity toBeanIf() {
			return ChargeActivity.this.toBeanIf();
		}

		@Override
		public int getActivityid() { // 在该id活动中
			_xdb_verify_unsafe_();
			return activityid;
		}

		@Override
		public int getTotalcharge() { // 充值的总数
			_xdb_verify_unsafe_();
			return totalcharge;
		}

		@Override
		public java.util.Map<Integer, Boolean> getIsgainaward() { // 是否已经领取奖励 key=元宝数量
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(isgainaward);
		}

		@Override
		public java.util.Map<Integer, Boolean> getIsgainawardAsData() { // 是否已经领取奖励 key=元宝数量
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Boolean> isgainaward;
			ChargeActivity _o_ = ChargeActivity.this;
			isgainaward = new java.util.HashMap<Integer, Boolean>();
			for (java.util.Map.Entry<Integer, Boolean> _e_ : _o_.isgainaward.entrySet())
				isgainaward.put(_e_.getKey(), _e_.getValue());
			return isgainaward;
		}

		@Override
		public void setActivityid(int _v_) { // 在该id活动中
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTotalcharge(int _v_) { // 充值的总数
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
			return ChargeActivity.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return ChargeActivity.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return ChargeActivity.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return ChargeActivity.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return ChargeActivity.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return ChargeActivity.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return ChargeActivity.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return ChargeActivity.this.hashCode();
		}

		@Override
		public String toString() {
			return ChargeActivity.this.toString();
		}

	}

	public static final class Data implements xbean.ChargeActivity {
		private int activityid; // 在该id活动中
		private int totalcharge; // 充值的总数
		private java.util.HashMap<Integer, Boolean> isgainaward; // 是否已经领取奖励 key=元宝数量

		public Data() {
			isgainaward = new java.util.HashMap<Integer, Boolean>();
		}

		Data(xbean.ChargeActivity _o1_) {
			if (_o1_ instanceof ChargeActivity) assign((ChargeActivity)_o1_);
			else if (_o1_ instanceof ChargeActivity.Data) assign((ChargeActivity.Data)_o1_);
			else if (_o1_ instanceof ChargeActivity.Const) assign(((ChargeActivity.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(ChargeActivity _o_) {
			activityid = _o_.activityid;
			totalcharge = _o_.totalcharge;
			isgainaward = new java.util.HashMap<Integer, Boolean>();
			for (java.util.Map.Entry<Integer, Boolean> _e_ : _o_.isgainaward.entrySet())
				isgainaward.put(_e_.getKey(), _e_.getValue());
		}

		private void assign(ChargeActivity.Data _o_) {
			activityid = _o_.activityid;
			totalcharge = _o_.totalcharge;
			isgainaward = new java.util.HashMap<Integer, Boolean>();
			for (java.util.Map.Entry<Integer, Boolean> _e_ : _o_.isgainaward.entrySet())
				isgainaward.put(_e_.getKey(), _e_.getValue());
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(activityid);
			_os_.marshal(totalcharge);
			_os_.compact_uint32(isgainaward.size());
			for (java.util.Map.Entry<Integer, Boolean> _e_ : isgainaward.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			activityid = _os_.unmarshal_int();
			totalcharge = _os_.unmarshal_int();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					isgainaward = new java.util.HashMap<Integer, Boolean>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					boolean _v_ = false;
					_v_ = _os_.unmarshal_boolean();
					isgainaward.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.ChargeActivity copy() {
			return new Data(this);
		}

		@Override
		public xbean.ChargeActivity toData() {
			return new Data(this);
		}

		public xbean.ChargeActivity toBean() {
			return new ChargeActivity(this, null, null);
		}

		@Override
		public xbean.ChargeActivity toDataIf() {
			return this;
		}

		public xbean.ChargeActivity toBeanIf() {
			return new ChargeActivity(this, null, null);
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
		public int getActivityid() { // 在该id活动中
			return activityid;
		}

		@Override
		public int getTotalcharge() { // 充值的总数
			return totalcharge;
		}

		@Override
		public java.util.Map<Integer, Boolean> getIsgainaward() { // 是否已经领取奖励 key=元宝数量
			return isgainaward;
		}

		@Override
		public java.util.Map<Integer, Boolean> getIsgainawardAsData() { // 是否已经领取奖励 key=元宝数量
			return isgainaward;
		}

		@Override
		public void setActivityid(int _v_) { // 在该id活动中
			activityid = _v_;
		}

		@Override
		public void setTotalcharge(int _v_) { // 充值的总数
			totalcharge = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof ChargeActivity.Data)) return false;
			ChargeActivity.Data _o_ = (ChargeActivity.Data) _o1_;
			if (activityid != _o_.activityid) return false;
			if (totalcharge != _o_.totalcharge) return false;
			if (!isgainaward.equals(_o_.isgainaward)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += activityid;
			_h_ += totalcharge;
			_h_ += isgainaward.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(activityid);
			_sb_.append(",");
			_sb_.append(totalcharge);
			_sb_.append(",");
			_sb_.append(isgainaward);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
