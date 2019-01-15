
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class BuffAgent extends xdb.XBean implements xbean.BuffAgent {
	private java.util.HashMap<Integer, xbean.Buff> buffs; // key为buffId

	BuffAgent(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		buffs = new java.util.HashMap<Integer, xbean.Buff>();
	}

	public BuffAgent() {
		this(0, null, null);
	}

	public BuffAgent(BuffAgent _o_) {
		this(_o_, null, null);
	}

	BuffAgent(xbean.BuffAgent _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof BuffAgent) assign((BuffAgent)_o1_);
		else if (_o1_ instanceof BuffAgent.Data) assign((BuffAgent.Data)_o1_);
		else if (_o1_ instanceof BuffAgent.Const) assign(((BuffAgent.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(BuffAgent _o_) {
		_o_._xdb_verify_unsafe_();
		buffs = new java.util.HashMap<Integer, xbean.Buff>();
		for (java.util.Map.Entry<Integer, xbean.Buff> _e_ : _o_.buffs.entrySet())
			buffs.put(_e_.getKey(), new Buff(_e_.getValue(), this, "buffs"));
	}

	private void assign(BuffAgent.Data _o_) {
		buffs = new java.util.HashMap<Integer, xbean.Buff>();
		for (java.util.Map.Entry<Integer, xbean.Buff> _e_ : _o_.buffs.entrySet())
			buffs.put(_e_.getKey(), new Buff(_e_.getValue(), this, "buffs"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(buffs.size());
		for (java.util.Map.Entry<Integer, xbean.Buff> _e_ : buffs.entrySet())
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
				buffs = new java.util.HashMap<Integer, xbean.Buff>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.Buff _v_ = new Buff(0, this, "buffs");
				_v_.unmarshal(_os_);
				buffs.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.BuffAgent copy() {
		_xdb_verify_unsafe_();
		return new BuffAgent(this);
	}

	@Override
	public xbean.BuffAgent toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BuffAgent toBean() {
		_xdb_verify_unsafe_();
		return new BuffAgent(this); // same as copy()
	}

	@Override
	public xbean.BuffAgent toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BuffAgent toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.Buff> getBuffs() { // key为buffId
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "buffs"), buffs);
	}

	@Override
	public java.util.Map<Integer, xbean.Buff> getBuffsAsData() { // key为buffId
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.Buff> buffs;
		BuffAgent _o_ = this;
		buffs = new java.util.HashMap<Integer, xbean.Buff>();
		for (java.util.Map.Entry<Integer, xbean.Buff> _e_ : _o_.buffs.entrySet())
			buffs.put(_e_.getKey(), new Buff.Data(_e_.getValue()));
		return buffs;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		BuffAgent _o_ = null;
		if ( _o1_ instanceof BuffAgent ) _o_ = (BuffAgent)_o1_;
		else if ( _o1_ instanceof BuffAgent.Const ) _o_ = ((BuffAgent.Const)_o1_).nThis();
		else return false;
		if (!buffs.equals(_o_.buffs)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += buffs.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(buffs);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("buffs"));
		return lb;
	}

	private class Const implements xbean.BuffAgent {
		BuffAgent nThis() {
			return BuffAgent.this;
		}

		@Override
		public xbean.BuffAgent copy() {
			return BuffAgent.this.copy();
		}

		@Override
		public xbean.BuffAgent toData() {
			return BuffAgent.this.toData();
		}

		public xbean.BuffAgent toBean() {
			return BuffAgent.this.toBean();
		}

		@Override
		public xbean.BuffAgent toDataIf() {
			return BuffAgent.this.toDataIf();
		}

		public xbean.BuffAgent toBeanIf() {
			return BuffAgent.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.Buff> getBuffs() { // key为buffId
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(buffs);
		}

		@Override
		public java.util.Map<Integer, xbean.Buff> getBuffsAsData() { // key为buffId
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.Buff> buffs;
			BuffAgent _o_ = BuffAgent.this;
			buffs = new java.util.HashMap<Integer, xbean.Buff>();
			for (java.util.Map.Entry<Integer, xbean.Buff> _e_ : _o_.buffs.entrySet())
				buffs.put(_e_.getKey(), new Buff.Data(_e_.getValue()));
			return buffs;
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
			return BuffAgent.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return BuffAgent.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return BuffAgent.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return BuffAgent.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return BuffAgent.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return BuffAgent.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return BuffAgent.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return BuffAgent.this.hashCode();
		}

		@Override
		public String toString() {
			return BuffAgent.this.toString();
		}

	}

	public static final class Data implements xbean.BuffAgent {
		private java.util.HashMap<Integer, xbean.Buff> buffs; // key为buffId

		public Data() {
			buffs = new java.util.HashMap<Integer, xbean.Buff>();
		}

		Data(xbean.BuffAgent _o1_) {
			if (_o1_ instanceof BuffAgent) assign((BuffAgent)_o1_);
			else if (_o1_ instanceof BuffAgent.Data) assign((BuffAgent.Data)_o1_);
			else if (_o1_ instanceof BuffAgent.Const) assign(((BuffAgent.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(BuffAgent _o_) {
			buffs = new java.util.HashMap<Integer, xbean.Buff>();
			for (java.util.Map.Entry<Integer, xbean.Buff> _e_ : _o_.buffs.entrySet())
				buffs.put(_e_.getKey(), new Buff.Data(_e_.getValue()));
		}

		private void assign(BuffAgent.Data _o_) {
			buffs = new java.util.HashMap<Integer, xbean.Buff>();
			for (java.util.Map.Entry<Integer, xbean.Buff> _e_ : _o_.buffs.entrySet())
				buffs.put(_e_.getKey(), new Buff.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(buffs.size());
			for (java.util.Map.Entry<Integer, xbean.Buff> _e_ : buffs.entrySet())
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
					buffs = new java.util.HashMap<Integer, xbean.Buff>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.Buff _v_ = xbean.Pod.newBuffData();
					_v_.unmarshal(_os_);
					buffs.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.BuffAgent copy() {
			return new Data(this);
		}

		@Override
		public xbean.BuffAgent toData() {
			return new Data(this);
		}

		public xbean.BuffAgent toBean() {
			return new BuffAgent(this, null, null);
		}

		@Override
		public xbean.BuffAgent toDataIf() {
			return this;
		}

		public xbean.BuffAgent toBeanIf() {
			return new BuffAgent(this, null, null);
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
		public java.util.Map<Integer, xbean.Buff> getBuffs() { // key为buffId
			return buffs;
		}

		@Override
		public java.util.Map<Integer, xbean.Buff> getBuffsAsData() { // key为buffId
			return buffs;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof BuffAgent.Data)) return false;
			BuffAgent.Data _o_ = (BuffAgent.Data) _o1_;
			if (!buffs.equals(_o_.buffs)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += buffs.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(buffs);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
