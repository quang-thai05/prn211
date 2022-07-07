using ProjectPRN211.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPRN211.Logics
{
    public class HospitalManager
    {
        PRN211_ProjectContext context;

        public HospitalManager()
        {
            context = new PRN211_ProjectContext();
        }

        public List<Hospital> GetHospitals()
        {
            return context.Hospitals.ToList();
        }

        public Hospital GetHospitalById(int id)
        {
            return context.Hospitals.FirstOrDefault(x => x.HospitalId == id);
        }
    }
}
