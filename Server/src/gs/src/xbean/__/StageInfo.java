
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class StageInfo extends xdb.XBean implements xbean.StageInfo {
	private int id; // 
	private int rewardgot; // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
	private java.util.HashMap<Integer, xbean.StageBattleInfo> stagebattles; // 

	StageInfo(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>();
	}

	public StageInfo() {
		this(0, null, null);
	}

	public StageInfo(StageInfo _o_) {
		this(_o_, null, null);
	}

	StageInfo(xbean.StageInfo _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof StageInfo) assign((StageInfo)_o1_);
		else if (_o1_ instanceof StageInfo.Data) assign((StageInfo.Data)_o1_);
		else if (_o1_ instanceof StageInfo.Const) assign(((StageInfo.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(StageInfo _o_) {
		_o_._xdb_verify_unsafe_();
		id = _o_.id;
		rewardgot = _o_.rewardgot;
		stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>();
		for (java.util.Map.Entry<Integer, xbean.StageBattleInfo> _e_ : _o_.stagebattles.entrySet())
			stagebattles.put(_e_.getKey(), new StageBattleInfo(_e_.getValue(), this, "stagebattles"));
	}

	private void assign(StageInfo.Data _o_) {
		id = _o_.id;
		rewardgot = _o_.rewardgot;
		stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>();
		for (java.util.Map.Entry<Integer, xbean.StageBattleInfo> _e_ : _o_.stagebattles.entrySet())
			stagebattles.put(_e_.getKey(), new StageBattleInfo(_e_.getValue(), this, "stagebattles"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(id);
		_os_.marshal(rewardgot);
		_os_.compact_uint32(stagebattles.size());
		for (java.util.Map.Entry<Integer, xbean.StageBattleInfo> _e_ : stagebattles.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		id = _os_.unmarshal_int();
		rewardgot = _os_.unmarshal_int();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.StageBattleInfo _v_ = new StageBattleInfo(0, this, "stagebattles");
				_v_.unmarshal(_os_);
				stagebattles.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.StageInfo copy() {
		_xdb_verify_unsafe_();
		return new StageInfo(this);
	}

	@Override
	public xbean.StageInfo toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.StageInfo toBean() {
		_xdb_verify_unsafe_();
		return new StageInfo(this); // same as copy()
	}

	@Override
	public xbean.StageInfo toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.StageInfo toBeanIf() {
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
	public int getRewardgot() { // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
		_xdb_verify_unsafe_();
		return rewardgot;
	}

	@Override
	public java.util.Map<Integer, xbean.StageBattleInfo> getStagebattles() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "stagebattles"), stagebattles);
	}

	@Override
	public java.util.Map<Integer, xbean.StageBattleInfo> getStagebattlesAsData() { // 
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.StageBattleInfo> stagebattles;
		StageInfo _o_ = this;
		stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>();
		for (java.util.Map.Entry<Integer, xbean.StageBattleInfo> _e_ : _o_.stagebattles.entrySet())
			stagebattles.put(_e_.getKey(), new StageBattleInfo.Data(_e_.getValue()));
		return stagebattles;
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
	public void setRewardgot(int _v_) { // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "rewardgot") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, rewardgot) {
					public void rollback() { rewardgot = _xdb_saved; }
				};}});
		rewardgot = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		StageInfo _o_ = null;
		if ( _o1_ instanceof StageInfo ) _o_ = (StageInfo)_o1_;
		else if ( _o1_ instanceof StageInfo.Const ) _o_ = ((StageInfo.Const)_o1_).nThis();
		else return false;
		if (id != _o_.id) return false;
		if (rewardgot != _o_.rewardgot) return false;
		if (!stagebattles.equals(_o_.stagebattles)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += id;
		_h_ += rewardgot;
		_h_ += stagebattles.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id);
		_sb_.append(",");
		_sb_.append(rewardgot);
		_sb_.append(",");
		_sb_.append(stagebattles);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("id"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("rewardgot"));
		lb.add(new xdb.logs.ListenableMap().setVarName("stagebattles"));
		return lb;
	}

	private class Const implements xbean.StageInfo {
		StageInfo nThis() {
			return StageInfo.this;
		}

		@Override
		public xbean.StageInfo copy() {
			return StageInfo.this.copy();
		}

		@Override
		public xbean.StageInfo toData() {
			return StageInfo.this.toData();
		}

		public xbean.StageInfo toBean() {
			return StageInfo.this.toBean();
		}

		@Override
		public xbean.StageInfo toDataIf() {
			return StageInfo.this.toDataIf();
		}

		public xbean.StageInfo toBeanIf() {
			return StageInfo.this.toBeanIf();
		}

		@Override
		public int getId() { // 
			_xdb_verify_unsafe_();
			return id;
		}

		@Override
		public int getRewardgot() { // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
			_xdb_verify_unsafe_();
			return rewardgot;
		}

		@Override
		public java.util.Map<Integer, xbean.StageBattleInfo> getStagebattles() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(stagebattles);
		}

		@Override
		public java.util.Map<Integer, xbean.StageBattleInfo> getStagebattlesAsData() { // 
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.StageBattleInfo> stagebattles;
			StageInfo _o_ = StageInfo.this;
			stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>();
			for (java.util.Map.Entry<Integer, xbean.StageBattleInfo> _e_ : _o_.stagebattles.entrySet())
				stagebattles.put(_e_.getKey(), new StageBattleInfo.Data(_e_.getValue()));
			return stagebattles;
		}

		@Override
		public void setId(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRewardgot(int _v_) { // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
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
			return StageInfo.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return StageInfo.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return StageInfo.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return StageInfo.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return StageInfo.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return StageInfo.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return StageInfo.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return StageInfo.this.hashCode();
		}

		@Override
		public String toString() {
			return StageInfo.this.toString();
		}

	}

	public static final class Data implements xbean.StageInfo {
		private int id; // 
		private int rewardgot; // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
		private java.util.HashMap<Integer, xbean.StageBattleInfo> stagebattles; // 

		public Data() {
			stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>();
		}

		Data(xbean.StageInfo _o1_) {
			if (_o1_ instanceof StageInfo) assign((StageInfo)_o1_);
			else if (_o1_ instanceof StageInfo.Data) assign((StageInfo.Data)_o1_);
			else if (_o1_ instanceof StageInfo.Const) assign(((StageInfo.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(StageInfo _o_) {
			id = _o_.id;
			rewardgot = _o_.rewardgot;
			stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>();
			for (java.util.Map.Entry<Integer, xbean.StageBattleInfo> _e_ : _o_.stagebattles.entrySet())
				stagebattles.put(_e_.getKey(), new StageBattleInfo.Data(_e_.getValue()));
		}

		private void assign(StageInfo.Data _o_) {
			id = _o_.id;
			rewardgot = _o_.rewardgot;
			stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>();
			for (java.util.Map.Entry<Integer, xbean.StageBattleInfo> _e_ : _o_.stagebattles.entrySet())
				stagebattles.put(_e_.getKey(), new StageBattleInfo.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(id);
			_os_.marshal(rewardgot);
			_os_.compact_uint32(stagebattles.size());
			for (java.util.Map.Entry<Integer, xbean.StageBattleInfo> _e_ : stagebattles.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			id = _os_.unmarshal_int();
			rewardgot = _os_.unmarshal_int();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					stagebattles = new java.util.HashMap<Integer, xbean.StageBattleInfo>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.StageBattleInfo _v_ = xbean.Pod.newStageBattleInfoData();
					_v_.unmarshal(_os_);
					stagebattles.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.StageInfo copy() {
			return new Data(this);
		}

		@Override
		public xbean.StageInfo toData() {
			return new Data(this);
		}

		public xbean.StageInfo toBean() {
			return new StageInfo(this, null, null);
		}

		@Override
		public xbean.StageInfo toDataIf() {
			return this;
		}

		public xbean.StageInfo toBeanIf() {
			return new StageInfo(this, null, null);
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
		public int getRewardgot() { // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
			return rewardgot;
		}

		@Override
		public java.util.Map<Integer, xbean.StageBattleInfo> getStagebattles() { // 
			return stagebattles;
		}

		@Override
		public java.util.Map<Integer, xbean.StageBattleInfo> getStagebattlesAsData() { // 
			return stagebattles;
		}

		@Override
		public void setId(int _v_) { // 
			id = _v_;
		}

		@Override
		public void setRewardgot(int _v_) { // 个位表示第一个宝箱，十位为第二个宝箱，以此类推，1已领取，0未领取
			rewardgot = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof StageInfo.Data)) return false;
			StageInfo.Data _o_ = (StageInfo.Data) _o1_;
			if (id != _o_.id) return false;
			if (rewardgot != _o_.rewardgot) return false;
			if (!stagebattles.equals(_o_.stagebattles)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += id;
			_h_ += rewardgot;
			_h_ += stagebattles.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(id);
			_sb_.append(",");
			_sb_.append(rewardgot);
			_sb_.append(",");
			_sb_.append(stagebattles);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
