using KickBlastEliteUI.ViewModels;
using System.Windows;

namespace KickBlastEliteUI;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
