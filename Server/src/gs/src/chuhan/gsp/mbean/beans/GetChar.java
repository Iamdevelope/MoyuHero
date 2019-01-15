package chuhan.gsp.mbean.beans;

import java.util.HashMap;
import java.util.Map;

import chuhan.gsp.exchange.ChargeRole;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class GetChar extends AbstractRequestHandler {

	public GetChar(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String charidStr = (String) paras.get("charid");
			if(null != charidStr) {
				Long charid = Long.valueOf(charidStr);
				xbean.Properties properties = xtable.Properties.select(charid);
				if (null == properties){
					return failedMsg("不存在的玩家charid:" + charidStr);
				}
				Map<Object, Object> result = new HashMap<Object, Object>();
				result.put("return", "true");
				result.put("rolename", properties.getRolename());
				result.put("userid", properties.getUserid());
				result.put("username", properties.getUsername());
				ChargeRole chargeRole = ChargeRole.getChargeRole(charid, true);
				result.put("chargesum", chargeRole.getChargedSum());
				return result;
			} else {
				return failedMsg("需要参数charid");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
