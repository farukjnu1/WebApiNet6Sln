using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebApiNet6.Models;
using WebApiNet6.ViewModels;
using System.Linq.Expressions;
using System.Transactions;

namespace WebApiNet6.Repo
{
    public class PatientRepository:IPatientRepository
    {
        private readonly hospitaldbContext _context;

        public PatientRepository(hospitaldbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Patient> FindAll()
        {
            return _context.Patients.ToList();
        }

        public PatientVm FindById(int id)
        {
            PatientVm patientVm = new PatientVm();
            using (var ctx = _context)
            {
                //context.Database.Log = Console.Write;
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Patient oPatient = ctx.Patients.Find(id);
                        if (oPatient != null)
                        {
                            patientVm = new PatientVm();
                            patientVm.Id = id;
                            patientVm.Name = oPatient.Name;
                            patientVm.DiseaseId = oPatient.DiseaseId;
                            patientVm.EpilepsyId = oPatient.EpilepsyId;
                            patientVm.Gender = oPatient.Gender;
                            patientVm.DateOfBirth = oPatient.DateOfBirth;
                            var NCDs = new List<NcdDetailVm>();
                            var listNcdDetail = (from x in ctx.NcdDetails where x.PatientId == id
                                                 select new NcdDetailVm
                                                 {
                                                     Id = x.Id,
                                                     PatientId = x.PatientId,
                                                     NcdId = x.NcdId 
                                                 }).ToList();
                            patientVm.NCDs = listNcdDetail;
                            var listAllergyDetail = (from x in ctx.AllergyDetails
                                                 where x.PatientId == id
                                                 select new AllergyDetailVm
                                                 {
                                                     Id = x.Id,
                                                     PatientId = x.PatientId,
                                                     AllergyId = x.AllergyId
                                                 }).ToList();
                            patientVm.Allergies = listAllergyDetail;
                        }
                    }
                    catch //(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return patientVm;
        }

        public bool Create(PatientVm model)
        {
            bool isEffected = false;
            using (var ctx = _context)
            {
                //context.Database.Log = Console.Write;
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        #region Patient
                        Patient oPatient = new Patient();
                        oPatient.Name = model.Name;
                        oPatient.DiseaseId = model.DiseaseId;
                        oPatient.EpilepsyId = model.EpilepsyId;
                        ctx.Add(oPatient);
                        ctx.SaveChanges();
                        #endregion
                        #region NcdDetail
                        var listNcdDetail = new List<NcdDetail>();
                        foreach (var ncd in model.NCDs)
                        {
                            var oNcdDetail = new NcdDetail();
                            oNcdDetail.PatientId = oPatient.Id;
                            oNcdDetail.NcdId = ncd.NcdId;
                            listNcdDetail.Add(oNcdDetail);
                        }
                        ctx.NcdDetails.AddRange(listNcdDetail);
                        ctx.SaveChanges();
                        #endregion
                        #region AllergyDetail
                        var listAllergyDetail = new List<AllergyDetail>();
                        foreach (var allergy in model.Allergies)
                        {
                            var oAllergyDetail = new AllergyDetail();
                            oAllergyDetail.PatientId = oPatient.Id;
                            oAllergyDetail.AllergyId = allergy.AllergyId;
                            listAllergyDetail.Add(oAllergyDetail);
                        }
                        ctx.AllergyDetails.AddRange(listAllergyDetail);
                        ctx.SaveChanges();
                        #endregion
                        transaction.Commit();
                        isEffected = true;
                    }
                    catch //(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return isEffected;
        }

        public bool Update(PatientVm model)
        {
            bool isEffected = false;
            using (var ctx = _context)
            {
                //context.Database.Log = Console.Write;
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        #region Delete

                        #region NcdDetail
                        var listNcdDetail = (from x in ctx.NcdDetails.Where(x => x.PatientId == model.Id) select x).ToList();
                        ctx.RemoveRange(listNcdDetail);
                        ctx.SaveChanges();
                        #endregion

                        #region AllergyDetail
                        var listAllergyDetail = (from x in ctx.AllergyDetails.Where(x => x.PatientId == model.Id) select x).ToList();
                        ctx.RemoveRange(listAllergyDetail);
                        ctx.SaveChanges();
                        #endregion

                        #endregion

                        #region Insert

                        #region Patient
                        Patient oPatient = ctx.Patients.Find(model.Id);
                        if (oPatient != null) 
                        {
                            #region Update
                            oPatient.Name = model.Name;
                            oPatient.DiseaseId = model.DiseaseId;
                            oPatient.EpilepsyId = model.EpilepsyId;
                            ctx.SaveChanges();
                            #endregion

                            #region NcdDetail
                            listNcdDetail = new List<NcdDetail>();
                            foreach (var ncd in model.NCDs)
                            {
                                var oNcdDetail = new NcdDetail();
                                oNcdDetail.PatientId = oPatient.Id;
                                oNcdDetail.NcdId = ncd.NcdId;
                                listNcdDetail.Add(oNcdDetail);
                            }
                            ctx.NcdDetails.AddRange(listNcdDetail);
                            ctx.SaveChanges();
                            #endregion

                            #region AllergyDetail
                            listAllergyDetail = new List<AllergyDetail>();
                            foreach (var allergy in model.Allergies)
                            {
                                var oAllergyDetail = new AllergyDetail();
                                oAllergyDetail.PatientId = oPatient.Id;
                                oAllergyDetail.AllergyId = allergy.AllergyId;
                                listAllergyDetail.Add(oAllergyDetail);
                            }
                            ctx.AllergyDetails.AddRange(listAllergyDetail);
                            ctx.SaveChanges();
                            #endregion
                        }
                        #endregion

                        #endregion

                        transaction.Commit();
                        isEffected = true;
                    }
                    catch //(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return isEffected;
        }

        public bool Delete(int id)
        {
            bool isEffected = false;
            using (var ctx = _context)
            {
                //context.Database.Log = Console.Write;
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        #region NcdDetail
                        var listNcdDetail = (from x in ctx.NcdDetails.Where(x=>x.PatientId == id) select x).ToList();
                        ctx.RemoveRange(listNcdDetail);
                        ctx.SaveChanges();
                        #endregion

                        #region AllergyDetail
                        var listAllergyDetail = (from x in ctx.AllergyDetails.Where(x => x.PatientId == id) select x).ToList();
                        ctx.RemoveRange(listAllergyDetail);
                        ctx.SaveChanges();
                        #endregion

                        #region Patient
                        Patient oPatient = ctx.Patients.Find(id);
                        ctx.Remove(oPatient);
                        ctx.SaveChanges();
                        #endregion

                        transaction.Commit();
                        isEffected = true;
                    }
                    catch //(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return isEffected;
        }

        public PatientMasterVm GetMaster()
        {
            PatientMasterVm patientMaster = new PatientMasterVm();
            using (var ctx = _context)
            {
                //context.Database.Log = Console.Write;
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        patientMaster.Allergies = (from x in ctx.Allergies
                                             select new AllergyVm
                                             {
                                                 Id = x.Id,
                                                 Name = x.Name,
                                             }).ToList();
                        patientMaster.Diseases = (from x in ctx.Diseases
                                                   select new DiseaseVm
                                                   {
                                                       Id = x.Id,
                                                       Name = x.Name,
                                                   }).ToList();
                        patientMaster.NCDs = (from x in ctx.Ncds
                                                  select new NcdVm
                                                  {
                                                      Id = x.Id,
                                                      Name = x.Name,
                                                  }).ToList();
                        var Epilepsies = Enum.GetValues(typeof(Epilepsy)).Cast<Epilepsy>();
                        var listEpilepsy = new List<EpilepsyVm>();
                        foreach (var epilepsy in Epilepsies)
                        {
                            listEpilepsy.Add(new EpilepsyVm() { Id = (int)epilepsy, Name = epilepsy.ToString() });
                        }
                        patientMaster.Epilepsies = listEpilepsy;
                    }
                    catch //(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return patientMaster;
        }
    }
}
