package chuhan.gsp.gm;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.item26;
import chuhan.gsp.main.ConfigManager;

public class Cmd_addallequip extends GMCommand {

	@Override
	boolean exec(String[] args) {
		final long roleid = getGmroleid();
		if(args.length != 1) {
			sendToGM("参数格式错误："+usage());
		}
		int stackNum = Integer.valueOf(args[0]);
		Map<Integer, item26> items = ConfigManager.getInstance().getConf(item26.class);
		List<Integer> itemIds = new ArrayList<Integer>();
		Iterator<Entry<Integer, item26>> it = items.entrySet().iterator();
		while(it.hasNext()) {
			Entry<Integer, item26> entry = it.next();
			//if(entry.getValue().bag == BagTypes.EQUIP || entry.getValue().bag == BagTypes.SKILL) {
				//itemIds.add(entry.getKey());
			//}
		}
		Collections.sort(itemIds);
		int size = itemIds.size();
		stackNum --;
		int fromIndex = stackNum < 0 ? 0 : stackNum;
		int toIndex = (stackNum + 20) > size ? size : (stackNum + 20);
		fromIndex = fromIndex > toIndex ? toIndex : fromIndex;
		List<Integer> hids = itemIds.subList(fromIndex, toIndex);
		for(final int itemId : hids) {
			try {
				new xdb.Procedure() {
					protected boolean process() throws Exception {
						ItemColumn itemcol = Module.getItemColumnByItemId(roleid, itemId, false);
						itemcol.addItem(itemId, 1, "gm_add", 1);
						return true;
					};
				}.submit().get();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		
		return true;
	}

	@Override
	String usage() {
		return "//addallitem [startIndex]";
	}

}
