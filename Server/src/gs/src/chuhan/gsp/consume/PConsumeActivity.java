package chuhan.gsp.consume;

import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

import xbean.ConsumeActivity;
import xbean.ConsumeActivityRole;
import chuhan.gsp.item.AddItemResult;
import chuhan.gsp.consume.Module;
import chuhan.gsp.item.leijixiaohaos;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.util.DateUtil;

/**
 * 累计消费活动，此活动因为没有界面，所以不同于task里的活动。需要单独重新写
 *
 */
public class PConsumeActivity{
	public static final String REAWARD_REASON = "consumeActivity_reward";
	
	public static boolean process(long roleId, int gold) throws Exception {
		Map<Integer, Map<Integer, leijixiaohaos>> prizeMap = Module.getInstance().getPrizeMap();
		
		//如果人物活动信息为空，则先按活动id构建人物信息
		ConsumeActivityRole consumeActivityRole = xtable.Consumeactivities.get(roleId);
		if(consumeActivityRole == null){
			consumeActivityRole = xbean.Pod.newConsumeActivityRole();
			xtable.Consumeactivities.insert(roleId, consumeActivityRole);
			consumeActivityRole =  xtable.Consumeactivities.get(roleId);
		}
		
		for(Map.Entry<Integer, Map<Integer, leijixiaohaos>> prize : prizeMap.entrySet()){
			if(prize.getValue() == null || prize.getValue().isEmpty()){
				continue;
			}
			
			String beginTime = prize.getValue().values().iterator().next().getStarttime();
			String endTime = prize.getValue().values().iterator().next().getEndtime();
			
			//活动未开始直接返回
			if(!DateUtil.isRunning(beginTime, endTime)){
				continue;
			}
			
			if(!consumeActivityRole.getActivities().containsKey(prize.getKey())){
				ConsumeActivity comsumeActivity = xbean.Pod.newConsumeActivity();
				comsumeActivity.setActivityid(prize.getKey());
				comsumeActivity.setTotalconsume(0);
//				comsumeActivity.getIsgainaward().put(0, false);
				
				consumeActivityRole.getActivities().put(prize.getKey(), comsumeActivity);
			}
			
			//下面是真正的处理
			ConsumeActivity comsumeActivity = consumeActivityRole.getActivities().get(prize.getKey());
			int total = comsumeActivity.getTotalconsume() + gold;
			comsumeActivity.setTotalconsume(total);
			
			for(Map.Entry<Integer, leijixiaohaos> p : prize.getValue().entrySet()){
				if(total >= p.getKey() && (!comsumeActivity.getIsgainaward().containsKey(p.getKey()) || comsumeActivity.getIsgainaward().get(p.getKey()) != true)){
					//发奖
					int itemId = Integer.parseInt(p.getValue().getReward());
					chuhan.gsp.item.ItemColumn itemColumn = chuhan.gsp.item.Module.getItemColumnByItemId(roleId, itemId, false);
					AddItemResult result = itemColumn.addItem(itemId, 1, REAWARD_REASON, 0);
					if(!result.isSuccess()) {//添加物品失败记得回滚
						return false;
					}
					
					xdb.Procedure.psendWhileCommit(roleId, result.getSShowAddItem());
					//发奖结束
					comsumeActivity.getIsgainaward().put(p.getKey(), true);
					//发送说明邮件
					MsgRole msgRole = MsgRole.getMsgRole(roleId, false);
					List<String> strs = new LinkedList<String>();
					strs.add(String.valueOf(total));
					strs.add(String.valueOf(p.getValue().getItemnum()));
					msgRole.addSysMsgWithSP(428, strs, null, 0, MsgRole.MST_TYPE_SYS);
				}
			}
//			xtable.Consumeactivities.insert(roleId, consumeActivityRole);
		}
		return true;
	}
}
