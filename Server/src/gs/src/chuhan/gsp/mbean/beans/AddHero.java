package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.hero.PAddHero;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class AddHero extends AbstractRequestHandler {

	public AddHero(String name) {
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
				if (value <= 0) {
					return failedMsg("英雄数量必须大于0");
				}
				if (grade <= 0) {
					return failedMsg("英雄等级必须大于0");
				}
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties) {
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				for(int i=0;i<value;i++) {
					boolean addSuc = new PAddHero(roleid, id, grade).submit().get().isSuccess();
					if (!addSuc) {
						return failedMsg("添加英雄失败");
					}
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
