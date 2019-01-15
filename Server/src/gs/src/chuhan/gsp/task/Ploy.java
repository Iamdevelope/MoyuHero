package chuhan.gsp.task;

public class Ploy {
	private static volatile int DOUBLEEXP = 1;//多倍经验
	
	public synchronized static void setDoubleExp(int newValue) {
		DOUBLEEXP = newValue;
	}
	
	public static int getDoubleExp() {
		return DOUBLEEXP;
	}
}
