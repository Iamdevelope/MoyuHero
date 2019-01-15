public class Task
{
    private Protocol protocol = null;

    public Task( Protocol paramProtocol )
    {
        protocol = paramProtocol;
    }

    public void run()
    {
        if (protocol != null)
            protocol.Process();
    }

    public static void Dispatch( Protocol paramProtocol )
    {
        Task localTask = new Task(paramProtocol);

        int i = paramProtocol.PriorPolicy();
        if (i > 0)
            TaskPool.AddTask(localTask);
    }

  
}
