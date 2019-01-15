
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class BloodRole extends xdb.XBean implements xbean.BloodRole {
	private int curlevel; // 当前层数
	private int lasthard; // 上一次战斗的难度
	private int curstar; // 剩余没用的星
	private int battle1; // 随机出的战斗
	private int battle2; // 
	private int battle3; // 
	private int itemlevel; // 已经获得的物品等级
	private java.util.HashMap<Integer, Float> effects; // 以前已加成的效果
	private int failed; // 1已失败
	private int relivetimes; // 今天已复活次数
	private long lastfighttime; // 上次战斗时间
	private int totalstar; // 累计星
	private int maxlevel; // 最高层
	private java.util.HashMap<Integer, Integer> repeatstaraward; // 
	private java.util.HashMap<Integer, Integer> fixstaraward; // 

	BloodRole(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		curlevel = 1;
		effects = new java.util.HashMap<Integer, Float>();
		repeatstaraward = new java.util.HashMap<Integer, Integer>();
		fixstaraward = new java.util.HashMap<Integer, Integer>();
	}

	public BloodRole() {
		this(0, null, null);
	}

	public BloodRole(BloodRole _o_) {
		this(_o_, null, null);
	}

	BloodRole(xbean.BloodRole _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof BloodRole) assign((BloodRole)_o1_);
		else if (_o1_ instanceof BloodRole.Data) assign((BloodRole.Data)_o1_);
		else if (_o1_ instanceof BloodRole.Const) assign(((BloodRole.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(BloodRole _o_) {
		_o_._xdb_verify_unsafe_();
		curlevel = _o_.curlevel;
		lasthard = _o_.lasthard;
		curstar = _o_.curstar;
		battle1 = _o_.battle1;
		battle2 = _o_.battle2;
		battle3 = _o_.battle3;
		itemlevel = _o_.itemlevel;
		effects = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
			effects.put(_e_.getKey(), _e_.getValue());
		failed = _o_.failed;
		relivetimes = _o_.relivetimes;
		lastfighttime = _o_.lastfighttime;
		totalstar = _o_.totalstar;
		maxlevel = _o_.maxlevel;
		repeatstaraward = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.repeatstaraward.entrySet())
			repeatstaraward.put(_e_.getKey(), _e_.getValue());
		fixstaraward = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.fixstaraward.entrySet())
			fixstaraward.put(_e_.getKey(), _e_.getValue());
	}

	private void assign(BloodRole.Data _o_) {
		curlevel = _o_.curlevel;
		lasthard = _o_.lasthard;
		curstar = _o_.curstar;
		battle1 = _o_.battle1;
		battle2 = _o_.battle2;
		battle3 = _o_.battle3;
		itemlevel = _o_.itemlevel;
		effects = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
			effects.put(_e_.getKey(), _e_.getValue());
		failed = _o_.failed;
		relivetimes = _o_.relivetimes;
		lastfighttime = _o_.lastfighttime;
		totalstar = _o_.totalstar;
		maxlevel = _o_.maxlevel;
		repeatstaraward = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.repeatstaraward.entrySet())
			repeatstaraward.put(_e_.getKey(), _e_.getValue());
		fixstaraward = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.fixstaraward.entrySet())
			fixstaraward.put(_e_.getKey(), _e_.getValue());
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(curlevel);
		_os_.marshal(lasthard);
		_os_.marshal(curstar);
		_os_.marshal(battle1);
		_os_.marshal(battle2);
		_os_.marshal(battle3);
		_os_.marshal(itemlevel);
		_os_.compact_uint32(effects.size());
		for (java.util.Map.Entry<Integer, Float> _e_ : effects.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(failed);
		_os_.marshal(relivetimes);
		_os_.marshal(lastfighttime);
		_os_.marshal(totalstar);
		_os_.marshal(maxlevel);
		_os_.compact_uint32(repeatstaraward.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : repeatstaraward.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(fixstaraward.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : fixstaraward.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		curlevel = _os_.unmarshal_int();
		lasthard = _os_.unmarshal_int();
		curstar = _os_.unmarshal_int();
		battle1 = _os_.unmarshal_int();
		battle2 = _os_.unmarshal_int();
		battle3 = _os_.unmarshal_int();
		itemlevel = _os_.unmarshal_int();
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
		failed = _os_.unmarshal_int();
		relivetimes = _os_.unmarshal_int();
		lastfighttime = _os_.unmarshal_long();
		totalstar = _os_.unmarshal_int();
		maxlevel = _os_.unmarshal_int();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				repeatstaraward = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				repeatstaraward.put(_k_, _v_);
			}
		}
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				fixstaraward = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				fixstaraward.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.BloodRole copy() {
		_xdb_verify_unsafe_();
		return new BloodRole(this);
	}

	@Override
	public xbean.BloodRole toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BloodRole toBean() {
		_xdb_verify_unsafe_();
		return new BloodRole(this); // same as copy()
	}

	@Override
	public xbean.BloodRole toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BloodRole toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getCurlevel() { // 当前层数
		_xdb_verify_unsafe_();
		return curlevel;
	}

	@Override
	public int getLasthard() { // 上一次战斗的难度
		_xdb_verify_unsafe_();
		return lasthard;
	}

	@Override
	public int getCurstar() { // 剩余没用的星
		_xdb_verify_unsafe_();
		return curstar;
	}

	@Override
	public int getBattle1() { // 随机出的战斗
		_xdb_verify_unsafe_();
		return battle1;
	}

	@Override
	public int getBattle2() { // 
		_xdb_verify_unsafe_();
		return battle2;
	}

	@Override
	public int getBattle3() { // 
		_xdb_verify_unsafe_();
		return battle3;
	}

	@Override
	public int getItemlevel() { // 已经获得的物品等级
		_xdb_verify_unsafe_();
		return itemlevel;
	}

	@Override
	public java.util.Map<Integer, Float> getEffects() { // 以前已加成的效果
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "effects"), effects);
	}

	@Override
	public java.util.Map<Integer, Float> getEffectsAsData() { // 以前已加成的效果
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Float> effects;
		BloodRole _o_ = this;
		effects = new java.util.HashMap<Integer, Float>();
		for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
			effects.put(_e_.getKey(), _e_.getValue());
		return effects;
	}

	@Override
	public int getFailed() { // 1已失败
		_xdb_verify_unsafe_();
		return failed;
	}

	@Override
	public int getRelivetimes() { // 今天已复活次数
		_xdb_verify_unsafe_();
		return relivetimes;
	}

	@Override
	public long getLastfighttime() { // 上次战斗时间
		_xdb_verify_unsafe_();
		return lastfighttime;
	}

	@Override
	public int getTotalstar() { // 累计星
		_xdb_verify_unsafe_();
		return totalstar;
	}

	@Override
	public int getMaxlevel() { // 最高层
		_xdb_verify_unsafe_();
		return maxlevel;
	}

	@Override
	public java.util.Map<Integer, Integer> getRepeatstaraward() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "repeatstaraward"), repeatstaraward);
	}

	@Override
	public java.util.Map<Integer, Integer> getRepeatstarawardAsData() { // 
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> repeatstaraward;
		BloodRole _o_ = this;
		repeatstaraward = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.repeatstaraward.entrySet())
			repeatstaraward.put(_e_.getKey(), _e_.getValue());
		return repeatstaraward;
	}

	@Override
	public java.util.Map<Integer, Integer> getFixstaraward() { // 
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "fixstaraward"), fixstaraward);
	}

	@Override
	public java.util.Map<Integer, Integer> getFixstarawardAsData() { // 
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> fixstaraward;
		BloodRole _o_ = this;
		fixstaraward = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.fixstaraward.entrySet())
			fixstaraward.put(_e_.getKey(), _e_.getValue());
		return fixstaraward;
	}

	@Override
	public void setCurlevel(int _v_) { // 当前层数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "curlevel") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, curlevel) {
					public void rollback() { curlevel = _xdb_saved; }
				};}});
		curlevel = _v_;
	}

	@Override
	public void setLasthard(int _v_) { // 上一次战斗的难度
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "lasthard") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, lasthard) {
					public void rollback() { lasthard = _xdb_saved; }
				};}});
		lasthard = _v_;
	}

	@Override
	public void setCurstar(int _v_) { // 剩余没用的星
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "curstar") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, curstar) {
					public void rollback() { curstar = _xdb_saved; }
				};}});
		curstar = _v_;
	}

	@Override
	public void setBattle1(int _v_) { // 随机出的战斗
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battle1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battle1) {
					public void rollback() { battle1 = _xdb_saved; }
				};}});
		battle1 = _v_;
	}

	@Override
	public void setBattle2(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battle2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battle2) {
					public void rollback() { battle2 = _xdb_saved; }
				};}});
		battle2 = _v_;
	}

	@Override
	public void setBattle3(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battle3") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battle3) {
					public void rollback() { battle3 = _xdb_saved; }
				};}});
		battle3 = _v_;
	}

	@Override
	public void setItemlevel(int _v_) { // 已经获得的物品等级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "itemlevel") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, itemlevel) {
					public void rollback() { itemlevel = _xdb_saved; }
				};}});
		itemlevel = _v_;
	}

	@Override
	public void setFailed(int _v_) { // 1已失败
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "failed") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, failed) {
					public void rollback() { failed = _xdb_saved; }
				};}});
		failed = _v_;
	}

	@Override
	public void setRelivetimes(int _v_) { // 今天已复活次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "relivetimes") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, relivetimes) {
					public void rollback() { relivetimes = _xdb_saved; }
				};}});
		relivetimes = _v_;
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
	public void setTotalstar(int _v_) { // 累计星
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "totalstar") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, totalstar) {
					public void rollback() { totalstar = _xdb_saved; }
				};}});
		totalstar = _v_;
	}

	@Override
	public void setMaxlevel(int _v_) { // 最高层
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "maxlevel") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, maxlevel) {
					public void rollback() { maxlevel = _xdb_saved; }
				};}});
		maxlevel = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		BloodRole _o_ = null;
		if ( _o1_ instanceof BloodRole ) _o_ = (BloodRole)_o1_;
		else if ( _o1_ instanceof BloodRole.Const ) _o_ = ((BloodRole.Const)_o1_).nThis();
		else return false;
		if (curlevel != _o_.curlevel) return false;
		if (lasthard != _o_.lasthard) return false;
		if (curstar != _o_.curstar) return false;
		if (battle1 != _o_.battle1) return false;
		if (battle2 != _o_.battle2) return false;
		if (battle3 != _o_.battle3) return false;
		if (itemlevel != _o_.itemlevel) return false;
		if (!effects.equals(_o_.effects)) return false;
		if (failed != _o_.failed) return false;
		if (relivetimes != _o_.relivetimes) return false;
		if (lastfighttime != _o_.lastfighttime) return false;
		if (totalstar != _o_.totalstar) return false;
		if (maxlevel != _o_.maxlevel) return false;
		if (!repeatstaraward.equals(_o_.repeatstaraward)) return false;
		if (!fixstaraward.equals(_o_.fixstaraward)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += curlevel;
		_h_ += lasthard;
		_h_ += curstar;
		_h_ += battle1;
		_h_ += battle2;
		_h_ += battle3;
		_h_ += itemlevel;
		_h_ += effects.hashCode();
		_h_ += failed;
		_h_ += relivetimes;
		_h_ += lastfighttime;
		_h_ += totalstar;
		_h_ += maxlevel;
		_h_ += repeatstaraward.hashCode();
		_h_ += fixstaraward.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(curlevel);
		_sb_.append(",");
		_sb_.append(lasthard);
		_sb_.append(",");
		_sb_.append(curstar);
		_sb_.append(",");
		_sb_.append(battle1);
		_sb_.append(",");
		_sb_.append(battle2);
		_sb_.append(",");
		_sb_.append(battle3);
		_sb_.append(",");
		_sb_.append(itemlevel);
		_sb_.append(",");
		_sb_.append(effects);
		_sb_.append(",");
		_sb_.append(failed);
		_sb_.append(",");
		_sb_.append(relivetimes);
		_sb_.append(",");
		_sb_.append(lastfighttime);
		_sb_.append(",");
		_sb_.append(totalstar);
		_sb_.append(",");
		_sb_.append(maxlevel);
		_sb_.append(",");
		_sb_.append(repeatstaraward);
		_sb_.append(",");
		_sb_.append(fixstaraward);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("curlevel"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lasthard"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("curstar"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("battle1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("battle2"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("battle3"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("itemlevel"));
		lb.add(new xdb.logs.ListenableMap().setVarName("effects"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("failed"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("relivetimes"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("lastfighttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("totalstar"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("maxlevel"));
		lb.add(new xdb.logs.ListenableMap().setVarName("repeatstaraward"));
		lb.add(new xdb.logs.ListenableMap().setVarName("fixstaraward"));
		return lb;
	}

	private class Const implements xbean.BloodRole {
		BloodRole nThis() {
			return BloodRole.this;
		}

		@Override
		public xbean.BloodRole copy() {
			return BloodRole.this.copy();
		}

		@Override
		public xbean.BloodRole toData() {
			return BloodRole.this.toData();
		}

		public xbean.BloodRole toBean() {
			return BloodRole.this.toBean();
		}

		@Override
		public xbean.BloodRole toDataIf() {
			return BloodRole.this.toDataIf();
		}

		public xbean.BloodRole toBeanIf() {
			return BloodRole.this.toBeanIf();
		}

		@Override
		public int getCurlevel() { // 当前层数
			_xdb_verify_unsafe_();
			return curlevel;
		}

		@Override
		public int getLasthard() { // 上一次战斗的难度
			_xdb_verify_unsafe_();
			return lasthard;
		}

		@Override
		public int getCurstar() { // 剩余没用的星
			_xdb_verify_unsafe_();
			return curstar;
		}

		@Override
		public int getBattle1() { // 随机出的战斗
			_xdb_verify_unsafe_();
			return battle1;
		}

		@Override
		public int getBattle2() { // 
			_xdb_verify_unsafe_();
			return battle2;
		}

		@Override
		public int getBattle3() { // 
			_xdb_verify_unsafe_();
			return battle3;
		}

		@Override
		public int getItemlevel() { // 已经获得的物品等级
			_xdb_verify_unsafe_();
			return itemlevel;
		}

		@Override
		public java.util.Map<Integer, Float> getEffects() { // 以前已加成的效果
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(effects);
		}

		@Override
		public java.util.Map<Integer, Float> getEffectsAsData() { // 以前已加成的效果
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Float> effects;
			BloodRole _o_ = BloodRole.this;
			effects = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
				effects.put(_e_.getKey(), _e_.getValue());
			return effects;
		}

		@Override
		public int getFailed() { // 1已失败
			_xdb_verify_unsafe_();
			return failed;
		}

		@Override
		public int getRelivetimes() { // 今天已复活次数
			_xdb_verify_unsafe_();
			return relivetimes;
		}

		@Override
		public long getLastfighttime() { // 上次战斗时间
			_xdb_verify_unsafe_();
			return lastfighttime;
		}

		@Override
		public int getTotalstar() { // 累计星
			_xdb_verify_unsafe_();
			return totalstar;
		}

		@Override
		public int getMaxlevel() { // 最高层
			_xdb_verify_unsafe_();
			return maxlevel;
		}

		@Override
		public java.util.Map<Integer, Integer> getRepeatstaraward() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(repeatstaraward);
		}

		@Override
		public java.util.Map<Integer, Integer> getRepeatstarawardAsData() { // 
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> repeatstaraward;
			BloodRole _o_ = BloodRole.this;
			repeatstaraward = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.repeatstaraward.entrySet())
				repeatstaraward.put(_e_.getKey(), _e_.getValue());
			return repeatstaraward;
		}

		@Override
		public java.util.Map<Integer, Integer> getFixstaraward() { // 
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(fixstaraward);
		}

		@Override
		public java.util.Map<Integer, Integer> getFixstarawardAsData() { // 
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> fixstaraward;
			BloodRole _o_ = BloodRole.this;
			fixstaraward = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.fixstaraward.entrySet())
				fixstaraward.put(_e_.getKey(), _e_.getValue());
			return fixstaraward;
		}

		@Override
		public void setCurlevel(int _v_) { // 当前层数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLasthard(int _v_) { // 上一次战斗的难度
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setCurstar(int _v_) { // 剩余没用的星
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBattle1(int _v_) { // 随机出的战斗
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBattle2(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBattle3(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setItemlevel(int _v_) { // 已经获得的物品等级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setFailed(int _v_) { // 1已失败
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRelivetimes(int _v_) { // 今天已复活次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLastfighttime(long _v_) { // 上次战斗时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTotalstar(int _v_) { // 累计星
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMaxlevel(int _v_) { // 最高层
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
			return BloodRole.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return BloodRole.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return BloodRole.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return BloodRole.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return BloodRole.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return BloodRole.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return BloodRole.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return BloodRole.this.hashCode();
		}

		@Override
		public String toString() {
			return BloodRole.this.toString();
		}

	}

	public static final class Data implements xbean.BloodRole {
		private int curlevel; // 当前层数
		private int lasthard; // 上一次战斗的难度
		private int curstar; // 剩余没用的星
		private int battle1; // 随机出的战斗
		private int battle2; // 
		private int battle3; // 
		private int itemlevel; // 已经获得的物品等级
		private java.util.HashMap<Integer, Float> effects; // 以前已加成的效果
		private int failed; // 1已失败
		private int relivetimes; // 今天已复活次数
		private long lastfighttime; // 上次战斗时间
		private int totalstar; // 累计星
		private int maxlevel; // 最高层
		private java.util.HashMap<Integer, Integer> repeatstaraward; // 
		private java.util.HashMap<Integer, Integer> fixstaraward; // 

		public Data() {
			curlevel = 1;
			effects = new java.util.HashMap<Integer, Float>();
			repeatstaraward = new java.util.HashMap<Integer, Integer>();
			fixstaraward = new java.util.HashMap<Integer, Integer>();
		}

		Data(xbean.BloodRole _o1_) {
			if (_o1_ instanceof BloodRole) assign((BloodRole)_o1_);
			else if (_o1_ instanceof BloodRole.Data) assign((BloodRole.Data)_o1_);
			else if (_o1_ instanceof BloodRole.Const) assign(((BloodRole.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(BloodRole _o_) {
			curlevel = _o_.curlevel;
			lasthard = _o_.lasthard;
			curstar = _o_.curstar;
			battle1 = _o_.battle1;
			battle2 = _o_.battle2;
			battle3 = _o_.battle3;
			itemlevel = _o_.itemlevel;
			effects = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
				effects.put(_e_.getKey(), _e_.getValue());
			failed = _o_.failed;
			relivetimes = _o_.relivetimes;
			lastfighttime = _o_.lastfighttime;
			totalstar = _o_.totalstar;
			maxlevel = _o_.maxlevel;
			repeatstaraward = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.repeatstaraward.entrySet())
				repeatstaraward.put(_e_.getKey(), _e_.getValue());
			fixstaraward = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.fixstaraward.entrySet())
				fixstaraward.put(_e_.getKey(), _e_.getValue());
		}

		private void assign(BloodRole.Data _o_) {
			curlevel = _o_.curlevel;
			lasthard = _o_.lasthard;
			curstar = _o_.curstar;
			battle1 = _o_.battle1;
			battle2 = _o_.battle2;
			battle3 = _o_.battle3;
			itemlevel = _o_.itemlevel;
			effects = new java.util.HashMap<Integer, Float>();
			for (java.util.Map.Entry<Integer, Float> _e_ : _o_.effects.entrySet())
				effects.put(_e_.getKey(), _e_.getValue());
			failed = _o_.failed;
			relivetimes = _o_.relivetimes;
			lastfighttime = _o_.lastfighttime;
			totalstar = _o_.totalstar;
			maxlevel = _o_.maxlevel;
			repeatstaraward = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.repeatstaraward.entrySet())
				repeatstaraward.put(_e_.getKey(), _e_.getValue());
			fixstaraward = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.fixstaraward.entrySet())
				fixstaraward.put(_e_.getKey(), _e_.getValue());
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(curlevel);
			_os_.marshal(lasthard);
			_os_.marshal(curstar);
			_os_.marshal(battle1);
			_os_.marshal(battle2);
			_os_.marshal(battle3);
			_os_.marshal(itemlevel);
			_os_.compact_uint32(effects.size());
			for (java.util.Map.Entry<Integer, Float> _e_ : effects.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.marshal(failed);
			_os_.marshal(relivetimes);
			_os_.marshal(lastfighttime);
			_os_.marshal(totalstar);
			_os_.marshal(maxlevel);
			_os_.compact_uint32(repeatstaraward.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : repeatstaraward.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.compact_uint32(fixstaraward.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : fixstaraward.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			curlevel = _os_.unmarshal_int();
			lasthard = _os_.unmarshal_int();
			curstar = _os_.unmarshal_int();
			battle1 = _os_.unmarshal_int();
			battle2 = _os_.unmarshal_int();
			battle3 = _os_.unmarshal_int();
			itemlevel = _os_.unmarshal_int();
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
			failed = _os_.unmarshal_int();
			relivetimes = _os_.unmarshal_int();
			lastfighttime = _os_.unmarshal_long();
			totalstar = _os_.unmarshal_int();
			maxlevel = _os_.unmarshal_int();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					repeatstaraward = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					repeatstaraward.put(_k_, _v_);
				}
			}
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					fixstaraward = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					fixstaraward.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.BloodRole copy() {
			return new Data(this);
		}

		@Override
		public xbean.BloodRole toData() {
			return new Data(this);
		}

		public xbean.BloodRole toBean() {
			return new BloodRole(this, null, null);
		}

		@Override
		public xbean.BloodRole toDataIf() {
			return this;
		}

		public xbean.BloodRole toBeanIf() {
			return new BloodRole(this, null, null);
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
		public int getCurlevel() { // 当前层数
			return curlevel;
		}

		@Override
		public int getLasthard() { // 上一次战斗的难度
			return lasthard;
		}

		@Override
		public int getCurstar() { // 剩余没用的星
			return curstar;
		}

		@Override
		public int getBattle1() { // 随机出的战斗
			return battle1;
		}

		@Override
		public int getBattle2() { // 
			return battle2;
		}

		@Override
		public int getBattle3() { // 
			return battle3;
		}

		@Override
		public int getItemlevel() { // 已经获得的物品等级
			return itemlevel;
		}

		@Override
		public java.util.Map<Integer, Float> getEffects() { // 以前已加成的效果
			return effects;
		}

		@Override
		public java.util.Map<Integer, Float> getEffectsAsData() { // 以前已加成的效果
			return effects;
		}

		@Override
		public int getFailed() { // 1已失败
			return failed;
		}

		@Override
		public int getRelivetimes() { // 今天已复活次数
			return relivetimes;
		}

		@Override
		public long getLastfighttime() { // 上次战斗时间
			return lastfighttime;
		}

		@Override
		public int getTotalstar() { // 累计星
			return totalstar;
		}

		@Override
		public int getMaxlevel() { // 最高层
			return maxlevel;
		}

		@Override
		public java.util.Map<Integer, Integer> getRepeatstaraward() { // 
			return repeatstaraward;
		}

		@Override
		public java.util.Map<Integer, Integer> getRepeatstarawardAsData() { // 
			return repeatstaraward;
		}

		@Override
		public java.util.Map<Integer, Integer> getFixstaraward() { // 
			return fixstaraward;
		}

		@Override
		public java.util.Map<Integer, Integer> getFixstarawardAsData() { // 
			return fixstaraward;
		}

		@Override
		public void setCurlevel(int _v_) { // 当前层数
			curlevel = _v_;
		}

		@Override
		public void setLasthard(int _v_) { // 上一次战斗的难度
			lasthard = _v_;
		}

		@Override
		public void setCurstar(int _v_) { // 剩余没用的星
			curstar = _v_;
		}

		@Override
		public void setBattle1(int _v_) { // 随机出的战斗
			battle1 = _v_;
		}

		@Override
		public void setBattle2(int _v_) { // 
			battle2 = _v_;
		}

		@Override
		public void setBattle3(int _v_) { // 
			battle3 = _v_;
		}

		@Override
		public void setItemlevel(int _v_) { // 已经获得的物品等级
			itemlevel = _v_;
		}

		@Override
		public void setFailed(int _v_) { // 1已失败
			failed = _v_;
		}

		@Override
		public void setRelivetimes(int _v_) { // 今天已复活次数
			relivetimes = _v_;
		}

		@Override
		public void setLastfighttime(long _v_) { // 上次战斗时间
			lastfighttime = _v_;
		}

		@Override
		public void setTotalstar(int _v_) { // 累计星
			totalstar = _v_;
		}

		@Override
		public void setMaxlevel(int _v_) { // 最高层
			maxlevel = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof BloodRole.Data)) return false;
			BloodRole.Data _o_ = (BloodRole.Data) _o1_;
			if (curlevel != _o_.curlevel) return false;
			if (lasthard != _o_.lasthard) return false;
			if (curstar != _o_.curstar) return false;
			if (battle1 != _o_.battle1) return false;
			if (battle2 != _o_.battle2) return false;
			if (battle3 != _o_.battle3) return false;
			if (itemlevel != _o_.itemlevel) return false;
			if (!effects.equals(_o_.effects)) return false;
			if (failed != _o_.failed) return false;
			if (relivetimes != _o_.relivetimes) return false;
			if (lastfighttime != _o_.lastfighttime) return false;
			if (totalstar != _o_.totalstar) return false;
			if (maxlevel != _o_.maxlevel) return false;
			if (!repeatstaraward.equals(_o_.repeatstaraward)) return false;
			if (!fixstaraward.equals(_o_.fixstaraward)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += curlevel;
			_h_ += lasthard;
			_h_ += curstar;
			_h_ += battle1;
			_h_ += battle2;
			_h_ += battle3;
			_h_ += itemlevel;
			_h_ += effects.hashCode();
			_h_ += failed;
			_h_ += relivetimes;
			_h_ += lastfighttime;
			_h_ += totalstar;
			_h_ += maxlevel;
			_h_ += repeatstaraward.hashCode();
			_h_ += fixstaraward.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(curlevel);
			_sb_.append(",");
			_sb_.append(lasthard);
			_sb_.append(",");
			_sb_.append(curstar);
			_sb_.append(",");
			_sb_.append(battle1);
			_sb_.append(",");
			_sb_.append(battle2);
			_sb_.append(",");
			_sb_.append(battle3);
			_sb_.append(",");
			_sb_.append(itemlevel);
			_sb_.append(",");
			_sb_.append(effects);
			_sb_.append(",");
			_sb_.append(failed);
			_sb_.append(",");
			_sb_.append(relivetimes);
			_sb_.append(",");
			_sb_.append(lastfighttime);
			_sb_.append(",");
			_sb_.append(totalstar);
			_sb_.append(",");
			_sb_.append(maxlevel);
			_sb_.append(",");
			_sb_.append(repeatstaraward);
			_sb_.append(",");
			_sb_.append(fixstaraward);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
