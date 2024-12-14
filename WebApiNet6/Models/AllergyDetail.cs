using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiNet6.Models
{
    public partial class AllergyDetail
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? AllergyId { get; set; }

        public virtual Allergy Allergy { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
