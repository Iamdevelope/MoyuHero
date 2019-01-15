package chuhan.gsp.util;

import java.util.Map;

import chuhan.gsp.attr.PropRole;
import chuhan.gsp.exchange.ChargeRole;
import chuhan.gsp.log.Logger;
import chuhan.gsp.log.RemoteLogParam;
import chuhan.gsp.main.ConfigManager;

public class LogUtil {
	
	/**
	 * putRoleBasicParams:role的基本属性参数,包括from,userid
	 * 
	 * @param roleid
	 * @param paras
	 *            void
	 * @throws
	 * @since 　
	 */
	public static Map<String, Object> putRoleBasicParams(long roleId, boolean readonly,
			Map<String, Object> paras) {
		return putRoleBasicParams(PropRole.getPropRole(roleId, readonly), paras);
	}
	/**
	 * putRoleBasicParams:role的基本属性参数,包括from,userid
	 * 
	 * @param PropRole propRole
	 * @param paras
	 *            void
	 * @throws
	 * @since 　
	 */
	public static Map<String, Object> putRoleBasicParams(PropRole propRole,
			Map<String, Object> paras) {
		if(propRole == null)
			return paras;
		paras.put(RemoteLogParam.FROM, ConfigManager.getGsZoneId());
		paras.put(RemoteLogParam.ACCOUNT, new String(EncodeBase64.transform(propRole.getProperties().getUsername().getBytes())));
		paras.put(RemoteLogParam.USERID, propRole.getProperties().getUserid());
		paras.put(RemoteLogParam.MAC, propRole.getProperties().getMac());
		paras.put(RemoteLogParam.OS, propRole.getProperties().getOstype());
		paras.put(RemoteLogParam.PLATFORM, propRole.getProperties().getPlattypestr());
		paras.put(RemoteLogParam.ROLEID, propRole.getRoleId());
		paras.put(RemoteLogParam.LEV, propRole.getLevel());
		ChargeRole crole = ChargeRole.getChargeRole(propRole.getRoleId(), propRole.readonly);
		int totalcash = (int)crole.getChargedSum();
		paras.put(RemoteLogParam.TOTALCASH, totalcash);
		paras.put(RemoteLogParam.HINT, "0");//预留字段，外部可以自由顶掉
		return paras;
	}
	/**
	 * 人物基础数据拼接成string
	 * @param propRole
	 * @return
	 */
	public static String getRoleBasicStr(PropRole propRole) {
		if(propRole == null)
			return null;
		StringBuffer strBuffer = new StringBuffer();
		strBuffer.append(RemoteLogParam.FROM).append(":").append(ConfigManager.getGsZoneId()).append(" ");
		strBuffer.append(RemoteLogParam.ACCOUNT).append(":").append(propRole.getProperties().getUsername()).append(" ");
		strBuffer.append(RemoteLogParam.USERID).append(":").append(propRole.getProperties().getUserid()).append(" ");
		strBuffer.append(RemoteLogParam.MAC).append(":").append(propRole.getProperties().getMac()).append(" ");
		strBuffer.append(RemoteLogParam.PLATFORM).append(":").append(propRole.getProperties().getPlattypestr()).append(" ");
		strBuffer.append(RemoteLogParam.ROLEID).append(":").append(propRole.getRoleId()).append(" ");
		strBuffer.append(RemoteLogParam.LEV).append(":").append(propRole.getLevel()).append(" ");
		ChargeRole crole = ChargeRole.getChargeRole(propRole.getRoleId(), propRole.readonly);
		int totalcash = (int)crole.getChargedSum();
		strBuffer.append(RemoteLogParam.TOTALCASH).append(":").append(totalcash).append(" ");
		return strBuffer.toString();
	}
	/**
	 * 人物基础数据拼接成string
	 * @param roleId
	 * @return
	 */
	public static String getRoleBasicStr(long roleId) {
		PropRole propRole = PropRole.getPropRole(roleId, true);
		if(propRole == null)
			return null;
		return getRoleBasicStr(propRole);
	}
	
	/**
	 * 记录玩家操作日志使用
	 * @param logger
	 * @param propRole
	 * @param otherStr
	 */
	public static void logInfoWhileCommit(String logType,Logger logger,PropRole propRole,String...otherStr){
		StringBuffer strBuffer = new StringBuffer();
		strBuffer.append(logType).append(" ");
		strBuffer.append(LogUtil.getRoleBasicStr(propRole)).append(" ");
		if(otherStr != null){
			for(String str : otherStr){
				strBuffer.append(str).append("--");
			}
		}
		logger.infoWhileCommit(strBuffer.toString());
	}
	
	public static void logInfoWhileCommit(String logType,Logger logger,long roleId,String...otherStr){
		PropRole propRole = PropRole.getPropRole(roleId, true);
		if(propRole == null)
			return;
		logInfoWhileCommit(logType,logger,propRole,otherStr);
	}
}
