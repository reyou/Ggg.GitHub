using GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Abstracts;

namespace GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Computers
{
    class BrandXComputer : Computer
    {
        int _mhz = 1500;
        public override int Mhz => _mhz;
    }
}