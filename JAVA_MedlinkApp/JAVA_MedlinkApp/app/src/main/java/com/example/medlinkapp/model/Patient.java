package com.example.medlinkapp.model;

import java.util.Date;

public class Patient extends Person{

    private int pers_pati_height;
    private float pers_pati_weight;
    private String pers_pati_remarks;
    private int pers_pati_caregiver_id;

    //region CONSTRUCTORS ---------------------------------------------------------------------
    public Patient (){

    }

    public Patient (int pers_id, String pers_nif, String pers_first_name, String pers_last_name_1,
                  String pers_last_name_2, Date pers_birthdate, String pers_phone_number,
                  String pers_email, GenderType pers_gender, String pers_addrs_street,
                  String pers_addrs_zip_code, String pers_addrs_city, String pers_addrs_province,
                  String pers_addrs_country, String pers_login_username, String pers_login_password,
                  int pers_pati_height,float pers_pati_weight,String pers_pati_remarks,int pers_pati_caregiver_id) {
        super(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                pers_birthdate, pers_phone_number, pers_email, pers_gender, pers_addrs_street,
                pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, pers_addrs_country,
                pers_login_username, pers_login_password);
        this.setPers_pati_height(pers_pati_height);
        this.setPers_pati_weight(pers_pati_weight);
        this.setPers_pati_remarks(pers_pati_remarks);
        this.setPers_pati_caregiver_id(pers_pati_caregiver_id);
    }


    //endregion

    //region GETTERS & SETTERS ---------------------------------------------------------------------
    public int getPers_pati_height() {
        return pers_pati_height;
    }

    public void setPers_pati_height(int pers_pati_height) {
        this.pers_pati_height = pers_pati_height;
    }

    public float getPers_pati_weight() {
        return pers_pati_weight;
    }

    public void setPers_pati_weight(float pers_pati_weight) {
        this.pers_pati_weight = pers_pati_weight;
    }

    public String getPers_pati_remarks() {
        return pers_pati_remarks;
    }

    public void setPers_pati_remarks(String pers_pati_remarks) {
        this.pers_pati_remarks = pers_pati_remarks;
    }

    public int getPers_pati_caregiver_id() {
        return pers_pati_caregiver_id;
    }

    public void setPers_pati_caregiver_id(int pers_pati_caregiver_id) {
        this.pers_pati_caregiver_id = pers_pati_caregiver_id;
    }
    //endregion
}
