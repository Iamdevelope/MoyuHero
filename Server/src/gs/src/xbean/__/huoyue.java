
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class huoyue extends xdb.XBean implements xbean.huoyue {
	private int huoyueid; // 活跃id
	private int num; // 发生次数
	private int numall; // 总次数
	private int huoyuetype; // 任务类型
	private int isok; // 是否完成

	huoyue(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public huoyue() {
		this(0, null, null);
	}

	public huoyue(huoyue _o_) {
		this(_o_, null, null);
	}

	huoyue(xbean.huoyue _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof huoyue) assign((huoyue)_o1_);
		else if (_o1_ instanceof huoyue.Data) assign((huoyue.Data)_o1_);
		else if (_o1_ instanceof huoyue.Const) assign(((huoyue.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(huoyue _o_) {
		_o_._xdb_verify_unsafe_();
		huoyueid = _o_.huoyueid;
		num = _o_.num;
		numall = _o_.numall;
		huoyuetype = _o_.huoyuetype;
		isok = _o_.isok;
	}

	private void assign(huoyue.Data _o_) {
		huoyueid = _o_.huoyueid;
		num = _o_.num;
		numall = _o_.numall;
		huoyuetype = _o_.huoyuetype;
		isok = _o_.isok;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(huoyueid);
		_os_.marshal(num);
		_os_.marshal(numall);
		_os_.marshal(huoyuetype);
		_os_.marshal(isok);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		huoyueid = _os_.unmarshal_int();
		num = _os_.unmarshal_int();
		numall = _os_.unmarshal_int();
		huoyuetype = _os_.unmarshal_int();
		isok = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.huoyue copy() {
		_xdb_verify_unsafe_();
		return new huoyue(this);
	}

	@Override
	public xbean.huoyue toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.huoyue toBean() {
		_xdb_verify_unsafe_();
		return new huoyue(this); // same as copy()
	}

	@Override
	public xbean.huoyue toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.huoyue toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getHuoyueid() { // 活跃id
		_xdb_verify_unsafe_();
		return huoyueid;
	}

	@Override
	public int getNum() { // 发生次数
		_xdb_verify_unsafe_();
		return num;
	}

	@Override
	public int getNumall() { // 总次数
		_xdb_verify_unsafe_();
		return numall;
	}

	@Override
	public int getHuoyuetype() { // 任务类型
		_xdb_verify_unsafe_();
		return huoyuetype;
	}

	@Override
	public int getIsok() { // 是否完成
		_xdb_verify_unsafe_();
		return isok;
	}

	@Override
	public void setHuoyueid(int _v_) { // 活跃id
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "huoyueid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, huoyueid) {
					public void rollback() { huoyueid = _xdb_saved; }
				};}});
		huoyueid = _v_;
	}

	@Override
	public void setNum(int _v_) { // 发生次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "num") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, num) {
					public void rollback() { num = _xdb_saved; }
				};}});
		num = _v_;
	}

	@Override
	public void setNumall(int _v_) { // 总次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "numall") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, numall) {
					public void rollback() { numall = _xdb_saved; }
				};}});
		numall = _v_;
	}

	@Override
	public void setHuoyuetype(int _v_) { // 任务类型
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "huoyuetype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, huoyuetype) {
					public void rollback() { huoyuetype = _xdb_saved; }
				};}});
		huoyuetype = _v_;
	}

	@Override
	public void setIsok(int _v_) { // 是否完成
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isok") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, isok) {
					public void rollback() { isok = _xdb_saved; }
				};}});
		isok = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		huoyue _o_ = null;
		if ( _o1_ instanceof huoyue ) _o_ = (huoyue)_o1_;
		else if ( _o1_ instanceof huoyue.Const ) _o_ = ((huoyue.Const)_o1_).nThis();
		else return false;
		if (huoyueid != _o_.huoyueid) return false;
		if (num != _o_.num) return false;
		if (numall != _o_.numall) return false;
		if (huoyuetype != _o_.huoyuetype) return false;
		if (isok != _o_.isok) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += huoyueid;
		_h_ += num;
		_h_ += numall;
		_h_ += huoyuetype;
		_h_ += isok;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(huoyueid);
		_sb_.append(",");
		_sb_.append(num);
		_sb_.append(",");
		_sb_.append(numall);
		_sb_.append(",");
		_sb_.append(huoyuetype);
		_sb_.append(",");
		_sb_.append(isok);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("huoyueid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("num"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("numall"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("huoyuetype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isok"));
		return lb;
	}

	private class Const implements xbean.huoyue {
		huoyue nThis() {
			return huoyue.this;
		}

		@Override
		public xbean.huoyue copy() {
			return huoyue.this.copy();
		}

		@Override
		public xbean.huoyue toData() {
			return huoyue.this.toData();
		}

		public xbean.huoyue toBean() {
			return huoyue.this.toBean();
		}

		@Override
		public xbean.huoyue toDataIf() {
			return huoyue.this.toDataIf();
		}

		public xbean.huoyue toBeanIf() {
			return huoyue.this.toBeanIf();
		}

		@Override
		public int getHuoyueid() { // 活跃id
			_xdb_verify_unsafe_();
			return huoyueid;
		}

		@Override
		public int getNum() { // 发生次数
			_xdb_verify_unsafe_();
			return num;
		}

		@Override
		public int getNumall() { // 总次数
			_xdb_verify_unsafe_();
			return numall;
		}

		@Override
		public int getHuoyuetype() { // 任务类型
			_xdb_verify_unsafe_();
			return huoyuetype;
		}

		@Override
		public int getIsok() { // 是否完成
			_xdb_verify_unsafe_();
			return isok;
		}

		@Override
		public void setHuoyueid(int _v_) { // 活跃id
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNum(int _v_) { // 发生次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setNumall(int _v_) { // 总次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHuoyuetype(int _v_) { // 任务类型
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsok(int _v_) { // 是否完成
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
			return huoyue.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return huoyue.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return huoyue.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return huoyue.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return huoyue.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return huoyue.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return huoyue.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return huoyue.this.hashCode();
		}

		@Override
		public String toString() {
			return huoyue.this.toString();
		}

	}

	public static final class Data implements xbean.huoyue {
		private int huoyueid; // 活跃id
		private int num; // 发生次数
		private int numall; // 总次数
		private int huoyuetype; // 任务类型
		private int isok; // 是否完成

		public Data() {
		}

		Data(xbean.huoyue _o1_) {
			if (_o1_ instanceof huoyue) assign((huoyue)_o1_);
			else if (_o1_ instanceof huoyue.Data) assign((huoyue.Data)_o1_);
			else if (_o1_ instanceof huoyue.Const) assign(((huoyue.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(huoyue _o_) {
			huoyueid = _o_.huoyueid;
			num = _o_.num;
			numall = _o_.numall;
			huoyuetype = _o_.huoyuetype;
			isok = _o_.isok;
		}

		private void assign(huoyue.Data _o_) {
			huoyueid = _o_.huoyueid;
			num = _o_.num;
			numall = _o_.numall;
			huoyuetype = _o_.huoyuetype;
			isok = _o_.isok;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(huoyueid);
			_os_.marshal(num);
			_os_.marshal(numall);
			_os_.marshal(huoyuetype);
			_os_.marshal(isok);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			huoyueid = _os_.unmarshal_int();
			num = _os_.unmarshal_int();
			numall = _os_.unmarshal_int();
			huoyuetype = _os_.unmarshal_int();
			isok = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.huoyue copy() {
			return new Data(this);
		}

		@Override
		public xbean.huoyue toData() {
			return new Data(this);
		}

		public xbean.huoyue toBean() {
			return new huoyue(this, null, null);
		}

		@Override
		public xbean.huoyue toDataIf() {
			return this;
		}

		public xbean.huoyue toBeanIf() {
			return new huoyue(this, null, null);
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
		public int getHuoyueid() { // 活跃id
			return huoyueid;
		}

		@Override
		public int getNum() { // 发生次数
			return num;
		}

		@Override
		public int getNumall() { // 总次数
			return numall;
		}

		@Override
		public int getHuoyuetype() { // 任务类型
			return huoyuetype;
		}

		@Override
		public int getIsok() { // 是否完成
			return isok;
		}

		@Override
		public void setHuoyueid(int _v_) { // 活跃id
			huoyueid = _v_;
		}

		@Override
		public void setNum(int _v_) { // 发生次数
			num = _v_;
		}

		@Override
		public void setNumall(int _v_) { // 总次数
			numall = _v_;
		}

		@Override
		public void setHuoyuetype(int _v_) { // 任务类型
			huoyuetype = _v_;
		}

		@Override
		public void setIsok(int _v_) { // 是否完成
			isok = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof huoyue.Data)) return false;
			huoyue.Data _o_ = (huoyue.Data) _o1_;
			if (huoyueid != _o_.huoyueid) return false;
			if (num != _o_.num) return false;
			if (numall != _o_.numall) return false;
			if (huoyuetype != _o_.huoyuetype) return false;
			if (isok != _o_.isok) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += huoyueid;
			_h_ += num;
			_h_ += numall;
			_h_ += huoyuetype;
			_h_ += isok;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(huoyueid);
			_sb_.append(",");
			_sb_.append(num);
			_sb_.append(",");
			_sb_.append(numall);
			_sb_.append(",");
			_sb_.append(huoyuetype);
			_sb_.append(",");
			_sb_.append(isok);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
