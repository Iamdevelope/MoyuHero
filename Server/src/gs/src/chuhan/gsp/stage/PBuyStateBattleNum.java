package chuhan.gsp.stage;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.config10;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.ParserString;

public class PBuyStateBattleNum extends xdb.Procedure	
{
	private final long roleId;
	private final int buytype; // 购买类型：1为扫荡，2为关卡（需要关卡id）
	private final int battleid; // 关卡ID

	public PBuyStateBattleNum(long roleId, int buytype, int battleid) {
		this.roleId = roleId;
		this.buytype = buytype;
		this.battleid = battleid;
	}
	
	
	@Override
	protected boolean process() throws Exception {
		xbean.Properties xprop = xtable.Properties.get(roleId);
		if(xprop == null){
			throw new IllegalArgumentException("构造角色时，角色 "+roleId+" 不存在。");
		}
		PropRole prole = PropRole.getPropRole(roleId, false);
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		boolean result = false;
		if(buytype ==1){			//扫荡
			java.util.List<Integer> costList = ParserString.parseString2Int(ConfigManager.getInstance().
					getConf(config10.class).get(1271).configvalue);
			if(costList != null){
				int cost = 0;
				int buyNum = prole.getProperties().getSweepbuynum();
				if(costList.size() > buyNum){
					cost = costList.get(buyNum);
				}else{
					cost = costList.get(costList.size() - 1);
				}
				if( -cost == prole.delYuanBao(-cost, 0) && prole.useSweepBuy(now)){
					result = true;
				}	
			}
			
		}else if(buytype == 2){		//关卡
			StageRole stagerole = StageRole.getStageRole(roleId);
			result = stagerole.buyStateBattleNum(prole, battleid,now);
		}
		
		SBuyStateBattleNum snd = new SBuyStateBattleNum();
		snd.buytype = this.buytype;
		if(result){
			snd.endtype = SBuyStateBattleNum.END_OK;
		}else{
			snd.endtype = SBuyStateBattleNum.END_ERROR;
		}
		xdb.Procedure.psend(roleId, snd);
		return result;
		
	}
	
}
