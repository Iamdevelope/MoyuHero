
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class stagetanxian extends xdb.XBean implements xbean.stagetanxian {
	private java.util.LinkedList<xbean.tanxian> stagetanxian; // 每章节探险列表

	stagetanxian(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		stagetanxian = new java.util.LinkedList<xbean.tanxian>();
	}

	public stagetanxian() {
		this(0, null, null);
	}

	public stagetanxian(stagetanxian _o_) {
		this(_o_, null, null);
	}

	stagetanxian(xbean.stagetanxian _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof stagetanxian) assign((stagetanxian)_o1_);
		else if (_o1_ instanceof stagetanxian.Data) assign((stagetanxian.Data)_o1_);
		else if (_o1_ instanceof stagetanxian.Const) assign(((stagetanxian.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(stagetanxian _o_) {
		_o_._xdb_verify_unsafe_();
		stagetanxian = new java.util.LinkedList<xbean.tanxian>();
		for (xbean.tanxian _v_ : _o_.stagetanxian)
			stagetanxian.add(new tanxian(_v_, this, "stagetanxian"));
	}

	private void assign(stagetanxian.Data _o_) {
		stagetanxian = new java.util.LinkedList<xbean.tanxian>();
		for (xbean.tanxian _v_ : _o_.stagetanxian)
			stagetanxian.add(new tanxian(_v_, this, "stagetanxian"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(stagetanxian.size());
		for (xbean.tanxian _v_ : stagetanxian) {
			_v_.marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.tanxian _v_ = new tanxian(0, this, "stagetanxian");
			_v_.unmarshal(_os_);
			stagetanxian.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.stagetanxian copy() {
		_xdb_verify_unsafe_();
		return new stagetanxian(this);
	}

	@Override
	public xbean.stagetanxian toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.stagetanxian toBean() {
		_xdb_verify_unsafe_();
		return new stagetanxian(this); // same as copy()
	}

	@Override
	public xbean.stagetanxian toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.stagetanxian toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.tanxian> getStagetanxian() { // 每章节探险列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "stagetanxian"), stagetanxian);
	}

	public java.util.List<xbean.tanxian> getStagetanxianAsData() { // 每章节探险列表
		_xdb_verify_unsafe_();
		java.util.List<xbean.tanxian> stagetanxian;
		stagetanxian _o_ = this;
		stagetanxian = new java.util.LinkedList<xbean.tanxian>();
		for (xbean.tanxian _v_ : _o_.stagetanxian)
			stagetanxian.add(new tanxian.Data(_v_));
		return stagetanxian;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		stagetanxian _o_ = null;
		if ( _o1_ instanceof stagetanxian ) _o_ = (stagetanxian)_o1_;
		else if ( _o1_ instanceof stagetanxian.Const ) _o_ = ((stagetanxian.Const)_o1_).nThis();
		else return false;
		if (!stagetanxian.equals(_o_.stagetanxian)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += stagetanxian.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(stagetanxian);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("stagetanxian"));
		return lb;
	}

	private class Const implements xbean.stagetanxian {
		stagetanxian nThis() {
			return stagetanxian.this;
		}

		@Override
		public xbean.stagetanxian copy() {
			return stagetanxian.this.copy();
		}

		@Override
		public xbean.stagetanxian toData() {
			return stagetanxian.this.toData();
		}

		public xbean.stagetanxian toBean() {
			return stagetanxian.this.toBean();
		}

		@Override
		public xbean.stagetanxian toDataIf() {
			return stagetanxian.this.toDataIf();
		}

		public xbean.stagetanxian toBeanIf() {
			return stagetanxian.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.tanxian> getStagetanxian() { // 每章节探险列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(stagetanxian);
		}

		public java.util.List<xbean.tanxian> getStagetanxianAsData() { // 每章节探险列表
			_xdb_verify_unsafe_();
			java.util.List<xbean.tanxian> stagetanxian;
			stagetanxian _o_ = stagetanxian.this;
		stagetanxian = new java.util.LinkedList<xbean.tanxian>();
		for (xbean.tanxian _v_ : _o_.stagetanxian)
			stagetanxian.add(new tanxian.Data(_v_));
			return stagetanxian;
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
			return stagetanxian.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return stagetanxian.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return stagetanxian.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return stagetanxian.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return stagetanxian.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return stagetanxian.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return stagetanxian.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return stagetanxian.this.hashCode();
		}

		@Override
		public String toString() {
			return stagetanxian.this.toString();
		}

	}

	public static final class Data implements xbean.stagetanxian {
		private java.util.LinkedList<xbean.tanxian> stagetanxian; // 每章节探险列表

		public Data() {
			stagetanxian = new java.util.LinkedList<xbean.tanxian>();
		}

		Data(xbean.stagetanxian _o1_) {
			if (_o1_ instanceof stagetanxian) assign((stagetanxian)_o1_);
			else if (_o1_ instanceof stagetanxian.Data) assign((stagetanxian.Data)_o1_);
			else if (_o1_ instanceof stagetanxian.Const) assign(((stagetanxian.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(stagetanxian _o_) {
			stagetanxian = new java.util.LinkedList<xbean.tanxian>();
			for (xbean.tanxian _v_ : _o_.stagetanxian)
				stagetanxian.add(new tanxian.Data(_v_));
		}

		private void assign(stagetanxian.Data _o_) {
			stagetanxian = new java.util.LinkedList<xbean.tanxian>();
			for (xbean.tanxian _v_ : _o_.stagetanxian)
				stagetanxian.add(new tanxian.Data(_v_));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(stagetanxian.size());
			for (xbean.tanxian _v_ : stagetanxian) {
				_v_.marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.tanxian _v_ = xbean.Pod.newtanxianData();
				_v_.unmarshal(_os_);
				stagetanxian.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.stagetanxian copy() {
			return new Data(this);
		}

		@Override
		public xbean.stagetanxian toData() {
			return new Data(this);
		}

		public xbean.stagetanxian toBean() {
			return new stagetanxian(this, null, null);
		}

		@Override
		public xbean.stagetanxian toDataIf() {
			return this;
		}

		public xbean.stagetanxian toBeanIf() {
			return new stagetanxian(this, null, null);
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
		public java.util.List<xbean.tanxian> getStagetanxian() { // 每章节探险列表
			return stagetanxian;
		}

		@Override
		public java.util.List<xbean.tanxian> getStagetanxianAsData() { // 每章节探险列表
			return stagetanxian;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof stagetanxian.Data)) return false;
			stagetanxian.Data _o_ = (stagetanxian.Data) _o1_;
			if (!stagetanxian.equals(_o_.stagetanxian)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += stagetanxian.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(stagetanxian);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
