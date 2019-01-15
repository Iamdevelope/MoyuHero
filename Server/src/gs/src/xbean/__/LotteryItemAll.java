
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class LotteryItemAll extends xdb.XBean implements xbean.LotteryItemAll {
	private int mapkey; // 第几层
	private int mapvalue; // 第几个
	private java.util.LinkedList<Integer> superlist; // 遗迹宝藏特殊list
	private long monthfirsttime; // 月卡首刷时间
	private long freelotterytime; // 免费单抽到期时间
	private long lastrefreshtime; // 上次刷新时间（每日刷新）
	private java.util.HashMap<Integer, xbean.LotteryItemlayer> lotteryitemmap; // 遗迹宝藏总信息

	LotteryItemAll(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		superlist = new java.util.LinkedList<Integer>();
		lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>();
	}

	public LotteryItemAll() {
		this(0, null, null);
	}

	public LotteryItemAll(LotteryItemAll _o_) {
		this(_o_, null, null);
	}

	LotteryItemAll(xbean.LotteryItemAll _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof LotteryItemAll) assign((LotteryItemAll)_o1_);
		else if (_o1_ instanceof LotteryItemAll.Data) assign((LotteryItemAll.Data)_o1_);
		else if (_o1_ instanceof LotteryItemAll.Const) assign(((LotteryItemAll.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(LotteryItemAll _o_) {
		_o_._xdb_verify_unsafe_();
		mapkey = _o_.mapkey;
		mapvalue = _o_.mapvalue;
		superlist = new java.util.LinkedList<Integer>();
		superlist.addAll(_o_.superlist);
		monthfirsttime = _o_.monthfirsttime;
		freelotterytime = _o_.freelotterytime;
		lastrefreshtime = _o_.lastrefreshtime;
		lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>();
		for (java.util.Map.Entry<Integer, xbean.LotteryItemlayer> _e_ : _o_.lotteryitemmap.entrySet())
			lotteryitemmap.put(_e_.getKey(), new LotteryItemlayer(_e_.getValue(), this, "lotteryitemmap"));
	}

	private void assign(LotteryItemAll.Data _o_) {
		mapkey = _o_.mapkey;
		mapvalue = _o_.mapvalue;
		superlist = new java.util.LinkedList<Integer>();
		superlist.addAll(_o_.superlist);
		monthfirsttime = _o_.monthfirsttime;
		freelotterytime = _o_.freelotterytime;
		lastrefreshtime = _o_.lastrefreshtime;
		lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>();
		for (java.util.Map.Entry<Integer, xbean.LotteryItemlayer> _e_ : _o_.lotteryitemmap.entrySet())
			lotteryitemmap.put(_e_.getKey(), new LotteryItemlayer(_e_.getValue(), this, "lotteryitemmap"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(mapkey);
		_os_.marshal(mapvalue);
		_os_.compact_uint32(superlist.size());
		for (Integer _v_ : superlist) {
			_os_.marshal(_v_);
		}
		_os_.marshal(monthfirsttime);
		_os_.marshal(freelotterytime);
		_os_.marshal(lastrefreshtime);
		_os_.compact_uint32(lotteryitemmap.size());
		for (java.util.Map.Entry<Integer, xbean.LotteryItemlayer> _e_ : lotteryitemmap.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		mapkey = _os_.unmarshal_int();
		mapvalue = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _v_ = 0;
			_v_ = _os_.unmarshal_int();
			superlist.add(_v_);
		}
		monthfirsttime = _os_.unmarshal_long();
		freelotterytime = _os_.unmarshal_long();
		lastrefreshtime = _os_.unmarshal_long();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.LotteryItemlayer _v_ = new LotteryItemlayer(0, this, "lotteryitemmap");
				_v_.unmarshal(_os_);
				lotteryitemmap.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.LotteryItemAll copy() {
		_xdb_verify_unsafe_();
		return new LotteryItemAll(this);
	}

	@Override
	public xbean.LotteryItemAll toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LotteryItemAll toBean() {
		_xdb_verify_unsafe_();
		return new LotteryItemAll(this); // same as copy()
	}

	@Override
	public xbean.LotteryItemAll toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LotteryItemAll toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getMapkey() { // 第几层
		_xdb_verify_unsafe_();
		return mapkey;
	}

	@Override
	public int getMapvalue() { // 第几个
		_xdb_verify_unsafe_();
		return mapvalue;
	}

	@Override
	public java.util.List<Integer> getSuperlist() { // 遗迹宝藏特殊list
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "superlist"), superlist);
	}

	public java.util.List<Integer> getSuperlistAsData() { // 遗迹宝藏特殊list
		_xdb_verify_unsafe_();
		java.util.List<Integer> superlist;
		LotteryItemAll _o_ = this;
		superlist = new java.util.LinkedList<Integer>();
		superlist.addAll(_o_.superlist);
		return superlist;
	}

	@Override
	public long getMonthfirsttime() { // 月卡首刷时间
		_xdb_verify_unsafe_();
		return monthfirsttime;
	}

	@Override
	public long getFreelotterytime() { // 免费单抽到期时间
		_xdb_verify_unsafe_();
		return freelotterytime;
	}

	@Override
	public long getLastrefreshtime() { // 上次刷新时间（每日刷新）
		_xdb_verify_unsafe_();
		return lastrefreshtime;
	}

	@Override
	public java.util.Map<Integer, xbean.LotteryItemlayer> getLotteryitemmap() { // 遗迹宝藏总信息
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "lotteryitemmap"), lotteryitemmap);
	}

	@Override
	public java.util.Map<Integer, xbean.LotteryItemlayer> getLotteryitemmapAsData() { // 遗迹宝藏总信息
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.LotteryItemlayer> lotteryitemmap;
		LotteryItemAll _o_ = this;
		lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>();
		for (java.util.Map.Entry<Integer, xbean.LotteryItemlayer> _e_ : _o_.lotteryitemmap.entrySet())
			lotteryitemmap.put(_e_.getKey(), new LotteryItemlayer.Data(_e_.getValue()));
		return lotteryitemmap;
	}

	@Override
	public void setMapkey(int _v_) { // 第几层
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "mapkey") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, mapkey) {
					public void rollback() { mapkey = _xdb_saved; }
				};}});
		mapkey = _v_;
	}

	@Override
	public void setMapvalue(int _v_) { // 第几个
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "mapvalue") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, mapvalue) {
					public void rollback() { mapvalue = _xdb_saved; }
				};}});
		mapvalue = _v_;
	}

	@Override
	public void setMonthfirsttime(long _v_) { // 月卡首刷时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "monthfirsttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, monthfirsttime) {
					public void rollback() { monthfirsttime = _xdb_saved; }
				};}});
		monthfirsttime = _v_;
	}

	@Override
	public void setFreelotterytime(long _v_) { // 免费单抽到期时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "freelotterytime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, freelotterytime) {
					public void rollback() { freelotterytime = _xdb_saved; }
				};}});
		freelotterytime = _v_;
	}

	@Override
	public void setLastrefreshtime(long _v_) { // 上次刷新时间（每日刷新）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lastrefreshtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, lastrefreshtime) {
					public void rollback() { lastrefreshtime = _xdb_saved; }
				};}});
		lastrefreshtime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		LotteryItemAll _o_ = null;
		if ( _o1_ instanceof LotteryItemAll ) _o_ = (LotteryItemAll)_o1_;
		else if ( _o1_ instanceof LotteryItemAll.Const ) _o_ = ((LotteryItemAll.Const)_o1_).nThis();
		else return false;
		if (mapkey != _o_.mapkey) return false;
		if (mapvalue != _o_.mapvalue) return false;
		if (!superlist.equals(_o_.superlist)) return false;
		if (monthfirsttime != _o_.monthfirsttime) return false;
		if (freelotterytime != _o_.freelotterytime) return false;
		if (lastrefreshtime != _o_.lastrefreshtime) return false;
		if (!lotteryitemmap.equals(_o_.lotteryitemmap)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += mapkey;
		_h_ += mapvalue;
		_h_ += superlist.hashCode();
		_h_ += monthfirsttime;
		_h_ += freelotterytime;
		_h_ += lastrefreshtime;
		_h_ += lotteryitemmap.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(mapkey);
		_sb_.append(",");
		_sb_.append(mapvalue);
		_sb_.append(",");
		_sb_.append(superlist);
		_sb_.append(",");
		_sb_.append(monthfirsttime);
		_sb_.append(",");
		_sb_.append(freelotterytime);
		_sb_.append(",");
		_sb_.append(lastrefreshtime);
		_sb_.append(",");
		_sb_.append(lotteryitemmap);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("mapkey"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("mapvalue"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("superlist"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("monthfirsttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("freelotterytime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lastrefreshtime"));
		lb.add(new xdb.logs.ListenableMap().setVarName("lotteryitemmap"));
		return lb;
	}

	private class Const implements xbean.LotteryItemAll {
		LotteryItemAll nThis() {
			return LotteryItemAll.this;
		}

		@Override
		public xbean.LotteryItemAll copy() {
			return LotteryItemAll.this.copy();
		}

		@Override
		public xbean.LotteryItemAll toData() {
			return LotteryItemAll.this.toData();
		}

		public xbean.LotteryItemAll toBean() {
			return LotteryItemAll.this.toBean();
		}

		@Override
		public xbean.LotteryItemAll toDataIf() {
			return LotteryItemAll.this.toDataIf();
		}

		public xbean.LotteryItemAll toBeanIf() {
			return LotteryItemAll.this.toBeanIf();
		}

		@Override
		public int getMapkey() { // 第几层
			_xdb_verify_unsafe_();
			return mapkey;
		}

		@Override
		public int getMapvalue() { // 第几个
			_xdb_verify_unsafe_();
			return mapvalue;
		}

		@Override
		public java.util.List<Integer> getSuperlist() { // 遗迹宝藏特殊list
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(superlist);
		}

		public java.util.List<Integer> getSuperlistAsData() { // 遗迹宝藏特殊list
			_xdb_verify_unsafe_();
			java.util.List<Integer> superlist;
			LotteryItemAll _o_ = LotteryItemAll.this;
		superlist = new java.util.LinkedList<Integer>();
		superlist.addAll(_o_.superlist);
			return superlist;
		}

		@Override
		public long getMonthfirsttime() { // 月卡首刷时间
			_xdb_verify_unsafe_();
			return monthfirsttime;
		}

		@Override
		public long getFreelotterytime() { // 免费单抽到期时间
			_xdb_verify_unsafe_();
			return freelotterytime;
		}

		@Override
		public long getLastrefreshtime() { // 上次刷新时间（每日刷新）
			_xdb_verify_unsafe_();
			return lastrefreshtime;
		}

		@Override
		public java.util.Map<Integer, xbean.LotteryItemlayer> getLotteryitemmap() { // 遗迹宝藏总信息
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(lotteryitemmap);
		}

		@Override
		public java.util.Map<Integer, xbean.LotteryItemlayer> getLotteryitemmapAsData() { // 遗迹宝藏总信息
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.LotteryItemlayer> lotteryitemmap;
			LotteryItemAll _o_ = LotteryItemAll.this;
			lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>();
			for (java.util.Map.Entry<Integer, xbean.LotteryItemlayer> _e_ : _o_.lotteryitemmap.entrySet())
				lotteryitemmap.put(_e_.getKey(), new LotteryItemlayer.Data(_e_.getValue()));
			return lotteryitemmap;
		}

		@Override
		public void setMapkey(int _v_) { // 第几层
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMapvalue(int _v_) { // 第几个
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMonthfirsttime(long _v_) { // 月卡首刷时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setFreelotterytime(long _v_) { // 免费单抽到期时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLastrefreshtime(long _v_) { // 上次刷新时间（每日刷新）
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
			return LotteryItemAll.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return LotteryItemAll.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return LotteryItemAll.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return LotteryItemAll.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return LotteryItemAll.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return LotteryItemAll.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return LotteryItemAll.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return LotteryItemAll.this.hashCode();
		}

		@Override
		public String toString() {
			return LotteryItemAll.this.toString();
		}

	}

	public static final class Data implements xbean.LotteryItemAll {
		private int mapkey; // 第几层
		private int mapvalue; // 第几个
		private java.util.LinkedList<Integer> superlist; // 遗迹宝藏特殊list
		private long monthfirsttime; // 月卡首刷时间
		private long freelotterytime; // 免费单抽到期时间
		private long lastrefreshtime; // 上次刷新时间（每日刷新）
		private java.util.HashMap<Integer, xbean.LotteryItemlayer> lotteryitemmap; // 遗迹宝藏总信息

		public Data() {
			superlist = new java.util.LinkedList<Integer>();
			lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>();
		}

		Data(xbean.LotteryItemAll _o1_) {
			if (_o1_ instanceof LotteryItemAll) assign((LotteryItemAll)_o1_);
			else if (_o1_ instanceof LotteryItemAll.Data) assign((LotteryItemAll.Data)_o1_);
			else if (_o1_ instanceof LotteryItemAll.Const) assign(((LotteryItemAll.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(LotteryItemAll _o_) {
			mapkey = _o_.mapkey;
			mapvalue = _o_.mapvalue;
			superlist = new java.util.LinkedList<Integer>();
			superlist.addAll(_o_.superlist);
			monthfirsttime = _o_.monthfirsttime;
			freelotterytime = _o_.freelotterytime;
			lastrefreshtime = _o_.lastrefreshtime;
			lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>();
			for (java.util.Map.Entry<Integer, xbean.LotteryItemlayer> _e_ : _o_.lotteryitemmap.entrySet())
				lotteryitemmap.put(_e_.getKey(), new LotteryItemlayer.Data(_e_.getValue()));
		}

		private void assign(LotteryItemAll.Data _o_) {
			mapkey = _o_.mapkey;
			mapvalue = _o_.mapvalue;
			superlist = new java.util.LinkedList<Integer>();
			superlist.addAll(_o_.superlist);
			monthfirsttime = _o_.monthfirsttime;
			freelotterytime = _o_.freelotterytime;
			lastrefreshtime = _o_.lastrefreshtime;
			lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>();
			for (java.util.Map.Entry<Integer, xbean.LotteryItemlayer> _e_ : _o_.lotteryitemmap.entrySet())
				lotteryitemmap.put(_e_.getKey(), new LotteryItemlayer.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(mapkey);
			_os_.marshal(mapvalue);
			_os_.compact_uint32(superlist.size());
			for (Integer _v_ : superlist) {
				_os_.marshal(_v_);
			}
			_os_.marshal(monthfirsttime);
			_os_.marshal(freelotterytime);
			_os_.marshal(lastrefreshtime);
			_os_.compact_uint32(lotteryitemmap.size());
			for (java.util.Map.Entry<Integer, xbean.LotteryItemlayer> _e_ : lotteryitemmap.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			mapkey = _os_.unmarshal_int();
			mapvalue = _os_.unmarshal_int();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				superlist.add(_v_);
			}
			monthfirsttime = _os_.unmarshal_long();
			freelotterytime = _os_.unmarshal_long();
			lastrefreshtime = _os_.unmarshal_long();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					lotteryitemmap = new java.util.HashMap<Integer, xbean.LotteryItemlayer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.LotteryItemlayer _v_ = xbean.Pod.newLotteryItemlayerData();
					_v_.unmarshal(_os_);
					lotteryitemmap.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.LotteryItemAll copy() {
			return new Data(this);
		}

		@Override
		public xbean.LotteryItemAll toData() {
			return new Data(this);
		}

		public xbean.LotteryItemAll toBean() {
			return new LotteryItemAll(this, null, null);
		}

		@Override
		public xbean.LotteryItemAll toDataIf() {
			return this;
		}

		public xbean.LotteryItemAll toBeanIf() {
			return new LotteryItemAll(this, null, null);
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
		public int getMapkey() { // 第几层
			return mapkey;
		}

		@Override
		public int getMapvalue() { // 第几个
			return mapvalue;
		}

		@Override
		public java.util.List<Integer> getSuperlist() { // 遗迹宝藏特殊list
			return superlist;
		}

		@Override
		public java.util.List<Integer> getSuperlistAsData() { // 遗迹宝藏特殊list
			return superlist;
		}

		@Override
		public long getMonthfirsttime() { // 月卡首刷时间
			return monthfirsttime;
		}

		@Override
		public long getFreelotterytime() { // 免费单抽到期时间
			return freelotterytime;
		}

		@Override
		public long getLastrefreshtime() { // 上次刷新时间（每日刷新）
			return lastrefreshtime;
		}

		@Override
		public java.util.Map<Integer, xbean.LotteryItemlayer> getLotteryitemmap() { // 遗迹宝藏总信息
			return lotteryitemmap;
		}

		@Override
		public java.util.Map<Integer, xbean.LotteryItemlayer> getLotteryitemmapAsData() { // 遗迹宝藏总信息
			return lotteryitemmap;
		}

		@Override
		public void setMapkey(int _v_) { // 第几层
			mapkey = _v_;
		}

		@Override
		public void setMapvalue(int _v_) { // 第几个
			mapvalue = _v_;
		}

		@Override
		public void setMonthfirsttime(long _v_) { // 月卡首刷时间
			monthfirsttime = _v_;
		}

		@Override
		public void setFreelotterytime(long _v_) { // 免费单抽到期时间
			freelotterytime = _v_;
		}

		@Override
		public void setLastrefreshtime(long _v_) { // 上次刷新时间（每日刷新）
			lastrefreshtime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof LotteryItemAll.Data)) return false;
			LotteryItemAll.Data _o_ = (LotteryItemAll.Data) _o1_;
			if (mapkey != _o_.mapkey) return false;
			if (mapvalue != _o_.mapvalue) return false;
			if (!superlist.equals(_o_.superlist)) return false;
			if (monthfirsttime != _o_.monthfirsttime) return false;
			if (freelotterytime != _o_.freelotterytime) return false;
			if (lastrefreshtime != _o_.lastrefreshtime) return false;
			if (!lotteryitemmap.equals(_o_.lotteryitemmap)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += mapkey;
			_h_ += mapvalue;
			_h_ += superlist.hashCode();
			_h_ += monthfirsttime;
			_h_ += freelotterytime;
			_h_ += lastrefreshtime;
			_h_ += lotteryitemmap.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(mapkey);
			_sb_.append(",");
			_sb_.append(mapvalue);
			_sb_.append(",");
			_sb_.append(superlist);
			_sb_.append(",");
			_sb_.append(monthfirsttime);
			_sb_.append(",");
			_sb_.append(freelotterytime);
			_sb_.append(",");
			_sb_.append(lastrefreshtime);
			_sb_.append(",");
			_sb_.append(lotteryitemmap);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
