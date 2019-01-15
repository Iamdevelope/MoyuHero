
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class GAMEACTIVITY implements Marshal , Comparable<GAMEACTIVITY>{
	public final static int CZ_FIRST_HISTORY = 1;
	public final static int CZ_FIRST_HISTORY2 = 2;
	public final static int CZ_FIRST_INTIME = 3;
	public final static int CZ_FIRST_INTIME2 = 4;
	public final static int CZ_ALL = 5;
	public final static int CZ_EVERYDAY = 6;
	public final static int CZ_GUDING = 7;
	public final static int XFYB_ALL = 8;
	public final static int XFTI_ALL = 9;
	public final static int XFYB_EVERYDAY = 10;
	public final static int XFTI_EVERYDAY = 11;
	public final static int XFYB_GUDING = 12;
	public final static int XFTI_GUDING = 13;
	public final static int DL_EVERYDAY = 14;
	public final static int DL_INDAY = 15;
	public final static int DL_INTIME = 16;
	public final static int KILL_MONSTER = 17;
	public final static int PASS_BATTLE = 18;
	public final static int ALL_EXPBUFF = 19;
	public final static int ALL_GOLDBUFF = 20;
	public final static int ALL_HERODROP = 21;
	public final static int ALL_ITEMDROP = 22;
	public final static int ALL_TESHUBATTLE = 23;
	public final static int ALL_BOSSBATTLE = 24;
	public final static int ALL_INBATTLE = 25;
	public final static int LEVEL_BEGIN = 26;
	public final static int LEVEL_UP = 27;
	public final static int RONGLIAN_STAR = 28;
	public final static int RONGLIAN_ALL = 29;
	public final static int RONGLIAN_ID = 30;
	public final static int RONGLIAN_NUM = 31;
	public final static int GET_ITEM_STAR = 32;
	public final static int GET_ITEM_ALL = 33;
	public final static int GET_ITEM_ID = 34;
	public final static int JD_ITEM_STAR = 35;
	public final static int JD_ITEM_ALL = 36;
	public final static int JD_ITEM_ID = 37;
	public final static int QH_ITEM_STAR = 38;
	public final static int QH_ITEM_ALL = 39;
	public final static int QH_ITEM_ID = 40;
	public final static int ZM_HERO_ALL = 41;
	public final static int ZM_HERO_STAR = 42;
	public final static int ZM_HERO_ZHENYING = 43;
	public final static int ZM_HERO_ID = 44;
	public final static int GET_HERO_ALL = 45;
	public final static int GET_HERO_STAR = 46;
	public final static int GET_HERO_ZHENYING = 47;
	public final static int GET_HERO_ID = 48;
	public final static int HERO_LEVEL = 49;
	public final static int HERO_LEVEL_STAR = 50;
	public final static int HERO_LEVEL_ZHENYING = 51;
	public final static int HERO_LEVEL_ID = 52;
	public final static int ZM_ITEM_ALL = 53;
	public final static int SKIN_OFF = 54;
	public final static int ZM_HERO_OFF = 55;


	public GAMEACTIVITY() {
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof GAMEACTIVITY) {
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

	public int compareTo(GAMEACTIVITY _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

