
package xbean;

public interface Properties extends xdb.Bean {
	public Properties copy(); // deep clone
	public Properties toData(); // a Data instance
	public Properties toBean(); // a Bean instance
	public Properties toDataIf(); // a Data instance If need. else return this
	public Properties toBeanIf(); // a Bean instance If need. else return this

	public String getRolename(); // 角色名
	public com.goldhuman.Common.Octets getRolenameOctets(); // 角色名
	public int getUserid(); // 所属角色id
	public String getUsername(); // 上次登录的账号名称
	public com.goldhuman.Common.Octets getUsernameOctets(); // 上次登录的账号名称
	public String getPlattypestr(); // 上次登录的平台string
	public com.goldhuman.Common.Octets getPlattypestrOctets(); // 上次登录的平台string
	public String getMac(); // 上次登录的MAC地址
	public com.goldhuman.Common.Octets getMacOctets(); // 上次登录的MAC地址
	public int getOstype(); // 系统类型
	public int getLevel(); // 等级
	public int getExp(); // 经验
	public int getViplv(); // vip等级
	public int getVipexp(); // vip经验
	public int getTi(); // 体力
	public long getTichangetime(); // 体力更新时间
	public int getGold(); // 金币
	public int getYuanbao(); // 元宝（水晶）
	public int getShenglingzq(); // 圣灵之泉
	public int getRonglian(); // 熔炼点
	public int getHuangjinxz(); // 黄金勋章
	public int getBaijinxz(); // 白金勋章
	public int getQingtongxz(); // 青铜勋章
	public int getChitiexz(); // 赤铁勋章
	public int getJyjiejing(); // 经验结晶
	public int getPvpti(); // PVP精力
	public long getPvptitime(); // PVP精力更新时间
	public int getTanxianti(); // 探险行动力
	public long getTanxiantitime(); // 探险行动力更新时间
	public int getJinengdian(); // 技能点
	public long getJinengdiantime(); // 技能点更新时间
	public java.util.Map<Integer, xbean.mohe> getMoheshop(); // 魔盒列表
	public java.util.Map<Integer, xbean.mohe> getMoheshopAsData(); // 魔盒列表
	public int getSmzhangjie(); // 神秘关卡或商店的所属章节记录
	public int getBattlenum(); // 神秘关卡或商店记录
	public long getSmendtime(); // 神秘关卡或商店结束时间
	public java.util.Map<Integer, xbean.smshopdata> getSmshop(); // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
	public java.util.Map<Integer, xbean.smshopdata> getSmshopAsData(); // 神秘商店随机出的物品（key为随机商店物品id，value为smshopdata）
	public int getSmguanka_nocome(); // 神秘关卡未出现次数
	public int getSmshop_notcome(); // 神秘商店未出现次数
	public long getCreatetime(); // 创建时间
	public long getOnlinetime(); // 上线时间
	public long getOfflinetime(); // 下线时间
	public int getTibuynum(); // 体力购买次数
	public long getTibuytime(); // 上次记录的体力购买时间
	public int getGoldbuynum(); // 金币购买次数
	public long getGoldbuytime(); // 上次记录的金币购买时间
	public int getSignnum7(); // 连续签到ID
	public int getSignnum28(); // 累计签到ID
	public long getSigntime(); // 最后签到时间
	public int getQiyuannum(); // 累计祈愿台次数
	public long getQiyuantime(); // 最后祈愿时间
	public int getQiyuanallnum(); // 祈愿回合次数，第一次为3，完成后均为5
	public short getBuybagnum(); // 扩充背包次数
	public short getBuyherobagnum(); // 扩充英雄背包次数
	public short getTroopnum(); // 默认编队号
	public int getSweepnum(); // 今日扫荡次数
	public long getTodaylasttime(); // 今日计时时间
	public int getSweepbuynum(); // 今日扫荡购买次数
	public int getMszqgetnum(); // 缪斯奏曲：个位为中午，十位为晚上
	public java.util.List<Integer> getNewyindao(); // 新手引导
	public java.util.List<Integer> getNewyindaoAsData(); // 新手引导

