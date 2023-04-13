using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public class PatientDB : PersonDB
    {
        private int pers_pati_height;
        private float pers_pati_weight;
        private String pers_pati_remarks;   //nullable
        private int pers_pati_caregiver_id;

        public PatientDB()
        {
        }

        public PatientDB(int pers_id, String pers_nif, String pers_first_name, String pers_last_name_1, String pers_last_name_2, 
            DateTime pers_birthdate, String pers_phone_number, String pers_email, GenderDB pers_gender, String pers_address, 
            String pers_login_username, String pers_login_password, bool pers_is_caregiver, PersonTypeDB pers_type,
            int pers_pati_height, float pers_pati_weight, String pers_pati_remarks, int pers_pati_caregiver_id) 
            : base(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2, pers_birthdate, pers_phone_number,
                  pers_email, pers_gender, pers_address, pers_login_username, pers_login_password, pers_is_caregiver, pers_type)
        {
            this.Pers_pati_height = pers_pati_height;
            this.Pers_pati_weight = pers_pati_weight;
            this.Pers_pati_remarks = pers_pati_remarks;
            this.Pers_pati_caregiver_id = pers_pati_caregiver_id;
        }

        public int Pers_pati_height { get => pers_pati_height; set => pers_pati_height = value; }
        public float Pers_pati_weight { get => pers_pati_weight; set => pers_pati_weight = value; }
        public string Pers_pati_remarks { get => pers_pati_remarks; set => pers_pati_remarks = value; }
        public int Pers_pati_caregiver_id { get => pers_pati_caregiver_id; set => pers_pati_caregiver_id = value; }
    }
}
