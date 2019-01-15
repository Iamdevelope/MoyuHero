package chuhan.gsp.hero;

import java.util.HashSet;
import java.util.Set;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoConsumeType;
import chuhan.gsp.item.Bag;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.Misc;

public class PRequestXiulian extends xdb.Procedure
{
	private final long roleId;
	private final int herokey;
	private final int costtype;
	public PRequestXiulian(long roleId, int herokey, int costtype) {
		this.roleId = roleId;
		this.herokey = herokey;
		this.costtype = costtype;
	}
	public static final int XIU_LIAN_DAN = 3208;
	@Override
	protected boolean process() throws Exception {
		
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		OldHero hero = herocol.getHero(herokey);
		if(hero == null)
			return false;
		PropRole prole = PropRole.getPropRole(roleId, false);
		
		Bag bag = new Bag(roleId, false);
		if(costtype == 1)
		{
			if(bag.removeItemById(XIU_LIAN_DAN, 1, 1, "xiulian") != 2)
			{
				Message.psendMsgNotify(roleId, 26);
				return false;
			}
		}else if(costtype == 2)
		{
			if(bag.removeItemById(XIU_LIAN_DAN, 1, 1, "xiulian") != 1)
			{
				Message.psendMsgNotify(roleId, 26);
				return false;
			}
			if(prole.delYuanBao(-10, YuanBaoConsumeType.OTHER) != -10)
			{
				Message.psendMsgNotify(roleId, 27);
				return false;
			}
		}
		else
			return false;
		
		//确定加减的值
		int delv = 0;
		if(costtype == 1)
			delv = Misc.getRandomBetween(1, 3);
		else
			delv = Misc.getRandomBetween(1, 5);
		/*//by yanglk  hero
		int addv = (hero.canXiulianTimes() <= 0) ? delv : delv + 5;
		
		//确定加减属性集合
		Set<Integer> addattrs = new HashSet<Integer>();
		Set<Integer> delattrs = new HashSet<Integer>();
		addattrs.add(1);
		if(hero.getArmy() >= delv)
			delattrs.add(1);
		addattrs.add(2);
		if(hero.getAttack() >= delv)
			delattrs.add(2);
		addattrs.add(3);
		if(hero.getDefend() >= delv)
			delattrs.add(3);
		addattrs.add(4);
		if(hero.getWisdom() >= delv)
			delattrs.add(4);
		if(delattrs.size() == 0)
			return false;
		
		//先随机减值属性，再随机加值属性
		int delattr = Misc.getRandom(delattrs);
		addattrs.remove(delattr);
		int addattr = Misc.getRandom(addattrs);
//		int addattr = Misc.getRandomBetween(1, 4);
//	
//		int delattr = Misc.getRandomBetween(1, 3);
//		if(delattr >= addattr)
//			delattr++;
		
		
		xbean.XiuLianResult xresult = xbean.Pod.newXiuLianResult();
		xresult.setHerokey(herokey);
		if(delattr == addattr)
			return false;
		switch(addattr)
		{
		case 1:
			xresult.setHp(addv);
			break;
		case 2:
			xresult.setAttack(addv);
			break;
		case 3:
			xresult.setDefend(addv);
			break;
		case 4:
			xresult.setWisdom(addv);
			break;
		}
		switch(delattr)
		{
		case 1:
			xresult.setHp(-delv);
			break;
		case 2:
			xresult.setAttack(-delv);
			break;
		case 3:
			xresult.setDefend(-delv);
			break;
		case 4:
			xresult.setWisdom(-delv);
			break;
		}
		xtable.Xiulianresults.remove(roleId);
		xtable.Xiulianresults.insert(roleId, xresult);
		SReplyXiulianResult snd = new SReplyXiulianResult(Conv.toByte(xresult.getHp()), Conv.toByte(xresult.getAttack()), Conv.toByte(xresult.getDefend()), Conv.toByte(xresult.getWisdom()));
		psendWhileCommit(roleId, snd);
		*/
		return true;
	}
}
