
package xbean;

public interface boss extends xdb.Bean {
	public boss copy(); // deep clone
	public boss toData(); // a Data instance
	public boss toBean(); // a Bean instance
	public boss toDataIf(); // a Data instance If need. else return this
	public boss toBeanIf(); // a Bean instance If need. else return this

	public long getLasthpall(); // 上次总血量
	public int getLastiskill(); // 上次是否杀掉，0未杀，1已杀
	public long getLastkillnum(); // 杀掉则为用时（毫秒），未杀则为受到的伤害
	public long getNewhpall(); // 最新总血量
	public long getNowhp(); // 现在血量
	public int getBossid1(); // bossid(第一个守门人)
	public int getBossid2(); // bossid(第一个boss)
	public int getBossid3(); // bossid(第二个守门人)
	public int getBossid4(); // bossid(第二个boss)
	public int getBossiskill(); // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
	public String getBoss1killname(); // 击杀1者名称
	public com.goldhuman.Common.Octets getBoss1killnameOctets(); // 击杀1者名称
	public String getBoss2killname(); // 击杀2者名称
	public com.goldhuman.Common.Octets getBoss2killnameOctets(); // 击杀2者名称
	public long getTime(); // 上次刷新时间

	public void setLasthpall(long _v_); // 上次总血量
	public void setLastiskill(int _v_); // 上次是否杀掉，0未杀，1已杀
	public void setLastkillnum(long _v_); // 杀掉则为用时（毫秒），未杀则为受到的伤害
	public void setNewhpall(long _v_); // 最新总血量
	public void setNowhp(long _v_); // 现在血量
	public void setBossid1(int _v_); // bossid(第一个守门人)
	public void setBossid2(int _v_); // bossid(第一个boss)
	public void setBossid3(int _v_); // bossid(第二个守门人)
	public void setBossid4(int _v_); // bossid(第二个boss)
	public void setBossiskill(int _v_); // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
	public void setBoss1killname(String _v_); // 击杀1者名称
	public void setBoss1killnameOctets(com.goldhuman.Common.Octets _v_); // 击杀1者名称
	public void setBoss2killname(String _v_); // 击杀2者名称
	public void setBoss2killnameOctets(com.goldhuman.Common.Octets _v_); // 击杀2者名称
	public void setTime(long _v_); // 上次刷新时间
}
