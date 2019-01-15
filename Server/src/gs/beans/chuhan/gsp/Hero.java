
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

/** 英雄信息 by yanglk
*/
public class Hero implements Marshal {
	public int key; // 英雄唯一ID
	public int heroid; // 英雄ID
	public int heroviewid; // 英雄显示ID（不同星级的同种英雄ID相同）
	public int heroexp; // 英雄本级经验
	public int herolevel; // 英雄等级
	public int heroallexp; // 英雄总经验
	public int qianghualevel; // 强化等级
	public int weapon; // 武器
	public int barde; // 铠甲
	public int ornament; // 饰品
	public int qhadd; // 强化加成
	public int peiyang1; // 培养1编号（默认为0）
	public int peiyang2; // 培养2编号（默认为0）
	public int peiyang3; // 培养3编号（默认为0）
	public int peiyang4; // 培养4编号（默认为0）
	public int skill1; // 技能1编号（未开通为0）
	public int skill2; // 技能2编号（未开通为0）
	public int skill3; // 技能3编号（未开通为0）
	public java.util.HashMap<Integer,Integer> items; // 符文装配
	public int herojinjiestar; // 进阶星级
	public int herojinjiesmall; // 进阶阶级
	public int heropinji; // 品质（升品换英雄配表ID）
	public java.lang.String heroskill; // 技能（:分割，根据位置记录技能ID）
	public java.lang.String heromishu; // 秘术（:一级分割，|二级分割，根据位置记录秘术等级和秘术经验）
	public java.lang.String heropeiyang; // 培养（:分割，根据位置记录培养等级）
	public java.lang.String heroequip; // 装备（:一级分割，|二级分割，根据位置记录装备ID和强化等级）

	public Hero() {
		items = new java.util.HashMap<Integer,Integer>();
		heroskill = "";
		heromishu = "";
		heropeiyang = "";
		heroequip = "";
	}

