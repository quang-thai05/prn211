using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectPRN211.Models
{
    public partial class Document
    {
        public int DocId { get; set; }
        public string DocSubject { get; set; }
        public string DocText { get; set; }
        public DateTime DocDate { get; set; }
        public int? HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }
    }
}
