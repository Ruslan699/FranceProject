using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaddyAgencies.EntityFramework.Models
{
    using Common.EntityFramework;
    using Common.EntityFramework.CustomAttributes;

    [Table("Properties")]
    public class PropertyEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Name")]
        [StringLength(128)]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        #region Fixed properties

        [Required]
        [Column("Condition")]
        public int Condition { get; set; }

        [Required]
        [Column("PropertyType")]
        public int PropertyType { get; set; }

        [Required]
        [Column("RepairType")]
        public int RepairType { get; set; }

        [Required]
        [Column("PropertyMaterial")]
        public int PropertyMaterial { get; set; }

        #endregion

        #region Other properties

        [Column("YearOfConstruction")]
        public int? YearOfConstruction { get; set; }

        //total 9 floors
        [Column("FloorsNumber")]
        public int? FloorsNumber { get; set; }

        //second floor
        [Column("FloorsOutOf")]
        public int? FloorsOutOf { get; set; }

        //second floor
        [Column("RoomsCount")]
        public int? RoomsCount { get; set; }

        #endregion

        #region Location

        [Required]
        [Column("Longitude")]
        [DecimalPrecision(18, 16)]
        public decimal Longitude { get; set; }

        [Required]
        [Column("Latitude")]
        [DecimalPrecision(18, 16)]
        public decimal Latitude { get; set; }

        [Column("Address")]
        [StringLength(500)]
        public string Address { get; set; }

        #endregion

        #region Price

        [Required]
        [Column("SalePrice")]
        [DecimalPrecision(18, 2)]
        public decimal SalePrice { get; set; }

        [Column("Tax")]
        [DecimalPrecision(18, 2)]
        public decimal? Tax { get; set; }

        [Column("TaxPercent")]
        [DecimalPrecision(18, 2)]
        public decimal? TaxPercent { get; set; }

        [Column("TaxInclude")]
        public bool? TaxInclude { get; set; }

        [Column("SecurityPayment")]
        [DecimalPrecision(18, 2)]
        public decimal? SecurityPayment { get; set; }

        [Column("ClientCommission")]
        [DecimalPrecision(18, 2)]
        public decimal? ClientCommission { get; set; }

        #endregion

        #region Square

        [Required]
        [Column("TotalSquare")]
        [DecimalPrecision(9, 2)]
        public decimal TotalSquare { get; set; }

        [Column("LandArea")]
        [DecimalPrecision(9, 2)]
        public decimal? LandArea { get; set; }

        #endregion

        #region Parking

        [Column("ParkingType")]
        public int? ParkingType { get; set; }

        [Column("ParkingSize")]
        public int? ParkingSize { get; set; }

        [Column("ParkingCost")]
        public int? ParkingCost { get; set; }

        #endregion

        #region AdditionalInfo
        public string Ges { get; set; }
        public string EnergyClass { get; set; }
        public string Status { get; set; }
        public string Parking { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerNote { get; set; }

        #endregion

        #region Relations

        [Column("PostalCodeUid")]
        [ForeignKey("PostalCode")]
        public Guid? PostalCodeUid { get; set; }

        public virtual PostalCodeEntity PostalCode { get; set; }

        public virtual List<PropertyDocumentEntity> Documents { get; set; }

        public virtual List<PropertyFloorEntity> Floors { get; set; }

        public virtual List<PropertyAdditionalElementEntity> AdditionalElements { get; set; }

        #endregion
    }
}