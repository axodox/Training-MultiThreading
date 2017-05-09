using System;
using System.IO;
using System.Linq;

namespace LowLevelExample
{
  /// <summary>
  /// Parallel LINQ example for determining size of a folder
  /// </summary>
  static class Example10
  {
    public static void Run()
    {
      var size = Directory
        .GetFiles(@"C:\cae\dev\ISF", "*.*", SearchOption.AllDirectories)
        .AsParallel()
        .Select(p => new FileInfo(p).Length)
        .Sum();

      Console.WriteLine($"The size of folder is {size}.");
      Console.ReadLine();
    }
  }
}
