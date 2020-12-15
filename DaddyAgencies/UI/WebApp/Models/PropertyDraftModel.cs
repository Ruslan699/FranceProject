using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class PropertyDraftModel : PropertyModel
    {
        public bool CreateAnother { get; set; }

        public List<HttpPostedFileBase> Images { get; set; }
    }
}