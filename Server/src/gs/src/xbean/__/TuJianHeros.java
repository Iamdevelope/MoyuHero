
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class TuJianHeros extends xdb.XBean implements xbean.TuJianHeros {
	private java.util.HashMap<Integer, Integer> tujianbox; // 宝箱获取（理论上有key则为已获取）
	private java.util.HashMap<Integer, xbean.TuJianHero> tujianhero; // 获得过的武将
	private java.util.LinkedList<Integer> tjheromaxlevel; // 满级图鉴列表

	TuJianHeros(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		tujianbox = new java.util.HashMap<Integer, Integer>();
		tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>();
		tjheromaxlevel = new java.util.LinkedList<Integer>();
	}

	public TuJianHeros() {
		this(0, null, null);
	}

	public TuJianHeros(TuJianHeros _o_) {
		this(_o_, null, null);
	}

	TuJianHeros(xbean.TuJianHeros _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof TuJianHeros) assign((TuJianHeros)_o1_);
		else if (_o1_ instanceof TuJianHeros.Data) assign((TuJianHeros.Data)_o1_);
		else if (_o1_ instanceof TuJianHeros.Const) assign(((TuJianHeros.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(TuJianHeros _o_) {
		_o_._xdb_verify_unsafe_();
		tujianbox = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tujianbox.entrySet())
			tujianbox.put(_e_.getKey(), _e_.getValue());
		tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>();
		for (java.util.Map.Entry<Integer, xbean.TuJianHero> _e_ : _o_.tujianhero.entrySet())
			tujianhero.put(_e_.getKey(), new TuJianHero(_e_.getValue(), this, "tujianhero"));
		tjheromaxlevel = new java.util.LinkedList<Integer>();
		tjheromaxlevel.addAll(_o_.tjheromaxlevel);
	}

	private void assign(TuJianHeros.Data _o_) {
		tujianbox = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tujianbox.entrySet())
			tujianbox.put(_e_.getKey(), _e_.getValue());
		tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>();
		for (java.util.Map.Entry<Integer, xbean.TuJianHero> _e_ : _o_.tujianhero.entrySet())
			tujianhero.put(_e_.getKey(), new TuJianHero(_e_.getValue(), this, "tujianhero"));
		tjheromaxlevel = new java.util.LinkedList<Integer>();
		tjheromaxlevel.addAll(_o_.tjheromaxlevel);
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(tujianbox.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : tujianbox.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(tujianhero.size());
		for (java.util.Map.Entry<Integer, xbean.TuJianHero> _e_ : tujianhero.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		_os_.compact_uint32(tjheromaxlevel.size());
		for (Integer _v_ : tjheromaxlevel) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				tujianbox = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				tujianbox.put(_k_, _v_);
			}
		}
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.TuJianHero _v_ = new TuJianHero(0, this, "tujianhero");
				_v_.unmarshal(_os_);
				tujianhero.put(_k_, _v_);
			}
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _v_ = 0;
			_v_ = _os_.unmarshal_int();
			tjheromaxlevel.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.TuJianHeros copy() {
		_xdb_verify_unsafe_();
		return new TuJianHeros(this);
	}

	@Override
	public xbean.TuJianHeros toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.TuJianHeros toBean() {
		_xdb_verify_unsafe_();
		return new TuJianHeros(this); // same as copy()
	}

	@Override
	public xbean.TuJianHeros toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.TuJianHeros toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.Map<Integer, Integer> getTujianbox() { // 宝箱获取（理论上有key则为已获取）
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "tujianbox"), tujianbox);
	}

	@Override
	public java.util.Map<Integer, Integer> getTujianboxAsData() { // 宝箱获取（理论上有key则为已获取）
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> tujianbox;
		TuJianHeros _o_ = this;
		tujianbox = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tujianbox.entrySet())
			tujianbox.put(_e_.getKey(), _e_.getValue());
		return tujianbox;
	}

	@Override
	public java.util.Map<Integer, xbean.TuJianHero> getTujianhero() { // 获得过的武将
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "tujianhero"), tujianhero);
	}

	@Override
	public java.util.Map<Integer, xbean.TuJianHero> getTujianheroAsData() { // 获得过的武将
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.TuJianHero> tujianhero;
		TuJianHeros _o_ = this;
		tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>();
		for (java.util.Map.Entry<Integer, xbean.TuJianHero> _e_ : _o_.tujianhero.entrySet())
			tujianhero.put(_e_.getKey(), new TuJianHero.Data(_e_.getValue()));
		return tujianhero;
	}

	@Override
	public java.util.List<Integer> getTjheromaxlevel() { // 满级图鉴列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "tjheromaxlevel"), tjheromaxlevel);
	}

	public java.util.List<Integer> getTjheromaxlevelAsData() { // 满级图鉴列表
		_xdb_verify_unsafe_();
		java.util.List<Integer> tjheromaxlevel;
		TuJianHeros _o_ = this;
		tjheromaxlevel = new java.util.LinkedList<Integer>();
		tjheromaxlevel.addAll(_o_.tjheromaxlevel);
		return tjheromaxlevel;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		TuJianHeros _o_ = null;
		if ( _o1_ instanceof TuJianHeros ) _o_ = (TuJianHeros)_o1_;
		else if ( _o1_ instanceof TuJianHeros.Const ) _o_ = ((TuJianHeros.Const)_o1_).nThis();
		else return false;
		if (!tujianbox.equals(_o_.tujianbox)) return false;
		if (!tujianhero.equals(_o_.tujianhero)) return false;
		if (!tjheromaxlevel.equals(_o_.tjheromaxlevel)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += tujianbox.hashCode();
		_h_ += tujianhero.hashCode();
		_h_ += tjheromaxlevel.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(tujianbox);
		_sb_.append(",");
		_sb_.append(tujianhero);
		_sb_.append(",");
		_sb_.append(tjheromaxlevel);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableMap().setVarName("tujianbox"));
		lb.add(new xdb.logs.ListenableMap().setVarName("tujianhero"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("tjheromaxlevel"));
		return lb;
	}

	private class Const implements xbean.TuJianHeros {
		TuJianHeros nThis() {
			return TuJianHeros.this;
		}

		@Override
		public xbean.TuJianHeros copy() {
			return TuJianHeros.this.copy();
		}

		@Override
		public xbean.TuJianHeros toData() {
			return TuJianHeros.this.toData();
		}

		public xbean.TuJianHeros toBean() {
			return TuJianHeros.this.toBean();
		}

		@Override
		public xbean.TuJianHeros toDataIf() {
			return TuJianHeros.this.toDataIf();
		}

		public xbean.TuJianHeros toBeanIf() {
			return TuJianHeros.this.toBeanIf();
		}

		@Override
		public java.util.Map<Integer, Integer> getTujianbox() { // 宝箱获取（理论上有key则为已获取）
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(tujianbox);
		}

		@Override
		public java.util.Map<Integer, Integer> getTujianboxAsData() { // 宝箱获取（理论上有key则为已获取）
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> tujianbox;
			TuJianHeros _o_ = TuJianHeros.this;
			tujianbox = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tujianbox.entrySet())
				tujianbox.put(_e_.getKey(), _e_.getValue());
			return tujianbox;
		}

		@Override
		public java.util.Map<Integer, xbean.TuJianHero> getTujianhero() { // 获得过的武将
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(tujianhero);
		}

		@Override
		public java.util.Map<Integer, xbean.TuJianHero> getTujianheroAsData() { // 获得过的武将
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.TuJianHero> tujianhero;
			TuJianHeros _o_ = TuJianHeros.this;
			tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>();
			for (java.util.Map.Entry<Integer, xbean.TuJianHero> _e_ : _o_.tujianhero.entrySet())
				tujianhero.put(_e_.getKey(), new TuJianHero.Data(_e_.getValue()));
			return tujianhero;
		}

		@Override
		public java.util.List<Integer> getTjheromaxlevel() { // 满级图鉴列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(tjheromaxlevel);
		}

		public java.util.List<Integer> getTjheromaxlevelAsData() { // 满级图鉴列表
			_xdb_verify_unsafe_();
			java.util.List<Integer> tjheromaxlevel;
			TuJianHeros _o_ = TuJianHeros.this;
		tjheromaxlevel = new java.util.LinkedList<Integer>();
		tjheromaxlevel.addAll(_o_.tjheromaxlevel);
			return tjheromaxlevel;
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
			return TuJianHeros.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return TuJianHeros.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return TuJianHeros.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return TuJianHeros.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return TuJianHeros.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return TuJianHeros.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return TuJianHeros.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return TuJianHeros.this.hashCode();
		}

		@Override
		public String toString() {
			return TuJianHeros.this.toString();
		}

	}

	public static final class Data implements xbean.TuJianHeros {
		private java.util.HashMap<Integer, Integer> tujianbox; // 宝箱获取（理论上有key则为已获取）
		private java.util.HashMap<Integer, xbean.TuJianHero> tujianhero; // 获得过的武将
		private java.util.LinkedList<Integer> tjheromaxlevel; // 满级图鉴列表

		public Data() {
			tujianbox = new java.util.HashMap<Integer, Integer>();
			tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>();
			tjheromaxlevel = new java.util.LinkedList<Integer>();
		}

		Data(xbean.TuJianHeros _o1_) {
			if (_o1_ instanceof TuJianHeros) assign((TuJianHeros)_o1_);
			else if (_o1_ instanceof TuJianHeros.Data) assign((TuJianHeros.Data)_o1_);
			else if (_o1_ instanceof TuJianHeros.Const) assign(((TuJianHeros.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(TuJianHeros _o_) {
			tujianbox = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tujianbox.entrySet())
				tujianbox.put(_e_.getKey(), _e_.getValue());
			tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>();
			for (java.util.Map.Entry<Integer, xbean.TuJianHero> _e_ : _o_.tujianhero.entrySet())
				tujianhero.put(_e_.getKey(), new TuJianHero.Data(_e_.getValue()));
			tjheromaxlevel = new java.util.LinkedList<Integer>();
			tjheromaxlevel.addAll(_o_.tjheromaxlevel);
		}

		private void assign(TuJianHeros.Data _o_) {
			tujianbox = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tujianbox.entrySet())
				tujianbox.put(_e_.getKey(), _e_.getValue());
			tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>();
			for (java.util.Map.Entry<Integer, xbean.TuJianHero> _e_ : _o_.tujianhero.entrySet())
				tujianhero.put(_e_.getKey(), new TuJianHero.Data(_e_.getValue()));
			tjheromaxlevel = new java.util.LinkedList<Integer>();
			tjheromaxlevel.addAll(_o_.tjheromaxlevel);
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(tujianbox.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : tujianbox.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.compact_uint32(tujianhero.size());
			for (java.util.Map.Entry<Integer, xbean.TuJianHero> _e_ : tujianhero.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			_os_.compact_uint32(tjheromaxlevel.size());
			for (Integer _v_ : tjheromaxlevel) {
				_os_.marshal(_v_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					tujianbox = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					tujianbox.put(_k_, _v_);
				}
			}
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					tujianhero = new java.util.HashMap<Integer, xbean.TuJianHero>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.TuJianHero _v_ = xbean.Pod.newTuJianHeroData();
					_v_.unmarshal(_os_);
					tujianhero.put(_k_, _v_);
				}
			}
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				tjheromaxlevel.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.TuJianHeros copy() {
			return new Data(this);
		}

		@Override
		public xbean.TuJianHeros toData() {
			return new Data(this);
		}

		public xbean.TuJianHeros toBean() {
			return new TuJianHeros(this, null, null);
		}

		@Override
		public xbean.TuJianHeros toDataIf() {
			return this;
		}

		public xbean.TuJianHeros toBeanIf() {
			return new TuJianHeros(this, null, null);
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
		public java.util.Map<Integer, Integer> getTujianbox() { // 宝箱获取（理论上有key则为已获取）
			return tujianbox;
		}

		@Override
		public java.util.Map<Integer, Integer> getTujianboxAsData() { // 宝箱获取（理论上有key则为已获取）
			return tujianbox;
		}

		@Override
		public java.util.Map<Integer, xbean.TuJianHero> getTujianhero() { // 获得过的武将
			return tujianhero;
		}

		@Override
		public java.util.Map<Integer, xbean.TuJianHero> getTujianheroAsData() { // 获得过的武将
			return tujianhero;
		}

		@Override
		public java.util.List<Integer> getTjheromaxlevel() { // 满级图鉴列表
			return tjheromaxlevel;
		}

		@Override
		public java.util.List<Integer> getTjheromaxlevelAsData() { // 满级图鉴列表
			return tjheromaxlevel;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof TuJianHeros.Data)) return false;
			TuJianHeros.Data _o_ = (TuJianHeros.Data) _o1_;
			if (!tujianbox.equals(_o_.tujianbox)) return false;
			if (!tujianhero.equals(_o_.tujianhero)) return false;
			if (!tjheromaxlevel.equals(_o_.tjheromaxlevel)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += tujianbox.hashCode();
			_h_ += tujianhero.hashCode();
			_h_ += tjheromaxlevel.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(tujianbox);
			_sb_.append(",");
			_sb_.append(tujianhero);
			_sb_.append(",");
			_sb_.append(tjheromaxlevel);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
