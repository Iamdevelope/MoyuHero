/**
 * Class: Module.java
 * Package: knight.gsp.activity.award
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2012-7-28 		yesheng
 *
 * Copyright (c) 2012, Perfect World All Rights Reserved.
*/

package chuhan.gsp.award;

import chuhan.gsp.main.ModuleInterface;
import chuhan.gsp.main.ModuleManager;
import chuhan.gsp.main.ReloadResult;

public class Module implements ModuleInterface {

	public static Module getInstance()
	{
		//不再用以上的单例模式，从ModuleManager获得，由其管理
		return ((Module)ModuleManager.getInstance().getModuleByName("award"));
	}
	
	@Override
	public void exit() {

	}

	@Override
	public void init() throws Exception {
		AwardManager.getInstance().init();
		CompenseManager.getInstance().init();
	}

	@Override
	public ReloadResult reload() throws Exception {
       try {
    	   AwardManager.reload();
    	   CompenseManager.reload();
    	   return new ReloadResult(true);
	  } catch (Exception e) {
		AwardManager.logger.error(e);
		return new ReloadResult(false, "reload award failed");
	  }
	
	}

}

