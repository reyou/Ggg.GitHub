using System;
using System.Collections.Generic;
using GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample;
using GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample.Abstracts;
using GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ee817667.aspx
    /// </summary>
    [TestClass]
    public class FactoryPatternTests
    {
        [TestMethod]
        public void AssembleComputerTest2()
        {
            ComputerFactory factory;
            List<string> args = new List<string>();
            int i = Guid.NewGuid().GetHashCode() % 2;
            if (i == 0)
            {
                args.Add("BrandX");
            }

            if (args.Count > 0 && args[0] == "BrandX")
            {
                factory = new BrandXFactory();
            }
            else
            {
                factory = new ConcreteComputerFactory();
            }
            ComputerAssembler computerAssembler = new ComputerAssembler();
            Computer computer = computerAssembler.AssembleComputer(factory);
            Console.WriteLine("assembled a {0} running at {1} MHz",
                computer.GetType().FullName, computer.Mhz);
        }

        [TestMethod]
        public void AssembleComputerTest()
        {
            ComputerFactory factory = new ConcreteComputerFactory();
            ComputerAssembler computerAssembler = new ComputerAssembler();
            Computer computer = computerAssembler.AssembleComputer(factory);
            Console.WriteLine("assembled a {0} running at {1} MHz",
                computer.GetType().FullName, computer.Mhz);
        }
    }
}
