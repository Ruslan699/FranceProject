
using System.ComponentModel;

namespace DaddyAgencies.Application.Models.Enums
{
    public enum PropertyType
    {
        [Description("Maison")]
        Building,
        [Description("Appartement")]
        Apartment,
        [Description("Immeuble")]
        Cottage,
        [Description("Terrain")]
        Area,
        [Description("Garage/Parking")]
        Parking,
        [Description("Locaux commerciaux")]
        Other
    }
}
