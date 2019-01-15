
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class ArtifactColumn extends xdb.XBean implements xbean.ArtifactColumn {
	private java.util.HashMap<Integer, xbean.Artifact> artifacts; // 

	ArtifactColumn(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		artifacts = new java.util.HashMap<Integer, xbean.Artifact>();
	}

	public ArtifactColumn() {
		this(0, null, null);
	}

	public ArtifactColumn(ArtifactColumn _o_) {
		this(_o_, null, null);
	}

	ArtifactColumn(xbean.ArtifactColumn _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof ArtifactColumn) assign((ArtifactColumn)_o1_);
		else if (_o1_ instanceof ArtifactColumn.Data) assign((ArtifactColumn.Data)_o1_);
		else if (_o1_ instanceof ArtifactColumn.Const) assign(((ArtifactColumn.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(ArtifactColumn _o_) {
		_o_._xdb_verify_unsafe_();
		artifacts = new java.util.HashMap<Integer, xbean.Artifact>();
		for (java.util.Map.Entry<Integer, xbean.Artifact> _e_ : _o_.artifacts.entrySet())
			artifacts.put(_e_.getKey(), new Artifact(_e_.getValue(), this, "artifacts"));
	}

	private void assign(ArtifactColumn.Data _o_) {
		artifacts = new java.util.HashMap<Integer, xbean.Artifact>();
		for (java.util.Map.Entry<Integer, xbean.Artifact> _e_ : _o_.artifacts.entrySet())
			artifacts.put(_e_.getKey(), new Artifact(_e_.getValue(), this, "artifacts"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(artifacts.size());
		for (java.util.Map.Entry<Integer, xbean.Artifact> _e_ : artifacts.entrySet())
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
				artifacts = new java.util.HashMap<Integer, xbean.Artifact>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.Artifact _v_ = new Artifact(0, this, "artifacts");
				_v_.unmarshal(_os_);
				artifacts.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.ArtifactColumn copy() {
		_xdb_verify_unsafe_();
		return new ArtifactColumn(this);
	}

	@Override
	public xbean.ArtifactColumn toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ArtifactColumn toBean() {
		_xdb_verify_unsafe_();
		return new ArtifactColumn(this); // same as copy()
	}

	@Override
	public xbean.ArtifactColumn toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.ArtifactColumn toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.Artifact> getArtifacts() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "artifacts"), artifacts);
	}

	@Override
	public java.util.Map<Integer, xbean.Artifact> getArtifactsAsData() { // 
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.Artifact> artifacts;
		ArtifactColumn _o_ = this;
		artifacts = new java.util.HashMap<Integer, xbean.Artifact>();
		for (java.util.Map.Entry<Integer, xbean.Artifact> _e_ : _o_.artifacts.entrySet())
			artifacts.put(_e_.getKey(), new Artifact.Data(_e_.getValue()));
		return artifacts;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		ArtifactColumn _o_ = null;
		if ( _o1_ instanceof ArtifactColumn ) _o_ = (ArtifactColumn)_o1_;
		else if ( _o1_ instanceof ArtifactColumn.Const ) _o_ = ((ArtifactColumn.Const)_o1_).nThis();
		else return false;
		if (!artifacts.equals(_o_.artifacts)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += artifacts.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(artifacts);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("artifacts"));
		return lb;
	}

	private class Const implements xbean.ArtifactColumn {
		ArtifactColumn nThis() {
			return ArtifactColumn.this;
		}

		@Override
		public xbean.ArtifactColumn copy() {
			return ArtifactColumn.this.copy();
		}

		@Override
		public xbean.ArtifactColumn toData() {
			return ArtifactColumn.this.toData();
		}

		public xbean.ArtifactColumn toBean() {
			return ArtifactColumn.this.toBean();
		}

		@Override
		public xbean.ArtifactColumn toDataIf() {
			return ArtifactColumn.this.toDataIf();
		}

		public xbean.ArtifactColumn toBeanIf() {
			return ArtifactColumn.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.Artifact> getArtifacts() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(artifacts);
		}

		@Override
		public java.util.Map<Integer, xbean.Artifact> getArtifactsAsData() { // 
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.Artifact> artifacts;
			ArtifactColumn _o_ = ArtifactColumn.this;
			artifacts = new java.util.HashMap<Integer, xbean.Artifact>();
			for (java.util.Map.Entry<Integer, xbean.Artifact> _e_ : _o_.artifacts.entrySet())
				artifacts.put(_e_.getKey(), new Artifact.Data(_e_.getValue()));
			return artifacts;
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
			return ArtifactColumn.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return ArtifactColumn.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return ArtifactColumn.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return ArtifactColumn.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return ArtifactColumn.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return ArtifactColumn.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return ArtifactColumn.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return ArtifactColumn.this.hashCode();
		}

		@Override
		public String toString() {
			return ArtifactColumn.this.toString();
		}

	}

	public static final class Data implements xbean.ArtifactColumn {
		private java.util.HashMap<Integer, xbean.Artifact> artifacts; // 

		public Data() {
			artifacts = new java.util.HashMap<Integer, xbean.Artifact>();
		}

		Data(xbean.ArtifactColumn _o1_) {
			if (_o1_ instanceof ArtifactColumn) assign((ArtifactColumn)_o1_);
			else if (_o1_ instanceof ArtifactColumn.Data) assign((ArtifactColumn.Data)_o1_);
			else if (_o1_ instanceof ArtifactColumn.Const) assign(((ArtifactColumn.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(ArtifactColumn _o_) {
			artifacts = new java.util.HashMap<Integer, xbean.Artifact>();
			for (java.util.Map.Entry<Integer, xbean.Artifact> _e_ : _o_.artifacts.entrySet())
				artifacts.put(_e_.getKey(), new Artifact.Data(_e_.getValue()));
		}

		private void assign(ArtifactColumn.Data _o_) {
			artifacts = new java.util.HashMap<Integer, xbean.Artifact>();
			for (java.util.Map.Entry<Integer, xbean.Artifact> _e_ : _o_.artifacts.entrySet())
				artifacts.put(_e_.getKey(), new Artifact.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(artifacts.size());
			for (java.util.Map.Entry<Integer, xbean.Artifact> _e_ : artifacts.entrySet())
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
					artifacts = new java.util.HashMap<Integer, xbean.Artifact>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.Artifact _v_ = xbean.Pod.newArtifactData();
					_v_.unmarshal(_os_);
					artifacts.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.ArtifactColumn copy() {
			return new Data(this);
		}

		@Override
		public xbean.ArtifactColumn toData() {
			return new Data(this);
		}

		public xbean.ArtifactColumn toBean() {
			return new ArtifactColumn(this, null, null);
		}

		@Override
		public xbean.ArtifactColumn toDataIf() {
			return this;
		}

		public xbean.ArtifactColumn toBeanIf() {
			return new ArtifactColumn(this, null, null);
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
		public java.util.Map<Integer, xbean.Artifact> getArtifacts() { // 
			return artifacts;
		}

		@Override
		public java.util.Map<Integer, xbean.Artifact> getArtifactsAsData() { // 
			return artifacts;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof ArtifactColumn.Data)) return false;
			ArtifactColumn.Data _o_ = (ArtifactColumn.Data) _o1_;
			if (!artifacts.equals(_o_.artifacts)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += artifacts.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(artifacts);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
