
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class LotteryItemlayer extends xdb.XBean implements xbean.LotteryItemlayer {
	private java.util.LinkedList<xbean.LotteryItem> lotteryitemlist; // 遗迹宝藏每层list

	LotteryItemlayer(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		lotteryitemlist = new java.util.LinkedList<xbean.LotteryItem>();
	}

	public LotteryItemlayer() {
		this(0, null, null);
	}

	public LotteryItemlayer(LotteryItemlayer _o_) {
		this(_o_, null, null);
	}

	LotteryItemlayer(xbean.LotteryItemlayer _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof LotteryItemlayer) assign((LotteryItemlayer)_o1_);
		else if (_o1_ instanceof LotteryItemlayer.Data) assign((LotteryItemlayer.Data)_o1_);
		else if (_o1_ instanceof LotteryItemlayer.Const) assign(((LotteryItemlayer.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(LotteryItemlayer _o_) {
		_o_._xdb_verify_unsafe_();
		lotteryitemlist = new java.util.LinkedList<xbean.LotteryItem>();
		for (xbean.LotteryItem _v_ : _o_.lotteryitemlist)
			lotteryitemlist.add(new LotteryItem(_v_, this, "lotteryitemlist"));
	}

	private void assign(LotteryItemlayer.Data _o_) {
		lotteryitemlist = new java.util.LinkedList<xbean.LotteryItem>();
		for (xbean.LotteryItem _v_ : _o_.lotteryitemlist)
			lotteryitemlist.add(new LotteryItem(_v_, this, "lotteryitemlist"));
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.compact_uint32(lotteryitemlist.size());
		for (xbean.LotteryItem _v_ : lotteryitemlist) {
			_v_.marshal(_os_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			xbean.LotteryItem _v_ = new LotteryItem(0, this, "lotteryitemlist");
			_v_.unmarshal(_os_);
			lotteryitemlist.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.LotteryItemlayer copy() {
		_xdb_verify_unsafe_();
		return new LotteryItemlayer(this);
	}

	@Override
	public xbean.LotteryItemlayer toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LotteryItemlayer toBean() {
		_xdb_verify_unsafe_();
		return new LotteryItemlayer(this); // same as copy()
	}

	@Override
	public xbean.LotteryItemlayer toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.LotteryItemlayer toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public java.util.List<xbean.LotteryItem> getLotteryitemlist() { // 遗迹宝藏每层list
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "lotteryitemlist"), lotteryitemlist);
	}

	public java.util.List<xbean.LotteryItem> getLotteryitemlistAsData() { // 遗迹宝藏每层list
		_xdb_verify_unsafe_();
		java.util.List<xbean.LotteryItem> lotteryitemlist;
		LotteryItemlayer _o_ = this;
		lotteryitemlist = new java.util.LinkedList<xbean.LotteryItem>();
		for (xbean.LotteryItem _v_ : _o_.lotteryitemlist)
			lotteryitemlist.add(new LotteryItem.Data(_v_));
		return lotteryitemlist;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		LotteryItemlayer _o_ = null;
		if ( _o1_ instanceof LotteryItemlayer ) _o_ = (LotteryItemlayer)_o1_;
		else if ( _o1_ instanceof LotteryItemlayer.Const ) _o_ = ((LotteryItemlayer.Const)_o1_).nThis();
		else return false;
		if (!lotteryitemlist.equals(_o_.lotteryitemlist)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += lotteryitemlist.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(lotteryitemlist);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("lotteryitemlist"));
		return lb;
	}

	private class Const implements xbean.LotteryItemlayer {
		LotteryItemlayer nThis() {
			return LotteryItemlayer.this;
		}

		@Override
		public xbean.LotteryItemlayer copy() {
			return LotteryItemlayer.this.copy();
		}

		@Override
		public xbean.LotteryItemlayer toData() {
			return LotteryItemlayer.this.toData();
		}

		public xbean.LotteryItemlayer toBean() {
			return LotteryItemlayer.this.toBean();
		}

		@Override
		public xbean.LotteryItemlayer toDataIf() {
			return LotteryItemlayer.this.toDataIf();
		}

		public xbean.LotteryItemlayer toBeanIf() {
			return LotteryItemlayer.this.toBeanIf();
		}

		@Override
		public java.util.List<xbean.LotteryItem> getLotteryitemlist() { // 遗迹宝藏每层list
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(lotteryitemlist);
		}

		public java.util.List<xbean.LotteryItem> getLotteryitemlistAsData() { // 遗迹宝藏每层list
			_xdb_verify_unsafe_();
			java.util.List<xbean.LotteryItem> lotteryitemlist;
			LotteryItemlayer _o_ = LotteryItemlayer.this;
		lotteryitemlist = new java.util.LinkedList<xbean.LotteryItem>();
		for (xbean.LotteryItem _v_ : _o_.lotteryitemlist)
			lotteryitemlist.add(new LotteryItem.Data(_v_));
			return lotteryitemlist;
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
			return LotteryItemlayer.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return LotteryItemlayer.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return LotteryItemlayer.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return LotteryItemlayer.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return LotteryItemlayer.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return LotteryItemlayer.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return LotteryItemlayer.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return LotteryItemlayer.this.hashCode();
		}

		@Override
		public String toString() {
			return LotteryItemlayer.this.toString();
		}

	}

	public static final class Data implements xbean.LotteryItemlayer {
		private java.util.LinkedList<xbean.LotteryItem> lotteryitemlist; // 遗迹宝藏每层list

		public Data() {
			lotteryitemlist = new java.util.LinkedList<xbean.LotteryItem>();
		}

		Data(xbean.LotteryItemlayer _o1_) {
			if (_o1_ instanceof LotteryItemlayer) assign((LotteryItemlayer)_o1_);
			else if (_o1_ instanceof LotteryItemlayer.Data) assign((LotteryItemlayer.Data)_o1_);
			else if (_o1_ instanceof LotteryItemlayer.Const) assign(((LotteryItemlayer.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(LotteryItemlayer _o_) {
			lotteryitemlist = new java.util.LinkedList<xbean.LotteryItem>();
			for (xbean.LotteryItem _v_ : _o_.lotteryitemlist)
				lotteryitemlist.add(new LotteryItem.Data(_v_));
		}

		private void assign(LotteryItemlayer.Data _o_) {
			lotteryitemlist = new java.util.LinkedList<xbean.LotteryItem>();
			for (xbean.LotteryItem _v_ : _o_.lotteryitemlist)
				lotteryitemlist.add(new LotteryItem.Data(_v_));
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.compact_uint32(lotteryitemlist.size());
			for (xbean.LotteryItem _v_ : lotteryitemlist) {
				_v_.marshal(_os_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				xbean.LotteryItem _v_ = xbean.Pod.newLotteryItemData();
				_v_.unmarshal(_os_);
				lotteryitemlist.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.LotteryItemlayer copy() {
			return new Data(this);
		}

		@Override
		public xbean.LotteryItemlayer toData() {
			return new Data(this);
		}

		public xbean.LotteryItemlayer toBean() {
			return new LotteryItemlayer(this, null, null);
		}

		@Override
		public xbean.LotteryItemlayer toDataIf() {
			return this;
		}

		public xbean.LotteryItemlayer toBeanIf() {
			return new LotteryItemlayer(this, null, null);
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
		public java.util.List<xbean.LotteryItem> getLotteryitemlist() { // 遗迹宝藏每层list
			return lotteryitemlist;
		}

		@Override
		public java.util.List<xbean.LotteryItem> getLotteryitemlistAsData() { // 遗迹宝藏每层list
			return lotteryitemlist;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof LotteryItemlayer.Data)) return false;
			LotteryItemlayer.Data _o_ = (LotteryItemlayer.Data) _o1_;
			if (!lotteryitemlist.equals(_o_.lotteryitemlist)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += lotteryitemlist.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(lotteryitemlist);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
