package chuhan.gsp;

public class PlatformTypeStr {

	
	public final static int OS_TYPE_IOS_PB = 1;//IOS越狱
	public final static int OS_TYPE_ANDROID = 2;//安卓
	public final static int OS_TYPE_IOS_APPSTORE = 3;//appstore简体
	public final static int OS_TYPE_IOS_APPSTORE_TRD = 4;//appstore繁体
	
	public static int getOsType(String platstr)
	{
		if(isLaHu(platstr))
			return OS_TYPE_IOS_APPSTORE;
		else if(isAppT(platstr))
			return OS_TYPE_IOS_APPSTORE_TRD;
		else if(isNormal(platstr)
				|| isWanglong91(platstr)
				|| isTieren25pp(platstr)
				|| isTieren26pp(platstr)
				|| isAppStore(platstr)
				|| isTongbutui(platstr)
				)
			return OS_TYPE_IOS_PB;
		else if(isWanglongAd91(platstr)
				|| isYoushiUC(platstr)
				|| isQihoo360(platstr)
				|| isDownjoy(platstr)
				|| isBaiduDuoku(platstr)
				|| isXiaoMi(platstr)
				|| isOppo(platstr)
				|| isWanmei173(platstr)
				|| isHuaWei(platstr)
				|| isGooglePlay(platstr)
				|| isUnicom(platstr)
				)
			return OS_TYPE_ANDROID;
		else
			return 0;
	}
	
	public static final String Normal = "wanmei"; // 完美通行证
	public final static String Wanglong91 = "wl91"; // 91
	public final static String WanglongAD91 = "ad91"; // 安卓91
	public final static String Tieren25pp = "25pp"; // PP
	public final static String Tieren26pp = "26pp"; // PP2
	public final static String AppStore = "apps"; // apps
	public final static String Tongbutui = "tngb"; // 同步推
	public final static String YoushiUC = "ysuc"; // 优视UC
	public final static String Wanmei173 = "w173"; // 完美173
	public final static String Qihoo360 = "qiho"; // 奇虎360
	public final static String Downjoy = "djoy"; // 当乐网
	public final static String BaiduDuoku = "doku"; // 百度多酷
	public final static String Xiaomi = "ximi"; // 小米
	public final static String LaHu = "lahu"; // appstore简体
	public final static String AppT = "appt"; // appstore繁体
	public final static String Oppo = "oppo"; //OPPO
	public final static String HuaWei = "hawi"; //华为
	public final static String GooglePlay = "goog"; //GooglePlay
	public final static String Unicom = "unic"; //联通
	
	public static final boolean isNormal(String platstr)
	{
		return platstr.equalsIgnoreCase(Normal);
	}
	public static final boolean isWanglong91(String platstr)
	{
		return platstr.equalsIgnoreCase(Wanglong91);
	}
	public static final boolean isWanglongAd91(String platstr)
	{
		return platstr.equalsIgnoreCase(WanglongAD91);
	}
	public static final boolean isTieren25pp(String platstr)
	{
		return platstr.equalsIgnoreCase(Tieren25pp);
	}
	public static final boolean isTieren26pp(String platstr)
	{
		return platstr.equalsIgnoreCase(Tieren26pp);
	}
	public static final boolean isAppStore(String platstr)
	{
		return platstr.equalsIgnoreCase(AppStore);
	}
	public static final boolean isLaHu(String platstr)
	{
		return platstr.equalsIgnoreCase(LaHu);
	}
	public static final boolean isAppT(String platstr) {
		return platstr.equalsIgnoreCase(AppT);
	}
	public static final boolean isTongbutui(String platstr)
	{
		return platstr.equalsIgnoreCase(Tongbutui);
	}
	public static final boolean isYoushiUC(String platstr)
	{
		return platstr.equalsIgnoreCase(YoushiUC);
	}
	public static final boolean isWanmei173(String platstr)
	{
		return platstr.equalsIgnoreCase(Wanmei173);
	}
	public static final boolean isQihoo360(String platstr)
	{
		return platstr.equalsIgnoreCase(Qihoo360);
	}
	public static final boolean isDownjoy(String platstr)
	{
		return platstr.equalsIgnoreCase(Downjoy);
	}
	public static final boolean isBaiduDuoku(String platstr)
	{
		return platstr.equalsIgnoreCase(BaiduDuoku);
	}
	public static final boolean isXiaoMi(String platstr) {
		return platstr.equalsIgnoreCase(Xiaomi);
	}
	public static final boolean isOppo(String platstr) {
		return platstr.equalsIgnoreCase(Oppo);
	}
	public static final boolean isHuaWei(String platstr) {
		return platstr.equalsIgnoreCase(HuaWei);
	}
	public static final boolean isUnicom(String platstr) {
		return platstr.equalsIgnoreCase(Unicom);
	}
	public static final boolean isGooglePlay(String platstr) {
		return platstr.equalsIgnoreCase(GooglePlay);
	}
}
