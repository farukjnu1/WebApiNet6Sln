namespace WebApiNet6.ViewModels
{
    public class PatientMasterVm
    {
        public List<AllergyVm> Allergies { get; set; } = new List<AllergyVm>();
        public List<DiseaseVm> Diseases { get; set; } = new List<DiseaseVm>();
        public List<NcdVm> NCDs { get; set; } = new List<NcdVm>();
        public List<EpilepsyVm> Epilepsies { get; set; } = new List<EpilepsyVm>();
    }
}
