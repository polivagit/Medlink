using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public class TreatmentDB
    {
        private int trea_id;
        private String trea_name;
        private String trea_description;
        private DateTime trea_date_start;
        private DateTime trea_date_end;
        private String trea_observations;
        private bool trea_is_active;
        private int trea_doctor_id;
        private int trea_patient_id;

        public TreatmentDB()
        {
        }

        public TreatmentDB(int trea_id, string trea_name, string trea_description, 
            DateTime trea_date_start, DateTime trea_date_end, string trea_observations,
            bool trea_is_active, int trea_doctor_id, int trea_patient_id)
        {
            this.Trea_id = trea_id;
            this.Trea_name = trea_name;
            this.Trea_description = trea_description;
            this.Trea_date_start = trea_date_start;
            this.Trea_date_end = trea_date_end;
            this.Trea_observations = trea_observations;
            this.Trea_is_active = trea_is_active;
            this.Trea_doctor_id = trea_doctor_id;
            this.Trea_patient_id = trea_patient_id;
        }

        public TreatmentDB(int trea_id, string trea_name, string trea_description,
            DateTime trea_date_start, DateTime trea_date_end,
            bool trea_is_active, int trea_doctor_id, int trea_patient_id)
        {
            this.Trea_id = trea_id;
            this.Trea_name = trea_name;
            this.Trea_description = trea_description;
            this.Trea_date_start = trea_date_start;
            this.Trea_date_end = trea_date_end;
            this.Trea_is_active = trea_is_active;
            this.Trea_doctor_id = trea_doctor_id;
            this.Trea_patient_id = trea_patient_id;
        }

        public int Trea_id { get => trea_id; set => trea_id = value; }
        public string Trea_name { get => trea_name; set => trea_name = value; }
        public string Trea_description { get => trea_description; set => trea_description = value; }
        public DateTime Trea_date_start { get => trea_date_start; set => trea_date_start = value; }
        public DateTime Trea_date_end { get => trea_date_end; set => trea_date_end = value; }
        public string Trea_observations { get => trea_observations; set => trea_observations = value; }
        public bool Trea_is_active { get => trea_is_active; set => trea_is_active = value; }
        public int Trea_doctor_id { get => trea_doctor_id; set => trea_doctor_id = value; }
        public int Trea_patient_id { get => trea_patient_id; set => trea_patient_id = value; }

        public override bool Equals(object obj)
        {
            return obj is TreatmentDB dB &&
                   trea_id == dB.trea_id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(trea_id);
        }
    }
}
