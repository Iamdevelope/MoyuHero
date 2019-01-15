package gnet.link;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;
import java.util.TreeSet;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.GameProp;

import org.apache.log4j.Logger;

import com.goldhuman.Common.Marshal.OctetsStream;

public class OnlineUsers extends HashMap<Integer, User>
{
	private int INIT_HEALTH_ONLINE_USER_NUM = 1; //初始健康承载上限，只在初始化时有用
	public int HEALTH_ONLINE_USER_NUM = 1;//健康承载上限，开始排队
	public int MAX_ONLINE_USER_NUM = 20;//最大承载上限，登入不允许
	private int maxLoginSize = INIT_HEALTH_ONLINE_USER_NUM;
	private int maxQueueSize = MAX_ONLINE_USER_NUM - maxLoginSize;
	
	private static long INCREASE_PERIOD = 10000;//增长间隔ms
	private static int INCREASE_NUM = 50;// 每次增长的个数
	private long lastIncreaseTime = 0;
	
	private static long ZERO_QUEUE_TICKET_MILLISECONDS = 120000; //0排位票的保存最长时间
	private static long BROADCAST_QUEUE_PERIOD = 20000;//广播客户端刷新排队信息的最小间隔
	private long lastBroadcastTime = 0;
	public int averageLoginSpeed = 3;//平均登入速度，人/分钟，最小为3
	private List<Integer> loginSpeeds = new LinkedList<Integer>();//记录最近3次的登入速度
	private int curLoginNum = 0;//当前Broadcast间隔内登入的人数
	
	private UserQueue userQueue = new UserQueue(User.STATE_IN_QUEUE);//正在排队的用户
	private UserQueue zeroUserQueue = new UserQueue(User.STATE_ZERO_QUEUE);//0排位的用户
	private Map<Integer,Long> zeroQueueTickets = new HashMap<Integer, Long>();//0排位的门票，保留两分钟，需要定期清空
	
	static private final Logger logger=Logger.getLogger(OnlineUsers.class);
	final private int userState = User.STATE_LOGIN;
	
	private static final long serialVersionUID = -2032473210881256513L;
	
	public final Map<Integer,User> waitingauuserinfos = new TreeMap<Integer,User>();
	
	public OnlineUsers()
	{
		super();
		initData();
	}
	
	private void initData()
	{
		INIT_HEALTH_ONLINE_USER_NUM = GameProp.getIntValue(ConfigManager.getInstance().getPropConf("sys"), "sys.queue.onlinenum.inithealth");
		HEALTH_ONLINE_USER_NUM = GameProp.getIntValue(ConfigManager.getInstance().getPropConf("sys"), "sys.queue.onlinenum.health");
		MAX_ONLINE_USER_NUM = GameProp.getIntValue(ConfigManager.getInstance().getPropConf("sys"), "sys.queue.onlinenum.max");
		ZERO_QUEUE_TICKET_MILLISECONDS = GameProp.getIntValue(ConfigManager.getInstance().getPropConf("sys"), "sys.queue.zeroticket.time");
		BROADCAST_QUEUE_PERIOD = GameProp.getIntValue(ConfigManager.getInstance().getPropConf("sys"), "sys.queue.broadcast.time");
		INCREASE_PERIOD = GameProp.getIntValue(ConfigManager.getInstance().getPropConf("sys"), "sys.queue.increase.period");
		INCREASE_NUM = GameProp.getIntValue(ConfigManager.getInstance().getPropConf("sys"), "sys.queue.increase.num");
		
		maxLoginSize = INIT_HEALTH_ONLINE_USER_NUM;
		maxQueueSize = MAX_ONLINE_USER_NUM - maxLoginSize;
	}
	
	/**
	 * 设置HEALTH_ONLINE_USER_NUM  MAX_ONLINE_USER_NUM
	 * 并试图提高maxQueueSize
	 * @param healthnum
	 * @param maxnum
	 */
	public synchronized void setInitData(int healthnum, int maxnum)
	{
		if(healthnum > maxnum)
			throw new IllegalArgumentException("maxnum必须大于healthnum");
		HEALTH_ONLINE_USER_NUM = healthnum;
		MAX_ONLINE_USER_NUM = maxnum;
		maxQueueSize = MAX_ONLINE_USER_NUM- maxLoginSize;
		tryIncMaxLoginSize(chuhan.gsp.main.GameTime.currentTimeMillis());
	}
	
