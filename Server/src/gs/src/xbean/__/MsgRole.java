
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class MsgRole extends xdb.XBean implements xbean.MsgRole {
	private java.util.LinkedList<xbean.SysMsg> sysmsgs; // 

	MsgRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		sysmsgs = new java.util.LinkedList<xbean.SysMsg>();
	}

	public MsgRole() {
		this(0, null, null);
	}

	public MsgRole(MsgRole _o_) {
		this(_o_, null, null);
	}

	MsgRole(xbean.MsgRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof MsgRole) assign((MsgRole)_o1_);
		else if (_o1_ instanceof MsgRole.Data) assign((MsgRole.Data)_o1_);
		else if (_o1_ instanceof MsgRole.Const) assign(((MsgRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(MsgRole _o_) {
		_o_._xdb_verify_unsafe_();
		sysmsgs = new java.util.LinkedList<xbean.SysMsg>();
		for (xbean.SysMsg _v_ : _o_.sysmsgs)
			sysmsgs.add(new SysMsg(_v_, this, "sysmsgs"));
	}

	private void assign(MsgRole.Data _o_) {
		sysmsgs = new java.util.LinkedList<xbean.SysMsg>();
		for (xbean.SysMsg _v_ : _o_.sysmsgs)
			sysmsgs.add(new SysMsg(_v_, this, "sysmsgs"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(sysmsgs.size());
		for (xbean.SysMsg _v_ : sysmsgs) {
			_v_.marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.SysMsg _v_ = new SysMsg(0, this, "sysmsgs");
			_v_.unmarshal(_os_);
			sysmsgs.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.MsgRole copy() {
		_xdb_verify_unsafe_();
		return new MsgRole(this);
	}

	@Override
	public xbean.MsgRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.MsgRole toBean() {
		_xdb_verify_unsafe_();
		return new MsgRole(this); // same as copy()
	}

	@Override
	public xbean.MsgRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.MsgRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.SysMsg> getSysmsgs() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "sysmsgs"), sysmsgs);
	}

	public java.util.List<xbean.SysMsg> getSysmsgsAsData() { // 
		_xdb_verify_unsafe_();
		java.util.List<xbean.SysMsg> sysmsgs;
		MsgRole _o_ = this;
		sysmsgs = new java.util.LinkedList<xbean.SysMsg>();
		for (xbean.SysMsg _v_ : _o_.sysmsgs)
			sysmsgs.add(new SysMsg.Data(_v_));
		return sysmsgs;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		MsgRole _o_ = null;
		if ( _o1_ instanceof MsgRole ) _o_ = (MsgRole)_o1_;
		else if ( _o1_ instanceof MsgRole.Const ) _o_ = ((MsgRole.Const)_o1_).nThis();
		else return false;
		if (!sysmsgs.equals(_o_.sysmsgs)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += sysmsgs.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(sysmsgs);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("sysmsgs"));
		return lb;
	}

	private class Const implements xbean.MsgRole {
		MsgRole nThis() {
			return MsgRole.this;
		}

		@Override
		public xbean.MsgRole copy() {
			return MsgRole.this.copy();
		}

		@Override
		public xbean.MsgRole toData() {
			return MsgRole.this.toData();
		}

		public xbean.MsgRole toBean() {
			return MsgRole.this.toBean();
		}

		@Override
		public xbean.MsgRole toDataIf() {
			return MsgRole.this.toDataIf();
		}

		public xbean.MsgRole toBeanIf() {
			return MsgRole.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.SysMsg> getSysmsgs() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(sysmsgs);
		}

		public java.util.List<xbean.SysMsg> getSysmsgsAsData() { // 
			_xdb_verify_unsafe_();
			java.util.List<xbean.SysMsg> sysmsgs;
			MsgRole _o_ = MsgRole.this;
		sysmsgs = new java.util.LinkedList<xbean.SysMsg>();
		for (xbean.SysMsg _v_ : _o_.sysmsgs)
			sysmsgs.add(new SysMsg.Data(_v_));
			return sysmsgs;
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
			return MsgRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return MsgRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return MsgRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return MsgRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return MsgRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return MsgRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return MsgRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return MsgRole.this.hashCode();
		}

		@Override
		public String toString() {
			return MsgRole.this.toString();
		}

	}

	public static final class Data implements xbean.MsgRole {
		private java.util.LinkedList<xbean.SysMsg> sysmsgs; // 

		public Data() {
			sysmsgs = new java.util.LinkedList<xbean.SysMsg>();
		}

		Data(xbean.MsgRole _o1_) {
			if (_o1_ instanceof MsgRole) assign((MsgRole)_o1_);
			else if (_o1_ instanceof MsgRole.Data) assign((MsgRole.Data)_o1_);
			else if (_o1_ instanceof MsgRole.Const) assign(((MsgRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(MsgRole _o_) {
			sysmsgs = new java.util.LinkedList<xbean.SysMsg>();
			for (xbean.SysMsg _v_ : _o_.sysmsgs)
				sysmsgs.add(new SysMsg.Data(_v_));
		}

		private void assign(MsgRole.Data _o_) {
			sysmsgs = new java.util.LinkedList<xbean.SysMsg>();
			for (xbean.SysMsg _v_ : _o_.sysmsgs)
				sysmsgs.add(new SysMsg.Data(_v_));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(sysmsgs.size());
			for (xbean.SysMsg _v_ : sysmsgs) {
				_v_.marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.SysMsg _v_ = xbean.Pod.newSysMsgData();
				_v_.unmarshal(_os_);
				sysmsgs.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.MsgRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.MsgRole toData() {
			return new Data(this);
		}

		public xbean.MsgRole toBean() {
			return new MsgRole(this, null, null);
		}

		@Override
		public xbean.MsgRole toDataIf() {
			return this;
		}

		public xbean.MsgRole toBeanIf() {
			return new MsgRole(this, null, null);
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
		public java.util.List<xbean.SysMsg> getSysmsgs() { // 
			return sysmsgs;
		}

		@Override
		public java.util.List<xbean.SysMsg> getSysmsgsAsData() { // 
			return sysmsgs;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof MsgRole.Data)) return false;
			MsgRole.Data _o_ = (MsgRole.Data) _o1_;
			if (!sysmsgs.equals(_o_.sysmsgs)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += sysmsgs.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(sysmsgs);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
