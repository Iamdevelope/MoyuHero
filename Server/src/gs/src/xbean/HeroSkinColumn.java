
package xbean;

public interface HeroSkinColumn extends xdb.Bean {
	public HeroSkinColumn copy(); // deep clone
	public HeroSkinColumn toData(); // a Data instance
	public HeroSkinColumn toBean(); // a Bean instance
	public HeroSkinColumn toDataIf(); // a Data instance If need. else return this
	public HeroSkinColumn toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.HeroSkin> getHeroskins(); // 
	public java.util.List<xbean.HeroSkin> getHeroskinsAsData(); // 

}
