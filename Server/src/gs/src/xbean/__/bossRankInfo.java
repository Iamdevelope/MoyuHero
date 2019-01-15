
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class bossRankInfo extends xdb.XBean implements xbean.bossRankInfo {
	private long roleid; // 玩家guid
	private String rolename; // 玩家名称
	private long num; // 伤害
	private int rankid; // 名次

	bossRankInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		rolename = "";
	}

	public bossRankInfo() {
		this(0, null, null);
	}

	public bossRankInfo(bossRankInfo _o_) {
		this(_o_, null, null);
	}

	bossRankInfo(xbean.bossRankInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof bossRankInfo) assign((bossRankInfo)_o1_);
		else if (_o1_ instanceof bossRankInfo.Data) assign((bossRankInfo.Data)_o1_);
		else if (_o1_ instanceof bossRankInfo.Const) assign(((bossRankInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(bossRankInfo _o_) {
		_o_._xdb_verify_unsafe_();
		roleid = _o_.roleid;
		rolename = _o_.rolename;
		num = _o_.num;
		rankid = _o_.rankid;
	}

	private void assign(bossRankInfo.Data _o_) {
		roleid = _o_.roleid;
		rolename = _o_.rolename;
		num = _o_.num;
		rankid = _o_.rankid;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(roleid);
		_os_.marshal(rolename, xdb.Const.IO_CHARSET);
		_os_.marshal(num);
		_os_.marshal(rankid);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		roleid = _os_.unmarshal_long();
		rolename = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		num = _os_.unmarshal_long();
		rankid = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.bossRankInfo copy() {
		_xdb_verify_unsafe_();
		return new bossRankInfo(this);
	}

	@Override
	public xbean.bossRankInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.bossRankInfo toBean() {
		_xdb_verify_unsafe_();
		return new bossRankInfo(this); // same as copy()
	}

	@Override
	public xbean.bossRankInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.bossRankInfo toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getRoleid() { // 玩家guid
		_xdb_verify_unsafe_();
		return roleid;
	}

	@Override
	public String getRolename() { // 玩家名称
		_xdb_verify_unsafe_();
		return rolename;
	}

	@Override
	public com.goldhuman.Common.Octets getRolenameOctets() { // 玩家名称
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getRolename(), xdb.Const.IO_CHARSET);
	}

	@Override
	public long getNum() { // 伤害
		_xdb_verify_unsafe_();
		return num;
	}

	@Override
	public int getRankid() { // 名次
		_xdb_verify_unsafe_();
		return rankid;
	}

	@Override
	public void setRoleid(long _v_) { // 玩家guid
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "roleid") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, roleid) {
					public void rollback() { roleid = _xdb_saved; }
				};}});
		roleid = _v_;
	}

	@Override
	public void setRolename(String _v_) { // 玩家名称
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "rolename") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, rolename) {
					public void rollback() { rolename = _xdb_saved; }
				};}});
		rolename = _v_;
	}

	@Override
	public void setRolenameOctets(com.goldhuman.Common.Octets _v_) { // 玩家名称
		_xdb_verify_unsafe_();
		this.setRolename(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setNum(long _v_) { // 伤害
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "num") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, num) {
					public void rollback() { num = _xdb_saved; }
				};}});
		num = _v_;
	}

	@Override
	public void setRankid(int _v_) { // 名次
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "rankid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, rankid) {
					public void rollback() { rankid = _xdb_saved; }
				};}});
		rankid = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		bossRankInfo _o_ = null;
		if ( _o1_ instanceof bossRankInfo ) _o_ = (bossRankInfo)_o1_;
		else if ( _o1_ instanceof bossRankInfo.Const ) _o_ = ((bossRankInfo.Const)_o1_).nThis();
		else return false;
		if (roleid != _o_.roleid) return false;
		if (!rolename.equals(_o_.rolename)) return false;
		if (num != _o_.num) return false;
		if (rankid != _o_.rankid) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += roleid;
		_h_ += rolename.hashCode();
		_h_ += num;
		_h_ += rankid;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid);
		_sb_.append(",");
		_sb_.append("'").append(rolename).append("'");
		_sb_.append(",");
		_sb_.append(num);
		_sb_.append(",");
		_sb_.append(rankid);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("roleid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("rolename"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("num"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("rankid"));
		return lb;
	}

	private class Const implements xbean.bossRankInfo {
		bossRankInfo nThis() {
			return bossRankInfo.this;
		}

		@Override
		public xbean.bossRankInfo copy() {
			return bossRankInfo.this.copy();
		}

		@Override
		public xbean.bossRankInfo toData() {
			return bossRankInfo.this.toData();
		}

		public xbean.bossRankInfo toBean() {
			return bossRankInfo.this.toBean();
		}

		@Override
		public xbean.bossRankInfo toDataIf() {
			return bossRankInfo.this.toDataIf();
		}

		public xbean.bossRankInfo toBeanIf() {
			return bossRankInfo.this.toBeanIf();
		}

		@Override
		public long getRoleid() { // 玩家guid
			_xdb_verify_unsafe_();
			return roleid;
		}

		@Override
		public String getRolename() { // 玩家名称
			_xdb_verify_unsafe_();
			return rolename;
		}

		@Override
		public com.goldhuman.Common.Octets getRolenameOctets() { // 玩家名称
			_xdb_verify_unsafe_();
			return bossRankInfo.this.getRolenameOctets();
		}

		@Override
		public long getNum() { // 伤害
			_xdb_verify_unsafe_();
			return num;
		}

		@Override
		public int getRankid() { // 名次
			_xdb_verify_unsafe_();
			return rankid;
		}

		@Override
		public void setRoleid(long _v_) { // 玩家guid
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRolename(String _v_) { // 玩家名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRolenameOctets(com.goldhuman.Common.Octets _v_) { // 玩家名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNum(long _v_) { // 伤害
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRankid(int _v_) { // 名次
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
			return bossRankInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return bossRankInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return bossRankInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return bossRankInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return bossRankInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return bossRankInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return bossRankInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return bossRankInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return bossRankInfo.this.toString();
		}

	}

	public static final class Data implements xbean.bossRankInfo {
		private long roleid; // 玩家guid
		private String rolename; // 玩家名称
		private long num; // 伤害
		private int rankid; // 名次

		public Data() {
			rolename = "";
		}

		Data(xbean.bossRankInfo _o1_) {
			if (_o1_ instanceof bossRankInfo) assign((bossRankInfo)_o1_);
			else if (_o1_ instanceof bossRankInfo.Data) assign((bossRankInfo.Data)_o1_);
			else if (_o1_ instanceof bossRankInfo.Const) assign(((bossRankInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(bossRankInfo _o_) {
			roleid = _o_.roleid;
			rolename = _o_.rolename;
			num = _o_.num;
			rankid = _o_.rankid;
		}

		private void assign(bossRankInfo.Data _o_) {
			roleid = _o_.roleid;
			rolename = _o_.rolename;
			num = _o_.num;
			rankid = _o_.rankid;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(roleid);
			_os_.marshal(rolename, xdb.Const.IO_CHARSET);
			_os_.marshal(num);
			_os_.marshal(rankid);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			roleid = _os_.unmarshal_long();
			rolename = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			num = _os_.unmarshal_long();
			rankid = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.bossRankInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.bossRankInfo toData() {
			return new Data(this);
		}

		public xbean.bossRankInfo toBean() {
			return new bossRankInfo(this, null, null);
		}

		@Override
		public xbean.bossRankInfo toDataIf() {
			return this;
		}

		public xbean.bossRankInfo toBeanIf() {
			return new bossRankInfo(this, null, null);
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
		public long getRoleid() { // 玩家guid
			return roleid;
		}

		@Override
		public String getRolename() { // 玩家名称
			return rolename;
		}

		@Override
		public com.goldhuman.Common.Octets getRolenameOctets() { // 玩家名称
			return com.goldhuman.Common.Octets.wrap(getRolename(), xdb.Const.IO_CHARSET);
		}

		@Override
		public long getNum() { // 伤害
			return num;
		}

		@Override
		public int getRankid() { // 名次
			return rankid;
		}

		@Override
		public void setRoleid(long _v_) { // 玩家guid
			roleid = _v_;
		}

		@Override
		public void setRolename(String _v_) { // 玩家名称
			if (null == _v_)
				throw new NullPointerException();
			rolename = _v_;
		}

		@Override
		public void setRolenameOctets(com.goldhuman.Common.Octets _v_) { // 玩家名称
			this.setRolename(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setNum(long _v_) { // 伤害
			num = _v_;
		}

		@Override
		public void setRankid(int _v_) { // 名次
			rankid = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof bossRankInfo.Data)) return false;
			bossRankInfo.Data _o_ = (bossRankInfo.Data) _o1_;
			if (roleid != _o_.roleid) return false;
			if (!rolename.equals(_o_.rolename)) return false;
			if (num != _o_.num) return false;
			if (rankid != _o_.rankid) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += roleid;
			_h_ += rolename.hashCode();
			_h_ += num;
			_h_ += rankid;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(roleid);
			_sb_.append(",");
			_sb_.append("'").append(rolename).append("'");
			_sb_.append(",");
			_sb_.append(num);
			_sb_.append(",");
			_sb_.append(rankid);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
