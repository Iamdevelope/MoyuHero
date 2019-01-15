package chuhan.gsp.award;

import gnet.link.Role;

import java.text.ParseException;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import xdb.Transaction;


import chuhan.gsp.game.sxitongjiangli;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.PAddCompense;

public class CompenseManager {
	public static CompenseManager instance = new CompenseManager();
	private CompenseManager() {

	}

	public static CompenseManager getInstance() {

		return instance;
	}
	
	public static void reload() throws Exception
	{
		CompenseManager newInstance = new CompenseManager();
		newInstance.init();
		newInstance.compare(instance);
		instance = newInstance;
	}
	
	private Map<Integer,CompenseConfig> compensecfgs = new TreeMap<Integer, CompenseConfig>();
	
	public void init() throws ParseException
	{
		for(sxitongjiangli scfg : ConfigManager.getInstance().getConf(sxitongjiangli.class).values())
		{
			compensecfgs.put(scfg.id , new CompenseConfig(scfg));
		}
	}
	
	public Map<Integer,CompenseConfig> getCompenseConfigs()
	{
		return compensecfgs;
	}
	
	public void compare(CompenseManager oldinstance)
	{
		try {
			List<CompenseConfig> newcfgs = new LinkedList<CompenseConfig>();
			for (CompenseConfig newcfg : compensecfgs.values()) {
				CompenseConfig oldcfg = oldinstance.getCompenseConfigs().get(
						newcfg.id);
				if (oldcfg == null) {
					newcfgs.add(newcfg);
				} else if (!oldcfg.enable && newcfg.enable) {
					newcfgs.add(newcfg);
				}
			}
			List<Role> roles = new LinkedList<Role>();
			roles.addAll(gnet.link.Onlines.getInstance().getRoles());
			for (Role role : roles)
			{
				PAddCompense p = new PAddCompense(role.getRoleid(), newcfgs);
				if(Transaction.current() != null)
					xdb.Procedure.pexecuteWhileCommit(p);
				else
					p.submit();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
}
