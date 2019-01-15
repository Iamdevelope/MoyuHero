package chuhan.gsp.hero;

import java.util.Map;

import chuhan.gsp.ColorType;
import chuhan.gsp.LangueVersion;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoConsumeType;
import chuhan.gsp.award.AwardItem;
import chuhan.gsp.award.AwardManager;
import chuhan.gsp.award.AddItem;
import chuhan.gsp.game.SDianjiangGailv;
import chuhan.gsp.game.SDianjiangQuanzhi;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.Misc;

public class PSelectHero extends xdb.Procedure
{
	
	public static int GOLDEN_COST = 298;
	public static int SILVER_COST = 98;
	public static int BRONZE_COST = 10;
	
	private final long roleId;
	private final int selecttype;
	public PSelectHero(long roleId, int selecttype) {
		this.roleId = roleId;
		this.selecttype = selecttype;
	}
	
	@Override
	protected boolean process() throws Exception {
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.Properties xprop = xtable.Properties.get(roleId);
		if(xprop == null)
			return false;
		/*
		if(selecttype == 1)
			selectGolden(xprop, now);
		else if(selecttype == 2)
			selectSilver(xprop, now);
		else if(selecttype == 3)
			selectBronze(xprop, now);
		*/
		return true;
	}
	/*
	private boolean selectGolden(xbean.Properties xprop, long now)
	{
		boolean isfree =  (now > xprop.getGoldenfreetime());
		if(LangueVersion.isJapan()) {
			isfree = (xprop.getGoldenfreeselect() == 0);
		}
		if(!isfree)
			if(!consumeYuanbao(GOLDEN_COST))
				return false;
		int sumtimes = xprop.getGoldenfreeselect() + xprop.getGoldenyuanselect();
		int todaytimes = -1;
		
		int awardId = 0;
		float addvalue = (float)ConfigManager.getInstance().getConf(SDianjiangQuanzhi.class).get(isfree ? 2 : 4).quanzhi;
		xprop.setSelectvalue(xprop.getSelectvalue()+addvalue);
		if(sumtimes == 0)
			awardId = 100020;
		else if(sumtimes == 1)
			awardId = 100022;
		else
		{//随机
			SDianjiangGailv gailv = ConfigManager.getInstance().getConf(SDianjiangGailv.class).get((int)xprop.getSelectvalue());
			int random = (gailv == null)? 1000 : gailv.ppurple;
			if(Misc.getRandomBetween(1, 1000) <= random)
				awardId = 100026;
			else
				awardId = 100023;
		}
		OldHero hero = giveHero(awardId);
		//by yanglk  hero		if(hero.getColor() >= ColorType.PURPLE)
		//by yanglk  hero			xprop.setSelectvalue(0);
		if(isfree)
			xprop.setGoldenfreeselect(xprop.getGoldenfreeselect()+1);
		else
			xprop.setGoldenyuanselect(xprop.getGoldenyuanselect()+1);
		
		long nextfreetime = isfree? getNextFreeTime(xprop, 1, now) : xprop.getGoldenfreetime();
		if(isfree)
			xprop.setGoldenfreetime(nextfreetime);
		int addsoul = (DateUtil.getCurrentWeekDay() != 5)? 38 : 58;
		specialActivity(addsoul);
		//by yanglk  hero		if(hero.getColor() >= ColorType.PURPLE && sumtimes > 2)
		//by yanglk  hero			Message.pbroadcastMsgNotify(160, xprop.getRolename(),hero.getConfig().getName());
		//by yanglk  hero		sendSSelectHero(hero.getHeroInfo().getKey(), nextfreetime, todaytimes);
		if(sumtimes > 0) {
			goldenActivity();
		}
		return true;
	}
	
	private boolean selectSilver(xbean.Properties xprop, long now)
	{
		boolean isfree =  (now > xprop.getSilverfreetime());
		if(LangueVersion.isJapan()) {
			isfree = false;
		}
		if(!isfree)
			if(!consumeYuanbao(SILVER_COST))
				return false;
		int sumtimes = xprop.getSilverfreeselect() + xprop.getSilveryuanselect();
		int todaytimes = -2;
		
		int awardId = 0;
		float addvalue = (float)ConfigManager.getInstance().getConf(SDianjiangQuanzhi.class).get(isfree ? 1 : 3).quanzhi;
		xprop.setSelectvalue(xprop.getSelectvalue()+addvalue);
		if(sumtimes == 0)
			awardId = 100019;
		else if(sumtimes == 1)
			awardId = 100021;
		else
		{//随机
			SDianjiangGailv gailv = ConfigManager.getInstance().getConf(SDianjiangGailv.class).get((int)xprop.getSelectvalue());
			int random = (gailv == null)? 1000 : gailv.ppurple;
			if(Misc.getRandomBetween(1, 1000) <= random)
				awardId = 100026;
			else
				awardId = 100024;
		}
		OldHero hero = giveHero(awardId);
		//by yanglk  hero		if(hero.getColor() >= ColorType.PURPLE)
		//by yanglk  hero			xprop.setSelectvalue(0);
		if(isfree)
			xprop.setSilverfreeselect(xprop.getSilverfreeselect()+1);
		else
			xprop.setSilveryuanselect(xprop.getSilveryuanselect()+1);
		long nextfreetime = isfree? getNextFreeTime(xprop, 2, now) : xprop.getSilverfreetime();
		if(isfree)
			xprop.setSilverfreetime(nextfreetime);
		//by yanglk  hero		if(hero.getColor() >= ColorType.PURPLE)
		//by yanglk  hero			Message.pbroadcastMsgNotify(160, xprop.getRolename(),hero.getConfig().getName());
		int addsoul = (DateUtil.getCurrentWeekDay() != 5)? 12 : 18;
		specialActivity(addsoul);
		//by yanglk  hero		sendSSelectHero(hero.getHeroInfo().getKey(), nextfreetime, todaytimes);
		return true;
	}
	
	private boolean selectBronze(xbean.Properties xprop, long now)
	{
		int todaytimes = getTodayBronzeFreeTimes(xprop, now);
		//铜牌的freetime比较特殊
		long nextfreetime = getNextFreeTime(xprop, 3, now);
		boolean isfree =  (now > nextfreetime);
		if(LangueVersion.isJapan()) {
			isfree = false;
		}
		if(!isfree)
			if(!consumeYuanbao(BRONZE_COST))
				return false;
		OldHero hero = giveHero(100025);
		//by yanglk  hero		if(hero.getColor() >= ColorType.PURPLE)
		//by yanglk  hero			xprop.setSelectvalue(0);
		if(isfree)
		{
			xprop.setBronzefreetime(now);
			xprop.setBronzefreeselect(xprop.getBronzefreeselect()+1);
			xprop.setTodayfreetimes(xprop.getTodayfreetimes()+1);
		}
		else
		{
			xprop.setBronzeyuanselect(xprop.getBronzeyuanselect()+1);
			//specialActivity(0);
		}
		nextfreetime = getNextFreeTime(xprop, 3, now);
		//by yanglk  hero		sendSSelectHero(hero.getHeroInfo().getKey(), nextfreetime, todaytimes);
		return true;
	}
	
	private OldHero giveHero(int awardId)
	{
		Map<Integer,AwardItem> awarditems = AwardManager.getInstance().distributeAllAward(roleId, awardId,null, false);
		AwardItem awarditem = awarditems.get(AwardManager.FIRSTC_AWARD);
		if(awarditem == null)
			awarditem = awarditems.get(AwardManager.SECONDC_AWARD);
		AddItem itm = awarditem.getItems().get(0);
		int herokey = itm.getKey();
		OldHeroColumn herocol = OldHeroColumn.getHeroColumn(roleId, false);
		return herocol.getHero(herokey);
	}
	
	private void sendSSelectHero(int herokey, long nextfreetime, int todaytimes)
	{
		SSelectHero snd = new SSelectHero();
		snd.herokey = herokey;
		snd.nextfreetime = nextfreetime;
		snd.todaytimes = Conv.toByte(todaytimes);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	private boolean consumeYuanbao(int consumeyuanbao)
	{
		PropRole prole = PropRole.getPropRole(roleId, false);
		if(prole.delYuanBao(-consumeyuanbao, YuanBaoConsumeType.SELECT_HERO) != -consumeyuanbao)
			return false;
		return true;
	}
	
	public static int getTodayBronzeFreeTimes(xbean.Properties xprop, long now)
	{
		if(xprop.getTodayfreetimes() == 0)
			return 0;
		if(DateUtil.inTheSameDay(xprop.getBronzefreetime(), now))
			return xprop.getTodayfreetimes();
		else
		{
			xprop.setTodayfreetimes(0);
			return 0;
		}
	}
	
	public static long getNextFreeTime(xbean.Properties xprop, int type, long now)
	{
		if(type == 1)
		{
			int sumtime = xprop.getGoldenfreeselect() + xprop.getGoldenyuanselect();
			if(sumtime == 0)
				return now + 1*DateUtil.minuteMills;
			if(sumtime == 1)
				return now + DateUtil.dayMills;
			return now + 70 * DateUtil.hourMills;
		}
		
		if(type == 2)
		{
			int sumtime = xprop.getSilverfreeselect() + xprop.getSilveryuanselect();
			if(sumtime == 0)
				return now;
			if(sumtime == 1)
				return now + 12 * DateUtil.hourMills;
			return now + 22 * DateUtil.hourMills;
		}
		
		if(type == 3)
		{
			long nexttime = xprop.getBronzefreetime() + 7*DateUtil.minuteMills;
			if(!DateUtil.inTheSameDay(nexttime, now))
				nexttime = DateUtil.getDayFirstSecond(nexttime);
			else
			{
				if(xprop.getTodayfreetimes() >= 5)
					nexttime = DateUtil.getDayFirstSecond(now) + DateUtil.dayMills;
			}
			return nexttime;
		}
		
		throw new IllegalArgumentException("type = " + type);
	}
	
	public static void onCreateRole(xbean.Properties xprop, long now)
	{
		xprop.setGoldenfreetime(getNextFreeTime(xprop, 1, now));
		xprop.setSilverfreetime(getNextFreeTime(xprop, 2, now));
		xprop.setBronzefreetime(now);
	}
	*/
	/**
	 * 存储过程内使用
	 * @param roleId
	 * @param xprop
	 * @param now
	 */
	/*
	public static void sendSSendFreeSelectTime(long roleId)
	{
		xbean.Properties xprop = xtable.Properties.get(roleId);
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		SSendFreeSelectTime snd = new SSendFreeSelectTime();
		snd.todaytimes = Conv.toByte(getTodayBronzeFreeTimes(xprop, now));
		snd.goldselecttime = xprop.getGoldenfreetime();
		snd.silverselecttime = xprop.getSilverfreetime();
		snd.bronzeselecttime = getNextFreeTime(xprop, 3, now);
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	*/
	
