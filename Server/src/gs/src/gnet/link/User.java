package gnet.link;

import java.util.concurrent.atomic.AtomicReference;


import chuhan.gsp.SRoleList;

import com.goldhuman.Common.Marshal.OctetsStream;

/**
 * 用户与网络之间的对应
 * 
 * @author LiuChen
 *
 */
public class User
{
	private final int userid;
	private final AtomicReference<Link.Session> linkSession = new AtomicReference<Link.Session>();
	private int state = 0;//
	private boolean linkBroken = false;
	private String mac;
	
	public static int STATE_FAILED = 0;//登入失败，直接返回提示
	public static int STATE_LOGIN = 1;//登入状态
	public static int STATE_IN_QUEUE = 2;//排队状态
	public static int STATE_ZERO_QUEUE = 3;//0排位状态

	
	public User(int userid)
	{
		this.userid = userid;
	}
	public int getUserid() {
		return userid;
	}

	public String getMac() {
		return mac;
	}
	public void setMac(String mac) {
		this.mac = mac;
	}
	public Link.Session getLinkSession() {
		return linkSession.get();
	}
	
	public int getState()
	{
		return state;
	}
	public void setState(int state)
	{
		this.state = state;
	}
	
	public boolean isLinkBroken()
	{
		return linkBroken;
	}
	public void setLinkBroken(boolean linkBroken)
	{
		this.linkBroken = linkBroken;
	}
	void linkAttach(Link link, int linksid) {
		linkSession.set(link.attach(linksid, this));
		this.setLogin(SetLogin.eLogin);
	}
	public boolean kick(int error) {
		Link.Session ls = linkSession.get();
		if (null != ls) {
			Kick p1 = new Kick();
			p1.linksid = ls.getSid();
			p1.action = Kick.A_QUICK_CLOSE;
			p1.error = error;
			return p1.send(ls.getXio());
		}
		return false;
	}
	

	/**
	 * 
	 * 设置角色在glinkd上的状态。只有状态设置为 SetLogin::eLogin 的玩家可以接收到广播消息。
	 * 
	 * [注意] 并不是所有的 Provider 都需要管理设置这个状态。
	 * 这个状态一般是主管玩家登陆逻辑的 Provider（如gsd）来管理。
	 * 为了简化外面使用，目前java版本的实现自动调用了setLogin。
	 * 当使用 java 实现不管理玩家登陆逻辑的 provider 服务时，
	 * 请删除 gnet.link 中的 setLogin 调用。
	 * 
	 * @param login
	 * @return
	 */
	boolean setLogin(int login) {
		Link.Session ls = linkSession.get();
		if (null != ls) {
			return new SetLogin(ls.getSid(), login, -1).send(ls.getXio());
		}
		return false;
	}
	
	public boolean send(xio.Protocol p2) {
		final Link.Session ls = linkSession.get();
		if (null != ls) {
			final Send p1 = new Send();
			p1.linksids.add(ls.getSid());
			p1.ptype = p2.getType();
			p1.pdata = new OctetsStream().marshal(p2);
			ls.getXio().getCreator().getManager().getCoder().checkSend(p2, p1.pdata.size());
			Onlines.sendSend(p1, ls.getXio());
			//p1.send(ls.getXio());
			return true;
		}
		return false;
	}
	
	public void sendRoleList()
	{
		if(xdb.Transaction.current()==null)
			new chuhan.gsp.PSendRoleList(this).submit();
		else
			xdb.Procedure.pexecuteWhileCommit(new chuhan.gsp.PSendRoleList(this));
	}
	
	
}
