package chuhan.gsp.item;

import java.util.HashSet;
import java.util.List;
import java.util.Set;

import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.hero.PResetSkill;
import chuhan.gsp.item.types.SkillItem;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.Conv;

public class PSkillLevelUp extends xdb.Procedure
{
	private final long roleId;
	private final int skillitemkey;
	public final Set<Integer> consumes = new HashSet<Integer>();;
	public PSkillLevelUp(long roleId, int skillitemkey,
			List<Integer> consumeitemkeys) {
		this.roleId = roleId;
		this.skillitemkey = skillitemkey;
		this.consumes.addAll(consumeitemkeys);
	}
	
	@Override
	protected boolean process() throws Exception {
		
		ItemColumn itemcol = chuhan.gsp.item.Module.getItemColumn(roleId, BagTypes.SKILL, false);
		SkillItem skillitem =  (SkillItem)itemcol.getItem(skillitemkey);
		
		if(skillitem.getSkillExtData().getLevel() >= PResetSkill.SKILL_MAXLV) {
			Message.psendMsgNotify(roleId, 396);
			return false;
		}
		if(consumes.isEmpty())
			return false;
		int sumexp = 0;
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		for(int consumekey : consumes)
		{
			if(herocol.getTroopBySkill(consumekey) != null)
				continue;
			SkillItem consumeskill = (SkillItem)itemcol.getItem(consumekey);
			sumexp += consumeskill.getSumExp();
			if(itemcol.removeItemByKey(consumekey, 1, 1, "技能升级",false) != 1)
				return false;
		}
		itemcol.sendRemovedItems();
		skillitem.addExp(sumexp);
		//发送协议
		SRefreshItem snd = new SRefreshItem();
		snd.bagid = Conv.toByte(BagTypes.SKILL);
		snd.data = skillitem.getProtocolItem();
		psendWhileCommit(roleId, snd);
		
		psendWhileCommit(roleId, new SLevelUpSkillItem());
		return true;
	}
	

}
