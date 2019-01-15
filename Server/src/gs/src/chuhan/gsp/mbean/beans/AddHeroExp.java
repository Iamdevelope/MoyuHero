package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.hero.PAddExpHero;
import chuhan.gsp.hero.PAddHero;
import chuhan.gsp.mbean.AbstractRequestHandler;
import java.util.ArrayList;
import java.util.List;
import chuhan.gsp.hero.PAddExpHero;

public class AddHeroExp extends AbstractRequestHandler {

	public AddHeroExp(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String idStr = (String) paras.get("id");
			String valueStr = (String) paras.get("value");
			if (null != roleidStr && null != idStr && null != valueStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer hero_id = Integer.valueOf(idStr);
				final Integer exp = Integer.valueOf(valueStr);
				if (hero_id <= 0) {
					return failedMsg("英雄编号必须大于0");
				}
				if (exp <= 0) {
					return failedMsg("经验值必须大于0");
				}
				java.util.LinkedList<Integer> herokeylist = new java.util.LinkedList<Integer>();
				herokeylist.add(hero_id);
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties) {
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				new xdb.Procedure()
				{
					protected boolean process() throws Exception {
						PAddExpHero hero = new PAddExpHero(roleid,herokeylist,exp,PAddExpHero.OTHER,"");
						hero.call();
						return true;
					};
				}.submit();
				return successMsg();
			} else {
				return failedMsg("需要参数roleid ids value");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
