package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class MReload extends AbstractRequestHandler {

	public MReload(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			boolean reload = new xdb.Procedure()
			{
				protected boolean process() throws Exception {
					ConfigManager.getInstance().init();
					return true;
				};
			}.submit().get().isSuccess();
			if (!reload) {
				return failedMsg("重载配置文件失败");
			}
			return successMsg();
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}
}
