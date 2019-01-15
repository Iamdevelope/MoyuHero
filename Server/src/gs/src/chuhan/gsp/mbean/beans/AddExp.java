package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.attr.PAddExpProc;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class AddExp extends AbstractRequestHandler {

	public AddExp(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String roleidStr = (String) paras.get("roleid");
			String expStr = (String) paras.get("exp");
			if(null != roleidStr && null != expStr) {
				final Long roleid = Long.valueOf(roleidStr);
				final Integer exp = Integer.valueOf(expStr);
//				if(exp <= 0) {
//					return failedMsg("参数exp必须大于0");
//				}
				xbean.Properties properties = xtable.Properties.select(roleid);
				if (null == properties){
					return failedMsg("不存在的玩家roleid:" + roleidStr);
				}
				final PAddExpProc addexpProc = new PAddExpProc(roleid, exp,PAddExpProc.OTHER,"Cmd_addexp添加");
				addexpProc.submit();
				return successMsg();
			} else {
				return failedMsg("需要参数roleid exp");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
