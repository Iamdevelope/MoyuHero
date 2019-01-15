
package chuhan.gsp.play.endlessbattle;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CGetEndlessRank__ extends xio.Protocol { }

/** 获取极限试炼排行榜
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CGetEndlessRank extends __CGetEndlessRank__ {
	@Override
	protected void process() {
		// protocol handle
		long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				
				xbean.Properties xprop = xtable.Properties.get(roleId);
				if(xprop == null){
					throw new IllegalArgumentException("构造角色时，角色 "+roleId+" 不存在。");
				}
				SGetEndlessRank snd = new SGetEndlessRank();
				snd.rank1_50.addAll(chuhan.gsp.play.ranking.endlessRanking.getInstance().list1_50);
				snd.rank51_100.addAll(chuhan.gsp.play.ranking.endlessRanking.getInstance().list51_100);
				snd.rank101_.addAll(chuhan.gsp.play.ranking.endlessRanking.getInstance().list101_);
				xdb.Procedure.psend(roleId, snd);
				return true;
			};
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788944;

	public int getType() {
		return 788944;
	}


	public CGetEndlessRank() {
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof CGetEndlessRank) {
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(")");
		return _sb_.toString();
	}

	public int compareTo(CGetEndlessRank _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

