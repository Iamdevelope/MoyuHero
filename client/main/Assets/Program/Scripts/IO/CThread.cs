
using System.Threading;

public abstract class CThread
{
    protected Thread newThread = null;
    abstract public void run();

    public void start()
    {
        if (newThread == null)
            newThread = new Thread(run);
        newThread.Start();
    }

	abstract public void abort ();

    public void stop()
    {
        if (newThread != null )
        {
			abort();
//            newThread.Abort();
            newThread = null;
        }
    }

};


