using System;
using System.Collections.Generic;

namespace DaddyAgencies.Application.Models
{
    public enum InseptionStatus
    {
        Nouveau = 1,
        Confirmation = 2,
        Confirmé = 3,
        Helded = 4,
        AbortedByUser = 5,
        Rejeté = 6
    }

    public class Inseption
    {
        public Guid Uid { get; set; }

        public string Description { get; set; }

        public Guid PostalCodeUid { get; set; }
        public string PostalCodeName { get; set; }

        public Guid RegionUid { get; set; }
        public string RegionName { get; set; }

        public Guid PropertyUid { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }


        public object CustomerFullName { get; set; }
        public object CustomerEmail { get; set; }
        public object CustomerPhone { get; set; }


        public string ConfirmedAddress { get; set; }


        public DateTime InseptionDate { get; set; }

        public DateTime RequestedDate { get; set; }

        public DateTime? ConfirmedDate { get; set; }
        public int? Time { get; set; }

        public Guid? RequesterUserUid { get; set; }
        public Guid? ConfirmerUserUid { get; set; }

        public int StatusId { get; set; }

        public InseptionStatus Status
        {
            get => (InseptionStatus)StatusId;
            set => StatusId = (int)value;
        }


        public string InceptionStatusName => Status.ToString(); 
    }
}
