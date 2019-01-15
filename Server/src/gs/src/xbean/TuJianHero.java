
package xbean;

public interface TuJianHero extends xdb.Bean {
	public TuJianHero copy(); // deep clone
	public TuJianHero toData(); // a Data instance
	public TuJianHero toBean(); // a Bean instance
	public TuJianHero toDataIf(); // a Data instance If need. else return this
	public TuJianHero toBeanIf(); // a Bean instance If need. else return this

	public int getHeroid(); // 获得过的武将
	public int getFlag(); // 是否满级，0未满，1满级

	public void setHeroid(int _v_); // 获得过的武将
	public void setFlag(int _v_); // 是否满级，0未满，1满级
}
