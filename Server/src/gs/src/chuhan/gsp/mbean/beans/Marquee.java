package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.mbean.AbstractRequestHandler;
import chuhan.gsp.play.activity.ActivityManager;

public class Marquee extends AbstractRequestHandler {

	public Marquee(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String msg = (String) paras.get("msg");
			boolean marquee = new xdb.Procedure()
			{
				protected boolean process() throws Exception {
					ActivityManager.getInstance().addMsgNotice(0, 0, "gmsend", msg);
					return true;
				};
			}.submit().get().isSuccess();
			if (!marquee) {
				return failedMsg("发送跑马灯失败");
			}
			return successMsg();

		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
