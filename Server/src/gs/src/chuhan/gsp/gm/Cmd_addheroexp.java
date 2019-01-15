package chuhan.gsp.gm;

import java.util.List;

import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.hero.PAddExpHero;

public class Cmd_addheroexp extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int exp = Integer.valueOf(args[0]);
		if (exp == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		final long roleid = getGmroleid();
		
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				java.util.LinkedList<Integer> heroIdList = new java.util.LinkedList<Integer>();
				HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
				List<xbean.Hero> xherolist = herocol.getxcolumn().getHeroes();
				for(xbean.Hero hero : xherolist)
				{
					heroIdList.addFirst(hero.getKey());
				}
				
				PAddExpHero hero = new PAddExpHero(roleid,heroIdList,exp,PAddExpHero.OTHER,"");
				hero.call();
				return true;
			};
		}.submit();
		
		return true;
		
	}

	@Override
	String usage() {
		return "//addheroexp [addnumber]";
	}

}