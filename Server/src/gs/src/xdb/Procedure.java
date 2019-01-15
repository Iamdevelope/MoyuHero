package xdb;

import java.util.concurrent.*;
import java.util.*;

import chuhan.gsp.hotdeploy.Module;

public class Procedure {

	public class Locks {
		private List<Lockey> locks = new ArrayList<Lockey>();

		public Locks() {
		}

		public final Locks add(Lockey lockey) {
			locks.add(lockey);
			return this;
		}

		public final Locks add(TTable<?, ?> table, Object key) {
			locks.add(Lockeys.get(table, key));
			return this;
		}

		public final Locks add(TTable<?, ?> table, Collection<Object> keys) {
			for (Lockey lockey : Lockeys.get(table, keys))
				locks.add(lockey);
			return this;
		}

		public final Locks add(TTable<?, ?> table, Object ... keys) {
			for (Lockey lockey : Lockeys.get(table, keys))
				locks.add(lockey);
			return this;
		}

		/**
		 * @see Procedure.lock
		 */
		public final void lock() {
			Procedure.this.lock(locks.toArray(new Lockey[locks.size()]));
		}
	}

	protected final void begin() {
		Transaction.current().begin();
	}

	protected final int beginAndSavepoint() {
		Transaction transaction = Transaction.current();
		transaction.begin();
		return transaction.savepoint();
	}

	protected final void commit() {
		Transaction.current().commit();
	}

	protected final int savepoint() {
		return Transaction.current().savepoint();
	}

	protected final void rollback(int savepoint) {
		Transaction.current().rollback(savepoint);
	}

	protected final int trancount() {
		return Transaction.current().trancount();
	}
	
	protected final void rollbackAll() {
		throw new XError("rollbackAll");
	}

	/**
	 * @deprecated
	 *   new sample : lock(xdb.Lockeys.get(xtable.Locks.ROLELOCK, keys))	
	 */
	protected void lock(String name, Object[] keys) {
		final int index = Lockeys.getInstance().getLockId(name);
		Lockey [] lockeys = new Lockey[keys.length];
		int i = 0;
		for (Object key : keys)
			lockeys[i++] = Lockeys.get(name, index, key);
		lock(lockeys);
	}

	/**
	 * @deprecated
	 *   new sample : lock(xdb.Lockeys.get(xtable.Locks.ROLELOCK, keys))	
	 */
	protected void lock(String name, Collection<?> keys) {
		final int index = Lockeys.getInstance().getLockId(name);
		Lockey [] lockeys = new Lockey[keys.size()];
		int i = 0;
		for (Object key : keys)
			lockeys[i++] = Lockeys.get(name, index, key);
		lock(lockeys);
	}

	protected void lock(TTable<?, ?> table, Collection<?> keys) {
		Lockeys.lock(table, keys);
	}

	protected void lock(Lockey[] lockeys) {
		Lockeys.lock(lockeys);
	}

	public boolean call() {
		if (Transaction.current() == null) {
			try {
				Transaction.create().perform(this);
			} catch (Throwable e) {
			} finally {
				Transaction.destroy();
				this.fetchTasks();
			}
			return this.isSuccess();
		}

		int savepoint = beginAndSavepoint();

		// ��׽�����쳣���ڷ����쳣��process����falseʱ���ع�����̿�ʼ�ı���㡣
		// ����׽�������еĴ����׵���㡣
		try {
	        if (Module.hasNewClass()){//if new classes are loaded
	        	Procedure hotdeployProc = Module.getHotdeployProcedure(this.getClass().getName(), this);
	        	   if (hotdeployProc == null) {//if this procedure has no new version
	        		   if (process()) {
	       				commit();
	       				this.setSuccess(true);
	       				return true;
	       			  }
	        	   }else if(hotdeployProc.process()){
	                   commit();
	                   this.setSuccess(true);
	                   return true;
	               }
	        }
	        else if (process()) {
				commit();
				this.setSuccess(true);
				return true;
			}
		} catch (Exception ex) {
			this.setException(ex);
			Trace.log(this.getConf().getTrace(), "Procedure execute", ex);
		}

		rollback(savepoint);
		return false;
	}

