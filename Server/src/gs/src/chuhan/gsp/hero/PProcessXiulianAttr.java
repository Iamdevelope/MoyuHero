package chuhan.gsp.hero;

import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.log.Logger;
import chuhan.gsp.msg.MsgRole;

public class PProcessXiulianAttr extends xdb.Procedure{
	
	public static Logger logger = Logger.getLogger(PProcessXiulianAttr.class); 
	
	private final long roleId;
	
	public PProcessXiulianAttr(long roleId)
	{
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		processHeroColumn(roleId);
		return true;
	}
	
	public static void processHeroColumn(long roleId)
	{
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		for(OldHero hero : herocol.getHeros().values())
		{
			//processExtraAttr(roleId,xhero);
			processMinusAttr(roleId, hero);
		}
	}
	
	/*private static void processExtraAttr(long roleId, Hero hero)
	{
		int xiuliantimes = hero.getXiuliantimes();
		
		int maxattr = xiuliantimes * 5;
		
		int sumattr = (int)(hero.getBfp().getAttack() + hero.getBfp().getDefend() 
				+ hero.getBfp().getHp() + hero.getBfp().getWisdom());
		
		int delattr = sumattr - maxattr - 50;
		
		if(delattr <= 0)
			return;
		
		int postivesum = 0;
		if(hero.getBfp().getAttack() > 0)
			postivesum +=hero.getBfp().getAttack();
		if(hero.getBfp().getDefend() > 0)
			postivesum +=hero.getBfp().getDefend();
		if(hero.getBfp().getHp() > 0)
			postivesum +=hero.getBfp().getHp();
		if(hero.getBfp().getWisdom() > 0)
			postivesum +=hero.getBfp().getWisdom();
		
		logger.info("RoleID="+roleId+",heroId="+hero.getId()+ ",overxiulian="+delattr);
		
		if(hero.getBfp().getAttack() > 0)
		{
			hero.getBfp().setAttack(hero.getBfp().getAttack() - (hero.getBfp().getAttack()/postivesum) * delattr);
		}
		
		if(hero.getBfp().getDefend() > 0)
		{
			hero.getBfp().setDefend(hero.getBfp().getDefend() - (hero.getBfp().getDefend()/postivesum) * delattr);
		}
		
		if(hero.getBfp().getHp() > 0)
		{
			hero.getBfp().setHp(hero.getBfp().getHp() - (hero.getBfp().getHp()/postivesum) * delattr);
		}
		
		if(hero.getBfp().getWisdom() > 0)
		{
			hero.getBfp().setWisdom(hero.getBfp().getWisdom() - (hero.getBfp().getWisdom()/postivesum) * delattr);
		}
	}*/
	
	private static void processMinusAttr(long roleId, OldHero hero)
	{
		/*//by yanglk  hero
		double postivesum = 0;
		int minussum = 0;
		if(hero.getAttack() > 0)
			postivesum += hero.getAttack();
		else
			minussum += hero.getAttack();
		if(hero.getDefend() > 0)
			postivesum += hero.getDefend();
		else
			minussum += hero.getDefend();
		if(hero.getArmy() > 0)
			postivesum += hero.getArmy();
		else
			minussum += hero.getArmy();
		if(hero.getWisdom() > 0)
			postivesum += hero.getWisdom();
		else
			minussum += hero.getWisdom();
		if(minussum == 0)
			return;
		String str = ("MOVE MINUS ATTRS ORIGNAL att="+hero.getAttack()
				+",def="+hero.getDefend()
				+",hp="+hero.getArmy()
				+",wis="+hero.getWisdom());
		if(hero.getAttack() > 0)
		{
			hero.getHeroInfo().getBfp().setAttack((int)(hero.getHeroInfo().getBfp().getAttack() + (hero.getAttack()/postivesum) * minussum));
		}
		else
			hero.getHeroInfo().getBfp().setAttack(hero.getHeroInfo().getBfp().getAttack() - hero.getAttack());
		if(hero.getDefend() > 0)
		{
			hero.getHeroInfo().getBfp().setDefend((int)(hero.getHeroInfo().getBfp().getDefend() + (hero.getDefend()/postivesum) * minussum));
		}
		else
			hero.getHeroInfo().getBfp().setDefend(hero.getHeroInfo().getBfp().getDefend() - hero.getDefend());
		if(hero.getWisdom() > 0)
		{
			hero.getHeroInfo().getBfp().setWisdom((int)(hero.getHeroInfo().getBfp().getWisdom() + (hero.getWisdom()/postivesum) * minussum));
		}
		else
			hero.getHeroInfo().getBfp().setWisdom(hero.getHeroInfo().getBfp().getWisdom() - hero.getWisdom());
		if(hero.getArmy() > 0)
		{
			hero.getHeroInfo().getBfp().setHp((int)(hero.getHeroInfo().getBfp().getHp() + (hero.getArmy()/postivesum) * minussum));
		}
		else
			hero.getHeroInfo().getBfp().setHp(hero.getHeroInfo().getBfp().getHp() - hero.getArmy());
		
		
		str += ("  TO att="+hero.getAttack()
				+",def="+hero.getDefend()
				+",hp="+hero.getArmy()
				+",wis="+hero.getWisdom());
		str += ". minus attr="+String.valueOf(-minussum);
		ItemColumn itemcol = chuhan.gsp.item.Module.getItemColumn(roleId, BagTypes.BAG, false);
		itemcol.addItem(3208, -minussum, "move minus attr", 1, 1);
		MsgRole msgrole = MsgRole.getMsgRole(roleId, false);
		msgrole.addSysMsgWithSP(0, null, "系统补偿@修正培养负属性问题，已补偿您"+String.valueOf(-minussum)+"培养丹。", 0, MsgRole.MST_TYPE_SYS);
		logger.info(str);
		*/
	}
}
