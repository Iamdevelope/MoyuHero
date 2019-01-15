/**
 * Class: Cmd_award.java
 * Package: knight.gsp.gm
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2012-2-21 		yesheng
 *
 * Copyright (c) 2012, Perfect World All Rights Reserved.
*/

package chuhan.gsp.gm;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.mbean.Reload;

public class Cmd_reload extends GMCommand {
	@Override
	boolean exec(final String[] args) {
		new xdb.Procedure() {
			@Override
			protected boolean process() throws Exception {
				try {
					if (args.length >= 1) {
						sendToGM(new Reload().reload());
						return true;
					}

					// 重load文件
					ConfigManager.getInstance().init();

//					chuhan.gsp.buff.Module.getInstance().reload();

//					chuhan.gsp.award.Module.getInstance().reload();

				} catch (Exception e) {
					sendToGM("reload error");
					e.printStackTrace();
				}
				return true;
			}
		}.submit();
		return true;
	}

	@Override
	String usage() {
		return "//reload hot ： 根据hotfix文件来热加载   ||  //reload 全重载模块";
	}

}

