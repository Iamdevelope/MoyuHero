package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.item.PBagExpansion;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class ExpandHeroBag extends AbstractRequestHandler {

	final static byte bagType = 3; // 装备是1， 英雄是3
	final static int expandPerTime = 50;

	public ExpandHeroBag(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String valueStr = (String) paras.get("value");
			if (null != roleidStr && null != valueStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer value = Integer.valueOf(valueStr);
				if (value <= 0) {
					return failedMsg("扩充值必须大于等于0");
				}
				if (value % expandPerTime != 0) {
					return failedMsg("扩充值必须是" + expandPerTime + "的整数倍");
				}
				int times = value / expandPerTime;
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties) {
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				for(int i=0;i<times;i++) {
					boolean expandSuc = new PBagExpansion(roleid, bagType).submit().get().isSuccess();
					if (!expandSuc) {
						return failedMsg("背包扩容失败");
					}
				}
				return successMsg();
			} else {
				return failedMsg("需要参数roleid value");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
