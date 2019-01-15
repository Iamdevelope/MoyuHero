
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class FirstFeedActivity extends xdb.XBean implements xbean.FirstFeedActivity {
	private long chargetime; // 首次充值时间
	private long rebatetime; // 领取时间
	private boolean isgainaward; // 是否已经参与过

	FirstFeedActivity(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public FirstFeedActivity() {
		this(0, null, null);
	}

	public FirstFeedActivity(FirstFeedActivity _o_) {
		this(_o_, null, null);
	}

	FirstFeedActivity(xbean.FirstFeedActivity _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof FirstFeedActivity) assign((FirstFeedActivity)_o1_);
		else if (_o1_ instanceof FirstFeedActivity.Data) assign((FirstFeedActivity.Data)_o1_);
		else if (_o1_ instanceof FirstFeedActivity.Const) assign(((FirstFeedActivity.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(FirstFeedActivity _o_) {
		_o_._xdb_verify_unsafe_();
		chargetime = _o_.chargetime;
		rebatetime = _o_.rebatetime;
		isgainaward = _o_.isgainaward;
	}

	private void assign(FirstFeedActivity.Data _o_) {
		chargetime = _o_.chargetime;
		rebatetime = _o_.rebatetime;
		isgainaward = _o_.isgainaward;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(chargetime);
		_os_.marshal(rebatetime);
		_os_.marshal(isgainaward);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		chargetime = _os_.unmarshal_long();
		rebatetime = _os_.unmarshal_long();
		isgainaward = _os_.unmarshal_boolean();
		return _os_;
	}

	@Override
	public xbean.FirstFeedActivity copy() {
		_xdb_verify_unsafe_();
		return new FirstFeedActivity(this);
	}

	@Override
	public xbean.FirstFeedActivity toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FirstFeedActivity toBean() {
		_xdb_verify_unsafe_();
		return new FirstFeedActivity(this); // same as copy()
	}

	@Override
	public xbean.FirstFeedActivity toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FirstFeedActivity toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getChargetime() { // 首次充值时间
		_xdb_verify_unsafe_();
		return chargetime;
	}

	@Override
	public long getRebatetime() { // 领取时间
		_xdb_verify_unsafe_();
		return rebatetime;
	}

	@Override
	public boolean getIsgainaward() { // 是否已经参与过
		_xdb_verify_unsafe_();
		return isgainaward;
	}

	@Override
	public void setChargetime(long _v_) { // 首次充值时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "chargetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, chargetime) {
					public void rollback() { chargetime = _xdb_saved; }
				};}});
		chargetime = _v_;
	}

	@Override
	public void setRebatetime(long _v_) { // 领取时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "rebatetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, rebatetime) {
					public void rollback() { rebatetime = _xdb_saved; }
				};}});
		rebatetime = _v_;
	}

	@Override
	public void setIsgainaward(boolean _v_) { // 是否已经参与过
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isgainaward") {
			protected xdb.Log create() {
				return new xdb.logs.LogObject<Boolean>(this, isgainaward) {
					public void rollback() { isgainaward = _xdb_saved; }
				};}});
		isgainaward = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		FirstFeedActivity _o_ = null;
		if ( _o1_ instanceof FirstFeedActivity ) _o_ = (FirstFeedActivity)_o1_;
		else if ( _o1_ instanceof FirstFeedActivity.Const ) _o_ = ((FirstFeedActivity.Const)_o1_).nThis();
		else return false;
		if (chargetime != _o_.chargetime) return false;
		if (rebatetime != _o_.rebatetime) return false;
		if (isgainaward != _o_.isgainaward) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += chargetime;
		_h_ += rebatetime;
		_h_ += isgainaward ? 1231 : 1237;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(chargetime);
		_sb_.append(",");
		_sb_.append(rebatetime);
		_sb_.append(",");
		_sb_.append(isgainaward);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("chargetime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("rebatetime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isgainaward"));
		return lb;
	}

	private class Const implements xbean.FirstFeedActivity {
		FirstFeedActivity nThis() {
			return FirstFeedActivity.this;
		}

		@Override
		public xbean.FirstFeedActivity copy() {
			return FirstFeedActivity.this.copy();
		}

		@Override
		public xbean.FirstFeedActivity toData() {
			return FirstFeedActivity.this.toData();
		}

		public xbean.FirstFeedActivity toBean() {
			return FirstFeedActivity.this.toBean();
		}

		@Override
		public xbean.FirstFeedActivity toDataIf() {
			return FirstFeedActivity.this.toDataIf();
		}

		public xbean.FirstFeedActivity toBeanIf() {
			return FirstFeedActivity.this.toBeanIf();
		}

		@Override
		public long getChargetime() { // 首次充值时间
			_xdb_verify_unsafe_();
			return chargetime;
		}

		@Override
		public long getRebatetime() { // 领取时间
			_xdb_verify_unsafe_();
			return rebatetime;
		}

		@Override
		public boolean getIsgainaward() { // 是否已经参与过
			_xdb_verify_unsafe_();
			return isgainaward;
		}

		@Override
		public void setChargetime(long _v_) { // 首次充值时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRebatetime(long _v_) { // 领取时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsgainaward(boolean _v_) { // 是否已经参与过
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
			return FirstFeedActivity.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return FirstFeedActivity.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return FirstFeedActivity.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return FirstFeedActivity.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return FirstFeedActivity.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return FirstFeedActivity.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return FirstFeedActivity.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return FirstFeedActivity.this.hashCode();
		}

		@Override
		public String toString() {
			return FirstFeedActivity.this.toString();
		}

	}

	public static final class Data implements xbean.FirstFeedActivity {
		private long chargetime; // 首次充值时间
		private long rebatetime; // 领取时间
		private boolean isgainaward; // 是否已经参与过

		public Data() {
		}

		Data(xbean.FirstFeedActivity _o1_) {
			if (_o1_ instanceof FirstFeedActivity) assign((FirstFeedActivity)_o1_);
			else if (_o1_ instanceof FirstFeedActivity.Data) assign((FirstFeedActivity.Data)_o1_);
			else if (_o1_ instanceof FirstFeedActivity.Const) assign(((FirstFeedActivity.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(FirstFeedActivity _o_) {
			chargetime = _o_.chargetime;
			rebatetime = _o_.rebatetime;
			isgainaward = _o_.isgainaward;
		}

		private void assign(FirstFeedActivity.Data _o_) {
			chargetime = _o_.chargetime;
			rebatetime = _o_.rebatetime;
			isgainaward = _o_.isgainaward;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(chargetime);
			_os_.marshal(rebatetime);
			_os_.marshal(isgainaward);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			chargetime = _os_.unmarshal_long();
			rebatetime = _os_.unmarshal_long();
			isgainaward = _os_.unmarshal_boolean();
			return _os_;
		}

		@Override
		public xbean.FirstFeedActivity copy() {
			return new Data(this);
		}

		@Override
		public xbean.FirstFeedActivity toData() {
			return new Data(this);
		}

		public xbean.FirstFeedActivity toBean() {
			return new FirstFeedActivity(this, null, null);
		}

		@Override
		public xbean.FirstFeedActivity toDataIf() {
			return this;
		}

		public xbean.FirstFeedActivity toBeanIf() {
			return new FirstFeedActivity(this, null, null);
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
		public long getChargetime() { // 首次充值时间
			return chargetime;
		}

		@Override
		public long getRebatetime() { // 领取时间
			return rebatetime;
		}

		@Override
		public boolean getIsgainaward() { // 是否已经参与过
			return isgainaward;
		}

		@Override
		public void setChargetime(long _v_) { // 首次充值时间
			chargetime = _v_;
		}

		@Override
		public void setRebatetime(long _v_) { // 领取时间
			rebatetime = _v_;
		}

		@Override
		public void setIsgainaward(boolean _v_) { // 是否已经参与过
			isgainaward = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof FirstFeedActivity.Data)) return false;
			FirstFeedActivity.Data _o_ = (FirstFeedActivity.Data) _o1_;
			if (chargetime != _o_.chargetime) return false;
			if (rebatetime != _o_.rebatetime) return false;
			if (isgainaward != _o_.isgainaward) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += chargetime;
			_h_ += rebatetime;
			_h_ += isgainaward ? 1231 : 1237;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(chargetime);
			_sb_.append(",");
			_sb_.append(rebatetime);
			_sb_.append(",");
			_sb_.append(isgainaward);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
