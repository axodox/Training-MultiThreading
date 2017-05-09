using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace UiExamples
{
  /// <summary>
  /// Async / await example
  /// </summary>
  static class Example2
  {
    public static async void Run()
    {
      MessageBox.Show("A");
      var task = GenerateAsync();
      MessageBox.Show("B");
      var result = await task;
      MessageBox.Show(result.ToString());
    }

    private static async Task<int> GenerateAsync()
    {
      return await Task.Run(() => Generate());
    }

    private static int Generate()
    {
      Thread.Sleep(3000);
      return 5;
    }
  }
}
