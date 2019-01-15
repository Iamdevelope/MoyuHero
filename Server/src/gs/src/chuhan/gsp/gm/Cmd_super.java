package chuhan.gsp.gm;

import java.util.List;

import chuhan.gsp.attr.PAddExpProc;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.hero.Hero;
import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.hero.PAddExpHero;
import chuhan.gsp.hero.PPeiYangHero;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.stage.StageRole;
import chuhan.gsp.util.Conv;

public class Cmd_super extends GMCommand {
	@Override
	boolean exec(String[] args) {

		final long roleid = getGmroleid();
		
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				DropManager.getInstance().dropAddByOther(1400000003, 999999999, 0, 0, roleid, "GMSUPER_ADD");
				java.util.LinkedList<Integer> heroIdList = new java.util.LinkedList<Integer>();
				HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
				List<xbean.Hero> xherolist = herocol.getxcolumn().getHeroes();
				for(xbean.Hero xhero : xherolist){
					Hero hero = herocol.getHByHKey(xhero.getKey());
					for(int i = 0;i<hero.getiHeroInfo().getTrainMaximum1();i++){
//						new PPeiYangHero(roleid, hero.getxHeroInfo().getKey(), Conv.toByte(1)).call();
					}
					for(int i = 0;i<hero.getiHeroInfo().getTrainMaximum2();i++){
//						new PPeiYangHero(roleid, hero.getxHeroInfo().getKey(), Conv.toByte(2)).call();
					}
					for(int i = 0;i<hero.getiHeroInfo().getTrainMaximum3();i++){
//						new PPeiYangHero(roleid, hero.getxHeroInfo().getKey(), Conv.toByte(3)).call();
					}
					for(int i = 0;i<hero.getiHeroInfo().getTrainMaximum4();i++){
//						new PPeiYangHero(roleid, hero.getxHeroInfo().getKey(), Conv.toByte(4)).call();
					}
//					xhero.setPeiyang1(hero.getiHeroInfo().getTrainMaximum1());
//					xhero.setPeiyang2(hero.getiHeroInfo().getTrainMaximum2());
//					xhero.setPeiyang3(hero.getiHeroInfo().getTrainMaximum3());
//					xhero.setPeiyang4(hero.getiHeroInfo().getTrainMaximum4());
					heroIdList.addFirst(xhero.getKey());
				}
				
				PAddExpHero hero = new PAddExpHero(roleid,heroIdList,999999999,PAddExpHero.OTHER,"");
				hero.call();
				final PAddExpProc addexpProc = new PAddExpProc(roleid, 9999999,PAddExpProc.OTHER,"Cmd_addexp添加");
				addexpProc.call();
				ItemColumn itemcol = Module.getItemColumnByItemId(roleid, 1402010002, false);
				itemcol.addItem(1402010002, 1000, "gm_add", 1);
				itemcol.addItem(1402010004, 1000, "gm_add", 1);
				StageRole stageRole = StageRole.getStageRole(roleid);
				stageRole.onInitStage(-99919);
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