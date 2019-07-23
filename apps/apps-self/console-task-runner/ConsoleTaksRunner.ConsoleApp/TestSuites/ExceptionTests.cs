using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleTaksRunner.ConsoleApp.TestSuites
{
    public class ExceptionTests : ITestSuite
    {
        public void InvalidOperationExceptionTest(ApplicationEnvironment applicationEnvironment)
        {
            throw new InvalidOperationException("This operation is invalid");
        }
    }
}
