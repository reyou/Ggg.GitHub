using GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Abstracts;

namespace GggDocsClassLibrary.DesignPatterns.FactoryPattern.msdnSample.Computers
{
    class ConcreteComputer : Computer
    {
        int _mhz = 500;
        public override int Mhz => _mhz;
    }
}