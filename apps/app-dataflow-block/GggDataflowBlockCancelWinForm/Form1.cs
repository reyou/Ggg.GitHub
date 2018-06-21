using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace GggDataflowBlockCancelWinForm
{
    public partial class Form1 : Form
    {
        // Enables the user interface to signal cancellation.
        /*Because the CancellationToken property permanently cancels dataflow block execution,
         the whole pipeline must be recreated after the user cancels the operation and 
         then wants to add more work items to the pipeline. */
        CancellationTokenSource _cancellationSource;
        // The first node in the dataflow pipeline.
        TransformBlock<WorkItem, WorkItem> _startWork;

        // The second, and final, node in the dataflow pipeline.
        ActionBlock<WorkItem> _completeWork;

        // Increments the value of the provided progress bar.
        ActionBlock<ToolStripProgressBar> _incrementProgress;

        // Decrements the value of the provided progress bar.
        ActionBlock<ToolStripProgressBar> _decrementProgress;

        // Enables progress bar actions to run on the UI thread.
        readonly TaskScheduler _uiTaskScheduler;



        public Form1()
        {
            InitializeComponent();
            // Create the UI task scheduler from the current sychronization
            // context.
            _uiTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }
        // A placeholder type that performs work.
        public class WorkItem
        {
            // Performs work for the provided number of milliseconds.
            public void DoWork(int milliseconds)
            {
                // For demonstration, suspend the current thread.
                Thread.Sleep(milliseconds);
            }
        }

        // Creates the blocks that participate in the dataflow pipeline.
        private void CreatePipeline()
        {

            // Create the cancellation source.
            _cancellationSource = new CancellationTokenSource();
            // Create the first node in the pipeline. 
            _startWork = new TransformBlock<WorkItem, WorkItem>(workItem =>
                {
                    // Perform some work.
                    workItem.DoWork(250);

                    // Decrement the progress bar that tracks the count of 
                    // active work items in this stage of the pipeline.
                    _decrementProgress.Post(toolStripProgressBar1);

                    // Increment the progress bar that tracks the count of 
                    // active work items in the next stage of the pipeline.
                    _incrementProgress.Post(toolStripProgressBar2);

                    // Send the work item to the next stage of the pipeline.
                    return workItem;
                },
                new ExecutionDataflowBlockOptions
                {
                    CancellationToken = _cancellationSource.Token
                });

            // Create the second, and final, node in the pipeline. 
            _completeWork = new ActionBlock<WorkItem>(workItem =>
                {
                    // Perform some work.
                    workItem.DoWork(1000);

                    // Decrement the progress bar that tracks the count of 
                    // active work items in this stage of the pipeline.
                    _decrementProgress.Post(toolStripProgressBar2);

                    // Increment the progress bar that tracks the overall 
                    // count of completed work items.
                    _incrementProgress.Post(toolStripProgressBar3);
                },
                new ExecutionDataflowBlockOptions
                {
                    CancellationToken = _cancellationSource.Token,
                    MaxDegreeOfParallelism = 2
                });

            // Connect the two nodes of the pipeline. When the first node completes, 
            // set the second node also to the completed state.
            _startWork.LinkTo(_completeWork, new DataflowLinkOptions { PropagateCompletion = true });

            // Create the dataflow action blocks that increment and decrement
            // progress bars.
            // These blocks use the task scheduler that is associated with
            // the UI thread.
            _incrementProgress = new ActionBlock<ToolStripProgressBar>(
                progressBar =>
                {
                    progressBar.Value++;
                    labelWorkItemStatus.Text = "_incrementProgress is running: " + progressBar.Value;
                },
                new ExecutionDataflowBlockOptions
                {
                    CancellationToken = _cancellationSource.Token,
                    TaskScheduler = _uiTaskScheduler
                });

            _decrementProgress = new ActionBlock<ToolStripProgressBar>(
                progressBar =>
                {
                    progressBar.Value--;
                    labelWorkItemStatus.Text = "_decrementProgress is running: " + progressBar.Value;
                },
                new ExecutionDataflowBlockOptions
                {
                    CancellationToken = _cancellationSource.Token,
                    TaskScheduler = _uiTaskScheduler
                });

        }

        private void toolStripButtonAddWorkItem_Click(object sender, System.EventArgs e)
        {
            // The Cancel button is disabled when the pipeline is not active.
            // Therefore, create the pipeline and enable the Cancel button
            // if the Cancel button is disabled.
            if (!toolStripButtonCancel.Enabled)
            {
                CreatePipeline();

                // Enable the Cancel button.
                toolStripButtonCancel.Enabled = true;
            }

            // Post several work items to the head of the pipeline.
            for (int i = 0; i < 100; i++)
            {
                toolStripProgressBar1.Value++;
                _startWork.Post(new WorkItem());
            }

        }

        private async void toolStripButtonCancel_Click(object sender, System.EventArgs e)
        {
            // Disable both buttons.
            toolStripButtonAddWorkItem.Enabled = false;
            toolStripButtonCancel.Enabled = false;
            // Trigger cancellation.
            _cancellationSource.Cancel();
            try
            {
                // Asynchronously wait for the pipeline to complete processing and for
                // the progress bars to update.
                await Task.WhenAll(
                    _completeWork.Completion,
                    _incrementProgress.Completion,
                    _decrementProgress.Completion);
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Increment the progress bar that tracks the number of cancelled 
            // work items by the number of active work items.
            toolStripProgressBar4.Value += toolStripProgressBar1.Value;
            toolStripProgressBar4.Value += toolStripProgressBar2.Value;

            // Reset the progress bars that track the number of active work items.
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar2.Value = 0;

            // Enable the Add Work Items button.      
            toolStripButtonAddWorkItem.Enabled = true;
        }
    }
}
