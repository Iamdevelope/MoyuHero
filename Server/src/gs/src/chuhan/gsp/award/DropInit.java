package chuhan.gsp.award;

/**
 * 掉落类
 * @author aa
 *
 */
public class DropInit
{
	public int percent;
	public final int id;
	int num;
	
	public int dropparameter1 = 0;
	public int oddsAdd = 0;
	public int lotteryteam = -1;
	public int sameType = -1;
	public DropInit(int percent, int id, int num)
	{
		this.percent = percent;
		this.id = id;
		this.num = num;
	}
	public DropInit(int percent, int id, int num,int dropparameter1,int oddsAdd,int lotteryteam)
	{
		this.percent = percent;
		this.id = id;
		this.num = num;
		this.dropparameter1 = dropparameter1;
		this.oddsAdd = oddsAdd;
		this.lotteryteam = lotteryteam;
	}
}
