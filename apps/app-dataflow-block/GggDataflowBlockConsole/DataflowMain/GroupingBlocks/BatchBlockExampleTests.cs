using GggDataflowBlockConsole.DataFlowMain.GroupingBlocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggDataflowBlockConsole.DataflowMain.GroupingBlocks
{

    [TestClass]
    public class BatchBlockExampleTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            TestUtilities.SetConsoleOutput("BatchBlockExampleTests");
        }
        [TestMethod]
        public void RunTest()
        {
            BatchBlockExample.Run();
        }
    }
}
