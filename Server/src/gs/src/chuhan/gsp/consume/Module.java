package chuhan.gsp.consume;

import java.util.Map;
import java.util.TreeMap;

import chuhan.gsp.item.leijixiaohaos;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.ModuleInterface;
import chuhan.gsp.main.ModuleManager;
import chuhan.gsp.main.ReloadResult;


public class Module implements ModuleInterface {
	Map<Integer, Map<Integer, leijixiaohaos>> prizeMap = new TreeMap<Integer, Map<Integer,leijixiaohaos>>();
	public static Module getInstance() {
		return (Module)ModuleManager.getInstance().getModuleByName("consume");
	}
	
    @Override
    public void exit() {
    }

    @Override
    public void init() throws Exception {
    	TreeMap<Integer, leijixiaohaos> consumeMap = ConfigManager.getInstance().getConf(leijixiaohaos.class);
    	for(Map.Entry<Integer, leijixiaohaos> consume : consumeMap.entrySet()){
    		if(!prizeMap.containsKey(consume.getValue().getHuodong())){
    			prizeMap.put(consume.getValue().getHuodong(), new TreeMap<Integer, leijixiaohaos>());
    		}
    		prizeMap.get(consume.getValue().getHuodong()).put(consume.getValue().yuanbao, consume.getValue());
    	}
    }

	@Override
    public ReloadResult reload() throws Exception {
        return new ReloadResult(false);
    }

	public Map<Integer, Map<Integer, leijixiaohaos>> getPrizeMap() {
		return prizeMap;
	}
}
