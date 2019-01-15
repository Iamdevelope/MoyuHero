
package xbean;

public interface HeroSkin extends xdb.Bean {
	public HeroSkin copy(); // deep clone
	public HeroSkin toData(); // a Data instance
	public HeroSkin toBean(); // a Bean instance
	public HeroSkin toDataIf(); // a Data instance If need. else return this
	public HeroSkin toBeanIf(); // a Bean instance If need. else return this

	public int getHeroskinid(); // 皮肤ID
	public long getCreatetime(); // 创建时间

	public void setHeroskinid(int _v_); // 皮肤ID
	public void setCreatetime(long _v_); // 创建时间
}
