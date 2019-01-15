using System.Collections.Generic;

public class TaskPool
{

    private static LinkedList<Task> remove = new LinkedList<Task>();
    private static TaskPool mInst = null;
    private static int task_count = 0;          //记录任务次数
    private static object task_count_locker = new object();

    public static TaskPool GetInstance()
    {
        if (mInst == null)
        {
            mInst = new TaskPool();
        }
        return mInst;
    }

    public TaskPool()
    {
        
    }

    public void run()
    {
        if (remove.Count != 0)
        {
            lock (remove)
            {
                do
                {
                    var localRunnable = remove.First.Value;
                    if (localRunnable != null)
                    {
                        lock(task_count_locker)
                        {
                          task_count -= 1;
                        }

                        localRunnable.run();
                    }
                       
                    remove.RemoveFirst();

                }
                while (remove.Count != 0);
            }
        }
    }

    public static void AddTask(Task paramRunnable)
    {
        lock (remove)
        {
            remove.AddLast(paramRunnable);
            lock(task_count_locker)
            {
                task_count += 1;
            }
        }
        
    }
  
}
