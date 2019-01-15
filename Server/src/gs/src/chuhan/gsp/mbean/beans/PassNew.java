package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.game.newbieguide60;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.mbean.AbstractRequestHandler;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityManager;

public class PassNew extends AbstractRequestHandler {

	public PassNew(String name) {
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
				boolean passNew = new xdb.Procedure() {
					protected boolean process() throws Exception {
						java.util.TreeMap<Integer,newbieguide60> initMap = ConfigManager.getInstance().getConf(newbieguide60.class);
						for(Map.Entry<Integer,newbieguide60> entry : initMap.entrySet()){
							ActivityManager.getInstance().addNewyindao(roleid, entry.getKey());
						}
						Message.psendMsgNotify(roleid, 135);
						return true;
					};
				}.submit().get().isSuccess();
				if(!passNew) {
					return failedMsg("跳过新手失败");
				}
				return successMsg();
			} else {
				return failedMsg("需要参数roleid");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
