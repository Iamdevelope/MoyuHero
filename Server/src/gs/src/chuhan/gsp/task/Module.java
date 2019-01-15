package chuhan.gsp.task;

import java.util.Calendar;
import java.util.concurrent.TimeUnit;

import chuhan.gsp.main.GameTime;
import chuhan.gsp.main.ModuleInterface;
import chuhan.gsp.main.ReloadResult;
import chuhan.gsp.util.DateUtil;


public class Module implements ModuleInterface {

    @Override
    public void exit() {
    }

    @Override
    public void init() throws Exception {
		final long now = GameTime.currentTimeMillis();
		Calendar till = Calendar.getInstance();
		till.set(Calendar.HOUR_OF_DAY, 23);
		till.set(Calendar.MINUTE, 59);
		till.set(Calendar.SECOND, 59);
		final long mtill = till.getTimeInMillis();

		final long timeSpace = (mtill - now) < 0 ? (mtill - now + 24*60*60*1000) : (mtill - now);
		final long sdelay = timeSpace/1000;
		
		xdb.Executor.getInstance().scheduleAtFixedRate(new EndOfDayTask(), sdelay + 2, DateUtil.dayMills/1000, TimeUnit.SECONDS);

//		xdb.Executor.getInstance().scheduleAtFixedRate(new FiveSecondsTask(), 0, 5, TimeUnit.SECONDS);
		xdb.Executor.getInstance().scheduleAtFixedRate(new OneSecondTickTask(), 10, 1, TimeUnit.SECONDS);
		xdb.Executor.getInstance().scheduleAtFixedRate(new FiveMinutesTask(), 0, 300, TimeUnit.SECONDS);
		//每周日22点执行的任务
		xdb.Executor.getInstance().scheduleAtFixedRate(new EndOfWeekTask(), 
				EndOfWeekTask.getDisMilSec2NextMonday() - 2 * DateUtil.hourMills,
				DateUtil.weekMills, TimeUnit.MILLISECONDS);
		//每天22点任务
		long next22delay = DateUtil.getDayFirstSecond(now) + 22 * DateUtil.hourMills - now;
		if(next22delay <= 0)
			next22delay += DateUtil.dayMills;
		xdb.Executor.getInstance().scheduleAtFixedRate(new TwentyTwoClockTask(), next22delay/1000, DateUtil.dayMills/1000, TimeUnit.SECONDS);
		//整点执行的任务
		xdb.Executor.getInstance().scheduleAtFixedRate(new HourTask(), DateUtil.getNextIntegral(now) - now, DateUtil.hourMills, TimeUnit.MILLISECONDS);
    }

	@Override
    public ReloadResult reload() throws Exception {
    	
        return new ReloadResult(false);
    }
}
