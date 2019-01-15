package chuhan.gsp.mbean.beans;

import java.util.List;
import java.util.Map;

import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class UserInfo extends AbstractRequestHandler {

	public UserInfo(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			if(null != roleidStr) {
				final Long roleid = Long.valueOf(roleidStr);
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties){
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				String heros = "";
				HeroColumn herocol = HeroColumn.getHeroColumn(roleid, true);
				List<xbean.Hero> xherolist = herocol.getxcolumn().getHeroes();
				for(xbean.Hero hero : xherolist)
				{
					if(heros == "") {
						heros = hero.getKey() + "," + hero.getHeroid() + "," + hero.getHeroexp() + "," + hero.getHerolevel();
					} else {
						heros = heros + ";" + hero.getKey() + "," + hero.getHeroid() + "," + hero.getHeroexp() + "," + hero.getHerolevel();
					}
					System.out.println(hero.getKey()+","+hero.getHeroid());
				}
				Map<Object, Object> successMsgMap = successMsg();
				successMsgMap.put("roleid", roleid);
				successMsgMap.put("rolename", properties.getRolename());
				successMsgMap.put("userid", properties.getUserid());
				successMsgMap.put("username", properties.getUsername());
				successMsgMap.put("plat", properties.getPlattypestr());
				successMsgMap.put("mac", properties.getMac());
				successMsgMap.put("os", properties.getOstype());
				successMsgMap.put("level", properties.getLevel());
				successMsgMap.put("viplv", properties.getViplv());
				successMsgMap.put("vipexp", properties.getVipexp());
				successMsgMap.put("ti", properties.getTi());
				successMsgMap.put("tichangetime", properties.getTichangetime());
				successMsgMap.put("gold", properties.getGold());
				successMsgMap.put("yuanbao", properties.getYuanbao());
				successMsgMap.put("heros", heros);
				return successMsgMap;
			} else {
				return failedMsg("需要参数roleid viplv");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