	@Override
	public synchronized User put(Integer key, User user)
	{
		curLoginNum++;
		user.setState(userState);
		//返回SRoleList
		user.sendRoleList();
		
		return super.put(key, user);
	}
	
	@Override
	public synchronized User get(Object key)
	{
		return super.get(key);
	}
	
	
	/**
	 * 根据现在的登入最大人数刷新队列
	 */
	private void loginFromQueue()
	{
		while(size() < maxLoginSize)
		{
			User newuser = getUserFromQueue();
			
			if(newuser == null)
				break;
			
			put(newuser.getUserid(), newuser);
		}
		return;
	}
	
	@Override
	public synchronized User remove(Object key)
	{
		User olduser = super.remove(key);
		if(olduser == null)
			return null;
		olduser.setLogin(SetLogin.eLogout);
		tryIncMaxLoginSize(chuhan.gsp.main.GameTime.currentTimeMillis());//检查当前登入上限是否有改变
		loginFromQueue();//试图选排队的角色登入，达到登入上限则不能登入
		return olduser; 
	}
	
	/**
	 * 通过userid获取User，可能在登入的账号里，也可能在排队中
	 * User里的getState表示该账户处于的状态
	 * @param userid
	 * @return
	 */
	public User find(int userid)
	{
		User user = get(userid);
		if(user != null)
			return user;
		user = zeroUserQueue.get(userid);
		if(user != null)
			return user;
		
		user = userQueue.get(userid);
		if(user != null)
			return user;
		
		return null;
	}
	
	/**
	 * 账号掉线时的处理
	 * @param userid
	 * @param isLinkBroken
	 * @return
	 */
	public synchronized User offline(int userid, boolean isLinkBroken)
	{
		User user = remove(userid);
		if(user != null)
		{
			if(isLinkBroken)
				zeroQueueTickets.put(userid, chuhan.gsp.main.GameTime.currentTimeMillis());
			return user;
		}
		
		user = zeroUserQueue.get(userid);
		if(user != null)
		{
			if(isLinkBroken)
				user.setLinkBroken(true);
			else
				zeroUserQueue.remove(userid);
			return user;
		}
		
		user = userQueue.get(userid);
		if(user != null)
		{
			if(isLinkBroken)
				user.setLinkBroken(true);
			else
			{
				userQueue.remove(userid);
				broadcastQueueInfo(chuhan.gsp.main.GameTime.currentTimeMillis());
			}
			return user;
		}
		logger.info("LinkBroken no User");
		return null;
	}
	
	
	public synchronized User online(xio.Protocol p2) {
		Dispatch ctx = (Dispatch) p2.getContext();
		return online(Onlines.getInstance().find(p2.getConnection()), ctx.linksid, ctx.userid);
	}
	
