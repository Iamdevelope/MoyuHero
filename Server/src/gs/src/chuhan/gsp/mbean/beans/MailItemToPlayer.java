package chuhan.gsp.mbean.beans;

import java.util.Map;

import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.item26;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.mbean.AbstractRequestHandler;

public class MailItemToPlayer extends AbstractRequestHandler {

	public MailItemToPlayer(String name) {
		super(name);
	}

	@Override
	protected Map<Object, Object> handleRequest(Map<?, ?> paras) {
		try {
			String playeridStr = (String) paras.get("playerid");
			String itemidStr = (String) paras.get("itemid");
			String itemnumStr = (String) paras.get("itemnum");
			if(null != playeridStr && null != itemidStr && itemnumStr != null) {
				final Long playerid = Long.valueOf(playeridStr);
				final Integer itemid = Integer.valueOf(itemidStr);
				final Integer itemnum = Integer.valueOf(itemnumStr);
				if(itemnum <= 0) {
					return failedMsg("参数itemnum必须大于0");
				}
				item26 itemAttr = ConfigManager.getInstance().getConf(item26.class).get(itemid);
				if(null == itemAttr) {
					return failedMsg("不存在的物品");
				}
				if(itemnum > 10 * itemAttr.getStackNum()) {
					return failedMsg("最多添加该物品数量为" + 10 * itemAttr.getStackNum());
				}
				xbean.Properties properties = xtable.Properties.select(playerid);
				if (null == properties){
					return failedMsg("不存在的玩家playerid:" + playeridStr);
				}
				boolean addSuc = new xdb.Procedure() {
					protected boolean process() throws Exception {
						ItemColumn itemcol = Module.getItemColumnByItemId(playerid, itemid, false);
						if(!itemcol.addItem(itemid, itemnum, "customerService_add", 1).isSuccess()) {
							return false;
						}
						return true;
					};
				}.submit().get().isSuccess();
				if(!addSuc) {
					return failedMsg("添加道具失败");
				}
				return successMsg();
			} else {
				return failedMsg("需要参数playerid itemid itemnum");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return failedMsg("执行出错");
		}
	}

}
