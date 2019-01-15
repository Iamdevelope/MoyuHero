
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 一些个别的初始数值
*/
public class DataInit implements Marshal , Comparable<DataInit>{
	public final static int ERROR_RESULT = -65535; // 错误的返回值
	public final static int ROLE_LEVEL_MAX = 99; // 角色最大等级
	public final static int HERO_LEVEL_MAX = 100; // 英雄最高等级
	public final static int EQUIP_WEAPON = 11; // 武器
	public final static int EQUIP_BARDE = 12; // 防具
	public final static int EQUIP_ORNAMENT = 13; // 饰品
	public final static int BATTLE_1_BEGIN = 10001; // 副本开局
	public final static int HERO_CLOWN_1 = 200001; // 1星小丑
	public final static int HERO_CLOWN_2 = 200002; // 2星小丑
	public final static int HERO_CLOWN_3 = 200003; // 3星小丑
	public final static int HERO_CLOWN_4 = 200004; // 4星小丑
	public final static int STAR1 = 1; // 1星
	public final static int STAR2 = 2; // 2星
	public final static int STAR3 = 3; // 3星
	public final static int STAR4 = 4; // 4星
	public final static int STAR5 = 5; // 5星
	public final static int ROLE_UP_POINT = 5; // 角色升级获得潜能数
	public final static int PET_UP_POINT = 5; // 宠物升级获得潜能数
	public final static int ROLE_UP_PHY = 5; // 每升一级增加5点体力
	public final static int ROLE_UP_ENERGY = 5; // 每升一级增加5点活力
	public final static int PET_INIT_LOY = 80; // 宠物初始忠诚度
	public final static int PET_MAX_LOY = 100; // 宠物最大忠诚度。
	public final static int PET_MAX_LIFE = 20000; // 宠物最大寿命
	public final static int FULL_PETLOY_LEVEL = 30; // 满宠物忠诚度的宠物等级上限
	public final static int ROLE_PET_LEVEL_SPACE = 5; // 人物宠物的等级差最大为5级。
	public final static int BASENUM = 1000; // 角色属性计算配置值的基数
	public final static int PET_USELEVEL_SPACE = 10; // 宠物参战时与人的最大等级差
	public final static int AUTO_UPGRADE_LEVEL = 10; // 人物自动升级的等级上限
	public final static int WILD_PET_MAXGENGU = 40;
	public final static int WILD_PET_MINGENGU = 1;
	public final static int HERO_UP_LEVEL_ADD_POINT = 4; // 宠物每升一级，给的潜能点数


	public DataInit() {
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
		if (_o1_ instanceof DataInit) {
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

	public int compareTo(DataInit _o_) {
		if (_o_ == this) return 0;
		int _c_ = 0;
		return _c_;
	}

}

