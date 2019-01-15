package chuhan.gsp.task;

import java.util.Collection;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;

import chuhan.gsp.award.AddItem;
import chuhan.gsp.item.AddItemResult;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.SShowAddItem;
import chuhan.gsp.item.ShowItemData;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.OctetsStream;

public class ChargeActivity extends AbstractActivity {
	public static final String REAWARD_REASON = "chargeActivity_reward";
	private xbean.ChargeActivityRole chargeActivityRole;
	
	private ChargeActivity(long roleId, SActivityConfig config, boolean readonly, xbean.ChargeActivityRole chargeActivityRole) {
		super(roleId, config, readonly);
		this.chargeActivityRole = chargeActivityRole;
	}
	
	public static ChargeActivity getChargeActivity(long roleId, SActivityConfig cfg, boolean readonly) {
		if(null == xtable.Properties.select(roleId)) {
			return null;
		}
		if(cfg.getType() != ActivityRole.ACTIVITY_CHARGE) {//这里做个防御，以免乱调用
			return null;
		}
		xbean.ChargeActivityRole chargeActivityRole;
		if(readonly) {
			chargeActivityRole = xtable.Chargeactivities.select(roleId);
		} else {
			chargeActivityRole = xtable.Chargeactivities.get(roleId);
		}
		
		if(null == chargeActivityRole) {
			if(readonly) {
				chargeActivityRole = xbean.Pod.newChargeActivityRoleData();
			} else {
				chargeActivityRole = xbean.Pod.newChargeActivityRole();
				xtable.Chargeactivities.insert(roleId, chargeActivityRole);
			}
		}
		if(null == chargeActivityRole.getActivities().get(cfg.getId())) {
			xbean.ChargeActivity chargeActivity;
			if(readonly) {
				chargeActivity = xbean.Pod.newChargeActivityData();
			} else {
				chargeActivity = xbean.Pod.newChargeActivity();
			}
			chargeActivity.setActivityid(cfg.getId());
			chargeActivity.setTotalcharge(0);
			chargeActivityRole.getActivities().put(cfg.getId(), chargeActivity);
		}
		
		return new ChargeActivity(roleId, cfg, readonly, chargeActivityRole);
	}
	
	/**
	 * 充值调用，记录充值总数
	 * @param roleId
	 * @param rmb
	 * @return
	 */
	public static boolean charge(long roleId, int rmb) {
		try {//try掉，防止出错导致不能充值了 然后大家就没工资了
			ChargeActivity _this = getCurrentActivity(roleId, false);
			if(null == _this) {//当前没有充值活动,返回true防止外面判断导致回滚
				return true;
			}
			xbean.ChargeActivity chargeActivity = _this.chargeActivityRole.getActivities().get(_this.config.getId());
			chargeActivity.setTotalcharge(chargeActivity.getTotalcharge() + rmb);
		} catch(Exception e) {
			e.printStackTrace();
		}
		
		return true;
	}
	
	/**
	 * 领取奖励
	 * @param yuanbao
	 * @return
	 */
	public boolean getReward(int yuanbao) {
		//充值活动的奖励ID只有一条始终=1，所以下面写死了
		schargeconfig schargeconfig = ConfigManager.getInstance().getConf(schargeconfig.class).get(1);
		int index = -1;//奖励的位置
		for(int i = 0, size = schargeconfig.yuanbao.size(); i < size; i ++) {
			if(schargeconfig.yuanbao.get(i) == yuanbao) {
				index = i;
				break;
			}
		}
		if(index == -1) {//没有找到对应的奖励
			return false;
		}
		
		xbean.ChargeActivity chargeActivity = chargeActivityRole.getActivities().get(this.config.getId());
		if(chargeActivity.getTotalcharge() < yuanbao) {//没有充值这么多
			return false;
		}
		Map<Integer, Boolean> isGains = chargeActivity.getIsgainaward();//各个金额的领取情况
		if(isGains.get(yuanbao) != null && isGains.get(yuanbao)) {//该次活动的该奖励已经领取
			return false;
		}
		
		//发放奖励
		int itemId = schargeconfig.items.get(index);
		chuhan.gsp.item.ItemColumn itemColumn = Module.getItemColumnByItemId(this.roleId, itemId, readonly);
		AddItemResult result = itemColumn.addItem(itemId, 1, REAWARD_REASON, 0);
		if(!result.isSuccess()) {//添加物品失败记得回滚
			return false;
		}
		xdb.Procedure.psendWhileCommit(roleId, result.getSShowAddItem());
    		
		//标记为已领取
		isGains.put(yuanbao, true);
		notifyRefresh();
		
		return true;
	}
	
	@Override
	public int getType() {
		return ActivityRole.ACTIVITY_CHARGE;
	}
	
	/**
	 * 获取当前时间的充值活动对象
	 * @return
	 */
	public static ChargeActivity getCurrentActivity(long roleId, boolean readonly) {
		Map<Integer,SActivityConfig> activityConfigMap = ConfigManager.getInstance().getConf(SActivityConfig.class);
		Collection<SActivityConfig>  c = activityConfigMap.values();
		for(SActivityConfig activityConfig : c) {
			if(activityConfig.getType() == ActivityRole.ACTIVITY_CHARGE) {//类型为充值活动的才算哦
				ChargeActivity _this = getChargeActivity(roleId, activityConfig, readonly);
				if(_this.isActive()) {
					return _this;
				} else {
					continue;
				}
			}
		}
		
		return null;
	}

	@Override
	public Octets getDataOctets() {
		ChargeActivityView chargeActivityView = new ChargeActivityView();
		xbean.ChargeActivity chargeActivity = chargeActivityRole.getActivities().get(this.config.getId());
		chargeActivityView.totalcharge = chargeActivity.getTotalcharge();
		Iterator<Entry<Integer, Boolean>> it = chargeActivity.getIsgainawardAsData().entrySet().iterator();
		while(it.hasNext()) {
			Entry<Integer, Boolean> en = it.next();
			if(en.getValue()) {//领取过的才传给客户端
				chargeActivityView.isgain.add(en.getKey());
			}
		}
		chargeActivityView.endtime = DateUtil.parseDate(this.config.getEnd());
		
		return chargeActivityView.marshal(new OctetsStream());
	}

}