	/**
	 * ���ء�д����ĵط���
	 * <pre>
	 * �쳣�����?
	 *    catch (Exception) ����� catch ֻ����־���Ͳ�Ҫ catch���ͷ���Դ��ʹ�� finally��
	 *    DO NOT catch(Error) 
	 *    DO NOT catch(Throwable)
	 * </pre>
	 * @return
	 *     true  ��ݴ���ɹ����ύ�޸ģ�* Ƕ�׵��á�
	 *     false ��ݴ���ʧ�ܣ���ǰ������е��޸ı�����ع�����̿�ʼʱ�ı���㡣
	 */
	protected boolean process() throws Exception {
		return false;
	}

	/////////////////////////////////////////////////////////
	// ���ýӿ�
	private static void verify() {
		if (Transaction.current() != null)
			throw new IllegalStateException("can not submit in transaction.");
	}

	/**
	 * �����洢���ִ�������ύ���̳߳��С�
	 * @return Future ���ڵȴ�洢���ִ����ɡ�
	 * @throws IllegalStateException �����������ִ����ʹ�á�
	 */
	public final Future<Procedure> submit() {
		verify();
		return new ProcedureFuture<Procedure>(this);
	}

	/**
	 * �����洢���ִ�������ύ���̳߳��С�
	 * 
	 * @param p �ύִ�еĴ洢��̡�
	 * @return Future ���ڵȴ�洢���ִ����ɡ�
	 * @throws IllegalStateException �����������ִ����ʹ�á�
	 */
	public static <P extends Procedure> Future<P> submit(P p) {
		verify();
		return new ProcedureFuture<P>(p);
	}

	/**
	 * �洢���ִ����ɻص��ӿڡ�
	 * �����첽�ȴ���ִ����ɡ�
	 * 
	 * @param <P> �ص���ʱ�����洢�������
	 */
	public static interface Done<P extends Procedure> {
		public void doDone(P p);
	}

	/**
	 * �����洢���ִ�������ύ���̳߳��С�
	 * 
	 * @param p �ύִ�еĴ洢��̡� 
	 * @param done �ص��ӿڡ�
	 */
	public static <P extends Procedure> void execute(P p, Done<P> done) {
		new ProcedureFuture<P>(p, done);
	}

	/**
	 * �����洢���ִ�������ύ���̳߳��С�
	 * 
	 * @param p �ύִ�еĴ洢��̡�
	 */
	public static void execute(Procedure p) {
		new ProcedureFuture<Procedure>(p, null);
	}

	/**
	 * �ύ���̳߳��С�
	 */
	public void execute() {
		new ProcedureFuture<Procedure>(this, null);
	}

	/////////////////////////////////////////////////////////////////////////////
	// �ڲ�״̬������.��ݱ����Ҫ���
	private volatile ProcedureConf conf;

	// execute ���
	private volatile boolean success = false;
	private volatile Throwable exception;

	public Procedure() {
	}

	public Procedure(ProcedureConf conf) {
		this.conf = conf;
	}

	public final ProcedureConf getConf() {
		if (null != conf)
			return conf;
		// default
		return Xdb.getInstance().getConf().getProcedureConf();
	}

	public final synchronized void setConf(ProcedureConf conf) {
		this.conf = conf;
	}

	public final boolean isSuccess() {
		return success;
	}

	public final void setSuccess(boolean success) {
		this.success = success;
	}

	public final Throwable getException() {
		return exception;
	}

	// setup by execute OR Transaction.perform
	public final void setException(Throwable exception) {
		this.exception = exception;
	}

	///////////////////////////////////////////////////////////////////////
	// �ռ����񣬲����������ʱִ�С�

	/**
	 * ���� run ��ʵ�������?
	 */
	public abstract static class Task implements Runnable, Log {
		private boolean actived = true;
		private boolean expected = true;

		public final void setActived(boolean actived) {
			this.actived = actived;
		}

		public final void setExpected(boolean expected) {
			this.expected = expected;
		}

		/**
		 * ����ִ����ڣ��ݲ��������ء�
		 */
		public final void process() {
			if (this.actived)
				this.run();
		}

		@Override
		public void commit() {
			this.setActived(this.expected);
		}

		@Override
		public void rollback() {
			this.setActived(!this.expected);
		}
	}

	/**
	 * һ����˵��һ���洢������治���ύ̫����������ʹ��  ArrayList��
	 */
	private static ThreadLocal<ArrayList<Task>> ptasks = new ThreadLocal<ArrayList<Task>>() {
		protected java.util.ArrayList<Task> initialValue() {
			return new ArrayList<Task>();
		}
	};

