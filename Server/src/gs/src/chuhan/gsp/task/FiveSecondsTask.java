package chuhan.gsp.task;
import java.util.HashMap;
import java.util.TimerTask;

import com.pwrd.op.LogOpChannel;

import chuhan.gsp.MsgType;
import chuhan.gsp.battle.realtime.RoomManager;
import chuhan.gsp.battle.realtime.SMove;
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
public class FiveSecondsTask extends TimerTask {
	

	public FiveSecondsTask(){
		
	}
	@Override
	public void run() {
		try
		{
			//TODO 可以在此处理所有五分钟任务，注意自己捕捉异常，不要影响其他任务的运行
			realtimewaitotherwin();
			
			
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
	
	void realtimewaitotherwin()
	{
		try {
			new xdb.Procedure()
			{
				protected boolean process() throws Exception {
					
					if(RoomManager.getInstance().IsLoopRoomList)
						return false;
					RoomManager.getInstance().WaitOtherWin();
					return true;
				};
			}.submit();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	

}
