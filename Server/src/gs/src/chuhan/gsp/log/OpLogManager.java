package chuhan.gsp.log;

import java.util.Properties;
import java.util.concurrent.atomic.AtomicBoolean;

import chuhan.gsp.util.GameProp;

import com.pwrd.op.LogOp;
import com.pwrd.op.LogOpChannel;

import xdb.Procedure;
import xdb.Transaction;
import xdb.Procedure.Task;

/**
 * 接入到战略发展部后台的日志
 * 
 * @author RanBo
 *
 */
public class OpLogManager {
	private static Properties prop = chuhan.gsp.main.ConfigManager.getInstance().getPropConf("sys");
	private static AtomicBoolean isOpLogOn = new AtomicBoolean(GameProp.getIntValue(prop, "sys.oplog.initalOn") == 1);
	private static final OpLogManager instance = new OpLogManager();
	
	/**
	 * 就一个对象，不能多建
	 */
	private OpLogManager() {}
	
	public static OpLogManager getInstance() {
		return instance;
	}
	
	public static boolean isOn() {
		return isOpLogOn.get();
	}
	
	private void log(LogOpChannel logChannelMain, Object... params) {
		LogOp.log(logChannelMain, params);
	}
	
	private void log(String logChannelConfig, Object... params) {
		LogOp.log(logChannelConfig, params);
	}
	
	/**
	 * 不论什么情况都会记录的定制日志
	 * @param logChannelMain
	 * @param params
	 */
	public void doLog(LogOpChannel logChannelMain, Object... params) {
		if(!isOn()) {//开关没开
			return;
		}
		if(null == logChannelMain || params.length == 0) {//没有传参数还想记日志，NO
			return;
		}
		try {
			log(logChannelMain, params);
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * 不论什么情况都会记录的自定义日志
	 * @param logChannelMain
	 * @param params
	 */
	public void doLog(String logChannelConfig, Object... params) {
		if(!isOn()) {//开关没开
			return;
		}
		if(null == logChannelConfig 
				|| ("").equals(logChannelConfig) 
				|| params.length == 0) {//没有传参数还想记日志，NO
			return;
		}
		try {
			log(logChannelConfig, params);
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * 事物提交时才会记录的定制日志
	 * @param logChannelMain
	 * @param params
	 */
	public void doLogWhileCommit(LogOpChannel logChannelMain, Object... params) {
		if(!isOn()) {//开关没开
			return;
		}
		if(null == logChannelMain || params.length == 0) {//没有传参数还想记日志，NO
			return;
		}
		try {
			if(null != Transaction.current())
				Procedure.ppostWhileCommit(new LogWhileProcedureEnd(logChannelMain, params));
			else
				log(logChannelMain, params);
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * 事物提交时才会记录的自定义日志
	 * @param logChannelMain
	 * @param params
	 */
	public void doLogWhileCommit(String logChannelConfig, Object... params) {
		if(!isOn()) {//开关没开
			return;
		}
		if(null == logChannelConfig 
				|| ("").equals(logChannelConfig) 
				|| params.length == 0) {//没有传参数还想记日志，NO
			return;
		}
		try {
			if(null !=  Transaction.current())
				Procedure.ppostWhileCommit(new LogWhileProcedureEnd(logChannelConfig, params));
			else
				log(logChannelConfig, params);
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * 事物回滚时才会记录的定制日志
	 * @param logChannelMain
	 * @param params
	 */
	public void doLogWhileRollback(LogOpChannel logChannelMain, Object... params) {
		if(!isOn()) {//开关没开
			return;
		}
		if(null == logChannelMain || params.length == 0) {//没有传参数还想记日志，NO
			return;
		}
		try {
			if(null != Transaction.current())
				Procedure.ppostWhileRollback(new LogWhileProcedureEnd(logChannelMain, params));
			else
				log(logChannelMain, params);
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * 事物提交时才会记录的自定义日志
	 * @param logChannelMain
	 * @param params
	 */
	public void doLogWhileRollback(String logChannelConfig, Object... params) {
		if(!isOn()) {//开关没开
			return;
		}
		if(null == logChannelConfig 
				|| ("").equals(logChannelConfig) 
				|| params.length == 0) {//没有传参数还想记日志，NO
			return;
		}
		try {
			if(null !=  Transaction.current())
				Procedure.ppostWhileRollback(new LogWhileProcedureEnd(logChannelConfig, params));
			else
				log(logChannelConfig, params);
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * 一个存储过程执行完后执行run
	 * @author RanBo
	 *
	 */
	public class LogWhileProcedureEnd extends Task {
		private LogOpChannel logChannelMain;
		private String logChannelConfig;
		private Object[] params;
		
		public LogWhileProcedureEnd(LogOpChannel logChannelMain, Object... params) {
			this.logChannelMain = logChannelMain;
			this.params = params;
		}
		
		public LogWhileProcedureEnd(String logChannelConfig, Object... params) {
			this.logChannelConfig = logChannelConfig;
			this.params = params;
		}
		
		@Override
		public void run() {
			if(null !=  logChannelMain) {
				log(logChannelMain, params);
			} else if(null != logChannelConfig) {
				log(logChannelConfig, params);
			}
		}
		
	}
}
