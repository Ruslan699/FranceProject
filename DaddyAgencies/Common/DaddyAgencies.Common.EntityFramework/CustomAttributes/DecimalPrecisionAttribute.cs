using System;

namespace DaddyAgencies.Common.EntityFramework.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DecimalPrecisionAttribute : Attribute
    {
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }

        public byte Precision { get; set; }

        public byte Scale { get; set; }
    }
}
