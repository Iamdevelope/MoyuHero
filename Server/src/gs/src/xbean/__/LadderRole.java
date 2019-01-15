
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class LadderRole extends xdb.XBean implements xbean.LadderRole {
	private int ladderrank; // 天梯排名
	private int laddersoul; // 天梯元魂
	private long lastsoulchangetime; // 上次天梯元魂变动时间
	private java.util.LinkedList<Long> enermies; // 最近的4个仇敌
	private int fighttimes; // 今天战斗次数
	private long lastfighttime; // 上次战斗时间

	LadderRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		enermies = new java.util.LinkedList<Long>();
	}

	public LadderRole() {
		this(0, null, null);
	}

	public LadderRole(LadderRole _o_) {
		this(_o_, null, null);
	}

	LadderRole(xbean.LadderRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof LadderRole) assign((LadderRole)_o1_);
		else if (_o1_ instanceof LadderRole.Data) assign((LadderRole.Data)_o1_);
		else if (_o1_ instanceof LadderRole.Const) assign(((LadderRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(LadderRole _o_) {
		_o_._xdb_verify_unsafe_();
		ladderrank = _o_.ladderrank;
		laddersoul = _o_.laddersoul;
		lastsoulchangetime = _o_.lastsoulchangetime;
		enermies = new java.util.LinkedList<Long>();
		enermies.addAll(_o_.enermies);
		fighttimes = _o_.fighttimes;
		lastfighttime = _o_.lastfighttime;
	}

	private void assign(LadderRole.Data _o_) {
		ladderrank = _o_.ladderrank;
		laddersoul = _o_.laddersoul;
		lastsoulchangetime = _o_.lastsoulchangetime;
		enermies = new java.util.LinkedList<Long>();
		enermies.addAll(_o_.enermies);
		fighttimes = _o_.fighttimes;
		lastfighttime = _o_.lastfighttime;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(ladderrank);
		_os_.marshal(laddersoul);
		_os_.marshal(lastsoulchangetime);
		_os_.compact_uint32(enermies.size());
		for (Long _v_ : enermies) {
			_os_.marshal(_v_);
		}
		_os_.marshal(fighttimes);
		_os_.marshal(lastfighttime);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		ladderrank = _os_.unmarshal_int();
		laddersoul = _os_.unmarshal_int();
		lastsoulchangetime = _os_.unmarshal_long();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			long _v_ = 0;
			_v_ = _os_.unmarshal_long();
			enermies.add(_v_);
		}
		fighttimes = _os_.unmarshal_int();
		lastfighttime = _os_.unmarshal_long();
		return _os_;
	}

	@Override
	public xbean.LadderRole copy() {
		_xdb_verify_unsafe_();
		return new LadderRole(this);
	}

	@Override
	public xbean.LadderRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LadderRole toBean() {
		_xdb_verify_unsafe_();
		return new LadderRole(this); // same as copy()
	}

	@Override
	public xbean.LadderRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LadderRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getLadderrank() { // 天梯排名
		_xdb_verify_unsafe_();
		return ladderrank;
	}

	@Override
	public int getLaddersoul() { // 天梯元魂
		_xdb_verify_unsafe_();
		return laddersoul;
	}

	@Override
	public long getLastsoulchangetime() { // 上次天梯元魂变动时间
		_xdb_verify_unsafe_();
		return lastsoulchangetime;
	}

	@Override
	public java.util.List<Long> getEnermies() { // 最近的4个仇敌
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "enermies"), enermies);
	}

	public java.util.List<Long> getEnermiesAsData() { // 最近的4个仇敌
		_xdb_verify_unsafe_();
		java.util.List<Long> enermies;
		LadderRole _o_ = this;
		enermies = new java.util.LinkedList<Long>();
		enermies.addAll(_o_.enermies);
		return enermies;
	}

	@Override
	public int getFighttimes() { // 今天战斗次数
		_xdb_verify_unsafe_();
		return fighttimes;
	}

	@Override
	public long getLastfighttime() { // 上次战斗时间
		_xdb_verify_unsafe_();
		return lastfighttime;
	}

	@Override
	public void setLadderrank(int _v_) { // 天梯排名
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "ladderrank") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, ladderrank) {
					public void rollback() { ladderrank = _xdb_saved; }
				};}});
		ladderrank = _v_;
	}

	@Override
	public void setLaddersoul(int _v_) { // 天梯元魂
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "laddersoul") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, laddersoul) {
					public void rollback() { laddersoul = _xdb_saved; }
				};}});
		laddersoul = _v_;
	}

	@Override
	public void setLastsoulchangetime(long _v_) { // 上次天梯元魂变动时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lastsoulchangetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, lastsoulchangetime) {
					public void rollback() { lastsoulchangetime = _xdb_saved; }
				};}});
		lastsoulchangetime = _v_;
	}

	@Override
	public void setFighttimes(int _v_) { // 今天战斗次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "fighttimes") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, fighttimes) {
					public void rollback() { fighttimes = _xdb_saved; }
				};}});
		fighttimes = _v_;
	}

	@Override
	public void setLastfighttime(long _v_) { // 上次战斗时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lastfighttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, lastfighttime) {
					public void rollback() { lastfighttime = _xdb_saved; }
				};}});
		lastfighttime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		LadderRole _o_ = null;
		if ( _o1_ instanceof LadderRole ) _o_ = (LadderRole)_o1_;
		else if ( _o1_ instanceof LadderRole.Const ) _o_ = ((LadderRole.Const)_o1_).nThis();
		else return false;
		if (ladderrank != _o_.ladderrank) return false;
		if (laddersoul != _o_.laddersoul) return false;
		if (lastsoulchangetime != _o_.lastsoulchangetime) return false;
		if (!enermies.equals(_o_.enermies)) return false;
		if (fighttimes != _o_.fighttimes) return false;
		if (lastfighttime != _o_.lastfighttime) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += ladderrank;
		_h_ += laddersoul;
		_h_ += lastsoulchangetime;
		_h_ += enermies.hashCode();
		_h_ += fighttimes;
		_h_ += lastfighttime;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(ladderrank);
		_sb_.append(",");
		_sb_.append(laddersoul);
		_sb_.append(",");
		_sb_.append(lastsoulchangetime);
		_sb_.append(",");
		_sb_.append(enermies);
		_sb_.append(",");
		_sb_.append(fighttimes);
		_sb_.append(",");
		_sb_.append(lastfighttime);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("ladderrank"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("laddersoul"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lastsoulchangetime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("enermies"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("fighttimes"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lastfighttime"));
		return lb;
	}

	private class Const implements xbean.LadderRole {
		LadderRole nThis() {
			return LadderRole.this;
		}

		@Override
		public xbean.LadderRole copy() {
			return LadderRole.this.copy();
		}

		@Override
		public xbean.LadderRole toData() {
			return LadderRole.this.toData();
		}

		public xbean.LadderRole toBean() {
			return LadderRole.this.toBean();
		}

		@Override
		public xbean.LadderRole toDataIf() {
			return LadderRole.this.toDataIf();
		}

		public xbean.LadderRole toBeanIf() {
			return LadderRole.this.toBeanIf();
		}

		@Override
		public int getLadderrank() { // 天梯排名
			_xdb_verify_unsafe_();
			return ladderrank;
		}

		@Override
		public int getLaddersoul() { // 天梯元魂
			_xdb_verify_unsafe_();
			return laddersoul;
		}

		@Override
		public long getLastsoulchangetime() { // 上次天梯元魂变动时间
			_xdb_verify_unsafe_();
			return lastsoulchangetime;
		}

		@Override
		public java.util.List<Long> getEnermies() { // 最近的4个仇敌
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(enermies);
		}

		public java.util.List<Long> getEnermiesAsData() { // 最近的4个仇敌
			_xdb_verify_unsafe_();
			java.util.List<Long> enermies;
			LadderRole _o_ = LadderRole.this;
		enermies = new java.util.LinkedList<Long>();
		enermies.addAll(_o_.enermies);
			return enermies;
		}

		@Override
		public int getFighttimes() { // 今天战斗次数
			_xdb_verify_unsafe_();
			return fighttimes;
		}

		@Override
		public long getLastfighttime() { // 上次战斗时间
			_xdb_verify_unsafe_();
			return lastfighttime;
		}

		@Override
		public void setLadderrank(int _v_) { // 天梯排名
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLaddersoul(int _v_) { // 天梯元魂
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLastsoulchangetime(long _v_) { // 上次天梯元魂变动时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setFighttimes(int _v_) { // 今天战斗次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLastfighttime(long _v_) { // 上次战斗时间
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
			return LadderRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return LadderRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return LadderRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return LadderRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return LadderRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return LadderRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return LadderRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return LadderRole.this.hashCode();
		}

		@Override
		public String toString() {
			return LadderRole.this.toString();
		}

	}

	public static final class Data implements xbean.LadderRole {
		private int ladderrank; // 天梯排名
		private int laddersoul; // 天梯元魂
		private long lastsoulchangetime; // 上次天梯元魂变动时间
		private java.util.LinkedList<Long> enermies; // 最近的4个仇敌
		private int fighttimes; // 今天战斗次数
		private long lastfighttime; // 上次战斗时间

		public Data() {
			enermies = new java.util.LinkedList<Long>();
		}

		Data(xbean.LadderRole _o1_) {
			if (_o1_ instanceof LadderRole) assign((LadderRole)_o1_);
			else if (_o1_ instanceof LadderRole.Data) assign((LadderRole.Data)_o1_);
			else if (_o1_ instanceof LadderRole.Const) assign(((LadderRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(LadderRole _o_) {
			ladderrank = _o_.ladderrank;
			laddersoul = _o_.laddersoul;
			lastsoulchangetime = _o_.lastsoulchangetime;
			enermies = new java.util.LinkedList<Long>();
			enermies.addAll(_o_.enermies);
			fighttimes = _o_.fighttimes;
			lastfighttime = _o_.lastfighttime;
		}

		private void assign(LadderRole.Data _o_) {
			ladderrank = _o_.ladderrank;
			laddersoul = _o_.laddersoul;
			lastsoulchangetime = _o_.lastsoulchangetime;
			enermies = new java.util.LinkedList<Long>();
			enermies.addAll(_o_.enermies);
			fighttimes = _o_.fighttimes;
			lastfighttime = _o_.lastfighttime;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(ladderrank);
			_os_.marshal(laddersoul);
			_os_.marshal(lastsoulchangetime);
			_os_.compact_uint32(enermies.size());
			for (Long _v_ : enermies) {
				_os_.marshal(_v_);
			}
			_os_.marshal(fighttimes);
			_os_.marshal(lastfighttime);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			ladderrank = _os_.unmarshal_int();
			laddersoul = _os_.unmarshal_int();
			lastsoulchangetime = _os_.unmarshal_long();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				long _v_ = 0;
				_v_ = _os_.unmarshal_long();
				enermies.add(_v_);
			}
			fighttimes = _os_.unmarshal_int();
			lastfighttime = _os_.unmarshal_long();
			return _os_;
		}

		@Override
		public xbean.LadderRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.LadderRole toData() {
			return new Data(this);
		}

		public xbean.LadderRole toBean() {
			return new LadderRole(this, null, null);
		}

		@Override
		public xbean.LadderRole toDataIf() {
			return this;
		}

		public xbean.LadderRole toBeanIf() {
			return new LadderRole(this, null, null);
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
		public int getLadderrank() { // 天梯排名
			return ladderrank;
		}

		@Override
		public int getLaddersoul() { // 天梯元魂
			return laddersoul;
		}

		@Override
		public long getLastsoulchangetime() { // 上次天梯元魂变动时间
			return lastsoulchangetime;
		}

		@Override
		public java.util.List<Long> getEnermies() { // 最近的4个仇敌
			return enermies;
		}

		@Override
		public java.util.List<Long> getEnermiesAsData() { // 最近的4个仇敌
			return enermies;
		}

		@Override
		public int getFighttimes() { // 今天战斗次数
			return fighttimes;
		}

		@Override
		public long getLastfighttime() { // 上次战斗时间
			return lastfighttime;
		}

		@Override
		public void setLadderrank(int _v_) { // 天梯排名
			ladderrank = _v_;
		}

		@Override
		public void setLaddersoul(int _v_) { // 天梯元魂
			laddersoul = _v_;
		}

		@Override
		public void setLastsoulchangetime(long _v_) { // 上次天梯元魂变动时间
			lastsoulchangetime = _v_;
		}

		@Override
		public void setFighttimes(int _v_) { // 今天战斗次数
			fighttimes = _v_;
		}

		@Override
		public void setLastfighttime(long _v_) { // 上次战斗时间
			lastfighttime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof LadderRole.Data)) return false;
			LadderRole.Data _o_ = (LadderRole.Data) _o1_;
			if (ladderrank != _o_.ladderrank) return false;
			if (laddersoul != _o_.laddersoul) return false;
			if (lastsoulchangetime != _o_.lastsoulchangetime) return false;
			if (!enermies.equals(_o_.enermies)) return false;
			if (fighttimes != _o_.fighttimes) return false;
			if (lastfighttime != _o_.lastfighttime) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += ladderrank;
			_h_ += laddersoul;
			_h_ += lastsoulchangetime;
			_h_ += enermies.hashCode();
			_h_ += fighttimes;
			_h_ += lastfighttime;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(ladderrank);
			_sb_.append(",");
			_sb_.append(laddersoul);
			_sb_.append(",");
			_sb_.append(lastsoulchangetime);
			_sb_.append(",");
			_sb_.append(enermies);
			_sb_.append(",");
			_sb_.append(fighttimes);
			_sb_.append(",");
			_sb_.append(lastfighttime);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
