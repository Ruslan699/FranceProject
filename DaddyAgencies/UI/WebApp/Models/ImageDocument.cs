using System;

namespace WebApp.Models
{
    public class ImageDocument
    {
        public Guid Uid { get; set; }

        public string FileName { get; set; }
        public int RowNo { get; set; }
        public string Extension { get; set; }

        public string AsBase64 { get; set; }
    }
}