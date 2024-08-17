using System;
using System.Threading;
using Repository;

class Manager
{
    private static RepositoryHandler obj = new RepositoryHandler();
    public void RegisterManager(string itemName, string itemContent, int itemType)
    {
        Thread workerThread = new Thread(() =>
        {
            Thread.Sleep(10);
            obj.Register(itemName, itemContent, itemType);
        });
        workerThread.Start();
        workerThread.Join();
    }
    
     public void DeRegisterManager(string content)
    {
        Thread workerThread = new Thread(() =>
        {
            Thread.Sleep(10);
            obj.DeRegister(content);
        });
        workerThread.Start();
        workerThread.Join();
    }

    public void RetrieveManager(string content,  Action<string?> callback)
    {
       Thread workerThread = new Thread(() =>
        {
            Thread.Sleep(10);
            string? result = obj.Retrieve(content);
            callback(result);
        });
        workerThread.Start();
        workerThread.Join();
    }
    public void GetTypeManager(string content,  Action<int?> callback)
    {
       Thread workerThread = new Thread(() =>
        {
            Thread.Sleep(10);
            int? result = obj.GetType(content);
            callback(result);
        });
        workerThread.Start();
        workerThread.Join();
    }

  
    
}
