package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class SetVipLv extends AbstractRequestHandler {

	public SetVipLv(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String viplvStr = (String) paras.get("viplv");
			if(null != roleidStr && null != viplvStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer viplv = Integer.valueOf(viplvStr);
				if(viplv < 0 || viplv > 20) {
					return failedMsg("参数viplv必须在范围[0,20]");
				}
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties){
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				new xdb.Procedure() {
					protected boolean process() throws Exception {
						PropRole prole = PropRole.getPropRole(roleid, false);
						prole.setVipLevel(viplv);
						return true;
					};
				}.submit().get();
				return successMsg();
			} else {
				return failedMsg("需要参数roleid viplv");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
