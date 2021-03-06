﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollectionsExample
{
  /// <summary>
  /// Parallel generation of random numbers with concurrent bag
  /// </summary>
  static class Example1
  {
    private static ConcurrentBag<double> _numbers = new ConcurrentBag<double>();
    
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
      for(int i = 0; i < 10000; i++)
      {
        var number = random.NextDouble();
        Console.WriteLine($"Thread #{id}: my number is {number}!");
        _numbers.Add(number);
      }

      Console.WriteLine($"Bye from thread {id}.");
    }
  }
}
