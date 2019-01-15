
package xbean.__;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public final class Properties extends xdb.XBean implements xbean.Properties {
	private String rolename; // 角色名
	private int userid; // 所属角色id
	private String username; // 上次登录的账号名称
	private String plattypestr; // 上次登录的平台string
	private String mac; // 上次登录的MAC地址
	private int ostype; // 系统类型
	private int level; // 等级
	private int exp; // 经验
	private int viplv; // vip等级
	private int vipexp; // vip经验
	private int ti; // 体力
	private long tichangetime; // 体力更新时间
	private int gold; // 金币
	private int yuanbao; // 元宝（水晶）
	private int shenglingzq; // 圣灵之泉
	private int ronglian; // 熔炼点
	private int huangjinxz; // 黄金勋章
	private int baijinxz; // 白金勋章
	private int qingtongxz; // 青铜勋章
	private int chitiexz; // 赤铁勋章
	private int jyjiejing; // 经验结晶
	private int pvpti; // PVP精力
	private long pvptitime; // PVP精力更新时间
	private int tanxianti; // 探险行动力
	private long tanxiantitime; // 探险行动力更新时间
	private int jinengdian; // 技能点
	private long jinengdiantime; // 技能点更新时间
	private java.util.HashMap<Integer, xbean.mohe> moheshop; // 魔盒列表
	private int smzhangjie; // 神秘关卡或商店的所属章节记录
	private int battlenum; // 神秘关卡或商店记录
	private long smendtime; // 神秘关卡或商店结束时间
	private java.util.HashMap<Integer, xbean.smshopdata> smshop; // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
	private int smguanka_nocome; // 神秘关卡未出现次数
	private int smshop_notcome; // 神秘商店未出现次数
	private long createtime; // 创建时间
	private long onlinetime; // 上线时间
	private long offlinetime; // 下线时间
	private int tibuynum; // 体力购买次数
	private long tibuytime; // 上次记录的体力购买时间
	private int goldbuynum; // 金币购买次数
	private long goldbuytime; // 上次记录的金币购买时间
	private int signnum7; // 连续签到ID
	private int signnum28; // 累计签到ID
	private long signtime; // 最后签到时间
	private int qiyuannum; // 累计祈愿台次数
	private long qiyuantime; // 最后祈愿时间
	private int qiyuanallnum; // 祈愿回合次数，第一次为3，完成后均为5
	private short buybagnum; // 扩充背包次数
	private short buyherobagnum; // 扩充英雄背包次数
	private short troopnum; // 默认编队号
	private int sweepnum; // 今日扫荡次数
	private long todaylasttime; // 今日计时时间
	private int sweepbuynum; // 今日扫荡购买次数
	private int mszqgetnum; // 缪斯奏曲：个位为中午，十位为晚上
	private java.util.LinkedList<Integer> newyindao; // 新手引导

	Properties(int __, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		rolename = "";
		username = "";
		plattypestr = "";
		mac = "";
		level = 1;
		viplv = 1;
		moheshop = new java.util.HashMap<Integer, xbean.mohe>();
		smshop = new java.util.HashMap<Integer, xbean.smshopdata>();
		tibuynum = 0;
		tibuytime = 0;
		goldbuynum = 0;
		goldbuytime = 0;
		signnum7 = 0;
		signnum28 = 0;
		signtime = 0;
		qiyuannum = 0;
		qiyuantime = 0;
		qiyuanallnum = 3;
		buybagnum = 0;
		buyherobagnum = 0;
		troopnum = 1;
		sweepnum = 0;
		todaylasttime = 0;
		sweepbuynum = 0;
		mszqgetnum = 0;
		newyindao = new java.util.LinkedList<Integer>();
	}

	public Properties() {
		this(0, null, null);
	}

	public Properties(Properties _o_) {
		this(_o_, null, null);
	}

	Properties(xbean.Properties _o1_, xdb.XBean _xp_, String _vn_) {
		super(_xp_, _vn_);
		if (_o1_ instanceof Properties) assign((Properties)_o1_);
		else if (_o1_ instanceof Properties.Data) assign((Properties.Data)_o1_);
		else if (_o1_ instanceof Properties.Const) assign(((Properties.Const)_o1_).nThis());
		else throw new UnsupportedOperationException();
	}

	private void assign(Properties _o_) {
		_o_._xdb_verify_unsafe_();
		rolename = _o_.rolename;
		userid = _o_.userid;
		username = _o_.username;
		plattypestr = _o_.plattypestr;
		mac = _o_.mac;
		ostype = _o_.ostype;
		level = _o_.level;
		exp = _o_.exp;
		viplv = _o_.viplv;
		vipexp = _o_.vipexp;
		ti = _o_.ti;
		tichangetime = _o_.tichangetime;
		gold = _o_.gold;
		yuanbao = _o_.yuanbao;
		shenglingzq = _o_.shenglingzq;
		ronglian = _o_.ronglian;
		huangjinxz = _o_.huangjinxz;
		baijinxz = _o_.baijinxz;
		qingtongxz = _o_.qingtongxz;
		chitiexz = _o_.chitiexz;
		jyjiejing = _o_.jyjiejing;
		pvpti = _o_.pvpti;
		pvptitime = _o_.pvptitime;
		tanxianti = _o_.tanxianti;
		tanxiantitime = _o_.tanxiantitime;
		jinengdian = _o_.jinengdian;
		jinengdiantime = _o_.jinengdiantime;
		moheshop = new java.util.HashMap<Integer, xbean.mohe>();
		for (java.util.Map.Entry<Integer, xbean.mohe> _e_ : _o_.moheshop.entrySet())
			moheshop.put(_e_.getKey(), new mohe(_e_.getValue(), this, "moheshop"));
		smzhangjie = _o_.smzhangjie;
		battlenum = _o_.battlenum;
		smendtime = _o_.smendtime;
		smshop = new java.util.HashMap<Integer, xbean.smshopdata>();
		for (java.util.Map.Entry<Integer, xbean.smshopdata> _e_ : _o_.smshop.entrySet())
			smshop.put(_e_.getKey(), new smshopdata(_e_.getValue(), this, "smshop"));
		smguanka_nocome = _o_.smguanka_nocome;
		smshop_notcome = _o_.smshop_notcome;
		createtime = _o_.createtime;
		onlinetime = _o_.onlinetime;
		offlinetime = _o_.offlinetime;
		tibuynum = _o_.tibuynum;
		tibuytime = _o_.tibuytime;
		goldbuynum = _o_.goldbuynum;
		goldbuytime = _o_.goldbuytime;
		signnum7 = _o_.signnum7;
		signnum28 = _o_.signnum28;
		signtime = _o_.signtime;
		qiyuannum = _o_.qiyuannum;
		qiyuantime = _o_.qiyuantime;
		qiyuanallnum = _o_.qiyuanallnum;
		buybagnum = _o_.buybagnum;
		buyherobagnum = _o_.buyherobagnum;
		troopnum = _o_.troopnum;
		sweepnum = _o_.sweepnum;
		todaylasttime = _o_.todaylasttime;
		sweepbuynum = _o_.sweepbuynum;
		mszqgetnum = _o_.mszqgetnum;
		newyindao = new java.util.LinkedList<Integer>();
		newyindao.addAll(_o_.newyindao);
	}

	private void assign(Properties.Data _o_) {
		rolename = _o_.rolename;
		userid = _o_.userid;
		username = _o_.username;
		plattypestr = _o_.plattypestr;
		mac = _o_.mac;
		ostype = _o_.ostype;
		level = _o_.level;
		exp = _o_.exp;
		viplv = _o_.viplv;
		vipexp = _o_.vipexp;
		ti = _o_.ti;
		tichangetime = _o_.tichangetime;
		gold = _o_.gold;
		yuanbao = _o_.yuanbao;
		shenglingzq = _o_.shenglingzq;
		ronglian = _o_.ronglian;
		huangjinxz = _o_.huangjinxz;
		baijinxz = _o_.baijinxz;
		qingtongxz = _o_.qingtongxz;
		chitiexz = _o_.chitiexz;
		jyjiejing = _o_.jyjiejing;
		pvpti = _o_.pvpti;
		pvptitime = _o_.pvptitime;
		tanxianti = _o_.tanxianti;
		tanxiantitime = _o_.tanxiantitime;
		jinengdian = _o_.jinengdian;
		jinengdiantime = _o_.jinengdiantime;
		moheshop = new java.util.HashMap<Integer, xbean.mohe>();
		for (java.util.Map.Entry<Integer, xbean.mohe> _e_ : _o_.moheshop.entrySet())
			moheshop.put(_e_.getKey(), new mohe(_e_.getValue(), this, "moheshop"));
		smzhangjie = _o_.smzhangjie;
		battlenum = _o_.battlenum;
		smendtime = _o_.smendtime;
		smshop = new java.util.HashMap<Integer, xbean.smshopdata>();
		for (java.util.Map.Entry<Integer, xbean.smshopdata> _e_ : _o_.smshop.entrySet())
			smshop.put(_e_.getKey(), new smshopdata(_e_.getValue(), this, "smshop"));
		smguanka_nocome = _o_.smguanka_nocome;
		smshop_notcome = _o_.smshop_notcome;
		createtime = _o_.createtime;
		onlinetime = _o_.onlinetime;
		offlinetime = _o_.offlinetime;
		tibuynum = _o_.tibuynum;
		tibuytime = _o_.tibuytime;
		goldbuynum = _o_.goldbuynum;
		goldbuytime = _o_.goldbuytime;
		signnum7 = _o_.signnum7;
		signnum28 = _o_.signnum28;
		signtime = _o_.signtime;
		qiyuannum = _o_.qiyuannum;
		qiyuantime = _o_.qiyuantime;
		qiyuanallnum = _o_.qiyuanallnum;
		buybagnum = _o_.buybagnum;
		buyherobagnum = _o_.buyherobagnum;
		troopnum = _o_.troopnum;
		sweepnum = _o_.sweepnum;
		todaylasttime = _o_.todaylasttime;
		sweepbuynum = _o_.sweepbuynum;
		mszqgetnum = _o_.mszqgetnum;
		newyindao = new java.util.LinkedList<Integer>();
		newyindao.addAll(_o_.newyindao);
	}

	@Override
	public final OctetsStream marshal(OctetsStream _os_) {
		_xdb_verify_unsafe_();
		_os_.marshal(rolename, xdb.Const.IO_CHARSET);
		_os_.marshal(userid);
		_os_.marshal(username, xdb.Const.IO_CHARSET);
		_os_.marshal(plattypestr, xdb.Const.IO_CHARSET);
		_os_.marshal(mac, xdb.Const.IO_CHARSET);
		_os_.marshal(ostype);
		_os_.marshal(level);
		_os_.marshal(exp);
		_os_.marshal(viplv);
		_os_.marshal(vipexp);
		_os_.marshal(ti);
		_os_.marshal(tichangetime);
		_os_.marshal(gold);
		_os_.marshal(yuanbao);
		_os_.marshal(shenglingzq);
		_os_.marshal(ronglian);
		_os_.marshal(huangjinxz);
		_os_.marshal(baijinxz);
		_os_.marshal(qingtongxz);
		_os_.marshal(chitiexz);
		_os_.marshal(jyjiejing);
		_os_.marshal(pvpti);
		_os_.marshal(pvptitime);
		_os_.marshal(tanxianti);
		_os_.marshal(tanxiantitime);
		_os_.marshal(jinengdian);
		_os_.marshal(jinengdiantime);
		_os_.compact_uint32(moheshop.size());
		for (java.util.Map.Entry<Integer, xbean.mohe> _e_ : moheshop.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		_os_.marshal(smzhangjie);
		_os_.marshal(battlenum);
		_os_.marshal(smendtime);
		_os_.compact_uint32(smshop.size());
		for (java.util.Map.Entry<Integer, xbean.smshopdata> _e_ : smshop.entrySet())
		{
			_os_.marshal(_e_.getKey());
			_e_.getValue().marshal(_os_);
		}
		_os_.marshal(smguanka_nocome);
		_os_.marshal(smshop_notcome);
		_os_.marshal(createtime);
		_os_.marshal(onlinetime);
		_os_.marshal(offlinetime);
		_os_.marshal(tibuynum);
		_os_.marshal(tibuytime);
		_os_.marshal(goldbuynum);
		_os_.marshal(goldbuytime);
		_os_.marshal(signnum7);
		_os_.marshal(signnum28);
		_os_.marshal(signtime);
		_os_.marshal(qiyuannum);
		_os_.marshal(qiyuantime);
		_os_.marshal(qiyuanallnum);
		_os_.marshal(buybagnum);
		_os_.marshal(buyherobagnum);
		_os_.marshal(troopnum);
		_os_.marshal(sweepnum);
		_os_.marshal(todaylasttime);
		_os_.marshal(sweepbuynum);
		_os_.marshal(mszqgetnum);
		_os_.compact_uint32(newyindao.size());
		for (Integer _v_ : newyindao) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	@Override
	public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		_xdb_verify_unsafe_();
		rolename = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		userid = _os_.unmarshal_int();
		username = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		plattypestr = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		mac = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
		ostype = _os_.unmarshal_int();
		level = _os_.unmarshal_int();
		exp = _os_.unmarshal_int();
		viplv = _os_.unmarshal_int();
		vipexp = _os_.unmarshal_int();
		ti = _os_.unmarshal_int();
		tichangetime = _os_.unmarshal_long();
		gold = _os_.unmarshal_int();
		yuanbao = _os_.unmarshal_int();
		shenglingzq = _os_.unmarshal_int();
		ronglian = _os_.unmarshal_int();
		huangjinxz = _os_.unmarshal_int();
		baijinxz = _os_.unmarshal_int();
		qingtongxz = _os_.unmarshal_int();
		chitiexz = _os_.unmarshal_int();
		jyjiejing = _os_.unmarshal_int();
		pvpti = _os_.unmarshal_int();
		pvptitime = _os_.unmarshal_long();
		tanxianti = _os_.unmarshal_int();
		tanxiantitime = _os_.unmarshal_long();
		jinengdian = _os_.unmarshal_int();
		jinengdiantime = _os_.unmarshal_long();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				moheshop = new java.util.HashMap<Integer, xbean.mohe>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.mohe _v_ = new mohe(0, this, "moheshop");
				_v_.unmarshal(_os_);
				moheshop.put(_k_, _v_);
			}
		}
		smzhangjie = _os_.unmarshal_int();
		battlenum = _os_.unmarshal_int();
		smendtime = _os_.unmarshal_long();
		{
			int size = _os_.uncompact_uint32();
			if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
				smshop = new java.util.HashMap<Integer, xbean.smshopdata>(size * 2);
			}
			for (; size > 0; --size)
			{
				int _k_ = 0;
				_k_ = _os_.unmarshal_int();
				xbean.smshopdata _v_ = new smshopdata(0, this, "smshop");
				_v_.unmarshal(_os_);
				smshop.put(_k_, _v_);
			}
		}
		smguanka_nocome = _os_.unmarshal_int();
		smshop_notcome = _os_.unmarshal_int();
		createtime = _os_.unmarshal_long();
		onlinetime = _os_.unmarshal_long();
		offlinetime = _os_.unmarshal_long();
		tibuynum = _os_.unmarshal_int();
		tibuytime = _os_.unmarshal_long();
		goldbuynum = _os_.unmarshal_int();
		goldbuytime = _os_.unmarshal_long();
		signnum7 = _os_.unmarshal_int();
		signnum28 = _os_.unmarshal_int();
		signtime = _os_.unmarshal_long();
		qiyuannum = _os_.unmarshal_int();
		qiyuantime = _os_.unmarshal_long();
		qiyuanallnum = _os_.unmarshal_int();
		buybagnum = _os_.unmarshal_short();
		buyherobagnum = _os_.unmarshal_short();
		troopnum = _os_.unmarshal_short();
		sweepnum = _os_.unmarshal_int();
		todaylasttime = _os_.unmarshal_long();
		sweepbuynum = _os_.unmarshal_int();
		mszqgetnum = _os_.unmarshal_int();
		for (int size = _os_.uncompact_uint32(); size > 0; --size) {
			int _v_ = 0;
			_v_ = _os_.unmarshal_int();
			newyindao.add(_v_);
		}
		return _os_;
	}

	@Override
	public xbean.Properties copy() {
		_xdb_verify_unsafe_();
		return new Properties(this);
	}

	@Override
	public xbean.Properties toData() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Properties toBean() {
		_xdb_verify_unsafe_();
		return new Properties(this); // same as copy()
	}

	@Override
	public xbean.Properties toDataIf() {
		_xdb_verify_unsafe_();
		return new Data(this);
	}

	public xbean.Properties toBeanIf() {
		_xdb_verify_unsafe_();
		return this;
	}

	@Override
	public xdb.Bean toConst() {
		_xdb_verify_unsafe_();
		return new Const();
	}

	@Override
	public String getRolename() { // 角色名
		_xdb_verify_unsafe_();
		return rolename;
	}

	@Override
	public com.goldhuman.Common.Octets getRolenameOctets() { // 角色名
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getRolename(), xdb.Const.IO_CHARSET);
	}

	@Override
	public int getUserid() { // 所属角色id
		_xdb_verify_unsafe_();
		return userid;
	}

	@Override
	public String getUsername() { // 上次登录的账号名称
		_xdb_verify_unsafe_();
		return username;
	}

	@Override
	public com.goldhuman.Common.Octets getUsernameOctets() { // 上次登录的账号名称
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getUsername(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getPlattypestr() { // 上次登录的平台string
		_xdb_verify_unsafe_();
		return plattypestr;
	}

	@Override
	public com.goldhuman.Common.Octets getPlattypestrOctets() { // 上次登录的平台string
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getPlattypestr(), xdb.Const.IO_CHARSET);
	}

	@Override
	public String getMac() { // 上次登录的MAC地址
		_xdb_verify_unsafe_();
		return mac;
	}

	@Override
	public com.goldhuman.Common.Octets getMacOctets() { // 上次登录的MAC地址
		_xdb_verify_unsafe_();
		return com.goldhuman.Common.Octets.wrap(getMac(), xdb.Const.IO_CHARSET);
	}

	@Override
	public int getOstype() { // 系统类型
		_xdb_verify_unsafe_();
		return ostype;
	}

	@Override
	public int getLevel() { // 等级
		_xdb_verify_unsafe_();
		return level;
	}

	@Override
	public int getExp() { // 经验
		_xdb_verify_unsafe_();
		return exp;
	}

	@Override
	public int getViplv() { // vip等级
		_xdb_verify_unsafe_();
		return viplv;
	}

	@Override
	public int getVipexp() { // vip经验
		_xdb_verify_unsafe_();
		return vipexp;
	}

	@Override
	public int getTi() { // 体力
		_xdb_verify_unsafe_();
		return ti;
	}

	@Override
	public long getTichangetime() { // 体力更新时间
		_xdb_verify_unsafe_();
		return tichangetime;
	}

	@Override
	public int getGold() { // 金币
		_xdb_verify_unsafe_();
		return gold;
	}

	@Override
	public int getYuanbao() { // 元宝（水晶）
		_xdb_verify_unsafe_();
		return yuanbao;
	}

	@Override
	public int getShenglingzq() { // 圣灵之泉
		_xdb_verify_unsafe_();
		return shenglingzq;
	}

	@Override
	public int getRonglian() { // 熔炼点
		_xdb_verify_unsafe_();
		return ronglian;
	}

	@Override
	public int getHuangjinxz() { // 黄金勋章
		_xdb_verify_unsafe_();
		return huangjinxz;
	}

	@Override
	public int getBaijinxz() { // 白金勋章
		_xdb_verify_unsafe_();
		return baijinxz;
	}

	@Override
	public int getQingtongxz() { // 青铜勋章
		_xdb_verify_unsafe_();
		return qingtongxz;
	}

	@Override
	public int getChitiexz() { // 赤铁勋章
		_xdb_verify_unsafe_();
		return chitiexz;
	}

	@Override
	public int getJyjiejing() { // 经验结晶
		_xdb_verify_unsafe_();
		return jyjiejing;
	}

	@Override
	public int getPvpti() { // PVP精力
		_xdb_verify_unsafe_();
		return pvpti;
	}

	@Override
	public long getPvptitime() { // PVP精力更新时间
		_xdb_verify_unsafe_();
		return pvptitime;
	}

	@Override
	public int getTanxianti() { // 探险行动力
		_xdb_verify_unsafe_();
		return tanxianti;
	}

	@Override
	public long getTanxiantitime() { // 探险行动力更新时间
		_xdb_verify_unsafe_();
		return tanxiantitime;
	}

	@Override
	public int getJinengdian() { // 技能点
		_xdb_verify_unsafe_();
		return jinengdian;
	}

	@Override
	public long getJinengdiantime() { // 技能点更新时间
		_xdb_verify_unsafe_();
		return jinengdiantime;
	}

	@Override
	public java.util.Map<Integer, xbean.mohe> getMoheshop() { // 魔盒列表
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "moheshop"), moheshop);
	}

	@Override
	public java.util.Map<Integer, xbean.mohe> getMoheshopAsData() { // 魔盒列表
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.mohe> moheshop;
		Properties _o_ = this;
		moheshop = new java.util.HashMap<Integer, xbean.mohe>();
		for (java.util.Map.Entry<Integer, xbean.mohe> _e_ : _o_.moheshop.entrySet())
			moheshop.put(_e_.getKey(), new mohe.Data(_e_.getValue()));
		return moheshop;
	}

	@Override
	public int getSmzhangjie() { // 神秘关卡或商店的所属章节记录
		_xdb_verify_unsafe_();
		return smzhangjie;
	}

	@Override
	public int getBattlenum() { // 神秘关卡或商店记录
		_xdb_verify_unsafe_();
		return battlenum;
	}

	@Override
	public long getSmendtime() { // 神秘关卡或商店结束时间
		_xdb_verify_unsafe_();
		return smendtime;
	}

	@Override
	public java.util.Map<Integer, xbean.smshopdata> getSmshop() { // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
		_xdb_verify_unsafe_();
		return xdb.Logs.logMap(new xdb.LogKey(this, "smshop"), smshop);
	}

	@Override
	public java.util.Map<Integer, xbean.smshopdata> getSmshopAsData() { // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
		_xdb_verify_unsafe_();
		java.util.Map<Integer, xbean.smshopdata> smshop;
		Properties _o_ = this;
		smshop = new java.util.HashMap<Integer, xbean.smshopdata>();
		for (java.util.Map.Entry<Integer, xbean.smshopdata> _e_ : _o_.smshop.entrySet())
			smshop.put(_e_.getKey(), new smshopdata.Data(_e_.getValue()));
		return smshop;
	}

	@Override
	public int getSmguanka_nocome() { // 神秘关卡未出现次数
		_xdb_verify_unsafe_();
		return smguanka_nocome;
	}

	@Override
	public int getSmshop_notcome() { // 神秘商店未出现次数
		_xdb_verify_unsafe_();
		return smshop_notcome;
	}

	@Override
	public long getCreatetime() { // 创建时间
		_xdb_verify_unsafe_();
		return createtime;
	}

	@Override
	public long getOnlinetime() { // 上线时间
		_xdb_verify_unsafe_();
		return onlinetime;
	}

	@Override
	public long getOfflinetime() { // 下线时间
		_xdb_verify_unsafe_();
		return offlinetime;
	}

	@Override
	public int getTibuynum() { // 体力购买次数
		_xdb_verify_unsafe_();
		return tibuynum;
	}

	@Override
	public long getTibuytime() { // 上次记录的体力购买时间
		_xdb_verify_unsafe_();
		return tibuytime;
	}

	@Override
	public int getGoldbuynum() { // 金币购买次数
		_xdb_verify_unsafe_();
		return goldbuynum;
	}

	@Override
	public long getGoldbuytime() { // 上次记录的金币购买时间
		_xdb_verify_unsafe_();
		return goldbuytime;
	}

	@Override
	public int getSignnum7() { // 连续签到ID
		_xdb_verify_unsafe_();
		return signnum7;
	}

	@Override
	public int getSignnum28() { // 累计签到ID
		_xdb_verify_unsafe_();
		return signnum28;
	}

	@Override
	public long getSigntime() { // 最后签到时间
		_xdb_verify_unsafe_();
		return signtime;
	}

	@Override
	public int getQiyuannum() { // 累计祈愿台次数
		_xdb_verify_unsafe_();
		return qiyuannum;
	}

	@Override
	public long getQiyuantime() { // 最后祈愿时间
		_xdb_verify_unsafe_();
		return qiyuantime;
	}

	@Override
	public int getQiyuanallnum() { // 祈愿回合次数，第一次为3，完成后均为5
		_xdb_verify_unsafe_();
		return qiyuanallnum;
	}

	@Override
	public short getBuybagnum() { // 扩充背包次数
		_xdb_verify_unsafe_();
		return buybagnum;
	}

	@Override
	public short getBuyherobagnum() { // 扩充英雄背包次数
		_xdb_verify_unsafe_();
		return buyherobagnum;
	}

	@Override
	public short getTroopnum() { // 默认编队号
		_xdb_verify_unsafe_();
		return troopnum;
	}

	@Override
	public int getSweepnum() { // 今日扫荡次数
		_xdb_verify_unsafe_();
		return sweepnum;
	}

	@Override
	public long getTodaylasttime() { // 今日计时时间
		_xdb_verify_unsafe_();
		return todaylasttime;
	}

	@Override
	public int getSweepbuynum() { // 今日扫荡购买次数
		_xdb_verify_unsafe_();
		return sweepbuynum;
	}

	@Override
	public int getMszqgetnum() { // 缪斯奏曲：个位为中午，十位为晚上
		_xdb_verify_unsafe_();
		return mszqgetnum;
	}

	@Override
	public java.util.List<Integer> getNewyindao() { // 新手引导
		_xdb_verify_unsafe_();
		return xdb.Logs.logList(new xdb.LogKey(this, "newyindao"), newyindao);
	}

	public java.util.List<Integer> getNewyindaoAsData() { // 新手引导
		_xdb_verify_unsafe_();
		java.util.List<Integer> newyindao;
		Properties _o_ = this;
		newyindao = new java.util.LinkedList<Integer>();
		newyindao.addAll(_o_.newyindao);
		return newyindao;
	}

	@Override
	public void setRolename(String _v_) { // 角色名
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "rolename") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, rolename) {
					public void rollback() { rolename = _xdb_saved; }
				};}});
		rolename = _v_;
	}

	@Override
	public void setRolenameOctets(com.goldhuman.Common.Octets _v_) { // 角色名
		_xdb_verify_unsafe_();
		this.setRolename(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setUserid(int _v_) { // 所属角色id
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "userid") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, userid) {
					public void rollback() { userid = _xdb_saved; }
				};}});
		userid = _v_;
	}

	@Override
	public void setUsername(String _v_) { // 上次登录的账号名称
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "username") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, username) {
					public void rollback() { username = _xdb_saved; }
				};}});
		username = _v_;
	}

	@Override
	public void setUsernameOctets(com.goldhuman.Common.Octets _v_) { // 上次登录的账号名称
		_xdb_verify_unsafe_();
		this.setUsername(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setPlattypestr(String _v_) { // 上次登录的平台string
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "plattypestr") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, plattypestr) {
					public void rollback() { plattypestr = _xdb_saved; }
				};}});
		plattypestr = _v_;
	}

	@Override
	public void setPlattypestrOctets(com.goldhuman.Common.Octets _v_) { // 上次登录的平台string
		_xdb_verify_unsafe_();
		this.setPlattypestr(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setMac(String _v_) { // 上次登录的MAC地址
		_xdb_verify_unsafe_();
		if (null == _v_)
			throw new NullPointerException();
		xdb.Logs.logIf(new xdb.LogKey(this, "mac") {
			protected xdb.Log create() {
				return new xdb.logs.LogString(this, mac) {
					public void rollback() { mac = _xdb_saved; }
				};}});
		mac = _v_;
	}

	@Override
	public void setMacOctets(com.goldhuman.Common.Octets _v_) { // 上次登录的MAC地址
		_xdb_verify_unsafe_();
		this.setMac(_v_.getString(xdb.Const.IO_CHARSET));
	}

	@Override
	public void setOstype(int _v_) { // 系统类型
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "ostype") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, ostype) {
					public void rollback() { ostype = _xdb_saved; }
				};}});
		ostype = _v_;
	}

	@Override
	public void setLevel(int _v_) { // 等级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "level") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, level) {
					public void rollback() { level = _xdb_saved; }
				};}});
		level = _v_;
	}

	@Override
	public void setExp(int _v_) { // 经验
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "exp") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, exp) {
					public void rollback() { exp = _xdb_saved; }
				};}});
		exp = _v_;
	}

	@Override
	public void setViplv(int _v_) { // vip等级
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "viplv") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, viplv) {
					public void rollback() { viplv = _xdb_saved; }
				};}});
		viplv = _v_;
	}

	@Override
	public void setVipexp(int _v_) { // vip经验
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "vipexp") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, vipexp) {
					public void rollback() { vipexp = _xdb_saved; }
				};}});
		vipexp = _v_;
	}

	@Override
	public void setTi(int _v_) { // 体力
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "ti") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, ti) {
					public void rollback() { ti = _xdb_saved; }
				};}});
		ti = _v_;
	}

	@Override
	public void setTichangetime(long _v_) { // 体力更新时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "tichangetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, tichangetime) {
					public void rollback() { tichangetime = _xdb_saved; }
				};}});
		tichangetime = _v_;
	}

	@Override
	public void setGold(int _v_) { // 金币
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "gold") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, gold) {
					public void rollback() { gold = _xdb_saved; }
				};}});
		gold = _v_;
	}

	@Override
	public void setYuanbao(int _v_) { // 元宝（水晶）
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "yuanbao") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, yuanbao) {
					public void rollback() { yuanbao = _xdb_saved; }
				};}});
		yuanbao = _v_;
	}

	@Override
	public void setShenglingzq(int _v_) { // 圣灵之泉
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "shenglingzq") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, shenglingzq) {
					public void rollback() { shenglingzq = _xdb_saved; }
				};}});
		shenglingzq = _v_;
	}

	@Override
	public void setRonglian(int _v_) { // 熔炼点
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "ronglian") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, ronglian) {
					public void rollback() { ronglian = _xdb_saved; }
				};}});
		ronglian = _v_;
	}

	@Override
	public void setHuangjinxz(int _v_) { // 黄金勋章
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "huangjinxz") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, huangjinxz) {
					public void rollback() { huangjinxz = _xdb_saved; }
				};}});
		huangjinxz = _v_;
	}

	@Override
	public void setBaijinxz(int _v_) { // 白金勋章
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "baijinxz") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, baijinxz) {
					public void rollback() { baijinxz = _xdb_saved; }
				};}});
		baijinxz = _v_;
	}

	@Override
	public void setQingtongxz(int _v_) { // 青铜勋章
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "qingtongxz") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, qingtongxz) {
					public void rollback() { qingtongxz = _xdb_saved; }
				};}});
		qingtongxz = _v_;
	}

	@Override
	public void setChitiexz(int _v_) { // 赤铁勋章
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "chitiexz") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, chitiexz) {
					public void rollback() { chitiexz = _xdb_saved; }
				};}});
		chitiexz = _v_;
	}

	@Override
	public void setJyjiejing(int _v_) { // 经验结晶
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "jyjiejing") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, jyjiejing) {
					public void rollback() { jyjiejing = _xdb_saved; }
				};}});
		jyjiejing = _v_;
	}

	@Override
	public void setPvpti(int _v_) { // PVP精力
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "pvpti") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, pvpti) {
					public void rollback() { pvpti = _xdb_saved; }
				};}});
		pvpti = _v_;
	}

	@Override
	public void setPvptitime(long _v_) { // PVP精力更新时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "pvptitime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, pvptitime) {
					public void rollback() { pvptitime = _xdb_saved; }
				};}});
		pvptitime = _v_;
	}

	@Override
	public void setTanxianti(int _v_) { // 探险行动力
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "tanxianti") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, tanxianti) {
					public void rollback() { tanxianti = _xdb_saved; }
				};}});
		tanxianti = _v_;
	}

	@Override
	public void setTanxiantitime(long _v_) { // 探险行动力更新时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "tanxiantitime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, tanxiantitime) {
					public void rollback() { tanxiantitime = _xdb_saved; }
				};}});
		tanxiantitime = _v_;
	}

	@Override
	public void setJinengdian(int _v_) { // 技能点
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "jinengdian") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, jinengdian) {
					public void rollback() { jinengdian = _xdb_saved; }
				};}});
		jinengdian = _v_;
	}

	@Override
	public void setJinengdiantime(long _v_) { // 技能点更新时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "jinengdiantime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, jinengdiantime) {
					public void rollback() { jinengdiantime = _xdb_saved; }
				};}});
		jinengdiantime = _v_;
	}

	@Override
	public void setSmzhangjie(int _v_) { // 神秘关卡或商店的所属章节记录
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "smzhangjie") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, smzhangjie) {
					public void rollback() { smzhangjie = _xdb_saved; }
				};}});
		smzhangjie = _v_;
	}

	@Override
	public void setBattlenum(int _v_) { // 神秘关卡或商店记录
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "battlenum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, battlenum) {
					public void rollback() { battlenum = _xdb_saved; }
				};}});
		battlenum = _v_;
	}

	@Override
	public void setSmendtime(long _v_) { // 神秘关卡或商店结束时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "smendtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, smendtime) {
					public void rollback() { smendtime = _xdb_saved; }
				};}});
		smendtime = _v_;
	}

	@Override
	public void setSmguanka_nocome(int _v_) { // 神秘关卡未出现次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "smguanka_nocome") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, smguanka_nocome) {
					public void rollback() { smguanka_nocome = _xdb_saved; }
				};}});
		smguanka_nocome = _v_;
	}

	@Override
	public void setSmshop_notcome(int _v_) { // 神秘商店未出现次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "smshop_notcome") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, smshop_notcome) {
					public void rollback() { smshop_notcome = _xdb_saved; }
				};}});
		smshop_notcome = _v_;
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
	public void setOnlinetime(long _v_) { // 上线时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "onlinetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, onlinetime) {
					public void rollback() { onlinetime = _xdb_saved; }
				};}});
		onlinetime = _v_;
	}

	@Override
	public void setOfflinetime(long _v_) { // 下线时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "offlinetime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, offlinetime) {
					public void rollback() { offlinetime = _xdb_saved; }
				};}});
		offlinetime = _v_;
	}

	@Override
	public void setTibuynum(int _v_) { // 体力购买次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "tibuynum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, tibuynum) {
					public void rollback() { tibuynum = _xdb_saved; }
				};}});
		tibuynum = _v_;
	}

	@Override
	public void setTibuytime(long _v_) { // 上次记录的体力购买时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "tibuytime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, tibuytime) {
					public void rollback() { tibuytime = _xdb_saved; }
				};}});
		tibuytime = _v_;
	}

	@Override
	public void setGoldbuynum(int _v_) { // 金币购买次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "goldbuynum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, goldbuynum) {
					public void rollback() { goldbuynum = _xdb_saved; }
				};}});
		goldbuynum = _v_;
	}

	@Override
	public void setGoldbuytime(long _v_) { // 上次记录的金币购买时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "goldbuytime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, goldbuytime) {
					public void rollback() { goldbuytime = _xdb_saved; }
				};}});
		goldbuytime = _v_;
	}

	@Override
	public void setSignnum7(int _v_) { // 连续签到ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "signnum7") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, signnum7) {
					public void rollback() { signnum7 = _xdb_saved; }
				};}});
		signnum7 = _v_;
	}

	@Override
	public void setSignnum28(int _v_) { // 累计签到ID
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "signnum28") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, signnum28) {
					public void rollback() { signnum28 = _xdb_saved; }
				};}});
		signnum28 = _v_;
	}

	@Override
	public void setSigntime(long _v_) { // 最后签到时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "signtime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, signtime) {
					public void rollback() { signtime = _xdb_saved; }
				};}});
		signtime = _v_;
	}

	@Override
	public void setQiyuannum(int _v_) { // 累计祈愿台次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "qiyuannum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, qiyuannum) {
					public void rollback() { qiyuannum = _xdb_saved; }
				};}});
		qiyuannum = _v_;
	}

	@Override
	public void setQiyuantime(long _v_) { // 最后祈愿时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "qiyuantime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, qiyuantime) {
					public void rollback() { qiyuantime = _xdb_saved; }
				};}});
		qiyuantime = _v_;
	}

	@Override
	public void setQiyuanallnum(int _v_) { // 祈愿回合次数，第一次为3，完成后均为5
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "qiyuanallnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, qiyuanallnum) {
					public void rollback() { qiyuanallnum = _xdb_saved; }
				};}});
		qiyuanallnum = _v_;
	}

	@Override
	public void setBuybagnum(short _v_) { // 扩充背包次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "buybagnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogShort(this, buybagnum) {
					public void rollback() { buybagnum = _xdb_saved; }
				};}});
		buybagnum = _v_;
	}

	@Override
	public void setBuyherobagnum(short _v_) { // 扩充英雄背包次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "buyherobagnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogShort(this, buyherobagnum) {
					public void rollback() { buyherobagnum = _xdb_saved; }
				};}});
		buyherobagnum = _v_;
	}

	@Override
	public void setTroopnum(short _v_) { // 默认编队号
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "troopnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogShort(this, troopnum) {
					public void rollback() { troopnum = _xdb_saved; }
				};}});
		troopnum = _v_;
	}

	@Override
	public void setSweepnum(int _v_) { // 今日扫荡次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "sweepnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, sweepnum) {
					public void rollback() { sweepnum = _xdb_saved; }
				};}});
		sweepnum = _v_;
	}

	@Override
	public void setTodaylasttime(long _v_) { // 今日计时时间
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "todaylasttime") {
			protected xdb.Log create() {
				return new xdb.logs.LogLong(this, todaylasttime) {
					public void rollback() { todaylasttime = _xdb_saved; }
				};}});
		todaylasttime = _v_;
	}

	@Override
	public void setSweepbuynum(int _v_) { // 今日扫荡购买次数
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "sweepbuynum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, sweepbuynum) {
					public void rollback() { sweepbuynum = _xdb_saved; }
				};}});
		sweepbuynum = _v_;
	}

	@Override
	public void setMszqgetnum(int _v_) { // 缪斯奏曲：个位为中午，十位为晚上
		_xdb_verify_unsafe_();
		xdb.Logs.logIf(new xdb.LogKey(this, "mszqgetnum") {
			protected xdb.Log create() {
				return new xdb.logs.LogInt(this, mszqgetnum) {
					public void rollback() { mszqgetnum = _xdb_saved; }
				};}});
		mszqgetnum = _v_;
	}

	@Override
	public final boolean equals(Object _o1_) {
		_xdb_verify_unsafe_();
		Properties _o_ = null;
		if ( _o1_ instanceof Properties ) _o_ = (Properties)_o1_;
		else if ( _o1_ instanceof Properties.Const ) _o_ = ((Properties.Const)_o1_).nThis();
		else return false;
		if (!rolename.equals(_o_.rolename)) return false;
		if (userid != _o_.userid) return false;
		if (!username.equals(_o_.username)) return false;
		if (!plattypestr.equals(_o_.plattypestr)) return false;
		if (!mac.equals(_o_.mac)) return false;
		if (ostype != _o_.ostype) return false;
		if (level != _o_.level) return false;
		if (exp != _o_.exp) return false;
		if (viplv != _o_.viplv) return false;
		if (vipexp != _o_.vipexp) return false;
		if (ti != _o_.ti) return false;
		if (tichangetime != _o_.tichangetime) return false;
		if (gold != _o_.gold) return false;
		if (yuanbao != _o_.yuanbao) return false;
		if (shenglingzq != _o_.shenglingzq) return false;
		if (ronglian != _o_.ronglian) return false;
		if (huangjinxz != _o_.huangjinxz) return false;
		if (baijinxz != _o_.baijinxz) return false;
		if (qingtongxz != _o_.qingtongxz) return false;
		if (chitiexz != _o_.chitiexz) return false;
		if (jyjiejing != _o_.jyjiejing) return false;
		if (pvpti != _o_.pvpti) return false;
		if (pvptitime != _o_.pvptitime) return false;
		if (tanxianti != _o_.tanxianti) return false;
		if (tanxiantitime != _o_.tanxiantitime) return false;
		if (jinengdian != _o_.jinengdian) return false;
		if (jinengdiantime != _o_.jinengdiantime) return false;
		if (!moheshop.equals(_o_.moheshop)) return false;
		if (smzhangjie != _o_.smzhangjie) return false;
		if (battlenum != _o_.battlenum) return false;
		if (smendtime != _o_.smendtime) return false;
		if (!smshop.equals(_o_.smshop)) return false;
		if (smguanka_nocome != _o_.smguanka_nocome) return false;
		if (smshop_notcome != _o_.smshop_notcome) return false;
		if (createtime != _o_.createtime) return false;
		if (onlinetime != _o_.onlinetime) return false;
		if (offlinetime != _o_.offlinetime) return false;
		if (tibuynum != _o_.tibuynum) return false;
		if (tibuytime != _o_.tibuytime) return false;
		if (goldbuynum != _o_.goldbuynum) return false;
		if (goldbuytime != _o_.goldbuytime) return false;
		if (signnum7 != _o_.signnum7) return false;
		if (signnum28 != _o_.signnum28) return false;
		if (signtime != _o_.signtime) return false;
		if (qiyuannum != _o_.qiyuannum) return false;
		if (qiyuantime != _o_.qiyuantime) return false;
		if (qiyuanallnum != _o_.qiyuanallnum) return false;
		if (buybagnum != _o_.buybagnum) return false;
		if (buyherobagnum != _o_.buyherobagnum) return false;
		if (troopnum != _o_.troopnum) return false;
		if (sweepnum != _o_.sweepnum) return false;
		if (todaylasttime != _o_.todaylasttime) return false;
		if (sweepbuynum != _o_.sweepbuynum) return false;
		if (mszqgetnum != _o_.mszqgetnum) return false;
		if (!newyindao.equals(_o_.newyindao)) return false;
		return true;
	}

	@Override
	public final int hashCode() {
		_xdb_verify_unsafe_();
		int _h_ = 0;
		_h_ += rolename.hashCode();
		_h_ += userid;
		_h_ += username.hashCode();
		_h_ += plattypestr.hashCode();
		_h_ += mac.hashCode();
		_h_ += ostype;
		_h_ += level;
		_h_ += exp;
		_h_ += viplv;
		_h_ += vipexp;
		_h_ += ti;
		_h_ += tichangetime;
		_h_ += gold;
		_h_ += yuanbao;
		_h_ += shenglingzq;
		_h_ += ronglian;
		_h_ += huangjinxz;
		_h_ += baijinxz;
		_h_ += qingtongxz;
		_h_ += chitiexz;
		_h_ += jyjiejing;
		_h_ += pvpti;
		_h_ += pvptitime;
		_h_ += tanxianti;
		_h_ += tanxiantitime;
		_h_ += jinengdian;
		_h_ += jinengdiantime;
		_h_ += moheshop.hashCode();
		_h_ += smzhangjie;
		_h_ += battlenum;
		_h_ += smendtime;
		_h_ += smshop.hashCode();
		_h_ += smguanka_nocome;
		_h_ += smshop_notcome;
		_h_ += createtime;
		_h_ += onlinetime;
		_h_ += offlinetime;
		_h_ += tibuynum;
		_h_ += tibuytime;
		_h_ += goldbuynum;
		_h_ += goldbuytime;
		_h_ += signnum7;
		_h_ += signnum28;
		_h_ += signtime;
		_h_ += qiyuannum;
		_h_ += qiyuantime;
		_h_ += qiyuanallnum;
		_h_ += buybagnum;
		_h_ += buyherobagnum;
		_h_ += troopnum;
		_h_ += sweepnum;
		_h_ += todaylasttime;
		_h_ += sweepbuynum;
		_h_ += mszqgetnum;
		_h_ += newyindao.hashCode();
		return _h_;
	}

	@Override
	public String toString() {
		_xdb_verify_unsafe_();
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append("'").append(rolename).append("'");
		_sb_.append(",");
		_sb_.append(userid);
		_sb_.append(",");
		_sb_.append("'").append(username).append("'");
		_sb_.append(",");
		_sb_.append("'").append(plattypestr).append("'");
		_sb_.append(",");
		_sb_.append("'").append(mac).append("'");
		_sb_.append(",");
		_sb_.append(ostype);
		_sb_.append(",");
		_sb_.append(level);
		_sb_.append(",");
		_sb_.append(exp);
		_sb_.append(",");
		_sb_.append(viplv);
		_sb_.append(",");
		_sb_.append(vipexp);
		_sb_.append(",");
		_sb_.append(ti);
		_sb_.append(",");
		_sb_.append(tichangetime);
		_sb_.append(",");
		_sb_.append(gold);
		_sb_.append(",");
		_sb_.append(yuanbao);
		_sb_.append(",");
		_sb_.append(shenglingzq);
		_sb_.append(",");
		_sb_.append(ronglian);
		_sb_.append(",");
		_sb_.append(huangjinxz);
		_sb_.append(",");
		_sb_.append(baijinxz);
		_sb_.append(",");
		_sb_.append(qingtongxz);
		_sb_.append(",");
		_sb_.append(chitiexz);
		_sb_.append(",");
		_sb_.append(jyjiejing);
		_sb_.append(",");
		_sb_.append(pvpti);
		_sb_.append(",");
		_sb_.append(pvptitime);
		_sb_.append(",");
		_sb_.append(tanxianti);
		_sb_.append(",");
		_sb_.append(tanxiantitime);
		_sb_.append(",");
		_sb_.append(jinengdian);
		_sb_.append(",");
		_sb_.append(jinengdiantime);
		_sb_.append(",");
		_sb_.append(moheshop);
		_sb_.append(",");
		_sb_.append(smzhangjie);
		_sb_.append(",");
		_sb_.append(battlenum);
		_sb_.append(",");
		_sb_.append(smendtime);
		_sb_.append(",");
		_sb_.append(smshop);
		_sb_.append(",");
		_sb_.append(smguanka_nocome);
		_sb_.append(",");
		_sb_.append(smshop_notcome);
		_sb_.append(",");
		_sb_.append(createtime);
		_sb_.append(",");
		_sb_.append(onlinetime);
		_sb_.append(",");
		_sb_.append(offlinetime);
		_sb_.append(",");
		_sb_.append(tibuynum);
		_sb_.append(",");
		_sb_.append(tibuytime);
		_sb_.append(",");
		_sb_.append(goldbuynum);
		_sb_.append(",");
		_sb_.append(goldbuytime);
		_sb_.append(",");
		_sb_.append(signnum7);
		_sb_.append(",");
		_sb_.append(signnum28);
		_sb_.append(",");
		_sb_.append(signtime);
		_sb_.append(",");
		_sb_.append(qiyuannum);
		_sb_.append(",");
		_sb_.append(qiyuantime);
		_sb_.append(",");
		_sb_.append(qiyuanallnum);
		_sb_.append(",");
		_sb_.append(buybagnum);
		_sb_.append(",");
		_sb_.append(buyherobagnum);
		_sb_.append(",");
		_sb_.append(troopnum);
		_sb_.append(",");
		_sb_.append(sweepnum);
		_sb_.append(",");
		_sb_.append(todaylasttime);
		_sb_.append(",");
		_sb_.append(sweepbuynum);
		_sb_.append(",");
		_sb_.append(mszqgetnum);
		_sb_.append(",");
		_sb_.append(newyindao);
		_sb_.append(")");
		return _sb_.toString();
	}

	@Override
	public xdb.logs.Listenable newListenable() {
		xdb.logs.ListenableBean lb = new xdb.logs.ListenableBean();
		lb.add(new xdb.logs.ListenableChanged().setVarName("rolename"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("userid"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("username"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("plattypestr"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("mac"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("ostype"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("level"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("exp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("viplv"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("vipexp"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("ti"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("tichangetime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("gold"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("yuanbao"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("shenglingzq"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("ronglian"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("huangjinxz"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("baijinxz"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("qingtongxz"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("chitiexz"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("jyjiejing"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("pvpti"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("pvptitime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("tanxianti"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("tanxiantitime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("jinengdian"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("jinengdiantime"));
		lb.add(new xdb.logs.ListenableMap().setVarName("moheshop"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("smzhangjie"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("battlenum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("smendtime"));
		lb.add(new xdb.logs.ListenableMap().setVarName("smshop"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("smguanka_nocome"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("smshop_notcome"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("createtime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("onlinetime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("offlinetime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("tibuynum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("tibuytime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("goldbuynum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("goldbuytime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("signnum7"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("signnum28"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("signtime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("qiyuannum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("qiyuantime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("qiyuanallnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("buybagnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("buyherobagnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("troopnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sweepnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("todaylasttime"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("sweepbuynum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("mszqgetnum"));
		lb.add(new xdb.logs.ListenableChanged().setVarName("newyindao"));
		return lb;
	}

	private class Const implements xbean.Properties {
		Properties nThis() {
			return Properties.this;
		}

		@Override
		public xbean.Properties copy() {
			return Properties.this.copy();
		}

		@Override
		public xbean.Properties toData() {
			return Properties.this.toData();
		}

		public xbean.Properties toBean() {
			return Properties.this.toBean();
		}

		@Override
		public xbean.Properties toDataIf() {
			return Properties.this.toDataIf();
		}

		public xbean.Properties toBeanIf() {
			return Properties.this.toBeanIf();
		}

		@Override
		public String getRolename() { // 角色名
			_xdb_verify_unsafe_();
			return rolename;
		}

		@Override
		public com.goldhuman.Common.Octets getRolenameOctets() { // 角色名
			_xdb_verify_unsafe_();
			return Properties.this.getRolenameOctets();
		}

		@Override
		public int getUserid() { // 所属角色id
			_xdb_verify_unsafe_();
			return userid;
		}

		@Override
		public String getUsername() { // 上次登录的账号名称
			_xdb_verify_unsafe_();
			return username;
		}

		@Override
		public com.goldhuman.Common.Octets getUsernameOctets() { // 上次登录的账号名称
			_xdb_verify_unsafe_();
			return Properties.this.getUsernameOctets();
		}

		@Override
		public String getPlattypestr() { // 上次登录的平台string
			_xdb_verify_unsafe_();
			return plattypestr;
		}

		@Override
		public com.goldhuman.Common.Octets getPlattypestrOctets() { // 上次登录的平台string
			_xdb_verify_unsafe_();
			return Properties.this.getPlattypestrOctets();
		}

		@Override
		public String getMac() { // 上次登录的MAC地址
			_xdb_verify_unsafe_();
			return mac;
		}

		@Override
		public com.goldhuman.Common.Octets getMacOctets() { // 上次登录的MAC地址
			_xdb_verify_unsafe_();
			return Properties.this.getMacOctets();
		}

		@Override
		public int getOstype() { // 系统类型
			_xdb_verify_unsafe_();
			return ostype;
		}

		@Override
		public int getLevel() { // 等级
			_xdb_verify_unsafe_();
			return level;
		}

		@Override
		public int getExp() { // 经验
			_xdb_verify_unsafe_();
			return exp;
		}

		@Override
		public int getViplv() { // vip等级
			_xdb_verify_unsafe_();
			return viplv;
		}

		@Override
		public int getVipexp() { // vip经验
			_xdb_verify_unsafe_();
			return vipexp;
		}

		@Override
		public int getTi() { // 体力
			_xdb_verify_unsafe_();
			return ti;
		}

		@Override
		public long getTichangetime() { // 体力更新时间
			_xdb_verify_unsafe_();
			return tichangetime;
		}

		@Override
		public int getGold() { // 金币
			_xdb_verify_unsafe_();
			return gold;
		}

		@Override
		public int getYuanbao() { // 元宝（水晶）
			_xdb_verify_unsafe_();
			return yuanbao;
		}

		@Override
		public int getShenglingzq() { // 圣灵之泉
			_xdb_verify_unsafe_();
			return shenglingzq;
		}

		@Override
		public int getRonglian() { // 熔炼点
			_xdb_verify_unsafe_();
			return ronglian;
		}

		@Override
		public int getHuangjinxz() { // 黄金勋章
			_xdb_verify_unsafe_();
			return huangjinxz;
		}

		@Override
		public int getBaijinxz() { // 白金勋章
			_xdb_verify_unsafe_();
			return baijinxz;
		}

		@Override
		public int getQingtongxz() { // 青铜勋章
			_xdb_verify_unsafe_();
			return qingtongxz;
		}

		@Override
		public int getChitiexz() { // 赤铁勋章
			_xdb_verify_unsafe_();
			return chitiexz;
		}

		@Override
		public int getJyjiejing() { // 经验结晶
			_xdb_verify_unsafe_();
			return jyjiejing;
		}

		@Override
		public int getPvpti() { // PVP精力
			_xdb_verify_unsafe_();
			return pvpti;
		}

		@Override
		public long getPvptitime() { // PVP精力更新时间
			_xdb_verify_unsafe_();
			return pvptitime;
		}

		@Override
		public int getTanxianti() { // 探险行动力
			_xdb_verify_unsafe_();
			return tanxianti;
		}

		@Override
		public long getTanxiantitime() { // 探险行动力更新时间
			_xdb_verify_unsafe_();
			return tanxiantitime;
		}

		@Override
		public int getJinengdian() { // 技能点
			_xdb_verify_unsafe_();
			return jinengdian;
		}

		@Override
		public long getJinengdiantime() { // 技能点更新时间
			_xdb_verify_unsafe_();
			return jinengdiantime;
		}

		@Override
		public java.util.Map<Integer, xbean.mohe> getMoheshop() { // 魔盒列表
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(moheshop);
		}

		@Override
		public java.util.Map<Integer, xbean.mohe> getMoheshopAsData() { // 魔盒列表
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.mohe> moheshop;
			Properties _o_ = Properties.this;
			moheshop = new java.util.HashMap<Integer, xbean.mohe>();
			for (java.util.Map.Entry<Integer, xbean.mohe> _e_ : _o_.moheshop.entrySet())
				moheshop.put(_e_.getKey(), new mohe.Data(_e_.getValue()));
			return moheshop;
		}

		@Override
		public int getSmzhangjie() { // 神秘关卡或商店的所属章节记录
			_xdb_verify_unsafe_();
			return smzhangjie;
		}

		@Override
		public int getBattlenum() { // 神秘关卡或商店记录
			_xdb_verify_unsafe_();
			return battlenum;
		}

		@Override
		public long getSmendtime() { // 神秘关卡或商店结束时间
			_xdb_verify_unsafe_();
			return smendtime;
		}

		@Override
		public java.util.Map<Integer, xbean.smshopdata> getSmshop() { // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
			_xdb_verify_unsafe_();
			return xdb.Consts.constMap(smshop);
		}

		@Override
		public java.util.Map<Integer, xbean.smshopdata> getSmshopAsData() { // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
			_xdb_verify_unsafe_();
			java.util.Map<Integer, xbean.smshopdata> smshop;
			Properties _o_ = Properties.this;
			smshop = new java.util.HashMap<Integer, xbean.smshopdata>();
			for (java.util.Map.Entry<Integer, xbean.smshopdata> _e_ : _o_.smshop.entrySet())
				smshop.put(_e_.getKey(), new smshopdata.Data(_e_.getValue()));
			return smshop;
		}

		@Override
		public int getSmguanka_nocome() { // 神秘关卡未出现次数
			_xdb_verify_unsafe_();
			return smguanka_nocome;
		}

		@Override
		public int getSmshop_notcome() { // 神秘商店未出现次数
			_xdb_verify_unsafe_();
			return smshop_notcome;
		}

		@Override
		public long getCreatetime() { // 创建时间
			_xdb_verify_unsafe_();
			return createtime;
		}

		@Override
		public long getOnlinetime() { // 上线时间
			_xdb_verify_unsafe_();
			return onlinetime;
		}

		@Override
		public long getOfflinetime() { // 下线时间
			_xdb_verify_unsafe_();
			return offlinetime;
		}

		@Override
		public int getTibuynum() { // 体力购买次数
			_xdb_verify_unsafe_();
			return tibuynum;
		}

		@Override
		public long getTibuytime() { // 上次记录的体力购买时间
			_xdb_verify_unsafe_();
			return tibuytime;
		}

		@Override
		public int getGoldbuynum() { // 金币购买次数
			_xdb_verify_unsafe_();
			return goldbuynum;
		}

		@Override
		public long getGoldbuytime() { // 上次记录的金币购买时间
			_xdb_verify_unsafe_();
			return goldbuytime;
		}

		@Override
		public int getSignnum7() { // 连续签到ID
			_xdb_verify_unsafe_();
			return signnum7;
		}

		@Override
		public int getSignnum28() { // 累计签到ID
			_xdb_verify_unsafe_();
			return signnum28;
		}

		@Override
		public long getSigntime() { // 最后签到时间
			_xdb_verify_unsafe_();
			return signtime;
		}

		@Override
		public int getQiyuannum() { // 累计祈愿台次数
			_xdb_verify_unsafe_();
			return qiyuannum;
		}

		@Override
		public long getQiyuantime() { // 最后祈愿时间
			_xdb_verify_unsafe_();
			return qiyuantime;
		}

		@Override
		public int getQiyuanallnum() { // 祈愿回合次数，第一次为3，完成后均为5
			_xdb_verify_unsafe_();
			return qiyuanallnum;
		}

		@Override
		public short getBuybagnum() { // 扩充背包次数
			_xdb_verify_unsafe_();
			return buybagnum;
		}

		@Override
		public short getBuyherobagnum() { // 扩充英雄背包次数
			_xdb_verify_unsafe_();
			return buyherobagnum;
		}

		@Override
		public short getTroopnum() { // 默认编队号
			_xdb_verify_unsafe_();
			return troopnum;
		}

		@Override
		public int getSweepnum() { // 今日扫荡次数
			_xdb_verify_unsafe_();
			return sweepnum;
		}

		@Override
		public long getTodaylasttime() { // 今日计时时间
			_xdb_verify_unsafe_();
			return todaylasttime;
		}

		@Override
		public int getSweepbuynum() { // 今日扫荡购买次数
			_xdb_verify_unsafe_();
			return sweepbuynum;
		}

		@Override
		public int getMszqgetnum() { // 缪斯奏曲：个位为中午，十位为晚上
			_xdb_verify_unsafe_();
			return mszqgetnum;
		}

		@Override
		public java.util.List<Integer> getNewyindao() { // 新手引导
			_xdb_verify_unsafe_();
			return xdb.Consts.constList(newyindao);
		}

		public java.util.List<Integer> getNewyindaoAsData() { // 新手引导
			_xdb_verify_unsafe_();
			java.util.List<Integer> newyindao;
			Properties _o_ = Properties.this;
		newyindao = new java.util.LinkedList<Integer>();
		newyindao.addAll(_o_.newyindao);
			return newyindao;
		}

		@Override
		public void setRolename(String _v_) { // 角色名
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRolenameOctets(com.goldhuman.Common.Octets _v_) { // 角色名
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setUserid(int _v_) { // 所属角色id
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setUsername(String _v_) { // 上次登录的账号名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setUsernameOctets(com.goldhuman.Common.Octets _v_) { // 上次登录的账号名称
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPlattypestr(String _v_) { // 上次登录的平台string
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPlattypestrOctets(com.goldhuman.Common.Octets _v_) { // 上次登录的平台string
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMac(String _v_) { // 上次登录的MAC地址
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMacOctets(com.goldhuman.Common.Octets _v_) { // 上次登录的MAC地址
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setOstype(int _v_) { // 系统类型
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setLevel(int _v_) { // 等级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setExp(int _v_) { // 经验
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setViplv(int _v_) { // vip等级
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setVipexp(int _v_) { // vip经验
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTi(int _v_) { // 体力
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTichangetime(long _v_) { // 体力更新时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGold(int _v_) { // 金币
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setYuanbao(int _v_) { // 元宝（水晶）
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setShenglingzq(int _v_) { // 圣灵之泉
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setRonglian(int _v_) { // 熔炼点
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setHuangjinxz(int _v_) { // 黄金勋章
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBaijinxz(int _v_) { // 白金勋章
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setQingtongxz(int _v_) { // 青铜勋章
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setChitiexz(int _v_) { // 赤铁勋章
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setJyjiejing(int _v_) { // 经验结晶
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPvpti(int _v_) { // PVP精力
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setPvptitime(long _v_) { // PVP精力更新时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTanxianti(int _v_) { // 探险行动力
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTanxiantitime(long _v_) { // 探险行动力更新时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setJinengdian(int _v_) { // 技能点
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setJinengdiantime(long _v_) { // 技能点更新时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSmzhangjie(int _v_) { // 神秘关卡或商店的所属章节记录
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBattlenum(int _v_) { // 神秘关卡或商店记录
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSmendtime(long _v_) { // 神秘关卡或商店结束时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSmguanka_nocome(int _v_) { // 神秘关卡未出现次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSmshop_notcome(int _v_) { // 神秘商店未出现次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setCreatetime(long _v_) { // 创建时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setOnlinetime(long _v_) { // 上线时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setOfflinetime(long _v_) { // 下线时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTibuynum(int _v_) { // 体力购买次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTibuytime(long _v_) { // 上次记录的体力购买时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGoldbuynum(int _v_) { // 金币购买次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setGoldbuytime(long _v_) { // 上次记录的金币购买时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSignnum7(int _v_) { // 连续签到ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSignnum28(int _v_) { // 累计签到ID
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSigntime(long _v_) { // 最后签到时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setQiyuannum(int _v_) { // 累计祈愿台次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setQiyuantime(long _v_) { // 最后祈愿时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setQiyuanallnum(int _v_) { // 祈愿回合次数，第一次为3，完成后均为5
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBuybagnum(short _v_) { // 扩充背包次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setBuyherobagnum(short _v_) { // 扩充英雄背包次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTroopnum(short _v_) { // 默认编队号
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSweepnum(int _v_) { // 今日扫荡次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setTodaylasttime(long _v_) { // 今日计时时间
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setSweepbuynum(int _v_) { // 今日扫荡购买次数
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public void setMszqgetnum(int _v_) { // 缪斯奏曲：个位为中午，十位为晚上
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
			return Properties.this.isData();
		}

		@Override
		public OctetsStream marshal(OctetsStream _os_) {
			return Properties.this.marshal(_os_);
		}

		@Override
		public OctetsStream unmarshal(OctetsStream arg0) throws MarshalException {
			_xdb_verify_unsafe_();
			throw new UnsupportedOperationException();
		}

		@Override
		public xdb.Bean xdbParent() {
			return Properties.this.xdbParent();
		}

		@Override
		public boolean xdbManaged() {
			return Properties.this.xdbManaged();
		}

		@Override
		public String xdbVarname() {
			return Properties.this.xdbVarname();
		}

		@Override
		public Long xdbObjId() {
			return Properties.this.xdbObjId();
		}

		@Override
		public boolean equals(Object obj) {
			return Properties.this.equals(obj);
		}

		@Override
		public int hashCode() {
			return Properties.this.hashCode();
		}

		@Override
		public String toString() {
			return Properties.this.toString();
		}

	}

	public static final class Data implements xbean.Properties {
		private String rolename; // 角色名
		private int userid; // 所属角色id
		private String username; // 上次登录的账号名称
		private String plattypestr; // 上次登录的平台string
		private String mac; // 上次登录的MAC地址
		private int ostype; // 系统类型
		private int level; // 等级
		private int exp; // 经验
		private int viplv; // vip等级
		private int vipexp; // vip经验
		private int ti; // 体力
		private long tichangetime; // 体力更新时间
		private int gold; // 金币
		private int yuanbao; // 元宝（水晶）
		private int shenglingzq; // 圣灵之泉
		private int ronglian; // 熔炼点
		private int huangjinxz; // 黄金勋章
		private int baijinxz; // 白金勋章
		private int qingtongxz; // 青铜勋章
		private int chitiexz; // 赤铁勋章
		private int jyjiejing; // 经验结晶
		private int pvpti; // PVP精力
		private long pvptitime; // PVP精力更新时间
		private int tanxianti; // 探险行动力
		private long tanxiantitime; // 探险行动力更新时间
		private int jinengdian; // 技能点
		private long jinengdiantime; // 技能点更新时间
		private java.util.HashMap<Integer, xbean.mohe> moheshop; // 魔盒列表
		private int smzhangjie; // 神秘关卡或商店的所属章节记录
		private int battlenum; // 神秘关卡或商店记录
		private long smendtime; // 神秘关卡或商店结束时间
		private java.util.HashMap<Integer, xbean.smshopdata> smshop; // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
		private int smguanka_nocome; // 神秘关卡未出现次数
		private int smshop_notcome; // 神秘商店未出现次数
		private long createtime; // 创建时间
		private long onlinetime; // 上线时间
		private long offlinetime; // 下线时间
		private int tibuynum; // 体力购买次数
		private long tibuytime; // 上次记录的体力购买时间
		private int goldbuynum; // 金币购买次数
		private long goldbuytime; // 上次记录的金币购买时间
		private int signnum7; // 连续签到ID
		private int signnum28; // 累计签到ID
		private long signtime; // 最后签到时间
		private int qiyuannum; // 累计祈愿台次数
		private long qiyuantime; // 最后祈愿时间
		private int qiyuanallnum; // 祈愿回合次数，第一次为3，完成后均为5
		private short buybagnum; // 扩充背包次数
		private short buyherobagnum; // 扩充英雄背包次数
		private short troopnum; // 默认编队号
		private int sweepnum; // 今日扫荡次数
		private long todaylasttime; // 今日计时时间
		private int sweepbuynum; // 今日扫荡购买次数
		private int mszqgetnum; // 缪斯奏曲：个位为中午，十位为晚上
		private java.util.LinkedList<Integer> newyindao; // 新手引导

		public Data() {
			rolename = "";
			username = "";
			plattypestr = "";
			mac = "";
			level = 1;
			viplv = 1;
			moheshop = new java.util.HashMap<Integer, xbean.mohe>();
			smshop = new java.util.HashMap<Integer, xbean.smshopdata>();
			tibuynum = 0;
			tibuytime = 0;
			goldbuynum = 0;
			goldbuytime = 0;
			signnum7 = 0;
			signnum28 = 0;
			signtime = 0;
			qiyuannum = 0;
			qiyuantime = 0;
			qiyuanallnum = 3;
			buybagnum = 0;
			buyherobagnum = 0;
			troopnum = 1;
			sweepnum = 0;
			todaylasttime = 0;
			sweepbuynum = 0;
			mszqgetnum = 0;
			newyindao = new java.util.LinkedList<Integer>();
		}

		Data(xbean.Properties _o1_) {
			if (_o1_ instanceof Properties) assign((Properties)_o1_);
			else if (_o1_ instanceof Properties.Data) assign((Properties.Data)_o1_);
			else if (_o1_ instanceof Properties.Const) assign(((Properties.Const)_o1_).nThis());
			else throw new UnsupportedOperationException();
		}

		private void assign(Properties _o_) {
			rolename = _o_.rolename;
			userid = _o_.userid;
			username = _o_.username;
			plattypestr = _o_.plattypestr;
			mac = _o_.mac;
			ostype = _o_.ostype;
			level = _o_.level;
			exp = _o_.exp;
			viplv = _o_.viplv;
			vipexp = _o_.vipexp;
			ti = _o_.ti;
			tichangetime = _o_.tichangetime;
			gold = _o_.gold;
			yuanbao = _o_.yuanbao;
			shenglingzq = _o_.shenglingzq;
			ronglian = _o_.ronglian;
			huangjinxz = _o_.huangjinxz;
			baijinxz = _o_.baijinxz;
			qingtongxz = _o_.qingtongxz;
			chitiexz = _o_.chitiexz;
			jyjiejing = _o_.jyjiejing;
			pvpti = _o_.pvpti;
			pvptitime = _o_.pvptitime;
			tanxianti = _o_.tanxianti;
			tanxiantitime = _o_.tanxiantitime;
			jinengdian = _o_.jinengdian;
			jinengdiantime = _o_.jinengdiantime;
			moheshop = new java.util.HashMap<Integer, xbean.mohe>();
			for (java.util.Map.Entry<Integer, xbean.mohe> _e_ : _o_.moheshop.entrySet())
				moheshop.put(_e_.getKey(), new mohe.Data(_e_.getValue()));
			smzhangjie = _o_.smzhangjie;
			battlenum = _o_.battlenum;
			smendtime = _o_.smendtime;
			smshop = new java.util.HashMap<Integer, xbean.smshopdata>();
			for (java.util.Map.Entry<Integer, xbean.smshopdata> _e_ : _o_.smshop.entrySet())
				smshop.put(_e_.getKey(), new smshopdata.Data(_e_.getValue()));
			smguanka_nocome = _o_.smguanka_nocome;
			smshop_notcome = _o_.smshop_notcome;
			createtime = _o_.createtime;
			onlinetime = _o_.onlinetime;
			offlinetime = _o_.offlinetime;
			tibuynum = _o_.tibuynum;
			tibuytime = _o_.tibuytime;
			goldbuynum = _o_.goldbuynum;
			goldbuytime = _o_.goldbuytime;
			signnum7 = _o_.signnum7;
			signnum28 = _o_.signnum28;
			signtime = _o_.signtime;
			qiyuannum = _o_.qiyuannum;
			qiyuantime = _o_.qiyuantime;
			qiyuanallnum = _o_.qiyuanallnum;
			buybagnum = _o_.buybagnum;
			buyherobagnum = _o_.buyherobagnum;
			troopnum = _o_.troopnum;
			sweepnum = _o_.sweepnum;
			todaylasttime = _o_.todaylasttime;
			sweepbuynum = _o_.sweepbuynum;
			mszqgetnum = _o_.mszqgetnum;
			newyindao = new java.util.LinkedList<Integer>();
			newyindao.addAll(_o_.newyindao);
		}

		private void assign(Properties.Data _o_) {
			rolename = _o_.rolename;
			userid = _o_.userid;
			username = _o_.username;
			plattypestr = _o_.plattypestr;
			mac = _o_.mac;
			ostype = _o_.ostype;
			level = _o_.level;
			exp = _o_.exp;
			viplv = _o_.viplv;
			vipexp = _o_.vipexp;
			ti = _o_.ti;
			tichangetime = _o_.tichangetime;
			gold = _o_.gold;
			yuanbao = _o_.yuanbao;
			shenglingzq = _o_.shenglingzq;
			ronglian = _o_.ronglian;
			huangjinxz = _o_.huangjinxz;
			baijinxz = _o_.baijinxz;
			qingtongxz = _o_.qingtongxz;
			chitiexz = _o_.chitiexz;
			jyjiejing = _o_.jyjiejing;
			pvpti = _o_.pvpti;
			pvptitime = _o_.pvptitime;
			tanxianti = _o_.tanxianti;
			tanxiantitime = _o_.tanxiantitime;
			jinengdian = _o_.jinengdian;
			jinengdiantime = _o_.jinengdiantime;
			moheshop = new java.util.HashMap<Integer, xbean.mohe>();
			for (java.util.Map.Entry<Integer, xbean.mohe> _e_ : _o_.moheshop.entrySet())
				moheshop.put(_e_.getKey(), new mohe.Data(_e_.getValue()));
			smzhangjie = _o_.smzhangjie;
			battlenum = _o_.battlenum;
			smendtime = _o_.smendtime;
			smshop = new java.util.HashMap<Integer, xbean.smshopdata>();
			for (java.util.Map.Entry<Integer, xbean.smshopdata> _e_ : _o_.smshop.entrySet())
				smshop.put(_e_.getKey(), new smshopdata.Data(_e_.getValue()));
			smguanka_nocome = _o_.smguanka_nocome;
			smshop_notcome = _o_.smshop_notcome;
			createtime = _o_.createtime;
			onlinetime = _o_.onlinetime;
			offlinetime = _o_.offlinetime;
			tibuynum = _o_.tibuynum;
			tibuytime = _o_.tibuytime;
			goldbuynum = _o_.goldbuynum;
			goldbuytime = _o_.goldbuytime;
			signnum7 = _o_.signnum7;
			signnum28 = _o_.signnum28;
			signtime = _o_.signtime;
			qiyuannum = _o_.qiyuannum;
			qiyuantime = _o_.qiyuantime;
			qiyuanallnum = _o_.qiyuanallnum;
			buybagnum = _o_.buybagnum;
			buyherobagnum = _o_.buyherobagnum;
			troopnum = _o_.troopnum;
			sweepnum = _o_.sweepnum;
			todaylasttime = _o_.todaylasttime;
			sweepbuynum = _o_.sweepbuynum;
			mszqgetnum = _o_.mszqgetnum;
			newyindao = new java.util.LinkedList<Integer>();
			newyindao.addAll(_o_.newyindao);
		}

		@Override
		public final OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(rolename, xdb.Const.IO_CHARSET);
			_os_.marshal(userid);
			_os_.marshal(username, xdb.Const.IO_CHARSET);
			_os_.marshal(plattypestr, xdb.Const.IO_CHARSET);
			_os_.marshal(mac, xdb.Const.IO_CHARSET);
			_os_.marshal(ostype);
			_os_.marshal(level);
			_os_.marshal(exp);
			_os_.marshal(viplv);
			_os_.marshal(vipexp);
			_os_.marshal(ti);
			_os_.marshal(tichangetime);
			_os_.marshal(gold);
			_os_.marshal(yuanbao);
			_os_.marshal(shenglingzq);
			_os_.marshal(ronglian);
			_os_.marshal(huangjinxz);
			_os_.marshal(baijinxz);
			_os_.marshal(qingtongxz);
			_os_.marshal(chitiexz);
			_os_.marshal(jyjiejing);
			_os_.marshal(pvpti);
			_os_.marshal(pvptitime);
			_os_.marshal(tanxianti);
			_os_.marshal(tanxiantitime);
			_os_.marshal(jinengdian);
			_os_.marshal(jinengdiantime);
			_os_.compact_uint32(moheshop.size());
			for (java.util.Map.Entry<Integer, xbean.mohe> _e_ : moheshop.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			_os_.marshal(smzhangjie);
			_os_.marshal(battlenum);
			_os_.marshal(smendtime);
			_os_.compact_uint32(smshop.size());
			for (java.util.Map.Entry<Integer, xbean.smshopdata> _e_ : smshop.entrySet())
			{
				_os_.marshal(_e_.getKey());
				_e_.getValue().marshal(_os_);
			}
			_os_.marshal(smguanka_nocome);
			_os_.marshal(smshop_notcome);
			_os_.marshal(createtime);
			_os_.marshal(onlinetime);
			_os_.marshal(offlinetime);
			_os_.marshal(tibuynum);
			_os_.marshal(tibuytime);
			_os_.marshal(goldbuynum);
			_os_.marshal(goldbuytime);
			_os_.marshal(signnum7);
			_os_.marshal(signnum28);
			_os_.marshal(signtime);
			_os_.marshal(qiyuannum);
			_os_.marshal(qiyuantime);
			_os_.marshal(qiyuanallnum);
			_os_.marshal(buybagnum);
			_os_.marshal(buyherobagnum);
			_os_.marshal(troopnum);
			_os_.marshal(sweepnum);
			_os_.marshal(todaylasttime);
			_os_.marshal(sweepbuynum);
			_os_.marshal(mszqgetnum);
			_os_.compact_uint32(newyindao.size());
			for (Integer _v_ : newyindao) {
				_os_.marshal(_v_);
			}
			return _os_;
		}

		@Override
		public final OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
			rolename = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			userid = _os_.unmarshal_int();
			username = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			plattypestr = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			mac = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			ostype = _os_.unmarshal_int();
			level = _os_.unmarshal_int();
			exp = _os_.unmarshal_int();
			viplv = _os_.unmarshal_int();
			vipexp = _os_.unmarshal_int();
			ti = _os_.unmarshal_int();
			tichangetime = _os_.unmarshal_long();
			gold = _os_.unmarshal_int();
			yuanbao = _os_.unmarshal_int();
			shenglingzq = _os_.unmarshal_int();
			ronglian = _os_.unmarshal_int();
			huangjinxz = _os_.unmarshal_int();
			baijinxz = _os_.unmarshal_int();
			qingtongxz = _os_.unmarshal_int();
			chitiexz = _os_.unmarshal_int();
			jyjiejing = _os_.unmarshal_int();
			pvpti = _os_.unmarshal_int();
			pvptitime = _os_.unmarshal_long();
			tanxianti = _os_.unmarshal_int();
			tanxiantitime = _os_.unmarshal_long();
			jinengdian = _os_.unmarshal_int();
			jinengdiantime = _os_.unmarshal_long();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					moheshop = new java.util.HashMap<Integer, xbean.mohe>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.mohe _v_ = xbean.Pod.newmoheData();
					_v_.unmarshal(_os_);
					moheshop.put(_k_, _v_);
				}
			}
			smzhangjie = _os_.unmarshal_int();
			battlenum = _os_.unmarshal_int();
			smendtime = _os_.unmarshal_long();
			{
				int size = _os_.uncompact_uint32();
				if (size >= 12) { // {java.util.HashMap} 16 * 0.75 = 12
					smshop = new java.util.HashMap<Integer, xbean.smshopdata>(size * 2);
				}
				for (; size > 0; --size)
				{
					int _k_ = 0;
					_k_ = _os_.unmarshal_int();
					xbean.smshopdata _v_ = xbean.Pod.newsmshopdataData();
					_v_.unmarshal(_os_);
					smshop.put(_k_, _v_);
				}
			}
			smguanka_nocome = _os_.unmarshal_int();
			smshop_notcome = _os_.unmarshal_int();
			createtime = _os_.unmarshal_long();
			onlinetime = _os_.unmarshal_long();
			offlinetime = _os_.unmarshal_long();
			tibuynum = _os_.unmarshal_int();
			tibuytime = _os_.unmarshal_long();
			goldbuynum = _os_.unmarshal_int();
			goldbuytime = _os_.unmarshal_long();
			signnum7 = _os_.unmarshal_int();
			signnum28 = _os_.unmarshal_int();
			signtime = _os_.unmarshal_long();
			qiyuannum = _os_.unmarshal_int();
			qiyuantime = _os_.unmarshal_long();
			qiyuanallnum = _os_.unmarshal_int();
			buybagnum = _os_.unmarshal_short();
			buyherobagnum = _os_.unmarshal_short();
			troopnum = _os_.unmarshal_short();
			sweepnum = _os_.unmarshal_int();
			todaylasttime = _os_.unmarshal_long();
			sweepbuynum = _os_.unmarshal_int();
			mszqgetnum = _os_.unmarshal_int();
			for (int size = _os_.uncompact_uint32(); size > 0; --size) {
				int _v_ = 0;
				_v_ = _os_.unmarshal_int();
				newyindao.add(_v_);
			}
			return _os_;
		}

		@Override
		public xbean.Properties copy() {
			return new Data(this);
		}

		@Override
		public xbean.Properties toData() {
			return new Data(this);
		}

		public xbean.Properties toBean() {
			return new Properties(this, null, null);
		}

		@Override
		public xbean.Properties toDataIf() {
			return this;
		}

		public xbean.Properties toBeanIf() {
			return new Properties(this, null, null);
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
		public String getRolename() { // 角色名
			return rolename;
		}

		@Override
		public com.goldhuman.Common.Octets getRolenameOctets() { // 角色名
			return com.goldhuman.Common.Octets.wrap(getRolename(), xdb.Const.IO_CHARSET);
		}

		@Override
		public int getUserid() { // 所属角色id
			return userid;
		}

		@Override
		public String getUsername() { // 上次登录的账号名称
			return username;
		}

		@Override
		public com.goldhuman.Common.Octets getUsernameOctets() { // 上次登录的账号名称
			return com.goldhuman.Common.Octets.wrap(getUsername(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getPlattypestr() { // 上次登录的平台string
			return plattypestr;
		}

		@Override
		public com.goldhuman.Common.Octets getPlattypestrOctets() { // 上次登录的平台string
			return com.goldhuman.Common.Octets.wrap(getPlattypestr(), xdb.Const.IO_CHARSET);
		}

		@Override
		public String getMac() { // 上次登录的MAC地址
			return mac;
		}

		@Override
		public com.goldhuman.Common.Octets getMacOctets() { // 上次登录的MAC地址
			return com.goldhuman.Common.Octets.wrap(getMac(), xdb.Const.IO_CHARSET);
		}

		@Override
		public int getOstype() { // 系统类型
			return ostype;
		}

		@Override
		public int getLevel() { // 等级
			return level;
		}

		@Override
		public int getExp() { // 经验
			return exp;
		}

		@Override
		public int getViplv() { // vip等级
			return viplv;
		}

		@Override
		public int getVipexp() { // vip经验
			return vipexp;
		}

		@Override
		public int getTi() { // 体力
			return ti;
		}

		@Override
		public long getTichangetime() { // 体力更新时间
			return tichangetime;
		}

		@Override
		public int getGold() { // 金币
			return gold;
		}

		@Override
		public int getYuanbao() { // 元宝（水晶）
			return yuanbao;
		}

		@Override
		public int getShenglingzq() { // 圣灵之泉
			return shenglingzq;
		}

		@Override
		public int getRonglian() { // 熔炼点
			return ronglian;
		}

		@Override
		public int getHuangjinxz() { // 黄金勋章
			return huangjinxz;
		}

		@Override
		public int getBaijinxz() { // 白金勋章
			return baijinxz;
		}

		@Override
		public int getQingtongxz() { // 青铜勋章
			return qingtongxz;
		}

		@Override
		public int getChitiexz() { // 赤铁勋章
			return chitiexz;
		}

		@Override
		public int getJyjiejing() { // 经验结晶
			return jyjiejing;
		}

		@Override
		public int getPvpti() { // PVP精力
			return pvpti;
		}

		@Override
		public long getPvptitime() { // PVP精力更新时间
			return pvptitime;
		}

		@Override
		public int getTanxianti() { // 探险行动力
			return tanxianti;
		}

		@Override
		public long getTanxiantitime() { // 探险行动力更新时间
			return tanxiantitime;
		}

		@Override
		public int getJinengdian() { // 技能点
			return jinengdian;
		}

		@Override
		public long getJinengdiantime() { // 技能点更新时间
			return jinengdiantime;
		}

		@Override
		public java.util.Map<Integer, xbean.mohe> getMoheshop() { // 魔盒列表
			return moheshop;
		}

		@Override
		public java.util.Map<Integer, xbean.mohe> getMoheshopAsData() { // 魔盒列表
			return moheshop;
		}

		@Override
		public int getSmzhangjie() { // 神秘关卡或商店的所属章节记录
			return smzhangjie;
		}

		@Override
		public int getBattlenum() { // 神秘关卡或商店记录
			return battlenum;
		}

		@Override
		public long getSmendtime() { // 神秘关卡或商店结束时间
			return smendtime;
		}

		@Override
		public java.util.Map<Integer, xbean.smshopdata> getSmshop() { // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
			return smshop;
		}

		@Override
		public java.util.Map<Integer, xbean.smshopdata> getSmshopAsData() { // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
			return smshop;
		}

		@Override
		public int getSmguanka_nocome() { // 神秘关卡未出现次数
			return smguanka_nocome;
		}

		@Override
		public int getSmshop_notcome() { // 神秘商店未出现次数
			return smshop_notcome;
		}

		@Override
		public long getCreatetime() { // 创建时间
			return createtime;
		}

		@Override
		public long getOnlinetime() { // 上线时间
			return onlinetime;
		}

		@Override
		public long getOfflinetime() { // 下线时间
			return offlinetime;
		}

		@Override
		public int getTibuynum() { // 体力购买次数
			return tibuynum;
		}

		@Override
		public long getTibuytime() { // 上次记录的体力购买时间
			return tibuytime;
		}

		@Override
		public int getGoldbuynum() { // 金币购买次数
			return goldbuynum;
		}

		@Override
		public long getGoldbuytime() { // 上次记录的金币购买时间
			return goldbuytime;
		}

		@Override
		public int getSignnum7() { // 连续签到ID
			return signnum7;
		}

		@Override
		public int getSignnum28() { // 累计签到ID
			return signnum28;
		}

		@Override
		public long getSigntime() { // 最后签到时间
			return signtime;
		}

		@Override
		public int getQiyuannum() { // 累计祈愿台次数
			return qiyuannum;
		}

		@Override
		public long getQiyuantime() { // 最后祈愿时间
			return qiyuantime;
		}

		@Override
		public int getQiyuanallnum() { // 祈愿回合次数，第一次为3，完成后均为5
			return qiyuanallnum;
		}

		@Override
		public short getBuybagnum() { // 扩充背包次数
			return buybagnum;
		}

		@Override
		public short getBuyherobagnum() { // 扩充英雄背包次数
			return buyherobagnum;
		}

		@Override
		public short getTroopnum() { // 默认编队号
			return troopnum;
		}

		@Override
		public int getSweepnum() { // 今日扫荡次数
			return sweepnum;
		}

		@Override
		public long getTodaylasttime() { // 今日计时时间
			return todaylasttime;
		}

		@Override
		public int getSweepbuynum() { // 今日扫荡购买次数
			return sweepbuynum;
		}

		@Override
		public int getMszqgetnum() { // 缪斯奏曲：个位为中午，十位为晚上
			return mszqgetnum;
		}

		@Override
		public java.util.List<Integer> getNewyindao() { // 新手引导
			return newyindao;
		}

		@Override
		public java.util.List<Integer> getNewyindaoAsData() { // 新手引导
			return newyindao;
		}

		@Override
		public void setRolename(String _v_) { // 角色名
			if (null == _v_)
				throw new NullPointerException();
			rolename = _v_;
		}

		@Override
		public void setRolenameOctets(com.goldhuman.Common.Octets _v_) { // 角色名
			this.setRolename(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setUserid(int _v_) { // 所属角色id
			userid = _v_;
		}

		@Override
		public void setUsername(String _v_) { // 上次登录的账号名称
			if (null == _v_)
				throw new NullPointerException();
			username = _v_;
		}

		@Override
		public void setUsernameOctets(com.goldhuman.Common.Octets _v_) { // 上次登录的账号名称
			this.setUsername(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setPlattypestr(String _v_) { // 上次登录的平台string
			if (null == _v_)
				throw new NullPointerException();
			plattypestr = _v_;
		}

		@Override
		public void setPlattypestrOctets(com.goldhuman.Common.Octets _v_) { // 上次登录的平台string
			this.setPlattypestr(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setMac(String _v_) { // 上次登录的MAC地址
			if (null == _v_)
				throw new NullPointerException();
			mac = _v_;
		}

		@Override
		public void setMacOctets(com.goldhuman.Common.Octets _v_) { // 上次登录的MAC地址
			this.setMac(_v_.getString(xdb.Const.IO_CHARSET));
		}

		@Override
		public void setOstype(int _v_) { // 系统类型
			ostype = _v_;
		}

		@Override
		public void setLevel(int _v_) { // 等级
			level = _v_;
		}

		@Override
		public void setExp(int _v_) { // 经验
			exp = _v_;
		}

		@Override
		public void setViplv(int _v_) { // vip等级
			viplv = _v_;
		}

		@Override
		public void setVipexp(int _v_) { // vip经验
			vipexp = _v_;
		}

		@Override
		public void setTi(int _v_) { // 体力
			ti = _v_;
		}

		@Override
		public void setTichangetime(long _v_) { // 体力更新时间
			tichangetime = _v_;
		}

		@Override
		public void setGold(int _v_) { // 金币
			gold = _v_;
		}

		@Override
		public void setYuanbao(int _v_) { // 元宝（水晶）
			yuanbao = _v_;
		}

		@Override
		public void setShenglingzq(int _v_) { // 圣灵之泉
			shenglingzq = _v_;
		}

		@Override
		public void setRonglian(int _v_) { // 熔炼点
			ronglian = _v_;
		}

		@Override
		public void setHuangjinxz(int _v_) { // 黄金勋章
			huangjinxz = _v_;
		}

		@Override
		public void setBaijinxz(int _v_) { // 白金勋章
			baijinxz = _v_;
		}

		@Override
		public void setQingtongxz(int _v_) { // 青铜勋章
			qingtongxz = _v_;
		}

		@Override
		public void setChitiexz(int _v_) { // 赤铁勋章
			chitiexz = _v_;
		}

		@Override
		public void setJyjiejing(int _v_) { // 经验结晶
			jyjiejing = _v_;
		}

		@Override
		public void setPvpti(int _v_) { // PVP精力
			pvpti = _v_;
		}

		@Override
		public void setPvptitime(long _v_) { // PVP精力更新时间
			pvptitime = _v_;
		}

		@Override
		public void setTanxianti(int _v_) { // 探险行动力
			tanxianti = _v_;
		}

		@Override
		public void setTanxiantitime(long _v_) { // 探险行动力更新时间
			tanxiantitime = _v_;
		}

		@Override
		public void setJinengdian(int _v_) { // 技能点
			jinengdian = _v_;
		}

		@Override
		public void setJinengdiantime(long _v_) { // 技能点更新时间
			jinengdiantime = _v_;
		}

		@Override
		public void setSmzhangjie(int _v_) { // 神秘关卡或商店的所属章节记录
			smzhangjie = _v_;
		}

		@Override
		public void setBattlenum(int _v_) { // 神秘关卡或商店记录
			battlenum = _v_;
		}

		@Override
		public void setSmendtime(long _v_) { // 神秘关卡或商店结束时间
			smendtime = _v_;
		}

		@Override
		public void setSmguanka_nocome(int _v_) { // 神秘关卡未出现次数
			smguanka_nocome = _v_;
		}

		@Override
		public void setSmshop_notcome(int _v_) { // 神秘商店未出现次数
			smshop_notcome = _v_;
		}

		@Override
		public void setCreatetime(long _v_) { // 创建时间
			createtime = _v_;
		}

		@Override
		public void setOnlinetime(long _v_) { // 上线时间
			onlinetime = _v_;
		}

		@Override
		public void setOfflinetime(long _v_) { // 下线时间
			offlinetime = _v_;
		}

		@Override
		public void setTibuynum(int _v_) { // 体力购买次数
			tibuynum = _v_;
		}

		@Override
		public void setTibuytime(long _v_) { // 上次记录的体力购买时间
			tibuytime = _v_;
		}

		@Override
		public void setGoldbuynum(int _v_) { // 金币购买次数
			goldbuynum = _v_;
		}

		@Override
		public void setGoldbuytime(long _v_) { // 上次记录的金币购买时间
			goldbuytime = _v_;
		}

		@Override
		public void setSignnum7(int _v_) { // 连续签到ID
			signnum7 = _v_;
		}

		@Override
		public void setSignnum28(int _v_) { // 累计签到ID
			signnum28 = _v_;
		}

		@Override
		public void setSigntime(long _v_) { // 最后签到时间
			signtime = _v_;
		}

		@Override
		public void setQiyuannum(int _v_) { // 累计祈愿台次数
			qiyuannum = _v_;
		}

		@Override
		public void setQiyuantime(long _v_) { // 最后祈愿时间
			qiyuantime = _v_;
		}

		@Override
		public void setQiyuanallnum(int _v_) { // 祈愿回合次数，第一次为3，完成后均为5
			qiyuanallnum = _v_;
		}

		@Override
		public void setBuybagnum(short _v_) { // 扩充背包次数
			buybagnum = _v_;
		}

		@Override
		public void setBuyherobagnum(short _v_) { // 扩充英雄背包次数
			buyherobagnum = _v_;
		}

		@Override
		public void setTroopnum(short _v_) { // 默认编队号
			troopnum = _v_;
		}

		@Override
		public void setSweepnum(int _v_) { // 今日扫荡次数
			sweepnum = _v_;
		}

		@Override
		public void setTodaylasttime(long _v_) { // 今日计时时间
			todaylasttime = _v_;
		}

		@Override
		public void setSweepbuynum(int _v_) { // 今日扫荡购买次数
			sweepbuynum = _v_;
		}

		@Override
		public void setMszqgetnum(int _v_) { // 缪斯奏曲：个位为中午，十位为晚上
			mszqgetnum = _v_;
		}

		@Override
		public final boolean equals(Object _o1_) {
			if (!(_o1_ instanceof Properties.Data)) return false;
			Properties.Data _o_ = (Properties.Data) _o1_;
			if (!rolename.equals(_o_.rolename)) return false;
			if (userid != _o_.userid) return false;
			if (!username.equals(_o_.username)) return false;
			if (!plattypestr.equals(_o_.plattypestr)) return false;
			if (!mac.equals(_o_.mac)) return false;
			if (ostype != _o_.ostype) return false;
			if (level != _o_.level) return false;
			if (exp != _o_.exp) return false;
			if (viplv != _o_.viplv) return false;
			if (vipexp != _o_.vipexp) return false;
			if (ti != _o_.ti) return false;
			if (tichangetime != _o_.tichangetime) return false;
			if (gold != _o_.gold) return false;
			if (yuanbao != _o_.yuanbao) return false;
			if (shenglingzq != _o_.shenglingzq) return false;
			if (ronglian != _o_.ronglian) return false;
			if (huangjinxz != _o_.huangjinxz) return false;
			if (baijinxz != _o_.baijinxz) return false;
			if (qingtongxz != _o_.qingtongxz) return false;
			if (chitiexz != _o_.chitiexz) return false;
			if (jyjiejing != _o_.jyjiejing) return false;
			if (pvpti != _o_.pvpti) return false;
			if (pvptitime != _o_.pvptitime) return false;
			if (tanxianti != _o_.tanxianti) return false;
			if (tanxiantitime != _o_.tanxiantitime) return false;
			if (jinengdian != _o_.jinengdian) return false;
			if (jinengdiantime != _o_.jinengdiantime) return false;
			if (!moheshop.equals(_o_.moheshop)) return false;
			if (smzhangjie != _o_.smzhangjie) return false;
			if (battlenum != _o_.battlenum) return false;
			if (smendtime != _o_.smendtime) return false;
			if (!smshop.equals(_o_.smshop)) return false;
			if (smguanka_nocome != _o_.smguanka_nocome) return false;
			if (smshop_notcome != _o_.smshop_notcome) return false;
			if (createtime != _o_.createtime) return false;
			if (onlinetime != _o_.onlinetime) return false;
			if (offlinetime != _o_.offlinetime) return false;
			if (tibuynum != _o_.tibuynum) return false;
			if (tibuytime != _o_.tibuytime) return false;
			if (goldbuynum != _o_.goldbuynum) return false;
			if (goldbuytime != _o_.goldbuytime) return false;
			if (signnum7 != _o_.signnum7) return false;
			if (signnum28 != _o_.signnum28) return false;
			if (signtime != _o_.signtime) return false;
			if (qiyuannum != _o_.qiyuannum) return false;
			if (qiyuantime != _o_.qiyuantime) return false;
			if (qiyuanallnum != _o_.qiyuanallnum) return false;
			if (buybagnum != _o_.buybagnum) return false;
			if (buyherobagnum != _o_.buyherobagnum) return false;
			if (troopnum != _o_.troopnum) return false;
			if (sweepnum != _o_.sweepnum) return false;
			if (todaylasttime != _o_.todaylasttime) return false;
			if (sweepbuynum != _o_.sweepbuynum) return false;
			if (mszqgetnum != _o_.mszqgetnum) return false;
			if (!newyindao.equals(_o_.newyindao)) return false;
			return true;
		}

		@Override
		public final int hashCode() {
			int _h_ = 0;
			_h_ += rolename.hashCode();
			_h_ += userid;
			_h_ += username.hashCode();
			_h_ += plattypestr.hashCode();
			_h_ += mac.hashCode();
			_h_ += ostype;
			_h_ += level;
			_h_ += exp;
			_h_ += viplv;
			_h_ += vipexp;
			_h_ += ti;
			_h_ += tichangetime;
			_h_ += gold;
			_h_ += yuanbao;
			_h_ += shenglingzq;
			_h_ += ronglian;
			_h_ += huangjinxz;
			_h_ += baijinxz;
			_h_ += qingtongxz;
			_h_ += chitiexz;
			_h_ += jyjiejing;
			_h_ += pvpti;
			_h_ += pvptitime;
			_h_ += tanxianti;
			_h_ += tanxiantitime;
			_h_ += jinengdian;
			_h_ += jinengdiantime;
			_h_ += moheshop.hashCode();
			_h_ += smzhangjie;
			_h_ += battlenum;
			_h_ += smendtime;
			_h_ += smshop.hashCode();
			_h_ += smguanka_nocome;
			_h_ += smshop_notcome;
			_h_ += createtime;
			_h_ += onlinetime;
			_h_ += offlinetime;
			_h_ += tibuynum;
			_h_ += tibuytime;
			_h_ += goldbuynum;
			_h_ += goldbuytime;
			_h_ += signnum7;
			_h_ += signnum28;
			_h_ += signtime;
			_h_ += qiyuannum;
			_h_ += qiyuantime;
			_h_ += qiyuanallnum;
			_h_ += buybagnum;
			_h_ += buyherobagnum;
			_h_ += troopnum;
			_h_ += sweepnum;
			_h_ += todaylasttime;
			_h_ += sweepbuynum;
			_h_ += mszqgetnum;
			_h_ += newyindao.hashCode();
			return _h_;
		}

		@Override
		public String toString() {
			StringBuilder _sb_ = new StringBuilder();
			_sb_.append("(");
			_sb_.append("'").append(rolename).append("'");
			_sb_.append(",");
			_sb_.append(userid);
			_sb_.append(",");
			_sb_.append("'").append(username).append("'");
			_sb_.append(",");
			_sb_.append("'").append(plattypestr).append("'");
			_sb_.append(",");
			_sb_.append("'").append(mac).append("'");
			_sb_.append(",");
			_sb_.append(ostype);
			_sb_.append(",");
			_sb_.append(level);
			_sb_.append(",");
			_sb_.append(exp);
			_sb_.append(",");
			_sb_.append(viplv);
			_sb_.append(",");
			_sb_.append(vipexp);
			_sb_.append(",");
			_sb_.append(ti);
			_sb_.append(",");
			_sb_.append(tichangetime);
			_sb_.append(",");
			_sb_.append(gold);
			_sb_.append(",");
			_sb_.append(yuanbao);
			_sb_.append(",");
			_sb_.append(shenglingzq);
			_sb_.append(",");
			_sb_.append(ronglian);
			_sb_.append(",");
			_sb_.append(huangjinxz);
			_sb_.append(",");
			_sb_.append(baijinxz);
			_sb_.append(",");
			_sb_.append(qingtongxz);
			_sb_.append(",");
			_sb_.append(chitiexz);
			_sb_.append(",");
			_sb_.append(jyjiejing);
			_sb_.append(",");
			_sb_.append(pvpti);
			_sb_.append(",");
			_sb_.append(pvptitime);
			_sb_.append(",");
			_sb_.append(tanxianti);
			_sb_.append(",");
			_sb_.append(tanxiantitime);
			_sb_.append(",");
			_sb_.append(jinengdian);
			_sb_.append(",");
			_sb_.append(jinengdiantime);
			_sb_.append(",");
			_sb_.append(moheshop);
			_sb_.append(",");
			_sb_.append(smzhangjie);
			_sb_.append(",");
			_sb_.append(battlenum);
			_sb_.append(",");
			_sb_.append(smendtime);
			_sb_.append(",");
			_sb_.append(smshop);
			_sb_.append(",");
			_sb_.append(smguanka_nocome);
			_sb_.append(",");
			_sb_.append(smshop_notcome);
			_sb_.append(",");
			_sb_.append(createtime);
			_sb_.append(",");
			_sb_.append(onlinetime);
			_sb_.append(",");
			_sb_.append(offlinetime);
			_sb_.append(",");
			_sb_.append(tibuynum);
			_sb_.append(",");
			_sb_.append(tibuytime);
			_sb_.append(",");
			_sb_.append(goldbuynum);
			_sb_.append(",");
			_sb_.append(goldbuytime);
			_sb_.append(",");
			_sb_.append(signnum7);
			_sb_.append(",");
			_sb_.append(signnum28);
			_sb_.append(",");
			_sb_.append(signtime);
			_sb_.append(",");
			_sb_.append(qiyuannum);
			_sb_.append(",");
			_sb_.append(qiyuantime);
			_sb_.append(",");
			_sb_.append(qiyuanallnum);
			_sb_.append(",");
			_sb_.append(buybagnum);
			_sb_.append(",");
			_sb_.append(buyherobagnum);
			_sb_.append(",");
			_sb_.append(troopnum);
			_sb_.append(",");
			_sb_.append(sweepnum);
			_sb_.append(",");
			_sb_.append(todaylasttime);
			_sb_.append(",");
			_sb_.append(sweepbuynum);
			_sb_.append(",");
			_sb_.append(mszqgetnum);
			_sb_.append(",");
			_sb_.append(newyindao);
			_sb_.append(")");
			return _sb_.toString();
		}

	}
}
