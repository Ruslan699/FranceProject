
using System;

namespace DataModel.Models
{

    public class Property
    {
        public Guid? Uid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}