	public void setRolename(String _v_); // 角色名
	public void setRolenameOctets(com.goldhuman.Common.Octets _v_); // 角色名
	public void setUserid(int _v_); // 所属角色id
	public void setUsername(String _v_); // 上次登录的账号名称
	public void setUsernameOctets(com.goldhuman.Common.Octets _v_); // 上次登录的账号名称
	public void setPlattypestr(String _v_); // 上次登录的平台string
	public void setPlattypestrOctets(com.goldhuman.Common.Octets _v_); // 上次登录的平台string
	public void setMac(String _v_); // 上次登录的MAC地址
	public void setMacOctets(com.goldhuman.Common.Octets _v_); // 上次登录的MAC地址
	public void setOstype(int _v_); // 系统类型
	public void setLevel(int _v_); // 等级
	public void setExp(int _v_); // 经验
	public void setViplv(int _v_); // vip等级
	public void setVipexp(int _v_); // vip经验
	public void setTi(int _v_); // 体力
	public void setTichangetime(long _v_); // 体力更新时间
	public void setGold(int _v_); // 金币
	public void setYuanbao(int _v_); // 元宝（水晶）
	public void setShenglingzq(int _v_); // 圣灵之泉
	public void setRonglian(int _v_); // 熔炼点
	public void setHuangjinxz(int _v_); // 黄金勋章
	public void setBaijinxz(int _v_); // 白金勋章
	public void setQingtongxz(int _v_); // 青铜勋章
	public void setChitiexz(int _v_); // 赤铁勋章
	public void setJyjiejing(int _v_); // 经验结晶
	public void setPvpti(int _v_); // PVP精力
	public void setPvptitime(long _v_); // PVP精力更新时间
	public void setTanxianti(int _v_); // 探险行动力
	public void setTanxiantitime(long _v_); // 探险行动力更新时间
	public void setJinengdian(int _v_); // 技能点
	public void setJinengdiantime(long _v_); // 技能点更新时间
	public void setSmzhangjie(int _v_); // 神秘关卡或商店的所属章节记录
	public void setBattlenum(int _v_); // 神秘关卡或商店记录
	public void setSmendtime(long _v_); // 神秘关卡或商店结束时间
	public void setSmguanka_nocome(int _v_); // 神秘关卡未出现次数
	public void setSmshop_notcome(int _v_); // 神秘商店未出现次数
	public void setCreatetime(long _v_); // 创建时间
	public void setOnlinetime(long _v_); // 上线时间
	public void setOfflinetime(long _v_); // 下线时间
	public void setTibuynum(int _v_); // 体力购买次数
	public void setTibuytime(long _v_); // 上次记录的体力购买时间
	public void setGoldbuynum(int _v_); // 金币购买次数
	public void setGoldbuytime(long _v_); // 上次记录的金币购买时间
	public void setSignnum7(int _v_); // 连续签到ID
	public void setSignnum28(int _v_); // 累计签到ID
	public void setSigntime(long _v_); // 最后签到时间
	public void setQiyuannum(int _v_); // 累计祈愿台次数
	public void setQiyuantime(long _v_); // 最后祈愿时间
	public void setQiyuanallnum(int _v_); // 祈愿回合次数，第一次为3，完成后均为5
	public void setBuybagnum(short _v_); // 扩充背包次数
	public void setBuyherobagnum(short _v_); // 扩充英雄背包次数
	public void setTroopnum(short _v_); // 默认编队号
	public void setSweepnum(int _v_); // 今日扫荡次数
	public void setTodaylasttime(long _v_); // 今日计时时间
	public void setSweepbuynum(int _v_); // 今日扫荡购买次数
	public void setMszqgetnum(int _v_); // 缪斯奏曲：个位为中午，十位为晚上
}
