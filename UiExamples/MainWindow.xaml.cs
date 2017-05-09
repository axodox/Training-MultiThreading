using System.Windows;

namespace UiExamples
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      //DataContext = new Example1();
      Example2.Run();
    }
  }
}
