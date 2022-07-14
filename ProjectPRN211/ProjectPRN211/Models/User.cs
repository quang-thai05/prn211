using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectPRN211.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public int HospitalId { get; set; }
        public int RoleId { get; set; }
        public bool Active { get; set; }
        public string Otp { get; set; }

        public virtual Hospital Hospital { get; set; }
        public virtual Role Role { get; set; }
    }
}
