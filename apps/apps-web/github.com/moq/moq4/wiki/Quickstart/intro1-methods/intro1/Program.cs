using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);

            // out arguments
            string outString = "ack";
            // TryParse will return true, and the out argument will return "ack", lazy evaluated
            mock.Setup(foo => foo.TryParse("ping", out outString)).Returns(true);


            // ref arguments
            Bar instance = new Bar();
            // Only matches if the ref argument to the invocation is the same instance
            mock.Setup(foo => foo.Submit(ref instance)).Returns(true);

            // access invocation arguments when returning a value
            mock.Setup(x => x.DoSomethingStringy(It.IsAny<string>()))
                .Returns((string s) => s.ToLower());
            // Multiple parameters overloads available

            // throwing when invoked with specific parameters
            mock.Setup(foo => foo.DoSomething("reset")).Throws<InvalidOperationException>();
            mock.Setup(foo => foo.DoSomething("")).Throws(new ArgumentException("command"));

            // lazy evaluating return value
            int count = 1;
            mock.Setup(foo => foo.GetCount()).Returns(() => count);


        }
    }
}
