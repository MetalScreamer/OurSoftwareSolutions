using System.Windows;

namespace Oss.Windows.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(object viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
