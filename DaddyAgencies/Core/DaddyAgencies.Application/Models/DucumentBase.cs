using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DaddyAgencies.Application.Models
{
    public class DucumentBase
    {
        public Guid Uid { get; set; }

        public string FileName { get; set; }

        public int Type { get; set; } = 1;
        
        public string Extension { get; set; }
        public int RowNo { get; set; }
        public byte[] Body { get; set; }

        //public string Base64StringImage => string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(Body));

    }
}