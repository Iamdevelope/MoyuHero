package chuhan.gsp.buff;



public class BuffConstant
{
	//清除类型
	
	public static final int CLEAR_TYPE_OUT_BATTLE = 0;//出战斗清除（下线也清除）
	
	public static final int CLEAR_TYPE_OFFLINE = 1;//下线清除
	
	public static final int CLEAR_TYPE_OFFLINE_KEEP_TIMING = 2;//下线不清除（如果有计时，继续计时）
	
	public static final int CLEAR_TYPE_OFFLINE_STOP_TIMING = 3;//下线不清除（如果有计时，封存计时）
	
	public static final int CLEAR_TYPE_IN_BATTLE_HURT = 8;//战斗内受伤清除（出战斗也清除）
	
	public static final int CLEAR_TYPE_IN_BATTLE_DEATH = 9;//战斗内死亡清除（出战斗也清除）
	
	
	
	
	public static final int BUFF_WEAPON = 500001;
	public static final int BUFF_ARMOR = 500002;
	public static final int BUFF_HORSE = 500003;
	public static final int BUFF_SKILL = 500004;
	public static final int BUFF_RELATION = 500005;
	public static final int BUFF_VICE_HERO = 500006;	
	public static final int BUFF_BLOOD_BATTLE = 500007;	
	public static final int BUFF_DEATH = 500101;
	public static final int BUFF_CAMP = 500102;
	public static final int BUFF_CAMP_CONTINUWIN = 500103;
}
