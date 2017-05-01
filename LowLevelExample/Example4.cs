using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LowLevelExample
{
  /// <summary>
  /// Locking with a timeout with Semaphore.
  /// </summary>
  static class Example4
  {
    private static List<double> _numbers = new List<double>();

    private static Semaphore _semaphore = new Semaphore(1, 1);
    
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

      var avg = _numbers.Average();
      Console.WriteLine($"The average is {avg}.");
      Console.ReadLine();
    }

    private static void Worker(object o)
    {
      var id = (int)o;
      Console.WriteLine($"Hello from thread {id}!");

      var random = new Random();
      for(int i = 0; i < 10; i++)
      {
        var number = random.NextDouble();        
        Console.WriteLine($"Thread #{id}: my number is {number}!");

        if(_semaphore.WaitOne(TimeSpan.FromSeconds(0.01)))
        {
          try
          {
            StoreNumber(number);
          }
          finally
          {
            _semaphore.Release();
          }
        }
      }

      Console.WriteLine($"Bye from thread {id}.");
    }

    private static void StoreNumber(double number)
    {
      Thread.Sleep(TimeSpan.FromSeconds(2 * number));
      _numbers.Add(number);
    }
  }
}
