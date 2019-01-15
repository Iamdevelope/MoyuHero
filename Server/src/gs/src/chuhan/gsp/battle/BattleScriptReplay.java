package chuhan.gsp.battle;

import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

public class BattleScriptReplay {
	public static int MAX_SCRIPT_NUM = 10;
	
	public static Map<Long,RoleScriptReplay> roles = new TreeMap<Long, BattleScriptReplay.RoleScriptReplay>();
	
	public static synchronized void addScript(long roleId, SSendBattleScript snd)
	{
		RoleScriptReplay role = roles.get(roleId);
		if(role == null)
		{
			role = new RoleScriptReplay(roleId);
			roles.put(roleId, role);
		}
		role.addScript(snd);
	}
	
	public static synchronized void sendScript(long roleId, long sndroleId, int verseindex)
	{
		RoleScriptReplay role = roles.get(roleId);
		if(role == null)
			return;
		role.sendScript(sndroleId, verseindex);
	}
	
	public static class RoleScriptReplay
	{
		public final long roleId;
		private List<SSendBattleScript> scripts = new LinkedList<SSendBattleScript>();
		public RoleScriptReplay(long roleId) {
			this.roleId = roleId;
		}
		
		public List<SSendBattleScript> getScripts() {
			return scripts;
		}
		
		public void addScript(SSendBattleScript snd)
		{
			scripts.add(snd);
			if(scripts.size() > MAX_SCRIPT_NUM)
				scripts.remove(0);
		}
		
		public void sendScript(long sndroleId, int verseindex)
		{
			if(verseindex > scripts.size())
				return;
			SSendBattleScript snd = scripts.get(scripts.size() - verseindex);
			gnet.link.Onlines.getInstance().send(sndroleId, snd);
		}
		
	}
}
