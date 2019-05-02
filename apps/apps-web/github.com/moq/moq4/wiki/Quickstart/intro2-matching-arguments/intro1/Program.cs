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

            // any value
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(true);


            // any value passed in a `ref` parameter (requires Moq 4.8 or later):
            mock.Setup(foo => foo.Submit(ref It.Ref<Bar>.IsAny)).Returns(true);


            // matching Func<int>, lazy evaluated
            mock.Setup(foo => foo.Add(It.Is<int>(i => i % 2 == 0))).Returns(true);


            // matching ranges
            mock.Setup(foo => foo.Add(It.IsInRange<int>(0, 10, Range.Inclusive))).Returns(true);


            // matching regex
            mock.Setup(x => x.DoSomethingStringy(It.IsRegex("[a-d]+", RegexOptions.IgnoreCase))).Returns("foo");


        }
    }
}
