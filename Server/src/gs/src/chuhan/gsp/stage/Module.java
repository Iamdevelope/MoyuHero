package chuhan.gsp.stage;

import chuhan.gsp.main.ModuleManager;
import chuhan.gsp.main.ReloadResult;

public class Module implements chuhan.gsp.main.ModuleInterface{


	public static Module getInstance() {
		return (Module)ModuleManager.getInstance().getModuleByName("stage");
	}
	
	@Override
	public void exit() {
		
	}

	@Override
	public void init() throws Exception {
		
	}

	@Override
	public ReloadResult reload() throws Exception {
		return null;
	}

}
