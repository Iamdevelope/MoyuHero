package chuhan.gsp.main;

public class PStartServer extends xdb.Procedure
{

	@Override
	protected boolean process() throws Exception {
		
		long now = GameTime.currentTimeMillis();
		xbean.ServerInfo xsrver =xtable.Serverinfo.get(1);
		if(xsrver == null)
		{
			xsrver = xbean.Pod.newServerInfo();
			xsrver.setFirsttime(now);
			xtable.Serverinfo.insert(1, xsrver);
		}
		xsrver.setStarttime(now);
		ConfigManager.FIRST_START_TIME =xsrver.getFirsttime();
		ConfigManager.THIS_START_TIME = xsrver.getStarttime();
		return true;
	}
}
