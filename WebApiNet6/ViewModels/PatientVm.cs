namespace WebApiNet6.ViewModels
{
    public class PatientVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? DiseaseId { get; set; }
        public int? EpilepsyId { get; set; }
        public List<NcdDetailVm> NCDs { get; set; } = new List<NcdDetailVm>();
        public List<AllergyDetailVm> Allergies { get; set; } = new List<AllergyDetailVm>();
    }
}
