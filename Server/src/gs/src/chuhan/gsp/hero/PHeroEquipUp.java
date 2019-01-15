package chuhan.gsp.hero;

public class PHeroEquipUp extends xdb.Procedure
{
	private final long roleId;
	private final int herokey; // 英雄key
	private final int equiplocation; // 装备位置 从1开始
	private final int islevelup; // 是否是升级，0为否（强化），1为升级
	private final int isstrength; // 是否一键强化，0为否，1为是
	public PHeroEquipUp(long roleId, int herokey,int equiplocation,int islevelup,int isstrength) {
		this.roleId = roleId;
		this.herokey = herokey;
		this.equiplocation = equiplocation;
		this.islevelup = islevelup;
		this.isstrength = isstrength;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		SHeroEquipUp snd = new SHeroEquipUp();
		snd.islevelup = this.islevelup;
		snd.isstrength = this.isstrength;
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);	
		boolean result = herocol.equipUpEntry(herokey,equiplocation,islevelup,isstrength);
		if(result){
			snd.result = SHeroEquipUp.END_OK;
		}else{
			snd.result = SHeroEquipUp.END_NOT_OK;
		}
		
		xdb.Procedure.psend(roleId,snd);
		
		return result;
	}
}
