package chuhan.gsp;


import java.util.HashMap;
import java.util.Map;

import org.apache.log4j.Logger;

import xdb.util.UniqName;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.config10;
import chuhan.gsp.game.sstarthero;
import chuhan.gsp.hero.PAddHero;
import chuhan.gsp.hero.PAddTroop;
import chuhan.gsp.hero.PSelectHero;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.log.OpLogManager;
import chuhan.gsp.log.RemoteLogID;
import chuhan.gsp.log.RemoteLogParam;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.util.CheckName;
import chuhan.gsp.util.DateUtil;
import chuhan.gsp.util.InetAddressUtil;

import com.pwrd.op.LogOpChannel;

public class PCreateRole extends xdb.Procedure {
	static private final Logger logger = Logger.getLogger(PCreateRole.class);
	
	// 创建角色时对应的客户端发送的协议
	private final xio.Protocol thisProtocol;
	
	// 创建角色需要的基本数据
	private final int userID;
	private final String name;
	private final int firsthero;
	
	public PCreateRole(CCreateRole protocol, int userID) {
		this.userID = userID;
		this.name = protocol.name;
		this.firsthero = protocol.firsthero;
		this.thisProtocol = protocol;
	}


	private boolean  sendError(int err){
		final SCreateRole res=new SCreateRole();
		res.error=err;
		return gnet.link.Onlines.getInstance().sendResponse(thisProtocol, res);
	}
	
	@Override
	public boolean process() {
		
		final SCreateRole snd = new SCreateRole();

		
		//检查用用户名长度是否合理
		if(name.length() > LangueVersion.getLangueVersion().getMaxNameLen()){
			sendError(SCreateRole.CREATE_OVERLEN);
			logger.debug("名字长度不对");
			return false;
		}
		else if (name.length() < LangueVersion.getLangueVersion().getMinNameLen()){
			sendError(SCreateRole.CREATE_SHORTLEN);
			logger.debug("名字长度不对");
			return false;
		}
		
		int resultCode = LangueVersion.getLangueVersion().checkName(name);
		if(resultCode == CheckName.WORD_ILLEGALITY){
			sendError(SCreateRole.CREATE_INVALID);
			logger.debug("只能输入2-8个字母、汉字、数字并且不能含有非法字符");
			return false;	
		}else if(resultCode == CheckName.SPECIAL_WORD_TOO_MANY){
			sendError(SCreateRole.CREATE_INVALID);
			logger.debug("特殊字符过多");
			return false;
		}else if(resultCode == CheckName.NONE_CHARACTER){
			sendError(SCreateRole.CREATE_INVALID);
			logger.debug("命名必须包含一个汉字或者字母");
			return false;
		}else if(resultCode == CheckName.WORD_ERROR_CHAR){
			sendError(SCreateRole.CREATE_INVALID);
			logger.debug("名称中有特殊符号");
			return false;
		}else if (resultCode == CheckName.WORD_SPACE) {
			sendError(SCreateRole.CREATE_INVALID);
			logger.debug("名称中有空格");
			return false;
		}
		
//		boolean b = UniqName.allocate("role", name.toLowerCase());
		// 检查角色名是否已经用过
		/*String lowerCaseName = name.toLowerCase();
		TODO 暂时不验重，策划说不需要 if (!UniqName.allocate("role", lowerCaseName)) {
			// 告诉客户端说角色名已重复
			if (name.length()==8) 
			sendError(SCreateRole.CREATE_DUPLICATED);
			else {
			  List<StringBuilder> result = NameRepository.addSymbols(new StringBuilder(name),4);
			  if (!result.isEmpty()) {
			  SRecommendsNames sRecommendsNames = new SRecommendsNames();
			  for (StringBuilder sb : result) {
				sRecommendsNames.recommendnames.add(Message.convertString2Octets(sb.toString()));
			  }
			  gnet.link.Onlines.getInstance().sendResponse(thisProtocol, sRecommendsNames);
			  }else {
				  sendError(SCreateRole.CREATE_DUPLICATED);
			 }
			}
			return false;
		}*/
		xbean.User u = xtable.User.get(userID);
		final long now = GameTime.currentTimeMillis();
		if (null == u) {
			u = xbean.Pod.newUser();
			u.setCreatetime(now);
			xtable.User.insert(userID, u);
		}
		if(!u.getIdlist().isEmpty()){
//			u.getIdlist().clear();
			return false;
		}
		
		final xbean.Properties pro = xbean.Pod.newProperties();
		
		pro.setCreatetime(now);
		pro.setOnlinetime(now);
		pro.setOfflinetime(now);
		pro.setRolename(name);
		pro.setUserid(userID);
		xbean.AUUserInfo auuser = xtable.Auuserinfo.select(userID);
		if(auuser != null)
		{
			pro.setUsername(auuser.getUsername());
			String[] strs = auuser.getNickname().split("#");
			pro.setPlattypestr(strs[0]);
			//pro.setPlattypestr(auuser.getNickname());
		}
		// 计算人物属性
		pro.setLevel(1);
		pro.setSignnum7(0);
		pro.setSignnum28(0);		
		pro.setSigntime(0);
		pro.setExp(0);
		pro.setViplv(1);
		pro.setTi(Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1003).configvalue));
		pro.setTichangetime(0);
		pro.setTanxianti(Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1015).configvalue));
		pro.setTanxiantitime(0);
		pro.setShenglingzq(0);
		pro.setJyjiejing(0);
		pro.setGold(Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1005).configvalue));
		pro.setYuanbao(Integer.parseInt(ConfigManager.getInstance().getConf(config10.class).get(1006).configvalue));
		pro.setBattlenum(0);
		pro.setJyjiejing(0);	
		pro.setShenglingzq(0); 			
		pro.setQiyuantime(now - DateUtil.dayMills);		//默认为昨天，防止第一次为断签
				
