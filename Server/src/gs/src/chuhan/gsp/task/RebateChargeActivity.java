package chuhan.gsp.task;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoAddType;
import chuhan.gsp.item.Chongzhi;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.MsgRole;

import com.goldhuman.Common.Octets;

public class RebateChargeActivity extends AbstractActivity {
	
	private xbean.RebateChargeActivityRole rebateChargeActivityRole;
	
	public RebateChargeActivity(long roleId, SActivityConfig config, boolean readonly, xbean.RebateChargeActivityRole rebateChargeActivityRole) {
		super(roleId, config, readonly);
		this.rebateChargeActivityRole = rebateChargeActivityRole;
	}
	
	public static RebateChargeActivity getChargeActivity(long roleId, SActivityConfig cfg, boolean readonly) {
		if(null == xtable.Properties.select(roleId)) {
			return null;
		}
		if(cfg.getType() != ActivityRole.ACTIVITY_REBATE_CHARGE) {//这里做个防御，以免乱调用
			return null;
		}
		xbean.RebateChargeActivityRole rebateChargeActivityRole;
		if(readonly) {
			rebateChargeActivityRole = xtable.Rebatechargeactivities.select(roleId);
		} else {
			rebateChargeActivityRole = xtable.Rebatechargeactivities.get(roleId);
		}
		
		if(null == rebateChargeActivityRole) {
			if(readonly) {
				rebateChargeActivityRole = xbean.Pod.newRebateChargeActivityRoleData();
			} else {
				rebateChargeActivityRole = xbean.Pod.newRebateChargeActivityRole();
				xtable.Rebatechargeactivities.insert(roleId, rebateChargeActivityRole);
			}
		}
		if(null == rebateChargeActivityRole.getActivities().get(cfg.getId())) {
			xbean.RebateChargeActivity rebateChargeActivity;
			if(readonly) {
				rebateChargeActivity = xbean.Pod.newRebateChargeActivityData();
			} else {
				rebateChargeActivity = xbean.Pod.newRebateChargeActivity();
			}
			rebateChargeActivity.getAwardinfo().put(cfg.getId(), 0);
			rebateChargeActivityRole.getActivities().put(cfg.getId(), rebateChargeActivity);
		}
		
		return new RebateChargeActivity(roleId, cfg, readonly, rebateChargeActivityRole);
	}
	
	@Override
	public int getType() {
		return ActivityRole.ACTIVITY_REBATE_CHARGE;
	}

	/**
	 * 充值调用，直接返利
	 * @param roleId
	 * @param rmb
	 * @return
	 */
	public static boolean rebateCharge(long roleId, int rmb) {
		try {//try掉，防止出错导致不能充值了 然后大家就没工资了
			RebateChargeActivity _this = getCurrentActivity(roleId, false);
			if(null == _this) {//当前没有充值活动,返回true防止外面判断导致回滚
				return true;
			}
			Chongzhi chongzhi = ConfigManager.getInstance().getConf(Chongzhi.class).get(rmb);
			if(null == chongzhi) {
				return true;
			}
			xbean.RebateChargeActivity rebateChargeActivity = _this.rebateChargeActivityRole.getActivities().get(_this.config.getId());
			Integer useNum = rebateChargeActivity.getAwardinfo().get(rmb);
			useNum = null == useNum ? 0 : useNum;
			if(useNum >= chongzhi.times) {//最多领取times次，次数已到上限
				return true;
			}
			if(PropRole.getPropRole(roleId, false).addYuanBao(chongzhi.getYinliang(), YuanBaoAddType.ADD_CASH) != chongzhi.getYinliang()) {
				return true;
			} else {
				rebateChargeActivity.getAwardinfo().put(rmb, useNum + 1);
				MsgRole msgRole = MsgRole.getMsgRole(roleId, false);
				List<String> strs = new LinkedList<String>();
				strs.add(String.valueOf(rmb));
				strs.add(String.valueOf(chongzhi.getYinliang()));
				strs.add(String.valueOf(chongzhi.times - rebateChargeActivity.getAwardinfo().get(rmb)));
				msgRole.addSysMsgWithSP(424, strs, null, 0, MsgRole.MST_TYPE_SYS);
			}
		} catch(Exception e) {
			e.printStackTrace();
		}
		
		return true;
	}
	
	/**
	 * 获取当前时间的充值活动对象
	 * @return
	 */
	public static RebateChargeActivity getCurrentActivity(long roleId, boolean readonly) {
		Map<Integer,SActivityConfig> activityConfigMap = ConfigManager.getInstance().getConf(SActivityConfig.class);
		Collection<SActivityConfig>  c = activityConfigMap.values();
		for(SActivityConfig activityConfig : c) {
			if(activityConfig.getType() == ActivityRole.ACTIVITY_REBATE_CHARGE) {//类型为充值活动的才算哦
				RebateChargeActivity _this = getChargeActivity(roleId, activityConfig, readonly);
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
		return null;
	}

}
