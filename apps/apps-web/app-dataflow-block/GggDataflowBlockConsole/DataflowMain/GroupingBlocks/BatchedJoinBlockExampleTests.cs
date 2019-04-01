using GggDataflowBlockConsole.DataFlowMain.GroupingBlocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggDataflowBlockConsole.DataflowMain.GroupingBlocks
{
    [TestClass]
    public class BatchedJoinBlockExampleTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            TestUtilities.SetConsoleOutput("BatchedJoinBlockExampleTests");
        }

        [TestMethod]
        public void BatchedJoinBlockExampleTest()
        {
            BatchedJoinBlockExample.Run();
        }
    }
}
