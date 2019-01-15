
package chuhan.gsp;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class RoleDetail implements Marshal {
	public long roleid; // ID
	public java.lang.String name; // 名称
	public byte isgm; // 0:no,1:yes
	public short level; // 等级
	public int exp; // 经验
	public byte viplv; // vip等级
	public int vipexp; // vip经验
	public short ti; // 体力
	public int titime; // 体力更新时间剩余
	public long money; // 游戏币
	public int yuanbao; // 元宝
	public int battlenum; // 关卡记录
	public long servertime; // 服务器时间
	public byte timezone; // 服务器时区[-12,+12]
	public java.util.LinkedList<chuhan.gsp.Hero> heroes;
	public java.util.LinkedList<chuhan.gsp.Troop> troops;
	public java.util.HashMap<Integer,chuhan.gsp.Bag> baginfo; // key是bagid,value是包裹的详细信息
	public int hammer; // 锤子
	public int freegoldtime; // 免费金币剩余时间
	public int freeybtime; // 免费元宝剩余时间
	public int goldbuynum; // 金币购买次数
	public int tibuynum; // 体力购买次数
	public int signnum7; // 连续签到ID
	public int signnum28; // 累计签到ID
	public byte mailsize; // 邮件数量
	public short buybagnum; // 购买背包次数
	public short buyherobagnum; // 扩充英雄背包次数
	public int smid; // 神秘关卡或商店ID
	public int smtime; // 神秘剩余时间
	public int smzhangjie; // 神秘所属章节
	public int shenglingzq; // 圣灵之泉
	public int ronglian; // 熔炼点
	public int huangjinxz; // 黄金勋章
	public int baijinxz; // 白金勋章
	public int qingtongxz; // 青铜勋章
	public int chitiexz; // 赤铁勋章
	public int jyjiejing; // 经验结晶
	public byte troopnum; // 默认编队号
	public java.util.LinkedList<Integer> heroskins; // 皮肤列表
	public java.util.HashMap<Integer,chuhan.gsp.Artifact> artifacts; // key是Artifacttype,value是神器的详细信息
	public java.util.HashMap<Integer,chuhan.gsp.Shopbuy> shopbuys; // key是shopid,value是Shopbuy的详细信息
	public int sweephavenum; // 今日扫荡剩余次数
	public int sweepbuyhavenum; // 今日扫荡剩余购买次数
	public int mszqgetnum; // 缪斯奏曲
	public int qiyuannum; // 累计祈愿台次数
	public int qiyuanallnum; // 祈愿回合次数，第一次为3，完成后均为5
	public int isqiyuantoday; // 个位是今日是否祈愿，十位为是否断签，0是否，1为是
	public short txti; // 探险体力
	public int txtitime; // 探险体力更新时间剩余
	public java.util.LinkedList<Integer> newyindao; // 新手引导
	public java.util.HashMap<Integer,chuhan.gsp.stage.smshopdata> smshop; // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）

	public RoleDetail() {
		name = "";
		heroes = new java.util.LinkedList<chuhan.gsp.Hero>();
		troops = new java.util.LinkedList<chuhan.gsp.Troop>();
		baginfo = new java.util.HashMap<Integer,chuhan.gsp.Bag>();
		signnum7 = 0;
		signnum28 = 0;
		heroskins = new java.util.LinkedList<Integer>();
		artifacts = new java.util.HashMap<Integer,chuhan.gsp.Artifact>();
		shopbuys = new java.util.HashMap<Integer,chuhan.gsp.Shopbuy>();
		newyindao = new java.util.LinkedList<Integer>();
		smshop = new java.util.HashMap<Integer,chuhan.gsp.stage.smshopdata>();
	}

	public RoleDetail(long _roleid_, java.lang.String _name_, byte _isgm_, short _level_, int _exp_, byte _viplv_, int _vipexp_, short _ti_, int _titime_, long _money_, int _yuanbao_, int _battlenum_, long _servertime_, byte _timezone_, java.util.LinkedList<chuhan.gsp.Hero> _heroes_, java.util.LinkedList<chuhan.gsp.Troop> _troops_, java.util.HashMap<Integer,chuhan.gsp.Bag> _baginfo_, int _hammer_, int _freegoldtime_, int _freeybtime_, int _goldbuynum_, int _tibuynum_, int _signnum7_, int _signnum28_, byte _mailsize_, short _buybagnum_, short _buyherobagnum_, int _smid_, int _smtime_, int _smzhangjie_, int _shenglingzq_, int _ronglian_, int _huangjinxz_, int _baijinxz_, int _qingtongxz_, int _chitiexz_, int _jyjiejing_, byte _troopnum_, java.util.LinkedList<Integer> _heroskins_, java.util.HashMap<Integer,chuhan.gsp.Artifact> _artifacts_, java.util.HashMap<Integer,chuhan.gsp.Shopbuy> _shopbuys_, int _sweephavenum_, int _sweepbuyhavenum_, int _mszqgetnum_, int _qiyuannum_, int _qiyuanallnum_, int _isqiyuantoday_, short _txti_, int _txtitime_, java.util.LinkedList<Integer> _newyindao_, java.util.HashMap<Integer,chuhan.gsp.stage.smshopdata> _smshop_) {
		this.roleid = _roleid_;
		this.name = _name_;
		this.isgm = _isgm_;
		this.level = _level_;
		this.exp = _exp_;
		this.viplv = _viplv_;
		this.vipexp = _vipexp_;
		this.ti = _ti_;
		this.titime = _titime_;
		this.money = _money_;
		this.yuanbao = _yuanbao_;
		this.battlenum = _battlenum_;
		this.servertime = _servertime_;
		this.timezone = _timezone_;
		this.heroes = _heroes_;
		this.troops = _troops_;
		this.baginfo = _baginfo_;
		this.hammer = _hammer_;
		this.freegoldtime = _freegoldtime_;
		this.freeybtime = _freeybtime_;
		this.goldbuynum = _goldbuynum_;
		this.tibuynum = _tibuynum_;
		this.signnum7 = _signnum7_;
		this.signnum28 = _signnum28_;
		this.mailsize = _mailsize_;
		this.buybagnum = _buybagnum_;
		this.buyherobagnum = _buyherobagnum_;
		this.smid = _smid_;
		this.smtime = _smtime_;
		this.smzhangjie = _smzhangjie_;
		this.shenglingzq = _shenglingzq_;
		this.ronglian = _ronglian_;
		this.huangjinxz = _huangjinxz_;
		this.baijinxz = _baijinxz_;
		this.qingtongxz = _qingtongxz_;
		this.chitiexz = _chitiexz_;
		this.jyjiejing = _jyjiejing_;
		this.troopnum = _troopnum_;
		this.heroskins = _heroskins_;
		this.artifacts = _artifacts_;
		this.shopbuys = _shopbuys_;
		this.sweephavenum = _sweephavenum_;
		this.sweepbuyhavenum = _sweepbuyhavenum_;
		this.mszqgetnum = _mszqgetnum_;
		this.qiyuannum = _qiyuannum_;
		this.qiyuanallnum = _qiyuanallnum_;
		this.isqiyuantoday = _isqiyuantoday_;
		this.txti = _txti_;
		this.txtitime = _txtitime_;
		this.newyindao = _newyindao_;
		this.smshop = _smshop_;
	}

	public final boolean _validator_() {
		if (roleid < 1) return false;
		if (level < 1) return false;
		if (exp < 0) return false;
		if (viplv < 0) return false;
		if (money < 0) return false;
		if (yuanbao < 0) return false;
		for (chuhan.gsp.Hero _v_ : heroes)
			if (!_v_._validator_()) return false;
		for (chuhan.gsp.Troop _v_ : troops)
			if (!_v_._validator_()) return false;
		for (java.util.Map.Entry<Integer, chuhan.gsp.Bag> _e_ : baginfo.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		for (java.util.Map.Entry<Integer, chuhan.gsp.Artifact> _e_ : artifacts.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		for (java.util.Map.Entry<Integer, chuhan.gsp.Shopbuy> _e_ : shopbuys.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.smshopdata> _e_ : smshop.entrySet()) {
			if (!_e_.getValue()._validator_()) return false;
		}
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(roleid);
		_os_.marshal(name, "UTF-16LE");
		_os_.marshal(isgm);
		_os_.marshal(level);
		_os_.marshal(exp);
		_os_.marshal(viplv);
		_os_.marshal(vipexp);
		_os_.marshal(ti);
		_os_.marshal(titime);
		_os_.marshal(money);
		_os_.marshal(yuanbao);
		_os_.marshal(battlenum);
		_os_.marshal(servertime);
		_os_.marshal(timezone);
		_os_.compact_uint32(heroes.size());
		for (chuhan.gsp.Hero _v_ : heroes) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(troops.size());
		for (chuhan.gsp.Troop _v_ : troops) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(baginfo.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.Bag> _e_ : baginfo.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(hammer);
		_os_.marshal(freegoldtime);
		_os_.marshal(freeybtime);
		_os_.marshal(goldbuynum);
		_os_.marshal(tibuynum);
		_os_.marshal(signnum7);
		_os_.marshal(signnum28);
		_os_.marshal(mailsize);
		_os_.marshal(buybagnum);
		_os_.marshal(buyherobagnum);
		_os_.marshal(smid);
		_os_.marshal(smtime);
		_os_.marshal(smzhangjie);
		_os_.marshal(shenglingzq);
		_os_.marshal(ronglian);
		_os_.marshal(huangjinxz);
		_os_.marshal(baijinxz);
		_os_.marshal(qingtongxz);
		_os_.marshal(chitiexz);
		_os_.marshal(jyjiejing);
		_os_.marshal(troopnum);
		_os_.compact_uint32(heroskins.size());
		for (Integer _v_ : heroskins) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(artifacts.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.Artifact> _e_ : artifacts.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.compact_uint32(shopbuys.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.Shopbuy> _e_ : shopbuys.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		_os_.marshal(sweephavenum);
		_os_.marshal(sweepbuyhavenum);
		_os_.marshal(mszqgetnum);
		_os_.marshal(qiyuannum);
		_os_.marshal(qiyuanallnum);
		_os_.marshal(isqiyuantoday);
		_os_.marshal(txti);
		_os_.marshal(txtitime);
		_os_.compact_uint32(newyindao.size());
		for (Integer _v_ : newyindao) {
			_os_.marshal(_v_);
		}
		_os_.compact_uint32(smshop.size());
		for (java.util.Map.Entry<Integer, chuhan.gsp.stage.smshopdata> _e_ : smshop.entrySet()) {
			_os_.marshal(_e_.getKey());
			_os_.marshal(_e_.getValue());
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		roleid = _os_.unmarshal_long();
		name = _os_.unmarshal_String("UTF-16LE");
		isgm = _os_.unmarshal_byte();
		level = _os_.unmarshal_short();
		exp = _os_.unmarshal_int();
		viplv = _os_.unmarshal_byte();
		vipexp = _os_.unmarshal_int();
		ti = _os_.unmarshal_short();
		titime = _os_.unmarshal_int();
		money = _os_.unmarshal_long();
		yuanbao = _os_.unmarshal_int();
		battlenum = _os_.unmarshal_int();
		servertime = _os_.unmarshal_long();
		timezone = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.Hero _v_ = new chuhan.gsp.Hero();
			_v_.unmarshal(_os_);
			heroes.add(_v_);
		}
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			chuhan.gsp.Troop _v_ = new chuhan.gsp.Troop();
			_v_.unmarshal(_os_);
			troops.add(_v_);
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.Bag _v_ = new chuhan.gsp.Bag();
			_v_.unmarshal(_os_);
			baginfo.put(_k_, _v_);
		}
		hammer = _os_.unmarshal_int();
		freegoldtime = _os_.unmarshal_int();
		freeybtime = _os_.unmarshal_int();
		goldbuynum = _os_.unmarshal_int();
		tibuynum = _os_.unmarshal_int();
		signnum7 = _os_.unmarshal_int();
		signnum28 = _os_.unmarshal_int();
		mailsize = _os_.unmarshal_byte();
		buybagnum = _os_.unmarshal_short();
		buyherobagnum = _os_.unmarshal_short();
		smid = _os_.unmarshal_int();
		smtime = _os_.unmarshal_int();
		smzhangjie = _os_.unmarshal_int();
		shenglingzq = _os_.unmarshal_int();
		ronglian = _os_.unmarshal_int();
		huangjinxz = _os_.unmarshal_int();
		baijinxz = _os_.unmarshal_int();
		qingtongxz = _os_.unmarshal_int();
		chitiexz = _os_.unmarshal_int();
		jyjiejing = _os_.unmarshal_int();
		troopnum = _os_.unmarshal_byte();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			heroskins.add(_v_);
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.Artifact _v_ = new chuhan.gsp.Artifact();
			_v_.unmarshal(_os_);
			artifacts.put(_k_, _v_);
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.Shopbuy _v_ = new chuhan.gsp.Shopbuy();
			_v_.unmarshal(_os_);
			shopbuys.put(_k_, _v_);
		}
		sweephavenum = _os_.unmarshal_int();
		sweepbuyhavenum = _os_.unmarshal_int();
		mszqgetnum = _os_.unmarshal_int();
		qiyuannum = _os_.unmarshal_int();
		qiyuanallnum = _os_.unmarshal_int();
		isqiyuantoday = _os_.unmarshal_int();
		txti = _os_.unmarshal_short();
		txtitime = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			newyindao.add(_v_);
		}
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _k_;
			_k_ = _os_.unmarshal_int();
			chuhan.gsp.stage.smshopdata _v_ = new chuhan.gsp.stage.smshopdata();
			_v_.unmarshal(_os_);
			smshop.put(_k_, _v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof RoleDetail) {
			RoleDetail _o_ = (RoleDetail)_o1_;
			if (roleid != _o_.roleid) return false;
			if (!name.equals(_o_.name)) return false;
			if (isgm != _o_.isgm) return false;
			if (level != _o_.level) return false;
			if (exp != _o_.exp) return false;
			if (viplv != _o_.viplv) return false;
			if (vipexp != _o_.vipexp) return false;
			if (ti != _o_.ti) return false;
			if (titime != _o_.titime) return false;
			if (money != _o_.money) return false;
			if (yuanbao != _o_.yuanbao) return false;
			if (battlenum != _o_.battlenum) return false;
			if (servertime != _o_.servertime) return false;
			if (timezone != _o_.timezone) return false;
			if (!heroes.equals(_o_.heroes)) return false;
			if (!troops.equals(_o_.troops)) return false;
			if (!baginfo.equals(_o_.baginfo)) return false;
			if (hammer != _o_.hammer) return false;
			if (freegoldtime != _o_.freegoldtime) return false;
			if (freeybtime != _o_.freeybtime) return false;
			if (goldbuynum != _o_.goldbuynum) return false;
			if (tibuynum != _o_.tibuynum) return false;
			if (signnum7 != _o_.signnum7) return false;
			if (signnum28 != _o_.signnum28) return false;
			if (mailsize != _o_.mailsize) return false;
			if (buybagnum != _o_.buybagnum) return false;
			if (buyherobagnum != _o_.buyherobagnum) return false;
			if (smid != _o_.smid) return false;
			if (smtime != _o_.smtime) return false;
			if (smzhangjie != _o_.smzhangjie) return false;
			if (shenglingzq != _o_.shenglingzq) return false;
			if (ronglian != _o_.ronglian) return false;
			if (huangjinxz != _o_.huangjinxz) return false;
			if (baijinxz != _o_.baijinxz) return false;
			if (qingtongxz != _o_.qingtongxz) return false;
			if (chitiexz != _o_.chitiexz) return false;
			if (jyjiejing != _o_.jyjiejing) return false;
			if (troopnum != _o_.troopnum) return false;
			if (!heroskins.equals(_o_.heroskins)) return false;
			if (!artifacts.equals(_o_.artifacts)) return false;
			if (!shopbuys.equals(_o_.shopbuys)) return false;
			if (sweephavenum != _o_.sweephavenum) return false;
			if (sweepbuyhavenum != _o_.sweepbuyhavenum) return false;
			if (mszqgetnum != _o_.mszqgetnum) return false;
			if (qiyuannum != _o_.qiyuannum) return false;
			if (qiyuanallnum != _o_.qiyuanallnum) return false;
			if (isqiyuantoday != _o_.isqiyuantoday) return false;
			if (txti != _o_.txti) return false;
			if (txtitime != _o_.txtitime) return false;
			if (!newyindao.equals(_o_.newyindao)) return false;
			if (!smshop.equals(_o_.smshop)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)roleid;
		_h_ += name.hashCode();
		_h_ += (int)isgm;
		_h_ += level;
		_h_ += exp;
		_h_ += (int)viplv;
		_h_ += vipexp;
		_h_ += ti;
		_h_ += titime;
		_h_ += (int)money;
		_h_ += yuanbao;
		_h_ += battlenum;
		_h_ += (int)servertime;
		_h_ += (int)timezone;
		_h_ += heroes.hashCode();
		_h_ += troops.hashCode();
		_h_ += baginfo.hashCode();
		_h_ += hammer;
		_h_ += freegoldtime;
		_h_ += freeybtime;
		_h_ += goldbuynum;
		_h_ += tibuynum;
		_h_ += signnum7;
		_h_ += signnum28;
		_h_ += (int)mailsize;
		_h_ += buybagnum;
		_h_ += buyherobagnum;
		_h_ += smid;
		_h_ += smtime;
		_h_ += smzhangjie;
		_h_ += shenglingzq;
		_h_ += ronglian;
		_h_ += huangjinxz;
		_h_ += baijinxz;
		_h_ += qingtongxz;
		_h_ += chitiexz;
		_h_ += jyjiejing;
		_h_ += (int)troopnum;
		_h_ += heroskins.hashCode();
		_h_ += artifacts.hashCode();
		_h_ += shopbuys.hashCode();
		_h_ += sweephavenum;
		_h_ += sweepbuyhavenum;
		_h_ += mszqgetnum;
		_h_ += qiyuannum;
		_h_ += qiyuanallnum;
		_h_ += isqiyuantoday;
		_h_ += txti;
		_h_ += txtitime;
		_h_ += newyindao.hashCode();
		_h_ += smshop.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(roleid).append(",");
		_sb_.append("T").append(name.length()).append(",");
		_sb_.append(isgm).append(",");
		_sb_.append(level).append(",");
		_sb_.append(exp).append(",");
		_sb_.append(viplv).append(",");
		_sb_.append(vipexp).append(",");
		_sb_.append(ti).append(",");
		_sb_.append(titime).append(",");
		_sb_.append(money).append(",");
		_sb_.append(yuanbao).append(",");
		_sb_.append(battlenum).append(",");
		_sb_.append(servertime).append(",");
		_sb_.append(timezone).append(",");
		_sb_.append(heroes).append(",");
		_sb_.append(troops).append(",");
		_sb_.append(baginfo).append(",");
		_sb_.append(hammer).append(",");
		_sb_.append(freegoldtime).append(",");
		_sb_.append(freeybtime).append(",");
		_sb_.append(goldbuynum).append(",");
		_sb_.append(tibuynum).append(",");
		_sb_.append(signnum7).append(",");
		_sb_.append(signnum28).append(",");
		_sb_.append(mailsize).append(",");
		_sb_.append(buybagnum).append(",");
		_sb_.append(buyherobagnum).append(",");
		_sb_.append(smid).append(",");
		_sb_.append(smtime).append(",");
		_sb_.append(smzhangjie).append(",");
		_sb_.append(shenglingzq).append(",");
		_sb_.append(ronglian).append(",");
		_sb_.append(huangjinxz).append(",");
		_sb_.append(baijinxz).append(",");
		_sb_.append(qingtongxz).append(",");
		_sb_.append(chitiexz).append(",");
		_sb_.append(jyjiejing).append(",");
		_sb_.append(troopnum).append(",");
		_sb_.append(heroskins).append(",");
		_sb_.append(artifacts).append(",");
		_sb_.append(shopbuys).append(",");
		_sb_.append(sweephavenum).append(",");
		_sb_.append(sweepbuyhavenum).append(",");
		_sb_.append(mszqgetnum).append(",");
		_sb_.append(qiyuannum).append(",");
		_sb_.append(qiyuanallnum).append(",");
		_sb_.append(isqiyuantoday).append(",");
		_sb_.append(txti).append(",");
		_sb_.append(txtitime).append(",");
		_sb_.append(newyindao).append(",");
		_sb_.append(smshop).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

