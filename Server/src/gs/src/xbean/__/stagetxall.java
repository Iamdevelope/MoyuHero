
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class stagetxall extends xdb.XBean implements xbean.stagetxall {
	private long txtime; // 探险每日刷新时间
	private java.util.HashMap<Integer, xbean.teamtanxian> teamallmap; // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
	private java.util.HashMap<Integer, xbean.stagetanxian> stagetxallmap; // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表

	stagetxall(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>();
		stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>();
	}

	public stagetxall() {
		this(0, null, null);
	}

	public stagetxall(stagetxall _o_) {
		this(_o_, null, null);
	}

	stagetxall(xbean.stagetxall _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof stagetxall) assign((stagetxall)_o1_);
		else if (_o1_ instanceof stagetxall.Data) assign((stagetxall.Data)_o1_);
		else if (_o1_ instanceof stagetxall.Const) assign(((stagetxall.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(stagetxall _o_) {
		_o_._xdb_verify_unsafe_();
		txtime = _o_.txtime;
		teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>();
		for (java.util.Map.Entry<Integer, xbean.teamtanxian> _e_ : _o_.teamallmap.entrySet())
			teamallmap.put(_e_.getKey(), new teamtanxian(_e_.getValue(), this, "teamallmap"));
		stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>();
		for (java.util.Map.Entry<Integer, xbean.stagetanxian> _e_ : _o_.stagetxallmap.entrySet())
			stagetxallmap.put(_e_.getKey(), new stagetanxian(_e_.getValue(), this, "stagetxallmap"));
	}

	private void assign(stagetxall.Data _o_) {
		txtime = _o_.txtime;
		teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>();
		for (java.util.Map.Entry<Integer, xbean.teamtanxian> _e_ : _o_.teamallmap.entrySet())
			teamallmap.put(_e_.getKey(), new teamtanxian(_e_.getValue(), this, "teamallmap"));
		stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>();
		for (java.util.Map.Entry<Integer, xbean.stagetanxian> _e_ : _o_.stagetxallmap.entrySet())
			stagetxallmap.put(_e_.getKey(), new stagetanxian(_e_.getValue(), this, "stagetxallmap"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(txtime);
		_os_.compact_uint32(teamallmap.size());
		for (java.util.Map.Entry<Integer, xbean.teamtanxian> _e_ : teamallmap.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		_os_.compact_uint32(stagetxallmap.size());
		for (java.util.Map.Entry<Integer, xbean.stagetanxian> _e_ : stagetxallmap.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		txtime = _os_.unmarshal_long();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.teamtanxian _v_ = new teamtanxian(0, this, "teamallmap");
				_v_.unmarshal(_os_);
				teamallmap.put(_k_, _v_);
			}
		}
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.stagetanxian _v_ = new stagetanxian(0, this, "stagetxallmap");
				_v_.unmarshal(_os_);
				stagetxallmap.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.stagetxall copy() {
		_xdb_verify_unsafe_();
		return new stagetxall(this);
	}

	@Override
	public xbean.stagetxall toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.stagetxall toBean() {
		_xdb_verify_unsafe_();
		return new stagetxall(this); // same as copy()
	}

	@Override
	public xbean.stagetxall toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.stagetxall toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getTxtime() { // 探险每日刷新时间
		_xdb_verify_unsafe_();
		return txtime;
	}

	@Override
	public java.util.Map<Integer, xbean.teamtanxian> getTeamallmap() { // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "teamallmap"), teamallmap);
	}

	@Override
	public java.util.Map<Integer, xbean.teamtanxian> getTeamallmapAsData() { // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.teamtanxian> teamallmap;
		stagetxall _o_ = this;
		teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>();
		for (java.util.Map.Entry<Integer, xbean.teamtanxian> _e_ : _o_.teamallmap.entrySet())
			teamallmap.put(_e_.getKey(), new teamtanxian.Data(_e_.getValue()));
		return teamallmap;
	}

	@Override
	public java.util.Map<Integer, xbean.stagetanxian> getStagetxallmap() { // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "stagetxallmap"), stagetxallmap);
	}

	@Override
	public java.util.Map<Integer, xbean.stagetanxian> getStagetxallmapAsData() { // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.stagetanxian> stagetxallmap;
		stagetxall _o_ = this;
		stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>();
		for (java.util.Map.Entry<Integer, xbean.stagetanxian> _e_ : _o_.stagetxallmap.entrySet())
			stagetxallmap.put(_e_.getKey(), new stagetanxian.Data(_e_.getValue()));
		return stagetxallmap;
	}

	@Override
	public void setTxtime(long _v_) { // 探险每日刷新时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "txtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, txtime) {
					public void rollback() { txtime = _xdb_saved; }
				};}});
		txtime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		stagetxall _o_ = null;
		if ( _o1_ instanceof stagetxall ) _o_ = (stagetxall)_o1_;
		else if ( _o1_ instanceof stagetxall.Const ) _o_ = ((stagetxall.Const)_o1_).nThis();
		else return false;
		if (txtime != _o_.txtime) return false;
		if (!teamallmap.equals(_o_.teamallmap)) return false;
		if (!stagetxallmap.equals(_o_.stagetxallmap)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += txtime;
		_h_ += teamallmap.hashCode();
		_h_ += stagetxallmap.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(txtime);
		_sb_.append(",");
		_sb_.append(teamallmap);
		_sb_.append(",");
		_sb_.append(stagetxallmap);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("txtime"));
		lb.add(new xdb.logs.ListenableMap().setVarName("teamallmap"));
		lb.add(new xdb.logs.ListenableMap().setVarName("stagetxallmap"));
		return lb;
	}

	private class Const implements xbean.stagetxall {
		stagetxall nThis() {
			return stagetxall.this;
		}

		@Override
		public xbean.stagetxall copy() {
			return stagetxall.this.copy();
		}

		@Override
		public xbean.stagetxall toData() {
			return stagetxall.this.toData();
		}

		public xbean.stagetxall toBean() {
			return stagetxall.this.toBean();
		}

		@Override
		public xbean.stagetxall toDataIf() {
			return stagetxall.this.toDataIf();
		}

		public xbean.stagetxall toBeanIf() {
			return stagetxall.this.toBeanIf();
		}

		@Override
		public long getTxtime() { // 探险每日刷新时间
			_xdb_verify_unsafe_();
			return txtime;
		}

		@Override
		public java.util.Map<Integer, xbean.teamtanxian> getTeamallmap() { // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(teamallmap);
		}

		@Override
		public java.util.Map<Integer, xbean.teamtanxian> getTeamallmapAsData() { // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.teamtanxian> teamallmap;
			stagetxall _o_ = stagetxall.this;
			teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>();
			for (java.util.Map.Entry<Integer, xbean.teamtanxian> _e_ : _o_.teamallmap.entrySet())
				teamallmap.put(_e_.getKey(), new teamtanxian.Data(_e_.getValue()));
			return teamallmap;
		}

		@Override
		public java.util.Map<Integer, xbean.stagetanxian> getStagetxallmap() { // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(stagetxallmap);
		}

		@Override
		public java.util.Map<Integer, xbean.stagetanxian> getStagetxallmapAsData() { // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.stagetanxian> stagetxallmap;
			stagetxall _o_ = stagetxall.this;
			stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>();
			for (java.util.Map.Entry<Integer, xbean.stagetanxian> _e_ : _o_.stagetxallmap.entrySet())
				stagetxallmap.put(_e_.getKey(), new stagetanxian.Data(_e_.getValue()));
			return stagetxallmap;
		}

		@Override
		public void setTxtime(long _v_) { // 探险每日刷新时间
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
			return stagetxall.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return stagetxall.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return stagetxall.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return stagetxall.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return stagetxall.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return stagetxall.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return stagetxall.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return stagetxall.this.hashCode();
		}

		@Override
		public String toString() {
			return stagetxall.this.toString();
		}

	}

	public static final class Data implements xbean.stagetxall {
		private long txtime; // 探险每日刷新时间
		private java.util.HashMap<Integer, xbean.teamtanxian> teamallmap; // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
		private java.util.HashMap<Integer, xbean.stagetanxian> stagetxallmap; // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表

		public Data() {
			teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>();
			stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>();
		}

		Data(xbean.stagetxall _o1_) {
			if (_o1_ instanceof stagetxall) assign((stagetxall)_o1_);
			else if (_o1_ instanceof stagetxall.Data) assign((stagetxall.Data)_o1_);
			else if (_o1_ instanceof stagetxall.Const) assign(((stagetxall.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(stagetxall _o_) {
			txtime = _o_.txtime;
			teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>();
			for (java.util.Map.Entry<Integer, xbean.teamtanxian> _e_ : _o_.teamallmap.entrySet())
				teamallmap.put(_e_.getKey(), new teamtanxian.Data(_e_.getValue()));
			stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>();
			for (java.util.Map.Entry<Integer, xbean.stagetanxian> _e_ : _o_.stagetxallmap.entrySet())
				stagetxallmap.put(_e_.getKey(), new stagetanxian.Data(_e_.getValue()));
		}

		private void assign(stagetxall.Data _o_) {
			txtime = _o_.txtime;
			teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>();
			for (java.util.Map.Entry<Integer, xbean.teamtanxian> _e_ : _o_.teamallmap.entrySet())
				teamallmap.put(_e_.getKey(), new teamtanxian.Data(_e_.getValue()));
			stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>();
			for (java.util.Map.Entry<Integer, xbean.stagetanxian> _e_ : _o_.stagetxallmap.entrySet())
				stagetxallmap.put(_e_.getKey(), new stagetanxian.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(txtime);
			_os_.compact_uint32(teamallmap.size());
			for (java.util.Map.Entry<Integer, xbean.teamtanxian> _e_ : teamallmap.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			_os_.compact_uint32(stagetxallmap.size());
			for (java.util.Map.Entry<Integer, xbean.stagetanxian> _e_ : stagetxallmap.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			txtime = _os_.unmarshal_long();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					teamallmap = new java.util.HashMap<Integer, xbean.teamtanxian>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.teamtanxian _v_ = xbean.Pod.newteamtanxianData();
					_v_.unmarshal(_os_);
					teamallmap.put(_k_, _v_);
				}
			}
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					stagetxallmap = new java.util.HashMap<Integer, xbean.stagetanxian>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.stagetanxian _v_ = xbean.Pod.newstagetanxianData();
					_v_.unmarshal(_os_);
					stagetxallmap.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.stagetxall copy() {
			return new Data(this);
		}

		@Override
		public xbean.stagetxall toData() {
			return new Data(this);
		}

		public xbean.stagetxall toBean() {
			return new stagetxall(this, null, null);
		}

		@Override
		public xbean.stagetxall toDataIf() {
			return this;
		}

		public xbean.stagetxall toBeanIf() {
			return new stagetxall(this, null, null);
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
		public long getTxtime() { // 探险每日刷新时间
			return txtime;
		}

		@Override
		public java.util.Map<Integer, xbean.teamtanxian> getTeamallmap() { // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
			return teamallmap;
		}

		@Override
		public java.util.Map<Integer, xbean.teamtanxian> getTeamallmapAsData() { // 探险任务小队表，key小队id（从1开始），value小队英雄key列表
			return teamallmap;
		}

		@Override
		public java.util.Map<Integer, xbean.stagetanxian> getStagetxallmap() { // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表
			return stagetxallmap;
		}

		@Override
		public java.util.Map<Integer, xbean.stagetanxian> getStagetxallmapAsData() { // 探险任务总表，key是章节ID(从1开始)，value是章节探险列表
			return stagetxallmap;
		}

		@Override
		public void setTxtime(long _v_) { // 探险每日刷新时间
			txtime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof stagetxall.Data)) return false;
			stagetxall.Data _o_ = (stagetxall.Data) _o1_;
			if (txtime != _o_.txtime) return false;
			if (!teamallmap.equals(_o_.teamallmap)) return false;
			if (!stagetxallmap.equals(_o_.stagetxallmap)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += txtime;
			_h_ += teamallmap.hashCode();
			_h_ += stagetxallmap.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(txtime);
			_sb_.append(",");
			_sb_.append(teamallmap);
			_sb_.append(",");
			_sb_.append(stagetxallmap);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
