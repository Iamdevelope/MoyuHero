
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CChangeSkin__ extends xio.Protocol { }

/** 换肤
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CChangeSkin extends __CChangeSkin__ {
	@Override
	protected void process() {
		final long roleid = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure() {
			protected boolean process() throws Exception {
				
				xbean.Properties xprop = xtable.Properties.get(roleid);
				if(xprop == null){
					throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
				}
				HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
				boolean result = herocol.changeskinEntry(herokey, skinid);
				SChangeSkin snd = new SChangeSkin();
				if(result)
					snd.result = SChangeSkin.END_OK;
				else
				{
					snd.result = SChangeSkin.END_ERROR;
				}
				xdb.Procedure.psend(roleid, snd);
				
				return result;
			}
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787769;

	public int getType() {
		return 787769;
	}

	public int herokey;
	public int skinid;

	public CChangeSkin() {
	}

	public CChangeSkin(int _herokey_, int _skinid_) {
		this.herokey = _herokey_;
		this.skinid = _skinid_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(herokey);
		_os_.marshal(skinid);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		herokey = _os_.unmarshal_int();
		skinid = _os_.unmarshal_int();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CChangeSkin) {
			CChangeSkin _o_ = (CChangeSkin)_o1_;
			if (herokey != _o_.herokey) return false;
			if (skinid != _o_.skinid) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += herokey;
		_h_ += skinid;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(herokey).append(",");
		_sb_.append(skinid).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CChangeSkin _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		_c_ = herokey - _o_.herokey;
		if (0 != _c_) return _c_;
		_c_ = skinid - _o_.skinid;
		if (0 != _c_) return _c_;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

