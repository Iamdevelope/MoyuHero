package chuhan.gsp.mbean.beans;

import java.util.Map;
import java.util.List;
import java.util.Arrays;
import java.util.ArrayList;

import chuhan.gsp.mbean.AbstractRequestHandler;
import chuhan.gsp.mail.MailColumn;

public class SendMail extends AbstractRequestHandler {

	public SendMail(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String dropsStr = (String) paras.get("drops");
			final String title = (String) paras.get("title");
			String msg = (String) paras.get("msg");
			if (null != roleidStr) {
				final Long roleid = Long.valueOf(roleidStr);
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties) {
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				List<String> dropStrList = Arrays.asList(dropsStr.split(","));
				List<Integer> dropList = new ArrayList<Integer>();
				for(String s : dropStrList) {
					if(s != "") {
						dropList.add(Integer.valueOf(s));
					}
				}
				long now = chuhan.gsp.main.GameTime.currentTimeMillis();
				boolean addSuc = new xdb.Procedure()
				{
					protected boolean process() throws Exception {
						MailColumn col = MailColumn.getMailColumn(roleid, false);
						col.addMail(col.createMail("mail_tips1", title, msg, dropList, null, now+MailColumn.DEFAULT_TIME,
								null),false);
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
