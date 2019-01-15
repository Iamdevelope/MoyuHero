package chuhan.gsp.mbean;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.ModuleManager;
import chuhan.gsp.main.ReloadResult;

public class Reload implements ReloadMXBean
{
	@Override
	public String reload()
	{
		ReloadResult result = new ReloadResult(true);
		try
		{
			result.appendResult(ConfigManager.getInstance().reload());
			if(!result.isSuccess())
				return result.getMsg();
		}
		catch(Exception e)
		{
			Module.logger.error("Reload unknown exception",e);
			return "Reload unknown exception, refer to log";
		}
		result.appendResult(ModuleManager.getInstance().reload());
		if(!result.isSuccess())
			return result.getMsg();
		
		result.appendMsg("Reload success. \n");
		Module.logger.info(result.getMsg());
		return result.getMsg();
	}
	
}
