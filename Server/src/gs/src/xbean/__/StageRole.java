
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class StageRole extends xdb.XBean implements xbean.StageRole {
	private java.util.HashMap<Integer, xbean.StageInfo> stages; // 

	StageRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		stages = new java.util.HashMap<Integer, xbean.StageInfo>();
	}

	public StageRole() {
		this(0, null, null);
	}

	public StageRole(StageRole _o_) {
		this(_o_, null, null);
	}

	StageRole(xbean.StageRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof StageRole) assign((StageRole)_o1_);
		else if (_o1_ instanceof StageRole.Data) assign((StageRole.Data)_o1_);
		else if (_o1_ instanceof StageRole.Const) assign(((StageRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(StageRole _o_) {
		_o_._xdb_verify_unsafe_();
		stages = new java.util.HashMap<Integer, xbean.StageInfo>();
		for (java.util.Map.Entry<Integer, xbean.StageInfo> _e_ : _o_.stages.entrySet())
			stages.put(_e_.getKey(), new StageInfo(_e_.getValue(), this, "stages"));
	}

	private void assign(StageRole.Data _o_) {
		stages = new java.util.HashMap<Integer, xbean.StageInfo>();
		for (java.util.Map.Entry<Integer, xbean.StageInfo> _e_ : _o_.stages.entrySet())
			stages.put(_e_.getKey(), new StageInfo(_e_.getValue(), this, "stages"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(stages.size());
		for (java.util.Map.Entry<Integer, xbean.StageInfo> _e_ : stages.entrySet())
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
				stages = new java.util.HashMap<Integer, xbean.StageInfo>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.StageInfo _v_ = new StageInfo(0, this, "stages");
				_v_.unmarshal(_os_);
				stages.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.StageRole copy() {
		_xdb_verify_unsafe_();
		return new StageRole(this);
	}

	@Override
	public xbean.StageRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.StageRole toBean() {
		_xdb_verify_unsafe_();
		return new StageRole(this); // same as copy()
	}

	@Override
	public xbean.StageRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.StageRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.StageInfo> getStages() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "stages"), stages);
	}

	@Override
	public java.util.Map<Integer, xbean.StageInfo> getStagesAsData() { // 
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.StageInfo> stages;
		StageRole _o_ = this;
		stages = new java.util.HashMap<Integer, xbean.StageInfo>();
		for (java.util.Map.Entry<Integer, xbean.StageInfo> _e_ : _o_.stages.entrySet())
			stages.put(_e_.getKey(), new StageInfo.Data(_e_.getValue()));
		return stages;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		StageRole _o_ = null;
		if ( _o1_ instanceof StageRole ) _o_ = (StageRole)_o1_;
		else if ( _o1_ instanceof StageRole.Const ) _o_ = ((StageRole.Const)_o1_).nThis();
		else return false;
		if (!stages.equals(_o_.stages)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += stages.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(stages);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("stages"));
		return lb;
	}

	private class Const implements xbean.StageRole {
		StageRole nThis() {
			return StageRole.this;
		}

		@Override
		public xbean.StageRole copy() {
			return StageRole.this.copy();
		}

		@Override
		public xbean.StageRole toData() {
			return StageRole.this.toData();
		}

		public xbean.StageRole toBean() {
			return StageRole.this.toBean();
		}

		@Override
		public xbean.StageRole toDataIf() {
			return StageRole.this.toDataIf();
		}

		public xbean.StageRole toBeanIf() {
			return StageRole.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.StageInfo> getStages() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(stages);
		}

		@Override
		public java.util.Map<Integer, xbean.StageInfo> getStagesAsData() { // 
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.StageInfo> stages;
			StageRole _o_ = StageRole.this;
			stages = new java.util.HashMap<Integer, xbean.StageInfo>();
			for (java.util.Map.Entry<Integer, xbean.StageInfo> _e_ : _o_.stages.entrySet())
				stages.put(_e_.getKey(), new StageInfo.Data(_e_.getValue()));
			return stages;
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
			return StageRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return StageRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return StageRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return StageRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return StageRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return StageRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return StageRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return StageRole.this.hashCode();
		}

		@Override
		public String toString() {
			return StageRole.this.toString();
		}

	}

	public static final class Data implements xbean.StageRole {
		private java.util.HashMap<Integer, xbean.StageInfo> stages; // 

		public Data() {
			stages = new java.util.HashMap<Integer, xbean.StageInfo>();
		}

		Data(xbean.StageRole _o1_) {
			if (_o1_ instanceof StageRole) assign((StageRole)_o1_);
			else if (_o1_ instanceof StageRole.Data) assign((StageRole.Data)_o1_);
			else if (_o1_ instanceof StageRole.Const) assign(((StageRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(StageRole _o_) {
			stages = new java.util.HashMap<Integer, xbean.StageInfo>();
			for (java.util.Map.Entry<Integer, xbean.StageInfo> _e_ : _o_.stages.entrySet())
				stages.put(_e_.getKey(), new StageInfo.Data(_e_.getValue()));
		}

		private void assign(StageRole.Data _o_) {
			stages = new java.util.HashMap<Integer, xbean.StageInfo>();
			for (java.util.Map.Entry<Integer, xbean.StageInfo> _e_ : _o_.stages.entrySet())
				stages.put(_e_.getKey(), new StageInfo.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(stages.size());
			for (java.util.Map.Entry<Integer, xbean.StageInfo> _e_ : stages.entrySet())
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
					stages = new java.util.HashMap<Integer, xbean.StageInfo>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.StageInfo _v_ = xbean.Pod.newStageInfoData();
					_v_.unmarshal(_os_);
					stages.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.StageRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.StageRole toData() {
			return new Data(this);
		}

		public xbean.StageRole toBean() {
			return new StageRole(this, null, null);
		}

		@Override
		public xbean.StageRole toDataIf() {
			return this;
		}

		public xbean.StageRole toBeanIf() {
			return new StageRole(this, null, null);
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
		public java.util.Map<Integer, xbean.StageInfo> getStages() { // 
			return stages;
		}

		@Override
		public java.util.Map<Integer, xbean.StageInfo> getStagesAsData() { // 
			return stages;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof StageRole.Data)) return false;
			StageRole.Data _o_ = (StageRole.Data) _o1_;
			if (!stages.equals(_o_.stages)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += stages.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(stages);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
