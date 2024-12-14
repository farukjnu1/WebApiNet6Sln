using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiNet6.Models
{
    public partial class Patient
    {
        public Patient()
        {
            AllergyDetails = new HashSet<AllergyDetail>();
            NcdDetails = new HashSet<NcdDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? DiseaseId { get; set; }
        public int? EpilepsyId { get; set; }

        public virtual Disease Disease { get; set; }
        public virtual ICollection<AllergyDetail> AllergyDetails { get; set; }
        public virtual ICollection<NcdDetail> NcdDetails { get; set; }
    }
}
