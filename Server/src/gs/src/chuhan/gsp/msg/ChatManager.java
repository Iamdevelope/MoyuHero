package chuhan.gsp.msg;

import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import chuhan.gsp.main.GameTime;
import chuhan.gsp.util.StringValidateUtil;

public class ChatManager {
	
	public static final int MAX_MSG_NUM = 100;
	public static final int MSG_SEND_INTERVAL = 5000;//5s
	public static final int MSG_MAX_LENGTH = 25;//25个字
	private static ChatManager instance = new ChatManager();
	public static ChatManager getInstance()
	{
		return instance;
	}
	private ChatManager(){}
	
	private List<ChatMessage> chatmsgs = new LinkedList<ChatMessage>(); //聊天记录，只保留最近的XX条
	private Map<Long,Long> inchatroles = new HashMap<Long,Long>();//在聊天界面的人，value 是发送上一条信息的时间
	private Map<Long,Long> absentroles = new HashMap<Long,Long>();//暂离界面的人，value 是收到上一条信息的时间
	/*
	 * 开界面即进入
	 */
	public synchronized void enterChat(long roleId, int num)
	{
		if(inchatroles.containsKey(roleId))
			return;//已经在了
		Long lastsend = absentroles.remove(roleId);
		if(lastsend == null)
			lastsend = 0l;
		List<ChatMessage> msgs = getChatMsgs(num, lastsend);
		if(!msgs.isEmpty())
		{
			try{
				SSendChatMsg snd = new SSendChatMsg();
				for(ChatMessage msg : msgs)
					snd.msgs.add(new ChatMsg(msg.rolename,msg.msg));
				gnet.link.Onlines.getInstance().send(roleId, snd);
			}catch(Exception e){
				e.printStackTrace();
			}
		}
		inchatroles.put(roleId, GameTime.currentTimeMillis());
	}
	/*
	 * 关界面即暂离
	 */
	public synchronized void absentChat(long roleId)
	{
		if(inchatroles.remove(roleId) == null)
			return;
		absentroles.put(roleId, GameTime.currentTimeMillis());
	}
	/*
	 * 下线才离开
	 */
	public synchronized void leaveChat(long roleId)
	{
		inchatroles.remove(roleId);
		absentroles.remove(roleId);
	}
	
	/**
	 * 发送聊天
	 * @param roleId
	 * @param rolename
	 * @param msg
	 */
	public synchronized void sendChat(long roleId, String rolename, String msg)
	{
		if(msg.length() > MSG_MAX_LENGTH)
		{
			Message.sendMsgNotify(roleId, 170);
			return;
		}
		Long lastsend = inchatroles.get(roleId);
		long now = GameTime.currentTimeMillis();
		if(lastsend != null)
		{
			if(now - lastsend < MSG_SEND_INTERVAL)
			{
				Message.sendMsgNotify(roleId, 169);
				return;
			}
		}
		lastsend = now;
		inchatroles.put(roleId, lastsend);
		msg = StringValidateUtil.checkAndReplaceIllegalWord(msg);
		ChatMessage chatmessage = new ChatMessage();
		chatmessage.roleId = roleId;
		chatmessage.rolename = rolename;
		chatmessage.msg = msg;
		chatmessage.time = GameTime.currentTimeMillis();
		chatmsgs.add(chatmessage);
		if(chatmsgs.size() > MAX_MSG_NUM)
			chatmsgs.remove(0);
		
		Set<Long> roleids = new HashSet<Long>(); 
		roleids.addAll(inchatroles.keySet());
		SSendChatMsg snd = new SSendChatMsg();
		snd.msgs.add(new ChatMsg(rolename, msg));
		gnet.link.Onlines.getInstance().send(roleids, snd);
	}
	
	private List<ChatMessage> getChatMsgs(int msgnum, long lastsend)
	{
		List<ChatMessage> msgs = new LinkedList<ChatMessage>();
		int count = 0;
		for(int i = chatmsgs.size() - 1; i>=0;i++)
		{
			ChatMessage msg = chatmsgs.get(i);
			if(msg.time <= lastsend)
				break;
			msgs.add(0,msg);
			count++;
			if(count >= msgs.size())
				break;
		}
		return msgs;
	}
}
