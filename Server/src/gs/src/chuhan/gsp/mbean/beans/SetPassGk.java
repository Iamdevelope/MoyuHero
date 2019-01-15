package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.mbean.AbstractRequestHandler;
import chuhan.gsp.stage.StageRole;

public class SetPassGk extends AbstractRequestHandler {

	public SetPassGk(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String guankaStr = (String) paras.get("guanka");
			if(null != roleidStr && null != guankaStr) {
				final Long roleid = Long.valueOf(roleidStr);
				int guanka = Integer.valueOf(guankaStr);
				if (guanka == 0){
					guanka = -99919;
				}
				final int guankaid = guanka;
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties){
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				boolean setSuc = new xdb.Procedure() {
					protected boolean process() throws Exception {
						StageRole stageRole = StageRole.getStageRole(roleid);
						stageRole.onInitStage(guankaid);
						return true;
					};
				}.submit().get().isSuccess();
				if(!setSuc) {
					return failedMsg("修改关卡进度");
				}
				return successMsg();
			} else {
				return failedMsg("需要参数roleid guanka");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
