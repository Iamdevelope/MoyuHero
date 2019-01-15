package chuhan.gsp.msg;

import java.util.TimerTask;

public class DelayBroadCastTask extends TimerTask {
	
	private SSendMsgNotify msg;
	
	public DelayBroadCastTask(SSendMsgNotify msg) {
		this.msg = msg;
	}

	@Override
	public void run() {
		gnet.link.Onlines.getInstance().broadcast(msg);
	}

}
