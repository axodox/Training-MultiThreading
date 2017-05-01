using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LowLevelExample
{
  static class Example1
  {
    public static void Run()
    {
      var threads = new List<Thread>();

      for (int i = 0; i < Environment.ProcessorCount; i++)
      {
        var thread = new Thread(Worker);
        threads.Add(thread);
        thread.Start(i);
      }

      foreach (var thread in threads)
      {
        thread.Join();
      }
      Console.ReadLine();
    }

    private static void Worker(object o)
    {
      var id = (int)o;
      Console.WriteLine($"Hello from thread {id}!");
      Thread.Sleep(100 * id);
      Console.WriteLine($"Bye from thread {id}.");
    }
  }
}