	private ArrayList<Task> tasks = null;

	/**
	 * ��������б��У����������ʱִ�У��������ִ�еĽ��
	 * @param task
	 */
	public static void ppost(Task task) {
		ptasks.get().add(task);
	}

	/**
	 * �Ѵ洢���ִ���ڼ��ռ�����������ֲ߳̾��洢������ȡ�����������浽�洢��̳�Ա�����С�
	 */
	void fetchTasks() {
		this.tasks = ptasks.get();
		ptasks.remove();
	}

	/**
	 * ���ش洢������һ��ִ�к��ռ���������
	 * ���洢���û��ִ�й�ʱ������null��
	 */
	public ArrayList<Task> getLastTasks() {
		return this.tasks;
	}

	/**
	 * ִ�д洢������һ��ִ�к��ռ���������
	 * ����ִ��ʧ��ʱ�����ж����������ִ�С�
	 * ����ִ�д����¼�� Trace ��־�С�
	 */
	public void runLastTasks() {
		if (null != this.tasks)
			for (Task task : this.tasks) {
				try {
					task.process();
				} catch (Throwable e) {
					// ��Щ�������ִ��ʧ�ܣ�����¼��־��
					// �������޷�����ദ�?
					xdb.Trace.error("Procedure.runTasks", e);
				}
			}
	}

	public static void ppostWhileCommit(Task task) {
		task.setExpected(true);
		Transaction.currentSavepoint().add(new LogKey(new XBean(null, null), ""), task);
		ppost(task);
	}

	public static void ppostWhileRollback(Task task) {
		task.setExpected(false);
		Transaction.currentSavepoint().add(new LogKey(new XBean(null, null), ""), task);
		ppost(task);
	}

	/**
	 * @see gnet.link.Onlines
	 * 
	 * ���м��������е���
	 */
	public static interface IOnlines {
		public boolean sendResponse(xio.Protocol THIS, xio.Protocol p2);
		public boolean send(Long roleid, xio.Protocol p2);
		public boolean send(java.util.Set<Long> roleids, xio.Protocol p2);
		public void broadcast(xio.Protocol p2, int timems);
	}

	private static volatile IOnlines onlines = null;

	protected static IOnlines getOlines() {
		return onlines;
	}

	public static void setOlines(IOnlines onlines) {
		Procedure.onlines= onlines;
	}
	//////////////////////////////////////////////////////////////////////////////
	// ���·����ύ�������ڴ洢��̽���ʱ��ִ�С�

	public static void psend(long roleid, xio.Protocol p) {
		ppost(new SendToRole(roleid, p));
	}

	public static void psend(long roleid1, long roleid2, xio.Protocol p) {
		ppost(new SendToRoles(roleid1, roleid2, p));
	}

	public static void psend(java.util.Collection<Long> roleids, xio.Protocol p) {
		ppost(new SendToRoles(roleids, p));
	}

	public static void psendResponse(xio.Protocol pFromLink, xio.Protocol p) {
		ppost(new SendResponse(pFromLink, p));
	}

	public static void pbroadcast(xio.Protocol p, int timems) {
		ppost(new Broadcast(p, timems));
	}

	public static void psend(xio.Xio conn, xio.Protocol p) {
		ppost(new SendToXio(conn, p));
	}

	public static void pexecute(Procedure p) {
		ppost(new ExecuteProcedure(p));
	}

	public static void pexecute(Runnable r) {
		ppost(new ExecuteRunnable(r));
	}

	//////////////////////////////////////////////////////////////////////////////
	// ���·����ύ�������ڴ洢��������ύʱ��Ч��

	public static void psendWhileCommit(long roleid, xio.Protocol p) {
		ppostWhileCommit(new SendToRole(roleid, p));
	}

	public static void psendWhileCommit(long roleid1, long roleid2, xio.Protocol p) {
		ppostWhileCommit(new SendToRoles(roleid1, roleid2, p));
	}

	public static void psendWhileCommit(java.util.Collection<Long> roleids, xio.Protocol p) {
		ppostWhileCommit(new SendToRoles(roleids, p));
	}

	public static void psendResponseWhileCommit(xio.Protocol pFromLink, xio.Protocol p) {
		ppostWhileCommit(new SendResponse(pFromLink, p));
	}

