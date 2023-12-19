namespace ImageCutter;

using MahApps.Metro.Controls;
using System.Windows;

/// <summary>
/// Interaction logic for StartupWindow.xaml
/// </summary>
public partial class StartupWindow : MetroWindow
{
    public StartupWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow()
        {
            SourceImagePath = source.Text,
            SaveFolderPath = destination.Text,
        }.Show();
    }
}
