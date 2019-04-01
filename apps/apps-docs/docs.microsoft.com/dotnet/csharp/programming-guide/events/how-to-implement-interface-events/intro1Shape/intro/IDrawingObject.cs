using System;

namespace intro
{
    public interface IDrawingObject
    {
        event EventHandler ShapeChanged;
    }
}