
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class StageBattleInfo extends xdb.XBean implements xbean.StageBattleInfo {
	private int id; // 
	private int maxstar; // 
	private int fightnum; // 
	private long lastfighttime; // 
	private int allfightnum; // 
	private int buybattlenum; // 购买关卡次数
	private long buybattlelasttime; // 最后购买关卡次数时间
	private int resetnum; // 已重置次数
	private int sweepnum; // 已扫荡次数

	StageBattleInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		allfightnum = 1;
		buybattlenum = 0;
		buybattlelasttime = 0;
	}

	public StageBattleInfo() {
		this(0, null, null);
	}

	public StageBattleInfo(StageBattleInfo _o_) {
		this(_o_, null, null);
	}

	StageBattleInfo(xbean.StageBattleInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof StageBattleInfo) assign((StageBattleInfo)_o1_);
		else if (_o1_ instanceof StageBattleInfo.Data) assign((StageBattleInfo.Data)_o1_);
		else if (_o1_ instanceof StageBattleInfo.Const) assign(((StageBattleInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(StageBattleInfo _o_) {
		_o_._xdb_verify_unsafe_();
		id = _o_.id;
		maxstar = _o_.maxstar;
		fightnum = _o_.fightnum;
		lastfighttime = _o_.lastfighttime;
		allfightnum = _o_.allfightnum;
		buybattlenum = _o_.buybattlenum;
		buybattlelasttime = _o_.buybattlelasttime;
		resetnum = _o_.resetnum;
		sweepnum = _o_.sweepnum;
	}

	private void assign(StageBattleInfo.Data _o_) {
		id = _o_.id;
		maxstar = _o_.maxstar;
		fightnum = _o_.fightnum;
		lastfighttime = _o_.lastfighttime;
		allfightnum = _o_.allfightnum;
		buybattlenum = _o_.buybattlenum;
		buybattlelasttime = _o_.buybattlelasttime;
		resetnum = _o_.resetnum;
		sweepnum = _o_.sweepnum;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(id);
		_os_.marshal(maxstar);
		_os_.marshal(fightnum);
		_os_.marshal(lastfighttime);
		_os_.marshal(allfightnum);
		_os_.marshal(buybattlenum);
		_os_.marshal(buybattlelasttime);
		_os_.marshal(resetnum);
		_os_.marshal(sweepnum);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		id = _os_.unmarshal_int();
		maxstar = _os_.unmarshal_int();
		fightnum = _os_.unmarshal_int();
		lastfighttime = _os_.unmarshal_long();
		allfightnum = _os_.unmarshal_int();
		buybattlenum = _os_.unmarshal_int();
		buybattlelasttime = _os_.unmarshal_long();
		resetnum = _os_.unmarshal_int();
		sweepnum = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.StageBattleInfo copy() {
		_xdb_verify_unsafe_();
		return new StageBattleInfo(this);
	}

	@Override
	public xbean.StageBattleInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.StageBattleInfo toBean() {
		_xdb_verify_unsafe_();
		return new StageBattleInfo(this); // same as copy()
	}

	@Override
	public xbean.StageBattleInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.StageBattleInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getId() { // 
		_xdb_verify_unsafe_();
		return id;
	}

	@Override
	public int getMaxstar() { // 
		_xdb_verify_unsafe_();
		return maxstar;
	}

	@Override
	public int getFightnum() { // 
		_xdb_verify_unsafe_();
		return fightnum;
	}

	@Override
	public long getLastfighttime() { // 
		_xdb_verify_unsafe_();
		return lastfighttime;
	}

	@Override
	public int getAllfightnum() { // 
		_xdb_verify_unsafe_();
		return allfightnum;
	}

	@Override
	public int getBuybattlenum() { // 购买关卡次数
		_xdb_verify_unsafe_();
		return buybattlenum;
	}

	@Override
	public long getBuybattlelasttime() { // 最后购买关卡次数时间
		_xdb_verify_unsafe_();
		return buybattlelasttime;
	}

	@Override
	public int getResetnum() { // 已重置次数
		_xdb_verify_unsafe_();
		return resetnum;
	}

	@Override
	public int getSweepnum() { // 已扫荡次数
		_xdb_verify_unsafe_();
		return sweepnum;
	}

	@Override
	public void setId(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "id") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, id) {
					public void rollback() { id = _xdb_saved; }
				};}});
		id = _v_;
	}

	@Override
	public void setMaxstar(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "maxstar") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, maxstar) {
					public void rollback() { maxstar = _xdb_saved; }
				};}});
		maxstar = _v_;
	}

	@Override
	public void setFightnum(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "fightnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, fightnum) {
					public void rollback() { fightnum = _xdb_saved; }
				};}});
		fightnum = _v_;
	}

	@Override
	public void setLastfighttime(long _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lastfighttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, lastfighttime) {
					public void rollback() { lastfighttime = _xdb_saved; }
				};}});
		lastfighttime = _v_;
	}

	@Override
	public void setAllfightnum(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "allfightnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, allfightnum) {
					public void rollback() { allfightnum = _xdb_saved; }
				};}});
		allfightnum = _v_;
	}

	@Override
	public void setBuybattlenum(int _v_) { // 购买关卡次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "buybattlenum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, buybattlenum) {
					public void rollback() { buybattlenum = _xdb_saved; }
				};}});
		buybattlenum = _v_;
	}

	@Override
	public void setBuybattlelasttime(long _v_) { // 最后购买关卡次数时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "buybattlelasttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, buybattlelasttime) {
					public void rollback() { buybattlelasttime = _xdb_saved; }
				};}});
		buybattlelasttime = _v_;
	}

	@Override
	public void setResetnum(int _v_) { // 已重置次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "resetnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, resetnum) {
					public void rollback() { resetnum = _xdb_saved; }
				};}});
		resetnum = _v_;
	}

	@Override
	public void setSweepnum(int _v_) { // 已扫荡次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "sweepnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, sweepnum) {
					public void rollback() { sweepnum = _xdb_saved; }
				};}});
		sweepnum = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		StageBattleInfo _o_ = null;
		if ( _o1_ instanceof StageBattleInfo ) _o_ = (StageBattleInfo)_o1_;
		else if ( _o1_ instanceof StageBattleInfo.Const ) _o_ = ((StageBattleInfo.Const)_o1_).nThis();
		else return false;
		if (id != _o_.id) return false;
		if (maxstar != _o_.maxstar) return false;
		if (fightnum != _o_.fightnum) return false;
		if (lastfighttime != _o_.lastfighttime) return false;
		if (allfightnum != _o_.allfightnum) return false;
		if (buybattlenum != _o_.buybattlenum) return false;
		if (buybattlelasttime != _o_.buybattlelasttime) return false;
		if (resetnum != _o_.resetnum) return false;
		if (sweepnum != _o_.sweepnum) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += id;
		_h_ += maxstar;
		_h_ += fightnum;
		_h_ += lastfighttime;
		_h_ += allfightnum;
		_h_ += buybattlenum;
		_h_ += buybattlelasttime;
		_h_ += resetnum;
		_h_ += sweepnum;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id);
		_sb_.append(",");
		_sb_.append(maxstar);
		_sb_.append(",");
		_sb_.append(fightnum);
		_sb_.append(",");
		_sb_.append(lastfighttime);
		_sb_.append(",");
		_sb_.append(allfightnum);
		_sb_.append(",");
		_sb_.append(buybattlenum);
		_sb_.append(",");
		_sb_.append(buybattlelasttime);
		_sb_.append(",");
		_sb_.append(resetnum);
		_sb_.append(",");
		_sb_.append(sweepnum);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("id"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("maxstar"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("fightnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lastfighttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("allfightnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("buybattlenum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("buybattlelasttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("resetnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sweepnum"));
		return lb;
	}

	private class Const implements xbean.StageBattleInfo {
		StageBattleInfo nThis() {
			return StageBattleInfo.this;
		}

		@Override
		public xbean.StageBattleInfo copy() {
			return StageBattleInfo.this.copy();
		}

		@Override
		public xbean.StageBattleInfo toData() {
			return StageBattleInfo.this.toData();
		}

		public xbean.StageBattleInfo toBean() {
			return StageBattleInfo.this.toBean();
		}

		@Override
		public xbean.StageBattleInfo toDataIf() {
			return StageBattleInfo.this.toDataIf();
		}

		public xbean.StageBattleInfo toBeanIf() {
			return StageBattleInfo.this.toBeanIf();
		}

		@Override
		public int getId() { // 
			_xdb_verify_unsafe_();
			return id;
		}

		@Override
		public int getMaxstar() { // 
			_xdb_verify_unsafe_();
			return maxstar;
		}

		@Override
		public int getFightnum() { // 
			_xdb_verify_unsafe_();
			return fightnum;
		}

		@Override
		public long getLastfighttime() { // 
			_xdb_verify_unsafe_();
			return lastfighttime;
		}

		@Override
		public int getAllfightnum() { // 
			_xdb_verify_unsafe_();
			return allfightnum;
		}

		@Override
		public int getBuybattlenum() { // 购买关卡次数
			_xdb_verify_unsafe_();
			return buybattlenum;
		}

		@Override
		public long getBuybattlelasttime() { // 最后购买关卡次数时间
			_xdb_verify_unsafe_();
			return buybattlelasttime;
		}

		@Override
		public int getResetnum() { // 已重置次数
			_xdb_verify_unsafe_();
			return resetnum;
		}

		@Override
		public int getSweepnum() { // 已扫荡次数
			_xdb_verify_unsafe_();
			return sweepnum;
		}

		@Override
		public void setId(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMaxstar(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setFightnum(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLastfighttime(long _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAllfightnum(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBuybattlenum(int _v_) { // 购买关卡次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBuybattlelasttime(long _v_) { // 最后购买关卡次数时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setResetnum(int _v_) { // 已重置次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSweepnum(int _v_) { // 已扫荡次数
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
			return StageBattleInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return StageBattleInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return StageBattleInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return StageBattleInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return StageBattleInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return StageBattleInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return StageBattleInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return StageBattleInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return StageBattleInfo.this.toString();
		}

	}

	public static final class Data implements xbean.StageBattleInfo {
		private int id; // 
		private int maxstar; // 
		private int fightnum; // 
		private long lastfighttime; // 
		private int allfightnum; // 
		private int buybattlenum; // 购买关卡次数
		private long buybattlelasttime; // 最后购买关卡次数时间
		private int resetnum; // 已重置次数
		private int sweepnum; // 已扫荡次数

		public Data() {
			allfightnum = 1;
			buybattlenum = 0;
			buybattlelasttime = 0;
		}

		Data(xbean.StageBattleInfo _o1_) {
			if (_o1_ instanceof StageBattleInfo) assign((StageBattleInfo)_o1_);
			else if (_o1_ instanceof StageBattleInfo.Data) assign((StageBattleInfo.Data)_o1_);
			else if (_o1_ instanceof StageBattleInfo.Const) assign(((StageBattleInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(StageBattleInfo _o_) {
			id = _o_.id;
			maxstar = _o_.maxstar;
			fightnum = _o_.fightnum;
			lastfighttime = _o_.lastfighttime;
			allfightnum = _o_.allfightnum;
			buybattlenum = _o_.buybattlenum;
			buybattlelasttime = _o_.buybattlelasttime;
			resetnum = _o_.resetnum;
			sweepnum = _o_.sweepnum;
		}

		private void assign(StageBattleInfo.Data _o_) {
			id = _o_.id;
			maxstar = _o_.maxstar;
			fightnum = _o_.fightnum;
			lastfighttime = _o_.lastfighttime;
			allfightnum = _o_.allfightnum;
			buybattlenum = _o_.buybattlenum;
			buybattlelasttime = _o_.buybattlelasttime;
			resetnum = _o_.resetnum;
			sweepnum = _o_.sweepnum;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(id);
			_os_.marshal(maxstar);
			_os_.marshal(fightnum);
			_os_.marshal(lastfighttime);
			_os_.marshal(allfightnum);
			_os_.marshal(buybattlenum);
			_os_.marshal(buybattlelasttime);
			_os_.marshal(resetnum);
			_os_.marshal(sweepnum);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			id = _os_.unmarshal_int();
			maxstar = _os_.unmarshal_int();
			fightnum = _os_.unmarshal_int();
			lastfighttime = _os_.unmarshal_long();
			allfightnum = _os_.unmarshal_int();
			buybattlenum = _os_.unmarshal_int();
			buybattlelasttime = _os_.unmarshal_long();
			resetnum = _os_.unmarshal_int();
			sweepnum = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.StageBattleInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.StageBattleInfo toData() {
			return new Data(this);
		}

		public xbean.StageBattleInfo toBean() {
			return new StageBattleInfo(this, null, null);
		}

		@Override
		public xbean.StageBattleInfo toDataIf() {
			return this;
		}

		public xbean.StageBattleInfo toBeanIf() {
			return new StageBattleInfo(this, null, null);
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
		public int getId() { // 
			return id;
		}

		@Override
		public int getMaxstar() { // 
			return maxstar;
		}

		@Override
		public int getFightnum() { // 
			return fightnum;
		}

		@Override
		public long getLastfighttime() { // 
			return lastfighttime;
		}

		@Override
		public int getAllfightnum() { // 
			return allfightnum;
		}

		@Override
		public int getBuybattlenum() { // 购买关卡次数
			return buybattlenum;
		}

		@Override
		public long getBuybattlelasttime() { // 最后购买关卡次数时间
			return buybattlelasttime;
		}

		@Override
		public int getResetnum() { // 已重置次数
			return resetnum;
		}

		@Override
		public int getSweepnum() { // 已扫荡次数
			return sweepnum;
		}

		@Override
		public void setId(int _v_) { // 
			id = _v_;
		}

		@Override
		public void setMaxstar(int _v_) { // 
			maxstar = _v_;
		}

		@Override
		public void setFightnum(int _v_) { // 
			fightnum = _v_;
		}

		@Override
		public void setLastfighttime(long _v_) { // 
			lastfighttime = _v_;
		}

		@Override
		public void setAllfightnum(int _v_) { // 
			allfightnum = _v_;
		}

		@Override
		public void setBuybattlenum(int _v_) { // 购买关卡次数
			buybattlenum = _v_;
		}

		@Override
		public void setBuybattlelasttime(long _v_) { // 最后购买关卡次数时间
			buybattlelasttime = _v_;
		}

		@Override
		public void setResetnum(int _v_) { // 已重置次数
			resetnum = _v_;
		}

		@Override
		public void setSweepnum(int _v_) { // 已扫荡次数
			sweepnum = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof StageBattleInfo.Data)) return false;
			StageBattleInfo.Data _o_ = (StageBattleInfo.Data) _o1_;
			if (id != _o_.id) return false;
			if (maxstar != _o_.maxstar) return false;
			if (fightnum != _o_.fightnum) return false;
			if (lastfighttime != _o_.lastfighttime) return false;
			if (allfightnum != _o_.allfightnum) return false;
			if (buybattlenum != _o_.buybattlenum) return false;
			if (buybattlelasttime != _o_.buybattlelasttime) return false;
			if (resetnum != _o_.resetnum) return false;
			if (sweepnum != _o_.sweepnum) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += id;
			_h_ += maxstar;
			_h_ += fightnum;
			_h_ += lastfighttime;
			_h_ += allfightnum;
			_h_ += buybattlenum;
			_h_ += buybattlelasttime;
			_h_ += resetnum;
			_h_ += sweepnum;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(id);
			_sb_.append(",");
			_sb_.append(maxstar);
			_sb_.append(",");
			_sb_.append(fightnum);
			_sb_.append(",");
			_sb_.append(lastfighttime);
			_sb_.append(",");
			_sb_.append(allfightnum);
			_sb_.append(",");
			_sb_.append(buybattlenum);
			_sb_.append(",");
			_sb_.append(buybattlelasttime);
			_sb_.append(",");
			_sb_.append(resetnum);
			_sb_.append(",");
			_sb_.append(sweepnum);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
