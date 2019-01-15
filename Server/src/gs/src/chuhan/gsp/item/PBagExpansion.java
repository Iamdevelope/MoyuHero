package chuhan.gsp.item;

import java.util.List;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.config10;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.ParserString;


public class PBagExpansion extends xdb.Procedure
{
	private final long roleId;
	private final byte bagType;
	public PBagExpansion(long roleId,byte bagType) {
		this.roleId = roleId;
		this.bagType = bagType;
	}
	
	
	@Override
	protected boolean process() throws Exception
	{
		PropRole prole = PropRole.getPropRole(roleId, false);
		if(prole == null)
			return false;
		if (bagType == 1) {
			config10 config = ConfigManager.getInstance().getConf(config10.class).get(1012);
			if (config == null) {
				return false;
			}

			int maxNum = Integer.valueOf(config.getConfigvalue());
			if (prole.getBagExpNum() >= maxNum) {
				Message.psendMsgNotify(roleId, 219, 1);
				return false;
			}

			short num = 1;
			config10 configcost = ConfigManager.getInstance().getConf(config10.class).get(1110);
			List<Integer> costList = ParserString.parseString2Int(configcost.getConfigvalue());
			if(costList == null)
				return false;
			if(costList.size() <= prole.getProperties().getBuybagnum())
				return false;
			Integer cost = costList.get(prole.getProperties().getBuybagnum());
			if(cost == null)
				return false;
			if(cost.intValue() * -1 != prole.delYuanBao(cost.intValue() * -1, 0))
				return false;
			
			prole.addBagExpNum(num,bagType);

			return true;
		}else{
			config10 config = ConfigManager.getInstance().getConf(config10.class).get(1011);
			if (config == null) {
				return false;
			}
			int maxNum = Integer.valueOf(config.getConfigvalue());
			if(prole.getProperties().getBuyherobagnum() >= maxNum){
				Message.psendMsgNotify(roleId, 219, 1);
				return false;
			}
			short num = 1;
			config10 configcost = ConfigManager.getInstance().getConf(config10.class).get(1109);
			List<Integer> costList = ParserString.parseString2Int(configcost.getConfigvalue());
			if(costList == null)
				return false;
			if(costList.size() <= prole.getProperties().getBuyherobagnum())
				return false;
			Integer cost = costList.get(prole.getProperties().getBuyherobagnum());
			if(cost == null)
				return false;
			if(cost.intValue() * -1 != prole.delYuanBao(cost.intValue() * -1, 0))
				return false;
			prole.addBagExpNum(num,bagType);
			return true;
		}
	}
	
	
}
