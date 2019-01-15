package chuhan.gsp.item.types;

import java.util.HashMap;
import java.util.Map;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.AddItem;
import chuhan.gsp.award.AwardItem;
import chuhan.gsp.award.AwardManager;
import chuhan.gsp.award.PDrop;
import chuhan.gsp.game.SAchestconfig;
import chuhan.gsp.game.SBchestconfig;
import chuhan.gsp.item.Bag;
import chuhan.gsp.item.item26;
import chuhan.gsp.item.UseResult;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;


public class ChestItem extends NormalItem{
	
	ChestItem(int itemid) {
		super(itemid);
	}
	ChestItem(xbean.Item item) {
		super(item);
	}
	@Override
	public UseResult use(long roleId, int num, int dstkey) {
		
		//消耗对应的钥匙，打开箱子
		PropRole prole = PropRole.getPropRole(roleId, false);
		//先加开箱子值
		addOpenChestValue(prole, getItemid());
		boolean firstaward = false;
		int awardid = getFirstAwardId(prole, getAttr());
		if(awardid <= 0)
			awardid = getSecondAwardId(prole, getAttr());
		else
			firstaward = true;
		if(awardid <= 0)
			awardid = 0;
		Map<Integer,AwardItem> awards = AwardManager.getInstance().distributeAllAward(roleId, awardid, null, true);
		addGetGoodValue(prole, awards, firstaward);
		return UseResult.SUCC;
	}
	public static Map<Integer,Integer> ChestA = new HashMap<Integer, Integer>();
	public static Map<Integer,Integer> ChestB = new HashMap<Integer, Integer>();
	
	static
	{
		ChestA.put(3105,8);
		ChestA.put(3101,4);
		ChestB.put(3102,2);
		ChestB.put(3103,1);
	}
	public static void addOpenChestValue(PropRole prole, int chestId)
	{
		Integer value = ChestA.get(chestId);
		if(value == null)
			value = ChestB.get(chestId);
		if(value == null)
			return;
		
//		prole.getProperties().setOpenchestvalue(prole.getProperties().getOpenchestvalue()+value);
	}
	
	public static int getFirstAwardId(PropRole prole, item26 chestattr)
	{
		if(!ChestA.containsKey(chestattr.id))
			return 0;
		int value = 0;//Math.max(0, prole.getProperties().getBuychestvalue() - prole.getProperties().getGetgoodvalue()); 
		for(SAchestconfig cfg : ConfigManager.getInstance().getConf(SAchestconfig.class).values())
		{
			if(value >= cfg.min && value <= cfg.max)
			{
				if(Math.random() <= cfg.pro)
					return cfg.reward;
				else
					return 0;
			}
		}
		
		return 0;
	}
	
	public static int getSecondAwardId(PropRole prole, item26 chestattr)
	{
		if(!ChestA.containsKey(chestattr.id) && !ChestB.containsKey(chestattr.id))
			return 0;
		int value = 0;//prole.getProperties().getOpenchestvalue(); 
		for(SBchestconfig cfg : ConfigManager.getInstance().getConf(SBchestconfig.class).values())
		{
			if(value >= cfg.min && value <= cfg.max)
			{
				if(Math.random() <= cfg.pro)
				{
//					prole.getProperties().setOpenchestvalue(0);
					return cfg.reward;
				}
				else
					return 0;
			}
		}
		
		return 0;
	}
	
	public static void addGetGoodValue(PropRole prole, Map<Integer,AwardItem> awards, boolean firstaward)
	{
		String itemname = null;
		for(AwardItem aitem : awards.values())
		{
			for(AddItem additem : aitem.getItems())
			{
				item26 itemcfg = ConfigManager.getInstance().getConf(item26.class).get(additem.getId());
				if(itemcfg == null)
					continue;
				if(!itemcfg.getClassname().equals(EquipItem.class.getName())
						&& !itemcfg.getClassname().equals(WeaponItem.class.getName())
						&& !itemcfg.getClassname().equals(HorseItem.class.getName())
						&& !itemcfg.getClassname().equals(ArmorItem.class.getName()))
					continue;
				if(itemname == null)
					itemname = itemcfg.getName();
			}
		}
		if(firstaward && itemname != null)
			Message.pbroadcastMsgNotifyWhileCommit(163, prole.getProperties().getRolename(), itemname);
	}
	
}
