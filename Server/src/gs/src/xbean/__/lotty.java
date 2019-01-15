
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class lotty extends xdb.XBean implements xbean.lotty {
	private int normalrecruitnum; // 普通招募累计次数
	private long normalrecruittime; // 最后普通招募时间
	private int toprecruitnum; // 顶级招募累计次数
	private long toprecruittime; // 最后顶级招募时间
	private int toprecruitheronum; // 顶级招募累计次数，为招十次必得英雄准备
	private int toptentime; // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
	private long freetime; // 可以免费抽奖的时间
	private int firstget; // 首抽是否已经完成
	private int dreamexp; // 梦想值
	private int dreamfree; // 梦想改变是否免费
	private int dream; // 梦想兑换展示
	private java.util.HashMap<Integer, Integer> singlelotty; // 单抽增加值
	private java.util.HashMap<Integer, Integer> tenlotty; // 十连抽增加值
	private java.util.HashMap<Integer, Integer> tensinglelotty; // 十连抽大奖增加值
	private java.util.HashMap<Integer, Integer> getherolotty; // 梦想兑换增加值

	lotty(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		normalrecruitnum = 0;
		normalrecruittime = 0;
		toprecruitnum = 0;
		toprecruittime = 0;
		toprecruitheronum = 0;
		toptentime = 0;
		firstget = 0;
		singlelotty = new java.util.HashMap<Integer, Integer>();
		tenlotty = new java.util.HashMap<Integer, Integer>();
		tensinglelotty = new java.util.HashMap<Integer, Integer>();
		getherolotty = new java.util.HashMap<Integer, Integer>();
	}

	public lotty() {
		this(0, null, null);
	}

	public lotty(lotty _o_) {
		this(_o_, null, null);
	}

	lotty(xbean.lotty _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof lotty) assign((lotty)_o1_);
		else if (_o1_ instanceof lotty.Data) assign((lotty.Data)_o1_);
		else if (_o1_ instanceof lotty.Const) assign(((lotty.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(lotty _o_) {
		_o_._xdb_verify_unsafe_();
		normalrecruitnum = _o_.normalrecruitnum;
		normalrecruittime = _o_.normalrecruittime;
		toprecruitnum = _o_.toprecruitnum;
		toprecruittime = _o_.toprecruittime;
		toprecruitheronum = _o_.toprecruitheronum;
		toptentime = _o_.toptentime;
		freetime = _o_.freetime;
		firstget = _o_.firstget;
		dreamexp = _o_.dreamexp;
		dreamfree = _o_.dreamfree;
		dream = _o_.dream;
		singlelotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.singlelotty.entrySet())
			singlelotty.put(_e_.getKey(), _e_.getValue());
		tenlotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tenlotty.entrySet())
			tenlotty.put(_e_.getKey(), _e_.getValue());
		tensinglelotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tensinglelotty.entrySet())
			tensinglelotty.put(_e_.getKey(), _e_.getValue());
		getherolotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.getherolotty.entrySet())
			getherolotty.put(_e_.getKey(), _e_.getValue());
	}

	private void assign(lotty.Data _o_) {
		normalrecruitnum = _o_.normalrecruitnum;
		normalrecruittime = _o_.normalrecruittime;
		toprecruitnum = _o_.toprecruitnum;
		toprecruittime = _o_.toprecruittime;
		toprecruitheronum = _o_.toprecruitheronum;
		toptentime = _o_.toptentime;
		freetime = _o_.freetime;
		firstget = _o_.firstget;
		dreamexp = _o_.dreamexp;
		dreamfree = _o_.dreamfree;
		dream = _o_.dream;
		singlelotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.singlelotty.entrySet())
			singlelotty.put(_e_.getKey(), _e_.getValue());
		tenlotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tenlotty.entrySet())
			tenlotty.put(_e_.getKey(), _e_.getValue());
		tensinglelotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tensinglelotty.entrySet())
			tensinglelotty.put(_e_.getKey(), _e_.getValue());
		getherolotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.getherolotty.entrySet())
			getherolotty.put(_e_.getKey(), _e_.getValue());
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(normalrecruitnum);
		_os_.marshal(normalrecruittime);
		_os_.marshal(toprecruitnum);
		_os_.marshal(toprecruittime);
		_os_.marshal(toprecruitheronum);
		_os_.marshal(toptentime);
		_os_.marshal(freetime);
		_os_.marshal(firstget);
		_os_.marshal(dreamexp);
		_os_.marshal(dreamfree);
		_os_.marshal(dream);
		_os_.compact_uint32(singlelotty.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : singlelotty.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(tenlotty.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : tenlotty.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(tensinglelotty.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : tensinglelotty.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(getherolotty.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : getherolotty.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		normalrecruitnum = _os_.unmarshal_int();
		normalrecruittime = _os_.unmarshal_long();
		toprecruitnum = _os_.unmarshal_int();
		toprecruittime = _os_.unmarshal_long();
		toprecruitheronum = _os_.unmarshal_int();
		toptentime = _os_.unmarshal_int();
		freetime = _os_.unmarshal_long();
		firstget = _os_.unmarshal_int();
		dreamexp = _os_.unmarshal_int();
		dreamfree = _os_.unmarshal_int();
		dream = _os_.unmarshal_int();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				singlelotty = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				singlelotty.put(_k_, _v_);
			}
		}
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				tenlotty = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				tenlotty.put(_k_, _v_);
			}
		}
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				tensinglelotty = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				tensinglelotty.put(_k_, _v_);
			}
		}
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				getherolotty = new java.util.HashMap<Integer, Integer>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				getherolotty.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.lotty copy() {
		_xdb_verify_unsafe_();
		return new lotty(this);
	}

	@Override
	public xbean.lotty toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.lotty toBean() {
		_xdb_verify_unsafe_();
		return new lotty(this); // same as copy()
	}

	@Override
	public xbean.lotty toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.lotty toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getNormalrecruitnum() { // 普通招募累计次数
		_xdb_verify_unsafe_();
		return normalrecruitnum;
	}

	@Override
	public long getNormalrecruittime() { // 最后普通招募时间
		_xdb_verify_unsafe_();
		return normalrecruittime;
	}

	@Override
	public int getToprecruitnum() { // 顶级招募累计次数
		_xdb_verify_unsafe_();
		return toprecruitnum;
	}

	@Override
	public long getToprecruittime() { // 最后顶级招募时间
		_xdb_verify_unsafe_();
		return toprecruittime;
	}

	@Override
	public int getToprecruitheronum() { // 顶级招募累计次数，为招十次必得英雄准备
		_xdb_verify_unsafe_();
		return toprecruitheronum;
	}

	@Override
	public int getToptentime() { // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
		_xdb_verify_unsafe_();
		return toptentime;
	}

	@Override
	public long getFreetime() { // 可以免费抽奖的时间
		_xdb_verify_unsafe_();
		return freetime;
	}

	@Override
	public int getFirstget() { // 首抽是否已经完成
		_xdb_verify_unsafe_();
		return firstget;
	}

	@Override
	public int getDreamexp() { // 梦想值
		_xdb_verify_unsafe_();
		return dreamexp;
	}

	@Override
	public int getDreamfree() { // 梦想改变是否免费
		_xdb_verify_unsafe_();
		return dreamfree;
	}

	@Override
	public int getDream() { // 梦想兑换展示
		_xdb_verify_unsafe_();
		return dream;
	}

	@Override
	public java.util.Map<Integer, Integer> getSinglelotty() { // 单抽增加值
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "singlelotty"), singlelotty);
	}

	@Override
	public java.util.Map<Integer, Integer> getSinglelottyAsData() { // 单抽增加值
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> singlelotty;
		lotty _o_ = this;
		singlelotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.singlelotty.entrySet())
			singlelotty.put(_e_.getKey(), _e_.getValue());
		return singlelotty;
	}

	@Override
	public java.util.Map<Integer, Integer> getTenlotty() { // 十连抽增加值
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "tenlotty"), tenlotty);
	}

	@Override
	public java.util.Map<Integer, Integer> getTenlottyAsData() { // 十连抽增加值
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> tenlotty;
		lotty _o_ = this;
		tenlotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tenlotty.entrySet())
			tenlotty.put(_e_.getKey(), _e_.getValue());
		return tenlotty;
	}

	@Override
	public java.util.Map<Integer, Integer> getTensinglelotty() { // 十连抽大奖增加值
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "tensinglelotty"), tensinglelotty);
	}

	@Override
	public java.util.Map<Integer, Integer> getTensinglelottyAsData() { // 十连抽大奖增加值
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> tensinglelotty;
		lotty _o_ = this;
		tensinglelotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tensinglelotty.entrySet())
			tensinglelotty.put(_e_.getKey(), _e_.getValue());
		return tensinglelotty;
	}

	@Override
	public java.util.Map<Integer, Integer> getGetherolotty() { // 梦想兑换增加值
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "getherolotty"), getherolotty);
	}

	@Override
	public java.util.Map<Integer, Integer> getGetherolottyAsData() { // 梦想兑换增加值
		_xdb_verify_unsafe_();
		java.util.Map<Integer, Integer> getherolotty;
		lotty _o_ = this;
		getherolotty = new java.util.HashMap<Integer, Integer>();
		for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.getherolotty.entrySet())
			getherolotty.put(_e_.getKey(), _e_.getValue());
		return getherolotty;
	}

	@Override
	public void setNormalrecruitnum(int _v_) { // 普通招募累计次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "normalrecruitnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, normalrecruitnum) {
					public void rollback() { normalrecruitnum = _xdb_saved; }
				};}});
		normalrecruitnum = _v_;
	}

	@Override
	public void setNormalrecruittime(long _v_) { // 最后普通招募时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "normalrecruittime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, normalrecruittime) {
					public void rollback() { normalrecruittime = _xdb_saved; }
				};}});
		normalrecruittime = _v_;
	}

	@Override
	public void setToprecruitnum(int _v_) { // 顶级招募累计次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "toprecruitnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, toprecruitnum) {
					public void rollback() { toprecruitnum = _xdb_saved; }
				};}});
		toprecruitnum = _v_;
	}

	@Override
	public void setToprecruittime(long _v_) { // 最后顶级招募时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "toprecruittime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, toprecruittime) {
					public void rollback() { toprecruittime = _xdb_saved; }
				};}});
		toprecruittime = _v_;
	}

	@Override
	public void setToprecruitheronum(int _v_) { // 顶级招募累计次数，为招十次必得英雄准备
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "toprecruitheronum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, toprecruitheronum) {
					public void rollback() { toprecruitheronum = _xdb_saved; }
				};}});
		toprecruitheronum = _v_;
	}

	@Override
	public void setToptentime(int _v_) { // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "toptentime") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, toptentime) {
					public void rollback() { toptentime = _xdb_saved; }
				};}});
		toptentime = _v_;
	}

	@Override
	public void setFreetime(long _v_) { // 可以免费抽奖的时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "freetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, freetime) {
					public void rollback() { freetime = _xdb_saved; }
				};}});
		freetime = _v_;
	}

	@Override
	public void setFirstget(int _v_) { // 首抽是否已经完成
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "firstget") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, firstget) {
					public void rollback() { firstget = _xdb_saved; }
				};}});
		firstget = _v_;
	}

	@Override
	public void setDreamexp(int _v_) { // 梦想值
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "dreamexp") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, dreamexp) {
					public void rollback() { dreamexp = _xdb_saved; }
				};}});
		dreamexp = _v_;
	}

	@Override
	public void setDreamfree(int _v_) { // 梦想改变是否免费
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "dreamfree") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, dreamfree) {
					public void rollback() { dreamfree = _xdb_saved; }
				};}});
		dreamfree = _v_;
	}

	@Override
	public void setDream(int _v_) { // 梦想兑换展示
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "dream") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, dream) {
					public void rollback() { dream = _xdb_saved; }
				};}});
		dream = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		lotty _o_ = null;
		if ( _o1_ instanceof lotty ) _o_ = (lotty)_o1_;
		else if ( _o1_ instanceof lotty.Const ) _o_ = ((lotty.Const)_o1_).nThis();
		else return false;
		if (normalrecruitnum != _o_.normalrecruitnum) return false;
		if (normalrecruittime != _o_.normalrecruittime) return false;
		if (toprecruitnum != _o_.toprecruitnum) return false;
		if (toprecruittime != _o_.toprecruittime) return false;
		if (toprecruitheronum != _o_.toprecruitheronum) return false;
		if (toptentime != _o_.toptentime) return false;
		if (freetime != _o_.freetime) return false;
		if (firstget != _o_.firstget) return false;
		if (dreamexp != _o_.dreamexp) return false;
		if (dreamfree != _o_.dreamfree) return false;
		if (dream != _o_.dream) return false;
		if (!singlelotty.equals(_o_.singlelotty)) return false;
		if (!tenlotty.equals(_o_.tenlotty)) return false;
		if (!tensinglelotty.equals(_o_.tensinglelotty)) return false;
		if (!getherolotty.equals(_o_.getherolotty)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += normalrecruitnum;
		_h_ += normalrecruittime;
		_h_ += toprecruitnum;
		_h_ += toprecruittime;
		_h_ += toprecruitheronum;
		_h_ += toptentime;
		_h_ += freetime;
		_h_ += firstget;
		_h_ += dreamexp;
		_h_ += dreamfree;
		_h_ += dream;
		_h_ += singlelotty.hashCode();
		_h_ += tenlotty.hashCode();
		_h_ += tensinglelotty.hashCode();
		_h_ += getherolotty.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(normalrecruitnum);
		_sb_.append(",");
		_sb_.append(normalrecruittime);
		_sb_.append(",");
		_sb_.append(toprecruitnum);
		_sb_.append(",");
		_sb_.append(toprecruittime);
		_sb_.append(",");
		_sb_.append(toprecruitheronum);
		_sb_.append(",");
		_sb_.append(toptentime);
		_sb_.append(",");
		_sb_.append(freetime);
		_sb_.append(",");
		_sb_.append(firstget);
		_sb_.append(",");
		_sb_.append(dreamexp);
		_sb_.append(",");
		_sb_.append(dreamfree);
		_sb_.append(",");
		_sb_.append(dream);
		_sb_.append(",");
		_sb_.append(singlelotty);
		_sb_.append(",");
		_sb_.append(tenlotty);
		_sb_.append(",");
		_sb_.append(tensinglelotty);
		_sb_.append(",");
		_sb_.append(getherolotty);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("normalrecruitnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("normalrecruittime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("toprecruitnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("toprecruittime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("toprecruitheronum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("toptentime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("freetime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("firstget"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("dreamexp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("dreamfree"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("dream"));
		lb.add(new xdb.logs.ListenableMap().setVarName("singlelotty"));
		lb.add(new xdb.logs.ListenableMap().setVarName("tenlotty"));
		lb.add(new xdb.logs.ListenableMap().setVarName("tensinglelotty"));
		lb.add(new xdb.logs.ListenableMap().setVarName("getherolotty"));
		return lb;
	}

	private class Const implements xbean.lotty {
		lotty nThis() {
			return lotty.this;
		}

		@Override
		public xbean.lotty copy() {
			return lotty.this.copy();
		}

		@Override
		public xbean.lotty toData() {
			return lotty.this.toData();
		}

		public xbean.lotty toBean() {
			return lotty.this.toBean();
		}

		@Override
		public xbean.lotty toDataIf() {
			return lotty.this.toDataIf();
		}

		public xbean.lotty toBeanIf() {
			return lotty.this.toBeanIf();
		}

		@Override
		public int getNormalrecruitnum() { // 普通招募累计次数
			_xdb_verify_unsafe_();
			return normalrecruitnum;
		}

		@Override
		public long getNormalrecruittime() { // 最后普通招募时间
			_xdb_verify_unsafe_();
			return normalrecruittime;
		}

		@Override
		public int getToprecruitnum() { // 顶级招募累计次数
			_xdb_verify_unsafe_();
			return toprecruitnum;
		}

		@Override
		public long getToprecruittime() { // 最后顶级招募时间
			_xdb_verify_unsafe_();
			return toprecruittime;
		}

		@Override
		public int getToprecruitheronum() { // 顶级招募累计次数，为招十次必得英雄准备
			_xdb_verify_unsafe_();
			return toprecruitheronum;
		}

		@Override
		public int getToptentime() { // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
			_xdb_verify_unsafe_();
			return toptentime;
		}

		@Override
		public long getFreetime() { // 可以免费抽奖的时间
			_xdb_verify_unsafe_();
			return freetime;
		}

		@Override
		public int getFirstget() { // 首抽是否已经完成
			_xdb_verify_unsafe_();
			return firstget;
		}

		@Override
		public int getDreamexp() { // 梦想值
			_xdb_verify_unsafe_();
			return dreamexp;
		}

		@Override
		public int getDreamfree() { // 梦想改变是否免费
			_xdb_verify_unsafe_();
			return dreamfree;
		}

		@Override
		public int getDream() { // 梦想兑换展示
			_xdb_verify_unsafe_();
			return dream;
		}

		@Override
		public java.util.Map<Integer, Integer> getSinglelotty() { // 单抽增加值
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(singlelotty);
		}

		@Override
		public java.util.Map<Integer, Integer> getSinglelottyAsData() { // 单抽增加值
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> singlelotty;
			lotty _o_ = lotty.this;
			singlelotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.singlelotty.entrySet())
				singlelotty.put(_e_.getKey(), _e_.getValue());
			return singlelotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getTenlotty() { // 十连抽增加值
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(tenlotty);
		}

		@Override
		public java.util.Map<Integer, Integer> getTenlottyAsData() { // 十连抽增加值
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> tenlotty;
			lotty _o_ = lotty.this;
			tenlotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tenlotty.entrySet())
				tenlotty.put(_e_.getKey(), _e_.getValue());
			return tenlotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getTensinglelotty() { // 十连抽大奖增加值
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(tensinglelotty);
		}

		@Override
		public java.util.Map<Integer, Integer> getTensinglelottyAsData() { // 十连抽大奖增加值
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> tensinglelotty;
			lotty _o_ = lotty.this;
			tensinglelotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tensinglelotty.entrySet())
				tensinglelotty.put(_e_.getKey(), _e_.getValue());
			return tensinglelotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getGetherolotty() { // 梦想兑换增加值
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(getherolotty);
		}

		@Override
		public java.util.Map<Integer, Integer> getGetherolottyAsData() { // 梦想兑换增加值
			_xdb_verify_unsafe_();
			java.util.Map<Integer, Integer> getherolotty;
			lotty _o_ = lotty.this;
			getherolotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.getherolotty.entrySet())
				getherolotty.put(_e_.getKey(), _e_.getValue());
			return getherolotty;
		}

		@Override
		public void setNormalrecruitnum(int _v_) { // 普通招募累计次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNormalrecruittime(long _v_) { // 最后普通招募时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setToprecruitnum(int _v_) { // 顶级招募累计次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setToprecruittime(long _v_) { // 最后顶级招募时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setToprecruitheronum(int _v_) { // 顶级招募累计次数，为招十次必得英雄准备
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setToptentime(int _v_) { // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setFreetime(long _v_) { // 可以免费抽奖的时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setFirstget(int _v_) { // 首抽是否已经完成
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDreamexp(int _v_) { // 梦想值
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDreamfree(int _v_) { // 梦想改变是否免费
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDream(int _v_) { // 梦想兑换展示
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
			return lotty.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return lotty.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return lotty.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return lotty.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return lotty.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return lotty.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return lotty.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return lotty.this.hashCode();
		}

		@Override
		public String toString() {
			return lotty.this.toString();
		}

	}

	public static final class Data implements xbean.lotty {
		private int normalrecruitnum; // 普通招募累计次数
		private long normalrecruittime; // 最后普通招募时间
		private int toprecruitnum; // 顶级招募累计次数
		private long toprecruittime; // 最后顶级招募时间
		private int toprecruitheronum; // 顶级招募累计次数，为招十次必得英雄准备
		private int toptentime; // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
		private long freetime; // 可以免费抽奖的时间
		private int firstget; // 首抽是否已经完成
		private int dreamexp; // 梦想值
		private int dreamfree; // 梦想改变是否免费
		private int dream; // 梦想兑换展示
		private java.util.HashMap<Integer, Integer> singlelotty; // 单抽增加值
		private java.util.HashMap<Integer, Integer> tenlotty; // 十连抽增加值
		private java.util.HashMap<Integer, Integer> tensinglelotty; // 十连抽大奖增加值
		private java.util.HashMap<Integer, Integer> getherolotty; // 梦想兑换增加值

		public Data() {
			normalrecruitnum = 0;
			normalrecruittime = 0;
			toprecruitnum = 0;
			toprecruittime = 0;
			toprecruitheronum = 0;
			toptentime = 0;
			firstget = 0;
			singlelotty = new java.util.HashMap<Integer, Integer>();
			tenlotty = new java.util.HashMap<Integer, Integer>();
			tensinglelotty = new java.util.HashMap<Integer, Integer>();
			getherolotty = new java.util.HashMap<Integer, Integer>();
		}

		Data(xbean.lotty _o1_) {
			if (_o1_ instanceof lotty) assign((lotty)_o1_);
			else if (_o1_ instanceof lotty.Data) assign((lotty.Data)_o1_);
			else if (_o1_ instanceof lotty.Const) assign(((lotty.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(lotty _o_) {
			normalrecruitnum = _o_.normalrecruitnum;
			normalrecruittime = _o_.normalrecruittime;
			toprecruitnum = _o_.toprecruitnum;
			toprecruittime = _o_.toprecruittime;
			toprecruitheronum = _o_.toprecruitheronum;
			toptentime = _o_.toptentime;
			freetime = _o_.freetime;
			firstget = _o_.firstget;
			dreamexp = _o_.dreamexp;
			dreamfree = _o_.dreamfree;
			dream = _o_.dream;
			singlelotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.singlelotty.entrySet())
				singlelotty.put(_e_.getKey(), _e_.getValue());
			tenlotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tenlotty.entrySet())
				tenlotty.put(_e_.getKey(), _e_.getValue());
			tensinglelotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tensinglelotty.entrySet())
				tensinglelotty.put(_e_.getKey(), _e_.getValue());
			getherolotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.getherolotty.entrySet())
				getherolotty.put(_e_.getKey(), _e_.getValue());
		}

		private void assign(lotty.Data _o_) {
			normalrecruitnum = _o_.normalrecruitnum;
			normalrecruittime = _o_.normalrecruittime;
			toprecruitnum = _o_.toprecruitnum;
			toprecruittime = _o_.toprecruittime;
			toprecruitheronum = _o_.toprecruitheronum;
			toptentime = _o_.toptentime;
			freetime = _o_.freetime;
			firstget = _o_.firstget;
			dreamexp = _o_.dreamexp;
			dreamfree = _o_.dreamfree;
			dream = _o_.dream;
			singlelotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.singlelotty.entrySet())
				singlelotty.put(_e_.getKey(), _e_.getValue());
			tenlotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tenlotty.entrySet())
				tenlotty.put(_e_.getKey(), _e_.getValue());
			tensinglelotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.tensinglelotty.entrySet())
				tensinglelotty.put(_e_.getKey(), _e_.getValue());
			getherolotty = new java.util.HashMap<Integer, Integer>();
			for (java.util.Map.Entry<Integer, Integer> _e_ : _o_.getherolotty.entrySet())
				getherolotty.put(_e_.getKey(), _e_.getValue());
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(normalrecruitnum);
			_os_.marshal(normalrecruittime);
			_os_.marshal(toprecruitnum);
			_os_.marshal(toprecruittime);
			_os_.marshal(toprecruitheronum);
			_os_.marshal(toptentime);
			_os_.marshal(freetime);
			_os_.marshal(firstget);
			_os_.marshal(dreamexp);
			_os_.marshal(dreamfree);
			_os_.marshal(dream);
			_os_.compact_uint32(singlelotty.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : singlelotty.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.compact_uint32(tenlotty.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : tenlotty.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.compact_uint32(tensinglelotty.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : tensinglelotty.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			_os_.compact_uint32(getherolotty.size());
			for (java.util.Map.Entry<Integer, Integer> _e_ : getherolotty.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_os_.marshal(_e_.getValue());
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			normalrecruitnum = _os_.unmarshal_int();
			normalrecruittime = _os_.unmarshal_long();
			toprecruitnum = _os_.unmarshal_int();
			toprecruittime = _os_.unmarshal_long();
			toprecruitheronum = _os_.unmarshal_int();
			toptentime = _os_.unmarshal_int();
			freetime = _os_.unmarshal_long();
			firstget = _os_.unmarshal_int();
			dreamexp = _os_.unmarshal_int();
			dreamfree = _os_.unmarshal_int();
			dream = _os_.unmarshal_int();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					singlelotty = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					singlelotty.put(_k_, _v_);
				}
			}
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					tenlotty = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					tenlotty.put(_k_, _v_);
				}
			}
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					tensinglelotty = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					tensinglelotty.put(_k_, _v_);
				}
			}
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					getherolotty = new java.util.HashMap<Integer, Integer>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					int _v_ = 0;
					_v_ = _os_.unmarshal_int();
					getherolotty.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.lotty copy() {
			return new Data(this);
		}

		@Override
		public xbean.lotty toData() {
			return new Data(this);
		}

		public xbean.lotty toBean() {
			return new lotty(this, null, null);
		}

		@Override
		public xbean.lotty toDataIf() {
			return this;
		}

		public xbean.lotty toBeanIf() {
			return new lotty(this, null, null);
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
		public int getNormalrecruitnum() { // 普通招募累计次数
			return normalrecruitnum;
		}

		@Override
		public long getNormalrecruittime() { // 最后普通招募时间
			return normalrecruittime;
		}

		@Override
		public int getToprecruitnum() { // 顶级招募累计次数
			return toprecruitnum;
		}

		@Override
		public long getToprecruittime() { // 最后顶级招募时间
			return toprecruittime;
		}

		@Override
		public int getToprecruitheronum() { // 顶级招募累计次数，为招十次必得英雄准备
			return toprecruitheronum;
		}

		@Override
		public int getToptentime() { // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
			return toptentime;
		}

		@Override
		public long getFreetime() { // 可以免费抽奖的时间
			return freetime;
		}

		@Override
		public int getFirstget() { // 首抽是否已经完成
			return firstget;
		}

		@Override
		public int getDreamexp() { // 梦想值
			return dreamexp;
		}

		@Override
		public int getDreamfree() { // 梦想改变是否免费
			return dreamfree;
		}

		@Override
		public int getDream() { // 梦想兑换展示
			return dream;
		}

		@Override
		public java.util.Map<Integer, Integer> getSinglelotty() { // 单抽增加值
			return singlelotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getSinglelottyAsData() { // 单抽增加值
			return singlelotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getTenlotty() { // 十连抽增加值
			return tenlotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getTenlottyAsData() { // 十连抽增加值
			return tenlotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getTensinglelotty() { // 十连抽大奖增加值
			return tensinglelotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getTensinglelottyAsData() { // 十连抽大奖增加值
			return tensinglelotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getGetherolotty() { // 梦想兑换增加值
			return getherolotty;
		}

		@Override
		public java.util.Map<Integer, Integer> getGetherolottyAsData() { // 梦想兑换增加值
			return getherolotty;
		}

		@Override
		public void setNormalrecruitnum(int _v_) { // 普通招募累计次数
			normalrecruitnum = _v_;
		}

		@Override
		public void setNormalrecruittime(long _v_) { // 最后普通招募时间
			normalrecruittime = _v_;
		}

		@Override
		public void setToprecruitnum(int _v_) { // 顶级招募累计次数
			toprecruitnum = _v_;
		}

		@Override
		public void setToprecruittime(long _v_) { // 最后顶级招募时间
			toprecruittime = _v_;
		}

		@Override
		public void setToprecruitheronum(int _v_) { // 顶级招募累计次数，为招十次必得英雄准备
			toprecruitheronum = _v_;
		}

		@Override
		public void setToptentime(int _v_) { // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
			toptentime = _v_;
		}

		@Override
		public void setFreetime(long _v_) { // 可以免费抽奖的时间
			freetime = _v_;
		}

		@Override
		public void setFirstget(int _v_) { // 首抽是否已经完成
			firstget = _v_;
		}

		@Override
		public void setDreamexp(int _v_) { // 梦想值
			dreamexp = _v_;
		}

		@Override
		public void setDreamfree(int _v_) { // 梦想改变是否免费
			dreamfree = _v_;
		}

		@Override
		public void setDream(int _v_) { // 梦想兑换展示
			dream = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof lotty.Data)) return false;
			lotty.Data _o_ = (lotty.Data) _o1_;
			if (normalrecruitnum != _o_.normalrecruitnum) return false;
			if (normalrecruittime != _o_.normalrecruittime) return false;
			if (toprecruitnum != _o_.toprecruitnum) return false;
			if (toprecruittime != _o_.toprecruittime) return false;
			if (toprecruitheronum != _o_.toprecruitheronum) return false;
			if (toptentime != _o_.toptentime) return false;
			if (freetime != _o_.freetime) return false;
			if (firstget != _o_.firstget) return false;
			if (dreamexp != _o_.dreamexp) return false;
			if (dreamfree != _o_.dreamfree) return false;
			if (dream != _o_.dream) return false;
			if (!singlelotty.equals(_o_.singlelotty)) return false;
			if (!tenlotty.equals(_o_.tenlotty)) return false;
			if (!tensinglelotty.equals(_o_.tensinglelotty)) return false;
			if (!getherolotty.equals(_o_.getherolotty)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += normalrecruitnum;
			_h_ += normalrecruittime;
			_h_ += toprecruitnum;
			_h_ += toprecruittime;
			_h_ += toprecruitheronum;
			_h_ += toptentime;
			_h_ += freetime;
			_h_ += firstget;
			_h_ += dreamexp;
			_h_ += dreamfree;
			_h_ += dream;
			_h_ += singlelotty.hashCode();
			_h_ += tenlotty.hashCode();
			_h_ += tensinglelotty.hashCode();
			_h_ += getherolotty.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(normalrecruitnum);
			_sb_.append(",");
			_sb_.append(normalrecruittime);
			_sb_.append(",");
			_sb_.append(toprecruitnum);
			_sb_.append(",");
			_sb_.append(toprecruittime);
			_sb_.append(",");
			_sb_.append(toprecruitheronum);
			_sb_.append(",");
			_sb_.append(toptentime);
			_sb_.append(",");
			_sb_.append(freetime);
			_sb_.append(",");
			_sb_.append(firstget);
			_sb_.append(",");
			_sb_.append(dreamexp);
			_sb_.append(",");
			_sb_.append(dreamfree);
			_sb_.append(",");
			_sb_.append(dream);
			_sb_.append(",");
			_sb_.append(singlelotty);
			_sb_.append(",");
			_sb_.append(tenlotty);
			_sb_.append(",");
			_sb_.append(tensinglelotty);
			_sb_.append(",");
			_sb_.append(getherolotty);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
