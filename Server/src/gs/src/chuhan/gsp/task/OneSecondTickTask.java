package chuhan.gsp.task;
import gnet.link.Role;

import java.util.Collection;
import java.util.TimerTask;

import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.ranking.bossRanking;
import chuhan.gsp.play.wordboss.PGetBossRank;
import chuhan.gsp.play.wordboss.Module;
import chuhan.gsp.util.DateUtil;

/**
 * 每秒tick定时任务
 * @author ylk
 *
 */
public class OneSecondTickTask extends TimerTask {
	

	public OneSecondTickTask(){
		
	}
	@Override
	public void run() {
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		try
		{
			//TODO 可以在此处理所有任务，注意自己捕捉异常，不要影响其他任务的运行
			bossBegin(now);
			bossSetHp(now);
			bossRank(now);
//			GameTime.broadcastGameTime();
//			sendSysMsg();
			
			
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
	
	void bossBegin(long now){
		try {
			new xdb.Procedure() {
				protected boolean process() throws Exception {
					boolean bossBegin = false;
					//boss战开始的时候
					if( now/1000  == Module.getInstance().getBeginTime(1, now)/1000 ||
							now/1000 == Module.getInstance().getBeginTime(3, now)/1000 ||
							now/1000 == Module.getInstance().getBeginTime(2, now)/1000 ||
							now/1000 == Module.getInstance().getBeginTime(4, now)/1000 ){
						//清空排行榜数据
						bossRanking.getInstance().list10.clear();
						bossRanking.getInstance().map10.clear();
						bossRanking.getInstance().mapall.clear();						
						bossBegin = true;
						
						Module.getInstance().sendBossOpenMsg(now);
					}
					
					if(now/1000  == Module.getInstance().getEndTime(2, now)/1000 ||
							now/1000  == Module.getInstance().getEndTime(4, now)/1000 || bossBegin){
						Collection<Role> roleList = gnet.link.Onlines.getInstance().getRoles();
						for(Role role : roleList){
							try{
								Module.getInstance().sendSGetWordBoss(role.getRoleid());
								new PGetBossRank(role.getRoleid()).call();
							}catch(Exception e){
								e.printStackTrace();
							}
						}
					}
					return true;
				}
			}.submit();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 设置新boss血量投入也发送排行榜奖励
	 * @param now
	 */
	void bossSetHp(long now){
		try {
			new xdb.Procedure() {
				protected boolean process() throws Exception {
					//boss战结束6分钟以后，重置新BOSS血量
					if( now/1000  == Module.getInstance().getEndTime(2, now)/1000 + 360 || 
							now/1000 == Module.getInstance().getEndTime(4, now)/1000 + 360 ){
						try{
							Module.getInstance().setNewHp(now);
						}catch(Exception e){
							e.printStackTrace();
						}
						try{
							bossRanking.getInstance().sendMail(now);
						}catch(Exception e){
							e.printStackTrace();
						}
					}
					return true;
				}
			}.submit();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * boss排行榜刷新
	 * @param now
	 */
	void bossRank(long now){
		try {
			new xdb.Procedure() {
				protected boolean process() throws Exception {
					int bossid = bossRanking.getInstance().bossId;
					if (bossid == 2 || bossid == 4) {
						if (Module.getInstance().isInOpenTime(bossid,now) != -1
									|| Module.getInstance().isInOpenTime(bossid, 
											now - 5 * DateUtil.minuteMills - 15 * 1000) != -1) {
							bossRanking.getInstance().ranking(now);
						}
					}
					return true;
				}
			}.submit();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
