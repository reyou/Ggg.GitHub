using GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample.Abstracts;

namespace GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample
{
    /// <summary>
    /// Please note that there is no modification or re-implementation of the 
    /// ComputerAssembler class whatsoever. This class is completely abstracted 
    /// from the classes of the instances it assembles, as well as the creation 
    /// and initialization of the same.
    /// </summary>
    class ComputerAssembler
    {
        public Computer AssembleComputer(ComputerFactory factory)
        {
            Computer computer = factory.GetComputer();
            return computer;
        }
    }
}