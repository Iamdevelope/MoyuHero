package chuhan.gsp.gm;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ExecutionException;

import chuhan.gsp.hero.PAddHero;
import chuhan.gsp.item.SHero;
import chuhan.gsp.main.ConfigManager;

public class Cmd_addallhero extends GMCommand {

	@Override
	boolean exec(String[] args) {
		long roleid = getGmroleid();
		if(args.length != 1) {
			sendToGM("参数格式错误："+usage());
		}
		int stackNum = Integer.valueOf(args[0]);
		Map<Integer, SHero> heros = ConfigManager.getInstance().getConf(SHero.class);
		List<Integer> heroIds = new ArrayList<Integer>();
		for(Integer heroId : heros.keySet()) {
			heroIds.add(heroId);
		}
		Collections.sort(heroIds);
		int size = heroIds.size();
		stackNum --;
		int fromIndex = stackNum < 0 ? 0 : stackNum;
		int toIndex = (stackNum + 20) > size ? size : (stackNum + 20);
		fromIndex = fromIndex > toIndex ? toIndex : fromIndex;
		List<Integer> hids = heroIds.subList(fromIndex, toIndex);
		for(int heroId : hids) {
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
		return "//addallhero [startIndex]";
	}

}
