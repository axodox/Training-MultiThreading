using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LowLevelExample
{
  /// <summary>
  /// Inter-process locking with a Mutex.
  /// </summary>
  static class Example5
  {
    private static bool isAlive = true;
    
    public static void Run()
    {
      var thread = new Thread(Worker);
      thread.Start();

      Console.WriteLine("Press enter to exit!");
      Console.ReadLine();
      isAlive = false;
      thread.Join();
    }

    private static void Worker()
    {
      Thread.CurrentThread.IsBackground = true;
      using (var mutex = new Mutex(false, "mutexExample"))
      {
        while (isAlive)
        {
          try
          {
            mutex.WaitOne();
            Console.WriteLine("I do something...");
            Thread.Sleep(500);
          }
          finally
          {
            mutex.ReleaseMutex();            
          }
        }
      }
    }
  }
}