	public Hero(int _key_, int _heroid_, int _heroviewid_, int _heroexp_, int _herolevel_, int _heroallexp_, int _qianghualevel_, int _weapon_, int _barde_, int _ornament_, int _qhadd_, int _peiyang1_, int _peiyang2_, int _peiyang3_, int _peiyang4_, int _skill1_, int _skill2_, int _skill3_, java.util.HashMap<Integer,Integer> _items_, int _herojinjiestar_, int _herojinjiesmall_, int _heropinji_, java.lang.String _heroskill_, java.lang.String _heromishu_, java.lang.String _heropeiyang_, java.lang.String _heroequip_) {
		this.key = _key_;
		this.heroid = _heroid_;
		this.heroviewid = _heroviewid_;
		this.heroexp = _heroexp_;
		this.herolevel = _herolevel_;
		this.heroallexp = _heroallexp_;
		this.qianghualevel = _qianghualevel_;
		this.weapon = _weapon_;
		this.barde = _barde_;
		this.ornament = _ornament_;
		this.qhadd = _qhadd_;
		this.peiyang1 = _peiyang1_;
		this.peiyang2 = _peiyang2_;
		this.peiyang3 = _peiyang3_;
		this.peiyang4 = _peiyang4_;
		this.skill1 = _skill1_;
		this.skill2 = _skill2_;
		this.skill3 = _skill3_;
		this.items = _items_;
		this.herojinjiestar = _herojinjiestar_;
		this.herojinjiesmall = _herojinjiesmall_;
		this.heropinji = _heropinji_;
		this.heroskill = _heroskill_;
		this.heromishu = _heromishu_;
		this.heropeiyang = _heropeiyang_;
		this.heroequip = _heroequip_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(key);
		_os_.marshal(heroid);
		_os_.marshal(heroviewid);
		_os_.marshal(heroexp);
		_os_.marshal(herolevel);
		_os_.marshal(heroallexp);
		_os_.marshal(qianghualevel);
		_os_.marshal(weapon);
		_os_.marshal(barde);
		_os_.marshal(ornament);
		_os_.marshal(qhadd);
		_os_.marshal(peiyang1);
		_os_.marshal(peiyang2);
		_os_.marshal(peiyang3);
		_os_.marshal(peiyang4);
		_os_.marshal(skill1);
		_os_.marshal(skill2);
		_os_.marshal(skill3);
		_os_.compact_uint32(items.size());
		for (java.util.Map.Entry<Integer, Integer> _e_ : items.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(herojinjiestar);
		_os_.marshal(herojinjiesmall);
		_os_.marshal(heropinji);
		_os_.marshal(heroskill, "UTF-16LE");
		_os_.marshal(heromishu, "UTF-16LE");
		_os_.marshal(heropeiyang, "UTF-16LE");
		_os_.marshal(heroequip, "UTF-16LE");
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		key = _os_.unmarshal_int();
		heroid = _os_.unmarshal_int();
		heroviewid = _os_.unmarshal_int();
		heroexp = _os_.unmarshal_int();
		herolevel = _os_.unmarshal_int();
		heroallexp = _os_.unmarshal_int();
		qianghualevel = _os_.unmarshal_int();
		weapon = _os_.unmarshal_int();
		barde = _os_.unmarshal_int();
		ornament = _os_.unmarshal_int();
		qhadd = _os_.unmarshal_int();
		peiyang1 = _os_.unmarshal_int();
		peiyang2 = _os_.unmarshal_int();
		peiyang3 = _os_.unmarshal_int();
		peiyang4 = _os_.unmarshal_int();
		skill1 = _os_.unmarshal_int();
		skill2 = _os_.unmarshal_int();
		skill3 = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			int _v_;
			_v_ = _os_.unmarshal_int();
			items.put(_k_, _v_);
		}
		herojinjiestar = _os_.unmarshal_int();
		herojinjiesmall = _os_.unmarshal_int();
		heropinji = _os_.unmarshal_int();
		heroskill = _os_.unmarshal_String("UTF-16LE");
		heromishu = _os_.unmarshal_String("UTF-16LE");
		heropeiyang = _os_.unmarshal_String("UTF-16LE");
		heroequip = _os_.unmarshal_String("UTF-16LE");
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof Hero) {
			Hero _o_ = (Hero)_o1_;
			if (key != _o_.key) return false;
			if (heroid != _o_.heroid) return false;
			if (heroviewid != _o_.heroviewid) return false;
			if (heroexp != _o_.heroexp) return false;
			if (herolevel != _o_.herolevel) return false;
			if (heroallexp != _o_.heroallexp) return false;
			if (qianghualevel != _o_.qianghualevel) return false;
			if (weapon != _o_.weapon) return false;
			if (barde != _o_.barde) return false;
			if (ornament != _o_.ornament) return false;
			if (qhadd != _o_.qhadd) return false;
			if (peiyang1 != _o_.peiyang1) return false;
			if (peiyang2 != _o_.peiyang2) return false;
			if (peiyang3 != _o_.peiyang3) return false;
			if (peiyang4 != _o_.peiyang4) return false;
			if (skill1 != _o_.skill1) return false;
			if (skill2 != _o_.skill2) return false;
			if (skill3 != _o_.skill3) return false;
			if (!items.equals(_o_.items)) return false;
			if (herojinjiestar != _o_.herojinjiestar) return false;
			if (herojinjiesmall != _o_.herojinjiesmall) return false;
			if (heropinji != _o_.heropinji) return false;
			if (!heroskill.equals(_o_.heroskill)) return false;
			if (!heromishu.equals(_o_.heromishu)) return false;
			if (!heropeiyang.equals(_o_.heropeiyang)) return false;
			if (!heroequip.equals(_o_.heroequip)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += key;
		_h_ += heroid;
		_h_ += heroviewid;
		_h_ += heroexp;
		_h_ += herolevel;
		_h_ += heroallexp;
		_h_ += qianghualevel;
		_h_ += weapon;
		_h_ += barde;
		_h_ += ornament;
		_h_ += qhadd;
		_h_ += peiyang1;
		_h_ += peiyang2;
		_h_ += peiyang3;
		_h_ += peiyang4;
		_h_ += skill1;
		_h_ += skill2;
		_h_ += skill3;
		_h_ += items.hashCode();
		_h_ += herojinjiestar;
		_h_ += herojinjiesmall;
		_h_ += heropinji;
		_h_ += heroskill.hashCode();
		_h_ += heromishu.hashCode();
		_h_ += heropeiyang.hashCode();
		_h_ += heroequip.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(key).append(",");
		_sb_.append(heroid).append(",");
		_sb_.append(heroviewid).append(",");
		_sb_.append(heroexp).append(",");
		_sb_.append(herolevel).append(",");
		_sb_.append(heroallexp).append(",");
		_sb_.append(qianghualevel).append(",");
		_sb_.append(weapon).append(",");
		_sb_.append(barde).append(",");
		_sb_.append(ornament).append(",");
		_sb_.append(qhadd).append(",");
		_sb_.append(peiyang1).append(",");
		_sb_.append(peiyang2).append(",");
		_sb_.append(peiyang3).append(",");
		_sb_.append(peiyang4).append(",");
		_sb_.append(skill1).append(",");
		_sb_.append(skill2).append(",");
		_sb_.append(skill3).append(",");
		_sb_.append(items).append(",");
		_sb_.append(herojinjiestar).append(",");
		_sb_.append(herojinjiesmall).append(",");
		_sb_.append(heropinji).append(",");
		_sb_.append("T").append(heroskill.length()).append(",");
		_sb_.append("T").append(heromishu.length()).append(",");
		_sb_.append("T").append(heropeiyang.length()).append(",");
		_sb_.append("T").append(heroequip.length()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

