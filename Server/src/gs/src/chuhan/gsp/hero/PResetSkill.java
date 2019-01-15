package chuhan.gsp.hero;

import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.types.SkillItem;
import chuhan.gsp.msg.MsgRole;

public class PResetSkill extends xdb.Procedure {
	public static final int SKILL_MAXLV = 12;
	private final long roleId;
		
	public PResetSkill(long roleId) {
		this.roleId = roleId;
	}
	
	@Override
	protected boolean process() throws Exception {
		ItemColumn itecol = Module.getItemColumn(roleId, BagTypes.SKILL, false);
		for(BasicItem item : itecol) {
			if(item instanceof SkillItem) {
				SkillItem skillItem = (SkillItem) item;
				if(skillItem.getSkillExtData().getLevel() > SKILL_MAXLV) {
					if(!addItem(skillItem.getSkillExtData().getLevel())) {
						return false;
					}
					skillItem.getSkillExtData().setLevel(SKILL_MAXLV);
					skillItem.getSkillExtData().setExp(0);
				}
			}
		}
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		for(OldHero hero : herocol.getHeros().values()) {
			/*//by yanglk  hero
			if(hero.getHeroInfo().getSkilllv() > SKILL_MAXLV) {
				if(!addItem(hero.getHeroInfo().getSkilllv())) {
					return false;
				}
				hero.getHeroInfo().setSkilllv(SKILL_MAXLV);
				hero.getHeroInfo().setSkillexp(0);	
			}*/
		}
		return true;
	}

	private boolean addItem(int lv) {
		ItemColumn itemcol = Module.getItemColumnByItemId(roleId, 3241, false);
		if(!itemcol.addItem(3241, 4 * (lv - SKILL_MAXLV), "resetSkillLv", 1).isSuccess()) {
			return false;
		}
		MsgRole msgRole = MsgRole.getMsgRole(roleId, false);
		msgRole.addSysMsgWithSP(397, null, null, 0, MsgRole.MST_TYPE_SYS);
		return true;
	}
}
