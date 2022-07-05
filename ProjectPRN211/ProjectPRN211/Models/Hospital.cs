using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectPRN211.Models
{
    public partial class Hospital
    {
        public Hospital()
        {
            Documents = new HashSet<Document>();
            Users = new HashSet<User>();
        }

        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
