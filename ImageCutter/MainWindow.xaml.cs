namespace ImageCutter;

using MahApps.Metro.Controls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Want it to be computed and private ")]
    private string tempPath => Path.Combine(SaveFolderPath, "temp.png");

    public string SourceImagePath { get; set; }

    public string SaveFolderPath { get; set; }

    public MainWindow()
    {
        InitializeComponent();
    }

    private void CuttingParametersChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        UpdateCut();
    }

    private void UpdateCut()
    {
        if (cuttedHeight?.Value.HasValue == true && cuttedWidth?.Value.HasValue == true
            && heightOffset?.Value.HasValue == true && widthOffset?.Value.HasValue == true && preview != null)
        {
            (Image.FromFile(SourceImagePath) as Bitmap)
                .Clone(new Rectangle((int)widthOffset.Value, (int)heightOffset.Value, (int)cuttedWidth.Value, (int)cuttedHeight.Value), PixelFormat.DontCare)
                .Save(tempPath, ImageFormat.Png);
            var image = new BitmapImage();
            using (FileStream stream = File.OpenRead(tempPath))
            {
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.DecodePixelHeight = (int)cuttedHeight.Value;
                image.EndInit();
            }
            preview.Source = image;
        }
    }

    private void SaveImage()
    {
        if (save.IsOn)
        {
            File.Move(tempPath, Path.ChangeExtension(Path.Combine(SaveFolderPath, resultName?.Text), ".png"), true);
        }
        resultName.Text = null;
        save.IsOn = false;
    }

    private void NextHorizontal(object sender, RoutedEventArgs e)
    {
        SaveImage();
        widthOffset.Value += cuttedWidth.Value;
    }

    private void NextVertical(object sender, RoutedEventArgs e)
    {
        SaveImage();
        heightOffset.Value += cuttedHeight.Value;
    }
}