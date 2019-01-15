
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class LotteryItem extends xdb.XBean implements xbean.LotteryItem {
	private int id; // 遗迹宝藏ID
	private int isget; // 是否领取
	private int viewnum; // 显示位置
	private int superid; // 激活的特殊事件

	LotteryItem(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public LotteryItem() {
		this(0, null, null);
	}

	public LotteryItem(LotteryItem _o_) {
		this(_o_, null, null);
	}

	LotteryItem(xbean.LotteryItem _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof LotteryItem) assign((LotteryItem)_o1_);
		else if (_o1_ instanceof LotteryItem.Data) assign((LotteryItem.Data)_o1_);
		else if (_o1_ instanceof LotteryItem.Const) assign(((LotteryItem.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(LotteryItem _o_) {
		_o_._xdb_verify_unsafe_();
		id = _o_.id;
		isget = _o_.isget;
		viewnum = _o_.viewnum;
		superid = _o_.superid;
	}

	private void assign(LotteryItem.Data _o_) {
		id = _o_.id;
		isget = _o_.isget;
		viewnum = _o_.viewnum;
		superid = _o_.superid;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(id);
		_os_.marshal(isget);
		_os_.marshal(viewnum);
		_os_.marshal(superid);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		id = _os_.unmarshal_int();
		isget = _os_.unmarshal_int();
		viewnum = _os_.unmarshal_int();
		superid = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.LotteryItem copy() {
		_xdb_verify_unsafe_();
		return new LotteryItem(this);
	}

	@Override
	public xbean.LotteryItem toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LotteryItem toBean() {
		_xdb_verify_unsafe_();
		return new LotteryItem(this); // same as copy()
	}

	@Override
	public xbean.LotteryItem toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LotteryItem toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getId() { // 遗迹宝藏ID
		_xdb_verify_unsafe_();
		return id;
	}

	@Override
	public int getIsget() { // 是否领取
		_xdb_verify_unsafe_();
		return isget;
	}

	@Override
	public int getViewnum() { // 显示位置
		_xdb_verify_unsafe_();
		return viewnum;
	}

	@Override
	public int getSuperid() { // 激活的特殊事件
		_xdb_verify_unsafe_();
		return superid;
	}

	@Override
	public void setId(int _v_) { // 遗迹宝藏ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "id") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, id) {
					public void rollback() { id = _xdb_saved; }
				};}});
		id = _v_;
	}

	@Override
	public void setIsget(int _v_) { // 是否领取
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "isget") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, isget) {
					public void rollback() { isget = _xdb_saved; }
				};}});
		isget = _v_;
	}

	@Override
	public void setViewnum(int _v_) { // 显示位置
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "viewnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, viewnum) {
					public void rollback() { viewnum = _xdb_saved; }
				};}});
		viewnum = _v_;
	}

	@Override
	public void setSuperid(int _v_) { // 激活的特殊事件
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "superid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, superid) {
					public void rollback() { superid = _xdb_saved; }
				};}});
		superid = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		LotteryItem _o_ = null;
		if ( _o1_ instanceof LotteryItem ) _o_ = (LotteryItem)_o1_;
		else if ( _o1_ instanceof LotteryItem.Const ) _o_ = ((LotteryItem.Const)_o1_).nThis();
		else return false;
		if (id != _o_.id) return false;
		if (isget != _o_.isget) return false;
		if (viewnum != _o_.viewnum) return false;
		if (superid != _o_.superid) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += id;
		_h_ += isget;
		_h_ += viewnum;
		_h_ += superid;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(id);
		_sb_.append(",");
		_sb_.append(isget);
		_sb_.append(",");
		_sb_.append(viewnum);
		_sb_.append(",");
		_sb_.append(superid);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("id"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("isget"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("viewnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("superid"));
		return lb;
	}

	private class Const implements xbean.LotteryItem {
		LotteryItem nThis() {
			return LotteryItem.this;
		}

		@Override
		public xbean.LotteryItem copy() {
			return LotteryItem.this.copy();
		}

		@Override
		public xbean.LotteryItem toData() {
			return LotteryItem.this.toData();
		}

		public xbean.LotteryItem toBean() {
			return LotteryItem.this.toBean();
		}

		@Override
		public xbean.LotteryItem toDataIf() {
			return LotteryItem.this.toDataIf();
		}

		public xbean.LotteryItem toBeanIf() {
			return LotteryItem.this.toBeanIf();
		}

		@Override
		public int getId() { // 遗迹宝藏ID
			_xdb_verify_unsafe_();
			return id;
		}

		@Override
		public int getIsget() { // 是否领取
			_xdb_verify_unsafe_();
			return isget;
		}

		@Override
		public int getViewnum() { // 显示位置
			_xdb_verify_unsafe_();
			return viewnum;
		}

		@Override
		public int getSuperid() { // 激活的特殊事件
			_xdb_verify_unsafe_();
			return superid;
		}

		@Override
		public void setId(int _v_) { // 遗迹宝藏ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setIsget(int _v_) { // 是否领取
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setViewnum(int _v_) { // 显示位置
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSuperid(int _v_) { // 激活的特殊事件
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
			return LotteryItem.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return LotteryItem.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return LotteryItem.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return LotteryItem.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return LotteryItem.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return LotteryItem.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return LotteryItem.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return LotteryItem.this.hashCode();
		}

		@Override
		public String toString() {
			return LotteryItem.this.toString();
		}

	}

	public static final class Data implements xbean.LotteryItem {
		private int id; // 遗迹宝藏ID
		private int isget; // 是否领取
		private int viewnum; // 显示位置
		private int superid; // 激活的特殊事件

		public Data() {
		}

		Data(xbean.LotteryItem _o1_) {
			if (_o1_ instanceof LotteryItem) assign((LotteryItem)_o1_);
			else if (_o1_ instanceof LotteryItem.Data) assign((LotteryItem.Data)_o1_);
			else if (_o1_ instanceof LotteryItem.Const) assign(((LotteryItem.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(LotteryItem _o_) {
			id = _o_.id;
			isget = _o_.isget;
			viewnum = _o_.viewnum;
			superid = _o_.superid;
		}

		private void assign(LotteryItem.Data _o_) {
			id = _o_.id;
			isget = _o_.isget;
			viewnum = _o_.viewnum;
			superid = _o_.superid;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(id);
			_os_.marshal(isget);
			_os_.marshal(viewnum);
			_os_.marshal(superid);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			id = _os_.unmarshal_int();
			isget = _os_.unmarshal_int();
			viewnum = _os_.unmarshal_int();
			superid = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.LotteryItem copy() {
			return new Data(this);
		}

		@Override
		public xbean.LotteryItem toData() {
			return new Data(this);
		}

		public xbean.LotteryItem toBean() {
			return new LotteryItem(this, null, null);
		}

		@Override
		public xbean.LotteryItem toDataIf() {
			return this;
		}

		public xbean.LotteryItem toBeanIf() {
			return new LotteryItem(this, null, null);
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
		public int getId() { // 遗迹宝藏ID
			return id;
		}

		@Override
		public int getIsget() { // 是否领取
			return isget;
		}

		@Override
		public int getViewnum() { // 显示位置
			return viewnum;
		}

		@Override
		public int getSuperid() { // 激活的特殊事件
			return superid;
		}

		@Override
		public void setId(int _v_) { // 遗迹宝藏ID
			id = _v_;
		}

		@Override
		public void setIsget(int _v_) { // 是否领取
			isget = _v_;
		}

		@Override
		public void setViewnum(int _v_) { // 显示位置
			viewnum = _v_;
		}

		@Override
		public void setSuperid(int _v_) { // 激活的特殊事件
			superid = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof LotteryItem.Data)) return false;
			LotteryItem.Data _o_ = (LotteryItem.Data) _o1_;
			if (id != _o_.id) return false;
			if (isget != _o_.isget) return false;
			if (viewnum != _o_.viewnum) return false;
			if (superid != _o_.superid) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += id;
			_h_ += isget;
			_h_ += viewnum;
			_h_ += superid;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(id);
			_sb_.append(",");
			_sb_.append(isget);
			_sb_.append(",");
			_sb_.append(viewnum);
			_sb_.append(",");
			_sb_.append(superid);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
