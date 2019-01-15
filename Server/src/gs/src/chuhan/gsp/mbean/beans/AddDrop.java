package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.mbean.AbstractRequestHandler;
import chuhan.gsp.award.DropManager;

public class AddDrop extends AbstractRequestHandler {

	public AddDrop(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String idStr = (String) paras.get("id");
			String gradeStr = (String) paras.get("grade");
			String valueStr = (String) paras.get("value");
			if (null != roleidStr && null != valueStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer id = Integer.valueOf(idStr);
				final Integer grade = Integer.valueOf(gradeStr);
				final Integer value = Integer.valueOf(valueStr);
				if (value == 0) {
					return failedMsg("数量等于0毫无意义");
				}
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties) {
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				boolean addSuc = new xdb.Procedure()
				{
					protected boolean process() throws Exception {
						DropManager.getInstance().dropAddByOther(id, value, grade, 0, roleid, "gm");
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
