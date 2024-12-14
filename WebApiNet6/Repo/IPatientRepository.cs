using WebApiNet6.Models;
using WebApiNet6.ViewModels;
using System.Linq.Expressions;

namespace WebApiNet6.Repo
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> FindAll();
        PatientVm FindById(int id);
        bool Create(PatientVm entity);
        bool Update(PatientVm entity);
        bool Delete(int id);
        PatientMasterVm GetMaster();
    }
}
