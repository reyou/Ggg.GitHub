using GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample.Abstracts;
using GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample.Computers;

namespace GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample.Factories
{
    class BrandXFactory : ComputerFactory
    {

        public override Computer GetComputer()
        {
            return new BrandXComputer();
        }

    }
}