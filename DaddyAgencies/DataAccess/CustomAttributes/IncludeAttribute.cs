using System;

namespace DataAccess.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IncludeAttribute : Attribute
    {
    }
}
