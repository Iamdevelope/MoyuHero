
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Friends extends xdb.XBean implements xbean.Friends {
	private java.util.HashMap<Long, xbean.FriendInfo> mine; // key=好友roldId

	Friends(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		mine = new java.util.HashMap<Long, xbean.FriendInfo>();
	}

	public Friends() {
		this(0, null, null);
	}

	public Friends(Friends _o_) {
		this(_o_, null, null);
	}

	Friends(xbean.Friends _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Friends) assign((Friends)_o1_);
		else if (_o1_ instanceof Friends.Data) assign((Friends.Data)_o1_);
		else if (_o1_ instanceof Friends.Const) assign(((Friends.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Friends _o_) {
		_o_._xdb_verify_unsafe_();
		mine = new java.util.HashMap<Long, xbean.FriendInfo>();
		for (java.util.Map.Entry<Long, xbean.FriendInfo> _e_ : _o_.mine.entrySet())
			mine.put(_e_.getKey(), new FriendInfo(_e_.getValue(), this, "mine"));
	}

	private void assign(Friends.Data _o_) {
		mine = new java.util.HashMap<Long, xbean.FriendInfo>();
		for (java.util.Map.Entry<Long, xbean.FriendInfo> _e_ : _o_.mine.entrySet())
			mine.put(_e_.getKey(), new FriendInfo(_e_.getValue(), this, "mine"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(mine.size());
		for (java.util.Map.Entry<Long, xbean.FriendInfo> _e_ : mine.entrySet())
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
				mine = new java.util.HashMap<Long, xbean.FriendInfo>(size * 2);
			}
			for (; size > 0; --size)
			{
				long _k_ = 0;
				_k_ = _os_.unmarshal_long();
				xbean.FriendInfo _v_ = new FriendInfo(0, this, "mine");
				_v_.unmarshal(_os_);
				mine.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.Friends copy() {
		_xdb_verify_unsafe_();
		return new Friends(this);
	}

	@Override
	public xbean.Friends toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Friends toBean() {
		_xdb_verify_unsafe_();
		return new Friends(this); // same as copy()
	}

	@Override
	public xbean.Friends toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Friends toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Long, xbean.FriendInfo> getMine() { // key=好友roldId
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "mine"), mine);
	}

	@Override
	public java.util.Map<Long, xbean.FriendInfo> getMineAsData() { // key=好友roldId
		_xdb_verify_unsafe_();
		java.util.Map<Long, xbean.FriendInfo> mine;
		Friends _o_ = this;
		mine = new java.util.HashMap<Long, xbean.FriendInfo>();
		for (java.util.Map.Entry<Long, xbean.FriendInfo> _e_ : _o_.mine.entrySet())
			mine.put(_e_.getKey(), new FriendInfo.Data(_e_.getValue()));
		return mine;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Friends _o_ = null;
		if ( _o1_ instanceof Friends ) _o_ = (Friends)_o1_;
		else if ( _o1_ instanceof Friends.Const ) _o_ = ((Friends.Const)_o1_).nThis();
		else return false;
		if (!mine.equals(_o_.mine)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += mine.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(mine);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("mine"));
		return lb;
	}

	private class Const implements xbean.Friends {
		Friends nThis() {
			return Friends.this;
		}

		@Override
		public xbean.Friends copy() {
			return Friends.this.copy();
		}

		@Override
		public xbean.Friends toData() {
			return Friends.this.toData();
		}

		public xbean.Friends toBean() {
			return Friends.this.toBean();
		}

		@Override
		public xbean.Friends toDataIf() {
			return Friends.this.toDataIf();
		}

		public xbean.Friends toBeanIf() {
			return Friends.this.toBeanIf();
		}

		@Override
		public java.util.Map<Long, xbean.FriendInfo> getMine() { // key=好友roldId
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(mine);
		}

		@Override
		public java.util.Map<Long, xbean.FriendInfo> getMineAsData() { // key=好友roldId
			_xdb_verify_unsafe_();
			java.util.Map<Long, xbean.FriendInfo> mine;
			Friends _o_ = Friends.this;
			mine = new java.util.HashMap<Long, xbean.FriendInfo>();
			for (java.util.Map.Entry<Long, xbean.FriendInfo> _e_ : _o_.mine.entrySet())
				mine.put(_e_.getKey(), new FriendInfo.Data(_e_.getValue()));
			return mine;
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
			return Friends.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Friends.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Friends.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Friends.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Friends.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Friends.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Friends.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Friends.this.hashCode();
		}

		@Override
		public String toString() {
			return Friends.this.toString();
		}

	}

	public static final class Data implements xbean.Friends {
		private java.util.HashMap<Long, xbean.FriendInfo> mine; // key=好友roldId

		public Data() {
			mine = new java.util.HashMap<Long, xbean.FriendInfo>();
		}

		Data(xbean.Friends _o1_) {
			if (_o1_ instanceof Friends) assign((Friends)_o1_);
			else if (_o1_ instanceof Friends.Data) assign((Friends.Data)_o1_);
			else if (_o1_ instanceof Friends.Const) assign(((Friends.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Friends _o_) {
			mine = new java.util.HashMap<Long, xbean.FriendInfo>();
			for (java.util.Map.Entry<Long, xbean.FriendInfo> _e_ : _o_.mine.entrySet())
				mine.put(_e_.getKey(), new FriendInfo.Data(_e_.getValue()));
		}

		private void assign(Friends.Data _o_) {
			mine = new java.util.HashMap<Long, xbean.FriendInfo>();
			for (java.util.Map.Entry<Long, xbean.FriendInfo> _e_ : _o_.mine.entrySet())
				mine.put(_e_.getKey(), new FriendInfo.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(mine.size());
			for (java.util.Map.Entry<Long, xbean.FriendInfo> _e_ : mine.entrySet())
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
					mine = new java.util.HashMap<Long, xbean.FriendInfo>(size * 2);
				}
				for (; size > 0; --size)
				{
					long _k_ = 0;
					_k_ = _os_.unmarshal_long();
					xbean.FriendInfo _v_ = xbean.Pod.newFriendInfoData();
					_v_.unmarshal(_os_);
					mine.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.Friends copy() {
			return new Data(this);
		}

		@Override
		public xbean.Friends toData() {
			return new Data(this);
		}

		public xbean.Friends toBean() {
			return new Friends(this, null, null);
		}

		@Override
		public xbean.Friends toDataIf() {
			return this;
		}

		public xbean.Friends toBeanIf() {
			return new Friends(this, null, null);
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
		public java.util.Map<Long, xbean.FriendInfo> getMine() { // key=好友roldId
			return mine;
		}

		@Override
		public java.util.Map<Long, xbean.FriendInfo> getMineAsData() { // key=好友roldId
			return mine;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Friends.Data)) return false;
			Friends.Data _o_ = (Friends.Data) _o1_;
			if (!mine.equals(_o_.mine)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += mine.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(mine);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
