using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LowLevelExample
{
  /// <summary>
  /// Parallel generation of random numbers
  /// </summary>
  static class Example9
  {
    public static void Run()
    {
      var numbers = new ConcurrentBag<double>();
      Parallel.For(0, 100000, i => numbers.Add(new Random().NextDouble()));

      var avg = numbers.Average();
      Console.WriteLine($"The average is {avg}.");
      Console.ReadLine();
    }
  }
}
