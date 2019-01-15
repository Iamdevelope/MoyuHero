
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class huoyues extends xdb.XBean implements xbean.huoyues {
	private int huoyuenum; // 活跃度
	private int getnum; // 领取记录，个位第一个，十位第二个~~
	private long huoyuetime; // 刷新时间，跨天用
	private java.util.HashMap<Integer, xbean.huoyue> huoyuemap; // 活跃任务列表，key为选择类型

	huoyues(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		huoyuenum = 0;
		getnum = 0;
		huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>();
	}

	public huoyues() {
		this(0, null, null);
	}

	public huoyues(huoyues _o_) {
		this(_o_, null, null);
	}

	huoyues(xbean.huoyues _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof huoyues) assign((huoyues)_o1_);
		else if (_o1_ instanceof huoyues.Data) assign((huoyues.Data)_o1_);
		else if (_o1_ instanceof huoyues.Const) assign(((huoyues.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(huoyues _o_) {
		_o_._xdb_verify_unsafe_();
		huoyuenum = _o_.huoyuenum;
		getnum = _o_.getnum;
		huoyuetime = _o_.huoyuetime;
		huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>();
		for (java.util.Map.Entry<Integer, xbean.huoyue> _e_ : _o_.huoyuemap.entrySet())
			huoyuemap.put(_e_.getKey(), new huoyue(_e_.getValue(), this, "huoyuemap"));
	}

	private void assign(huoyues.Data _o_) {
		huoyuenum = _o_.huoyuenum;
		getnum = _o_.getnum;
		huoyuetime = _o_.huoyuetime;
		huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>();
		for (java.util.Map.Entry<Integer, xbean.huoyue> _e_ : _o_.huoyuemap.entrySet())
			huoyuemap.put(_e_.getKey(), new huoyue(_e_.getValue(), this, "huoyuemap"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(huoyuenum);
		_os_.marshal(getnum);
		_os_.marshal(huoyuetime);
		_os_.compact_uint32(huoyuemap.size());
		for (java.util.Map.Entry<Integer, xbean.huoyue> _e_ : huoyuemap.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		huoyuenum = _os_.unmarshal_int();
		getnum = _os_.unmarshal_int();
		huoyuetime = _os_.unmarshal_long();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.huoyue _v_ = new huoyue(0, this, "huoyuemap");
				_v_.unmarshal(_os_);
				huoyuemap.put(_k_, _v_);
			}
		}
		return _os_;
	}

	@Override
	public xbean.huoyues copy() {
		_xdb_verify_unsafe_();
		return new huoyues(this);
	}

	@Override
	public xbean.huoyues toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.huoyues toBean() {
		_xdb_verify_unsafe_();
		return new huoyues(this); // same as copy()
	}

	@Override
	public xbean.huoyues toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.huoyues toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getHuoyuenum() { // 活跃度
		_xdb_verify_unsafe_();
		return huoyuenum;
	}

	@Override
	public int getGetnum() { // 领取记录，个位第一个，十位第二个~~
		_xdb_verify_unsafe_();
		return getnum;
	}

	@Override
	public long getHuoyuetime() { // 刷新时间，跨天用
		_xdb_verify_unsafe_();
		return huoyuetime;
	}

	@Override
	public java.util.Map<Integer, xbean.huoyue> getHuoyuemap() { // 活跃任务列表，key为选择类型
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "huoyuemap"), huoyuemap);
	}

	@Override
	public java.util.Map<Integer, xbean.huoyue> getHuoyuemapAsData() { // 活跃任务列表，key为选择类型
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.huoyue> huoyuemap;
		huoyues _o_ = this;
		huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>();
		for (java.util.Map.Entry<Integer, xbean.huoyue> _e_ : _o_.huoyuemap.entrySet())
			huoyuemap.put(_e_.getKey(), new huoyue.Data(_e_.getValue()));
		return huoyuemap;
	}

	@Override
	public void setHuoyuenum(int _v_) { // 活跃度
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "huoyuenum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, huoyuenum) {
					public void rollback() { huoyuenum = _xdb_saved; }
				};}});
		huoyuenum = _v_;
	}

	@Override
	public void setGetnum(int _v_) { // 领取记录，个位第一个，十位第二个~~
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "getnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, getnum) {
					public void rollback() { getnum = _xdb_saved; }
				};}});
		getnum = _v_;
	}

	@Override
	public void setHuoyuetime(long _v_) { // 刷新时间，跨天用
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "huoyuetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, huoyuetime) {
					public void rollback() { huoyuetime = _xdb_saved; }
				};}});
		huoyuetime = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		huoyues _o_ = null;
		if ( _o1_ instanceof huoyues ) _o_ = (huoyues)_o1_;
		else if ( _o1_ instanceof huoyues.Const ) _o_ = ((huoyues.Const)_o1_).nThis();
		else return false;
		if (huoyuenum != _o_.huoyuenum) return false;
		if (getnum != _o_.getnum) return false;
		if (huoyuetime != _o_.huoyuetime) return false;
		if (!huoyuemap.equals(_o_.huoyuemap)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += huoyuenum;
		_h_ += getnum;
		_h_ += huoyuetime;
		_h_ += huoyuemap.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(huoyuenum);
		_sb_.append(",");
		_sb_.append(getnum);
		_sb_.append(",");
		_sb_.append(huoyuetime);
		_sb_.append(",");
		_sb_.append(huoyuemap);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("huoyuenum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("getnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("huoyuetime"));
		lb.add(new xdb.logs.ListenableMap().setVarName("huoyuemap"));
		return lb;
	}

	private class Const implements xbean.huoyues {
		huoyues nThis() {
			return huoyues.this;
		}

		@Override
		public xbean.huoyues copy() {
			return huoyues.this.copy();
		}

		@Override
		public xbean.huoyues toData() {
			return huoyues.this.toData();
		}

		public xbean.huoyues toBean() {
			return huoyues.this.toBean();
		}

		@Override
		public xbean.huoyues toDataIf() {
			return huoyues.this.toDataIf();
		}

		public xbean.huoyues toBeanIf() {
			return huoyues.this.toBeanIf();
		}

		@Override
		public int getHuoyuenum() { // 活跃度
			_xdb_verify_unsafe_();
			return huoyuenum;
		}

		@Override
		public int getGetnum() { // 领取记录，个位第一个，十位第二个~~
			_xdb_verify_unsafe_();
			return getnum;
		}

		@Override
		public long getHuoyuetime() { // 刷新时间，跨天用
			_xdb_verify_unsafe_();
			return huoyuetime;
		}

		@Override
		public java.util.Map<Integer, xbean.huoyue> getHuoyuemap() { // 活跃任务列表，key为选择类型
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(huoyuemap);
		}

		@Override
		public java.util.Map<Integer, xbean.huoyue> getHuoyuemapAsData() { // 活跃任务列表，key为选择类型
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.huoyue> huoyuemap;
			huoyues _o_ = huoyues.this;
			huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>();
			for (java.util.Map.Entry<Integer, xbean.huoyue> _e_ : _o_.huoyuemap.entrySet())
				huoyuemap.put(_e_.getKey(), new huoyue.Data(_e_.getValue()));
			return huoyuemap;
		}

		@Override
		public void setHuoyuenum(int _v_) { // 活跃度
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGetnum(int _v_) { // 领取记录，个位第一个，十位第二个~~
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHuoyuetime(long _v_) { // 刷新时间，跨天用
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
			return huoyues.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return huoyues.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return huoyues.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return huoyues.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return huoyues.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return huoyues.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return huoyues.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return huoyues.this.hashCode();
		}

		@Override
		public String toString() {
			return huoyues.this.toString();
		}

	}

	public static final class Data implements xbean.huoyues {
		private int huoyuenum; // 活跃度
		private int getnum; // 领取记录，个位第一个，十位第二个~~
		private long huoyuetime; // 刷新时间，跨天用
		private java.util.HashMap<Integer, xbean.huoyue> huoyuemap; // 活跃任务列表，key为选择类型

		public Data() {
			huoyuenum = 0;
			getnum = 0;
			huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>();
		}

		Data(xbean.huoyues _o1_) {
			if (_o1_ instanceof huoyues) assign((huoyues)_o1_);
			else if (_o1_ instanceof huoyues.Data) assign((huoyues.Data)_o1_);
			else if (_o1_ instanceof huoyues.Const) assign(((huoyues.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(huoyues _o_) {
			huoyuenum = _o_.huoyuenum;
			getnum = _o_.getnum;
			huoyuetime = _o_.huoyuetime;
			huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>();
			for (java.util.Map.Entry<Integer, xbean.huoyue> _e_ : _o_.huoyuemap.entrySet())
				huoyuemap.put(_e_.getKey(), new huoyue.Data(_e_.getValue()));
		}

		private void assign(huoyues.Data _o_) {
			huoyuenum = _o_.huoyuenum;
			getnum = _o_.getnum;
			huoyuetime = _o_.huoyuetime;
			huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>();
			for (java.util.Map.Entry<Integer, xbean.huoyue> _e_ : _o_.huoyuemap.entrySet())
				huoyuemap.put(_e_.getKey(), new huoyue.Data(_e_.getValue()));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(huoyuenum);
			_os_.marshal(getnum);
			_os_.marshal(huoyuetime);
			_os_.compact_uint32(huoyuemap.size());
			for (java.util.Map.Entry<Integer, xbean.huoyue> _e_ : huoyuemap.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			huoyuenum = _os_.unmarshal_int();
			getnum = _os_.unmarshal_int();
			huoyuetime = _os_.unmarshal_long();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					huoyuemap = new java.util.HashMap<Integer, xbean.huoyue>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.huoyue _v_ = xbean.Pod.newhuoyueData();
					_v_.unmarshal(_os_);
					huoyuemap.put(_k_, _v_);
				}
			}
			return _os_;
		}

		@Override
		public xbean.huoyues copy() {
			return new Data(this);
		}

		@Override
		public xbean.huoyues toData() {
			return new Data(this);
		}

		public xbean.huoyues toBean() {
			return new huoyues(this, null, null);
		}

		@Override
		public xbean.huoyues toDataIf() {
			return this;
		}

		public xbean.huoyues toBeanIf() {
			return new huoyues(this, null, null);
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
		public int getHuoyuenum() { // 活跃度
			return huoyuenum;
		}

		@Override
		public int getGetnum() { // 领取记录，个位第一个，十位第二个~~
			return getnum;
		}

		@Override
		public long getHuoyuetime() { // 刷新时间，跨天用
			return huoyuetime;
		}

		@Override
		public java.util.Map<Integer, xbean.huoyue> getHuoyuemap() { // 活跃任务列表，key为选择类型
			return huoyuemap;
		}

		@Override
		public java.util.Map<Integer, xbean.huoyue> getHuoyuemapAsData() { // 活跃任务列表，key为选择类型
			return huoyuemap;
		}

		@Override
		public void setHuoyuenum(int _v_) { // 活跃度
			huoyuenum = _v_;
		}

		@Override
		public void setGetnum(int _v_) { // 领取记录，个位第一个，十位第二个~~
			getnum = _v_;
		}

		@Override
		public void setHuoyuetime(long _v_) { // 刷新时间，跨天用
			huoyuetime = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof huoyues.Data)) return false;
			huoyues.Data _o_ = (huoyues.Data) _o1_;
			if (huoyuenum != _o_.huoyuenum) return false;
			if (getnum != _o_.getnum) return false;
			if (huoyuetime != _o_.huoyuetime) return false;
			if (!huoyuemap.equals(_o_.huoyuemap)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += huoyuenum;
			_h_ += getnum;
			_h_ += huoyuetime;
			_h_ += huoyuemap.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(huoyuenum);
			_sb_.append(",");
			_sb_.append(getnum);
			_sb_.append(",");
			_sb_.append(huoyuetime);
			_sb_.append(",");
			_sb_.append(huoyuemap);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
