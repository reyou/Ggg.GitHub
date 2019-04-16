using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

// Be sure you have these namespaces!

namespace intro1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            ProcessFiles();
        }
        private void ProcessFiles()
        {
            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles(@".\TestPictures", "*.jpg", SearchOption.AllDirectories);
            string newDir = @".\ModifiedPictures";
            Directory.CreateDirectory(newDir);
            // Process the image data in a blocking manner.
            Parallel.ForEach(files, currentFile =>
            {
                string filename = Path.GetFileName(currentFile);
                using (Bitmap bitmap = new Bitmap(currentFile))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, filename ?? throw new InvalidOperationException()));
                    // This code statement is now a problem! See next section.
                    Dispatcher.Invoke(() =>
                    {
                        Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                        TextBoxStatus.Text += Title + Environment.NewLine;
                    });
                }
            });

        }
    }



}

