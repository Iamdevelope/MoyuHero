
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class BasicFightProperties extends xdb.XBean implements xbean.BasicFightProperties {
	private float hp; // 
	private float attack; // 
	private float defend; // 
	private float wisdom; // 

	BasicFightProperties(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
	}

	public BasicFightProperties() {
		this(0, null, null);
	}

	public BasicFightProperties(BasicFightProperties _o_) {
		this(_o_, null, null);
	}

	BasicFightProperties(xbean.BasicFightProperties _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof BasicFightProperties) assign((BasicFightProperties)_o1_);
		else if (_o1_ instanceof BasicFightProperties.Data) assign((BasicFightProperties.Data)_o1_);
		else if (_o1_ instanceof BasicFightProperties.Const) assign(((BasicFightProperties.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(BasicFightProperties _o_) {
		_o_._xdb_verify_unsafe_();
		hp = _o_.hp;
		attack = _o_.attack;
		defend = _o_.defend;
		wisdom = _o_.wisdom;
	}

	private void assign(BasicFightProperties.Data _o_) {
		hp = _o_.hp;
		attack = _o_.attack;
		defend = _o_.defend;
		wisdom = _o_.wisdom;
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(hp);
		_os_.marshal(attack);
		_os_.marshal(defend);
		_os_.marshal(wisdom);
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		hp = _os_.unmarshal_float();
		attack = _os_.unmarshal_float();
		defend = _os_.unmarshal_float();
		wisdom = _os_.unmarshal_float();
		return _os_;
	}

	@Override
	public xbean.BasicFightProperties copy() {
		_xdb_verify_unsafe_();
		return new BasicFightProperties(this);
	}

	@Override
	public xbean.BasicFightProperties toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BasicFightProperties toBean() {
		_xdb_verify_unsafe_();
		return new BasicFightProperties(this); // same as copy()
	}

	@Override
	public xbean.BasicFightProperties toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.BasicFightProperties toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public float getHp() { // 
		_xdb_verify_unsafe_();
		return hp;
	}

	@Override
	public float getAttack() { // 
		_xdb_verify_unsafe_();
		return attack;
	}

	@Override
	public float getDefend() { // 
		_xdb_verify_unsafe_();
		return defend;
	}

	@Override
	public float getWisdom() { // 
		_xdb_verify_unsafe_();
		return wisdom;
	}

	@Override
	public void setHp(float _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "hp") {
			protected xdb.Log create() {
				return new xdb.logs.LogFloat(this, hp) {
					public void rollback() { hp = _xdb_saved; }
				};}});
		hp = _v_;
	}

	@Override
	public void setAttack(float _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "attack") {
			protected xdb.Log create() {
				return new xdb.logs.LogFloat(this, attack) {
					public void rollback() { attack = _xdb_saved; }
				};}});
		attack = _v_;
	}

	@Override
	public void setDefend(float _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "defend") {
			protected xdb.Log create() {
				return new xdb.logs.LogFloat(this, defend) {
					public void rollback() { defend = _xdb_saved; }
				};}});
		defend = _v_;
	}

	@Override
	public void setWisdom(float _v_) { // 
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "wisdom") {
			protected xdb.Log create() {
				return new xdb.logs.LogFloat(this, wisdom) {
					public void rollback() { wisdom = _xdb_saved; }
				};}});
		wisdom = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		BasicFightProperties _o_ = null;
		if ( _o1_ instanceof BasicFightProperties ) _o_ = (BasicFightProperties)_o1_;
		else if ( _o1_ instanceof BasicFightProperties.Const ) _o_ = ((BasicFightProperties.Const)_o1_).nThis();
		else return false;
		if (hp != _o_.hp) return false;
		if (attack != _o_.attack) return false;
		if (defend != _o_.defend) return false;
		if (wisdom != _o_.wisdom) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += hp;
		_h_ += attack;
		_h_ += defend;
		_h_ += wisdom;
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(hp);
		_sb_.append(",");
		_sb_.append(attack);
		_sb_.append(",");
		_sb_.append(defend);
		_sb_.append(",");
		_sb_.append(wisdom);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("hp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("attack"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("defend"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("wisdom"));
		return lb;
	}

	private class Const implements xbean.BasicFightProperties {
		BasicFightProperties nThis() {
			return BasicFightProperties.this;
		}

		@Override
		public xbean.BasicFightProperties copy() {
			return BasicFightProperties.this.copy();
		}

		@Override
		public xbean.BasicFightProperties toData() {
			return BasicFightProperties.this.toData();
		}

		public xbean.BasicFightProperties toBean() {
			return BasicFightProperties.this.toBean();
		}

		@Override
		public xbean.BasicFightProperties toDataIf() {
			return BasicFightProperties.this.toDataIf();
		}

		public xbean.BasicFightProperties toBeanIf() {
			return BasicFightProperties.this.toBeanIf();
		}

		@Override
		public float getHp() { // 
			_xdb_verify_unsafe_();
			return hp;
		}

		@Override
		public float getAttack() { // 
			_xdb_verify_unsafe_();
			return attack;
		}

		@Override
		public float getDefend() { // 
			_xdb_verify_unsafe_();
			return defend;
		}

		@Override
		public float getWisdom() { // 
			_xdb_verify_unsafe_();
			return wisdom;
		}

		@Override
		public void setHp(float _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setAttack(float _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setDefend(float _v_) { // 
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setWisdom(float _v_) { // 
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
			return BasicFightProperties.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return BasicFightProperties.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return BasicFightProperties.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return BasicFightProperties.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return BasicFightProperties.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return BasicFightProperties.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return BasicFightProperties.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return BasicFightProperties.this.hashCode();
		}

		@Override
		public String toString() {
			return BasicFightProperties.this.toString();
		}

	}

	public static final class Data implements xbean.BasicFightProperties {
		private float hp; // 
		private float attack; // 
		private float defend; // 
		private float wisdom; // 

		public Data() {
		}

		Data(xbean.BasicFightProperties _o1_) {
			if (_o1_ instanceof BasicFightProperties) assign((BasicFightProperties)_o1_);
			else if (_o1_ instanceof BasicFightProperties.Data) assign((BasicFightProperties.Data)_o1_);
			else if (_o1_ instanceof BasicFightProperties.Const) assign(((BasicFightProperties.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(BasicFightProperties _o_) {
			hp = _o_.hp;
			attack = _o_.attack;
			defend = _o_.defend;
			wisdom = _o_.wisdom;
		}

		private void assign(BasicFightProperties.Data _o_) {
			hp = _o_.hp;
			attack = _o_.attack;
			defend = _o_.defend;
			wisdom = _o_.wisdom;
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(hp);
			_os_.marshal(attack);
			_os_.marshal(defend);
			_os_.marshal(wisdom);
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			hp = _os_.unmarshal_float();
			attack = _os_.unmarshal_float();
			defend = _os_.unmarshal_float();
			wisdom = _os_.unmarshal_float();
			return _os_;
		}

		@Override
		public xbean.BasicFightProperties copy() {
			return new Data(this);
		}

		@Override
		public xbean.BasicFightProperties toData() {
			return new Data(this);
		}

		public xbean.BasicFightProperties toBean() {
			return new BasicFightProperties(this, null, null);
		}

		@Override
		public xbean.BasicFightProperties toDataIf() {
			return this;
		}

		public xbean.BasicFightProperties toBeanIf() {
			return new BasicFightProperties(this, null, null);
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
		public float getHp() { // 
			return hp;
		}

		@Override
		public float getAttack() { // 
			return attack;
		}

		@Override
		public float getDefend() { // 
			return defend;
		}

		@Override
		public float getWisdom() { // 
			return wisdom;
		}

		@Override
		public void setHp(float _v_) { // 
			hp = _v_;
		}

		@Override
		public void setAttack(float _v_) { // 
			attack = _v_;
		}

		@Override
		public void setDefend(float _v_) { // 
			defend = _v_;
		}

		@Override
		public void setWisdom(float _v_) { // 
			wisdom = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof BasicFightProperties.Data)) return false;
			BasicFightProperties.Data _o_ = (BasicFightProperties.Data) _o1_;
			if (hp != _o_.hp) return false;
			if (attack != _o_.attack) return false;
			if (defend != _o_.defend) return false;
			if (wisdom != _o_.wisdom) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += hp;
			_h_ += attack;
			_h_ += defend;
			_h_ += wisdom;
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append(hp);
			_sb_.append(",");
			_sb_.append(attack);
			_sb_.append(",");
			_sb_.append(defend);
			_sb_.append(",");
			_sb_.append(wisdom);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
