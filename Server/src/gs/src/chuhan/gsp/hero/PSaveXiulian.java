package chuhan.gsp.hero;

public class PSaveXiulian extends xdb.Procedure
{
	private final long roleId;
	public PSaveXiulian(long roleId) {
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		
//		xbean.XiuLianResult xresult = xtable.Xiulianresults.get(roleId);
		
//		if(xresult == null)
//			return false;
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		OldHero hero = herocol.getHero(1);
		if(hero == null)
			return false;
		/*//by yanglk  hero
		int canXiulian = hero.canXiulianTimes();
		int addv = xresult.getHp()+xresult.getAttack()+xresult.getDefend()+xresult.getWisdom();
		int maxv = (canXiulian > 0)? 5:0; 
		if(addv > maxv)
			return false;
		hero.getHeroInfo().getBfp().setHp(hero.getHeroInfo().getBfp().getHp() + xresult.getHp());
		hero.getHeroInfo().getBfp().setAttack(hero.getHeroInfo().getBfp().getAttack() + xresult.getAttack());
		hero.getHeroInfo().getBfp().setDefend(hero.getHeroInfo().getBfp().getDefend() + xresult.getDefend());
		hero.getHeroInfo().getBfp().setWisdom(hero.getHeroInfo().getBfp().getWisdom() + xresult.getWisdom());
		if(canXiulian > 0)
			hero.getHeroInfo().setXiuliantimes(hero.getHeroInfo().getXiuliantimes()+1);
		xtable.Xiulianresults.remove(roleId);
		psendWhileCommit(roleId, new SRefreshHero(hero.getProtocolHero()));
		*/
		return true;
	}
}
