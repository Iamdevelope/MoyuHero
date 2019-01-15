
package chuhan.gsp.hero;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __CTuJianHeros__ extends xio.Protocol { }

/** 获取获得过的武将(图鉴)
*/
// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class CTuJianHeros extends __CTuJianHeros__ {
	@Override
	protected void process() {
		final long roleId = gnet.link.Onlines.getInstance().findRoleid(this);
		new xdb.Procedure(){
			protected boolean process() throws Exception {
				xbean.TuJianHeros tjheros = xtable.Tujianheros.get(roleId);
				java.util.LinkedList<chuhan.gsp.HeroTuJian> returntjlist = new java.util.LinkedList<chuhan.gsp.HeroTuJian>();
				for(java.util.Map.Entry<Integer, xbean.TuJianHero> entry : tjheros.getTujianhero().entrySet()){
					chuhan.gsp.HeroTuJian herotj = new chuhan.gsp.HeroTuJian();
					herotj.heroid = entry.getValue().getHeroid();
					herotj.flag = entry.getValue().getFlag();
					returntjlist.addFirst(herotj);
				}
				java.util.LinkedList<Integer> boxlist = new java.util.LinkedList<Integer>();
				for(java.util.Map.Entry<Integer, Integer> entry : tjheros.getTujianbox().entrySet()){
					boxlist.add(entry.getKey());
				}
				STuJianHeros sTuJianHeros = new STuJianHeros();
				sTuJianHeros.herotujian = returntjlist;
				sTuJianHeros.isnew = STuJianHeros.NOT_NEW;
				sTuJianHeros.tujianbox = boxlist;
				sTuJianHeros.tjheromaxlevel.addAll(tjheros.getTjheromaxlevel());
				gnet.link.Onlines.getInstance().send(roleId, sTuJianHeros);
				tjheros.getTjheromaxlevel().clear();
				return true;
			};
		}.submit();		
	}
	
	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 787761;

	public int getType() {
		return 787761;
	}


	public CTuJianHeros() {
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
		if (_o1_ instanceof CTuJianHeros) {
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

	public int compareTo(CTuJianHeros _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

