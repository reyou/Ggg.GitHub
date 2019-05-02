using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Setup a property so that it will automatically
        /// start tracking its value (also known as Stub):
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            Mock<IFoo> mock = new Mock<IFoo>();

            // start "tracking" sets/gets to this property
            mock.SetupProperty(f => f.Name);

            // alternatively, provide a default value for the stubbed property
            mock.SetupProperty(f => f.Name, "foo");

            // Stub all properties on a mock (not available on Silverlight):
            mock.SetupAllProperties();

            // Now you can do:

            IFoo foo = mock.Object;
            // Initial value was stored
            Assert.AreEqual("foo", foo.Name);

            // New value set which changes the initial value
            foo.Name = "bar";
            Assert.AreEqual("bar", foo.Name);
        }
    }
}
