using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace GggDataflowBlockSpecifyTaskSchedulerWinForms
{
    public partial class Form1 : Form
    {


        // Broadcasts values to an ActionBlock<int> object that is associated
        // with each check box.
        /*BroadcastBlock<T> Class Provides a buffer for storing at most one element at time,
         overwriting each message with the next as it arrives*/
        BroadcastBlock<int> broadcaster = new BroadcastBlock<int>(null);
        public Form1()
        {
            InitializeComponent();
            /*In the Form1 constructor, after the call to InitializeComponent,
             create an ActionBlock<TInput> object that toggles the state of 
             CheckBox objects.*/
            // Create an ActionBlock<CheckBox> object that toggles the state
            // of CheckBox objects.
            // Specifying the current synchronization context enables the 
            // action to run on the user-interface thread.
            ActionBlock<CheckBox> toggleCheckBox = new ActionBlock<CheckBox>(checkbox =>
            {
                checkbox.Checked = !checkbox.Checked;

            }, new ExecutionDataflowBlockOptions()
            {
                TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
            });

            // Create a ConcurrentExclusiveSchedulerPair object.
            // Readers will run on the concurrent part of the scheduler pair.
            // The writer will run on the exclusive part of the scheduler pair.
            /*ConcurrentExclusiveSchedulerPair Class Provides task schedulers that coordinate to
             execute tasks while ensuring that concurrent tasks may run concurrently and 
             exclusive tasks never do*/
            ConcurrentExclusiveSchedulerPair taskSchedulerPair = new ConcurrentExclusiveSchedulerPair();

            // Create an ActionBlock<int> object for each reader CheckBox object.
            // Each ActionBlock<int> object represents an action that can read 
            // from a resource in parallel to other readers.
            // Specifying the concurrent part of the scheduler pair enables the 
            // reader to run in parallel to other actions that are managed by 
            // that scheduler.
            IEnumerable<ActionBlock<int>> readerActions
                = from checkbox in new[]
                    { checkBox1, checkBox2, checkBox3, checkBox4 }
                  select new ActionBlock<int>(milliseconds =>
                  {
                      // Toggle the check box to the checked state.
                      toggleCheckBox.Post(checkbox);

                      // Perform the read action. For demostration, suspend the current
                      // thread to simulate a length read operation.
                      Thread.Sleep(milliseconds);

                      // Toggle the check box to the unchecked state.
                      toggleCheckBox.Post(checkbox);
                  }, new ExecutionDataflowBlockOptions()
                  {
                      TaskScheduler = taskSchedulerPair.ConcurrentScheduler
                  });

            // Create an ActionBlock<int> object for the writer CheckBox object.
            // This ActionBlock<int> object represents an action that writes to 
            // a resource, but cannot run in parallel to readers.
            // Specifying the exclusive part of the scheduler pair enables the 
            // writer to run in exclusively with respect to other actions that are 
            // managed by the scheduler pair.
            ActionBlock<int> writerAction = new ActionBlock<int>(milliseconds =>
            {
                // Toggle the check box to the checked state.
                toggleCheckBox.Post(checkBox4);

                // Perform the write action. For demonstration, suspend the current
                // thread to simulate a lengthy write operation.
                Thread.Sleep(milliseconds);

                // Toggle the check box to the unchecked state.
                toggleCheckBox.Post(checkBox4);
            }, new ExecutionDataflowBlockOptions()
            {
                TaskScheduler = taskSchedulerPair.ExclusiveScheduler
            });

            // Link the broadcaster to each reader and writer block.
            // The BroadcastBlock<T> class propagates values that it 
            // receives to all connected targets.
            foreach (ActionBlock<int> readerAction in readerActions)
            {
                broadcaster.LinkTo(readerAction);
            }

            broadcaster.LinkTo(writerAction);

            timer1.Start();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            // Post a value to the broadcaster. The broadcaster
            // sends this message to each target. 
            broadcaster.Post(1000);
        }
    }
}
