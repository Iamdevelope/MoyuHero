package chuhan.gsp.stage;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.battle.BattleManager;
import chuhan.gsp.battle.SEndBattle;




public class PEndFightStageBattle extends xdb.Procedure{
	private final long roleid;
	public final int pass; // 0未通过，1通过1，2通过2，3全通
	
//	public int herokey;
	
	public PEndFightStageBattle(long roleid, int pass) {
		this.roleid = roleid;
		this.pass = pass;
	}
	
	@Override
	protected boolean process() throws Exception {
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
		}
		StageRole stagerole = StageRole.getStageRole(roleid);
		PropRole prole = PropRole.getPropRole(roleid, false);
//		return stagerole.EndfightStageBattle(roleid,pass);
//		/*
		boolean result = stagerole.EndfightStageBattle(roleid,pass,false);
		if(!result){
			SEndFightStageBattle snd = new SEndFightStageBattle();
			snd.time = 0;
			snd.zhangjie = 0;
			snd.endtype = SEndFightStageBattle.END_ERROR;
			snd.smid = 0;
			xdb.Procedure.psend(roleid, snd);
		}
		return result;
		
//		return true;;
//		*/
	}
	
}
