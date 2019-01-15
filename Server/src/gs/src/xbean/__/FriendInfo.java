
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class FriendInfo extends xdb.XBean implements xbean.FriendInfo {
	private int totilinum; // 今日赠送给他体力次数
	private int givetilinum; // 今日给我体力次数
	private long lastdaychangetime; // 上次数据变动时间，为跨天清除用

	FriendInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		lastdaychangetime = 0;
	}

	public FriendInfo() {
		this(0, null, null);
	}

	public FriendInfo(FriendInfo _o_) {
		this(_o_, null, null);
	}

	FriendInfo(xbean.FriendInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof FriendInfo) assign((FriendInfo)_o1_);
		else if (_o1_ instanceof FriendInfo.Data) assign((FriendInfo.Data)_o1_);
		else if (_o1_ instanceof FriendInfo.Const) assign(((FriendInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(FriendInfo _o_) {
		_o_._xdb_verify_unsafe_();
		totilinum = _o_.totilinum;
		givetilinum = _o_.givetilinum;
		lastdaychangetime = _o_.lastdaychangetime;
	}

	private void assign(FriendInfo.Data _o_) {
		totilinum = _o_.totilinum;
		givetilinum = _o_.givetilinum;
		lastdaychangetime = _o_.lastdaychangetime;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(totilinum);
		_os_.marshal(givetilinum);
		_os_.marshal(lastdaychangetime);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		totilinum = _os_.unmarshal_int();
		givetilinum = _os_.unmarshal_int();
		lastdaychangetime = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.FriendInfo copy() {
		_xdb_verify_unsafe_();
		return new FriendInfo(this);
	}

	@Override
	public xbean.FriendInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FriendInfo toBean() {
		_xdb_verify_unsafe_();
		return new FriendInfo(this); // same as copy()
	}

	@Override
	public xbean.FriendInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FriendInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getTotilinum() { // 今日赠送给他体力次数
		_xdb_verify_unsafe_();
		return totilinum;
	}

	@Override
	public int getGivetilinum() { // 今日给我体力次数
		_xdb_verify_unsafe_();
		return givetilinum;
	}

	@Override
	public long getLastdaychangetime() { // 上次数据变动时间，为跨天清除用
		_xdb_verify_unsafe_();
		return lastdaychangetime;
	}

	@Override
	public void setTotilinum(int _v_) { // 今日赠送给他体力次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "totilinum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, totilinum) {
					public void rollback() { totilinum = _xdb_saved; }
				};}});
		totilinum = _v_;
	}

	@Override
	public void setGivetilinum(int _v_) { // 今日给我体力次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "givetilinum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, givetilinum) {
					public void rollback() { givetilinum = _xdb_saved; }
				};}});
		givetilinum = _v_;
	}

	@Override
	public void setLastdaychangetime(long _v_) { // 上次数据变动时间，为跨天清除用
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lastdaychangetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, lastdaychangetime) {
					public void rollback() { lastdaychangetime = _xdb_saved; }
				};}});
		lastdaychangetime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		FriendInfo _o_ = null;
		if ( _o1_ instanceof FriendInfo ) _o_ = (FriendInfo)_o1_;
		else if ( _o1_ instanceof FriendInfo.Const ) _o_ = ((FriendInfo.Const)_o1_).nThis();
		else return false;
		if (totilinum != _o_.totilinum) return false;
		if (givetilinum != _o_.givetilinum) return false;
		if (lastdaychangetime != _o_.lastdaychangetime) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += totilinum;
		_h_ += givetilinum;
		_h_ += lastdaychangetime;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(totilinum);
		_sb_.append(",");
		_sb_.append(givetilinum);
		_sb_.append(",");
		_sb_.append(lastdaychangetime);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("totilinum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("givetilinum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lastdaychangetime"));
		return lb;
	}

	private class Const implements xbean.FriendInfo {
		FriendInfo nThis() {
			return FriendInfo.this;
		}

		@Override
		public xbean.FriendInfo copy() {
			return FriendInfo.this.copy();
		}

		@Override
		public xbean.FriendInfo toData() {
			return FriendInfo.this.toData();
		}

		public xbean.FriendInfo toBean() {
			return FriendInfo.this.toBean();
		}

		@Override
		public xbean.FriendInfo toDataIf() {
			return FriendInfo.this.toDataIf();
		}

		public xbean.FriendInfo toBeanIf() {
			return FriendInfo.this.toBeanIf();
		}

		@Override
		public int getTotilinum() { // 今日赠送给他体力次数
			_xdb_verify_unsafe_();
			return totilinum;
		}

		@Override
		public int getGivetilinum() { // 今日给我体力次数
			_xdb_verify_unsafe_();
			return givetilinum;
		}

		@Override
		public long getLastdaychangetime() { // 上次数据变动时间，为跨天清除用
			_xdb_verify_unsafe_();
			return lastdaychangetime;
		}

		@Override
		public void setTotilinum(int _v_) { // 今日赠送给他体力次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGivetilinum(int _v_) { // 今日给我体力次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLastdaychangetime(long _v_) { // 上次数据变动时间，为跨天清除用
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
			return FriendInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return FriendInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return FriendInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return FriendInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return FriendInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return FriendInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return FriendInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return FriendInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return FriendInfo.this.toString();
		}

	}

	public static final class Data implements xbean.FriendInfo {
		private int totilinum; // 今日赠送给他体力次数
		private int givetilinum; // 今日给我体力次数
		private long lastdaychangetime; // 上次数据变动时间，为跨天清除用

		public Data() {
			lastdaychangetime = 0;
		}

		Data(xbean.FriendInfo _o1_) {
			if (_o1_ instanceof FriendInfo) assign((FriendInfo)_o1_);
			else if (_o1_ instanceof FriendInfo.Data) assign((FriendInfo.Data)_o1_);
			else if (_o1_ instanceof FriendInfo.Const) assign(((FriendInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(FriendInfo _o_) {
			totilinum = _o_.totilinum;
			givetilinum = _o_.givetilinum;
			lastdaychangetime = _o_.lastdaychangetime;
		}

		private void assign(FriendInfo.Data _o_) {
			totilinum = _o_.totilinum;
			givetilinum = _o_.givetilinum;
			lastdaychangetime = _o_.lastdaychangetime;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(totilinum);
			_os_.marshal(givetilinum);
			_os_.marshal(lastdaychangetime);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			totilinum = _os_.unmarshal_int();
			givetilinum = _os_.unmarshal_int();
			lastdaychangetime = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.FriendInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.FriendInfo toData() {
			return new Data(this);
		}

		public xbean.FriendInfo toBean() {
			return new FriendInfo(this, null, null);
		}

		@Override
		public xbean.FriendInfo toDataIf() {
			return this;
		}

		public xbean.FriendInfo toBeanIf() {
			return new FriendInfo(this, null, null);
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
		public int getTotilinum() { // 今日赠送给他体力次数
			return totilinum;
		}

		@Override
		public int getGivetilinum() { // 今日给我体力次数
			return givetilinum;
		}

		@Override
		public long getLastdaychangetime() { // 上次数据变动时间，为跨天清除用
			return lastdaychangetime;
		}

		@Override
		public void setTotilinum(int _v_) { // 今日赠送给他体力次数
			totilinum = _v_;
		}

		@Override
		public void setGivetilinum(int _v_) { // 今日给我体力次数
			givetilinum = _v_;
		}

		@Override
		public void setLastdaychangetime(long _v_) { // 上次数据变动时间，为跨天清除用
			lastdaychangetime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof FriendInfo.Data)) return false;
			FriendInfo.Data _o_ = (FriendInfo.Data) _o1_;
			if (totilinum != _o_.totilinum) return false;
			if (givetilinum != _o_.givetilinum) return false;
			if (lastdaychangetime != _o_.lastdaychangetime) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += totilinum;
			_h_ += givetilinum;
			_h_ += lastdaychangetime;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(totilinum);
			_sb_.append(",");
			_sb_.append(givetilinum);
			_sb_.append(",");
			_sb_.append(lastdaychangetime);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
