package chuhan.gsp.stage;

public class PFightStageBattle extends xdb.Procedure	
{
	private final long roleId;
	private final int stagebattleid;
	private final boolean useyuanbao;
	public short troopid; // 战队ID

	public PFightStageBattle(long roleId, int stagebattleid, short troopid, boolean useyuanbao) {
		this.roleId = roleId;
		this.stagebattleid = stagebattleid;
		this.useyuanbao = useyuanbao;
		this.troopid = troopid;
	}
	
	
	@Override
	protected boolean process() throws Exception {
		xbean.Properties xprop = xtable.Properties.get(roleId);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleId+" 不存在。");
		}
		StageRole stagerole = StageRole.getStageRole(roleId);
		return stagerole.fightStageBattle(stagebattleid,troopid, useyuanbao,false);
	}
	
}
