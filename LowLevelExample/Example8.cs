using System;
using System.Collections.Generic;
using System.Threading;

namespace LowLevelExample
{
  /// <summary>
  /// Lock-less cooperation with interlocked operations
  /// This solution is lock-less on x86 based platforms, but might not be on others!
  /// </summary>
  static class Example8
  {
    private static int _count = 0;
    private static int _sum = 0;

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

      var avg = (double)_sum / _count;
      Console.WriteLine($"The average is {avg}.");
      Console.ReadLine();
    }

    private static void Worker(object o)
    {
      var id = (int)o;
      Console.WriteLine($"Hello from thread {id}!");

      var random = new Random();
      for (int i = 0; i < 10000; i++)
      {
        var number = random.Next(100);
        Console.WriteLine($"Thread #{id}: my number is {number}!");
        Interlocked.Add(ref _sum, number);
        Interlocked.Increment(ref _count);
      }

      Console.WriteLine($"Bye from thread {id}.");
    }
  }
}
