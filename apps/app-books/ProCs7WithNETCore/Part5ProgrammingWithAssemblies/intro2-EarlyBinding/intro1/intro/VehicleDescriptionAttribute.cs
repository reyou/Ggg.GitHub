using System;

namespace intro
{
    // This time, we are using the AttributeUsage attribute
    // to annotate our custom attribute.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class VehicleDescriptionAttribute : Attribute
    {
        public string Description { get; set; }
        public VehicleDescriptionAttribute(string vehicalDescription) => Description =
            vehicalDescription;

        public VehicleDescriptionAttribute()
        {

        }
    }

    // Assign description using a "named property."
}