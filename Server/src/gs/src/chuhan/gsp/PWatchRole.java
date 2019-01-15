package chuhan.gsp;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.friends.FriendRole;
import chuhan.gsp.hero.OldHeroColumn;
import chuhan.gsp.item.types.EquipItem;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.Conv;

public class PWatchRole extends xdb.Procedure{

	private final long watcherId;
	private final long roleId;
	public PWatchRole(long watcherId, long roleId) {
		this.watcherId = watcherId;
		this.roleId = roleId;
	}
	@Override
	protected boolean process() throws Exception {
		
		PropRole prole = PropRole.getPropRole(roleId, true);
		if(prole == null) {
			Message.psendMsgNotify(watcherId, 126, Message.getMessage(21));
			return false;
		}
		
		SWatchRole snd = new SWatchRole();
		snd.roleid = roleId;
		snd.name = prole.getProperties().getRolename();
		snd.level = Conv.toShort(prole.getLevel());
		snd.viplv = Conv.toByte(prole.getVipLevel());
		FriendRole friendRole = FriendRole.getFriendRole(watcherId, true);
		if(friendRole.isFriend(roleId)) {//是否我的好友
			snd.ismyfriend = Conv.toByte(1);
		} else {
			snd.ismyfriend = Conv.toByte(0);
		}
		
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		for(chuhan.gsp.hero.OldTroop troop : herocol.getTroops())
		{
			WatchTroopInfo info = new WatchTroopInfo();
			chuhan.gsp.hero.OldHero hero = null;//*by yanglk trooptroop.getHero();
			//by yanglk  hero			info.heroid = Conv.toShort(hero.getId());
			//by yanglk  hero			info.colorgrade = toByte(hero.getColor(), hero.getGrade());
			EquipItem weapon = troop.getWeapon();
			/*
			if(weapon != null)
				info.weapon = toByte(weapon.getFinalColor(), weapon.getGrade());
			EquipItem armor = troop.getArmor();
			if(armor != null)
				info.armor = toByte(armor.getFinalColor(), armor.getGrade());
			EquipItem horse = troop.getHorse();
			if(horse != null)
				info.horse = toByte(horse.getFinalColor(), horse.getGrade());
				*/
//			int skinId = BeautyRole.getEquipSkinId(roleId, info.heroid);
/*			if(skinId > 0) {
				info.shape = Conv.toByte(BeautyRole.getShapeWithSkinId(skinId));
			}*/
//*by yanglk troop			hero = troop.getViceHero1();
			if(hero != null)
			{
				//by yanglk  hero				info.vicehero1 = Conv.toShort(hero.getId());
				//by yanglk  hero				info.vhero1color = Conv.toByte(hero.getColor());
			}
//*by yanglk troop			hero = troop.getViceHero2();
			if(hero != null)
			{
				//by yanglk  hero				info.vicehero2 = Conv.toShort(hero.getId());
				//by yanglk  hero				info.vhero2color = Conv.toByte(hero.getColor());
			}
			snd.troops.add(info);
		}
		xdb.Procedure.psendWhileCommit(watcherId, snd);
		return true;
	}
	
	private byte toByte(int color, int grade)
	{
		return (byte)((color << 4) + grade); 
	}
	
}
