
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class gameactivitys extends xdb.XBean implements xbean.gameactivitys {
	private java.util.HashMap<Integer, xbean.gameactivity> gameactivitymap; // 

	gameactivitys(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>();
	}

	public gameactivitys() {
		this(0, null, null);
	}

	public gameactivitys(gameactivitys _o_) {
		this(_o_, null, null);
	}

	gameactivitys(xbean.gameactivitys _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof gameactivitys) assign((gameactivitys)_o1_);
		else if (_o1_ instanceof gameactivitys.Data) assign((gameactivitys.Data)_o1_);
		else if (_o1_ instanceof gameactivitys.Const) assign(((gameactivitys.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(gameactivitys _o_) {
		_o_._xdb_verify_unsafe_();
		gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>();
		for (java.util.Map.Entry<Integer, xbean.gameactivity> _e_ : _o_.gameactivitymap.entrySet())
			gameactivitymap.put(_e_.getKey(), new gameactivity(_e_.getValue(), this, "gameactivitymap"));
	}

	private void assign(gameactivitys.Data _o_) {
		gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>();
		for (java.util.Map.Entry<Integer, xbean.gameactivity> _e_ : _o_.gameactivitymap.entrySet())
			gameactivitymap.put(_e_.getKey(), new gameactivity(_e_.getValue(), this, "gameactivitymap"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(gameactivitymap.size());
		for (java.util.Map.Entry<Integer, xbean.gameactivity> _e_ : gameactivitymap.entrySet())
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
				gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.gameactivity _v_ = new gameactivity(0, this, "gameactivitymap");
				_v_.unmarshal(_os_);
				gameactivitymap.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.gameactivitys copy() {
		_xdb_verify_unsafe_();
		return new gameactivitys(this);
	}

	@Override
	public xbean.gameactivitys toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.gameactivitys toBean() {
		_xdb_verify_unsafe_();
		return new gameactivitys(this); // same as copy()
	}

	@Override
	public xbean.gameactivitys toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.gameactivitys toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, xbean.gameactivity> getGameactivitymap() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "gameactivitymap"), gameactivitymap);
	}

	@Override
	public java.util.Map<Integer, xbean.gameactivity> getGameactivitymapAsData() { // 
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.gameactivity> gameactivitymap;
		gameactivitys _o_ = this;
		gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>();
		for (java.util.Map.Entry<Integer, xbean.gameactivity> _e_ : _o_.gameactivitymap.entrySet())
			gameactivitymap.put(_e_.getKey(), new gameactivity.Data(_e_.getValue()));
		return gameactivitymap;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		gameactivitys _o_ = null;
		if ( _o1_ instanceof gameactivitys ) _o_ = (gameactivitys)_o1_;
		else if ( _o1_ instanceof gameactivitys.Const ) _o_ = ((gameactivitys.Const)_o1_).nThis();
		else return false;
		if (!gameactivitymap.equals(_o_.gameactivitymap)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += gameactivitymap.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(gameactivitymap);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("gameactivitymap"));
		return lb;
	}

	private class Const implements xbean.gameactivitys {
		gameactivitys nThis() {
			return gameactivitys.this;
		}

		@Override
		public xbean.gameactivitys copy() {
			return gameactivitys.this.copy();
		}

		@Override
		public xbean.gameactivitys toData() {
			return gameactivitys.this.toData();
		}

		public xbean.gameactivitys toBean() {
			return gameactivitys.this.toBean();
		}

		@Override
		public xbean.gameactivitys toDataIf() {
			return gameactivitys.this.toDataIf();
		}

		public xbean.gameactivitys toBeanIf() {
			return gameactivitys.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, xbean.gameactivity> getGameactivitymap() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(gameactivitymap);
		}

		@Override
		public java.util.Map<Integer, xbean.gameactivity> getGameactivitymapAsData() { // 
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.gameactivity> gameactivitymap;
			gameactivitys _o_ = gameactivitys.this;
			gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>();
			for (java.util.Map.Entry<Integer, xbean.gameactivity> _e_ : _o_.gameactivitymap.entrySet())
				gameactivitymap.put(_e_.getKey(), new gameactivity.Data(_e_.getValue()));
			return gameactivitymap;
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
			return gameactivitys.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return gameactivitys.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return gameactivitys.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return gameactivitys.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return gameactivitys.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return gameactivitys.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return gameactivitys.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return gameactivitys.this.hashCode();
		}

		@Override
		public String toString() {
			return gameactivitys.this.toString();
		}

	}

	public static final class Data implements xbean.gameactivitys {
		private java.util.HashMap<Integer, xbean.gameactivity> gameactivitymap; // 

		public Data() {
			gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>();
		}

		Data(xbean.gameactivitys _o1_) {
			if (_o1_ instanceof gameactivitys) assign((gameactivitys)_o1_);
			else if (_o1_ instanceof gameactivitys.Data) assign((gameactivitys.Data)_o1_);
			else if (_o1_ instanceof gameactivitys.Const) assign(((gameactivitys.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(gameactivitys _o_) {
			gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>();
			for (java.util.Map.Entry<Integer, xbean.gameactivity> _e_ : _o_.gameactivitymap.entrySet())
				gameactivitymap.put(_e_.getKey(), new gameactivity.Data(_e_.getValue()));
		}

		private void assign(gameactivitys.Data _o_) {
			gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>();
			for (java.util.Map.Entry<Integer, xbean.gameactivity> _e_ : _o_.gameactivitymap.entrySet())
				gameactivitymap.put(_e_.getKey(), new gameactivity.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(gameactivitymap.size());
			for (java.util.Map.Entry<Integer, xbean.gameactivity> _e_ : gameactivitymap.entrySet())
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
					gameactivitymap = new java.util.HashMap<Integer, xbean.gameactivity>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.gameactivity _v_ = xbean.Pod.newgameactivityData();
					_v_.unmarshal(_os_);
					gameactivitymap.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.gameactivitys copy() {
			return new Data(this);
		}

		@Override
		public xbean.gameactivitys toData() {
			return new Data(this);
		}

		public xbean.gameactivitys toBean() {
			return new gameactivitys(this, null, null);
		}

		@Override
		public xbean.gameactivitys toDataIf() {
			return this;
		}

		public xbean.gameactivitys toBeanIf() {
			return new gameactivitys(this, null, null);
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
		public java.util.Map<Integer, xbean.gameactivity> getGameactivitymap() { // 
			return gameactivitymap;
		}

		@Override
		public java.util.Map<Integer, xbean.gameactivity> getGameactivitymapAsData() { // 
			return gameactivitymap;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof gameactivitys.Data)) return false;
			gameactivitys.Data _o_ = (gameactivitys.Data) _o1_;
			if (!gameactivitymap.equals(_o_.gameactivitymap)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += gameactivitymap.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(gameactivitymap);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
