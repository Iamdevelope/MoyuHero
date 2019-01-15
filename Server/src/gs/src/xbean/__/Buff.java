
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Buff extends xdb.XBean implements xbean.Buff {
	private int id; // buff类型Id，一种类型的buff只能有一个
	private long attachtime; // buff attach时的时间，用于计算剩余时间和检测到期
	private long time; // 计时buff总持续时间（period时的period）
	private int round; // 计数buff剩余回合（period时的count）
	private long amount; // buff的剩余量（period时的initDelay）
	private java.util.HashMap<Integer, Float> effects; // key = effect type id
	private java.util.HashMap<Integer, Float> extdata; // 额外数据，由buff实现者自己定义和使用

	Buff(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		attachtime = 0;
		time = 0;
		round = 0;
		amount = 0;
		effects = new java.util.HashMap<Integer, Float>();
		extdata = new java.util.HashMap<Integer, Float>();
	}

	public Buff() {
		this(0, null, null);
	}

	public Buff(Buff _o_) {
		this(_o_, null, null);
	}

	Buff(xbean.Buff _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Buff) assign((Buff)_o1_);
		else if (_o1_ instanceof Buff.Data) assign((Buff.Data)_o1_);
		else if (_o1_ instanceof Buff.Const) assign(((Buff.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Buff _o_) {
		_o_._xdb_verify_unsafe_();
		id = _o_.id;
		attachtime = _o_.attachtime;
		time = _o_.time;
		round = _o_.round;
		amount = _o_.amount;
		effects = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
			effects.put(_e_.getKey(), _e_.getValue());
		extdata = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.extdata.entrySet())
			extdata.put(_e_.getKey(), _e_.getValue());
	}

	private void assign(Buff.Data _o_) {
		id = _o_.id;
		attachtime = _o_.attachtime;
		time = _o_.time;
		round = _o_.round;
		amount = _o_.amount;
		effects = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
			effects.put(_e_.getKey(), _e_.getValue());
		extdata = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.extdata.entrySet())
			extdata.put(_e_.getKey(), _e_.getValue());
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(id);
		_os_.marshal(attachtime);
		_os_.marshal(time);
		_os_.marshal(round);
		_os_.marshal(amount);
		_os_.compact_uint32(effects.size());
		for (java.util.Map.Entry<Integer, Float> _e_ : effects.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(extdata.size());
		for (java.util.Map.Entry<Integer, Float> _e_ : extdata.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		id = _os_.unmarshal_int();
		attachtime = _os_.unmarshal_long();
		time = _os_.unmarshal_long();
		round = _os_.unmarshal_int();
		amount = _os_.unmarshal_long();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				effects = new java.util.HashMap<Integer, Float>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				float _v_ = 0.0f;
				_v_ = _os_.unmarshal_float();
				effects.put(_k_, _v_);
			}
		}
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				extdata = new java.util.HashMap<Integer, Float>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				float _v_ = 0.0f;
				_v_ = _os_.unmarshal_float();
				extdata.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.Buff copy() {
		_xdb_verify_unsafe_();
		return new Buff(this);
	}

	@Override
	public xbean.Buff toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Buff toBean() {
		_xdb_verify_unsafe_();
		return new Buff(this); // same as copy()
	}

	@Override
	public xbean.Buff toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Buff toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getId() { // buff类型Id，一种类型的buff只能有一个
		_xdb_verify_unsafe_();
		return id;
	}

	@Override
	public long getAttachtime() { // buff attach时的时间，用于计算剩余时间和检测到期
		_xdb_verify_unsafe_();
		return attachtime;
	}

	@Override
	public long getTime() { // 计时buff总持续时间（period时的period）
		_xdb_verify_unsafe_();
		return time;
	}

	@Override
	public int getRound() { // 计数buff剩余回合（period时的count）
		_xdb_verify_unsafe_();
		return round;
	}

	@Override
	public long getAmount() { // buff的剩余量（period时的initDelay）
		_xdb_verify_unsafe_();
		return amount;
	}

	@Override
	public java.util.Map<Integer, Float> getEffects() { // key = effect type id
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "effects"), effects);
	}

	@Override
	public java.util.Map<Integer, Float> getEffectsAsData() { // key = effect type id
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Float> effects;
		Buff _o_ = this;
		effects = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
			effects.put(_e_.getKey(), _e_.getValue());
		return effects;
	}

	@Override
	public java.util.Map<Integer, Float> getExtdata() { // 额外数据，由buff实现者自己定义和使用
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "extdata"), extdata);
	}

	@Override
	public java.util.Map<Integer, Float> getExtdataAsData() { // 额外数据，由buff实现者自己定义和使用
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Float> extdata;
		Buff _o_ = this;
		extdata = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.extdata.entrySet())
			extdata.put(_e_.getKey(), _e_.getValue());
		return extdata;
	}

	@Override
	public void setId(int _v_) { // buff类型Id，一种类型的buff只能有一个
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "id") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, id) {
					public void rollback() { id = _xdb_saved; }
				};}});
		id = _v_;
	}

	@Override
	public void setAttachtime(long _v_) { // buff attach时的时间，用于计算剩余时间和检测到期
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "attachtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, attachtime) {
					public void rollback() { attachtime = _xdb_saved; }
				};}});
		attachtime = _v_;
	}

	@Override
	public void setTime(long _v_) { // 计时buff总持续时间（period时的period）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "time") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, time) {
					public void rollback() { time = _xdb_saved; }
				};}});
		time = _v_;
	}

	@Override
	public void setRound(int _v_) { // 计数buff剩余回合（period时的count）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "round") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, round) {
					public void rollback() { round = _xdb_saved; }
				};}});
		round = _v_;
	}

	@Override
	public void setAmount(long _v_) { // buff的剩余量（period时的initDelay）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "amount") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, amount) {
					public void rollback() { amount = _xdb_saved; }
				};}});
		amount = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Buff _o_ = null;
		if ( _o1_ instanceof Buff ) _o_ = (Buff)_o1_;
		else if ( _o1_ instanceof Buff.Const ) _o_ = ((Buff.Const)_o1_).nThis();
		else return false;
		if (id != _o_.id) return false;
		if (attachtime != _o_.attachtime) return false;
		if (time != _o_.time) return false;
		if (round != _o_.round) return false;
		if (amount != _o_.amount) return false;
		if (!effects.equals(_o_.effects)) return false;
		if (!extdata.equals(_o_.extdata)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += id;
		_h_ += attachtime;
		_h_ += time;
		_h_ += round;
		_h_ += amount;
		_h_ += effects.hashCode();
		_h_ += extdata.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id);
		_sb_.append(",");
		_sb_.append(attachtime);
		_sb_.append(",");
		_sb_.append(time);
		_sb_.append(",");
		_sb_.append(round);
		_sb_.append(",");
		_sb_.append(amount);
		_sb_.append(",");
		_sb_.append(effects);
		_sb_.append(",");
		_sb_.append(extdata);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("id"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("attachtime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("time"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("round"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("amount"));
		lb.add(new xdb.logs.ListenableMap().setVarName("effects"));
		lb.add(new xdb.logs.ListenableMap().setVarName("extdata"));
		return lb;
	}

	private class Const implements xbean.Buff {
		Buff nThis() {
			return Buff.this;
		}

		@Override
		public xbean.Buff copy() {
			return Buff.this.copy();
		}

		@Override
		public xbean.Buff toData() {
			return Buff.this.toData();
		}

		public xbean.Buff toBean() {
			return Buff.this.toBean();
		}

		@Override
		public xbean.Buff toDataIf() {
			return Buff.this.toDataIf();
		}

		public xbean.Buff toBeanIf() {
			return Buff.this.toBeanIf();
		}

		@Override
		public int getId() { // buff类型Id，一种类型的buff只能有一个
			_xdb_verify_unsafe_();
			return id;
		}

		@Override
		public long getAttachtime() { // buff attach时的时间，用于计算剩余时间和检测到期
			_xdb_verify_unsafe_();
			return attachtime;
		}

		@Override
		public long getTime() { // 计时buff总持续时间（period时的period）
			_xdb_verify_unsafe_();
			return time;
		}

		@Override
		public int getRound() { // 计数buff剩余回合（period时的count）
			_xdb_verify_unsafe_();
			return round;
		}

		@Override
		public long getAmount() { // buff的剩余量（period时的initDelay）
			_xdb_verify_unsafe_();
			return amount;
		}

		@Override
		public java.util.Map<Integer, Float> getEffects() { // key = effect type id
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(effects);
		}

		@Override
		public java.util.Map<Integer, Float> getEffectsAsData() { // key = effect type id
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Float> effects;
			Buff _o_ = Buff.this;
			effects = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
				effects.put(_e_.getKey(), _e_.getValue());
			return effects;
		}

		@Override
		public java.util.Map<Integer, Float> getExtdata() { // 额外数据，由buff实现者自己定义和使用
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(extdata);
		}

		@Override
		public java.util.Map<Integer, Float> getExtdataAsData() { // 额外数据，由buff实现者自己定义和使用
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Float> extdata;
			Buff _o_ = Buff.this;
			extdata = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.extdata.entrySet())
				extdata.put(_e_.getKey(), _e_.getValue());
			return extdata;
		}

		@Override
		public void setId(int _v_) { // buff类型Id，一种类型的buff只能有一个
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAttachtime(long _v_) { // buff attach时的时间，用于计算剩余时间和检测到期
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTime(long _v_) { // 计时buff总持续时间（period时的period）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRound(int _v_) { // 计数buff剩余回合（period时的count）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAmount(long _v_) { // buff的剩余量（period时的initDelay）
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
			return Buff.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Buff.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Buff.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Buff.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Buff.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Buff.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Buff.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Buff.this.hashCode();
		}

		@Override
		public String toString() {
			return Buff.this.toString();
		}

	}

	public static final class Data implements xbean.Buff {
		private int id; // buff类型Id，一种类型的buff只能有一个
		private long attachtime; // buff attach时的时间，用于计算剩余时间和检测到期
		private long time; // 计时buff总持续时间（period时的period）
		private int round; // 计数buff剩余回合（period时的count）
		private long amount; // buff的剩余量（period时的initDelay）
		private java.util.HashMap<Integer, Float> effects; // key = effect type id
		private java.util.HashMap<Integer, Float> extdata; // 额外数据，由buff实现者自己定义和使用

		public Data() {
			attachtime = 0;
			time = 0;
			round = 0;
			amount = 0;
			effects = new java.util.HashMap<Integer, Float>();
			extdata = new java.util.HashMap<Integer, Float>();
		}

		Data(xbean.Buff _o1_) {
			if (_o1_ instanceof Buff) assign((Buff)_o1_);
			else if (_o1_ instanceof Buff.Data) assign((Buff.Data)_o1_);
			else if (_o1_ instanceof Buff.Const) assign(((Buff.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Buff _o_) {
			id = _o_.id;
			attachtime = _o_.attachtime;
			time = _o_.time;
			round = _o_.round;
			amount = _o_.amount;
			effects = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
				effects.put(_e_.getKey(), _e_.getValue());
			extdata = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.extdata.entrySet())
				extdata.put(_e_.getKey(), _e_.getValue());
		}

		private void assign(Buff.Data _o_) {
			id = _o_.id;
			attachtime = _o_.attachtime;
			time = _o_.time;
			round = _o_.round;
			amount = _o_.amount;
			effects = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
				effects.put(_e_.getKey(), _e_.getValue());
			extdata = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.extdata.entrySet())
				extdata.put(_e_.getKey(), _e_.getValue());
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(id);
			_os_.marshal(attachtime);
			_os_.marshal(time);
			_os_.marshal(round);
			_os_.marshal(amount);
			_os_.compact_uint32(effects.size());
			for (java.util.Map.Entry<Integer, Float> _e_ : effects.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.compact_uint32(extdata.size());
			for (java.util.Map.Entry<Integer, Float> _e_ : extdata.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			id = _os_.unmarshal_int();
			attachtime = _os_.unmarshal_long();
			time = _os_.unmarshal_long();
			round = _os_.unmarshal_int();
			amount = _os_.unmarshal_long();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					effects = new java.util.HashMap<Integer, Float>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					float _v_ = 0.0f;
					_v_ = _os_.unmarshal_float();
					effects.put(_k_, _v_);
				}
			}
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					extdata = new java.util.HashMap<Integer, Float>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					float _v_ = 0.0f;
					_v_ = _os_.unmarshal_float();
					extdata.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.Buff copy() {
			return new Data(this);
		}

		@Override
		public xbean.Buff toData() {
			return new Data(this);
		}

		public xbean.Buff toBean() {
			return new Buff(this, null, null);
		}

		@Override
		public xbean.Buff toDataIf() {
			return this;
		}

		public xbean.Buff toBeanIf() {
			return new Buff(this, null, null);
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
		public int getId() { // buff类型Id，一种类型的buff只能有一个
			return id;
		}

		@Override
		public long getAttachtime() { // buff attach时的时间，用于计算剩余时间和检测到期
			return attachtime;
		}

		@Override
		public long getTime() { // 计时buff总持续时间（period时的period）
			return time;
		}

		@Override
		public int getRound() { // 计数buff剩余回合（period时的count）
			return round;
		}

		@Override
		public long getAmount() { // buff的剩余量（period时的initDelay）
			return amount;
		}

		@Override
		public java.util.Map<Integer, Float> getEffects() { // key = effect type id
			return effects;
		}

		@Override
		public java.util.Map<Integer, Float> getEffectsAsData() { // key = effect type id
			return effects;
		}

		@Override
		public java.util.Map<Integer, Float> getExtdata() { // 额外数据，由buff实现者自己定义和使用
			return extdata;
		}

		@Override
		public java.util.Map<Integer, Float> getExtdataAsData() { // 额外数据，由buff实现者自己定义和使用
			return extdata;
		}

		@Override
		public void setId(int _v_) { // buff类型Id，一种类型的buff只能有一个
			id = _v_;
		}

		@Override
		public void setAttachtime(long _v_) { // buff attach时的时间，用于计算剩余时间和检测到期
			attachtime = _v_;
		}

		@Override
		public void setTime(long _v_) { // 计时buff总持续时间（period时的period）
			time = _v_;
		}

		@Override
		public void setRound(int _v_) { // 计数buff剩余回合（period时的count）
			round = _v_;
		}

		@Override
		public void setAmount(long _v_) { // buff的剩余量（period时的initDelay）
			amount = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Buff.Data)) return false;
			Buff.Data _o_ = (Buff.Data) _o1_;
			if (id != _o_.id) return false;
			if (attachtime != _o_.attachtime) return false;
			if (time != _o_.time) return false;
			if (round != _o_.round) return false;
			if (amount != _o_.amount) return false;
			if (!effects.equals(_o_.effects)) return false;
			if (!extdata.equals(_o_.extdata)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += id;
			_h_ += attachtime;
			_h_ += time;
			_h_ += round;
			_h_ += amount;
			_h_ += effects.hashCode();
			_h_ += extdata.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(id);
			_sb_.append(",");
			_sb_.append(attachtime);
			_sb_.append(",");
			_sb_.append(time);
			_sb_.append(",");
			_sb_.append(round);
			_sb_.append(",");
			_sb_.append(amount);
			_sb_.append(",");
			_sb_.append(effects);
			_sb_.append(",");
			_sb_.append(extdata);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