	private void specialActivity(int addsoul)
	{
		/*long now = GameTime.currentTimeMillis();
		Calendar c = Calendar.getInstance();
		c.setTimeInMillis(now);
		if(c.get(Calendar.YEAR) != 2013)
			return;
		if(c.get(Calendar.MONTH) != Calendar.APRIL)
			return;
		int dayofmonth = c.get(Calendar.DAY_OF_MONTH);
		if(dayofmonth < 1 || dayofmonth > 10)
			return;
		if(dayofmonth == 10)
		{
			if(c.get(Calendar.HOUR_OF_DAY) >= 11)
				return;
		}//*/
		PropRole prole = PropRole.getPropRole(roleId, false);
//		prole.addSoul(addsoul);
		//Message.psendMsgNotifyWhileCommit(roleId, 122,addsoul);
	}
	
	private void goldenActivity()
	{
		/*long now = GameTime.currentTimeMillis();
		Calendar c = Calendar.getInstance();
		c.setTimeInMillis(now);
		if(c.get(Calendar.YEAR) != 2013)
			return;
		int month = c.get(Calendar.MONTH);
		int dayofmonth = c.get(Calendar.DAY_OF_MONTH);
		if(month == Calendar.JUNE)
		{
			if(dayofmonth < 26)
				return;
		}
		else if(month == Calendar.JULY)
		{
			if(dayofmonth >= 3)
				return;
		}
		else
			return;*/
		AwardManager.getInstance().distributeAllAward(roleId, 100766, null, true);
	}
}
