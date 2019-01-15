
package xbean;

public interface HeroColumn extends xdb.Bean {
	public HeroColumn copy(); // deep clone
	public HeroColumn toData(); // a Data instance
	public HeroColumn toBean(); // a Bean instance
	public HeroColumn toDataIf(); // a Data instance If need. else return this
	public HeroColumn toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.Hero> getHeroes(); // 
	public java.util.List<xbean.Hero> getHeroesAsData(); // 
	public int getNextkey(); // 

	public void setNextkey(int _v_); // 
}
