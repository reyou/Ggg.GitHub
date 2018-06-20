using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace GggDataflowBlockCancelWinForm
{
    public partial class Form1 : Form
    {
        // Enables the user interface to signal cancellation.
        CancellationTokenSource cancellationSource;
        // The first node in the dataflow pipeline.
        TransformBlock<WorkItem, WorkItem> startWork;

        // The second, and final, node in the dataflow pipeline.
        ActionBlock<WorkItem> completeWork;

        // Increments the value of the provided progress bar.
        ActionBlock<ToolStripProgressBar> incrementProgress;

        // Decrements the value of the provided progress bar.
        ActionBlock<ToolStripProgressBar> decrementProgress;

        // Enables progress bar actions to run on the UI thread.
        TaskScheduler uiTaskScheduler;



        public Form1()
        {
            InitializeComponent();
        }
        // A placeholder type that performs work.
        class WorkItem
        {
            // Performs work for the provided number of milliseconds.
            public void DoWork(int milliseconds)
            {
                // For demonstration, suspend the current thread.
                Thread.Sleep(milliseconds);
            }
        }


    }
}
