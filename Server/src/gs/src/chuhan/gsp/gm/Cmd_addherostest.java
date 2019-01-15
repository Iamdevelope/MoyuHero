package chuhan.gsp.gm;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.ExecutionException;

import chuhan.gsp.hero.PAddHero;
import chuhan.gsp.item.hero01;
import chuhan.gsp.main.ConfigManager;

public class Cmd_addherostest extends GMCommand {

	@Override
	boolean exec(String[] args) {
		long roleid = getGmroleid();
		if(args.length < 1) {
			sendToGM("参数格式错误：" + usage());
		}
		List<Integer> heroIds = new ArrayList<Integer>();
		TreeMap<Integer,hero01> heroInitMap = ConfigManager.getInstance().getConf(hero01.class);
/*		heroIds.add(Integer.valueOf(1403100033));
		heroIds.add(Integer.valueOf(1403100175));
		heroIds.add(Integer.valueOf(1403100023));
		heroIds.add(Integer.valueOf(1403100134));
		heroIds.add(Integer.valueOf(1403100185));*/

		for(Map.Entry<Integer, hero01> entry : heroInitMap.entrySet()) {
			PAddHero pAddHero = new PAddHero(roleid, entry.getValue().getId(),1);
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
		return "//addherostest";
	}

}
