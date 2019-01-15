package chuhan.gsp.hero;

import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.SColor;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;

public class PConsumeHero extends xdb.Procedure
{
	private final long roleId;
	private final List<Integer> herokeys;
	private final boolean ishero;
	public PConsumeHero(long roleId, List<Integer> herokeys, boolean ishero) {
		this.roleId = roleId;
		this.herokeys = herokeys;
		this.ishero = ishero;
	}
	
	@Override
	protected boolean process() throws Exception {
		PropRole prole = PropRole.getPropRole(roleId, false);
		
		Set<Integer> keyset = new HashSet<Integer>();
		keyset.addAll(herokeys);
		if(keyset.isEmpty())
			return false;
		int addsoul = 0;
		if(ishero)
		{
			SRemoveHero removesnd= new SRemoveHero();
			OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
			for (int key : keyset) {
				OldHero hero = herocol.getHero(key);
				if (hero == null)
					continue;
				OldHero removed = herocol.removeHero(key,false);
				if(removed == null)
					continue;
				Map<Integer,SColor> colors = ConfigManager.getInstance().getConf(SColor.class);
				int v = 0;//by yanglk  hero   colors.get(hero.getInitColor()).getHeroOfferGrow();
				int grade =0;//by yanglk  hero    removed.getGrade();
				if(grade >= 4)
					v *= 16;
				else if (grade >= 3)
					v *= 8;
				else if (grade >= 2)
					v *= 4;
				else if (grade >= 1)
					v *= 2;
				if (v <= 0)
					continue;
				addsoul += v;
				removesnd.herokey.add(key);
			}
			xdb.Procedure.psendWhileCommit(roleId, removesnd);
			Message.psendMsgNotifyWhileCommit(roleId, 56, keyset.size(),addsoul);
		}
		else
		{
			int num = 0;
			ItemColumn itemcol = chuhan.gsp.item.Module.getItemColumn(roleId, BagTypes.SOUL, false);
			for (int key : keyset) {
				BasicItem item = itemcol.getItem(key);
				if (item == null)
					continue;
				int itemnum = item.getNumber();
				if(itemcol.removeItemByKey(key, itemnum, 0,"consume_to_soul",false) != itemnum)
					continue;
				int v = ConfigManager.getInstance().getConf(SColor.class).get(item.getColor()).getSoulOfferGrow() * itemnum;
				if (v <= 0)
					continue;
				addsoul += v;
				num += itemnum;
			}
			itemcol.sendRemovedItems();
			Message.psendMsgNotifyWhileCommit(roleId, 57, num,addsoul);
		}
//		prole.addSoul(addsoul);
		xdb.Procedure.psendWhileCommit(roleId, new SConsumeHero(addsoul));
		return true;
	}
}
