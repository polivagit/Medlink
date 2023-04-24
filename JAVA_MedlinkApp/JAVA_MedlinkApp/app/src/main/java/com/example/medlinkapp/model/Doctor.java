package com.example.medlinkapp.model;

import java.util.Date;

public class Doctor extends Person{

    //region FIELDS --------------------------------------------------------------------------------
    private String pers_doct_collegiate_uid;
    private int pers_doct_specialty_id;
    //endregion

    //region CONSTRUCTORS --------------------------------------------------------------------------
    public Doctor() {
    }

    public Doctor(int pers_id, String pers_nif, String pers_first_name, String pers_last_name_1,
                  String pers_last_name_2, Date pers_birthdate, String pers_phone_number,
                  String pers_email, GenderType pers_gender, String pers_addrs_street,
                  String pers_addrs_zip_code, String pers_addrs_city, String pers_addrs_province,
                  String pers_addrs_country, String pers_login_username, String pers_login_password,
                  String pers_doct_collegiate_uid, int pers_doct_specialty_id) {
        super(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                pers_birthdate, pers_phone_number, pers_email, pers_gender, pers_addrs_street,
                pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, pers_addrs_country,
                pers_login_username, pers_login_password);
        this.pers_doct_collegiate_uid = pers_doct_collegiate_uid;
        this.pers_doct_specialty_id = pers_doct_specialty_id;
    }

    public Doctor(int pers_id, String pers_nif, String pers_first_name, String pers_last_name_1,
                  Date pers_birthdate, String pers_phone_number, String pers_email,
                  GenderType pers_gender, String pers_addrs_street, String pers_addrs_zip_code,
                  String pers_addrs_city, String pers_addrs_province, String pers_addrs_country,
                  String pers_login_username, String pers_login_password,
                  String pers_doct_collegiate_uid, int pers_doct_specialty_id) {
        super(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_birthdate,
                pers_phone_number, pers_email, pers_gender, pers_addrs_street, pers_addrs_zip_code,
                pers_addrs_city, pers_addrs_province, pers_addrs_country,
                pers_login_username, pers_login_password);
        this.pers_doct_collegiate_uid = pers_doct_collegiate_uid;
        this.pers_doct_specialty_id = pers_doct_specialty_id;
    }
    //endregion

    //region GETTERS & SETTERS ---------------------------------------------------------------------
    public String getPers_doct_collegiate_uid() {
        return pers_doct_collegiate_uid;
    }

    public void setPers_doct_collegiate_uid(String pers_doct_collegiate_uid) {
        this.pers_doct_collegiate_uid = pers_doct_collegiate_uid;
    }

    public int getPers_doct_specialty_id() {
        return pers_doct_specialty_id;
    }

    public void setPers_doct_specialty_id(int pers_doct_specialty_id) {
        this.pers_doct_specialty_id = pers_doct_specialty_id;
    }
    //endregion
}
