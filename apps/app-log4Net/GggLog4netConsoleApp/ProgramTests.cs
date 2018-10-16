using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggUnitTestProject1
{
    [TestClass]
    public class ProgramTests
    {
        /// <summary>
        /// https://sadi02.wordpress.com/2008/09/15/how-to-store-log-in-database-using-log4net/
        /// </summary>
        [TestMethod]
        public void Log()
        {
            Program.Log();
        }
    }
}
