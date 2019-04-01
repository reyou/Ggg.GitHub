using GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample.Abstracts;

namespace GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.FactoryPattern.msdnSample.Computers
{
    class BrandXComputer : Computer
    {
        int _mhz = 1500;
        public override int Mhz => _mhz;
    }
}