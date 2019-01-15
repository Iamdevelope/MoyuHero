
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class FirstLadderInfo extends xdb.XBean implements xbean.FirstLadderInfo {
	private long starttime; // 上一次登上天梯第一名的时间
	private int zaiweimilsec; // 本周在天梯第一名的总时间 单位：毫秒

	FirstLadderInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public FirstLadderInfo() {
		this(0, null, null);
	}

	public FirstLadderInfo(FirstLadderInfo _o_) {
		this(_o_, null, null);
	}

	FirstLadderInfo(xbean.FirstLadderInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof FirstLadderInfo) assign((FirstLadderInfo)_o1_);
		else if (_o1_ instanceof FirstLadderInfo.Data) assign((FirstLadderInfo.Data)_o1_);
		else if (_o1_ instanceof FirstLadderInfo.Const) assign(((FirstLadderInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(FirstLadderInfo _o_) {
		_o_._xdb_verify_unsafe_();
		starttime = _o_.starttime;
		zaiweimilsec = _o_.zaiweimilsec;
	}

	private void assign(FirstLadderInfo.Data _o_) {
		starttime = _o_.starttime;
		zaiweimilsec = _o_.zaiweimilsec;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(starttime);
		_os_.marshal(zaiweimilsec);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		starttime = _os_.unmarshal_long();
		zaiweimilsec = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.FirstLadderInfo copy() {
		_xdb_verify_unsafe_();
		return new FirstLadderInfo(this);
	}

	@Override
	public xbean.FirstLadderInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FirstLadderInfo toBean() {
		_xdb_verify_unsafe_();
		return new FirstLadderInfo(this); // same as copy()
	}

	@Override
	public xbean.FirstLadderInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.FirstLadderInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getStarttime() { // 上一次登上天梯第一名的时间
		_xdb_verify_unsafe_();
		return starttime;
	}

	@Override
	public int getZaiweimilsec() { // 本周在天梯第一名的总时间 单位：毫秒
		_xdb_verify_unsafe_();
		return zaiweimilsec;
	}

	@Override
	public void setStarttime(long _v_) { // 上一次登上天梯第一名的时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "starttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, starttime) {
					public void rollback() { starttime = _xdb_saved; }
				};}});
		starttime = _v_;
	}

	@Override
	public void setZaiweimilsec(int _v_) { // 本周在天梯第一名的总时间 单位：毫秒
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "zaiweimilsec") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, zaiweimilsec) {
					public void rollback() { zaiweimilsec = _xdb_saved; }
				};}});
		zaiweimilsec = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		FirstLadderInfo _o_ = null;
		if ( _o1_ instanceof FirstLadderInfo ) _o_ = (FirstLadderInfo)_o1_;
		else if ( _o1_ instanceof FirstLadderInfo.Const ) _o_ = ((FirstLadderInfo.Const)_o1_).nThis();
		else return false;
		if (starttime != _o_.starttime) return false;
		if (zaiweimilsec != _o_.zaiweimilsec) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += starttime;
		_h_ += zaiweimilsec;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(starttime);
		_sb_.append(",");
		_sb_.append(zaiweimilsec);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("starttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("zaiweimilsec"));
		return lb;
	}

	private class Const implements xbean.FirstLadderInfo {
		FirstLadderInfo nThis() {
			return FirstLadderInfo.this;
		}

		@Override
		public xbean.FirstLadderInfo copy() {
			return FirstLadderInfo.this.copy();
		}

		@Override
		public xbean.FirstLadderInfo toData() {
			return FirstLadderInfo.this.toData();
		}

		public xbean.FirstLadderInfo toBean() {
			return FirstLadderInfo.this.toBean();
		}

		@Override
		public xbean.FirstLadderInfo toDataIf() {
			return FirstLadderInfo.this.toDataIf();
		}

		public xbean.FirstLadderInfo toBeanIf() {
			return FirstLadderInfo.this.toBeanIf();
		}

		@Override
		public long getStarttime() { // 上一次登上天梯第一名的时间
			_xdb_verify_unsafe_();
			return starttime;
		}

		@Override
		public int getZaiweimilsec() { // 本周在天梯第一名的总时间 单位：毫秒
			_xdb_verify_unsafe_();
			return zaiweimilsec;
		}

		@Override
		public void setStarttime(long _v_) { // 上一次登上天梯第一名的时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setZaiweimilsec(int _v_) { // 本周在天梯第一名的总时间 单位：毫秒
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
			return FirstLadderInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return FirstLadderInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return FirstLadderInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return FirstLadderInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return FirstLadderInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return FirstLadderInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return FirstLadderInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return FirstLadderInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return FirstLadderInfo.this.toString();
		}

	}

	public static final class Data implements xbean.FirstLadderInfo {
		private long starttime; // 上一次登上天梯第一名的时间
		private int zaiweimilsec; // 本周在天梯第一名的总时间 单位：毫秒

		public Data() {
		}

		Data(xbean.FirstLadderInfo _o1_) {
			if (_o1_ instanceof FirstLadderInfo) assign((FirstLadderInfo)_o1_);
			else if (_o1_ instanceof FirstLadderInfo.Data) assign((FirstLadderInfo.Data)_o1_);
			else if (_o1_ instanceof FirstLadderInfo.Const) assign(((FirstLadderInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(FirstLadderInfo _o_) {
			starttime = _o_.starttime;
			zaiweimilsec = _o_.zaiweimilsec;
		}

		private void assign(FirstLadderInfo.Data _o_) {
			starttime = _o_.starttime;
			zaiweimilsec = _o_.zaiweimilsec;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(starttime);
			_os_.marshal(zaiweimilsec);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			starttime = _os_.unmarshal_long();
			zaiweimilsec = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.FirstLadderInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.FirstLadderInfo toData() {
			return new Data(this);
		}

		public xbean.FirstLadderInfo toBean() {
			return new FirstLadderInfo(this, null, null);
		}

		@Override
		public xbean.FirstLadderInfo toDataIf() {
			return this;
		}

		public xbean.FirstLadderInfo toBeanIf() {
			return new FirstLadderInfo(this, null, null);
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
		public long getStarttime() { // 上一次登上天梯第一名的时间
			return starttime;
		}

		@Override
		public int getZaiweimilsec() { // 本周在天梯第一名的总时间 单位：毫秒
			return zaiweimilsec;
		}

		@Override
		public void setStarttime(long _v_) { // 上一次登上天梯第一名的时间
			starttime = _v_;
		}

		@Override
		public void setZaiweimilsec(int _v_) { // 本周在天梯第一名的总时间 单位：毫秒
			zaiweimilsec = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof FirstLadderInfo.Data)) return false;
			FirstLadderInfo.Data _o_ = (FirstLadderInfo.Data) _o1_;
			if (starttime != _o_.starttime) return false;
			if (zaiweimilsec != _o_.zaiweimilsec) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += starttime;
			_h_ += zaiweimilsec;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(starttime);
			_sb_.append(",");
			_sb_.append(zaiweimilsec);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
