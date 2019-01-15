
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class BillData extends xdb.XBean implements xbean.BillData {
	private long billid; // 
	private int goodid; // 
	private int goodnum; // 
	private int present; // 
	private float price; // 总价格
	private long createtime; // 创建时间
	private int state; // 
	private int confirmtimes; // 向au确认订单的次数
	private String platbillid; // 平台生成的订单号

	BillData(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		platbillid = "";
	}

	public BillData() {
		this(0, null, null);
	}

	public BillData(BillData _o_) {
		this(_o_, null, null);
	}

	BillData(xbean.BillData _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof BillData) assign((BillData)_o1_);
		else if (_o1_ instanceof BillData.Data) assign((BillData.Data)_o1_);
		else if (_o1_ instanceof BillData.Const) assign(((BillData.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(BillData _o_) {
		_o_._xdb_verify_unsafe_();
		billid = _o_.billid;
		goodid = _o_.goodid;
		goodnum = _o_.goodnum;
		present = _o_.present;
		price = _o_.price;
		createtime = _o_.createtime;
		state = _o_.state;
		confirmtimes = _o_.confirmtimes;
		platbillid = _o_.platbillid;
	}

	private void assign(BillData.Data _o_) {
		billid = _o_.billid;
		goodid = _o_.goodid;
		goodnum = _o_.goodnum;
		present = _o_.present;
		price = _o_.price;
		createtime = _o_.createtime;
		state = _o_.state;
		confirmtimes = _o_.confirmtimes;
		platbillid = _o_.platbillid;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(billid);
		_os_.marshal(goodid);
		_os_.marshal(goodnum);
		_os_.marshal(present);
		_os_.marshal(price);
		_os_.marshal(createtime);
		_os_.marshal(state);
		_os_.marshal(confirmtimes);
		_os_.marshal(platbillid, xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		billid = _os_.unmarshal_long();
		goodid = _os_.unmarshal_int();
		goodnum = _os_.unmarshal_int();
		present = _os_.unmarshal_int();
		price = _os_.unmarshal_float();
		createtime = _os_.unmarshal_long();
		state = _os_.unmarshal_int();
		confirmtimes = _os_.unmarshal_int();
		platbillid = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		return _os_;
	}

	@Override
	public xbean.BillData copy() {
		_xdb_verify_unsafe_();
		return new BillData(this);
	}

	@Override
	public xbean.BillData toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BillData toBean() {
		_xdb_verify_unsafe_();
		return new BillData(this); // same as copy()
	}

	@Override
	public xbean.BillData toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BillData toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public long getBillid() { // 
		_xdb_verify_unsafe_();
		return billid;
	}

	@Override
	public int getGoodid() { // 
		_xdb_verify_unsafe_();
		return goodid;
	}

	@Override
	public int getGoodnum() { // 
		_xdb_verify_unsafe_();
		return goodnum;
	}

	@Override
	public int getPresent() { // 
		_xdb_verify_unsafe_();
		return present;
	}

	@Override
	public float getPrice() { // 总价格
		_xdb_verify_unsafe_();
		return price;
	}

	@Override
	public long getCreatetime() { // 创建时间
		_xdb_verify_unsafe_();
		return createtime;
	}

	@Override
	public int getState() { // 
		_xdb_verify_unsafe_();
		return state;
	}

	@Override
	public int getConfirmtimes() { // 向au确认订单的次数
		_xdb_verify_unsafe_();
		return confirmtimes;
	}

	@Override
	public String getPlatbillid() { // 平台生成的订单号
		_xdb_verify_unsafe_();
		return platbillid;
	}

	@Override
	public com.goldhuman.Common.Octets getPlatbillidOctets() { // 平台生成的订单号
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getPlatbillid(), xdb.Const.IO_CHARSET);
	}

	@Override
	public void setBillid(long _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "billid") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, billid) {
					public void rollback() { billid = _xdb_saved; }
				};}});
		billid = _v_;
	}

	@Override
	public void setGoodid(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "goodid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, goodid) {
					public void rollback() { goodid = _xdb_saved; }
				};}});
		goodid = _v_;
	}

	@Override
	public void setGoodnum(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "goodnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, goodnum) {
					public void rollback() { goodnum = _xdb_saved; }
				};}});
		goodnum = _v_;
	}

	@Override
	public void setPresent(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "present") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, present) {
					public void rollback() { present = _xdb_saved; }
				};}});
		present = _v_;
	}

	@Override
	public void setPrice(float _v_) { // 总价格
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "price") {
			protected xdb.Log create() {
				return new xdb.logs.LogFloat(this, price) {
					public void rollback() { price = _xdb_saved; }
				};}});
		price = _v_;
	}

	@Override
	public void setCreatetime(long _v_) { // 创建时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "createtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, createtime) {
					public void rollback() { createtime = _xdb_saved; }
				};}});
		createtime = _v_;
	}

	@Override
	public void setState(int _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "state") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, state) {
					public void rollback() { state = _xdb_saved; }
				};}});
		state = _v_;
	}

	@Override
	public void setConfirmtimes(int _v_) { // 向au确认订单的次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "confirmtimes") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, confirmtimes) {
					public void rollback() { confirmtimes = _xdb_saved; }
				};}});
		confirmtimes = _v_;
	}

	@Override
	public void setPlatbillid(String _v_) { // 平台生成的订单号
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "platbillid") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, platbillid) {
					public void rollback() { platbillid = _xdb_saved; }
				};}});
		platbillid = _v_;
	}

	@Override
	public void setPlatbillidOctets(com.goldhuman.Common.Octets _v_) { // 平台生成的订单号
		_xdb_verify_unsafe_();
		this.setPlatbillid(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		BillData _o_ = null;
		if ( _o1_ instanceof BillData ) _o_ = (BillData)_o1_;
		else if ( _o1_ instanceof BillData.Const ) _o_ = ((BillData.Const)_o1_).nThis();
		else return false;
		if (billid != _o_.billid) return false;
		if (goodid != _o_.goodid) return false;
		if (goodnum != _o_.goodnum) return false;
		if (present != _o_.present) return false;
		if (price != _o_.price) return false;
		if (createtime != _o_.createtime) return false;
		if (state != _o_.state) return false;
		if (confirmtimes != _o_.confirmtimes) return false;
		if (!platbillid.equals(_o_.platbillid)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += billid;
		_h_ += goodid;
		_h_ += goodnum;
		_h_ += present;
		_h_ += price;
		_h_ += createtime;
		_h_ += state;
		_h_ += confirmtimes;
		_h_ += platbillid.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(billid);
		_sb_.append(",");
		_sb_.append(goodid);
		_sb_.append(",");
		_sb_.append(goodnum);
		_sb_.append(",");
		_sb_.append(present);
		_sb_.append(",");
		_sb_.append(price);
		_sb_.append(",");
		_sb_.append(createtime);
		_sb_.append(",");
		_sb_.append(state);
		_sb_.append(",");
		_sb_.append(confirmtimes);
		_sb_.append(",");
		_sb_.append("'").append(platbillid).append("'");
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("billid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("goodid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("goodnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("present"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("price"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("createtime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("state"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("confirmtimes"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("platbillid"));
		return lb;
	}

	private class Const implements xbean.BillData {
		BillData nThis() {
			return BillData.this;
		}

		@Override
		public xbean.BillData copy() {
			return BillData.this.copy();
		}

		@Override
		public xbean.BillData toData() {
			return BillData.this.toData();
		}

		public xbean.BillData toBean() {
			return BillData.this.toBean();
		}

		@Override
		public xbean.BillData toDataIf() {
			return BillData.this.toDataIf();
		}

		public xbean.BillData toBeanIf() {
			return BillData.this.toBeanIf();
		}

		@Override
		public long getBillid() { // 
			_xdb_verify_unsafe_();
			return billid;
		}

		@Override
		public int getGoodid() { // 
			_xdb_verify_unsafe_();
			return goodid;
		}

		@Override
		public int getGoodnum() { // 
			_xdb_verify_unsafe_();
			return goodnum;
		}

		@Override
		public int getPresent() { // 
			_xdb_verify_unsafe_();
			return present;
		}

		@Override
		public float getPrice() { // 总价格
			_xdb_verify_unsafe_();
			return price;
		}

		@Override
		public long getCreatetime() { // 创建时间
			_xdb_verify_unsafe_();
			return createtime;
		}

		@Override
		public int getState() { // 
			_xdb_verify_unsafe_();
			return state;
		}

		@Override
		public int getConfirmtimes() { // 向au确认订单的次数
			_xdb_verify_unsafe_();
			return confirmtimes;
		}

		@Override
		public String getPlatbillid() { // 平台生成的订单号
			_xdb_verify_unsafe_();
			return platbillid;
		}

		@Override
		public com.goldhuman.Common.Octets getPlatbillidOctets() { // 平台生成的订单号
			_xdb_verify_unsafe_();
			return BillData.this.getPlatbillidOctets();
		}

		@Override
		public void setBillid(long _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGoodid(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGoodnum(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPresent(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPrice(float _v_) { // 总价格
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setCreatetime(long _v_) { // 创建时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setState(int _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setConfirmtimes(int _v_) { // 向au确认订单的次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPlatbillid(String _v_) { // 平台生成的订单号
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPlatbillidOctets(com.goldhuman.Common.Octets _v_) { // 平台生成的订单号
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
			return BillData.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return BillData.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return BillData.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return BillData.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return BillData.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return BillData.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return BillData.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return BillData.this.hashCode();
		}

		@Override
		public String toString() {
			return BillData.this.toString();
		}

	}

	public static final class Data implements xbean.BillData {
		private long billid; // 
		private int goodid; // 
		private int goodnum; // 
		private int present; // 
		private float price; // 总价格
		private long createtime; // 创建时间
		private int state; // 
		private int confirmtimes; // 向au确认订单的次数
		private String platbillid; // 平台生成的订单号

		public Data() {
			platbillid = "";
		}

		Data(xbean.BillData _o1_) {
			if (_o1_ instanceof BillData) assign((BillData)_o1_);
			else if (_o1_ instanceof BillData.Data) assign((BillData.Data)_o1_);
			else if (_o1_ instanceof BillData.Const) assign(((BillData.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(BillData _o_) {
			billid = _o_.billid;
			goodid = _o_.goodid;
			goodnum = _o_.goodnum;
			present = _o_.present;
			price = _o_.price;
			createtime = _o_.createtime;
			state = _o_.state;
			confirmtimes = _o_.confirmtimes;
			platbillid = _o_.platbillid;
		}

		private void assign(BillData.Data _o_) {
			billid = _o_.billid;
			goodid = _o_.goodid;
			goodnum = _o_.goodnum;
			present = _o_.present;
			price = _o_.price;
			createtime = _o_.createtime;
			state = _o_.state;
			confirmtimes = _o_.confirmtimes;
			platbillid = _o_.platbillid;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(billid);
			_os_.marshal(goodid);
			_os_.marshal(goodnum);
			_os_.marshal(present);
			_os_.marshal(price);
			_os_.marshal(createtime);
			_os_.marshal(state);
			_os_.marshal(confirmtimes);
			_os_.marshal(platbillid, xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			billid = _os_.unmarshal_long();
			goodid = _os_.unmarshal_int();
			goodnum = _os_.unmarshal_int();
			present = _os_.unmarshal_int();
			price = _os_.unmarshal_float();
			createtime = _os_.unmarshal_long();
			state = _os_.unmarshal_int();
			confirmtimes = _os_.unmarshal_int();
			platbillid = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			return _os_;
		}

		@Override
		public xbean.BillData copy() {
			return new Data(this);
		}

		@Override
		public xbean.BillData toData() {
			return new Data(this);
		}

		public xbean.BillData toBean() {
			return new BillData(this, null, null);
		}

		@Override
		public xbean.BillData toDataIf() {
			return this;
		}

		public xbean.BillData toBeanIf() {
			return new BillData(this, null, null);
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
		public long getBillid() { // 
			return billid;
		}

		@Override
		public int getGoodid() { // 
			return goodid;
		}

		@Override
		public int getGoodnum() { // 
			return goodnum;
		}

		@Override
		public int getPresent() { // 
			return present;
		}

		@Override
		public float getPrice() { // 总价格
			return price;
		}

		@Override
		public long getCreatetime() { // 创建时间
			return createtime;
		}

		@Override
		public int getState() { // 
			return state;
		}

		@Override
		public int getConfirmtimes() { // 向au确认订单的次数
			return confirmtimes;
		}

		@Override
		public String getPlatbillid() { // 平台生成的订单号
			return platbillid;
		}

		@Override
		public com.goldhuman.Common.Octets getPlatbillidOctets() { // 平台生成的订单号
			return com.goldhuman.Common.Octets.wrap(getPlatbillid(), xdb.Const.IO_CHARSET);
		}

		@Override
		public void setBillid(long _v_) { // 
			billid = _v_;
		}

		@Override
		public void setGoodid(int _v_) { // 
			goodid = _v_;
		}

		@Override
		public void setGoodnum(int _v_) { // 
			goodnum = _v_;
		}

		@Override
		public void setPresent(int _v_) { // 
			present = _v_;
		}

		@Override
		public void setPrice(float _v_) { // 总价格
			price = _v_;
		}

		@Override
		public void setCreatetime(long _v_) { // 创建时间
			createtime = _v_;
		}

		@Override
		public void setState(int _v_) { // 
			state = _v_;
		}

		@Override
		public void setConfirmtimes(int _v_) { // 向au确认订单的次数
			confirmtimes = _v_;
		}

		@Override
		public void setPlatbillid(String _v_) { // 平台生成的订单号
			if (null == _v_)
				throw new NullPointerException();
			platbillid = _v_;
		}

		@Override
		public void setPlatbillidOctets(com.goldhuman.Common.Octets _v_) { // 平台生成的订单号
			this.setPlatbillid(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof BillData.Data)) return false;
			BillData.Data _o_ = (BillData.Data) _o1_;
			if (billid != _o_.billid) return false;
			if (goodid != _o_.goodid) return false;
			if (goodnum != _o_.goodnum) return false;
			if (present != _o_.present) return false;
			if (price != _o_.price) return false;
			if (createtime != _o_.createtime) return false;
			if (state != _o_.state) return false;
			if (confirmtimes != _o_.confirmtimes) return false;
			if (!platbillid.equals(_o_.platbillid)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += billid;
			_h_ += goodid;
			_h_ += goodnum;
			_h_ += present;
			_h_ += price;
			_h_ += createtime;
			_h_ += state;
			_h_ += confirmtimes;
			_h_ += platbillid.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(billid);
			_sb_.append(",");
			_sb_.append(goodid);
			_sb_.append(",");
			_sb_.append(goodnum);
			_sb_.append(",");
			_sb_.append(present);
			_sb_.append(",");
			_sb_.append(price);
			_sb_.append(",");
			_sb_.append(createtime);
			_sb_.append(",");
			_sb_.append(state);
			_sb_.append(",");
			_sb_.append(confirmtimes);
			_sb_.append(",");
			_sb_.append("'").append(platbillid).append("'");
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
