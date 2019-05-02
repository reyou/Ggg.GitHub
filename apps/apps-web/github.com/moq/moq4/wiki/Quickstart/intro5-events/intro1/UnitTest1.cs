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
        ChildIntro Child { get; set; }
        event EventHandler Sent;
        event EventHandler FooEvent;
        bool DoSomething(string value);
        bool DoSomething(int number, string value);
        string DoSomethingStringy(string value);
        bool TryParse(string value, out string outputValue);
        bool Submit();
        int GetCount();
        bool Add(int value);
    }

    public class ChildIntro
    {
        public FirstIntro First { get; set; }
    }

    public class FirstIntro
    {
        public event EventHandler FooEvent;

        protected virtual void OnFooEvent()
        {
            FooEvent?.Invoke(this, EventArgs.Empty);
        }
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
        [TestMethod]
        public void TestMethod1()
        {
            Mock<IFoo> mock = new Mock<IFoo>();
            // Raising an event on the mock
            string fooValue = "Raising an event on the mock";
            mock.Raise(m => m.FooEvent += null, new FooEventArgs(fooValue));

            // Raising an event on the mock that has sender in handler parameters
            mock.Raise(m => m.FooEvent += null, this, new FooEventArgs(fooValue));

            // Raising an event on a descendant down the hierarchy
            mock.Raise(m => m.Child.First.FooEvent += null, new FooEventArgs(fooValue));

            // Causing an event to raise automatically when Submit is invoked
            // mock.Setup(foo => foo.Submit()).Raises(f => f.Sent += null, EventArgs.Empty);
            // The raised event would trigger behavior on the object under test, which 
            // you would make assertions about later (how its state changed as a consequence, typically)

        }
    }

    public class FooEventArgs : EventArgs
    {
        public FooEventArgs(object fooValue)
        {
            throw new NotImplementedException();
        }
    }
}
