﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public class DoctorDB : PersonDB
    {
        private String pers_doct_collegiate_uid;
        private int pers_doct_specialty_id;

        public DoctorDB()
        {
        }

        public DoctorDB(int pers_id, string pers_nif, string pers_first_name, string pers_last_name_1, string pers_last_name_2, DateTime pers_birthdate,
            string pers_phone_number, string pers_email, GenderTypeDB pers_gender, string pers_addrs_street, string pers_addrs_zip_code, string pers_addrs_city, 
            string pers_addrs_province, string pers_addrs_country, string pers_login_username, string pers_login_password, string pers_doct_collegiate_uid, int pers_doct_specialty_id) 
            : base(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2, pers_birthdate, pers_phone_number, pers_email, pers_gender, 
                  pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, pers_addrs_country, pers_login_username, pers_login_password)
        {
            this.Pers_doct_collegiate_uid = pers_doct_collegiate_uid;
            this.Pers_doct_specialty_id = pers_doct_specialty_id;
        }

        public string Pers_doct_collegiate_uid { get => pers_doct_collegiate_uid; set => pers_doct_collegiate_uid = value; }
        public int Pers_doct_specialty_id { get => pers_doct_specialty_id; set => pers_doct_specialty_id = value; }
    }
}
