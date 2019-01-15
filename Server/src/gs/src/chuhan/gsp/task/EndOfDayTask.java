package chuhan.gsp.task;
import gnet.link.Role;

import java.util.Calendar;
import java.util.LinkedList;
import java.util.List;
import java.util.TimerTask;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoAddType;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.endlessbattle.EndlessinfoColumns;
import chuhan.gsp.play.ranking.endlessRanking;
import chuhan.gsp.play.wordboss.Module;
import chuhan.gsp.util.DateUtil;

/**
 * 跨天定时任务
 * @author 刘琛
 *
 */
public class EndOfDayTask extends TimerTask {
	

	public EndOfDayTask(){
		
	}
	@Override
	public void run() {
		try
		{
			long now = chuhan.gsp.main.GameTime.currentTimeMillis();
			endless();
			wordboss();
			newdayRefresh(now);
//			processSpecialLadderActivity();
//			CampRole.resetJifen();
			//TODO 可以在此处理所有跨天的任务，注意自己捕捉异常，不要影响其他任务的运行
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
	void endless(){
		try {
			endlessRanking.getInstance().endlessRankTime(true);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	void wordboss(){
		try {
			new xdb.Procedure() {
				protected boolean process() throws Exception {
					Module.getInstance().initData();
					return true;
				};
			}.submit();

		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	void newdayRefresh(long now){
		List<Role> roles = new LinkedList<Role>();
        roles.addAll(gnet.link.Onlines.getInstance().getRoles());
        for(Role role : roles){
        	long roleId = role.getRoleid();
        	chuhan.gsp.attr.PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
        	//极限试炼数据刷新
        	try{
    			new xdb.Procedure() {
    				protected boolean process() throws Exception {
    					EndlessinfoColumns endcol = EndlessinfoColumns.getEndLessColumn(roleId,false);
    					endcol.sendTodayEndless();
    					return true;
    				};
    			}.submit();
    		}catch(Exception e){
    			e.printStackTrace();
    		}
        	//祈愿台数据刷新
        	try{
    			new xdb.Procedure() {
    				protected boolean process() throws Exception {
    					prop.refreshSweep(now);
    					return true;
    				};
    			}.submit();
    		}catch(Exception e){
    			e.printStackTrace();
    		}
        	//月卡数据刷新
        	try{
    			new xdb.Procedure() {
    				protected boolean process() throws Exception {
    					ActivityManager.getInstance().sSRefreshMonthCard(roleId, now);
    					return true;
    				};
    			}.submit();
    		}catch(Exception e){
    			e.printStackTrace();
    		}
        }
        
		
	}
	
	/*void processSpecialLadderActivity()
	{
		try {
			long now = GameTime.currentTimeMillis();

			long dayfirst = DateUtil.getDayFirstSecond(now);

			if (Math.abs(dayfirst - now) > 600000) {
				dayfirst += DateUtil.dayMills;
				if (Math.abs(dayfirst - now) > 600000)
					return;// 超过误差10分钟
			}

			Calendar c = Calendar.getInstance();
			c.setTimeInMillis(dayfirst + 600000);// 过10分，防止在正好一天0ms的地方计算有误
			if (c.get(Calendar.YEAR) != 2013)
				return;
			if (c.get(Calendar.MONTH) != Calendar.APRIL)
				return;
			int dayofmonth = c.get(Calendar.DAY_OF_MONTH);
			if (dayofmonth < 6 || dayofmonth > 9)
				return;
			new xdb.Procedure() {
				protected boolean process() throws Exception {
					for (int i = 1; i <= 5; i++) {
						xbean.LadderInfo ladder = xtable.Pvpladder.get(i);
						if (ladder == null)
							continue;
						PropRole prole = PropRole.getPropRole(
								ladder.getRoleid(), false);
						prole.addYuanBao(500, YuanBaoAddType.OTHER);
					}
					return true;
				};
			}.submit();

		} catch (Exception e) {
			e.printStackTrace();
		}
	}*/
}
