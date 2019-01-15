package chuhan.gsp.play.endlessbattle;

import java.util.List;
import java.util.concurrent.ConcurrentHashMap;

import chuhan.gsp.log.Logger;


/////暂时没用到

public class MonsterManager{
	
	public static Logger logger = Logger.getLogger(MonsterManager.class);
	static private MonsterManager instance = null;
	
	private MonsterManager(){}
	public static MonsterManager getInstance() {
		if(instance == null)
		{
			instance = new MonsterManager();
		}
		return instance;
	}
	
	public ConcurrentHashMap<Long,endlessMonster> monsterMap = new ConcurrentHashMap<Long,endlessMonster>(); 
	
	
	
	
	
	public class endlessMonster{
		ConcurrentHashMap<Integer,List<Integer>> roleMonsterMap = new ConcurrentHashMap<Integer,List<Integer>>(); 
		endlessMonster(){}
	}
}