	/**
	 * User登录，插入User并发送相应协议
	 * @param link
	 * @param linksid
	 * @param userid
	 * @return
	 */
	private User online(Link link, int linksid, int userid) {
		if (null != link)
		{
			User old = get(userid);
			UserQueue queue = null;
			if (old != null)
				logger.info("User登录时已经有了旧的User，会有这种情况出现吗？UserId = " + userid);
			if (null == old)
			{
				old = zeroUserQueue.get(userid);
				queue = zeroUserQueue;
			}
			if (null == old)
			{
				old = userQueue.get(userid);
				queue = userQueue;
			}
			// 如果已有
			if (null != old)
			{
				old.setLinkBroken(false);
				old.linkAttach(link, linksid);
				if (queue != null)
				{
					queue.sendQueueInfo(old);
					return old;
				}
				put(userid, old);
				return old;
			}
			// 新登录
			User user = new User(userid);
			user.linkAttach(link, linksid);
			long now = chuhan.gsp.main.GameTime.currentTimeMillis();
			// 看是否到达健康上限
			if (tryIncMaxLoginSize(now) > size())
			{
				put(userid, user);
				return user;
			}
			
			//是否可以插队
			/*if(ConfigManager.getInstance().getConf(JumpQueueUser.class).containsKey(userid))
			{
				put(userid, user);
				return user;
			}*/

			// 达到上限，看是否有0排位票
			Long time = zeroQueueTickets.get(userid);
			if (time != null)
			{
				zeroQueueTickets.remove(userid);
				if ((now - time) < ZERO_QUEUE_TICKET_MILLISECONDS)
				{
					zeroUserQueue.put(user.getUserid(), user);
					broadcastQueueInfo(now);
					return user;
				}
			}

			// 看排队人数是否已满
			if (userQueue.size() + zeroUserQueue.size() < maxQueueSize)
			{
				userQueue.put(user.getUserid(), user);
				broadcastQueueInfo(now);
				return user;
			} else
			{// 不允许登录
				user.setState(User.STATE_FAILED);
				//XXX user.send(new SSendQueueInfo(-1, -1, -1));
				return user;
			}
		}
		else
			logger.info("Insert User : Link not found");
		return null;
	}
	
	/**
	 * 从两个排队队列中获取一个正在排队的User
	 * @return null表示队列中无正在排队的玩家
	 */
	public User getUserFromQueue()
	{
		User user = getUserFromQueue(zeroUserQueue);
		if(user != null)
			return user;
		
		return getUserFromQueue(userQueue);
	}
	
	private User getUserFromQueue(UserQueue queue)
	{
		if(queue.isEmpty())
			return null;
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		Iterator<User> it = queue.values().iterator();
		while(it.hasNext())
		{
			User user = it.next();
			it.remove();
			if(!user.isLinkBroken())
			{
				broadcastQueueInfo(now);//广播队列信息
				return user;
			}
			zeroQueueTickets.put(user.getUserid(), now);//排到他，他却掉线
		}
		return null;
	}
	
	//定期广播排队信息
	synchronized void broadcastQueueInfo(long now)
	{
		long period = now - lastBroadcastTime;
		if(period < BROADCAST_QUEUE_PERIOD)
			return;
		lastBroadcastTime = now;
		int size = userQueue.size();
		if(zeroUserQueue.size() > 0)
		{
			/*SSendQueueInfo snd = new SSendQueueInfo(0, size, 0);
			send(zeroUserQueue.values(), snd);*/
		}
		
		averageLoginSpeed = calcAvgLoginSpeed(period);
		
		int i = 0;
		for(User user : userQueue.values())
		{
			i++;
			/*SSendQueueInfo snd = new SSendQueueInfo(i, size, (i /averageLoginSpeed) + 1);
			user.send(snd);*/
		}
		//清理过期票
		List<Integer> timeoutTickets = new ArrayList<Integer>(); 
		for(Map.Entry<Integer, Long> entry : zeroQueueTickets.entrySet())
		{
			if((now - entry.getValue()) >  ZERO_QUEUE_TICKET_MILLISECONDS)
				timeoutTickets.add(entry.getKey());
		}
		for(int userid : timeoutTickets)
			zeroQueueTickets.remove(userid);
	}
	/**
	 * 计算平均登入速度
	 * @return
	 */
	private int calcAvgLoginSpeed(long period)
	{
		int curspeed = (int)(curLoginNum * (60000.0 / period));
		
		loginSpeeds.add(curspeed);
		int sum = 0;
		for(int speed : loginSpeeds)
			sum += speed;
		int avgspeed = sum/loginSpeeds.size();
		if(loginSpeeds.size() >= 3)
			loginSpeeds.remove(0);
		curLoginNum = 0;
		return Math.max(3, avgspeed);//最小1分钟进3人
	}
	
	int getUserQueueSize(int queueUserState)
	{
		if(queueUserState == User.STATE_IN_QUEUE)
			return userQueue.size();
		else if(queueUserState == User.STATE_ZERO_QUEUE)
			return zeroUserQueue.size();
		else
			return 0;
	}
	