//		PSelectHero.onCreateRole(pro, now);
		
		final long newRoleID = xtable.Properties.insert(pro);

		u.getIdlist().add(newRoleID);

//		PAddHero pAddHero = new PAddHero(newRoleID, firsthero,1);
//		if(!pAddHero.call())
//			return false;
		
		//测试用，增加英雄
		addherotest(newRoleID);

		doLog(newRoleID);
		/*chuhan.gsp.item.Bag bag = new chuhan.gsp.item.Bag(newRoleID, false);
		bag.addItem(3000, 1, "create_add", 1,1);
		bag.addItem(3212, 1, "create_add", 1,1);*/
		logger.info("创建角色 :\t" + name + "\troleID:" + newRoleID
				+ "\tuserID:" + userID);
		snd.newinfo.roleid = newRoleID;
		snd.newinfo.rolename = name;
		snd.error = SCreateRole.CREATE_OK;
		gnet.link.Onlines.getInstance().sendResponse(thisProtocol, snd);//最后再发送创建成功的消息，以免失败后无法回滚
		return true;
	}

	private void addherotest(long roleid)
	{
//		PAddHero pAddHero = new PAddHero(roleid, 1,1);
//		pAddHero.call();
		//初始化编队数据
//		PAddTroop pAddTroop6 = new PAddTroop(roleid,1, 0,1,0);
//		pAddTroop6.call();
				
//		PAddTroop pAddTroop7 = new PAddTroop(roleid,1, 0,2,0);
//		pAddTroop7.call();
		
		try{
			int heroId = Integer.parseInt( ConfigManager.getInstance().getConf(config10.class).get(1328).configvalue );
			PAddHero pAddHero1 = new PAddHero(roleid, heroId,1);
			pAddHero1.call();
			

			PAddTroop pAddTroop3 = new PAddTroop(roleid,1,3);
			pAddTroop3.call();
		}catch(Exception e){
			e.printStackTrace();
			PAddHero pAddHero1 = new PAddHero(roleid, 1403100053,1);
			pAddHero1.call();
		}
		
		/*
		PAddHero pAddHero1 = new PAddHero(roleid, 1403100033,1);
		pAddHero1.call();
		PAddHero pAddHero2 = new PAddHero(roleid, 1403100175,1);
		pAddHero2.call();
		
		
		PAddHero pAddHero3 = new PAddHero(roleid, 1403100023,1);
		pAddHero3.call();
		
		PAddHero pAddHero4 = new PAddHero(roleid, 1403100134,1);
		pAddHero4.call();
	
		PAddHero pAddHero5 = new PAddHero(roleid, 1403100185,1);
		pAddHero5.call();
		
		PAddTroop pAddTroop = new PAddTroop(roleid,1, 1,0,1);
		pAddTroop.call();
		PAddTroop pAddTroop2 = new PAddTroop(roleid,1, 2,0,2);
		pAddTroop2.call();
		
		PAddTroop pAddTroop3 = new PAddTroop(roleid,1, 3,0,3);
		pAddTroop3.call();
		PAddTroop pAddTroop4 = new PAddTroop(roleid,1, 4,0,4);
		pAddTroop4.call();
	
		PAddTroop pAddTroop5 = new PAddTroop(roleid,1, 5,0,5);
		pAddTroop5.call();
		*/
		
	}
	
	private void doLog(long roleId)
	{
		try{
			PropRole propRole = PropRole.getPropRole(roleId, false);
			if(null != propRole) {
				OpLogManager.getInstance().doLogWhileCommit(LogOpChannel.REGISTER, roleId, name,
						GameTime.currentTimeMillis(), propRole.getProperties().getMac(),
						DateUtil.getCurrentStringFormatEn(GameTime.currentTimeMillis()),
						0, propRole.getProperties().getUsername(), propRole.getProperties().getPlattypestr(), null);		
			
				java.util.Map<String, Object> paras = new HashMap<String, Object>();
				paras.put(RemoteLogParam.FROM, ConfigManager.getGsZoneId());
				paras.put(RemoteLogParam.ACCOUNT, propRole.getProperties().getUsername());
				paras.put(RemoteLogParam.USERID, propRole.getProperties().getUserid());
				paras.put(RemoteLogParam.PLATFORM, propRole.getProperties().getPlattypestr());
				paras.put(RemoteLogParam.ROLEID, propRole.getRoleId());
				paras.put(RemoteLogParam.HINT, "0");//预留字段
				paras.put(RemoteLogParam.OS, propRole.getProperties().getOstype());
				xbean.AUUserInfo auuser = xtable.Auuserinfo.get(propRole.getProperties().getUserid());
				if(auuser != null)
				{
					String ipstr = InetAddressUtil.ipInt2String(auuser.getLoginip());
					paras.put(RemoteLogParam.IP, ipstr);
				}
				else
					paras.put(RemoteLogParam.IP, "127.0.0.1");
				LogManager.getInstance().doLogWhileCommit(RemoteLogID.CREATEROLE, paras);
			}
		}catch(Exception e)
		{
			e.printStackTrace();
		}
	}

	
}
