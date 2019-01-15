
package chuhan.gsp.attr;

import chuhan.gsp.DataInit;
import chuhan.gsp.hero.OldHero;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.util.Conv;

public class PAddExpProc extends xdb.Procedure {

	public final static int BATTLE = 1;		//战斗奖励
	public final static int OTHER = 99;   //其他
	
	protected final long roleid;

	private final int addexp;
	
	private int reason;
	
	private String hint;

	public PAddExpProc(long roleid, int addexp, int reason, String hint) {

		this.roleid = roleid;
		this.addexp = addexp;
		this.reason = reason;
		this.hint = hint;
	}

	@Override
	public boolean process() {

		if (addexp == 0)
			return false;
		
		PropRole proprole = PropRole.getPropRole(roleid, false);
		if (null == proprole)
			return false;
/*		if(proprole.getLevel() >= DataInit.ROLE_LEVEL_MAX)
		{
			LogManager.logger.error("玩家超过等级上限。roleid："+roleid+"level:"+proprole.getLevel()+"maxLevel:"+DataInit.ROLE_LEVEL_MAX);
			//TODO 不再获得经验
			return false;
		}*/
		
		final int curexp = proprole.getExp();
		if (curexp + addexp <= 0){
			proprole.setExp(0);
		}
		else if(curexp + addexp >= proprole.getExpMax()){
			proprole.setExp(curexp + addexp);
			int i = 1;
			boolean isSend = false;
			while(proprole.getExp() >= proprole.getExpMax())
			{
				boolean succ = proprole.levelUp();
				if( !isSend ){
					isSend = succ;
				}
				if (!succ)
				{
//					if(proprole.getLevel() >= DataInit.ROLE_LEVEL_MAX){
//						LogManager.logger.error("玩家超过等级上限。roleid："+roleid+"level:"+proprole.getLevel()+"maxLevel:"+DataInit.ROLE_LEVEL_MAX);
//						proprole.setExp(proprole.getExpMax());//不成功且超经验了则平经验
//					}
					break;
				}
				i++;
/*				if(i >= DataInit.ROLE_LEVEL_MAX)
				{
					chuhan.gsp.attr.PropRole.logger.error("role一次升级次数过多");
					break;
				}*/
			}
			if(isSend){
				xdb.Procedure.psendWhileCommit(roleid, new SRefreshLevel(Conv.toShort(proprole.getLevel())));
			}
		}
		else{
			proprole.setExp(curexp + addexp);
		}
		psendWhileCommit(roleid, new SRefreshRoleExp(proprole.getExp()));
		//psendWhileCommit(roleid, new SRefreshLevel(Conv.toShort(proprole.getLevel())));
		
		return true;	
	}

	public static void logAddExp(long roleId, long addexp, String hint, int reason)
	{
		
	}

}
