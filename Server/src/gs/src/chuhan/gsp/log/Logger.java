package chuhan.gsp.log;

import org.apache.log4j.Level;
import org.apache.log4j.Priority;

import xdb.Procedure;
import xdb.Transaction;
import xdb.Procedure.Task;

public class Logger
{
	
	public final org.apache.log4j.Logger logger4j;

	public static Logger getLogger(String name)
	{
		return new Logger(name);
	}
	
	public static Logger getLogger(Class<?> classname)
	{
		return new Logger(classname);
	}
	
	protected Logger(Class<?> classname)
	{
		logger4j = org.apache.log4j.Logger.getLogger(classname);
	}
	
	protected Logger(String name)
	{
		logger4j = org.apache.log4j.Logger.getLogger(name);
	}
	
	public boolean isDebugEnabled()
	{
		return logger4j.isDebugEnabled();
	}
	
	public Level getLevel()
	{
		return logger4j.getParent().getLevel();
	}
	
	public void debug(Object message)
	{
		if(getLevel().toInt() > Priority.DEBUG_INT)
			return;
		logger4j.debug(message);
	}
	
	public void debug(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.DEBUG_INT)
			return;
		logger4j.debug(message,t);
	}
	
	public void debugWhileCommit(Object message)
	{
		if(getLevel().toInt() > Priority.DEBUG_INT)
			return;
		debugWhileCommit(message,null);
	}

	public void debugWhileRollback(Object message)
	{
		if(getLevel().toInt() > Priority.DEBUG_INT)
			return;
		debugWhileRollback(message, null);
	}
	
	public void debugWhileCommit(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.DEBUG_INT)
			return;
		if(Transaction.current() !=null)
			Procedure.ppostWhileCommit(new LogWhileProcedureEnd(this, Priority.DEBUG_INT, message, t));
		else
			logger4j.debug(message);
	}

	public void debugWhileRollback(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.DEBUG_INT)
			return;
		if(Transaction.current() !=null)
			Procedure.ppostWhileRollback(new LogWhileProcedureEnd(this, Priority.DEBUG_INT, message, t));
		else
			logger4j.debug(message);
	}
	
	public void info(Object message)
	{
		if(getLevel().toInt() > Priority.INFO_INT)
			return;
		logger4j.info(message);
	}
	
	public void info(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.INFO_INT)
			return;
		logger4j.info(message,t);
	}
	
	public void infoWhileCommit(Object message)
	{
		if(getLevel().toInt() > Priority.INFO_INT)
			return;
		infoWhileCommit(message,null);
	}

	public void infoWhileRollback(Object message)
	{
		if(getLevel().toInt() > Priority.INFO_INT)
			return;
		infoWhileRollback(message, null);
	}
	
	public void infoWhileCommit(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.INFO_INT)
			return;
		if(Transaction.current() !=null)
			Procedure.ppostWhileCommit(new LogWhileProcedureEnd(this, Priority.INFO_INT, message, t));
		else
			logger4j.info(message);
	}

	public void infoWhileRollback(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.INFO_INT)
			return;
		if(Transaction.current() !=null)
			Procedure.ppostWhileRollback(new LogWhileProcedureEnd(this, Priority.INFO_INT, message, t));
		else
			logger4j.info(message);
	}
	
	public void error(Object message)
	{
		if(getLevel().toInt() > Priority.ERROR_INT)
			return;
		logger4j.error(message);
	}
	
	public void error(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.ERROR_INT)
			return;
		logger4j.error(message,t);
	}
	
	public void errorWhileCommit(Object message)
	{
		if(getLevel().toInt() > Priority.ERROR_INT)
			return;
		errorWhileCommit(message,null);
	}

	public void errorWhileRollback(Object message)
	{
		if(getLevel().toInt() > Priority.ERROR_INT)
			return;
		errorWhileRollback(message, null);
	}
	
	public void errorWhileCommit(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.ERROR_INT)
			return;
		if(Transaction.current() !=null)
			Procedure.ppostWhileCommit(new LogWhileProcedureEnd(this, Priority.ERROR_INT, message, t));
		else
			logger4j.error(message);
	}

	public void errorWhileRollback(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.ERROR_INT)
			return;
		if(Transaction.current() !=null)
			Procedure.ppostWhileRollback(new LogWhileProcedureEnd(this, Priority.ERROR_INT, message, t));
		else
			logger4j.error(message);
	}

	public void fatal(Object message)
	{
		if(getLevel().toInt() > Priority.FATAL_INT)
			return;
		logger4j.fatal(message);
	}
	
	public void fatal(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.FATAL_INT)
			return;
		logger4j.fatal(message,t);
	}
	
	public void fatalWhileCommit(Object message)
	{
		if(getLevel().toInt() > Priority.FATAL_INT)
			return;
		fatalWhileCommit(message,null);
	}

	public void fatalWhileRollback(Object message)
	{
		if(getLevel().toInt() > Priority.FATAL_INT)
			return;
		fatalWhileRollback(message, null);
	}
	
	public void fatalWhileCommit(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.FATAL_INT)
			return;
		if(Transaction.current() !=null)
			Procedure.ppostWhileCommit(new LogWhileProcedureEnd(this, Priority.FATAL_INT, message, t));
		else
			logger4j.fatal(message);
	}

	public void fatalWhileRollback(Object message,Throwable t)
	{
		if(getLevel().toInt() > Priority.FATAL_INT)
			return;
		if(Transaction.current() !=null)
			Procedure.ppostWhileRollback(new LogWhileProcedureEnd(this, Priority.FATAL_INT, message, t));
		else
			logger4j.fatal(message);
	}
	
	public static class LogWhileProcedureEnd extends Task {

		private final Logger logger;
		private final int level;
		private final Object message;
		private final Throwable t;
		
		public LogWhileProcedureEnd(Logger logger , int level , Object message,Throwable t) {
			this.logger = logger;
			this.level = level;
			this.message = message;
			this.t = t;
		}

		@Override
		public void run() {
			switch(level)
			{
			case org.apache.log4j.Priority.ALL_INT:
				break;
			case org.apache.log4j.Priority.DEBUG_INT:
				if(t == null)
					logger.logger4j.debug(message);
				else
					logger.logger4j.debug(message, t);
				break;
			case org.apache.log4j.Priority.ERROR_INT:
				if(t == null)
					logger.logger4j.error(message);
				else
					logger.logger4j.error(message, t);
				break;
			case org.apache.log4j.Priority.FATAL_INT:
				if(t == null)
					logger.logger4j.fatal(message);
				else
					logger.logger4j.fatal(message, t);
				break;
			case org.apache.log4j.Priority.INFO_INT:
				if(t == null)
					logger.logger4j.info(message);
				else
					logger.logger4j.info(message, t);
				break;
			case org.apache.log4j.Priority.OFF_INT:
				break;
			case org.apache.log4j.Priority.WARN_INT:
				if(t == null)
					logger.logger4j.warn(message);
				else
					logger.logger4j.warn(message, t);
				break;
			}
		}
	}	
}
