using GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Abstracts;
using GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Computers;

namespace GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Factories
{
    class BrandXFactory : ComputerFactory
    {

        public override Computer GetComputer()
        {
            return new BrandXComputer();
        }

    }
}