	/**
	 * 群发，给多个用户发送协议。
	 * 
	 * 根据用户所在的link分组发送。
	 * 
	 * @return
	 *     true 全部发送成功。
	 *     false 发送失败（可能部分发送成功）。不报告所有错误，仅记录日志。
	 */
	public boolean send(Collection<User> users, xio.Protocol p2) {
		Map<Link, HashSet<Integer>> group = new HashMap<Link, HashSet<Integer>>();

		boolean rc = true;
		for (User user : users) {
			Link.Session ls = user.getLinkSession();
			if (null == ls) {
				rc = false;
				//logger.warn("send2 role has broken, roleid=" + roleid);
				continue;
			}
			HashSet<Integer> si = group.get(ls.getLink());
			if (null == si)
				group.put(ls.getLink(), si = new HashSet<Integer>());
			si.add(ls.getSid());
		}

		Send msg = new Send();
		msg.ptype = p2.getType();
		msg.pdata = new OctetsStream().marshal(p2);

		for (Map.Entry<Link, HashSet<Integer>> e : group.entrySet()) {
			msg.linksids.clear();
			if (Onlines.getInstance().send(e.getKey(), e.getValue(), msg))
				continue;
			rc = false;
			logger.warn("send2 error, p2=" + Integer.toHexString(p2.getType()) + "link=" + e.getKey());
		}
		return rc;
	}
	
	/**
	 * 试图提升maxLoginSize
	 * 如果当前数没有达到maxLoginSize，返回当前maxLoginSize
	 * 如果当前数达到HEALTH_ONLINE_USER_NUM，返回当前maxLoginSize
	 * 如果当前数超过HEALTH_ONLINE_USER_NUM，设置maxLoginSize为当前已登入的数量，并返回
	 * 如果当前数达到maxLoginSize，且没有达到HEALTH_ONLINE_USER_NUM，试图提高maxLoginSize（看时间）
	 * @return 提升后的maxLoginSize
	 */
	private int tryIncMaxLoginSize(long now)
	{
		if(maxLoginSize > HEALTH_ONLINE_USER_NUM)
		{
			setMaxLoginSize(Math.max(size(), HEALTH_ONLINE_USER_NUM));
			return maxLoginSize; 
		}
		
		if(size() < maxLoginSize)
			return maxLoginSize;//还没到排队的程度
		
		if(maxLoginSize < HEALTH_ONLINE_USER_NUM)
		{//没到到HEALTH_ONLINE_USER_NUM，则检测是否能增长
			long period = now - lastIncreaseTime;
			if(period >= INCREASE_PERIOD)
			{
				int num = Math.min(6, (int)(period / INCREASE_PERIOD));
				setMaxLoginSize(Math.min(HEALTH_ONLINE_USER_NUM, maxLoginSize + INCREASE_NUM * num));
				lastIncreaseTime = now;
			}
		}
		
		return maxLoginSize;
	}
	
	/**
	 * 不仅要更新maxLoginSize，还得更新maxQueueSize
	 * 同时可能要选取正在排队的登入
	 * @param maxloginsize
	 */
	private void setMaxLoginSize(int maxloginsize)
	{
		this.maxLoginSize = maxloginsize;
		maxQueueSize = MAX_ONLINE_USER_NUM - maxloginsize;
		loginFromQueue();//增长时会优先选择队列中的用户登入
	}
	
	public synchronized int getUserNumInQueue()
	{
		return userQueue.size() + zeroUserQueue.size();
	}
	
	public static void main(String[] args)
	{
		LinkedHashMap<Integer, String> maps = new LinkedHashMap<Integer, String>();
		maps.put(2, "2");
		maps.put(3, "3");
		maps.put(1, "1");
		Iterator<String> it = maps.values().iterator();
		String str = it.next();
		it.remove();
		System.out.println(str);
		str = it.next();
		System.out.println(str);
		
	}
	
	public void addAuuserInfoId(int userid, User user)
	{
		synchronized (waitingauuserinfos) {
			waitingauuserinfos.put(userid,user);
		}
	}
	public User removeAuuserInfoId(int userid)
	{
		synchronized (waitingauuserinfos) {
			return waitingauuserinfos.remove(userid);
		}
	}
}