	public static void pbroadcastWhileCommit(xio.Protocol p, int timems) {
		ppostWhileCommit(new Broadcast(p, timems));
	}

	public static void psendWhileCommit(xio.Xio conn, xio.Protocol p) {
		ppostWhileCommit(new SendToXio(conn, p));
	}

	public static void pexecuteWhileCommit(Procedure p) {
		ppostWhileCommit(new ExecuteProcedure(p));
	}

	public static void pexecuteWhileCommit(Runnable r) {
		ppostWhileCommit(new ExecuteRunnable(r));
	}

	///////////////////////////////////////////////////////////////////
	// ���·����ύ�������ڴ洢��̻ع�ʱ��Ч��
	public static void psendWhileRollback(long roleid, xio.Protocol p) {
		ppostWhileRollback(new SendToRole(roleid, p));
	}

	public static void psendWhileRollback(long roleid1, long roleid2, xio.Protocol p) {
		ppostWhileRollback(new SendToRoles(roleid1, roleid2, p));
	}

	public static void psendWhileRollback(java.util.Collection<Long> roleids, xio.Protocol p) {
		ppostWhileRollback(new SendToRoles(roleids, p));
	}

	public static void psendResponseWhileRollback(xio.Protocol pFromLink, xio.Protocol p) {
		ppostWhileRollback(new SendResponse(pFromLink, p));
	}

	public static void pbroadcastWhileRollback(xio.Protocol p, int timems) {
		ppostWhileRollback(new Broadcast(p, timems));
	}

	public static void psendWhileRollback(xio.Xio conn, xio.Protocol p) {
		ppostWhileRollback(new SendToXio(conn, p));
	}

	public static void pexecuteWhileRollback(Procedure p) {
		ppostWhileRollback(new ExecuteProcedure(p));
	}

	public static void pexecuteWhileRollback(Runnable r) {
		ppostWhileRollback(new ExecuteRunnable(r));
	}

	//////////////////////////////////////////////////////////////////////////
	// ����ʵ��
	public static class SendToRole extends Task {
		private long roleid;
		private xio.Protocol p;

		public SendToRole(long roleid, xio.Protocol p) {
			this.roleid = roleid;
			this.p = p;
		}

		@Override
		public void run() {
			Procedure.getOlines().send(roleid, p);
		}
	}

	public static class SendToRoles extends Task {
		private java.util.HashSet<Long> roleids = new java.util.HashSet<Long>();
		private xio.Protocol p;

		public SendToRoles(java.util.Collection<Long> roleids, xio.Protocol p) {
			this.roleids.addAll(roleids);
			this.p = p;
		}

		public SendToRoles(long roleid1, long roleid2, xio.Protocol p) {
			this.roleids.add(roleid1);
			this.roleids.add(roleid2);
			this.p = p;
		}

		@Override
		public void run() {
			Procedure.getOlines().send(roleids, p);
		}
	}

	public static class SendResponse extends Task {
		private xio.Protocol pFromLink;
		private xio.Protocol p;

		public SendResponse(xio.Protocol pFromLink, xio.Protocol p) {
			this.pFromLink = pFromLink;
			this.p = p;
		}

		@Override
		public void run() {
			Procedure.getOlines().sendResponse(pFromLink, p);
		}
	}

	public static class Broadcast extends Task {
		private xio.Protocol p;
		private int timems;

		public Broadcast(xio.Protocol p) {
			this.p = p;
			this.timems = 0;
		}

		public Broadcast(xio.Protocol p, int timems) {
			this.p = p;
			this.timems = timems;
		}

		@Override
		public void run() {
			Procedure.getOlines().broadcast(p, timems);
		}
	}

	public static class SendToXio extends Task {
		private xio.Xio conn;
		private xio.Protocol p;

		public SendToXio(xio.Xio conn, xio.Protocol p) {
			this.conn = conn;
			this.p = p;
		}

		@Override
		public void run() {
			this.p.send(conn);
		}
	}

	public static class ExecuteProcedure extends Task {
		private Procedure proc;

		public ExecuteProcedure(Procedure proc) {
			this.proc = proc;
		}

		@Override
		public void run() {
			this.proc.execute();
		}
	}

	public static class ExecuteRunnable extends Task {
		private Runnable command;

		public ExecuteRunnable(Runnable r) {
			this.command = r;
		}

		@Override
		public void run() {
			xdb.Xdb.executor().execute(command);
		}
	}
}
