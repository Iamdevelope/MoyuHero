package chuhan.gsp.task;
import java.util.HashMap;
import java.util.TimerTask;

import com.pwrd.op.LogOpChannel;

import chuhan.gsp.MsgType;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.log.OpLogManager;
import chuhan.gsp.log.RemoteLogID;
import chuhan.gsp.log.RemoteLogParam;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.DateUtil;

/**
 * 五分钟定时任务
 * @author 刘琛
 *
 */
public class FiveMinutesTask extends TimerTask {
	

	public FiveMinutesTask(){
		
	}
	@Override
	public void run() {
		try
		{
			logOnlineRoles();
			//TODO 可以在此处理所有五分钟任务，注意自己捕捉异常，不要影响其他任务的运行
			
//			GameTime.broadcastGameTime();
//			sendSysMsg();
			
			
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
	
	void sendSysMsg()
	{
		try {
//			Message.broadcastMsgNotifyWithDelay(1000, MsgType.MSG_HERO_GET,"zy0029",String.valueOf(3),"hero-0004");
			Message.broadcastMsgNotifyWithDelay(20000, MsgType.MSG_ACTIVITY_TILI);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	void logOnlineRoles()
	{
		try {
			int rolenum = gnet.link.Onlines.getInstance().getRoles().size();
			OpLogManager.getInstance().doLog(LogOpChannel.ONLINE, 0, rolenum, GameTime.currentTimeMillis(),
					DateUtil.getCurrentStringFormatEn(GameTime.currentTimeMillis()));
			
			java.util.Map<String, Object> params = new HashMap<String, Object>();
			params.put(RemoteLogParam.FROM, ConfigManager.getGsZoneId());
			params.put(RemoteLogParam.CURRENTNUM, rolenum);
			params.put(RemoteLogParam.HINT, "0");
			LogManager.getInstance().doLog(RemoteLogID.ONLINEUSER, params);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
