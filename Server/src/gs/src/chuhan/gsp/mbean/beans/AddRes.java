package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class AddRes extends AbstractRequestHandler {

	public AddRes(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String idStr = (String) paras.get("id");
			String valueStr = (String) paras.get("value");
			if (null != roleidStr && null != valueStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer id = Integer.valueOf(idStr);
				final Integer value = Integer.valueOf(valueStr);
				if (value == 0) {
					return failedMsg("资源数量等于0毫无意义");
				}
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties) {
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				boolean addSuc = new xdb.Procedure()
				{
					protected boolean process() throws Exception {
						PropRole prole = PropRole.getPropRole(roleid, false);
						if(value>0) {
							DropManager.getInstance().dropAddByOther(id, value, 0, 0, roleid, "gm");
						} else {
							prole.delZiYuan(value, 0, id);
						}
						return true;
					};
				}.submit().get().isSuccess();
				if (!addSuc) {
					return failedMsg("添加资源失败");
				}
				return successMsg();
			} else {
				return failedMsg("需要参数roleid ids grade value");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
