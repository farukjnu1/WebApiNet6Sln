using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiNet6.Models
{
    public partial class Disease
    {
        public Disease()
        {
            Patients = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
