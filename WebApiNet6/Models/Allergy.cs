using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiNet6.Models
{
    public partial class Allergy
    {
        public Allergy()
        {
            AllergyDetails = new HashSet<AllergyDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AllergyDetail> AllergyDetails { get; set; }
    }
}
