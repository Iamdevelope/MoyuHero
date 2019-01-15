package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.mbean.AbstractRequestHandler;
import chuhan.gsp.play.endlessbattle.EndlessinfoColumns;
import chuhan.gsp.play.ranking.endlessRanking;

public class SetEndless extends AbstractRequestHandler {

	public SetEndless(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String guankaStr = (String) paras.get("guanka");
			if(null != roleidStr && null != guankaStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer guanka = Integer.valueOf(guankaStr);
				if(guanka < 0) {
					return failedMsg("参数guanka必须是正数");
				}
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties){
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				boolean addSuc = new xdb.Procedure() {
					protected boolean process() throws Exception {
						EndlessinfoColumns endcol = EndlessinfoColumns.getEndLessColumn(roleid, false);
						endcol.xcolumn.setGroupnum(guanka);
						endcol.xcolumn.setIsend(1);
						endcol.sendTodayEndless();
						endlessRanking.getInstance().addInRank(endcol);
						return true;
					};
				}.submit().get().isSuccess();
				if(!addSuc) {
					return failedMsg("设置极限关卡失败");
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
