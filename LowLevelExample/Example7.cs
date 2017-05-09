using System;
using System.Threading;

namespace LowLevelExample
{
  /// <summary>
  /// Passing work to a background thread while main thread interacts with the user
  /// Another thread also gives work to worker with an async timer
  /// </summary>
  static class Example7
  {
    private static int _number;
    private static ManualResetEvent _workerReady = new ManualResetEvent(false);
    private static AutoResetEvent _numberReady = new AutoResetEvent(false);
    private static Timer _timer = new Timer(Bot, null, 1000, 1000);
    private static Random _random = new Random();

    public static void Run()
    {
      Console.WriteLine("Main: Let's create a worker.");
      var thread = new Thread(Worker);
      thread.Start();
      Console.WriteLine("Main: I am waiting...");
      _workerReady.WaitOne();

      while (true)
      {
        Console.WriteLine("Main: Give me a number!");
        if (int.TryParse(Console.ReadLine(), out _number))
        {
          _numberReady.Set();
        }
      }
    }

    private static void Bot(object o)
    {
      var number = _random.Next(10);
      Console.WriteLine($"Bot: I give you {number}!");
      _number = number;
      _numberReady.Set();
    }

    private static void Worker()
    {
      Console.WriteLine("Worker: Hello!");
      Console.WriteLine("Worker: I am initializing...");

      var sum = 0;
      Thread.Sleep(500);
      Console.WriteLine("Worker: I am ready!");
      _workerReady.Set();

      while (true)
      {
        _numberReady.WaitOne();

        Console.WriteLine($"Worker: {sum} + {_number} = {sum + _number}");
        sum += _number;
      }
    }
  }
}
