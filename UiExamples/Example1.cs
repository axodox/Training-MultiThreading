using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

namespace UiExamples
{
  /// <summary>
  /// Using the dispatcher to update the UI
  /// </summary>
  class Example1
  {
    public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

    public Example1()
    {
      new Thread(Worker).Start();
    }

    private void Worker()
    {
      Thread.CurrentThread.IsBackground = true;
      var number = 0;
      while (true)
      {
        Thread.Sleep(100);
        //Items.Add($"Item {number++}"); -> runtime error
        Application.Current.Dispatcher.Invoke(() => Items.Add($"Item {number++}"));
      }
    }
  }
}
