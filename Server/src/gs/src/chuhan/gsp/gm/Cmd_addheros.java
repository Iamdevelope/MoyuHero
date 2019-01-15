package chuhan.gsp.gm;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ExecutionException;

import chuhan.gsp.hero.PAddHero;

public class Cmd_addheros extends GMCommand {

	@Override
	boolean exec(String[] args) {
		long roleid = getGmroleid();
		if(args.length < 1) {
			sendToGM("参数格式错误：" + usage());
		}
		List<Integer> heroIds = new ArrayList<Integer>();
		for(String id : args) {
			heroIds.add(Integer.valueOf(id));
		}
		for(int heroId : heroIds) {
			PAddHero pAddHero = new PAddHero(roleid, heroId,1);
			try {
				pAddHero.submit().get();
			} catch (InterruptedException e) {
				e.printStackTrace();
			} catch (ExecutionException e) {
				e.printStackTrace();
			}
		}
		return true;
	}

	@Override
	String usage() {
		return "//addheros [heroId1 heroId2 ...]";
	}

}
