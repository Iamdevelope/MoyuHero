package chuhan.gsp.stage;

public class PSweepBattle extends xdb.Procedure	
{
	private final long roleId;
	private final int stagebattleid;
	private final boolean useyuanbao;
	private final int troopid; // 战队ID
	private final byte num; // 扫荡次数，1为1次，其他为10次

	public PSweepBattle(long roleId, int stagebattleid, int troopid, boolean useyuanbao,byte num) {
		this.roleId = roleId;
		this.stagebattleid = stagebattleid;
		this.useyuanbao = useyuanbao;
		this.troopid = troopid;
		this.num = num;
	}
	
	
	@Override
	protected boolean process() throws Exception {
		xbean.Properties xprop = xtable.Properties.get(roleId);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleId+" 不存在。");
		}
		StageRole stagerole = StageRole.getStageRole(roleId);
		int loopNum = 1;
		if(num != 1){
			loopNum = 10;
		}
		stagerole.battleInfoList.clear();
		boolean result = false;
		for(int i = 0;i<loopNum;i++){
			if(stagerole.fightStageBattle(stagebattleid,troopid, useyuanbao,true)){
				if(stagerole.EndfightStageBattle(roleId, 3, true)){
					result = true;
				}else{
					break;
				}
			}else{
				break;
			}
		}
		if(result){
			SSweepBattle snd = new SSweepBattle();
			snd.time = 0;
			snd.zhangjie = 0;
			snd.endtype = SSweepBattle.END_OK;
			snd.smid = 0;
			snd.battleinfolist.addAll(stagerole.battleInfoList);
			xdb.Procedure.psend(roleId, snd);
			return true;
		}
		
		
		SSweepBattle snd = new SSweepBattle();
		snd.time = 0;
		snd.zhangjie = 0;
		snd.endtype = SSweepBattle.END_ERROR;
		snd.smid = 0;
		xdb.Procedure.psend(roleId, snd);
		return false;
		
	}
	
}
