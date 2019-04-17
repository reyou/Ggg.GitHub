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
        // New Window-level variable.
        private readonly CancellationTokenSource _cancelToken = new CancellationTokenSource();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // This will be used to tell all the worker threads to stop!
            _cancelToken.Cancel();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            ProcessFiles();
        }
        private void ProcessFiles()
        {

            // Use ParallelOptions instance to store the CancellationToken.
            ParallelOptions parOpts = new ParallelOptions();
            parOpts.CancellationToken = _cancelToken.Token;
            parOpts.MaxDegreeOfParallelism = Environment.ProcessorCount;
            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles(@".\TestPictures", "*.jpg", SearchOption.AllDirectories);
            string newDir = @".\ModifiedPictures";
            Directory.CreateDirectory(newDir);

            Task.Run(() =>
            {
                try
                {
                    // Process the image data in a parallel manner!
                    Parallel.ForEach(files, parOpts, currentFile =>
                    {
                        parOpts.CancellationToken.ThrowIfCancellationRequested();
                        string filename = Path.GetFileName(currentFile);
                        using (Bitmap bitmap = new Bitmap(currentFile))
                        {
                            bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            bitmap.Save(Path.Combine(newDir, filename ?? throw new InvalidOperationException()));
                            // taking while to process image
                            Thread.Sleep(3000);
                            int currentThreadManagedThreadId = Thread.CurrentThread.ManagedThreadId;
                            Dispatcher.Invoke(delegate
                                {
                                    Title =
                                        $"Processing {filename} on thread {currentThreadManagedThreadId}";
                                    TextBoxStatus.Text += Title + Environment.NewLine;
                                }
                            );
                        }
                    }
                );
                    Dispatcher.Invoke(() => Title = "Done!");
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Title = ex.Message;
                        TextBoxStatus.Text += Title + Environment.NewLine;
                    });
                }

            });


        }

    }



}

