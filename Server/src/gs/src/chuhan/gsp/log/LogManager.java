package chuhan.gsp.log;

import java.util.Map;
import java.util.concurrent.locks.ReentrantLock;

import chuhan.RemoteLog;

import org.apache.log4j.Logger;

import xdb.Procedure;
import xdb.Procedure.Task;
import xdb.Transaction;
import xio.Manager;
import xio.Xio;

import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.msg.MsgRole;
import chuhan.gsp.msg.PAddSysMsg;
import chuhan.gsp.util.OctetsUtil;

import com.goldhuman.Common.Octets;

/**
 * 这个仅仅是用来给运营记录日志用的。调试用的日志不需要用这个纪录。请用Log4j
 * 
 * @author CM && ZC
 *
 */
public class LogManager extends Manager {
	public static Logger logger = Logger.getLogger(LogManager.class);

	public static Long showLogGMRoleid = null;
	public static boolean isShowLog = false;
	
	private static LogManager instance = new LogManager();

	public LogManager() {
		synchronized (LogManager.class) {
			instance = this;
		}
	}

	public static LogManager getInstance() {
		synchronized (LogManager.class) {
			return instance;
		}
	}
	
	private xio.Xio handle = null;
	private final ReentrantLock lock = new ReentrantLock();

	@Override
	protected void addXio(Xio xio) {
		lock.lock();
		try {
			// TODO: if handle!=null ??
			handle = xio;
			logger.info("LogServer connected");
		} finally {
			lock.unlock();
		}
	}

	@Override
	public Xio get() {
		lock.lock();
		try {
			return handle;
		} finally {
			lock.unlock();
		}
	}

	@Override
	protected void removeXio(Xio xio, Throwable e) {
		lock.lock();
		try {
			handle = null;
		} finally {
			lock.unlock();
		}

	}

	@Override
	public int size() {
		lock.lock();
		try {
			return handle == null ? 0 : 1;
		} finally {
			lock.unlock();
		}
	}

	/**
	 * 发送RemoteLog消息，打印运营Log
	 * 
	 * @param priority log的属性值，在LogPriority定义一组宏
	 * @param msg 消息的文本
	 */
	private void log(int priority, String msg) {
		//HostName和ServiceName暂时发送空字符串
		final String hostname="";
		final String servicename2="";
		logger.debug(msg);
		Octets msgOctets = OctetsUtil.toLogOctets(msg);
		Octets hostOctets = OctetsUtil.toLogOctets(hostname);
		Octets serviceOctets = OctetsUtil.toLogOctets(servicename2);
		
		final RemoteLog log = new RemoteLog(priority, msgOctets, hostOctets, serviceOctets);
		
		lock.lock();
		try {
			if (handle != null){
				try{
					log.send(handle);
				}catch (Throwable error) {
					logger.error("打印远程log出错：  ", error);
					logger.error("出错的远程log id : " + priority + "内容：  " + msg);
				}
				
			}
				
		} finally {
			lock.unlock();
		}
	}
	
	/**
	 * 正常打印
	 * 
	 * @param priority
	 * @param msg
	 */
	public void doLog(int logID, Map<String, Object> param){
		int priority = getPriorityByLogID(logID);
		if(-1 == priority)
			return;
		
		String msg = convertFormatLog(logID, param);
		
		showLog(msg, param);

		if(null == msg)
			return;
		
		log(priority, msg);
	}
	
	public static String convertFormatLog(int logId,
			Map<String, Object> paramValue) {
		chuhan.gsp.log.SLogFormatConfig config = ConfigManager.getInstance().getConf(chuhan.gsp.log.SLogFormatConfig.class).get(logId);
		if (null == config) {
			LogManager.logger.error("LOG ID为： " + logId + " 的log格式配置找不到！！！");
			return null;
		}

		String logFormat = config.getFormat();
		boolean isError = false;
		String errorParam = null;
		int startIndex = 0;
		String result = new String(logFormat);
		while (!isError) {
			int index = result.indexOf("$", startIndex);
			if (index == -1)
				break;

			int nexIndex = result.indexOf("$", index + 1);
			if (nexIndex == -1)
				break;

			/*
			 * String param = result.substring(index + 1,
			 * nexIndex).toUpperCase();
			 * 
			 * Integer paramID = Module.getParamHashCodeByName(param); if(null
			 * == paramID){ startIndex = nexIndex + 1; continue;
			 * 
			 * }
			 */
			String param = result.substring(index + 1, nexIndex);
			Object value = paramValue.get(param);
			if (value != null) {
				result = result.replace("$" + param.toLowerCase() + "$",
						value.toString());
				nexIndex += (value.toString().length() - param.length());// 这里必须要把nexindex变化，因为更换的字符串长度与原来不一致
			} else {
				errorParam = param;
				isError = true;
			}

			startIndex = nexIndex + 1;
		}

		if (isError) {
			LogManager.logger.error("发送LOG ID为： " + logId
					+ " 有错，调用的参数错误！！！Miss parameter:" + errorParam);
			return null;
		}

		return result;
	}
	
	private void showLog(String msg, Map<String, Object> param) {
		if(!isShowLog || showLogGMRoleid == null)
			return;
		
		Long roleid = (Long) param.get(RemoteLogParam.ROLEID);
		if(roleid != null && roleid.longValue() != showLogGMRoleid)
			return;
		PAddSysMsg p = new PAddSysMsg(showLogGMRoleid, 0, null, msg);
		if(xdb.Transaction.current() == null)
			p.submit();
		else
			xdb.Procedure.pexecute(p);
	}

	/**
	 * 存储过程中调用，在提交之后打印
	 * 
	 * @param priority
	 * @param msg
	 */
	public void doLogWhileCommit(int logID, Map<String, Object> param){
		int priority = getPriorityByLogID(logID);
		if(-1 == priority)
			return;
		
		String msg = convertFormatLog(logID, param);
		if(null == msg)
			return;
		
		showLog(msg, param);
		
		if(Transaction.current() !=null)
			Procedure.ppostWhileCommit(new LogWhileProcedureEnd(msg, priority));
		else
			log(priority, msg);
	}
	
	/**
	 * 存储过程中调用，回滚时候打印
	 * 
	 * @param priority
	 * @param msg
	 */
	public void doLogWhileRollback(int logID, Map<String, Object> param){
		int priority = getPriorityByLogID(logID);
		if(-1 == priority)
			return;
		
		String msg = convertFormatLog(logID, param);
		if(null == msg)
			return;
		
		showLog(msg, param);
		
		if(Transaction.current() !=null)
			Procedure.ppostWhileRollback(new LogWhileProcedureEnd(msg, priority));
		else
			log(priority, msg);
	}
	
	public class LogWhileProcedureEnd extends Task {

		private String message;
		
		private int priority;
		
		public LogWhileProcedureEnd(String message, int priority) {
			super();
			this.message = message;
			this.priority = priority;
		}
		
		@Override
		public void run() {
			log(priority, message);
		}
		
	}
	public static int getPriorityByLogID(int logID) {
		chuhan.gsp.log.SLogFormatConfig config = ConfigManager.getInstance().getConf(chuhan.gsp.log.SLogFormatConfig.class).get(logID);
		if (null == config) {
			LogManager.logger.error("LOG ID为： " + logID + " 的log格式配置找不到！！！");
			return -1;
		}

		return config.getType();
	}
}