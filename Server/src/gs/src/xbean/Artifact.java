
package xbean;

public interface Artifact extends xdb.Bean {
	public Artifact copy(); // deep clone
	public Artifact toData(); // a Data instance
	public Artifact toBean(); // a Bean instance
	public Artifact toDataIf(); // a Data instance If need. else return this
	public Artifact toBeanIf(); // a Bean instance If need. else return this

	public int getArtifacttype(); // 神器类型（key）
	public int getArtifactid(); // 神器ID
	public int getHeronum1(); // 英雄数量1
	public int getHeronum2(); // 英雄数量2
	public int getHeronum3(); // 英雄数量3
	public int getHeronum4(); // 英雄数量4
	public int getHeronum5(); // 英雄数量5

	public void setArtifacttype(int _v_); // 神器类型（key）
	public void setArtifactid(int _v_); // 神器ID
	public void setHeronum1(int _v_); // 英雄数量1
	public void setHeronum2(int _v_); // 英雄数量2
	public void setHeronum3(int _v_); // 英雄数量3
	public void setHeronum4(int _v_); // 英雄数量4
	public void setHeronum5(int _v_); // 英雄数量5
}
