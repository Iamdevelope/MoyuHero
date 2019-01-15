package chuhan.gsp.gm;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ExecutionException;

import chuhan.gsp.award.AwardManager;
import chuhan.gsp.game.SVIPrepay;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;

import xdb.TTable.IWalk;

public class Cmd_viprepay extends GMCommand {

	@Override
	boolean exec(String[] args) {
		final Map<Integer, List<Long>> vipRoles = new HashMap<Integer, List<Long>>();
		long startTime = GameTime.currentTimeMillis();
		xtable.Properties.getTable().walk(new IWalk<Long, xbean.Properties>() {
			@Override
			public boolean onRecord(Long k, xbean.Properties v) {
				if(v.getViplv() > 2) {
					List<Long> oldRoles = vipRoles.get(v.getViplv());
					if(null == oldRoles) {
						oldRoles = new LinkedList<Long>();
						vipRoles.put(v.getViplv(), oldRoles);
					}
					oldRoles.add(k);
				}
				return true;
			}
		});
		StringBuilder sb = new StringBuilder();
		for(int vipLv : vipRoles.keySet()) {
			List<Long> roles = vipRoles.get(vipLv);
			final SVIPrepay sviPrepay = ConfigManager.getInstance().getConf(SVIPrepay.class).get(vipLv);
			if(null == sviPrepay) {
				sb.append("vip" + vipLv + "不发奖!");
				continue;
			}
			sb.append("vip" + vipLv + "共" + roles.size() + "人;");
			for(long roleId : roles) {
				final long rid = roleId;
				try {
					new xdb.Procedure(){
						protected boolean process() throws Exception {
							AwardManager.getInstance().distributeAllAward(rid, sviPrepay.reward, null, false);
							return true;
						};
					}.submit().get();//顺序一个个加
				} catch (InterruptedException e) {
					e.printStackTrace();
				} catch (ExecutionException e) {
					e.printStackTrace();
				}
			}
		}
		sendToGM(sb.toString());long endTime = GameTime.currentTimeMillis();
		GMCommand.logger.info(sb.toString() + "------耗时:" + (endTime - startTime) + "ms");
		
		return true;
	}

	@Override
	String usage() {
		return "//sendvipaward";
	}

}
