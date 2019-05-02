using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Moq;

namespace intro1
{
    public interface IFoo
    {
        Bar Bar { get; set; }
        string Name { get; set; }
        int Value { get; set; }
        bool DoSomething(string value);
        bool DoSomething(int number, string value);
        string DoSomethingStringy(string value);
        bool TryParse(string value, out string outputValue);
        bool Submit(ref Bar bar);
        int GetCount();
        bool Add(int value);
    }

    public class Bar
    {
        public virtual Baz Baz { get; set; }
        public virtual bool Submit() { return false; }
    }

    public class Baz
    {
        public virtual string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mock<IFoo> mock = new Mock<IFoo>();

            mock.Setup(foo => foo.Name).Returns("bar");


            // auto-mocking hierarchies (a.k.a. recursive mocks)
            mock.Setup(foo => foo.Bar.Baz.Name).Returns("baz");

            // expects an invocation to set the value to "foo"
            mock.SetupSet(foo => foo.Name = "foo");

            // or verify the setter directly
            mock.VerifySet(foo => foo.Name = "foo");



        }
    }
}
