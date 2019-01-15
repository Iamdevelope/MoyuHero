package chuhan.gsp.main;

import java.util.Calendar;
import java.util.concurrent.locks.Condition;
import java.util.concurrent.locks.ReentrantLock;

public class Stopper implements StopperMXBean {
	static transient final ReentrantLock shutdownAlarmLock = new ReentrantLock();
	static transient final Condition shutdownAlarm = shutdownAlarmLock.newCondition();
	static transient final ReentrantLock shutdownCompletedLock = new ReentrantLock();
	static transient final Condition shutdownCompleted = shutdownCompletedLock.newCondition();
	public static long stopTime = -1L;
	

	public void doWait() {
		while (true) {
			try {
				shutdownAlarmLock.lockInterruptibly();
			}catch(final InterruptedException ex){
				break;
			}
			try {
				if (stopTime < 0)
					shutdownAlarm.await();
				else {
					final long now = java.util.Calendar.getInstance()
							.getTimeInMillis();
					if (now >= stopTime)
						break;
					else
						shutdownAlarm.awaitUntil(new java.util.Date(
								stopTime));
				}
			} catch (final InterruptedException ex) {
				break;
			} finally {
				shutdownAlarmLock.unlock();
			}
		}

	}

	@Override
	public long getStopTime() {
		final long time; 
		shutdownAlarmLock.lock();
		try {
			time=stopTime;
		} finally {
			shutdownAlarmLock.unlock();
		}
		if(time<=0) return time;
		else return (time-Calendar.getInstance().getTimeInMillis())/1000;
	}

	@Override
	public void setStopTime(long seconds) {
		if (seconds < 0)
			return;
		shutdownAlarmLock.lock();
		try {
			stopTime = Calendar.getInstance().getTimeInMillis() + seconds
					* 1000L;
			shutdownAlarm.signalAll();
		} finally {
			shutdownAlarmLock.unlock();
		}

	}

	/**
	 * 同步关闭，等到服务器彻底关闭完成后，才返回
	 * @param seconds
	 */
	@Override
	public void stop(int seconds)
	{
		setStopTime(seconds);
		try {
			shutdownCompletedLock.lockInterruptibly();
		}catch(final InterruptedException ex){
			return;
		}
		try {
			shutdownCompleted.await();
				
		} catch (final InterruptedException ex) {
			return;
		} finally {
			shutdownCompletedLock.unlock();
		}
		return;
	}
	
}
