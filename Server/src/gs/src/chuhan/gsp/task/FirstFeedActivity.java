package chuhan.gsp.task;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoAddType;
import chuhan.gsp.item.shouchonghuodong;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.util.DateUtil;

import com.goldhuman.Common.Octets;

public class FirstFeedActivity extends AbstractActivity {
	
	public static final String REAWARD_REASON = "chargeActivity_reward";
	private xbean.FirstFeedActivityRole activityRole;
	
	private FirstFeedActivity(long roleId, SActivityConfig config, boolean readonly, xbean.FirstFeedActivityRole activityRole) {
		super(roleId, config, readonly);
		this.activityRole = activityRole;
	}
	
	public static FirstFeedActivity getFirstFeedActivity(long roleId, SActivityConfig cfg, boolean readonly) {
		if(null == xtable.Properties.select(roleId)) {
			return null;
		}
		if(cfg.getType() != ActivityRole.ACTIVITY_FIRST_CHARGE_FEED) {//这里做个防御，以免乱调用
			return null;
		}
		xbean.FirstFeedActivityRole activityRole;
		if(readonly) {
			activityRole = xtable.Firstfeedactivities.select(roleId);
		} else {
			activityRole = xtable.Firstfeedactivities.get(roleId);
		}
		
		if(null == activityRole) {
			if(readonly) {
				activityRole = xbean.Pod.newFirstFeedActivityRoleData();
			} else {
				activityRole = xbean.Pod.newFirstFeedActivityRole();
				xtable.Firstfeedactivities.insert(roleId, activityRole);
			}
		}
		if(null == activityRole.getActivities().get(cfg.getId())) {
			xbean.FirstFeedActivity activity;
			if(readonly) {
				activity = xbean.Pod.newFirstFeedActivityData();
			} else {
				activity = xbean.Pod.newFirstFeedActivity();
			}
			activity.setChargetime(0);
			activity.setIsgainaward(false);
			activityRole.getActivities().put(cfg.getId(), activity);
		}
		
		return new FirstFeedActivity(roleId, cfg, readonly, activityRole);
	}
	
	/**
	 * 充值调用，判断并给出首冲奖励
	 * @param roleId
	 * @param rmb
	 * @return
	 */
	public static boolean sendFirstFeed(long roleId, int rmb) {
		try {//try掉，防止出错导致不能充值了 然后大家就没工资了
			FirstFeedActivity _this = getCurrentActivity(roleId, false);
			if(null == _this) {//当前没有充值活动,返回true防止外面判断导致回滚
				return true;
			}
			xbean.FirstFeedActivity activity = _this.activityRole.getActivities().get(_this.config.getId());
			if(!activity.getIsgainaward()) {
				shouchonghuodong schargeconfig = ConfigManager.getInstance().getConf(shouchonghuodong.class).get(1);
				if(rmb == schargeconfig.chargenum) {
					PropRole prole = PropRole.getPropRole(roleId, false);
					if(prole.addYuanBao(schargeconfig.getyb, YuanBaoAddType.ADD_CASH) != schargeconfig.getyb) {
						return false;
					}
					activity.setChargetime(GameTime.currentTimeMillis());
					activity.setIsgainaward(true);
				}
			}
		} catch(Exception e) {
			e.printStackTrace();
		}
		
		return true;
	}
	
	/**
	 * 判断并给出每日赠送的元宝
	 * @param yuanbao
	 * @return
	 */
	public static boolean sendFeedPreDay(long roleId) {
		try {
			FirstFeedActivity _this = getCurrentActivity(roleId, false);
			if(null == _this) {
				return true;
			}
			xbean.FirstFeedActivity activity = _this.activityRole.getActivities().get(_this.config.getId());
			if(activity.getIsgainaward() && !DateUtil.inTheSameDay(activity.getChargetime(), GameTime.currentTimeMillis())) {
				if(!DateUtil.inTheSameDay(activity.getRebatetime(), GameTime.currentTimeMillis())) { 
					shouchonghuodong schargeconfig = ConfigManager.getInstance().getConf(shouchonghuodong.class).get(1);
					int leftTime = schargeconfig.times - (DateUtil.getCurrentDay(GameTime.currentTimeMillis()) - DateUtil.getCurrentDay(activity.getChargetime()));
					if(leftTime >= 0 && leftTime < schargeconfig.times) {
						PropRole prole = PropRole.getPropRole(roleId, false);
						if(prole.addYuanBao(schargeconfig.everydayyb, YuanBaoAddType.ADD_CASH) != schargeconfig.everydayyb) {
							return false;
						}
						MsgRole msgRole = MsgRole.getMsgRole(roleId, false);
						List<String> strs = new LinkedList<String>();
						strs.add(String.valueOf(schargeconfig.chargenum));
						strs.add(String.valueOf(schargeconfig.everydayyb));
						strs.add(String.valueOf(leftTime));
						msgRole.addSysMsgWithSP(425, strs, null, 0, MsgRole.MST_TYPE_SYS);
						activity.setRebatetime(GameTime.currentTimeMillis());
					} else {
						activity.setIsgainaward(false);
					}
				}
			}
		} catch(Exception e) {
			e.printStackTrace();
		}
		
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
	public static FirstFeedActivity getCurrentActivity(long roleId, boolean readonly) {
		Map<Integer,SActivityConfig> activityConfigMap = ConfigManager.getInstance().getConf(SActivityConfig.class);
		Collection<SActivityConfig>  c = activityConfigMap.values();
		for(SActivityConfig activityConfig : c) {
			if(activityConfig.getType() == ActivityRole.ACTIVITY_FIRST_CHARGE_FEED) {//类型为充值活动的才算哦
				FirstFeedActivity _this = getFirstFeedActivity(roleId, activityConfig, readonly);
				if(_this.isActive() || _this.activityRole.getActivities().get(_this.config.getId()).getIsgainaward()) {
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
