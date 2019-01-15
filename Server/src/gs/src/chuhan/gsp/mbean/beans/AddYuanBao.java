package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoAddType;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class AddYuanBao extends AbstractRequestHandler {

	public AddYuanBao(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String yuanbaoStr = (String) paras.get("yuanbao");
			if(null != roleidStr && null != yuanbaoStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer yuanbao = Integer.valueOf(yuanbaoStr);
				if(yuanbao <= 0) {
					return failedMsg("参数yuanbao必须大于0");
				}
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties){
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				boolean addSuc = new xdb.Procedure() {
					protected boolean process() throws Exception {
						PropRole prole = PropRole.getPropRole(roleid, false);
						if(yuanbao != prole.addYuanBao(yuanbao, YuanBaoAddType.CUSTOMERSERVICE_ADD)) {
							return false;
						}
						return true;
					};
				}.submit().get().isSuccess();
				if(!addSuc) {
					return failedMsg("添加元宝失败");
				}
				return successMsg();
			} else {
				return failedMsg("需要参数roleid yuanbao");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
