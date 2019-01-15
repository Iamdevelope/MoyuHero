package chuhan.gsp.log;
public interface RemoteLogID{
	//日志的类型，为每条log第一个冒号前的单词
	//例：login：account=hbe4589:userid=12542045:peer=58.22.232.12
	//上例中日志的类型为：login
	public final static int ROLELOGIN = 1;
	public final static int ROLELOGOUT = 2;
	public final static int CREATEROLE = 3;
	public final static int LEVELUP = 4;
	public final static int ADDCASH = 5;
	public final static int ADDYUANBAO = 6;
	public final static int COSTYUANBAO = 7;
	public final static int ONLINEUSER = 8;
}
