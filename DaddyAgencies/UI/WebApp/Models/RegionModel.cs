using System;

namespace WebApp.Models
{
    public class RegionModel
    {
        public Guid? Uid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsSelected { get; set; }
    }
}