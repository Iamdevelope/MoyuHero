
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class MailItem extends xdb.XBean implements xbean.MailItem {
	private int objectid; // 物品ID
	private int dropnum; // 数量
	private int dropparameter1; // 附加条件1
	private int dropparameter2; // 附加条件2

	MailItem(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public MailItem() {
		this(0, null, null);
	}

	public MailItem(MailItem _o_) {
		this(_o_, null, null);
	}

	MailItem(xbean.MailItem _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof MailItem) assign((MailItem)_o1_);
		else if (_o1_ instanceof MailItem.Data) assign((MailItem.Data)_o1_);
		else if (_o1_ instanceof MailItem.Const) assign(((MailItem.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(MailItem _o_) {
		_o_._xdb_verify_unsafe_();
		objectid = _o_.objectid;
		dropnum = _o_.dropnum;
		dropparameter1 = _o_.dropparameter1;
		dropparameter2 = _o_.dropparameter2;
	}

	private void assign(MailItem.Data _o_) {
		objectid = _o_.objectid;
		dropnum = _o_.dropnum;
		dropparameter1 = _o_.dropparameter1;
		dropparameter2 = _o_.dropparameter2;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(objectid);
		_os_.marshal(dropnum);
		_os_.marshal(dropparameter1);
		_os_.marshal(dropparameter2);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		objectid = _os_.unmarshal_int();
		dropnum = _os_.unmarshal_int();
		dropparameter1 = _os_.unmarshal_int();
		dropparameter2 = _os_.unmarshal_int();
		return _os_;
	}

	@Override
	public xbean.MailItem copy() {
		_xdb_verify_unsafe_();
		return new MailItem(this);
	}

	@Override
	public xbean.MailItem toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.MailItem toBean() {
		_xdb_verify_unsafe_();
		return new MailItem(this); // same as copy()
	}

	@Override
	public xbean.MailItem toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.MailItem toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public int getObjectid() { // 物品ID
		_xdb_verify_unsafe_();
		return objectid;
	}

	@Override
	public int getDropnum() { // 数量
		_xdb_verify_unsafe_();
		return dropnum;
	}

	@Override
	public int getDropparameter1() { // 附加条件1
		_xdb_verify_unsafe_();
		return dropparameter1;
	}

	@Override
	public int getDropparameter2() { // 附加条件2
		_xdb_verify_unsafe_();
		return dropparameter2;
	}

	@Override
	public void setObjectid(int _v_) { // 物品ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "objectid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, objectid) {
					public void rollback() { objectid = _xdb_saved; }
				};}});
		objectid = _v_;
	}

	@Override
	public void setDropnum(int _v_) { // 数量
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "dropnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, dropnum) {
					public void rollback() { dropnum = _xdb_saved; }
				};}});
		dropnum = _v_;
	}

	@Override
	public void setDropparameter1(int _v_) { // 附加条件1
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "dropparameter1") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, dropparameter1) {
					public void rollback() { dropparameter1 = _xdb_saved; }
				};}});
		dropparameter1 = _v_;
	}

	@Override
	public void setDropparameter2(int _v_) { // 附加条件2
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "dropparameter2") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, dropparameter2) {
					public void rollback() { dropparameter2 = _xdb_saved; }
				};}});
		dropparameter2 = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		MailItem _o_ = null;
		if ( _o1_ instanceof MailItem ) _o_ = (MailItem)_o1_;
		else if ( _o1_ instanceof MailItem.Const ) _o_ = ((MailItem.Const)_o1_).nThis();
		else return false;
		if (objectid != _o_.objectid) return false;
		if (dropnum != _o_.dropnum) return false;
		if (dropparameter1 != _o_.dropparameter1) return false;
		if (dropparameter2 != _o_.dropparameter2) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += objectid;
		_h_ += dropnum;
		_h_ += dropparameter1;
		_h_ += dropparameter2;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(objectid);
		_sb_.append(",");
		_sb_.append(dropnum);
		_sb_.append(",");
		_sb_.append(dropparameter1);
		_sb_.append(",");
		_sb_.append(dropparameter2);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("objectid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("dropnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("dropparameter1"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("dropparameter2"));
		return lb;
	}

	private class Const implements xbean.MailItem {
		MailItem nThis() {
			return MailItem.this;
		}

		@Override
		public xbean.MailItem copy() {
			return MailItem.this.copy();
		}

		@Override
		public xbean.MailItem toData() {
			return MailItem.this.toData();
		}

		public xbean.MailItem toBean() {
			return MailItem.this.toBean();
		}

		@Override
		public xbean.MailItem toDataIf() {
			return MailItem.this.toDataIf();
		}

		public xbean.MailItem toBeanIf() {
			return MailItem.this.toBeanIf();
		}

		@Override
		public int getObjectid() { // 物品ID
			_xdb_verify_unsafe_();
			return objectid;
		}

		@Override
		public int getDropnum() { // 数量
			_xdb_verify_unsafe_();
			return dropnum;
		}

		@Override
		public int getDropparameter1() { // 附加条件1
			_xdb_verify_unsafe_();
			return dropparameter1;
		}

		@Override
		public int getDropparameter2() { // 附加条件2
			_xdb_verify_unsafe_();
			return dropparameter2;
		}

		@Override
		public void setObjectid(int _v_) { // 物品ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDropnum(int _v_) { // 数量
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDropparameter1(int _v_) { // 附加条件1
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDropparameter2(int _v_) { // 附加条件2
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
			return MailItem.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return MailItem.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return MailItem.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return MailItem.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return MailItem.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return MailItem.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return MailItem.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return MailItem.this.hashCode();
		}

		@Override
		public String toString() {
			return MailItem.this.toString();
		}

	}

	public static final class Data implements xbean.MailItem {
		private int objectid; // 物品ID
		private int dropnum; // 数量
		private int dropparameter1; // 附加条件1
		private int dropparameter2; // 附加条件2

		public Data() {
		}

		Data(xbean.MailItem _o1_) {
			if (_o1_ instanceof MailItem) assign((MailItem)_o1_);
			else if (_o1_ instanceof MailItem.Data) assign((MailItem.Data)_o1_);
			else if (_o1_ instanceof MailItem.Const) assign(((MailItem.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(MailItem _o_) {
			objectid = _o_.objectid;
			dropnum = _o_.dropnum;
			dropparameter1 = _o_.dropparameter1;
			dropparameter2 = _o_.dropparameter2;
		}

		private void assign(MailItem.Data _o_) {
			objectid = _o_.objectid;
			dropnum = _o_.dropnum;
			dropparameter1 = _o_.dropparameter1;
			dropparameter2 = _o_.dropparameter2;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(objectid);
			_os_.marshal(dropnum);
			_os_.marshal(dropparameter1);
			_os_.marshal(dropparameter2);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			objectid = _os_.unmarshal_int();
			dropnum = _os_.unmarshal_int();
			dropparameter1 = _os_.unmarshal_int();
			dropparameter2 = _os_.unmarshal_int();
			return _os_;
		}

		@Override
		public xbean.MailItem copy() {
			return new Data(this);
		}

		@Override
		public xbean.MailItem toData() {
			return new Data(this);
		}

		public xbean.MailItem toBean() {
			return new MailItem(this, null, null);
		}

		@Override
		public xbean.MailItem toDataIf() {
			return this;
		}

		public xbean.MailItem toBeanIf() {
			return new MailItem(this, null, null);
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
		public int getObjectid() { // 物品ID
			return objectid;
		}

		@Override
		public int getDropnum() { // 数量
			return dropnum;
		}

		@Override
		public int getDropparameter1() { // 附加条件1
			return dropparameter1;
		}

		@Override
		public int getDropparameter2() { // 附加条件2
			return dropparameter2;
		}

		@Override
		public void setObjectid(int _v_) { // 物品ID
			objectid = _v_;
		}

		@Override
		public void setDropnum(int _v_) { // 数量
			dropnum = _v_;
		}

		@Override
		public void setDropparameter1(int _v_) { // 附加条件1
			dropparameter1 = _v_;
		}

		@Override
		public void setDropparameter2(int _v_) { // 附加条件2
			dropparameter2 = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof MailItem.Data)) return false;
			MailItem.Data _o_ = (MailItem.Data) _o1_;
			if (objectid != _o_.objectid) return false;
			if (dropnum != _o_.dropnum) return false;
			if (dropparameter1 != _o_.dropparameter1) return false;
			if (dropparameter2 != _o_.dropparameter2) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += objectid;
			_h_ += dropnum;
			_h_ += dropparameter1;
			_h_ += dropparameter2;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(objectid);
			_sb_.append(",");
			_sb_.append(dropnum);
			_sb_.append(",");
			_sb_.append(dropparameter1);
			_sb_.append(",");
			_sb_.append(dropparameter2);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
