package chuhan.gsp.msg;

import java.io.UnsupportedEncodingException;
import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Observable;
import java.util.Set;
import java.util.concurrent.TimeUnit;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.message.SMessage;

import com.goldhuman.Common.Octets;

/**
 * 服务器向客户发送的频道消息
 * @author DevUser
 *
 */
public class Message extends Observable {
	
	public static int EMPTY_MSG_ID = 126;
	
	private static Message instance=new Message();
	private Message(){
	}
	public static Message getInstance(){
		return instance;
	}
	
	/**
	 * 向roleId角色推送消息提示
	 * @param roleId 推送对象
	 * @param msgId 消息Id
	 * @param parameters 顺序放置的字符参数,可以为null
	 */
	public static void sendPopMsg(long roleId,String text)
	{
		sendMsgNotify(roleId, EMPTY_MSG_ID, text);
	}
	
	/**
	 * 向roleId角色推送消息提示
	 * @param roleId 推送对象
	 * @param msgId 消息Id
	 * @param parameters 顺序放置的字符参数,可以为null
	 */
	public static void sendMsgNotify(long roleId,int msgId, Object... parameters)
	{
		gnet.link.Onlines.getInstance().send(roleId, getMsgNotify(msgId, parameters));
	}
	public static void psendMsgNotify(long roleId,int msgId, Object... parameters)
	{
		xdb.Procedure.psend(roleId, getMsgNotify(msgId, parameters));
	}
	public static void psendMsgNotifyWhileCommit(long roleId,int msgId, Object... parameters)
	{
		xdb.Procedure.psendWhileCommit(roleId, getMsgNotify(msgId, parameters));
	}
	
	/**
	 * 向roleIds角色推送消息提示,当推送npc消息时使用
	 * @param roleId 推送对象
	 * @param msgId 消息Id
s	 * @param parameters 顺序放置的字符参数,可以为null
	 */
	public static void sendMsgNotify(Collection<Long> roleids, int msgId, Object... parameters) {
		Set<Long> roleset = new HashSet<Long>();
		roleset.addAll(roleids);
		gnet.link.Onlines.getInstance().send(roleset, getMsgNotify(msgId,  parameters));
	}
	public static void psendMsgNotifyWhileCommit(Collection<Long> roleIds,int msgId, Object... parameters)
	{
		xdb.Procedure.psendWhileCommit(roleIds, getMsgNotify(msgId, parameters));
	}
	public static void psendMsgNotify(Collection<Long> roleids, int msgId, Object... parameters) {
		xdb.Procedure.psend(roleids, getMsgNotify(msgId,  parameters));
	}
	
	/**
	 * 向在线角色广播消息
	 * @param msgId 消息Id
	 * @param parameters 顺序放置的字符参数,可以为null
	 */
	public static void broadcastMsgNotify(int msgId, String... parameters)
	{
		gnet.link.Onlines.getInstance().broadcast(getMsgNotify(msgId, parameters));
	}
	public static void pbroadcastMsgNotify(int msgId, String... parameters)
	{
		xdb.Procedure.pbroadcast(getMsgNotify(msgId, parameters),999);
	}
	public static void pbroadcastMsgNotifyWhileCommit(int msgId, String... parameters)
	{
		xdb.Procedure.pbroadcastWhileCommit(getMsgNotify(msgId, parameters),999);
	}
	public static void broadcastMsgNotifyWithDelay(long delay, int msgId, String... parameters)
	{
		xdb.Executor.getInstance().schedule(new DelayBroadCastTask(getMsgNotify(msgId, parameters)), delay, TimeUnit.MILLISECONDS);
	}
	/**
	 * 向玩家发送一条系统消息，系统消息先保存，只发送个数，客户端请求时再发送
	 * @param roleId
	 * @param msgId
	 * @param parameters
	 */
	public static void sendSystemMsg(long roleId, int msgId, String... parameters)
	{
		List<String> params = new LinkedList<String>();
		for(String str : parameters)
			params.add(str);
		new PAddSysMsg(roleId, msgId, params, null).submit();
	}
	public static void psendSystemMsg(long roleId, int msgId, String... parameters)
	{
		List<String> params = new LinkedList<String>();
		for(String str : parameters)
			params.add(str);
		xdb.Procedure.pexecute(new PAddSysMsg(roleId, msgId, params, null));
	}
	
	public static void psendSystemMsgWhileCommit(long roleId, int msgId, String... parameters)
	{
		List<String> params = new LinkedList<String>();
		for(String str : parameters)
			params.add(str);
		xdb.Procedure.pexecuteWhileCommit(new PAddSysMsg(roleId, msgId, params, null));
	}
	
	/**
	 * 向在线玩家发送广播系统消息，系统消息先保存，只发送个数，客户端请求时再发送
	 * @param roleId
	 * @param msgId
	 * @param parameters
	 */
	public static void broadcastSystemMsg(int msgId, String... parameters)
	{
		List<String> params = new LinkedList<String>();
		for(String str : parameters)
			params.add(str);
		for(gnet.link.Role linkrole: gnet.link.Onlines.getInstance().getRoles())
		{
			new PAddSysMsg(linkrole.getRoleid(), msgId, params, null).submit();
		}
	}
	public static void pbroadcastSystemMsg(int msgId, String... parameters)
	{
		List<String> params = new LinkedList<String>();
		for(String str : parameters)
			params.add(str);
		for(gnet.link.Role linkrole: gnet.link.Onlines.getInstance().getRoles())
		{
			xdb.Procedure.pexecute(new PAddSysMsg(linkrole.getRoleid(), msgId, params, null));
		}
	}
	public static void pbroadcastSystemMsgWhileCommit(int msgId, String... parameters)
	{
		List<String> params = new LinkedList<String>();
		for(String str : parameters)
			params.add(str);
		for(gnet.link.Role linkrole: gnet.link.Onlines.getInstance().getRoles())
		{
			xdb.Procedure.pexecuteWhileCommit(new PAddSysMsg(linkrole.getRoleid(), msgId, params, null));
		}
	}
	
	/**
	 * 构造SSendMsgNotify消息，自己发
	 * @param roleId
	 * @param msgId
	 * @param parameters,可以为null
	 * @return SSendMsgNotify
	 */
	public static SSendMsgNotify getMsgNotify(int msgId, Object[] parameters)
	{
		java.util.ArrayList<com.goldhuman.Common.Octets> octetsparas = new java.util.ArrayList<com.goldhuman.Common.Octets>();
		if(parameters!=null)
		{
			for (Object parameter : parameters)
			{
				octetsparas.add(convertString2Octets(parameter.toString()));
			}
		}
		return new SSendMsgNotify(msgId, octetsparas);
	}
	
	public static com.goldhuman.Common.Octets convertString2Octets(String str)
	{
		try
		{
			return new Octets(str.getBytes("UTF-16LE"));
		} catch (UnsupportedEncodingException e)
		{
			e.printStackTrace();// 吞掉这个exception，因为"UTF-16LE"不能写错
		}
		return null;
	}
	
	public static com.goldhuman.Common.Octets convertString2LogOctets(String str){
		try
		{
			return new Octets(str.getBytes("UTF-8"));
		} catch (UnsupportedEncodingException e)
		{
			e.printStackTrace();// 吞掉这个exception，因为"UTF-16LE"不能写错
		}
		return null;
	}
	
	public static String getMessage(int id) {
		return ConfigManager.getInstance().getConf(SMessage.class).get(id).getMsg();
	}
	
}
