package chuhan.gsp.item;

import java.util.ArrayList;
import java.util.List;

import chuhan.gsp.hero.PAddTuJianHero;
import chuhan.gsp.main.ConfigManager;


public class SoulColumn extends ItemColumn {

	SoulColumn(long roleid, boolean readonly) {
		super(roleid, BagTypes.SOUL, readonly);
	}

	public AddItemResult addItem(final int itemid, final int num, final int numtype, 
			final int initflag,
			final String reason,
			final int countertype) {
		AddItemResult addresult = super.addItem(itemid, num, numtype, initflag, reason, countertype);
		if(!addresult.isSuccess())
			return addresult;
		
		//更新图鉴武将数量
		item26 sItemAttr = ConfigManager.getInstance().getConf(item26.class).get(itemid);
		List<Integer> heroIds = new ArrayList<Integer>();
		//heroIds.add(sItemAttr.par1);
//		new PAddTuJianHero(roleid, heroIds).call();
		
		return addresult;
	}
}
