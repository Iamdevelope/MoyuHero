
package xbean;

public interface TuJianHeros extends xdb.Bean {
	public TuJianHeros copy(); // deep clone
	public TuJianHeros toData(); // a Data instance
	public TuJianHeros toBean(); // a Bean instance
	public TuJianHeros toDataIf(); // a Data instance If need. else return this
	public TuJianHeros toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, Integer> getTujianbox(); // 宝箱获取（理论上有key则为已获取）
	public java.util.Map<Integer, Integer> getTujianboxAsData(); // 宝箱获取（理论上有key则为已获取）
	public java.util.Map<Integer, xbean.TuJianHero> getTujianhero(); // 获得过的武将
	public java.util.Map<Integer, xbean.TuJianHero> getTujianheroAsData(); // 获得过的武将
	public java.util.List<Integer> getTjheromaxlevel(); // 满级图鉴列表
	public java.util.List<Integer> getTjheromaxlevelAsData(); // 满级图鉴列表

}
