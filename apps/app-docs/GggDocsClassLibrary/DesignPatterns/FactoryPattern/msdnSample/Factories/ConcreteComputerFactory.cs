using GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Abstracts;
using GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Computers;

namespace GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Factories
{
    class ConcreteComputerFactory : ComputerFactory
    {
        public override Computer GetComputer()
        {
            return new ConcreteComputer();

        }
    }
}