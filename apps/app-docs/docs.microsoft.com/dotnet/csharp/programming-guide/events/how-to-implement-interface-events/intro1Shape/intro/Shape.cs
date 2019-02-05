using System;

namespace intro
{
    public class Shape : IDrawingObject
    {
        public event EventHandler ShapeChanged;

        public void ChangeShape()
        {
            // Do something here before the event…  
            Console.WriteLine("changing shape now...");

            OnShapeChanged(new MyEventArgs(/*arguments*/));

            // or do something here after the event.
            Console.WriteLine("shape has been changed.");
        }
        protected virtual void OnShapeChanged(MyEventArgs e)
        {
            ShapeChanged?.Invoke(this, e);
        }
    }
}