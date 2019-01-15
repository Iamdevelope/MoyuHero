package gnet.link;

import java.util.Iterator;
import java.util.LinkedHashMap;

public class UserQueue extends LinkedHashMap<Integer,User>
{
	private static final long serialVersionUID = -7290627688877735735L;
	final private int userState;
	public UserQueue(int userState)
	{
		super();
		this.userState = userState;
	}
	
	
	@Override
	public synchronized User put(Integer userid, User e)
	{
		e.setState(userState);
		User u = super.put(userid, e);
		int size = size();
		sendQueueInfo(e, size);
		return u;
	}
	
	public void sendQueueInfo(User user,int order)
	{
		int size = Onlines.getInstance().getOnlineUsers().getUserQueueSize(User.STATE_IN_QUEUE);
		/*if(userState == User.STATE_IN_QUEUE)
			user.send(new SSendQueueInfo(order,size,(order / Onlines.getInstance().getOnlineUsers().averageLoginSpeed) + 1));
		else
			user.send(new SSendQueueInfo(0,size,0));*/
	}
	
	public void sendQueueInfo(User user)
	{
		if(userState == User.STATE_ZERO_QUEUE)
			sendQueueInfo(user, 0);
		else
		{
			int order = 1;
			Iterator<User> it = this.values().iterator();
			while(it.hasNext())
			{
				User tmpuser = it.next();
				if(user.getUserid() == tmpuser.getUserid())
					break;
				order++;
			}
			sendQueueInfo(user, order);
		}
	}
	
	@Override
	public synchronized User remove(Object key)
	{
		return super.remove(key);
	}
	
}
