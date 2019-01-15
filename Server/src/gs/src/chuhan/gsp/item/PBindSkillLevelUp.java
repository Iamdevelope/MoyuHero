package chuhan.gsp.item;

import java.util.HashSet;
import java.util.List;
import java.util.Set;

import chuhan.gsp.hero.OldHero;
import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.hero.PResetSkill;
import chuhan.gsp.hero.SRefreshHero;
import chuhan.gsp.item.types.SkillItem;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;

public class PBindSkillLevelUp extends xdb.Procedure
{
	private final long roleId;
	private final int herokey;
	public final Set<Integer> consumes = new HashSet<Integer>();;
	public PBindSkillLevelUp(long roleId, int herokey,
			List<Integer> consumeitemkeys) {
		this.roleId = roleId;
		this.herokey = herokey;
		this.consumes.addAll(consumeitemkeys);
	}
	
	@Override
	protected boolean process() throws Exception {
		ItemColumn itemcol = Module.getItemColumn(roleId, BagTypes.SKILL, false);
		
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		OldHero hero = herocol.getHero(herokey);
		if(hero == null)
			return false;
		
		//by yanglk  hero		if(hero.getHeroInfo().getSkilllv() >= PResetSkill.SKILL_MAXLV) {
		//by yanglk  hero			Message.psendMsgNotify(roleId, 396);
		//by yanglk  hero			return false;
		//by yanglk  hero		}
		if(consumes.isEmpty())
			return false;
		int sumexp = 0;
		for(int consumekey : consumes)
		{
			SkillItem consumeskill = (SkillItem)itemcol.getItem(consumekey);
			sumexp += consumeskill.getSumExp();
			if(itemcol.removeItemByKey(consumekey, 1, 1, "技能升级",false) != 1)
				return false;
		}
		itemcol.sendRemovedItems();
		/*//by yanglk  hero
		hero.getHeroInfo().setSkillexp(hero.getHeroInfo().getSkillexp()+sumexp);
		SSkill skillcfg = ConfigManager.getInstance().getConf(SSkill.class).get(hero.getConfig().getBindskill());
		int color = skillcfg.color;
		int nextexp = SkillItem.getNextExp(hero.getHeroInfo().getSkilllv(),color);
		while(hero.getHeroInfo().getSkillexp() >= nextexp)
		{
			hero.getHeroInfo().setSkillexp(hero.getHeroInfo().getSkillexp() - nextexp);
			hero.getHeroInfo().setSkilllv(hero.getHeroInfo().getSkilllv()+1);
			nextexp = SkillItem.getNextExp(hero.getHeroInfo().getSkilllv(),color);
		}
		//发送协议
		psendWhileCommit(roleId, new SLevelUpSkillItem());
		psendWhileCommit(roleId, new SRefreshHero(hero.getProtocolHero()));
		*/
		return true;
	}
	

}
