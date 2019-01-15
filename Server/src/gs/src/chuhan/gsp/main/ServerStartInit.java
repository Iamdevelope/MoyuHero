package chuhan.gsp.main;

import org.apache.log4j.Logger;


public class ServerStartInit {
	private static final Logger logger = Logger.getLogger(ServerStartInit.class);
	private ServerStartInit() {}
	
	public static void init() {
		if(!Gs.isInMainThread()) throw new RuntimeException("不在主线程中");
//		new PCalLadderInit().submit();
//		logger.info("PCalLadderInit end...");
//		RaidBossRole.init();
//		logger.info("RaidBossRoleInit end...");
//		CampRole.init();
//		logger.info("CampRoleInit end...");
	}
}
