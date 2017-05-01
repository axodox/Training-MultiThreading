using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollectionsExample
{
  /// <summary>
  /// Passing work to a background thread while main thread interacts with the user
  /// Another thread also gives work to worker with an async timer
  /// </summary>
  static class Example2
  {
    private static BlockingCollection<int> _numbers = new BlockingCollection<int>();
    private static ManualResetEvent _workerReady = new ManualResetEvent(false);
    private static Timer _timer = new Timer(Bot, null, 1000, 1000);
    private static Random _random = new Random();
    
    public static void Run()
    {
      Console.WriteLine("Main: Let's create a worker.");
      var thread = new Thread(Worker);
      thread.Start();
      Console.WriteLine("Main: I am waiting...");
      _workerReady.WaitOne();
      
      while(true)
      {
        Console.WriteLine("Main: Give me a number!");
        int number;
        if(int.TryParse(Console.ReadLine(), out number))
        {
          _numbers.Add(number);
        }
      }
    }
    
    private static void Bot(object o)
    {
      var number = _random.Next(10);
      Console.WriteLine($"Bot: I give you {number}!");
      _numbers.Add(number);
    }

    private static void Worker()
    {
      Console.WriteLine("Worker: Hello!");
      Console.WriteLine("Worker: I am initializing...");

      var sum = 0;
      Thread.Sleep(500);
      Console.WriteLine("Worker: I am ready!");
      _workerReady.Set();

      while(true)
      {
        var number = _numbers.Take();
        
        Console.WriteLine($"Worker: {sum} + {number} = {sum + number}");
        sum += number;
      }
    }
  }
}
